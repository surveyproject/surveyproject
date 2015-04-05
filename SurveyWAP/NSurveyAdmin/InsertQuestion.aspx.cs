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
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.Enums;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Insert form for a new question
    /// </summary>
    public partial class InsertQuestion : PageBase
    {
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.Button AddNewSingleButton;
        protected System.Web.UI.WebControls.DropDownList QuestionDropDownList;
        protected System.Web.UI.WebControls.Button AddQuestionButton;

//      protected FreeTextBoxControls.FreeTextBox QuestionFreeTextBox;
        protected CKEditor.NET.CKEditorControl QuestionFreeTextBox;

        protected System.Web.UI.WebControls.Literal NewQuestionTitle;
        protected System.Web.UI.WebControls.Label QuestionLabel;
        protected System.Web.UI.WebControls.Label TypeLabel;
        protected System.Web.UI.WebControls.Label CopyExistingQuestionTitle;
        protected System.Web.UI.WebControls.Button CopyExistingQuestionButton;
        protected System.Web.UI.WebControls.DropDownList SurveyListDropdownlist;
        protected System.Web.UI.WebControls.DropDownList SurveyQuestionListDropdownlist;
        protected System.Web.UI.WebControls.Label ImportQuestionTitle;
        protected System.Web.UI.HtmlControls.HtmlInputFile ImportFile;
        new protected HeaderControl Header;
        protected System.Web.UI.WebControls.DropDownList SourceDropDownList;
        protected System.Web.UI.WebControls.DropDownList LibraryDropDownList;
        protected System.Web.UI.WebControls.DropDownList LibraryQuestionsDropDownList;
        protected System.Web.UI.WebControls.Button ImportXMLButton;
        protected PlaceHolder ImportXmlQuestionPlaceHolder;

        private void Page_Load(object sender, System.EventArgs e)
        {

            SetupSecurity();
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.FormBuilder);

            MessageLabel.Visible = false;

            _displayOrder =
                Information.IsNumeric(Request["DisplayOrder"]) ? int.Parse(Request["DisplayOrder"]) : 0;

            _pageNumber =
                Information.IsNumeric(Request["Page"]) && int.Parse(Request["Page"]) > 0 ? int.Parse(Request["Page"]) : 1;

            _libraryId =
                Information.IsNumeric(Request["LibraryId"]) ? int.Parse(Request["LibraryId"]) : -1;

            LocalizePage();

            if (!Page.IsPostBack)
            {
                // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = getSurveyId();
            }

            //CKEditor config:
            QuestionFreeTextBox.config.enterMode = CKEditor.NET.EnterMode.BR;
            QuestionFreeTextBox.config.skin = "moonocolor";

            QuestionFreeTextBox.config.toolbar = new object[]
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
            CheckRight(NSurveyRights.AccessFormBuilder, true);
            ImportXmlQuestionPlaceHolder.Visible = CheckRight(NSurveyRights.AllowXmlImport, false);
        }

        private void LocalizePage()
        {
            if (_libraryId != -1)
            {
                AddQuestionButton.Text = GetPageResource("AddQuestionToLibraryButton");

                LibraryData library = new Libraries().GetLibraryById(_libraryId);
                if (library.Libraries.Rows.Count != 0)
                {
                    NewQuestionTitle.Text = string.Format(
                        GetPageResource("NewLibraryQuestionTitle"), library.Libraries[0].LibraryName);
                }
            }
            else
            {
                NewQuestionTitle.Text = GetPageResource("NewQuestionTitle");
                AddQuestionButton.Text = GetPageResource("AddQuestionButton");
            }

            QuestionLabel.Text = GetPageResource("QuestionLabel");
            if (!Page.IsPostBack)
            {
                QuestionDropDownList.Items.Add(new ListItem(GetPageResource("SingleQuestionOption"), "single"));
                QuestionDropDownList.Items.Add(new ListItem(GetPageResource("MatrixQuestionOption"), "matrix"));
                QuestionDropDownList.Items.Add(new ListItem(GetPageResource("StaticTextQuestionOption"), "static"));

                SourceDropDownList.Items.Add(new ListItem(GetPageResource("SelectSourceMessage"), "-1"));
                if (_libraryId == -1 && CheckRight(NSurveyRights.CopyQuestionFromLibrary, false))
                {
                    SourceDropDownList.Items.Add(new ListItem(GetPageResource("LibraryOption"), "0"));
                }
                if (CheckRight(NSurveyRights.CopyQuestionFromAllSurvey, false) ||
                    NSurveyUser.Identity.HasAllSurveyAccess)
                {
                    SourceDropDownList.Items.Add(new ListItem(GetPageResource("SurveyOption"), "1"));
                }
            }
            ImportQuestionTitle.Text = GetPageResource("ImportQuestionTitle");
            ImportXMLButton.Text = GetPageResource("ImportXMLButton");
            TypeLabel.Text = GetPageResource("TypeLabel");
            if (_libraryId == -1)
            {
                CopyExistingQuestionButton.Text = GetPageResource("CopyExistingQuestionButton");
            }
            else
            {
                CopyExistingQuestionButton.Text = GetPageResource("CopyExistingQuestionToLibraryButton");
            }

            CopyExistingQuestionTitle.Text = GetPageResource("CopyExistingQuestionTitle");
            TypeLabel.Text = GetPageResource("TypeLabel");

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
            this.AddQuestionButton.Click += new System.EventHandler(this.AddQuestionButton_Click);
            this.SourceDropDownList.SelectedIndexChanged += new System.EventHandler(this.SourceDropDownList_SelectedIndexChanged);
            this.SurveyListDropdownlist.SelectedIndexChanged += new System.EventHandler(this.SurveyListDropdownlist_SelectedIndexChanged);
            this.LibraryDropDownList.SelectedIndexChanged += new System.EventHandler(this.LibraryDropDownList_SelectedIndexChanged);
            this.CopyExistingQuestionButton.Click += new System.EventHandler(this.CopyExistingQuestionButton_Click);
            this.ImportXMLButton.Click += new System.EventHandler(this.ImportXMLButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        protected void OnBackButton(object sender, System.EventArgs e)
        {
            int _questionId = -1;
            if (!(Request.QueryString["QuestionId"] != null && int.TryParse(Request.QueryString["QuestionId"], out _questionId)))
                _questionId = -1;
            Response.Redirect(UINavigator.LibraryTemplateReturnUrl() == null ?
               UINavigator.SurveyContentBuilderLink + (_questionId == -1 ? string.Empty : "?" + Constants.Constants.ScrollQuestionQstr + "=" + _questionId.ToString()) : UINavigator.LibraryTemplateReturnUrl());


            Response.Redirect(UINavigator.LibraryTemplateReturnUrl() == null ? UINavigator.SurveyContentBuilderLink : UINavigator.LibraryTemplateReturnUrl());
        }

        private void AddQuestionButton_Click(object sender, System.EventArgs e)
        {
            // Question cannot be empty
            if (QuestionFreeTextBox.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingQuestionMessage"));
            }
            else
            {
                // Removes any single paragraph description
                if (QuestionFreeTextBox.Text.StartsWith("<p>") &&
                    QuestionFreeTextBox.Text.EndsWith("</p>") &&
                    QuestionFreeTextBox.Text.IndexOf("<p>", 3) < 0)
                {
                    QuestionFreeTextBox.Text = QuestionFreeTextBox.Text.Substring(3, QuestionFreeTextBox.Text.Length - 7);
                }

                if (_libraryId == -1)
                {
                    AddQuestion();
                }
                else
                {
                    AddQuestionToLibrary();
                }
            }

        }

        private void AddQuestion()
        {
            if (QuestionDropDownList.SelectedValue == "single")
            {
                // Adds the new single question


                QuestionData newQuestion =
                    new Question().AddDefaultSingleQuestion(SurveyId, _displayOrder, _pageNumber,
                   Server.HtmlDecode(QuestionFreeTextBox.Text), txtQuestionID.Text);

                UINavigator.NavigateToSingleQuestionEdit(getSurveyId(), newQuestion.Questions[0].QuestionId, _libraryId, MenuIndex);
            }
            else if (QuestionDropDownList.SelectedValue == "matrix")
            {
                // Adds the new parent matrix question H:\SurveyProject\SurveyWAP\Images\
                QuestionData newQuestion =
                    new Question().AddDefaultMatrixQuestion(SurveyId, _displayOrder, _pageNumber,
                    Server.HtmlDecode(QuestionFreeTextBox.Text), txtQuestionID.Text);

                UINavigator.NavigateToMatrixQuestionEdit(SurveyId, newQuestion.Questions[0].QuestionId, _libraryId, MenuIndex);
            }
            else
            {
                new Question().AddStaticInformationText(SurveyId, _displayOrder, _pageNumber, QuestionFreeTextBox.Text, txtQuestionID.Text);
                UINavigator.NavigateToSurveyBuilder(SurveyId, MenuIndex);
            }
        }

        private void AddQuestionToLibrary()
        {
            if (QuestionDropDownList.SelectedValue == "single")
            {
                // Adds the new single question to the library 
                QuestionData newQuestion =
                    new Question().AddDefaultSingleQuestion(_libraryId, Server.HtmlDecode(QuestionFreeTextBox.Text), txtQuestionID.Text);

                UINavigator.NavigateToSingleQuestionEdit(getSurveyId(), newQuestion.Questions[0].QuestionId, _libraryId, MenuIndex);
            }
            else if (QuestionDropDownList.SelectedValue == "matrix")
            {
                // Adds the new parent matrix question to the library
                QuestionData newQuestion =
                    new Question().AddDefaultMatrixQuestion(_libraryId, Server.HtmlDecode(QuestionFreeTextBox.Text), txtQuestionID.Text);

                UINavigator.NavigateToMatrixQuestionEdit(getSurveyId(), newQuestion.Questions[0].QuestionId, _libraryId, MenuIndex);

            }
            else
            {
                new Question().AddStaticInformationText(_libraryId, Server.HtmlDecode(QuestionFreeTextBox.Text), txtQuestionID.Text);
                UINavigator.NavigateToLibraryTemplates(getSurveyId(), _libraryId, MenuIndex);
            }
        }

        private void CopyExistingQuestionButton_Click(object sender, System.EventArgs e)
        {
            if (_libraryId == -1)
            {
                int questionId;
                if (SourceDropDownList.SelectedValue == "0")
                {
                    questionId = int.Parse(LibraryQuestionsDropDownList.SelectedValue);
                }
                else
                {
                    questionId = int.Parse(SurveyQuestionListDropdownlist.SelectedValue);
                }


                new Question().CopyQuestionById(questionId,
                    SurveyId, _displayOrder, _pageNumber);

                UINavigator.NavigateToSurveyBuilder(SurveyId, MenuIndex);
            }
            else
            {
                new Question().CopyQuestionToLibrary(int.Parse(SurveyQuestionListDropdownlist.SelectedValue), _libraryId);
                UINavigator.NavigateToLibraryTemplates(getSurveyId(), _libraryId, MenuIndex);
            }
        }

        private void SurveyListDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SurveyListDropdownlist.SelectedValue == "-1")
            {
                SurveyQuestionListDropdownlist.Visible = false;
                CopyExistingQuestionButton.Enabled = false;
                SurveyQuestionListDropdownlist.Items.Clear();
            }
            else
            {
                SurveyQuestionListDropdownlist.Visible = true;
                QuestionData questions = new Questions().GetAnswerableQuestionListWithoutChilds(int.Parse(SurveyListDropdownlist.SelectedValue));
                if (questions.Questions.Rows.Count > 0)
                {
                    SurveyQuestionListDropdownlist.DataSource = questions;
                    SurveyQuestionListDropdownlist.DataMember = "questions";
                    SurveyQuestionListDropdownlist.DataTextField = "questionText";
                    SurveyQuestionListDropdownlist.DataValueField = "questionid";
                    SurveyQuestionListDropdownlist.DataBind();
                    CopyExistingQuestionButton.Enabled = true;
                }
                else
                {
                    SurveyQuestionListDropdownlist.Items.Clear();
                    SurveyQuestionListDropdownlist.Items.Insert(0, new ListItem(GetPageResource("NoQuestionToCopyAvailableMessage"), "-1"));
                }
            }
        }

        private void ImportXMLButton_Click(object sender, System.EventArgs e)
        {
            if (ImportFile.PostedFile != null)
            {
                NSurveyQuestion importedQuestions = new NSurveyQuestion();
                try
                {
                    importedQuestions.ReadXml(ImportFile.PostedFile.InputStream);


                    SetImportedQuestionsDefaults(importedQuestions);
                    new Question().ImportQuestions(importedQuestions, NSurveyUser.Identity.UserId);
                    if (_libraryId == -1)
                    {
                        UINavigator.NavigateToSurveyBuilder(SurveyId, MenuIndex);
                    }
                    else
                    {
                        UINavigator.NavigateToLibraryTemplates(getSurveyId(), _libraryId, MenuIndex);
                    }
                }
                catch (Exception ex)
                {
                    ((PageBase)Page).ShowErrorMessage(MessageLabel, ex.Message);
                    MessageLabel.Visible = true;
                }
            }
        }

        /// <summary>
        /// Set the imported questions local values (SurveyId, libraryid etc...)
        /// </summary>
        private void SetImportedQuestionsDefaults(NSurveyQuestion importedQuestions)
        {
            int questionDisplayOrder = _displayOrder;
            foreach (NSurveyQuestion.QuestionRow question in importedQuestions.Question)
            {
                if (_libraryId == -1)
                {
                    question.SurveyId = SurveyId;
                    question.PageNumber = _pageNumber;
                    question.DisplayOrder = questionDisplayOrder;
                    question.SetLibraryIdNull();
                }
                else
                {
                    question.SetSurveyIdNull();
                    question.PageNumber = 1;
                    question.DisplayOrder = 1;
                    question.LibraryId = _libraryId;
                }

                questionDisplayOrder++;
            }

            // Prevents SQL injection from custom hand written datasources Sql answer types in the import Xml 
            if (!GlobalConfig.SqlBasedAnswerTypesAllowed || !CheckRight(NSurveyRights.SqlAnswerTypesEdition, false))
            {
                foreach (NSurveyQuestion.AnswerTypeRow answerType in importedQuestions.AnswerType)
                {
                    answerType.DataSource = null;
                }
            }

        }

        private void SourceDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SourceDropDownList.SelectedValue == "-1")
            {
                SurveyListDropdownlist.Items.Clear();
                SurveyListDropdownlist.Visible = false;
                SurveyQuestionListDropdownlist.Items.Clear();
                SurveyQuestionListDropdownlist.Visible = false;
                LibraryDropDownList.Items.Clear();
                LibraryDropDownList.Visible = false;
                LibraryQuestionsDropDownList.Items.Clear();
                LibraryQuestionsDropDownList.Visible = false;
                CopyExistingQuestionButton.Enabled = false;

            }
            else if (SourceDropDownList.SelectedValue == "0")
            {
                SurveyListDropdownlist.Items.Clear();
                SurveyListDropdownlist.Visible = false;
                SurveyQuestionListDropdownlist.Items.Clear();
                SurveyQuestionListDropdownlist.Visible = false;

                // Retrieve the library data
                LibraryData libraryData =
                    new Libraries().GetLibraries();
                LibraryDropDownList.DataSource = libraryData;
                LibraryDropDownList.DataMember = "Libraries";
                LibraryDropDownList.DataTextField = "LibraryName";
                LibraryDropDownList.DataValueField = "LibraryId";
                LibraryDropDownList.DataBind();
                LibraryDropDownList.Items.Insert(0, new ListItem(GetPageResource("SelectLibraryMessage"), "-1"));
                LibraryDropDownList.Visible = true;
                CopyExistingQuestionButton.Enabled = false;
            }
            else
            {
                CheckRight(NSurveyRights.CopyQuestionFromAllSurvey, true);
                LibraryDropDownList.Items.Clear();
                LibraryDropDownList.Visible = false;
                LibraryQuestionsDropDownList.Items.Clear();
                LibraryQuestionsDropDownList.Visible = false;

                SurveyData surveys;
                /*
                if (CheckRight(NSurveyRights.CopyQuestionFromAllSurvey, false) ||
                    NSurveyUser.Identity.HasAllSurveyAccess)
              
                {
                    surveys = new Surveys().GetAllSurveysList();
                }
                else
                     
                {
                 * */
                surveys = new Surveys().GetAssignedSurveysList(NSurveyUser.Identity.UserId);


                SurveyListDropdownlist.DataSource = surveys;
                SurveyListDropdownlist.DataMember = "surveys";
                SurveyListDropdownlist.DataTextField = "title";
                SurveyListDropdownlist.DataValueField = "surveyid";
                SurveyListDropdownlist.DataBind();
                SurveyListDropdownlist.Items.Insert(0, new ListItem(GetPageResource("SelectSurveyMessage"), "-1"));
                SurveyListDropdownlist.Visible = true;
                CopyExistingQuestionButton.Enabled = false;
            }

        }

        private void LibraryDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (SurveyListDropdownlist.SelectedValue == "-1")
            {
                LibraryQuestionsDropDownList.Visible = false;
                LibraryQuestionsDropDownList.Items.Clear();
                CopyExistingQuestionButton.Enabled = false;
            }
            else
            {
                LibraryQuestionsDropDownList.Visible = true;

                QuestionData questions = new Questions().GetLibraryQuestionListWithoutChilds(int.Parse(LibraryDropDownList.SelectedValue));
                if (questions.Questions.Rows.Count > 0)
                {
                    LibraryQuestionsDropDownList.DataSource = questions;
                    LibraryQuestionsDropDownList.DataMember = "questions";
                    LibraryQuestionsDropDownList.DataTextField = "questionText";
                    LibraryQuestionsDropDownList.DataValueField = "questionid";
                    LibraryQuestionsDropDownList.DataBind();
                    CopyExistingQuestionButton.Enabled = true;
                }
                else
                {
                    LibraryQuestionsDropDownList.Items.Clear();
                    LibraryQuestionsDropDownList.Items.Insert(0, new ListItem(GetPageResource("NoQuestionToCopyAvailableMessage"), "-1"));
                }
            }
        }

        private int _displayOrder = 1,
            _pageNumber = 1,
            _libraryId = -1;

        protected void CopyExistingQuestionButton_Click1(object sender, EventArgs e)
        {

        }

    }

}
