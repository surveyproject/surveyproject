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
	using System.Web;
	using System.Web.UI.WebControls;
	using Votations.NSurvey.Data;
	using Votations.NSurvey.BusinessRules;
	using Votations.NSurvey.Enums;

	/// <summary>
	///	Handles the question's edition options	
	/// </summary>
	public partial class QuestionOptionsControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.ImageButton UpImageButton;
		protected System.Web.UI.WebControls.HyperLink EditHyperLink;
		protected System.Web.UI.WebControls.LinkButton DeleteButton;
		protected System.Web.UI.WebControls.LinkButton CloneButton;
		protected System.Web.UI.WebControls.HyperLink InsertHyperLink;
		protected System.Web.UI.WebControls.LinkButton InsertPageBreakButton;
		protected System.Web.UI.WebControls.LinkButton InsertLineBreakButton;
		protected System.Web.UI.WebControls.HyperLink EditAnswersHyperLink;
		protected System.Web.UI.WebControls.HyperLink EditSkipLogicHyperLink;
		protected System.Web.UI.WebControls.ImageButton DownImageButton;
		protected SkipLogigRulesControl SkipLogicRules;

		// Count of questions in the set
		public int QuestionCount
		{
			get { return _questionCount; }
			set { _questionCount = value; }
		}

		public QuestionData.QuestionsRow Question
		{
			get { return _question; }
			set { _question = value; }
		}

		public bool PageBreak
		{
			get { return _pageBreak; }
			set { _pageBreak = value; }
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			LocalizePage();

			if (Question != null)
			{
				SkipLogicRules.QuestionId = Question.QuestionId;
				SkipLogicRules.BindData();

				UpImageButton.Visible = (Question.DisplayOrder > 1 || Question.PageNumber > 1);
				UpImageButton.CommandArgument = Question.QuestionId.ToString();
				
				DownImageButton.Visible = (Question.DisplayOrder<QuestionCount);;
				DownImageButton.CommandArgument = Question.QuestionId.ToString();
				
				int editable = (int)((QuestionTypeMode)Question.TypeMode & QuestionTypeMode.Editable),
					childQuestions = (int)((QuestionTypeMode)Question.TypeMode & QuestionTypeMode.ChildQuestion),
					answerable = (int)((QuestionTypeMode)Question.TypeMode & QuestionTypeMode.Answerable);

				if ( editable > 0 && childQuestions > 0)
				{
					EditHyperLink.NavigateUrl = 
						String.Format(UINavigator.EditMatrixQuestionLink+"?surveyid={0}&parentquestionid={1}&MenuIndex={2}", 
						Question.SurveyId, Question.QuestionId, ((PageBase)Page).MenuIndex);
				}
				else if (editable > 0)
				{
					EditHyperLink.NavigateUrl = 
						String.Format(UINavigator.EditSingleQuestionLink+"?surveyid={0}&questionid={1}&MenuIndex={2}", 
						Question.SurveyId, Question.QuestionId, ((PageBase)Page).MenuIndex);

					EditAnswersHyperLink.NavigateUrl = 
						String.Format(UINavigator.AnswerEditorHyperLink+"?surveyid={0}&questionid={1}&MenuIndex={2}", 
						Question.SurveyId, Question.QuestionId, ((PageBase)Page).MenuIndex);
				}				

				if (answerable > 0 && childQuestions == 0)
				{
					EditAnswersHyperLink.Enabled = true;
				}
				else
				{
					EditAnswersHyperLink.Enabled = false;
				}

				if (editable > 0)
				{
					EditHyperLink.Enabled = true;
				}
				else
				{
					EditHyperLink.Enabled = false;
				}

				if (Question.PageNumber < 2)
				{
					EditSkipLogicHyperLink.Enabled = false;
				}
				else
				{
					EditSkipLogicHyperLink.NavigateUrl = 
						String.Format(UINavigator.EditSkipLogicHyperLink+"?SurveyID={0}&QuestionId={1}&Page={2}&MenuIndex={3}", 
						Question.SurveyId, Question.QuestionId, Question.PageNumber, ((PageBase)Page).MenuIndex);
				}

				DeleteButton.Attributes.Add("onClick",
					"javascript:if(confirm('" +((PageBase)Page).GetPageResource("DeleteQuestionConfirmationMessage")+ "')== false) return false;");

				InsertHyperLink.NavigateUrl = 
					String.Format(UINavigator.InsertQuestionLink+"?SurveyID={0}&DisplayOrder={1}&Page={2}&MenuIndex={3}&QuestionId={4}", 
						Question.SurveyId, Question.DisplayOrder+1, Question.PageNumber, ((PageBase)Page).MenuIndex,Question.QuestionId);

				InsertPageBreakButton.CommandArgument = Question.DisplayOrder.ToString();
				InsertLineBreakButton.CommandArgument = Question.DisplayOrder.ToString();
			
				DeleteButton.CommandArgument = Question.SelectionModeId.ToString();
                lblId.Text = _question.QuestionIdText;
                lblHelpText.Text = _question.HelpText;
			}
		}

		private void LocalizePage()
		{
			EditAnswersHyperLink.Text = ((PageBase)Page).GetPageResource("EditAnswersHyperLink");
			EditHyperLink.Text = ((PageBase)Page).GetPageResource("EditHyperLink");
			DeleteButton.Text = ((PageBase)Page).GetPageResource("DeleteButton");
			CloneButton.Text = ((PageBase)Page).GetPageResource("CloneButton");
			InsertHyperLink.Text = ((PageBase)Page).GetPageResource("InsertHyperLink");
			InsertPageBreakButton.Text = ((PageBase)Page).GetPageResource("InsertPageBreakButton");
			InsertLineBreakButton.Text = ((PageBase)Page).GetPageResource("InsertLineBreakButton");
			EditSkipLogicHyperLink.Text = ((PageBase)Page).GetPageResource("EditSkipLogicHyperLink");
            lblHelpLabel.Text = ((PageBase)Page).GetPageResource("HelpTextLabel");
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
			this.UpImageButton.Click += new System.Web.UI.ImageClickEventHandler(this.UpImageButton_Click);
			this.DownImageButton.Click += new System.Web.UI.ImageClickEventHandler(this.DownImageButton_Click);
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.CloneButton.Click += new System.EventHandler(this.CloneButton_Click);
			this.InsertPageBreakButton.Click += new System.EventHandler(this.InsertPageBreakButton_Click);
			this.InsertLineBreakButton.Click += new System.EventHandler(this.InsertLineBreak_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UpImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			int questionId = int.Parse(((ImageButton)sender).CommandArgument);
			new Question().MoveQuestionPositionUp(questionId);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(Question.SurveyId, ((PageBase)Page).MenuIndex,Question.QuestionId);
		}

		private void DownImageButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			int questionId = int.Parse(((ImageButton)sender).CommandArgument);
			new Question().MoveQuestionPositionDown(questionId);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(Question.SurveyId, ((PageBase)Page).MenuIndex,Question.QuestionId);

		}

		private void DeleteButton_Click(object sender, System.EventArgs e)
		{
			new Question().DeleteQuestionById(Question.QuestionId);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(Question.SurveyId, ((PageBase)Page).MenuIndex,Question.QuestionId);
		}

		private void CloneButton_Click(object sender, System.EventArgs e)
		{
			new Question().CloneQuestionById(Question.QuestionId);
		
			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(Question.SurveyId, ((PageBase)Page).MenuIndex,Question.QuestionId);
		}

		private void InsertNewButton_Click(object sender, System.EventArgs e)
		{
			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(Question.SurveyId, ((PageBase)Page).MenuIndex,Question.QuestionId);
		}

		private void InsertPageBreakButton_Click(object sender, System.EventArgs e)
		{
	
			int displayOrder = int.Parse(((LinkButton)sender).CommandArgument);
			new Survey().InsertPageBreak(Question.SurveyId, displayOrder+1);

			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(Question.SurveyId, ((PageBase)Page).MenuIndex,Question.QuestionId);
		}

		private void InsertLineBreak_Click(object sender, System.EventArgs e)
		{
			int displayOrder = int.Parse(((LinkButton)sender).CommandArgument);
			
			new Survey().InsertSurveyLineBreak(Question.SurveyId, displayOrder+1, Question.PageNumber);
			
			// Reloads the builder
			UINavigator.NavigateToSurveyBuilder(Question.SurveyId, ((PageBase)Page).MenuIndex,Question.QuestionId);
		}

		int _questionCount = 0;
		bool _pageBreak = false;
		QuestionData.QuestionsRow _question;

	}
}
