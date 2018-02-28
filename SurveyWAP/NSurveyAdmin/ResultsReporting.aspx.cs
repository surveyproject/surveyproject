/**************************************************************************************************
	Survey™ Project changes: copyright (c) 2009-2017, W3DevPro™ (https://github.com/surveyproject)	

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
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.Enums;
using Votations.NSurvey.WebControls.UI;
using Votations.NSurvey.WebAdmin.UserControls;
using Microsoft.VisualBasic;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the selection results of a survey
	/// </summary>
    public partial class ResultsReporting : PageBase
	{
		//protected SurveyListControl SurveyList;
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.PlaceHolder ResultsPlaceHolder;
		protected System.Web.UI.WebControls.Repeater ChartRepeater;
		protected System.Web.UI.WebControls.DropDownList QuestionsDropDownList;
		protected System.Web.UI.WebControls.DropDownList LayoutDropDownList;
		protected System.Web.UI.WebControls.Image SingleChartImage;
		protected System.Web.UI.WebControls.DropDownList FilterDropDownList;
		protected System.Web.UI.WebControls.Literal SurveyResultsTitle;
		protected System.Web.UI.WebControls.Label QuestionsResultsDisplaylabel;
		protected System.Web.UI.WebControls.Label ResultsLayoutLabel;
		protected System.Web.UI.WebControls.Literal ApplyFilter;
		protected System.Web.UI.WebControls.Label ApplyFilterLabel;
		protected System.Web.UI.WebControls.HyperLink FilterEditorHyperLink;
		protected System.Web.UI.WebControls.Label ResultsOrderLabel;
		protected System.Web.UI.WebControls.DropDownList ResultsOrderDropDownList;
		protected System.Web.UI.WebControls.DropDownList LanguagesDropdownlist;
		protected System.Web.UI.WebControls.Label LanguageFilterLabel;
		protected System.Web.UI.WebControls.Label DateRangeLabel;
		protected System.Web.UI.WebControls.Literal DateToRangeLabel;
		protected System.Web.UI.WebControls.TextBox StartDateTextBox;
		protected System.Web.UI.WebControls.TextBox EndDateTextBox;
		protected System.Web.UI.WebControls.Button ApplyRangeButton;
		new protected HeaderControl Header;

        internal const string DdlDynamicName = "ddlDynamic-";
        internal const string PlhDynamicName = "plhDynamic-";

		private void Page_Load(object sender, System.EventArgs e)
		{
            RecreateControls(DdlDynamicName);

            UITabList.SetResultsTabs((MsterPageTabs)Page.Master, UITabList.ResultTabs.Reports);

            if (LayoutDropDownList.SelectedValue != "2")
            {
                ThreeDeeDropDownList.Visible = true;
            }
            else
            {
                ThreeDeeDropDownList.Visible = false;
            }

			SetupSecurity();
			LocalizePage();

			if (!Page.IsPostBack)
			{
		
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				QuestionsDropDownList.DataSource = new Questions().GetQuestionListWithSelectableAnswers(SurveyId);
				QuestionsDropDownList.DataTextField = "QuestionText";
				QuestionsDropDownList.DataValueField = "QuestionID";
   
				QuestionsDropDownList.DataBind();
				QuestionsDropDownList.Items.Insert(0, new ListItem(GetPageResource("DisplayAllResultsMessage"), "0"));
				QuestionsDropDownList.Items.Insert(0, new ListItem(GetPageResource("SelectQuestionMessage"), "-1"));
		
				//FilterDropDownList.DataSource = new Filters().GetFilters(SurveyId);
                FilterDropDownList.DataSource = new Filters().GetFiltersByParent(SurveyId, 0);
				FilterDropDownList.DataMember = "Filters";
				FilterDropDownList.DataTextField = "Description";
				FilterDropDownList.DataValueField = "FilterID";
				FilterDropDownList.DataBind();
				FilterDropDownList.Items.Insert(0, 
					new ListItem(GetPageResource("SelectFilterMessage"),"-1"));

				LayoutDropDownList.Items.Add(new ListItem(GetPageResource("ColumnChartOption"),"1"));
				LayoutDropDownList.Items.Add(new ListItem(GetPageResource("HTMLChartOption"),"2"));
				LayoutDropDownList.Items.Add(new ListItem("Bubble Chart","3"));
                LayoutDropDownList.Items.Add(new ListItem("Bar Chart", "4"));
                LayoutDropDownList.Items.Add(new ListItem("Pie Chart", "5"));

                ThreeDeeDropDownList.Items.Add(new ListItem("3D", "1"));
                ThreeDeeDropDownList.Items.Add(new ListItem("2D", "2"));                   

				FilterEditorHyperLink.Text = GetPageResource("FilterEditorHyperLink");
				FilterEditorHyperLink.NavigateUrl = UINavigator.FilterEditor + "?SurveyId=" + SurveyId +"&menuindex="+MenuIndex;
			
				BindLanguages();
			}
		}

        protected void rbListSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblReports.SelectedValue)
            {
                case "GR": Response.Redirect(UINavigator.ResultsReportHyperlink); break;
                case "CTR": Response.Redirect(UINavigator.CrossTabHyperLink); break;
                case "SSRS": Response.Redirect(UINavigator.SSRSReportHyperlink); break;
            }
        }

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessReports, true);
			FilterEditorHyperLink.Visible = CheckRight(NSurveyRights.CreateResultsFilter, false);
            rblReports.Items[2].Enabled = CheckRight(NSurveyRights.AccessSsrsReports, false);

		}

		private void LocalizePage()
		{
			SurveyResultsTitle.Text = GetPageResource("SurveyResultsTitle");
			QuestionsResultsDisplaylabel.Text = GetPageResource("QuestionsResultsDisplaylabel");
			ResultsLayoutLabel.Text = GetPageResource("ResultsLayoutLabel");
			ApplyFilterLabel.Text = GetPageResource("ApplyFilterLabel");
			ResultsOrderLabel.Text = GetPageResource("ResultsOrderLabel");
			LanguageFilterLabel.Text = GetPageResource("LanguageFilterLabel");
			DateRangeLabel.Text = GetPageResource("DateRangeLabel");
			DateToRangeLabel.Text = GetPageResource("DateToRangeLabel");
			ApplyRangeButton.Text = GetPageResource("ApplyRangeButton");
            TranslateListControl(rblReports);
			if (!Page.IsPostBack)
			{
				ResultsOrderDropDownList.Items.Add(new ListItem(GetPageResource("AnswerOrderOption"),"ANS"));
				ResultsOrderDropDownList.Items.Add(new ListItem(GetPageResource("AscendingOrderOption"),"ASC"));
				ResultsOrderDropDownList.Items.Add(new ListItem(GetPageResource("DescendingOrderOption"),"DESC"));
             

			}

		}

		/// <summary>
		/// Get the current DB stats and build 
		/// the results tables
		/// </summary>
		private void BuildHtmlResults(int questionId)
		{
			QuestionData questionsData;
			if (questionId == 0)
			{
				questionsData = new Questions().GetQuestionListWithSelectableAnswers(SurveyId);
			}
			else
			{
				questionsData = new Questions().GetQuestionById(questionId, null);
			}


			ChartRepeater.Visible = false;
			SingleChartImage.Visible = false;
			
			Table resultsTable = new Table();
			resultsTable.CellSpacing = 10;
			TableCell resultsTableCell = new TableCell();
			TableRow resultsTableRow = new TableRow();

			resultsTable.CssClass = "resultsTable";
			foreach (QuestionData.QuestionsRow question in questionsData.Questions.Rows)
			{
					resultsTableCell = new TableCell();
					resultsTableRow = new TableRow();
					Style questionStyle = new Style();
					Style answerStyle = new Style();
					questionStyle.Font.Bold = true;
					questionStyle.Font.Size = FontUnit.XSmall;
					questionStyle.ForeColor = System.Drawing.Color.Black;
					//questionStyle.BackColor = System.Drawing.Color.FromArgb(225,238,241);
					answerStyle.Font.Size = FontUnit.XXSmall;
					ResultItem questionResults = new ResultItem();
					questionResults.BorderWidth = Unit.Pixel(1);
					questionResults.BorderColor = System.Drawing.Color.FromArgb(123,123,123);
					questionResults.BackColor = System.Drawing.Color.FromArgb(243,243,243);
					questionResults.Width = Unit.Pixel(655);
					questionResults.BarColor = "blueadmin";
					questionResults.QuestionStyle = questionStyle;
					questionResults.AnswerStyle = answerStyle;
					questionResults.FootStyle = answerStyle;
					
					DateTime startDate = Information.IsDate(StartDateTextBox.Text) ?
						DateTime.Parse(StartDateTextBox.Text) : new DateTime(1900,1,1),
						endDate = Information.IsDate(EndDateTextBox.Text) ?
						DateTime.Parse(EndDateTextBox.Text) : new DateTime(2100,1,1);
				
					questionResults.DataSource =new Questions().GetQuestionResults(question.QuestionId,
                        GetFilterId(), ResultsOrderDropDownList.SelectedValue, LanguagesDropdownlist.SelectedValue, startDate, endDate);
					
					questionResults.DataBind();
					resultsTableCell.Controls.Add(questionResults);
					
					resultsTableRow.Cells.Add(resultsTableCell);
					resultsTable.Rows.Add(resultsTableRow);
			}

			ResultsPlaceHolder.Controls.Add(resultsTable);
		}

		private void QuestionsDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (QuestionsDropDownList.SelectedValue != "-1")
			{
				if (LayoutDropDownList.SelectedValue == "2")
				{
					BuildHtmlResults(int.Parse(QuestionsDropDownList.SelectedValue));
				}
                //else if (LayoutDropDownList.SelectedValue == "3")
                //{
                //    //BuildPieChartResults(int.Parse(QuestionsDropDownList.SelectedValue));
                //    BuildBarChartResults(int.Parse(QuestionsDropDownList.SelectedValue));
                //}
                //else if (LayoutDropDownList.SelectedValue == "4")
                //{
                //    BuildBarChartResults(int.Parse(QuestionsDropDownList.SelectedValue));
                //}
				else
				{
					BuildChartColumnResults(int.Parse(QuestionsDropDownList.SelectedValue));
				}

				ApplyRangeButton.Enabled = true;
				LanguagesDropdownlist.Enabled = true;

			}
			else
			{
				ApplyRangeButton.Enabled = false;
				LanguagesDropdownlist.Enabled = false;
			}
		
		}

		void BuildChartColumnResults(int questionId)
		{
			if (questionId == 0)
			{
				ChartRepeater.Visible = true;
				SingleChartImage.Visible = false;
				ChartRepeater.DataSource = new Questions().GetQuestionListWithSelectableAnswers(SurveyId);
				ChartRepeater.DataBind();
			}
			else
			{
				ChartRepeater.Visible = false;
				SingleChartImage.Visible = true;
                //SingleChartImage.ImageUrl = Server.HtmlDecode("BarChartReport.aspx?questionid="+questionId+"&filterid="+GetFilterId().ToString()+"&SortOrder="+ResultsOrderDropDownList.SelectedValue+"&LanguageCode="+LanguagesDropdownlist.SelectedValue);
                SingleChartImage.ImageUrl = Server.HtmlDecode("ColumnChartReport.aspx?questionid=" + questionId + "&filterid=" + GetFilterId().ToString() + "&SortOrder=" + ResultsOrderDropDownList.SelectedValue + "&LanguageCode=" + LanguagesDropdownlist.SelectedValue + "&ChartType=" + LayoutDropDownList.SelectedValue + "&Enable3D=" + ThreeDeeDropDownList.SelectedValue);
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
			this.QuestionsDropDownList.SelectedIndexChanged += new System.EventHandler(this.QuestionsDropDownList_SelectedIndexChanged);
			this.LayoutDropDownList.SelectedIndexChanged += new System.EventHandler(this.QuestionsDropDownList_SelectedIndexChanged);
			this.ResultsOrderDropDownList.SelectedIndexChanged += new System.EventHandler(this.QuestionsDropDownList_SelectedIndexChanged);
			this.LanguagesDropdownlist.SelectedIndexChanged += new System.EventHandler(this.LanguagesDropdownlist_SelectedIndexChanged);
			this.ApplyRangeButton.Click += new System.EventHandler(this.ApplyRangeButton_Click);
			this.FilterDropDownList.SelectedIndexChanged += new System.EventHandler(this.FilterDropDownList_SelectedIndexChanged);
            this.ThreeDeeDropDownList.SelectedIndexChanged += new System.EventHandler(this.QuestionsDropDownList_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void FilterDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (QuestionsDropDownList.SelectedValue != "-1")
			{
				if (LayoutDropDownList.SelectedValue == "2")
				{
					BuildHtmlResults(int.Parse(QuestionsDropDownList.SelectedValue));
				}
				else if (LayoutDropDownList.SelectedValue == "3")
				{
					//BuildPieChartResults(int.Parse(QuestionsDropDownList.SelectedValue));
                    BuildChartColumnResults(int.Parse(QuestionsDropDownList.SelectedValue));
				}
				else
				{
					BuildChartColumnResults(int.Parse(QuestionsDropDownList.SelectedValue));
				}
			}		
		}

		private void BindLanguages()
		{
			MultiLanguageMode languageMode = new MultiLanguages().GetMultiLanguageMode(SurveyId);
			if (languageMode != MultiLanguageMode.None)
			{
				MultiLanguageData surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);
				LanguagesDropdownlist.Items.Clear();
				foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
				{
					ListItem defaultItem = new ListItem(GetPageLanguageCodes(language.LanguageDescription), language.LanguageCode);
					if (language.DefaultLanguage)
					{
						defaultItem.Value = "";
						defaultItem.Text += " " + GetPageLanguageCodes("LanguageDefaultText");
					}

					LanguagesDropdownlist.Items.Add(defaultItem);
				}

				LanguagesDropdownlist.Items.Insert(0, new ListItem(GetPageResource("AllLanguagesFilterOption"), "-1"));

				LanguagesDropdownlist.Visible = true;
				LanguageFilterLabel.Visible = true;
			}
			else
			{
				LanguagesDropdownlist.Visible = false;
				LanguageFilterLabel.Visible = false;
			}
		}

		private void LanguagesDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (LayoutDropDownList.SelectedValue == "2")
			{
				BuildHtmlResults(int.Parse(QuestionsDropDownList.SelectedValue));
			}
			else if (LayoutDropDownList.SelectedValue == "3")
			{
				//BuildPieChartResults(int.Parse(QuestionsDropDownList.SelectedValue));
                BuildChartColumnResults(int.Parse(QuestionsDropDownList.SelectedValue));
			}
			else
			{
				BuildChartColumnResults(int.Parse(QuestionsDropDownList.SelectedValue));
			}
		}

		private void ApplyRangeButton_Click(object sender, System.EventArgs e)
		{
			if (LayoutDropDownList.SelectedValue == "2")
			{
				BuildHtmlResults(int.Parse(QuestionsDropDownList.SelectedValue));
			}
			else if (LayoutDropDownList.SelectedValue == "3")
			{
				//BuildPieChartResults(int.Parse(QuestionsDropDownList.SelectedValue));
                BuildChartColumnResults(int.Parse(QuestionsDropDownList.SelectedValue));
			}
			else
			{
				BuildChartColumnResults(int.Parse(QuestionsDropDownList.SelectedValue));
			}		
		}

		protected string GetFileName()
		{
            //if (LayoutDropDownList.SelectedValue == "3")
            //{
            //    return "PieChartReport.aspx";
            //}
			return "ColumnChartReport.aspx";
		}

        protected string GetChartType()
        {
            if (LayoutDropDownList.SelectedValue == "4")
            {
                return "Bar";
            }
            return "Column";
        }


        private void RecreateControls(string ctrlPrefix)
        {
            var ctrls = Request.Form.ToString().Split('&');
            foreach (var ctrl in ctrls.Where(ctrl => ctrl.Contains(ctrlPrefix) && !ctrl.Contains("EVENTTARGET")))
            {
                var ctrlID = ctrl.Split('=')[0];
                int parentID;
                if (int.TryParse(ctrlID.Split('-').Last(), out parentID))
                {
                    CreateDropDownList(parentID);
                }
            }
        }

        protected void OnParentFilterChange(object sender, EventArgs e)
        {
            var ddl = sender as DropDownList;
            if (ddl == null) return;

            int filterId;
            filterId = int.TryParse(ddl.SelectedValue, out filterId) ? filterId : 0;
            RemoveDependentDropDownLists(ddl);
            CreateDropDownList(filterId);
        }

        private void RemoveDependentDropDownLists(ListControl ddl)
        {
            var values = ddl.Items.Cast<ListItem>().Select(i => i.Value).Where(value => value != "-1").ToList();

            foreach (var value in values)
            {
                var pnl = (PlaceHolder)pnlDropDownList.FindControl(PlhDynamicName + value);
                if (pnl != null)
                {
                    RemoveDependentDropDownLists(value);
                    pnlDropDownList.Controls.Remove(pnl);
                }
            }
        }

        private void RemoveDependentDropDownLists(string id)
        {
            var ddl = (DropDownList)pnlDropDownList.FindControl(DdlDynamicName + id);
            if (ddl != null)
            {
                RemoveDependentDropDownLists(ddl);
            }
        }

        private void CreateDropDownList(int filterId)
        {
            var id = DdlDynamicName + filterId.ToString(CultureInfo.InvariantCulture);
            var ddl = new DropDownList
            {
                ID = id,
                AutoPostBack = true,
                EnableViewState = true,
                //Width = 240,
                DataSource = new Filters().GetFiltersByParent(SurveyId, filterId),
                DataMember = "Filters",
                DataTextField = "Description",
                DataValueField = "FilterID",
            };

            ddl.DataBind();
            if (ddl.Items.Count < 1) return;
            ddl.Items.Insert(0, new ListItem(GetPageResource("SelectFilterMessage"), "-1"));
            ddl.TextChanged += OnParentFilterChange;
            ddl.SelectedIndexChanged += FilterDropDownList_SelectedIndexChanged;

            AddControlToPanel(ddl, filterId);
        }

        private void AddControlToPanel(Control control, int id)
        {
            var placeHolder1 = new PlaceHolder { ID = PlhDynamicName + id };

            //var lt = new Literal { Text = "<div>&nbsp;</div>" };
            //placeHolder1.Controls.Add(lt);

            placeHolder1.Controls.Add(control);

            //lt = new Literal { Text = "</div>" };
            //placeHolder1.Controls.Add(lt);

            pnlDropDownList.Controls.Add(placeHolder1);
        }

        public int GetFilterId()
        {
            var id = GetFilterId(FilterDropDownList);
            return id;
        }

        private int GetFilterId(ListControl ddl)
        {
            int id;
            id = int.TryParse(ddl.SelectedValue, out id) ? id : -1;

            var childDdl = (DropDownList)pnlDropDownList.FindControl(DdlDynamicName + id);
            if (childDdl == null) return id;
            return childDdl.SelectedValue == "-1" ? id : GetFilterId(childDdl);
        }
	}

}
