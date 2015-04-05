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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display a cross tabulation table of the results
	/// </summary>
    public partial class ResultsCrossTabulation : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Literal SurveyCrossTabTitle;
		protected System.Web.UI.WebControls.PlaceHolder CrossTabResultsPlaceHolder;
		protected System.Web.UI.WebControls.DropDownList BaseQuestionDropDownList;
		protected System.Web.UI.WebControls.DropDownList CompareQuestionDropDownList;
		protected System.Web.UI.WebControls.Literal BaseLabel;
		protected System.Web.UI.WebControls.Label CompareQuestionLabel;
		protected System.Web.UI.WebControls.Label BaseQuestionChoiceLabel;
		protected System.Web.UI.WebControls.Label CompareQuestionChoiceLabel;
		protected System.Web.UI.WebControls.Label BaseQuestionLabel;
		protected SurveyListControl SurveyList;

		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetResultsTabs((MsterPageTabs)Page.Master, UITabList.ResultTabs.Reports);

			SetupSecurity();
			LocalizePage();

			if (!Page.IsPostBack)
			{
			
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				FillFields();
			}
		}
        protected void rbListSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblReports.SelectedValue)
            {
                case "GR": Response.Redirect(UINavigator.ResultsReportHyperlink); break;
                case "TR": Response.Redirect(UINavigator.FieldsReportHyperlink); break;
                case "CTR": Response.Redirect(UINavigator.CrossTabHyperLink); break;
            }
        }
		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessCrossTab, true);
		}

		private void LocalizePage()
		{
			CompareQuestionLabel.Text = GetPageResource("CompareQuestionLabel");
			BaseQuestionLabel.Text = GetPageResource("BaseQuestionLabel");
			SurveyCrossTabTitle.Text = GetPageResource("SurveyCrossTabTitle");
			BaseQuestionChoiceLabel.Text = GetPageResource("BaseQuestionChoiceLabel");
			CompareQuestionChoiceLabel.Text = GetPageResource("CompareQuestionChoiceLabel");
            TranslateListControl(rblReports);
		}


		/// <summary>
		/// Get the current DB stats and fill 
		/// the label with them
		/// </summary>
		private void FillFields()
		{
			BaseQuestionDropDownList.DataSource = new Questions().GetQuestionListWithSelectableAnswers(SurveyId);
			BaseQuestionDropDownList.DataTextField = "QuestionText";
			BaseQuestionDropDownList.DataValueField = "QuestionID";
			BaseQuestionDropDownList.DataBind();
			BaseQuestionDropDownList.Items.Insert(0, new ListItem(GetPageResource("SelectQuestionMessage"), "-1"));

			CompareQuestionDropDownList.DataSource = new Questions().GetQuestionListWithSelectableAnswers(SurveyId);
			CompareQuestionDropDownList.DataTextField = "QuestionText";
			CompareQuestionDropDownList.DataValueField = "QuestionID";
			CompareQuestionDropDownList.DataBind();
			CompareQuestionDropDownList.Items.Insert(0, new ListItem(GetPageResource("SelectQuestionMessage"), "-1"));

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
			this.BaseQuestionDropDownList.SelectedIndexChanged += new System.EventHandler(this.BaseQuestionDropDownList_SelectedIndexChanged);
			this.CompareQuestionDropDownList.SelectedIndexChanged += new System.EventHandler(this.BaseQuestionDropDownList_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private TableRow GenerateBaseRow(AnswerData.AnswersRow baseAnswer, int compareQuestionId, DataSet totalCountData)
		{
			TableRow baseRow = new TableRow();
			TableCell baseCell = new TableCell();
			baseCell.Wrap = false;
			baseCell.CssClass = "crossTabBaseCell";
			baseCell.Text = baseAnswer.AnswerText;
			baseRow.Cells.Add(baseCell);

			DataSet crossTabStats = new Questions().GetCrossTabResults(
				compareQuestionId, baseAnswer.AnswerId);

			for (int i=0;i<crossTabStats.Tables[0].Rows.Count;i++)
			{
				baseCell = new TableCell();
				baseCell.Wrap = false;

				double columnTotal = Convert.ToDouble(totalCountData.Tables[0].Rows[i].ItemArray[0]);
				double voterTotal = Convert.ToDouble(crossTabStats.Tables[0].Rows[i].ItemArray[0]);
				if (voterTotal > 0 && columnTotal>0)
				{
					baseCell.Text = string.Format("{0}% ({1})", ((voterTotal/columnTotal)*100).ToString("##.##"), voterTotal);
				}
				else
				{
					baseCell.Text = "0% (0)";
				}

				baseRow.Cells.Add(baseCell);
			}

			return baseRow;
		}

		private TableRow GenerateTotalRow(DataSet totalCountData)
		{
			TableRow totalRow= new TableRow();
			TableCell totalCell = new TableCell();
			
			totalCell.Wrap = false;
			totalCell.Text = GetPageResource("CrossTabVotesTotalMessage");
			totalRow.Cells.Add(totalCell);

			foreach (DataRow totalCount in totalCountData.Tables[0].Rows)
			{
				totalCell = new TableCell();
				totalCell.Wrap = false;

				totalCell.Text = totalCount.ItemArray[0].ToString();
				totalRow.Cells.Add(totalCell);
			}

			return totalRow;
		}

		private TableRow GenerateNotAnsweredRow(int compareQuestionId, int baseQuestionId, DataSet totalCountData)
		{
			TableRow baseRow = new TableRow();
			TableCell baseCell = new TableCell();
			baseCell.Wrap = false;
			baseCell.CssClass = "crossTabBaseCell";
			baseCell.Text = GetPageResource("CrossTabNotAnsweredMessage");
			baseRow.Cells.Add(baseCell);

			DataSet crossTabStats = new Questions().GetUnansweredCrossTabResults(compareQuestionId, baseQuestionId);

			for (int i=0;i<crossTabStats.Tables[0].Rows.Count;i++)
			{
				baseCell = new TableCell();
				baseCell.Wrap = false;

				double columnTotal = Convert.ToDouble(totalCountData.Tables[0].Rows[i].ItemArray[0]);
				double voterTotal = Convert.ToDouble(crossTabStats.Tables[0].Rows[i].ItemArray[0]);
				if (voterTotal > 0 && columnTotal>0)
				{
					baseCell.Text = string.Format("{0}% ({1})", ((voterTotal/columnTotal)*100).ToString("##.##"), voterTotal);
				}
				else
				{
					baseCell.Text = "0% (0)";
				}

				baseRow.Cells.Add(baseCell);
			}

			return baseRow;
		}

		private TableRow GenerateTableHeader(AnswerData compareAnswers)
		{
			TableRow header = new TableRow();
			TableCell headerCell = new TableCell();
			header.Cells.Add(headerCell);

			foreach (AnswerData.AnswersRow answer in compareAnswers.Answers.Rows)
			{
				headerCell = new TableCell();
				headerCell.Wrap = false;
				headerCell.Text = answer.AnswerText;
				headerCell.CssClass = "crossTabCompareCell";
				header.Cells.Add(headerCell);
			}

			return header;
		}

		private void BaseQuestionDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (BaseQuestionDropDownList.SelectedValue != "-1")
			{
				BaseQuestionChoiceLabel.Text = BaseQuestionDropDownList.SelectedItem.Text;
			}
			else
			{
				BaseQuestionChoiceLabel.Text = GetPageResource("BaseQuestionChoiceLabel");
			}

			if (CompareQuestionDropDownList.SelectedValue != "-1")
			{
				CompareQuestionChoiceLabel.Text = CompareQuestionDropDownList.SelectedItem.Text;
			}
			else
			{
				CompareQuestionChoiceLabel.Text = GetPageResource("CompareQuestionChoiceLabel");
			}

			if (BaseQuestionDropDownList.SelectedValue != "-1" && CompareQuestionDropDownList.SelectedValue != "-1" &&
				BaseQuestionDropDownList.SelectedValue != CompareQuestionDropDownList.SelectedValue)
			{
				BuildCrossTabTabe();
			}

		
		}


		void BuildCrossTabTabe()
		{
			AnswerData 
				baseAnswers = new Answers().GetSelectableAnswers(int.Parse(BaseQuestionDropDownList.SelectedValue)),
				compareAnswers = new Answers().GetSelectableAnswers(int.Parse(CompareQuestionDropDownList.SelectedValue));

			int compareQuestionId = int.Parse(CompareQuestionDropDownList.SelectedValue),
				baseQuestionId = int.Parse(BaseQuestionDropDownList.SelectedValue);

			DataSet totalCountData = new Questions().GetTotalCrossTabResults(compareQuestionId, baseQuestionId);


			Table crossTable = new Table();
			crossTable.CssClass = "crossTabTable";

			crossTable.Rows.Add(GenerateTableHeader(compareAnswers));

			crossTable.Rows.Add(GenerateNotAnsweredRow(compareQuestionId, baseQuestionId, totalCountData));

			foreach (AnswerData.AnswersRow answer in baseAnswers.Answers.Rows)
			{
				crossTable.Rows.Add(GenerateBaseRow(answer, compareQuestionId, totalCountData));
			}

			if (totalCountData.Tables.Count > 0)
			{
				TableRow breakRow= new TableRow();
				TableCell breakCell = new TableCell();
				breakCell.ColumnSpan = totalCountData.Tables[0].Rows.Count + 1;
                breakCell.Text = "&nbsp;";
				breakRow.Cells.Add(breakCell);
				crossTable.Rows.Add(breakRow);
			}

			crossTable.Rows.Add(GenerateTotalRow(totalCountData));
				
			CrossTabResultsPlaceHolder.Controls.Add(crossTable);
		}



	}

}
