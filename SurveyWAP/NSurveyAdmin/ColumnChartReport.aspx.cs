/**************************************************************************************************
	Survey Project changes: copyright (c) 2014, W3DevPro TM (http://survey.codeplex.com)	

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.DataVisualization.Charting;

using Microsoft.VisualBasic;

using Votations.NSurvey;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Enums;
using Votations.NSurvey.Resources;
using Votations.NSurvey.Web;
using Votations.NSurvey.BusinessRules;
using System.Text.RegularExpressions;

namespace Votations.NSurvey.WebAdmin
{
    public partial class ColumnChartReport : PageBase
    {
        QuestionResultsData _dataSource = new QuestionResultsData();
        int _questionId,
            _filterId,
            _enable3D,
            _chartType;
        string _sortOrder;

        public int SurveyId
        {
            get { return ((PageBase)Page).getSurveyId(); }
            set { ((PageBase)Page).SurveyId = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _questionId = Information.IsNumeric(Request["QuestionId"]) ? int.Parse(Request["QuestionId"]) : -1;
            _filterId = Information.IsNumeric(Request["FilterId"]) ? int.Parse(Request["FilterId"]) : -1;
            _sortOrder = Request["SortOrder"] == null ? "ans" : Request["SortOrder"];
            _chartType = Information.IsNumeric(Request["ChartType"]) ? int.Parse(Request["ChartType"]) : 1; 
            _enable3D = Information.IsNumeric(Request["Enable3D"]) ? int.Parse(Request["Enable3D"]) : 2;


            if (_questionId == -1)
            {
                Response.End();
            }
            else if (!NSurveyUser.Identity.IsAdmin &&
                !NSurveyUser.Identity.HasAllSurveyAccess)
            {
                if (!new Question().CheckQuestionUser(_questionId, NSurveyUser.Identity.UserId))
                {
                    Response.End();
                }
            }

            // Chart: Set series and chart type etc.:
            if (_chartType == 4)
            {
                Chart1.Series["Series1"].ChartType = SeriesChartType.Bar;
            }
            else if (_chartType == 5)
            {
                Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
            }
            else if (_chartType == 3)
            {
                Chart1.Series["Series1"].ChartType = SeriesChartType.Bubble;
            }
            else
            {
                Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
            }

               //Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
               //Chart1.Titles[0].Text = "SP Column Chart";


            // Add custom legend item
            this.AddLegendItem();

               SetQuestionData(Chart1);

            // Set point width of the series
            Chart1.Series["Series1"]["PointWidth"] = "0.8";

            // Set drawing style
            Chart1.Series["Series1"]["DrawingStyle"] = "Default";

            // Show as 2D or 3D
            if (_enable3D == 1)
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            else
                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            // Show point labels
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.Series["Series1"].LabelFormat = "P0";

            Chart1.Series["Series1"].Name = "Results";

            Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format =  "{0:0%}";


        }




        protected void SetQuestionData(Chart chart1)
        {
            try
            {
                DateTime startDate = Information.IsDate(Request["StartDate"]) ?
                    DateTime.Parse(Request["StartDate"]) : new DateTime(1900, 1, 1),
                    endDate = Information.IsDate(Request["EndDate"]) ?
                    DateTime.Parse(Request["EndDate"]) : new DateTime(2100, 1, 1);

                _dataSource = new Questions().GetQuestionResults(_questionId, _filterId, _sortOrder,
                    Request["LanguageCode"], startDate, endDate);
            }
            catch (QuestionNotFoundException)
            {
                return;
            }

            QuestionResultsData.AnswersRow[] answers = _dataSource.Questions[0].GetAnswersRows();


            // Do we need to show the parent question text for matrix based child questions
            if (!_dataSource.Questions[0].IsParentQuestionIdNull() && _dataSource.Questions[0].ParentQuestionText != null)
            {
                String questionText = String.Format("{0} - {1}",
                    _dataSource.Questions[0].ParentQuestionText,
                    _dataSource.Questions[0].QuestionText);

                // Show parent and child question text
                Chart1.Titles[0].Text = Server.HtmlDecode(questionText);
            }
            else
            {
                // Show question text
                Chart1.Titles[0].Text = Server.HtmlDecode(_dataSource.Questions[0].QuestionText);
            }

            SetAnswerData(answers, GetVotersTotal(answers), Chart1);
          

        }

        private float GetVotersTotal(QuestionResultsData.AnswersRow[] answers)
        {
            float total = 0;
            foreach (QuestionResultsData.AnswersRow answer in answers)
            {
                // Increment only selection answers that can be selected
                if ((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) > 0))
                {
                    total += answer.VoterCount;
                }
            }
            return total;
        }

        private void SetAnswerData(QuestionResultsData.AnswersRow[] answers, float totalOfVotes, Chart chart1)
        {
            float currentRate = 0;
            float totalRate = 0;
            float totalRateVotes = 0;

            string[] xval = new string[answers.Count()];
            int i = 0;
            double[] yval = new double[answers.Count()];
            int p = 0;

            foreach (QuestionResultsData.AnswersRow answer in answers)
            {
                if ((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) > 0))
                {
                    float VotePercent = 0;

                    if (totalOfVotes != 0)
                    {
                        if (answer.VoterCount == 0)
                            VotePercent = 0;
                        else
                            VotePercent = ((float)answer.VoterCount / (float)totalOfVotes) * 100;
                    }

                    // Add answer text & vote count
                    answer.AnswerText = Server.HtmlDecode(System.Text.RegularExpressions.Regex.Replace(answer.AnswerText, "<[^>]*>", " "));
                    string answerChartText;
                    answerChartText = string.Format("{0} ({1})",
                            answer.AnswerText, answer.VoterCount.ToString());

                    //columnChart.Data.Add(new ChartPoint(answerChartText, VotePercent));

                    xval[i++] = answerChartText;
                    yval[p++] = VotePercent/100;

                    // Do we include this answer in the
                    // rating total
                    if (answer.RatePart)
                    {
                        currentRate++;
                        totalRate += currentRate * answer.VoterCount;
                        totalRateVotes += answer.VoterCount;
                    }
                }
            }

            Chart1.Series["Series1"].Points.DataBindXY(xval, yval);

            // Do we show the average rating 
            if (_dataSource.Questions[0].RatingEnabled)
            {
                double meanRate = 0;

                if (totalOfVotes == 0)
                    meanRate = 0;
                else
                    meanRate = totalRate / totalRateVotes;

                Chart1.Legends["Default"].CustomItems[0].Cells[0].Text = string.Format("{0}{1}{2}",
                    String.Format(ResourceManager.GetString("TotalOfVotes"), totalOfVotes),
                    Environment.NewLine,
                    string.Format(ResourceManager.GetString("RatingResults"), meanRate.ToString("##.##"), currentRate));

            }
            else
            {

                Chart1.Legends["Default"].CustomItems[0].Cells[0].Text = String.Format(ResourceManager.GetString("TotalOfVotes"), totalOfVotes);
            }
        }


        // Adds one custom legend item and its cells to the legend
        private void AddLegendItem()
        {
            Chart1.Legends["Default"].CustomItems.Clear();

            // Add new custom legend item
            Chart1.Legends["Default"].CustomItems.Add(new LegendItem("LegendItem", Color.Red, ""));

            // Add five new cells to the custom legend item
            Chart1.Legends["Default"].CustomItems[0].Cells.Add(new LegendCell(LegendCellType.Text, "", ContentAlignment.MiddleLeft));
            //Chart1.Legends["Default"].CustomItems[0].Cells.Add(new LegendCell(LegendCellType.Text, "", ContentAlignment.MiddleRight));
            //Chart1.Legends["Default"].CustomItems[0].Cells.Add(new LegendCell(LegendCellType.Text, "", ContentAlignment.MiddleRight));
            //Chart1.Legends["Default"].CustomItems[0].Cells.Add(new LegendCell(LegendCellType.Image, "", ContentAlignment.MiddleLeft));
            //Chart1.Legends["Default"].CustomItems[0].Cells.Add(new LegendCell(LegendCellType.Text, "", ContentAlignment.MiddleLeft));

            Chart1.Legends["Default"].Docking = Docking.Bottom;

            // Retrieve the survey data
            SurveyData surveyData = new Surveys().GetSurveyById(SurveyId, "");
            SurveyData.SurveysRow survey = surveyData.Surveys[0];

            // Assigns the retrieved data to the correct fields
            string surveyTitle = survey.Title;

            Chart1.Legends["Default"].Title = surveyTitle;


        }




    }
}