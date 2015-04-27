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
using Microsoft.VisualBasic;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Skip logic edition page
	/// </summary>
    public partial class EditSkipLogicRules : PageBase
	{
		protected System.Web.UI.WebControls.DropDownList QuestionFilterDropdownlist;
		protected System.Web.UI.WebControls.Label AnswerLabel;
		protected System.Web.UI.WebControls.DropDownList AnswerFilterDropdownlist;
		protected System.Web.UI.WebControls.Label FilterTextLabel;
		protected System.Web.UI.WebControls.TextBox TextFilterTextbox;
		protected System.Web.UI.WebControls.Button AddRuleButton;
		protected System.Web.UI.WebControls.DropDownList LogicDropDownList;
		protected System.Web.UI.WebControls.Label ConditionalLabel;
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected SkipLogigRulesControl SkipLogicRules;
		protected System.Web.UI.WebControls.Label QuestionLabel;
		protected System.Web.UI.WebControls.Label ScoreLabel;
		protected System.Web.UI.WebControls.TextBox ScoreTextbox;
		protected System.Web.UI.WebControls.Label ScoreRangeLabel;
		protected System.Web.UI.WebControls.TextBox ScoreMaxTextbox;
		protected System.Web.UI.WebControls.Literal AddNewSkipLogicTitle;
		protected System.Web.UI.WebControls.Literal SkipLogicRulesTitle;
		protected System.Web.UI.WebControls.Literal SkipLogicEvaluationConditionInfo;
		protected System.Web.UI.WebControls.Label TextEvaluationConditionLabel;
		protected System.Web.UI.WebControls.DropDownList ExpressionLogicDropdownlist;
		
		new protected HeaderControl Header;

		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.FormBuilder);
            SetupSecurity();
			LocalizePage();

			MessageLabel.Visible = false;
		
			_pageNumber = 
				Information.IsNumeric(Request["Page"]) ? int.Parse(Request["Page"]) : -1;
			if (_pageNumber == -1)
			{
				//throw new FormatException("Invalid page number!");
                throw new FormatException(GetPageResource("InvalidPageNumber"));
			}	
			
			ValidateRequestQuestionId();

			SkipLogicRules.QuestionId = _questionId;

			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				BindFields();
			}

		}

        protected void OnBackButton(object sender, System.EventArgs e)
        {
            Response.Redirect(UINavigator.SurveyContentBuilderLink);
        }

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessFormBuilder, true);
		}

		private void LocalizePage()
		{
			AddNewSkipLogicTitle.Text = string.Format(GetPageResource("AddNewSkipLogicTitle"), Request["Page"]);
			QuestionLabel.Text = GetPageResource("QuestionLabel");
			ConditionalLabel.Text = GetPageResource("ConditionalLabel");
			AnswerLabel.Text = GetPageResource("AnswerLabel");
			FilterTextLabel.Text = GetPageResource("FilterTextLabel");
			SkipLogicEvaluationConditionInfo.Text = GetPageResource("SkipLogicEvaluationConditionInfo");
			AddRuleButton.Text = GetPageResource("AddRuleButton");
			ScoreRangeLabel.Text = GetPageResource("ScoreRangeLabel");
			TextEvaluationConditionLabel.Text = GetPageResource("TextEvaluationConditionLabel");
			SkipLogicRulesTitle.Text = GetPageResource("SkipLogicRulesTitle");	
		}

		private void ValidateRequestQuestionId()
		{
			_questionId = 
				Information.IsNumeric(Request["QuestionId"]) ? int.Parse(Request["QuestionId"]) : -1;
			
			if (_questionId != -1 && !NSurveyUser.Identity.IsAdmin &&
				!NSurveyUser.Identity.HasAllSurveyAccess)
			{
				if (!new Question().CheckQuestionUser(_questionId, NSurveyUser.Identity.UserId))
				{
					_questionId = -1;
				}
			}

			if (_questionId == -1)
			{
				//throw new FormatException("Invalid question id!");
                throw new FormatException(GetPageResource("InvalidQuestionID"));
			}		
		}

		private void BindFields()
		{
		
			SkipLogicRules.BindData();
			QuestionFilterDropdownlist.DataSource = new Questions().GetAnswerableQuestionListInPageRange(SurveyId, 1, _pageNumber-1);
			QuestionFilterDropdownlist.DataTextField = "QuestionText";
			QuestionFilterDropdownlist.DataValueField = "QuestionID";
			QuestionFilterDropdownlist.DataBind();
			QuestionFilterDropdownlist.Items.Insert(0, new ListItem(GetPageResource("SelectQuestionMessage"), "-1"));

			ExpressionLogicDropdownlist.Items.Clear();
			ExpressionLogicDropdownlist.Items.Add(new ListItem(GetPageResource("EqualsOperator"), ((int)ExpressionConditionalOperator.Equals).ToString()));
			ExpressionLogicDropdownlist.Items.Add(new ListItem(GetPageResource("ContainsOperator"), ((int)ExpressionConditionalOperator.Contains).ToString()));
			ExpressionLogicDropdownlist.Items.Add(new ListItem(GetPageResource("GreaterThanOperator"), ((int)ExpressionConditionalOperator.GreaterThan).ToString()));
			ExpressionLogicDropdownlist.Items.Add(new ListItem(GetPageResource("LessThanOperator"), ((int)ExpressionConditionalOperator.LessThan).ToString()));

			HideFields(true, true, true, true, true, true);
		}

		protected void HideFields(bool hideConditions, bool hideAnswers, bool hidePage, 
			bool hideScore, bool hideScoreRange, bool rebindLogic)
		{
			ScoreLabel.Visible = !hideScore;
			ScoreTextbox.Visible  = !hideScore;
			ScoreRangeLabel.Visible = !hideScoreRange;
			ScoreMaxTextbox.Visible = !hideScoreRange;
			ConditionalLabel.Visible = !hideConditions;
			LogicDropDownList.Visible = !hideConditions;
			AnswerLabel.Visible = !hideAnswers;
			AnswerFilterDropdownlist.Visible = !hideAnswers;
			TextFilterTextbox.Visible = false;
			FilterTextLabel.Visible = false;
			TextFilterTextbox.Text = string.Empty;
			ExpressionLogicDropdownlist.Visible = false;
			TextEvaluationConditionLabel.Visible = false;

			AnswerFilterDropdownlist.Items.Clear();

			if (!Page.IsPostBack || rebindLogic)
			{
				LogicDropDownList.Items.Clear();
				LogicDropDownList.Items.Add(new ListItem(GetPageResource("AnsweredConditionOption"), "1"));
				LogicDropDownList.Items.Add(new ListItem(GetPageResource("NotAnsweredConditionOption"), "2"));
				if (new Surveys().IsSurveyScored(SurveyId))
				{
					bool showScoreOptions = true;
					if (QuestionFilterDropdownlist.SelectedValue != "-1")
					{
						// Check if the question is a matrix type
						// currently matrix type interface doesnt support
						// setting scores
						QuestionData question = new Questions().GetQuestionById(int.Parse(QuestionFilterDropdownlist.SelectedValue), null);
						if (question.Questions.Rows.Count > 0 && 
							((QuestionTypeMode)question.Questions[0].TypeMode & QuestionTypeMode.ChildQuestion)>0)
							showScoreOptions = false;
					}

					if (showScoreOptions)
					{
						LogicDropDownList.Items.Add(new ListItem(GetPageResource("ScoredEqualConditionOption"), "3"));
						LogicDropDownList.Items.Add(new ListItem(GetPageResource("ScoredLessConditionOption"), "4"));
						LogicDropDownList.Items.Add(new ListItem(GetPageResource("ScoredGreaterConditionOption"), "5"));
						LogicDropDownList.Items.Add(new ListItem(GetPageResource("ScoredRangeConditionOperator"), "6"));
					}
				}
			}

		}

		private void QuestionFilterDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (QuestionFilterDropdownlist.SelectedValue != "-1")
			{
				AddRuleButton.Enabled = true;
				HideFields(false, false, false, true, true, true);
				BindAnswerDropDownList();
			}
			else
			{
				AddRuleButton.Enabled = false;
				HideFields(true, true, true, true, true, true);
			}
		}

		/// <summary>
		/// Bind the list and mark field answer 
		/// with a negative answer id value
		/// </summary>
		private void BindAnswerDropDownList()
		{
			AnswerData answers = new Answers().GetAnswersList(int.Parse(QuestionFilterDropdownlist.SelectedValue));
			AnswerFilterDropdownlist.Items.Clear();
			AnswerFilterDropdownlist.Items.Add(new ListItem(GetPageResource("AnyAnswerMessage"), "0"));

			foreach (AnswerData.AnswersRow answerRow in answers.Answers)
			{
				if ((((AnswerTypeMode)answerRow.TypeMode & AnswerTypeMode.Field) > 0) ||
					(((AnswerTypeMode)answerRow.TypeMode & AnswerTypeMode.Custom) > 0) ||
					(((AnswerTypeMode)answerRow.TypeMode & AnswerTypeMode.DataSource) > 0))
				{
					// Mark field answer with a negative answerid
					AnswerFilterDropdownlist.Items.Add(new ListItem(answerRow.AnswerText +" "+ GetPageResource("TextEntryInfo"), (-answerRow.AnswerId).ToString()));
				}
				else
				{
					AnswerFilterDropdownlist.Items.Add(new ListItem(answerRow.AnswerText +" "+ GetPageResource("SelectionInfo"), answerRow.AnswerId.ToString()));
				}
			}
		}

		private void AnswerFilterDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (int.Parse(AnswerFilterDropdownlist.SelectedValue) < 0)
			{
				TextFilterTextbox.Visible = true;
				FilterTextLabel.Visible = true;
				ExpressionLogicDropdownlist.Visible = true;
				TextEvaluationConditionLabel.Visible = true;
			}
			else
			{
				TextFilterTextbox.Text = string.Empty;
				TextFilterTextbox.Visible = false;
				FilterTextLabel.Visible = false;
				ExpressionLogicDropdownlist.Visible = false;
				TextEvaluationConditionLabel.Visible = false;
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
			this.QuestionFilterDropdownlist.SelectedIndexChanged += new System.EventHandler(this.QuestionFilterDropdownlist_SelectedIndexChanged);
			this.LogicDropDownList.SelectedIndexChanged += new System.EventHandler(this.LogicDropDownList_SelectedIndexChanged);
			this.AnswerFilterDropdownlist.SelectedIndexChanged += new System.EventHandler(this.AnswerFilterDropdownlist_SelectedIndexChanged);
			this.AddRuleButton.Click += new System.EventHandler(this.AddRuleButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void AddRuleButton_Click(object sender, System.EventArgs e)
		{
			SkipLogicRuleData skipLogicRuleData = new SkipLogicRuleData();
			SkipLogicRuleData.SkipLogicRulesRow skipLogicRule = skipLogicRuleData.SkipLogicRules.NewSkipLogicRulesRow();

			if ((LogicDropDownList.SelectedValue == "3" ||
				LogicDropDownList.SelectedValue == "4" ||
				LogicDropDownList.SelectedValue == "5") &&
				Information.IsNumeric(ScoreTextbox.Text))
			{
				skipLogicRule.Score = int.Parse(ScoreTextbox.Text);
			}
			else if (LogicDropDownList.SelectedValue == "6" &&
				Information.IsNumeric(ScoreTextbox.Text) &&
				Information.IsNumeric(ScoreMaxTextbox.Text))
			{
				skipLogicRule.Score = int.Parse(ScoreTextbox.Text);
				skipLogicRule.ScoreMax = int.Parse(ScoreMaxTextbox.Text);
			}
			else if (LogicDropDownList.SelectedValue == "3" ||
				LogicDropDownList.SelectedValue == "4" ||
				LogicDropDownList.SelectedValue == "5" ||
				LogicDropDownList.SelectedValue == "6")
			{
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("ScoreNotNumericMessage"));
				MessageLabel.Visible = true;
				return;
			}
			else
			{

			
				int answerId = int.Parse(AnswerFilterDropdownlist.SelectedValue);
				if (answerId == 0)
				{
					skipLogicRule.SetAnswerIdNull();
				}
				else if ( answerId < 0)
				{
					skipLogicRule.AnswerId  = -answerId;
				}
				else
				{
					skipLogicRule.AnswerId = answerId;
				}

				if (TextFilterTextbox.Visible)
				{
					skipLogicRule.ExpressionOperator = 
						int.Parse(ExpressionLogicDropdownlist.SelectedValue);
					skipLogicRule.TextFilter = TextFilterTextbox.Text;
				}
			}

			skipLogicRule.SkipQuestionId = _questionId;
			skipLogicRule.QuestionId = int.Parse(QuestionFilterDropdownlist.SelectedValue);
			skipLogicRule.ConditionalOperator = int.Parse(LogicDropDownList.SelectedValue);
			skipLogicRuleData.SkipLogicRules.AddSkipLogicRulesRow(skipLogicRule);
			new Question().AddSkipLogicRule(skipLogicRuleData);
			MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("SkipLogicRuleAddedMessage"));
			BindFields();
			ScoreTextbox.Text = string.Empty;
			ScoreMaxTextbox.Text = string.Empty;
		}

		int _pageNumber = 1,
			_questionId = -1;

		private void LogicDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (LogicDropDownList.SelectedValue == "3" ||
				LogicDropDownList.SelectedValue == "4" ||
				LogicDropDownList.SelectedValue == "5")
			{
				HideFields(false,true, false, false, true, false);
			}
			else if (LogicDropDownList.SelectedValue == "6")
			{
				HideFields(false,true, false, false, false, false);
				BindAnswerDropDownList();
			}
			else
			{
				HideFields(false,false, false, true, true, false);
				BindAnswerDropDownList();
			}
		}
	}

}
