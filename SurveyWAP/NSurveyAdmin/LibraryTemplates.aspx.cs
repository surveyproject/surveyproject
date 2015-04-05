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
using Microsoft.VisualBasic;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.WebControls.UI;
using Votations.NSurvey.WebControlsFactories;
using Votations.NSurvey.Web;

using Votations.NSurvey.WebAdmin.NSurveyAdmin;


namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the items of the given library
	/// </summary>
    public partial class LibraryTemplates : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Literal LibraryQuestionsTemplatesTitle;
		protected System.Web.UI.WebControls.PlaceHolder TemplatesPlaceHolder;
		protected System.Web.UI.WebControls.Button InsertQuestionButton;

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }

        int _libraryId=-1;
		private void Page_Load(object sender, System.EventArgs e)
		{
			SetupSecurity();
			MessageLabel.Visible = false;

		//	_libraryId = 
			//	Information.IsNumeric(Request["LibraryId"]) ? int.Parse(Request["LibraryId"]) : -1;
            _libraryId=((PageBase)Page).LibraryId;
           
            CheckVariablesSet();
			LocalizePage();
            UITabList.SetLibraryTemplatesTabs((MsterPageTabs)Page.Master, UITabList.LibraryTemplatesTabs.LibraryTemplates);

	
			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = getSurveyId();
                BindLanguages();
               
			}
            BuildTemplateList();

		}

        private void CheckVariablesSet()
        {
           
        }
		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessLibrary, true);
			InsertQuestionButton.Visible = NSurveyUser.Identity.IsAdmin || CheckRight(NSurveyRights.ManageLibrary, false);
		}

		private void LocalizePage()
		{
			LibraryQuestionsTemplatesTitle.Text = GetPageResource("QuestionTemplatesTitle");
			InsertQuestionButton.Text = GetPageResource("InsertQuestionButton");
            PreviewSurveyLanguageLabel.Text = GetPageResource("PreviewLibraryLanguageLabel");
		}

              private void BindLanguages()
        {
        
                 MultiLanguageData surveyLanguages ;
              
                     surveyLanguages = new MultiLanguages().GetSurveyLanguages(_libraryId, Constants.Constants.EntityLibrary);
                LanguagesDropdownlist.Items.Clear();
                foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
                {
                    ListItem defaultItem = new ListItem(GetPageResource(language.LanguageDescription), language.LanguageCode);
                    if (language.DefaultLanguage)
                    {
                        defaultItem.Value = "";
                        defaultItem.Text += " " + GetPageResource("LanguageDefaultText");
                    }

                    LanguagesDropdownlist.Items.Add(defaultItem);
                 
                }

                LanguagesDropdownlist.Visible = true;
               
           
        }
		private void BuildTemplateList()
		{
            TemplatesPlaceHolder.Controls.Clear();
			_questionsData = new Questions().GetLibraryQuestions(_libraryId,LanguagesDropdownlist.SelectedValue);	
			
			foreach (QuestionData.QuestionsRow question in _questionsData.Questions.Rows)
			{
				Table questionsContainer = new Table();
				questionsContainer.Width = Unit.Percentage(100);
				Table questionTable = new Table();
				questionTable.CellSpacing = 2;
				questionTable.CellPadding = 4;
				questionTable.CssClass = "questionBuilder";
				AddQuestionWebControl(questionTable, question);
                questionsContainer.Rows.Add(BuildRow(questionTable, ""));
				TemplatesPlaceHolder.Controls.Add(questionsContainer);
			}

		}


		private TableRow BuildRow(Control child, string hlpText)
		{
			TableRow row = new TableRow();
			TableCell cell  = new TableCell();
			cell.Controls.Add(child);
            Label lblHelpText = new Label();
            lblHelpText.Text = hlpText;
            cell.Controls.Add(lblHelpText);

			row.Cells.Add(cell);
			return row;
		}

		/// <summary>
		/// Get a web control instance of the question row 
		/// and adds it with its options to the table 
		/// </summary>
		private void AddQuestionWebControl(Table questionTable, QuestionData.QuestionsRow question)
		{
			QuestionItem questionWebControl = QuestionItemFactory.Create(question, LanguagesDropdownlist.SelectedValue, this.UniqueID, 0, null, true,true);

			// Set question's style
			// and bind the data
			Style questionStyle = new Style();
			questionStyle.CssClass = "surveyQuestion";
			Style answerStyle = new Style();
			answerStyle.CssClass = "surveyAnswer";	
			Style markStyle = new Style();

			if (questionWebControl is ActiveQuestion)
			{
				((ActiveQuestion)questionWebControl).EnableClientSideValidation = false;
				((ActiveQuestion)questionWebControl).EnableServerSideValidation = false;
			}
			if (questionWebControl is SectionQuestion)
			{
				((SectionQuestion)questionWebControl).SectionOptionStyle.CssClass = "questionOptions";
			}
			if (questionWebControl is MatrixQuestion)
			{
				((MatrixQuestion)questionWebControl).MatrixHeaderStyle = answerStyle;
				((MatrixQuestion)questionWebControl).MatrixItemStyle = answerStyle;
				((MatrixQuestion)questionWebControl).MatrixAlternatingItemStyle = answerStyle;
			}

			questionWebControl.RenderMode = ControlRenderMode.ReadOnly;
			questionWebControl.AnswerStyle = answerStyle;
			questionWebControl.QuestionStyle = questionStyle;
			questions.Add(questionWebControl);
						
			// Add the question and its options to the table
			if (NSurveyUser.Identity.IsAdmin || CheckRight(NSurveyRights.ManageLibrary, false))
			{
				questionTable.Rows.Add(BuildQuestionOptionsRow(question));
			}
			
			questionTable.Rows.Add(BuildRow(questionWebControl, ""));
		}

        protected void OnOrderUp(object sender,  CommandEventArgs e)
        {
            int qid = int.Parse(((LibraryQuestionOptionsControl)sender).QuestionId);
            new Questions().MoveQuestionUp(qid);
            UINavigator.NavigateToLibraryTemplates(getSurveyId(), _libraryId, -1);
        }

        protected void OnOrderDown(object sender, CommandEventArgs e)
        {
            int qid = int.Parse(((LibraryQuestionOptionsControl)sender).QuestionId);
            new Questions().MoveQuestionDown(qid);
            UINavigator.NavigateToLibraryTemplates(getSurveyId(), _libraryId, -1);
        }

		private TableRow BuildQuestionOptionsRow(QuestionData.QuestionsRow question)
		{
			LibraryQuestionOptionsControl libraryQuestionOptionsControl = 
				(LibraryQuestionOptionsControl)LoadControl("UserControls/LibraryQuestionOptionsControl.ascx");
			libraryQuestionOptionsControl.Question = question;
            libraryQuestionOptionsControl.EventOrderUp += new CommandEventHandler(OnOrderUp);
            libraryQuestionOptionsControl.EventOrderDown += new CommandEventHandler(OnOrderDown);
            libraryQuestionOptionsControl.QuestionId = question.QuestionId.ToString();
            //libraryQuestionOptionsControl.QuestionHelpText = question.q
			return BuildRow(libraryQuestionOptionsControl, "");
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
			this.InsertQuestionButton.Click += new System.EventHandler(this.InsertQuestionButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void InsertQuestionButton_Click(object sender, System.EventArgs e)
		{
			UINavigator.NavigateToInsertQuestion(getSurveyId(), 1, 1, _libraryId, MenuIndex);
		}


		
		QuestionData _questionsData;
		private QuestionItemCollection questions = new QuestionItemCollection();

        protected void LanguagesDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildTemplateList();
        }

	}

}
