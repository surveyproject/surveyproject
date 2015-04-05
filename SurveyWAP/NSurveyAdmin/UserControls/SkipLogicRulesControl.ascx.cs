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
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Votations.NSurvey.Data;
	using Votations.NSurvey.DataAccess;
	using Votations.NSurvey.BusinessRules;


	/// <summary>
	/// Show the skip logic rules for a question.
	/// </summary>
    public partial class SkipLogigRulesControl : System.Web.UI.UserControl
	{
		public int QuestionId
		{
			get { return _questionId; }
			set { _questionId = value; }
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		public void BindData()
		{
			RulesRepeater.DataSource = new Questions().GetQuestionSkipLogicRules(QuestionId);
			RulesRepeater.DataMember = "SkipLogicRules";
			RulesRepeater.DataBind();
		}

		protected string FormatRule(object conditionalOperator, string questionText, string answerText, 
			string textFilter, string scoreText, string scoreMaxText, object expressionCondition)
		{
			ConditionalOperator condition = (ConditionalOperator)conditionalOperator;

			questionText = new Questions().ParseHTMLTagsFromQuestionText(questionText,40);

			if (condition == ConditionalOperator.ScoredLess)
			{
				return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleScoreLess"),
					scoreText,
					questionText);
			}
			else if (condition == ConditionalOperator.ScoreGreater)
			{
				return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleScoreGreater"),
					scoreText,
					questionText);
			}
			else if (condition == ConditionalOperator.ScoreRange)
			{
				return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleScoreRange"),
					scoreText,
					scoreMaxText,
					questionText);
			}
			else if (condition == ConditionalOperator.ScoreEquals)
			{
				return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleScoreEqual"),
					scoreText,
					questionText);
			}
			else
			{
				if (answerText.Length == 0)
				{
					if (condition == ConditionalOperator.Equal)
					{
						return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleNoAnswer"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")));
					}
					else
					{
						return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleNoAnswerNot"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")));
					}
				}
				else if (textFilter.Length == 0)
				{
					if (condition == ConditionalOperator.Equal)
					{
						return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleAnswer"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")));
					}
					else
					{
						return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleAnswerNot"),
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
						return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleAnswerWithText"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
							conditionText,
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(textFilter, "<[^>]*>", " ")));
					}
					else
					{
						return string.Format(((PageBase)Page).GetPageResource("SkipLogicRuleAnswerWithTextNot"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
							conditionText,
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(textFilter, "<[^>]*>", " ")));
					}
				}
			}
		}

		private void RulesRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			new Question().DeleteSkipLogicRuleById(int.Parse(e.CommandArgument.ToString()));
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
			this.RulesRepeater.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.RulesRepeater_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		int _questionId = -1;

		protected System.Web.UI.WebControls.Repeater RulesRepeater;
	}
}
