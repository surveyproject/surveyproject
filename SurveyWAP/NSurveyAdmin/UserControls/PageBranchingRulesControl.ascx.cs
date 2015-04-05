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
	///		Summary description for PageBranchingRules.
	/// </summary>
    public partial class PageBranchingRulesControl : System.Web.UI.UserControl
	{
		public int SurveyId
		{
			get { return _surveyId; }
			set { _surveyId = value; }
		}

		public int PageNumber
		{
			get { return _pageNumber; }
			set { _pageNumber = value; }
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
		}

		public void BindData()
		{
			RulesRepeater.DataSource = new Surveys().GetSurveyPageBranchingRulesDetails(SurveyId, PageNumber);
			RulesRepeater.DataMember = "BranchingRules";
			RulesRepeater.DataBind();
		}

		protected string ShowFilterTextRule(object filterText)
		{
			if (filterText.ToString().Length > 0)
			{
				return string.Format("with an entry text value \"<b>{0}</b>\"", filterText.ToString());
			}

			return string.Empty;
		}

		protected string ShowAnswerText(object filterText)
		{
			if (filterText.ToString().Length > 0)
			{
				return string.Format("with \"<b>{0}</b>\" answer", filterText.ToString());
			}

			return string.Empty;
		}

		protected string ShowCondition(object conditionalOperator)
		{
			ConditionalOperator condition = (ConditionalOperator)conditionalOperator;

			if (condition == ConditionalOperator.Equal)
			{
				return "who answered";
			}
			else
			{
				return "who didn't answer";
			}
		}

		protected string FormatRule(object conditionalOperator, string questionText, string answerText, 
			string textFilter, string pageNumber, string scoreText, string scoreMaxText, object expressionCondition)
		{
			ConditionalOperator condition = (ConditionalOperator)conditionalOperator;

			questionText = new Questions().ParseHTMLTagsFromQuestionText(questionText,40);

			if (pageNumber == "-1")
			{
                pageNumber = ((PageBase)Page).GetPageResource("BranchingRuleEndSurvey");
			}
			else
			{
				pageNumber = string.Format(((PageBase)Page).GetPageResource("BranchingRulePage"), pageNumber);
			}
			
			if (condition == ConditionalOperator.ScoredLess)
			{
				return string.Format(((PageBase)Page).GetPageResource("BranchingRuleScoreLess"),
					scoreText,
					questionText,
					pageNumber);
			}
			else if (condition == ConditionalOperator.ScoreGreater)
			{
				return string.Format(((PageBase)Page).GetPageResource("BranchingRuleScoreGreater"),
					scoreText,
					questionText,
					pageNumber);
			}
			else if (condition == ConditionalOperator.ScoreRange)
			{
				return string.Format(((PageBase)Page).GetPageResource("BranchingRuleScoreRange"),
					scoreText,
					scoreMaxText,
					questionText,
					pageNumber);
			}
			else if (condition == ConditionalOperator.ScoreEquals)
			{
				return string.Format(((PageBase)Page).GetPageResource("BranchingRuleScoreEqual"),
					scoreText,
					questionText,
					pageNumber);
			}
			else
			{
				if (answerText.Length == 0)
				{
					if (condition == ConditionalOperator.Equal)
					{
						return string.Format(((PageBase)Page).GetPageResource("BranchingRuleNoAnswer"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							pageNumber);
					}
					else
					{
						return string.Format(((PageBase)Page).GetPageResource("BranchingRuleNoAnswerNot"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							pageNumber);
					}
				}
				else if (textFilter.Length == 0)
				{
					if (condition == ConditionalOperator.Equal)
					{
						return string.Format(((PageBase)Page).GetPageResource("BranchingRuleAnswer"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
							pageNumber);
					}
					else
					{
						return string.Format(((PageBase)Page).GetPageResource("BranchingRuleAnswerNot"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
							pageNumber);
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
						return string.Format(((PageBase)Page).GetPageResource("BranchingRuleAnswerWithText"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
							conditionText,
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(textFilter, "<[^>]*>", " ")),
							pageNumber);
					}
					else
					{
						return string.Format(((PageBase)Page).GetPageResource("BranchingRuleAnswerWithTextNot"),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(questionText, "<[^>]*>", " ")),
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(answerText, "<[^>]*>", " ")),
							conditionText,
							Server.HtmlEncode(System.Text.RegularExpressions.Regex.Replace(textFilter, "<[^>]*>", " ")),
							pageNumber);
					}
				}
			}
		}

		private void RulesRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			new Survey().DeleteBranchingRuleById(int.Parse(e.CommandArgument.ToString()));
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

		int _surveyId = -1,
			_pageNumber = 1;
		protected System.Web.UI.WebControls.Repeater RulesRepeater;
	}
}
