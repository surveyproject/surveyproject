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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;

using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Edit the conditions that will show a given "Thanks" message
	/// </summary>
    public partial class ConditionalEndMessage : PageBase
	{
		protected System.Web.UI.WebControls.DropDownList QuestionFilterDropdownlist;
		protected System.Web.UI.WebControls.Label AnswerLabel;
		protected System.Web.UI.WebControls.DropDownList AnswerFilterDropdownlist;
		protected System.Web.UI.WebControls.Label FilterTextLabel;
		protected System.Web.UI.WebControls.TextBox TextFilterTextbox;
		protected System.Web.UI.WebControls.DropDownList LogicDropDownList;
		protected System.Web.UI.WebControls.Label ConditionalLabel;
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label QuestionLabel;
		protected System.Web.UI.WebControls.Label ScoreLabel;
		protected System.Web.UI.WebControls.TextBox ScoreTextbox;
		protected System.Web.UI.WebControls.Label ScoreRangeLabel;
		protected System.Web.UI.WebControls.TextBox ScoreMaxTextbox;
		protected System.Web.UI.WebControls.Label ConditionalMessageLabel;
//		protected FreeTextBoxControls.FreeTextBox ThankYouFreeTextBox;
		protected System.Web.UI.WebControls.Button AddMessageConditionButton;
		protected System.Web.UI.WebControls.Label MessageConditionLabel;
		protected System.Web.UI.WebControls.DropDownList MessageConditionDropdownlist;
		protected System.Web.UI.WebControls.Literal EvaluationMessageConditionInfo;
		protected System.Web.UI.WebControls.Button UpdateMessageConditionButton;
		protected System.Web.UI.WebControls.Literal AddNewConditionTitle;
		protected System.Web.UI.WebControls.Button AddShowCondition;
		protected System.Web.UI.WebControls.Button ConditionCancelButton;
		protected System.Web.UI.WebControls.Label ScoreTagLabel;
		protected System.Web.UI.WebControls.Label TextEvaluationConditionLabel;
		protected System.Web.UI.WebControls.DropDownList ExpressionLogicDropdownlist;
		
		new protected HeaderControl Header;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetupSecurity();
			MessageLabel.Visible = false;
			_messageConditionId = 
				Information.IsNumeric(Request["messageconditionid"]) ? int.Parse(Request["messageconditionid"]) : -1;
			_isScored = new Surveys().IsSurveyScored(SurveyId);

			LocalizePage();
            UITabList.SetConditionalEndMessageTabs((MsterPageTabs)Page.Master, UITabList.ConditionalEndMessageTabs.EndMessages);

			if (_messageConditionId == -1)
			{
				SwitchToCreationMode();
			}
			else
			{
				SwitchToEditionMode();
			}

            //CKEditor settings:
            //ThankYouCKEditor.config.toolbar = "Simple";
            //ThankYouCKEditor.config.uiColor = "#DDDDDD";
            ConditionCKeditor.config.enterMode = CKEditor.NET.EnterMode.BR;
            ConditionCKeditor.config.skin = "moonocolor";

            ConditionCKeditor.config.toolbar = new object[]
			{
				new object[] { "Source", "-", "NewPage", "Preview", "-", "Templates" },
				new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt" },
				new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
				new object[] { "Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField" },
				"/",
				new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
				new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
				new object[] { "BidiLtr", "BidiRtl" },
				new object[] { "Link", "Unlink", "Anchor" },
				new object[] { "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe" },
				"/",
				new object[] { "Styles", "Format", "Font", "FontSize" },
				new object[] { "TextColor", "BGColor" },
				new object[] { "Maximize", "ShowBlocks", "-", "About" }
			};


		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessPrivacySettings, true);
		}

		/// <summary>
		/// Setup the creation mode
		/// </summary>
		private void SwitchToCreationMode()
		{
			// Creation mode
			AddMessageConditionButton.Visible = true;
			UpdateMessageConditionButton.Visible = false;
			AddNewConditionTitle.Text = string.Format(GetPageResource("AddNewConditionTitle"));

			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				BindFields();
			}
		}

		/// <summary>
		/// Setup the edition mode
		/// </summary>
		private void SwitchToEditionMode()
		{
			AddMessageConditionButton.Visible = false;
			UpdateMessageConditionButton.Visible = true;
			AddNewConditionTitle.Text = string.Format(GetPageResource("UpdateConditionTitle"));

			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				BindFields();
				BindUpdate();
			}
		}

		/// <summary>
		///  Get the existing message condition
		///  and restore the UI
		/// </summary>
		private void BindUpdate()
		{
			MessageConditionData messageConditionData = new Surveys().GetSurveyMessageCondition(_messageConditionId);
			if (messageConditionData.MessageConditions.Rows.Count > 0)
			{
				MessageConditionData.MessageConditionsRow messageCondition = 
					(MessageConditionData.MessageConditionsRow)messageConditionData.MessageConditions[0];
			
				if (MessageConditionDropdownlist.Items.FindByValue(messageCondition.MessageConditionalOperator.ToString()) != null)
				{
					MessageConditionDropdownlist.SelectedValue = messageCondition.MessageConditionalOperator.ToString();
				}

                ConditionCKeditor.Text = messageCondition.ThankYouMessage;

				// Check if condition is based on the survey's score or on its questions answers
				if (_isScored && (messageCondition.MessageConditionalOperator == (int)MessageConditionalOperator.ScoredLess ||
					messageCondition.MessageConditionalOperator == (int)MessageConditionalOperator.ScoreEquals ||
					messageCondition.MessageConditionalOperator == (int)MessageConditionalOperator.ScoreGreater))
				{
					ScoreTextbox.Text = messageCondition.Score.ToString();
					HideFields(true, true, false, false, true, false, true);
				}
				else if (_isScored && messageCondition.MessageConditionalOperator == (int)MessageConditionalOperator.ScoreRange)
				{
					ScoreTextbox.Text = messageCondition.Score.ToString();
					ScoreMaxTextbox.Text = messageCondition.ScoreMax.ToString();
					HideFields(true, true, false, false, false, false, true);
				}
				// User selected a condition from a question's answer
				else if (messageCondition.MessageConditionalOperator == (int)MessageConditionalOperator.QuestionAnswer)
				{
					QuestionFilterDropdownlist.SelectedValue = messageCondition.QuestionId.ToString();

					if (_isScored && (messageCondition.ConditionalOperator == (int)ConditionalOperator.ScoredLess ||
						messageCondition.ConditionalOperator == (int)ConditionalOperator.ScoreEquals ||
						messageCondition.ConditionalOperator == (int)ConditionalOperator.ScoreGreater))
					{
						ScoreTextbox.Text = messageCondition.Score.ToString();
						HideFields(false, true, false, false, true, false, false);
					}
					else if (_isScored && messageCondition.ConditionalOperator == (int)ConditionalOperator.ScoreRange)
					{
						ScoreTextbox.Text = messageCondition.Score.ToString();
						ScoreMaxTextbox.Text = messageCondition.ScoreMax.ToString();
						HideFields(false, true, false, false, false,false, false);
					}
					else
					{
						BindAnswerDropDownList();

						// Is the question's answer a field type
						if (!messageCondition.IsAnswerIdNull() && 
							AnswerFilterDropdownlist.Items.FindByValue((-messageCondition.AnswerId).ToString()) != null)
						{
							AnswerFilterDropdownlist.SelectedValue = (-messageCondition.AnswerId).ToString();
							HideFields(false,false, false, true, true, false, false);
							
							// Set text filter and show it
							TextFilterTextbox.Text = messageCondition.TextFilter;
							TextFilterTextbox.Visible = true;
							FilterTextLabel.Visible = true;
							ExpressionLogicDropdownlist.SelectedValue = messageCondition.ExpressionOperator.ToString();
							ExpressionLogicDropdownlist.Visible = true;
							TextEvaluationConditionLabel.Visible = true;

						}
						else
						{
							AnswerFilterDropdownlist.SelectedValue = messageCondition.IsAnswerIdNull() ?
								"0" : messageCondition.AnswerId.ToString();
							HideFields(false,false, false, true, true, false, false);
						}
					}

					LogicDropDownList.SelectedValue = messageCondition.ConditionalOperator.ToString();
				}
			}

		}

		private void LocalizePage()
		{
			QuestionLabel.Text = GetPageResource("QuestionLabel");
			ConditionalLabel.Text = GetPageResource("ConditionalLabel");
			AnswerLabel.Text = GetPageResource("AnswerLabel");
			FilterTextLabel.Text = GetPageResource("FilterTextLabel");
			ScoreRangeLabel.Text = GetPageResource("ScoreRangeLabel");
			ScoreTagLabel.Text = GetPageResource("ScoreTagLabel");
			TextEvaluationConditionLabel.Text = GetPageResource("TextEvaluationConditionLabel");
			MessageConditionLabel.Text = GetPageResource("MessageConditionLabel");
			AddMessageConditionButton.Text = GetPageResource("AddMessageConditionButton");
			UpdateMessageConditionButton.Text = GetPageResource("UpdateMessageConditionButton");
			ConditionCancelButton .Text = GetPageResource("ConditionCancelButton");
            ConditionalMessageLabel.Text = GetPageResource("ConditionalMessageLabel");
		}

		private void BindFields()
		{
			MessageConditionDropdownlist.Items.Clear();
			MessageConditionDropdownlist.Items.Add(new ListItem(GetPageResource("SelectQuestionConditionMessage"), "-1"));
			MessageConditionDropdownlist.Items.Add(new ListItem(GetPageResource("SurveyQuestionConditionOption"), "1"));
			if (_isScored)
			{
				MessageConditionDropdownlist.Items.Add(new ListItem(GetPageResource("SurveyScoredEqualConditionOption"), "2"));
				MessageConditionDropdownlist.Items.Add(new ListItem(GetPageResource("SurveyScoredLessConditionOption"), "3"));
				MessageConditionDropdownlist.Items.Add(new ListItem(GetPageResource("SurveyScoredGreaterConditionOption"), "4"));
				MessageConditionDropdownlist.Items.Add(new ListItem(GetPageResource("SurveyScoredRangeConditionOperator"), "5"));
			}
		
			BindQuestions();

			ExpressionLogicDropdownlist.Items.Clear();
			ExpressionLogicDropdownlist.Items.Add(new ListItem(GetPageResource("EqualsOperator"), ((int)ExpressionConditionalOperator.Equals).ToString()));
			ExpressionLogicDropdownlist.Items.Add(new ListItem(GetPageResource("ContainsOperator"), ((int)ExpressionConditionalOperator.Contains).ToString()));
			ExpressionLogicDropdownlist.Items.Add(new ListItem(GetPageResource("GreaterThanOperator"), ((int)ExpressionConditionalOperator.GreaterThan).ToString()));
			ExpressionLogicDropdownlist.Items.Add(new ListItem(GetPageResource("LessThanOperator"), ((int)ExpressionConditionalOperator.LessThan).ToString()));

			HideFields(true, true, true, true, true, false, true);
		}

		private void BindQuestions()
		{
			QuestionFilterDropdownlist.DataSource = new Questions().GetAnswerableQuestionList(SurveyId);
			QuestionFilterDropdownlist.DataTextField = "QuestionText";
			QuestionFilterDropdownlist.DataValueField = "QuestionID";
			QuestionFilterDropdownlist.DataBind();
		}

		protected void HideFields(bool hideConditions, bool hideAnswers, bool hideTextBox, 
			bool hideScore, bool hideScoreRange, bool rebindLogic, bool hideQuestion)
		{
			QuestionFilterDropdownlist.Visible = !hideQuestion;
			QuestionLabel.Visible = !hideQuestion;
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
            ConditionCKeditor.Visible = !hideTextBox;
			ScoreTagLabel.Visible = _isScored && !hideTextBox;
			ExpressionLogicDropdownlist.Visible = false;
			TextEvaluationConditionLabel.Visible = false;

			ConditionalMessageLabel.Visible = !hideTextBox;

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

					if (showScoreOptions && _isScored)
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
			HideFields(false, false, false, true, true, true, false);
			BindAnswerDropDownList();
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
			this.MessageConditionDropdownlist.SelectedIndexChanged += new System.EventHandler(this.MessageConditionDropdownlist_SelectedIndexChanged);
			this.QuestionFilterDropdownlist.SelectedIndexChanged += new System.EventHandler(this.QuestionFilterDropdownlist_SelectedIndexChanged);
			this.LogicDropDownList.SelectedIndexChanged += new System.EventHandler(this.LogicDropDownList_SelectedIndexChanged);
			this.AnswerFilterDropdownlist.SelectedIndexChanged += new System.EventHandler(this.AnswerFilterDropdownlist_SelectedIndexChanged);
			this.AddMessageConditionButton.Click += new System.EventHandler(this.AddRuleButton_Click);
			this.UpdateMessageConditionButton.Click += new System.EventHandler(this.UpdateMessageConditionButton_Click);
			this.ConditionCancelButton.Click += new System.EventHandler(this.ConditionCancelButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void AddRuleButton_Click(object sender, System.EventArgs e)
		{
			AddUpdateMessageCondition(false);
			UINavigator.NavigateToSurveyPrivacyOptions(SurveyId, MenuIndex);
		}

		private void AddUpdateMessageCondition(bool updateMode)
		{
			MessageConditionData messageConditionData = new MessageConditionData();
			MessageConditionData .MessageConditionsRow messageCondition = messageConditionData .MessageConditions.NewMessageConditionsRow();

			if ((MessageConditionDropdownlist.SelectedValue == "2" ||
				MessageConditionDropdownlist.SelectedValue == "3" ||
				MessageConditionDropdownlist.SelectedValue == "4") &&
				Information.IsNumeric(ScoreTextbox.Text))
			{
				messageCondition.Score = int.Parse(ScoreTextbox.Text);

			}
			else if (MessageConditionDropdownlist.SelectedValue == "5" &&
				Information.IsNumeric(ScoreTextbox.Text) &&
				Information.IsNumeric(ScoreMaxTextbox.Text))
			{
				messageCondition.Score = int.Parse(ScoreTextbox.Text);
				messageCondition.ScoreMax = int.Parse(ScoreMaxTextbox.Text);
			}
			else if (MessageConditionDropdownlist.SelectedValue == "2" ||
				MessageConditionDropdownlist.SelectedValue == "3" ||
				MessageConditionDropdownlist.SelectedValue == "4" ||
				MessageConditionDropdownlist.SelectedValue == "5")
			
			{
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("ScoreNotNumericMessage"));
				MessageLabel.Visible = true;
				return;
			}
			// Condition created from a selected question
			else if (MessageConditionDropdownlist.SelectedValue == "1")
			{
				if ((LogicDropDownList.SelectedValue == "3" ||
					LogicDropDownList.SelectedValue == "4" ||
					LogicDropDownList.SelectedValue == "5") &&
					Information.IsNumeric(ScoreTextbox.Text))
				{
					messageCondition.Score = int.Parse(ScoreTextbox.Text);
				}
				else if (LogicDropDownList.SelectedValue == "6" &&
					Information.IsNumeric(ScoreTextbox.Text) &&
					Information.IsNumeric(ScoreMaxTextbox.Text))
				{
					messageCondition.Score = int.Parse(ScoreTextbox.Text);
					messageCondition.ScoreMax = int.Parse(ScoreMaxTextbox.Text);
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
						messageCondition.SetAnswerIdNull();
					}
					else if ( answerId < 0)
					{
						messageCondition.AnswerId  = -answerId;
					}
					else
					{
						messageCondition.AnswerId = answerId;
					}

					if (TextFilterTextbox.Visible)
					{
						messageCondition.ExpressionOperator = 
							int.Parse(ExpressionLogicDropdownlist.SelectedValue);
						messageCondition.TextFilter = TextFilterTextbox.Text;
					}

				}

				messageCondition.QuestionId = int.Parse(QuestionFilterDropdownlist.SelectedValue);
				messageCondition.ConditionalOperator = int.Parse(LogicDropDownList.SelectedValue);
			}

            messageCondition.ThankYouMessage = ConditionCKeditor.Text.Length > 3900 ?
                ConditionCKeditor.Text.Substring(0, 3900) : ConditionCKeditor.Text;
			messageCondition.MessageConditionalOperator = int.Parse(MessageConditionDropdownlist.SelectedValue);
			messageConditionData.MessageConditions.AddMessageConditionsRow(messageCondition);
			
			if (updateMode)
			{
				messageCondition.MessageConditionId = _messageConditionId;
				new Survey().UpdateMessageCondition(messageConditionData);
			}
			else
			{
				messageCondition.SurveyId = SurveyId;
				new Survey().AddMessageCondition(messageConditionData);
			}
		}
		

		private void LogicDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (LogicDropDownList.SelectedValue == "3" ||
				LogicDropDownList.SelectedValue == "4" ||
				LogicDropDownList.SelectedValue == "5")
			{
				HideFields(false,true, false, false, true, false, false);
			}
			else if (LogicDropDownList.SelectedValue == "6")
			{
				HideFields(false,true, false, false, false, false, false);
				BindAnswerDropDownList();
			}
			else
			{
				HideFields(false,false, false, true, true, false, false);
				BindAnswerDropDownList();
			}
		}

		private void MessageConditionDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (new Questions().GetAnswerableQuestionList(SurveyId).Questions.Count == 0) return;

			if (MessageConditionDropdownlist.SelectedValue == "2" ||
				MessageConditionDropdownlist.SelectedValue == "3" ||
				MessageConditionDropdownlist.SelectedValue == "4")
			{
				AddMessageConditionButton.Enabled = true;
				HideFields(true, true, false, false, true, true, true);
			}
			else if (MessageConditionDropdownlist.SelectedValue == "5")
			{
				AddMessageConditionButton.Enabled = true;
				HideFields(true, true, false, false, false, true, true);
				BindAnswerDropDownList();
			}
			else if (MessageConditionDropdownlist.SelectedValue == "1")
			{
				AddMessageConditionButton.Enabled = true;
				HideFields(false,false, false, true, true, true, false);
				QuestionFilterDropdownlist.ClearSelection();
				BindAnswerDropDownList();
			}
			else
			{
				AddMessageConditionButton.Enabled = false;
				HideFields(true, true, true, true, true, true, true);
			}
		
		}

		private void UpdateMessageConditionButton_Click(object sender, System.EventArgs e)
		{
			AddUpdateMessageCondition(true);
			MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("MessageConditionAddedMessage"));
		}

		private void ConditionCancelButton_Click(object sender, System.EventArgs e)
		{
			UINavigator.NavigateToSurveyPrivacyOptions(SurveyId, MenuIndex);
		}

		int _messageConditionId = -1;
		bool _isScored = false;

	}

}
