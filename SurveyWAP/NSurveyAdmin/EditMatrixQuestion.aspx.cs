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
using Votations.NSurvey.Enums;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using System.Data;
using CKEditor;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Edit a matrix question type
    /// </summary>
    public partial class EditMatrixQuestion : PageBase
    {
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.DropDownList MinSelectionDropDownList;
        protected System.Web.UI.WebControls.DropDownList MaxAllowedDropDownList;
        protected System.Web.UI.WebControls.Button UpdateQuestionButton;
        protected System.Web.UI.WebControls.Button AddRowButton;
        protected System.Web.UI.WebControls.Button AddColumnButton;
        protected System.Web.UI.WebControls.DropDownList AnswerTypeDropDownList;
        protected System.Web.UI.WebControls.TextBox NewRowTextBox;
        protected System.Web.UI.WebControls.TextBox NewColumnTextBox;
        protected System.Web.UI.WebControls.CheckBox MultipleChoiceCheckbox;
        protected System.Web.UI.WebControls.DataGrid RowsDataGrid;
        protected System.Web.UI.WebControls.DataGrid ColsDataGrid;
        protected System.Web.UI.WebControls.CheckBox RatingCheckbox;

        //protected FreeTextBoxControls.FreeTextBox QuestionFreeTextBox;
        //protected CKEditor.NET.CKEditorControl QuestionFreeTextBox;

        protected System.Web.UI.WebControls.Literal EditMatrixQuestionTitle;
        protected System.Web.UI.WebControls.Label MultipleChoicesMatrixLabel;
        protected System.Web.UI.WebControls.Label AnswerRatingLabel;
        protected System.Web.UI.WebControls.Literal InsertNewTitle;
        protected System.Web.UI.WebControls.Label RowLabel;
        protected System.Web.UI.WebControls.Label ColumnLabel;
        protected System.Web.UI.WebControls.Literal CurrentRowsColumnsTitle;
        protected System.Web.UI.WebControls.Button ExportXMLButton;
        protected System.Web.UI.WebControls.Label MinSelectionMatrixLabel;
        protected System.Web.UI.WebControls.Label MaxSelectionMatrixAllowedLabel;
        protected System.Web.UI.WebControls.Label AllowMultipleMatrixSectionsLabel;
        protected System.Web.UI.WebControls.CheckBox RepeatSectionCheckbox;
        protected System.Web.UI.WebControls.Label AddRepeatSectionLabel;
        protected System.Web.UI.WebControls.TextBox AddSectionLinkTextBox;
        protected System.Web.UI.WebControls.Label DeleteRepeatSectionLabel;
        protected System.Web.UI.WebControls.TextBox DeleteSectionLinkTextBox;
        protected System.Web.UI.WebControls.PlaceHolder RepeatSectionOptionPlaceholder;
        protected System.Web.UI.WebControls.Label EditionLanguageLabel;
        protected System.Web.UI.WebControls.DropDownList LanguagesDropdownlist;
        protected System.Web.UI.WebControls.Label ChildsEditionLanguageLabel;
        protected System.Web.UI.WebControls.DropDownList ChildsLanguageDropdownlist;

        public string GetScrollParentQuestionId()
        {
            return Request.QueryString[Constants.Constants.ScrollParentQuestionQstr];
        }

        new protected HeaderControl Header;
        int getLibraryId()
        {

            if (ViewState["LibrayID"] == null) return -1;
            return (int)ViewState["LibrayID"];
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.FormBuilder);
            SetupSecurity();
            ValidateRequestParentQuestionId();
            LocalizePage();
            MessageLabel.Visible = false;
            if (!Page.IsPostBack)
            {

                // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = getSurveyId();
                BindQuestionOptions();
                BindChildQuestions();
                BindAnswerOptions();
                BindLanguages();
            }


            //CKEditor settings:
            //ThankYouCKEditor.config.toolbar = "Simple";
            //ThankYouCKEditor.config.uiColor = "#DDDDDD";
            QuestionFreeTextBox.config.enterMode = CKEditor.NET.EnterMode.BR;
            QuestionFreeTextBox.config.skin = "moonocolor";

            // In case of fixed default language, e.g. Dutch:
            // ThankYouCKEditor.config.language = "nl";

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
        }

        private void LocalizePage()
        {
            EditMatrixQuestionTitle.Text = GetPageResource("EditMatrixQuestionTitle");
            MultipleChoicesMatrixLabel.Text = GetPageResource("MultipleChoicesMatrixLabel");
            AnswerRatingLabel.Text = GetPageResource("AnswerRatingLabel");
            MinSelectionMatrixLabel.Text = GetPageResource("MinSelectionMatrixLabel");
            MaxSelectionMatrixAllowedLabel.Text = GetPageResource("MaxSelectionMatrixAllowedLabel");
            AllowMultipleMatrixSectionsLabel.Text = GetPageResource("AllowMultipleMatrixSectionsLabel");
            UpdateQuestionButton.Text = GetPageResource("UpdateQuestionButton");
            InsertNewTitle.Text = GetPageResource("InsertNewTitle");
            RowLabel.Text = GetPageResource("RowLabel");
            ExportXMLButton.Text = GetPageResource("ExportXMLButton");
            ColumnLabel.Text = GetPageResource("ColumnLabel");
            AddColumnButton.Text = GetPageResource("AddColumnButton");
            AddRowButton.Text = GetPageResource("AddRowButton");
            DeleteRepeatSectionLabel.Text = GetPageResource("DeleteRepeatSectionLabel");
            AddRepeatSectionLabel.Text = GetPageResource("AddRepeatSectionLabel");
            CurrentRowsColumnsTitle.Text = GetPageResource("CurrentRowsColumnsTitle");
            EditionLanguageLabel.Text = GetPageResource("EditionLanguageLabel");
            ChildsEditionLanguageLabel.Text = GetPageResource("EditionLanguageLabel");
            AliasLabel.Text = GetPageResource("AliasLabel");
            HelpTextLabel.Text = GetPageResource("HelpTextLabel");
            ShowHelpTextLabel.Text = GetPageResource("ShowHelpTextLabel");
            GroupLabel.Text = GetPageResource("GroupLabel");
            SubGroupLabel.Text = GetPageResource("SubGroupLabel");

            ((BoundColumn)RowsDataGrid.Columns[0]).HeaderText = GetPageResource("RowsHeader");
            ((EditCommandColumn)RowsDataGrid.Columns[1]).UpdateText = GetPageResource("UpdateRowTextHeader");
            ((EditCommandColumn)RowsDataGrid.Columns[1]).CancelText = GetPageResource("CancelRowTextHeader");
            //((EditCommandColumn)RowsDataGrid.Columns[1]).EditText = GetPageResource("EditRowTextHeader");
            //((ButtonColumn)RowsDataGrid.Columns[2]).Text = GetPageResource("DeleteRowText");

            ((BoundColumn)ColsDataGrid.Columns[0]).HeaderText = GetPageResource("AnswerHeader");
            ((TemplateColumn)ColsDataGrid.Columns[1]).HeaderText = GetPageResource("ColTypeHeader");
            ((TemplateColumn)ColsDataGrid.Columns[2]).HeaderText = GetPageResource("ColRatingHeader");
            ((TemplateColumn)ColsDataGrid.Columns[3]).HeaderText = GetPageResource("ColMandatoryHeader");
            ((EditCommandColumn)ColsDataGrid.Columns[4]).UpdateText = GetPageResource("UpdateColTextHeader");
            ((EditCommandColumn)ColsDataGrid.Columns[4]).CancelText = GetPageResource("CancelColTextHeader");
            //((EditCommandColumn)ColsDataGrid.Columns[4]).EditText = GetPageResource("EditColTextHeader");
            //((ButtonColumn)ColsDataGrid.Columns[5]).Text = GetPageResource("DeleteColText");

            if (!Page.IsPostBack)
            {
                MinSelectionDropDownList.Items.Insert(0, new ListItem(GetPageResource("NoSelectionRequiredOption"), "0"));
                MaxAllowedDropDownList.Items.Insert(0, new ListItem(GetPageResource("UnlimitedSelectionOption"), "0"));
            }

        }

        private void ValidateRequestParentQuestionId()
        {
            _parentQuestionId =
                Information.IsNumeric(Request["ParentQuestionId"]) ? int.Parse(Request["ParentQuestionId"]) : -1;

            if (_parentQuestionId != -1 && !NSurveyUser.Identity.IsAdmin &&
                !NSurveyUser.Identity.HasAllSurveyAccess)
            {
                if (!new Question().CheckQuestionUser(_parentQuestionId, NSurveyUser.Identity.UserId))
                {
                    _parentQuestionId = -1;
                }
            }


            if (_parentQuestionId == -1)
            {
                //throw new FormatException("Invalid matrix question id!");
                throw new FormatException(GetPageResource("InvalidMatrixQuestionID"));
            }
        }


        /// <summary>
        /// Set the forms to match DB question options
        /// </summary>
        private void BindQuestionOptions()
        {
            // Retrieve the original question values
            QuestionData question = new Questions().GetQuestionById(_parentQuestionId, LanguagesDropdownlist.SelectedValue);
            QuestionData.QuestionsRow questionRow = question.Questions[0];
            QuestionExtraLinks1.Initialize(questionRow.SurveyId, questionRow.QuestionId, questionRow.DisplayOrder, questionRow.PageNumber);
            // 
            // Assign them to the form
            QuestionFreeTextBox.Text = questionRow.QuestionText;
            MinSelectionDropDownList.SelectedValue =
                (questionRow.IsMinSelectionRequiredNull()) ? "0" : questionRow.MinSelectionRequired.ToString();
            MaxAllowedDropDownList.SelectedValue =
                (questionRow.IsMaxSelectionAllowedNull()) ? "0" : questionRow.MaxSelectionAllowed.ToString();

            MultipleChoiceCheckbox.Checked = questionRow.SelectionModeId == (int)QuestionSelectionMode.MultiMatrix;
            RatingCheckbox.Checked = questionRow.RatingEnabled;
            ViewState["LibrayID"] = questionRow.IsLibraryIdNull() ? -1 : questionRow.LibraryId;

            if (!questionRow.IsLibraryIdNull() && !NSurveyUser.Identity.IsAdmin)
            {
                CheckRight(NSurveyRights.ManageLibrary, true);
            }

            QuestionSectionOptionData sectionOptions = new Questions().GetQuestionSectionOptions(_parentQuestionId, LanguagesDropdownlist.SelectedValue);
            if (sectionOptions.QuestionSectionOptions.Rows.Count > 0)
            {

                RepeatSectionCheckbox.Checked = true;
                RepeatSectionOptionPlaceholder.Visible = true;
                AddSectionLinkTextBox.Text = sectionOptions.QuestionSectionOptions[0].AddSectionLinkText;
                DeleteSectionLinkTextBox.Text = sectionOptions.QuestionSectionOptions[0].DeleteSectionLinkText;
            }
            else
            {
                RepeatSectionCheckbox.Checked = false;
                RepeatSectionOptionPlaceholder.Visible = false;


            }


            RatingCheckbox.Checked = questionRow.RatingEnabled;

            EnableDisableGroupFields(RatingCheckbox.Checked);
            BindQuestionGroups();

            txtQuestionID.Text = questionRow.QuestionIdText;
            txtAlias.Text = questionRow.Alias;
            txtHelpText.Text = questionRow.HelpText;
            chbShowHelpText.Checked = (questionRow.ShowHelpText != null) ? (bool)questionRow.ShowHelpText : false;
        }

        private void BindChildQuestions()
        {
            MatrixChildQuestionData childQuestions = new MatrixChildQuestionData();
            childQuestions = new Questions().GetMatrixChildQuestions(_parentQuestionId, ChildsLanguageDropdownlist.SelectedValue);
            RowsDataGrid.DataSource = childQuestions;
            RowsDataGrid.DataMember = "ChildQuestions";
            RowsDataGrid.DataKeyField = "QuestionID";
            RowsDataGrid.DataBind();
        }

        private void BindLanguages()
        {
            MultiLanguageMode languageMode = MultiLanguageMode.UserSelection;
            if (getLibraryId() == -1)
                languageMode = new MultiLanguages().GetMultiLanguageMode(SurveyId);
            if (getLibraryId() > -1 || languageMode != MultiLanguageMode.None)
            {
                MultiLanguageData surveyLanguages;
                if (getLibraryId() == -1)
                    surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);
                else
                    surveyLanguages = new MultiLanguages().GetSurveyLanguages(getLibraryId(), Constants.Constants.EntityLibrary);
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
                    ChildsLanguageDropdownlist.Items.Add(defaultItem);
                }

                LanguagesDropdownlist.Visible = true;
                EditionLanguageLabel.Visible = true;
                ChildsEditionLanguageLabel.Visible = true;
                ChildsLanguageDropdownlist.Visible = true;
            }
            else
            {
                LanguagesDropdownlist.Visible = false;
                EditionLanguageLabel.Visible = false;
                ChildsEditionLanguageLabel.Visible = false;
                ChildsLanguageDropdownlist.Visible = false;
            }
        }

        private void BindAnswerOptions()
        {
            if (NSurveyUser.Identity.IsAdmin ||
                NSurveyUser.Identity.HasAllSurveyAccess)
            {
                _answerTypes = new AnswerTypes().GetAnswerTypesList();
            }
            else
            {
                _answerTypes =
                    new AnswerTypes().GetAssignedAnswerTypesList(NSurveyUser.Identity.UserId, getSurveyId());
            }

            AnswerTypeDropDownList.DataSource = _answerTypes;
            AnswerTypeDropDownList.DataMember = "AnswerTypes";
            AnswerTypeDropDownList.DataTextField = "Description";
            AnswerTypeDropDownList.DataValueField = "AnswerTypeID";
            AnswerTypeDropDownList.DataBind();
            TranslateListControl(AnswerTypeDropDownList);
            AnswerTypeDropDownList.Items.Insert(0,
                new ListItem(GetPageResource("SelectTypeMessage"), "-1"));
            AnswerTypeDropDownList.SelectedValue = "1";
            _answers = new Answers().GetAnswers(_parentQuestionId, ChildsLanguageDropdownlist.SelectedValue);
            ColsDataGrid.Columns[2].Visible = RatingCheckbox.Checked;
            ColsDataGrid.DataSource = _answers;
            ColsDataGrid.DataMember = "Answers";
            ColsDataGrid.DataKeyField = "AnswerId";
            ColsDataGrid.DataBind();
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
            this.LanguagesDropdownlist.SelectedIndexChanged += new System.EventHandler(this.LanguagesDropdownlist_SelectedIndexChanged);
            this.RepeatSectionCheckbox.CheckedChanged += new System.EventHandler(this.RepeatSectionCheckbox_CheckedChanged);
            this.UpdateQuestionButton.Click += new System.EventHandler(this.UpdateQuestionButton_Click);
            this.ExportXMLButton.Click += new System.EventHandler(this.ExportXMLButton_Click);
            this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            this.AddColumnButton.Click += new System.EventHandler(this.AddColumnButton_Click);
            this.RatingCheckbox.CheckedChanged += new EventHandler(RatingCheckbox_CheckedChanged);
            this.ChildsLanguageDropdownlist.SelectedIndexChanged += new System.EventHandler(this.ChildsLanguageDropdownlist_SelectedIndexChanged);
            this.RowsDataGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.RowsDataGrid_CancelCommand);
            this.RowsDataGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.RowsDataGrid_EditCommand);
            this.RowsDataGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.RowsDataGrid_UpdateCommand);
            this.RowsDataGrid.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.RowsDataGrid_DeleteCommand);
            this.ColsDataGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ColsDataGrid_CancelCommand);
            this.ColsDataGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ColsDataGrid_EditCommand);
            this.ColsDataGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ColsDataGrid_UpdateCommand);
            this.ColsDataGrid.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ColsDataGrid_DeleteCommand);
            this.ColsDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.ColsDataGrid_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        private void BindQuestionGroups()
        {
            QuestionData question = new Questions().GetQuestionById(_parentQuestionId, LanguagesDropdownlist.SelectedValue);
            QuestionData.QuestionsRow questionRow = question.Questions[0];
            int questionGroupId = questionRow.IsQuestionGroupIDNull() ? -1 : (int)questionRow.QuestionGroupID;

            int selectedQuestionGroupId = -1;

            QuestionGroupsData data = new Votations.NSurvey.DataAccess.QuestionGroups().GetAll(LanguagesDropdownlist.SelectedValue);
            DataView groupView = data.DefaultViewManager.CreateDataView(data.Tables[0]);
            DataView subGroupView = data.DefaultViewManager.CreateDataView(data.Tables[0]);
            groupView.RowFilter = "ParentGroupId is null";


            foreach (QuestionGroupsData.QuestionGroupsRow row in data.Tables[0].Rows)
            {
                if (row.ID == questionGroupId)
                {
                    selectedQuestionGroupId = (row.ParentGroupID != null) ? (int)row.ParentGroupID : questionGroupId;
                    break;
                }
            }


            if (ddlGroup.SelectedIndex > -1 && ddlGroup.SelectedValue != selectedQuestionGroupId.ToString())
                selectedQuestionGroupId = int.Parse(ddlGroup.SelectedValue);

            subGroupView.RowFilter = "ParentGroupId = " + selectedQuestionGroupId;
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-Select-", "-1"));
            ddlSubGroup.Items.Clear();
            ddlSubGroup.Items.Add(new ListItem("-Select-", "-1"));

            ddlGroup.DataSource = groupView;
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataValueField = "ID";
            ddlGroup.DataBind();

            ddlSubGroup.DataSource = subGroupView;
            ddlSubGroup.DataTextField = "GroupName";
            ddlSubGroup.DataValueField = "ID";
            ddlSubGroup.DataBind();

            foreach (ListItem item in ddlGroup.Items)
            {
                if (item.Value == selectedQuestionGroupId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }

            foreach (ListItem item in ddlSubGroup.Items)
            {
                if (item.Value == questionGroupId.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        private void EnableDisableGroupFields(bool enableFlag)
        {
            ddlGroup.Visible = enableFlag;
            ddlSubGroup.Visible = enableFlag;
            GroupLabel.Visible = enableFlag;
            SubGroupLabel.Visible = enableFlag;
        }
        void RatingCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableGroupFields(RatingCheckbox.Checked);
            BindQuestionGroups();
        }
        #endregion

        protected void OnddlGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindQuestionGroups();
        }

        private void RowsDataGrid_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            RowsDataGrid.EditItemIndex = e.Item.ItemIndex;
            BindChildQuestions();
        }

        private void RowsDataGrid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            RowsDataGrid.EditItemIndex = -1;
            BindChildQuestions();
        }

        private void RowsDataGrid_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

            TextBox updatedChildText = ((TextBox)e.Item.Cells[0].Controls[0]);
            if (updatedChildText.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("RowMissingQuestionMessage"));
            }
            else
            {
                MatrixChildQuestionData childQuestion = new MatrixChildQuestionData();
                MatrixChildQuestionData.ChildQuestionsRow updatedChildQuestion =
                childQuestion.ChildQuestions.NewChildQuestionsRow();
                updatedChildQuestion.QuestionId =
                    int.Parse(RowsDataGrid.DataKeys[e.Item.ItemIndex].ToString());
                updatedChildQuestion.QuestionText = updatedChildText.Text;
                childQuestion.ChildQuestions.AddChildQuestionsRow(updatedChildQuestion);
                new Question().UpdateChildQuestion(childQuestion, ChildsLanguageDropdownlist.SelectedValue);
                RowsDataGrid.EditItemIndex = -1;
                BindChildQuestions();
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("MatrixRowUpdatedMessage"));
            }
        }

        private void RowsDataGrid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            new Question().DeleteQuestionById(
                int.Parse(RowsDataGrid.DataKeys[e.Item.ItemIndex].ToString()));
            BindChildQuestions();
            MessageLabel.Visible = true;
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("MatrixRowDeletedMessage"));

        }

        private void AddRowButton_Click(object sender, System.EventArgs e)
        {
            if (NewRowTextBox.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("RowMissingQuestiondMessage"));
            }
            else
            {
                MatrixChildQuestionData childQuestion = new MatrixChildQuestionData();
                MatrixChildQuestionData.ChildQuestionsRow updatedChildQuestion =
                    childQuestion.ChildQuestions.NewChildQuestionsRow();
                updatedChildQuestion.ParentQuestionId = _parentQuestionId;
                updatedChildQuestion.QuestionText = NewRowTextBox.Text;
                childQuestion.ChildQuestions.AddChildQuestionsRow(updatedChildQuestion);
                new Question().AddChildQuestion(childQuestion);

                BindChildQuestions();
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("MatrixRowAddedMessage"));

                NewRowTextBox.Text = string.Empty;
            }
        }

        private void UpdateQuestionButton_Click(object sender, System.EventArgs e)
        {
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

                // creates a BE and update the question
                QuestionData question = new QuestionData();
                QuestionData.QuestionsRow updatedQuestion =
                    question.Questions.NewQuestionsRow();

                // Set the updated fields
                updatedQuestion.QuestionId = _parentQuestionId;
                updatedQuestion.SurveyId = getSurveyId();
                updatedQuestion.QuestionText = QuestionFreeTextBox.Text.Length > 3900 ?
                  Server.HtmlDecode(QuestionFreeTextBox.Text.Substring(0, 3900)) : Server.HtmlDecode(QuestionFreeTextBox.Text);
                updatedQuestion.MinSelectionRequired = int.Parse(MinSelectionDropDownList.SelectedValue);
                updatedQuestion.MaxSelectionAllowed = int.Parse(MaxAllowedDropDownList.SelectedValue);
                updatedQuestion.RatingEnabled = RatingCheckbox.Checked;
                updatedQuestion.RandomizeAnswers = false;
                updatedQuestion.QuestionIdText = txtQuestionID.Text;
                updatedQuestion.ShowHelpText = chbShowHelpText.Checked;
                updatedQuestion.Alias = txtAlias.Text;
                updatedQuestion.HelpText = txtHelpText.Text;
                updatedQuestion.QuestionIdText = txtQuestionID.Text;
                updatedQuestion.SetQuestionGroupIDNull();
                if (ddlGroup.SelectedIndex != 0)
                    updatedQuestion.QuestionGroupID = int.Parse(ddlGroup.SelectedValue);
                if (ddlSubGroup.SelectedIndex != 0)
                    updatedQuestion.QuestionGroupID = int.Parse(ddlSubGroup.SelectedValue);
                if (MultipleChoiceCheckbox.Checked)
                {
                    updatedQuestion.SelectionModeId = (int)QuestionSelectionMode.MultiMatrix;
                }
                else
                {
                    updatedQuestion.SelectionModeId = (int)QuestionSelectionMode.Matrix;
                }

                question.Questions.AddQuestionsRow(updatedQuestion);

                new Question().UpdateQuestion(question, LanguagesDropdownlist.SelectedValue);

                // Matrix can be repeated
                if (RepeatSectionCheckbox.Checked)
                {
                    // creates a BE and update the options
                    QuestionSectionOptionData sectionOptions = new QuestionSectionOptionData();
                    QuestionSectionOptionData.QuestionSectionOptionsRow updatedSectionOption =
                        sectionOptions.QuestionSectionOptions.NewQuestionSectionOptionsRow();

                    // Set the updated fields
                    updatedSectionOption.QuestionId = _parentQuestionId;
                    updatedSectionOption.RepeatableSectionModeId = (int)RepeatableSectionMode.FullAnswers;
                    updatedSectionOption.DeleteSectionLinkText = DeleteSectionLinkTextBox.Text;
                    updatedSectionOption.AddSectionLinkText = AddSectionLinkTextBox.Text;
                    sectionOptions.QuestionSectionOptions.AddQuestionSectionOptionsRow(updatedSectionOption);
                    new Question().UpdateQuestionSectionOptions(sectionOptions, LanguagesDropdownlist.SelectedValue);
                }
                else
                {
                    new Question().DeleteQuestionSectionOptions(_parentQuestionId);
                    DeleteSectionLinkTextBox.Text = string.Empty;
                    AddSectionLinkTextBox.Text = string.Empty;
                }

                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("OptionsUpdatedMessage"));


                BindQuestionOptions();
                BindAnswerOptions();
            }

        }

        private void AddColumnButton_Click(object sender, System.EventArgs e)
        {
            if (AnswerTypeDropDownList.SelectedValue == "-1")
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingTypeMessage"));
                MessageLabel.Visible = true;
            }
            else
            {

                AnswerData answer = new AnswerData();
                AnswerData.AnswersRow newAnswer = answer.Answers.NewAnswersRow();

                newAnswer.QuestionId = _parentQuestionId;
                newAnswer.AnswerText = NewColumnTextBox.Text;
                newAnswer.AnswerTypeId =
                    int.Parse(AnswerTypeDropDownList.SelectedValue);
                answer.Answers.AddAnswersRow(newAnswer);

                new Answer().AddMatrixAnswer(answer);

                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("MatrixColumnAddedMessage"));

                NewColumnTextBox.Text = string.Empty;
                BindAnswerOptions();
            }
        }


        private void ColsDataGrid_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            DataGridItem gridItem = (DataGridItem)e.Item;
            ColsDataGrid.EditItemIndex = gridItem.ItemIndex;

            // Rebind the answers
            BindAnswerOptions();

            // Enable the dropdownlist 
            DropDownList currentAnswerTypes =
                (DropDownList)ColsDataGrid.Items[gridItem.ItemIndex].Cells[1].Controls[1];
            currentAnswerTypes.Enabled = true;

            CheckBox ratingCheckBox =
                (CheckBox)ColsDataGrid.Items[gridItem.ItemIndex].Cells[3].FindControl("RatingPartCheckBox"),
                mandatoryCheckBox =
                (CheckBox)ColsDataGrid.Items[gridItem.ItemIndex].Cells[4].FindControl("MandatoryCheckBox");


            ratingCheckBox.Enabled = true;
            mandatoryCheckBox.Enabled = true;
        }


        private void ColsDataGrid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ColsDataGrid.EditItemIndex = -1;
            BindAnswerOptions();

        }

        /// <summary>
        /// Generates the templated column with the
        /// selection mode
        /// </summary>
        private void ColsDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            DataGridItem gridItem = (DataGridItem)e.Item;
            if (gridItem.ItemType != ListItemType.Footer && gridItem.ItemType != ListItemType.Header)
            {
                DropDownList answerTypesDropDownList =
                    (DropDownList)gridItem.Cells[1].Controls[1];
                answerTypesDropDownList.DataSource = _answerTypes;
                answerTypesDropDownList.DataMember = "AnswerTypes";
                answerTypesDropDownList.DataTextField = "Description";
                answerTypesDropDownList.DataValueField = "AnswerTypeID";
                answerTypesDropDownList.DataBind();
                TranslateListControl(answerTypesDropDownList);

                answerTypesDropDownList.Enabled = false;

                if (AnswerTypeDropDownList.Items.FindByValue(_answers.Answers[gridItem.DataSetIndex].AnswerTypeId.ToString()) != null)
                {
                    answerTypesDropDownList.SelectedValue =
                        _answers.Answers[gridItem.DataSetIndex].AnswerTypeId.ToString();
                }
                else
                {
                    answerTypesDropDownList.SelectedValue = "1";
                }

                int typeMode = int.Parse(DataBinder.Eval(e.Item.DataItem, "TypeMode").ToString());
                bool ratePart = (bool)DataBinder.Eval(gridItem.DataItem, "RatePart");
                CheckBox ratingPartCheckBox = (CheckBox)gridItem.FindControl("RatingPartCheckBox");

                // Can this answer be rated ?
                if ((((AnswerTypeMode)typeMode & AnswerTypeMode.Selection) > 0))
                {
                    ratingPartCheckBox.Visible = true;
                    if (ratePart)
                    {
                        ((Label)gridItem.FindControl("RatingLabel")).Text = _currentRating.ToString();
                        ratingPartCheckBox.Checked = true;
                        _currentRating++;
                    }
                    else
                    {
                        ((Label)gridItem.FindControl("RatingLabel")).Text = "0";
                    }
                }
                else
                {
                    ratingPartCheckBox.Visible = false;
                    ratingPartCheckBox.Checked = false;
                }

                CheckBox mandatoryCheckBox = (CheckBox)gridItem.FindControl("MandatoryCheckBox");
                // Can this answer be mandatory  ?
                if ((((AnswerTypeMode)typeMode & AnswerTypeMode.Mandatory) > 0))
                {
                    mandatoryCheckBox.Visible = true;
                    mandatoryCheckBox.Checked = (bool)DataBinder.Eval(gridItem.DataItem, "Mandatory"); ;
                }
                else
                {
                    mandatoryCheckBox.Visible = false;
                    mandatoryCheckBox.Checked = false;
                }

            }

        }


        /// <summary>
        /// Updates the columns
        /// </summary>
        private void ColsDataGrid_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            DataGridItem gridItem = (DataGridItem)e.Item;
            AnswerData answer = new AnswerData();

            AnswerData.AnswersRow updatedAnswer = answer.Answers.NewAnswersRow();

            updatedAnswer.AnswerText = ((TextBox)gridItem.Cells[0].Controls[0]).Text;
            updatedAnswer.AnswerTypeId =
                int.Parse(((DropDownList)gridItem.Cells[1].Controls[1]).SelectedValue);
            updatedAnswer.AnswerId = int.Parse(ColsDataGrid.DataKeys[gridItem.ItemIndex].ToString());
            updatedAnswer.RatePart = ((CheckBox)gridItem.Cells[2].FindControl("RatingPartCheckBox")).Checked;
            updatedAnswer.Mandatory = ((CheckBox)gridItem.Cells[3].FindControl("MandatoryCheckBox")).Checked;
            answer.Answers.AddAnswersRow(updatedAnswer);

            new Answer().UpdateMatrixAnswer(answer, ChildsLanguageDropdownlist.SelectedValue);

            ColsDataGrid.EditItemIndex = -1;
            BindAnswerOptions();

            MessageLabel.Visible = true;
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("MatrixColumnUpdatedMessage"));

        }

        /// <summary>
        /// Deletes the answer column
        /// </summary>
        private void ColsDataGrid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            new Answer().DeleteMatrixAnswer(
                int.Parse(ColsDataGrid.DataKeys[e.Item.ItemIndex].ToString()));

            MessageLabel.Visible = true;
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("MatrixColumnDeletedMessage"));
            BindAnswerOptions();
        }

        private void ExportXMLButton_Click(object sender, System.EventArgs e)
        {
            Response.Charset = "utf-8";
            Response.ContentType = "application/octet-stream";

            Response.ContentType = "text/xml";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"nsurvey_question" + _parentQuestionId + ".xml\"");

            NSurveyQuestion questionForm = new Questions().GetQuestionForExport(_parentQuestionId);
            questionForm.WriteXml(Response.OutputStream, System.Data.XmlWriteMode.IgnoreSchema);
            Response.End();
        }

        private void RepeatSectionCheckbox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (RepeatSectionCheckbox.Checked)
            {
                RepeatSectionOptionPlaceholder.Visible = true;
            }
            else
            {
                RepeatSectionOptionPlaceholder.Visible = false;
            }
        }


        int _parentQuestionId,
            _currentRating = 1;
        AnswerTypeData _answerTypes;
        AnswerData _answers;

        private void LanguagesDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindQuestionOptions();
        }

        private void ChildsLanguageDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindChildQuestions();
            BindAnswerOptions();
        }


        protected void OnBackButton(object sender, System.EventArgs e)
        {
            int _questionId = -1;
            if (!(Request.QueryString["parentquestionid"] != null && int.TryParse(Request.QueryString["parentquestionid"], out _questionId)))
                _questionId = -1;
            Response.Redirect(UINavigator.LibraryTemplateReturnUrl() == null ?
               UINavigator.SurveyContentBuilderLink + (_questionId == -1 ? string.Empty : "?" + Constants.Constants.ScrollQuestionQstr + "=" + _questionId.ToString()) : UINavigator.LibraryTemplateReturnUrl());
        }

    }

}
