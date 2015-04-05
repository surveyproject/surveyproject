/**************************************************************************************************
	Survey changes: copyright (c) 2010, Fryslan Webservices TM (http://survey.codeplex.com)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

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
using System.Drawing;
using System.Web;
using System.Web.UI;
using Microsoft.VisualBasic;
using WebChart;
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
	/// <summary>
	/// Summary description for PieChart.
	/// </summary>
    public partial class PieChartReport : PageBase
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
		
			_questionId = 
				Information.IsNumeric(Request["QuestionId"]) ? int.Parse(Request["QuestionId"]) : -1;
			_filterId = 
				Information.IsNumeric(Request["FilterId"]) ? int.Parse(Request["FilterId"]) : -1;
			_sortOrder =
				Request["SortOrder"] == null ? "ans" : Request["SortOrder"];

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

			ChartEngine engine = new ChartEngine();
			ChartCollection charts = new ChartCollection(engine);
			engine.Size = new Size(700, 500);
			engine.Charts = charts;
			
			
			PieChart pie = new PieChart();
			//pie.Colors = new Color[]{ Color.Red};

			ChartLegend legend = new ChartLegend();
			legend.Position = LegendPosition.Right;
			legend.Width = 200;
			legend.Background.Color = Color.FromArgb(245,249,251);
			
			pie.DataLabels.NumberFormat ="0.00";
			pie.Explosion = 6; 
			pie.Shadow.Visible = true; 
			pie.Shadow.Color=Color.LightGray; 
			pie.Shadow.OffsetY = 5; 
			engine.HasChartLegend = true;
			engine.Legend = legend;
			engine.GridLines = GridLines.None;


			SetQuestionData(engine, pie);
			SetMoreProperties(engine);

			charts.Add(pie);


			// send chart to browser
			Bitmap bmp;
			System.IO.MemoryStream memStream = new System.IO.MemoryStream();
			bmp = engine.GetBitmap();
			bmp.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);
			memStream.WriteTo(Response.OutputStream);
			Response.End();

		}

		private void SetupSecurity()
		{
			if (NSurveyUser.Identity.IsAdmin || 
				NSurveyUser.HasRight(NSurveyRights.AccessReports))
			{
				return;
			}
			else
			{
				Response.End();
			}
		}

		private void SetMoreProperties(ChartEngine engine)
		{
			engine.ChartPadding = 15;
            engine.BottomChartPadding = 10;
			engine.PlotBackground.Color = Color.FromArgb(240,244,248);
			engine.Background.Type = InteriorType.LinearGradient;
			engine.Background.EndPoint = new Point(700,400);
			engine.Background.ForeColor = Color.FromArgb(245,249,251);
			engine.Background.Color = Color.FromArgb(100,75,249);
		}

		protected void SetQuestionData(ChartEngine engine, PieChart pieChart)
		{
			try
			{
				DateTime startDate = Information.IsDate(Request["StartDate"]) ?
					DateTime.Parse(Request["StartDate"]) : new DateTime(1900,1,1),
					endDate = Information.IsDate(Request["EndDate"]) ?
					DateTime.Parse(Request["EndDate"]) : new DateTime(2100,1,1);

				_dataSource = new Questions().GetQuestionResults(_questionId, _filterId, _sortOrder, 
					Request["LanguageCode"], startDate, endDate);
			}
			catch (QuestionNotFoundException)
			{
				return;
			}

			QuestionResultsData.AnswersRow[] answers = _dataSource.Questions[0].GetAnswersRows();

			// Set-up question text
			engine.Title  = new ChartText();
			engine.Title.ForeColor = Color.FromArgb(255, 255, 255);
			engine.Title.Font = new Font("Tahoma", 9, FontStyle.Bold);

			// Do we need to show the parent question text for matrix based child questions
			if (!_dataSource.Questions[0].IsParentQuestionIdNull() && _dataSource.Questions[0].ParentQuestionText != null) 
			{
				String questionText = String.Format("{0} - {1}", 
					_dataSource.Questions[0].ParentQuestionText, 
					_dataSource.Questions[0].QuestionText);

				// Show parent and child question text
				engine.Title.Text = Server.HtmlDecode(questionText);
			} 
			else 
			{
				// Show question text
				engine.Title.Text = Server.HtmlDecode(_dataSource.Questions[0].QuestionText);
			}

			SetAnswerData(answers, GetVotersTotal(answers), engine, pieChart);
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

		private void SetAnswerData(QuestionResultsData.AnswersRow[] answers, float totalOfVotes, ChartEngine engine, WebChart.PieChart pieChart)
		{
			float currentRate = 0; 
			float totalRate = 0;
			float totalRateVotes = 0;
			
			foreach (QuestionResultsData.AnswersRow answer in answers)
			{
				if ((((AnswerTypeMode)answer.TypeMode & AnswerTypeMode.Selection) > 0))
				{
					float VotePercent = 0;

					if (totalOfVotes != 0)
					{
						if (answer.VoterCount==0)
							VotePercent = 0;		
						else
							VotePercent = ((float)answer.VoterCount / (float)totalOfVotes) * 100;
					}

					// Add answer text & vote count
					answer.AnswerText = Server.HtmlDecode(System.Text.RegularExpressions.Regex.Replace(answer.AnswerText, "<[^>]*>", " "));
					string answerChartText;
					
					if (VotePercent == 0)
					{
						answerChartText = string.Format("{0} " + Environment.NewLine +"({1} - 0%)",
							answer.AnswerText, answer.VoterCount.ToString(), VotePercent.ToString("##.##"));
					}
					else
					{
						answerChartText = string.Format("{0} " + Environment.NewLine +" ({1} - {2}%)",
							answer.AnswerText, answer.VoterCount.ToString(), VotePercent.ToString("##.##"));
					}
					
					
					pieChart.Data.Add(new ChartPoint(answerChartText, VotePercent));

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
		
			StringFormat horizontalFormat = new StringFormat();
			horizontalFormat.LineAlignment = StringAlignment.Near;
			engine.XTitle  = new ChartText();
			engine.XTitle.StringFormat = horizontalFormat;
			engine.XTitle.Font = new System.Drawing.Font("Verdana", 8);

			// Do we show the average rating 
			if (_dataSource.Questions[0].RatingEnabled)
			{
				double meanRate = 0;

				if (totalOfVotes == 0)
					meanRate = 0;
				else
					meanRate = totalRate / totalRateVotes;

				engine.XTitle.Text = string.Format("{0} - {1}",
					String.Format(ResourceManager.GetString("TotalOfVotes"), totalOfVotes),
					string.Format(ResourceManager.GetString("RatingResults"), meanRate.ToString("##.##"), currentRate));		
			}
			else
			{
				engine.XTitle.Text = String.Format(ResourceManager.GetString("TotalOfVotes"), totalOfVotes);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	
		QuestionResultsData _dataSource = new QuestionResultsData();
		int _questionId,
			_filterId;
		string _sortOrder;

	}
	
}
