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
	using Votations.NSurvey.DataAccess;
	using Votations.NSurvey.BusinessRules;
	using Votations.NSurvey.Enums;
    using System.Web.UI;

	/// <summary>
	///	Handles the question's edition options	
	/// </summary>
    public partial class LibraryQuestionOptionsControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.HyperLink EditHyperLink;
		protected System.Web.UI.WebControls.LinkButton DeleteButton;
		protected System.Web.UI.WebControls.HyperLink EditAnswersHyperLink;
		protected System.Web.UI.WebControls.LinkButton ExportButton;
        
		public QuestionData.QuestionsRow Question
		{
			get { return _question; }
			set { _question = value; }
		}
        public CommandEventHandler EventOrderUp { get; set; }
        public CommandEventHandler EventOrderDown { get; set; }
        public string QuestionId { get; set; }
        public string QuestionHelpText { get; set; }

        protected void OnOrderUp(object sender, CommandEventArgs e)
        {
            if (EventOrderUp != null)
                EventOrderUp(this, e);
        }

        protected void OnOrderDown(object sender, CommandEventArgs e)
        {
            if (EventOrderDown != null)
                EventOrderDown(this, e);
        }

        /*
            Events for buttons are set Initilize() in order to work properly
            btnQuestionDown.Command += new CommandEventHandler(OnOrderDown);
            btnQuestionUP.Command += new CommandEventHandler(OnOrderUp);

         */
        /*
        protected override void  OnInit(EventArgs e)
        {
            btnQuestionDown.Command += new CommandEventHandler(OnOrderDown);
            btnQuestionUP.Command += new CommandEventHandler(OnOrderUp);

 	        base.OnInit(e);
        }
        */
        
		private void Page_Load(object sender, System.EventArgs e)
		{
			LocalizePage();

            btnQuestionDown.CommandArgument = QuestionId;
            btnQuestionDown.CommandName = "OrderDown";
            btnQuestionUP.CommandArgument = QuestionId;
            btnQuestionUP.CommandName = "OrderUp";
            lblQuestionId.Text = Question.QuestionIdText;
            lblQuestionHelpText.Text = Question.HelpText;

			if (Question != null)
			{
				int editable = (int)((QuestionTypeMode)Question.TypeMode & QuestionTypeMode.Editable),
					childQuestions = (int)((QuestionTypeMode)Question.TypeMode & QuestionTypeMode.ChildQuestion),
					answerable = (int)((QuestionTypeMode)Question.TypeMode & QuestionTypeMode.Answerable);

				if ( editable > 0 && childQuestions > 0)
				{
					EditHyperLink.NavigateUrl = 
						String.Format(UINavigator.EditMatrixQuestionLink+"?surveyid={0}&parentquestionid={1}&libraryid={2}&MenuIndex={3}", 
						((PageBase)Page).getSurveyId(), Question.QuestionId, Question.LibraryId, ((PageBase)Page).MenuIndex);
				}
				else if (editable > 0)
				{
					EditHyperLink.NavigateUrl = 
						String.Format(UINavigator.EditSingleQuestionLink+"?surveyid={0}&questionid={1}&libraryid={2}&MenuIndex={3}", 
						((PageBase)Page).getSurveyId(), Question.QuestionId, Question.LibraryId, ((PageBase)Page).MenuIndex);

					EditAnswersHyperLink.NavigateUrl = 
						String.Format(UINavigator.AnswerEditorHyperLink+"?surveyid={0}&questionid={1}&libraryid={2}&MenuIndex={3}", 
						((PageBase)Page).getSurveyId(), Question.QuestionId, Question.LibraryId, ((PageBase)Page).MenuIndex);
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

				DeleteButton.Attributes.Add("onClick",
					"javascript:if(confirm('" +((PageBase)Page).GetPageResource("DeleteQuestionConfirmationMessage")+ "')== false) return false;");
				DeleteButton.CommandArgument = Question.SelectionModeId.ToString();
			}
		}

		private void LocalizePage()
		{
			EditAnswersHyperLink.Text = ((PageBase)Page).GetPageResource("EditAnswersHyperLink");
			EditHyperLink.Text = ((PageBase)Page).GetPageResource("EditHyperLink");
			DeleteButton.Text = ((PageBase)Page).GetPageResource("DeleteButton");
			ExportButton.Text = ((PageBase)Page).GetPageResource("ExportButton");
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
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);
            btnQuestionDown.Command += new CommandEventHandler(OnOrderDown);
            btnQuestionUP.Command += new CommandEventHandler(OnOrderUp);

		}
		#endregion

		private void DeleteButton_Click(object sender, System.EventArgs e)
		{
			new Question().DeleteQuestionById(Question.QuestionId);

			// Reloads the library
			UINavigator.NavigateToLibraryTemplates(((PageBase)Page).getSurveyId(), Question.LibraryId, ((PageBase)Page).MenuIndex);
		}

		QuestionData.QuestionsRow _question;

		private void ExportButton_Click(object sender, System.EventArgs e)
		{
			Response.Charset = "utf-8";
			Response.ContentType = "application/octet-stream";
			
			Response.ContentType = "text/xml";
			Response.AddHeader("Content-Disposition", "attachment; filename=\"nsurvey_question"+Question.QuestionId+".xml\"");

			NSurveyQuestion questionForm = new Questions().GetQuestionForExport(Question.QuestionId);
			questionForm.WriteXml(Response.OutputStream, System.Data.XmlWriteMode.IgnoreSchema);
			Response.End();
		}
	}
}
