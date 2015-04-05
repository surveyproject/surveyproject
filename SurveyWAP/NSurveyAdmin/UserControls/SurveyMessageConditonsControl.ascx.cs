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

namespace Votations.NSurvey.WebAdmin.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Votations.NSurvey.Data;
	using Votations.NSurvey.DataAccess;
	using Votations.NSurvey.BusinessRules;


	/// <summary>
	///	Lists the message condition that are required to show 
	///	a specific "thank you" message
	/// </summary>
    public partial class SurveyMessageConditionsControl : System.Web.UI.UserControl
	{
		public int SurveyId
		{
			get { return _surveyId; }
			set { _surveyId = value; }
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		public void BindData()
		{
			RulesRepeater.DataSource = new Surveys().GetSurveyMessageConditions(SurveyId);
			RulesRepeater.DataMember = "MessageConditions";
			RulesRepeater.DataBind();
		}


		protected string FormatRule(object messageConditionalOperator, object conditionalOperator, 
			string questionText, string answerText, string textFilter, string thanksMessage, 
			string scoreText, string scoreMaxText, object expressionCondition)
		{
			MessageConditionalOperator messageCondition = (MessageConditionalOperator) int.Parse(messageConditionalOperator.ToString());
			thanksMessage = new Questions().ParseHTMLTagsFromQuestionText(thanksMessage,40);
			
			if (messageCondition == MessageConditionalOperator.QuestionAnswer)
			{
				ConditionalOperator condition = (ConditionalOperator)conditionalOperator;
				questionText = new Questions().ParseHTMLTagsFromQuestionText(questionText,40);

				if (condition == ConditionalOperator.ScoredLess)
				{
					return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionScoreLess"),
						thanksMessage,
						scoreText,
						questionText);
				}
				else if (condition == ConditionalOperator.ScoreGreater)
				{
					return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionScoreGreater"),
						thanksMessage,
						scoreText,
						questionText);
				}
				else if (condition == ConditionalOperator.ScoreRange)
				{
					return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionScoreRange"),
						thanksMessage,
						scoreText,
						scoreMaxText,
						questionText);
				}
				else if (condition == ConditionalOperator.ScoreEquals)
				{
					return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionScoreEqual"),
						thanksMessage,
						scoreText,
						questionText);
				}
				else
				{
					if (answerText.Length == 0)
					{
						if (condition == ConditionalOperator.Equal)
						{
							return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionNoAnswer"),
								thanksMessage,
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")));
						}
						else
						{
							return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionNoAnswerNot"),
								thanksMessage,
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")));
						}
					}
					else if (textFilter.Length == 0)
					{
						if (condition == ConditionalOperator.Equal)
						{
							return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionAnswer"),
								thanksMessage,
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")));
						}
						else
						{
							return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionAnswerNot"),
								thanksMessage,
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")));
						}
					}
					else 
					{
						string conditionText;
						ExpressionConditionalOperator conditionOperator = ExpressionConditionalOperator.Contains;
						if (expressionCondition.ToString().Length > 0)
						{
							conditionOperator = (ExpressionConditionalOperator) expressionCondition;
						}

						switch (conditionOperator)
						{
							case ExpressionConditionalOperator.Equals :
							{
								conditionText = (((PageBase)Page).GetPageResource("EqualsOperator")).ToLower();
								break;
							}
							case ExpressionConditionalOperator.GreaterThan :
							{
								conditionText = (((PageBase)Page).GetPageResource("GreaterThanOperator")).ToLower();
								break;
							}
							case ExpressionConditionalOperator.LessThan:
							{
								conditionText = (((PageBase)Page).GetPageResource("LessThanOperator")).ToLower();
								break;
							}
							default :
							{
								conditionText = (((PageBase)Page).GetPageResource("ContainsOperator")).ToLower();
								break;
							}
						}

						if (condition == ConditionalOperator.Equal)
						{
							return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionAnswerWithText"),
								thanksMessage,
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
								conditionText,
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(textFilter, "<[^>]*>", " ")));
						}
						else
						{
							return string.Format(((PageBase)Page).GetPageResource("MessageConditionQuestionAnswerWithTextNot"),
								thanksMessage,
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
								conditionText,
								Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(textFilter, "<[^>]*>", " ")));
						}
					}
				}
			}
			else if (messageCondition == MessageConditionalOperator.ScoredLess)
			{
				return string.Format(((PageBase)Page).GetPageResource("MessageConditionScoreLess"),
					thanksMessage,
					scoreText);
			}
			else if (messageCondition == MessageConditionalOperator.ScoreEquals)
			{
				return string.Format(((PageBase)Page).GetPageResource("MessageConditionScoreEqual"),
					thanksMessage,
					scoreText);
			}
			else if (messageCondition == MessageConditionalOperator.ScoreGreater)
			{
				return string.Format(((PageBase)Page).GetPageResource("MessageConditionScoreGreater"),
					thanksMessage,
					scoreText);
			}
			else if (messageCondition == MessageConditionalOperator.ScoreRange)
			{
				return string.Format(((PageBase)Page).GetPageResource("MessageConditionScoreRange"),
					thanksMessage,
					scoreText,
					scoreMaxText);
			}
			else
			{
				return null;
			}
		}

		private void RulesRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			new Survey().DeleteMessageConditionById(int.Parse(e.CommandArgument.ToString()));
			BindData();
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.RulesRepeater.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.RulesRepeater_ItemDataBound);
			this.RulesRepeater.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.RulesRepeater_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void RulesRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				HyperLink editHyperLink = (HyperLink)e.Item.FindControl("EditHyperLink");
				if (editHyperLink!= null)
				{
					editHyperLink.NavigateUrl = String.Format("{0}?surveyid={1}&messageconditionid={2}&MenuIndex={3}",
						UINavigator.MessageConditionEditorHyperLink,
						SurveyId,
						DataBinder.Eval(e.Item.DataItem,"MessageConditionID"),
						((PageBase)Page).MenuIndex);
					editHyperLink.Text = ((PageBase)Page).GetPageResource("EditConditionInfo");
				}
			}
		}

		int _surveyId = -1;
		protected System.Web.UI.WebControls.Repeater RulesRepeater;

	}
}
