/**************************************************************************************************
	Survey changes: copyright (c) 2010, W3DevPro TM (http://survey.codeplex.com)	

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
using Microsoft.VisualBasic;
using MetaBuilders.WebControls;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Enums;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using System.Data;


namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Edit form for a single question
    /// </summary>
    public partial class EditSingleQuestion : PageBase
    {
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.DropDownList SelectionModeDropDownList;
        protected System.Web.UI.WebControls.DropDownList DisplayModeDropDownList;
        protected System.Web.UI.WebControls.Button UpdateQuestionButton;
        protected System.Web.UI.WebControls.DropDownList MinSelectionDropDownList;
        protected System.Web.UI.WebControls.DropDownList MaxAllowedDropDownList;
        protected System.Web.UI.WebControls.CheckBox RandomizeCheckBox;
        protected System.Web.UI.WebControls.CheckBox RatingCheckbox;
        protected System.Web.UI.WebControls.PlaceHolder AnswerOptionsPlaceholder;

//      protected FreeTextBoxControls.FreeTextBox QuestionFreeTextBox;
        protected CKEditor.NET.CKEditorControl QuestionFreeTextBox;

        protected System.Web.UI.WebControls.DropDownList ColumnDropdownlist;
        protected System.Web.UI.WebControls.Literal EditQuestionTitle;
        protected System.Web.UI.WebControls.Label SelectionModeLabel;
        protected System.Web.UI.WebControls.Label DisplayModeLabel;
        protected System.Web.UI.WebControls.Label ColumnNumberLabel;
        protected System.Web.UI.WebControls.Label RandomizeAnswersLabel;
        protected System.Web.UI.WebControls.Label AnswerRatingLabel;
        protected System.Web.UI.WebControls.Label MinSelectionLabel;
        protected System.Web.UI.WebControls.Label MaxSelectionAllowed;
        protected System.Web.UI.WebControls.Button EditAnswersButton;
        protected System.Web.UI.WebControls.Label QuestionPipeAliasLabel;
        protected System.Web.UI.WebControls.TextBox PipeAliasTextBox;
        protected System.Web.UI.WebControls.Button ExportXMLButton;
        protected System.Web.UI.WebControls.Literal RepeatableSectionsLabel;
        protected System.Web.UI.WebControls.Label RepeatModeLiteral2;
        protected System.Web.UI.WebControls.DropDownList RepeatModeDropdownlist;
        protected System.Web.UI.WebControls.Button UpdateSectionsButton;
        protected System.Web.UI.WebControls.Label RepeatModeLabel;
        protected System.Web.UI.WebControls.Label AddRepeatSectionLabel;
        protected System.Web.UI.WebControls.Label DeleteRepeatSectionLabel;
        protected System.Web.UI.WebControls.Label UpdateRepeatSectionLabel;
        protected System.Web.UI.WebControls.TextBox AddSectionLinkTextBox;
        protected System.Web.UI.WebControls.TextBox DeleteSectionLinkTextBox;
        protected System.Web.UI.WebControls.TextBox UpdateSectionLinkTextBox;
        protected System.Web.UI.WebControls.TextBox EditSectionLinkTextBox;
        protected System.Web.UI.WebControls.Label GridAnswersLabel;
        protected System.Web.UI.WebControls.ListBox GridAnswersListBox;
        protected System.Web.UI.WebControls.ListBox AnswersListBox;
        protected System.Web.UI.WebControls.Label GridAnswersChoiceLabel;
        protected System.Web.UI.WebControls.Label EditRepeatSectionLabel;
        protected System.Web.UI.WebControls.TextBox EditSectionTextbox;
        protected System.Web.UI.WebControls.PlaceHolder RepeatSectionOptionPlaceHolder;
        protected System.Web.UI.WebControls.Label MaxSectionAllowedLabel;
        protected System.Web.UI.WebControls.DropDownList MaxSectionAllowedDropdownlist;
        protected System.Web.UI.WebControls.Label EditionLanguageLabel;
        protected System.Web.UI.WebControls.DropDownList LanguagesDropdownlist;

        new protected HeaderControl Header;

        protected bool MultipleSelection
        {
            get { return (bool)ViewState["MultipleSelection"]; }
            set { ViewState["MultipleSelection"] = value; }
        }
        int getLibraryId()
        {
            if (ViewState["LibraryId"] == null) return -1;
            return (int)ViewState["LibraryId"];
        }
        void EstablishLibraryOrNot()
        {
            QuestionData question = new Questions().GetQuestionById(_questionId, LanguagesDropdownlist.SelectedValue);
            QuestionData.QuestionsRow questionRow = question.Questions[0];
            ViewState["LibraryId"] = questionRow.IsLibraryIdNull() ? -1 : questionRow.LibraryId;
            if (!questionRow.IsLibraryIdNull() && !NSurveyUser.Identity.IsAdmin)
            {
                CheckRight(NSurveyRights.ManageLibrary, true);
            }

            // Can the question have answers
        }
        private void Page_Load(object sender, System.EventArgs e)
        {
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.FormBuilder);

            SetupSecurity();
            LocalizePage();

            ValidateRequestQuestionId();
            MessageLabel.Visible = false;
            if (!Page.IsPostBack)
            {
                // Header.SurveyId = SurveyId;
                EstablishLibraryOrNot();
                ((Wap)Master.Master).HeaderControl.SurveyId = getSurveyId();
                BindLanguages();
                FillDefaultFields();
                BindQuestionOptions();
                BindQuestionSectionOptions();
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
        }

        private void LocalizePage()
        {
            EditQuestionTitle.Text = GetPageResource("EditQuestionTitle");
            SelectionModeLabel.Text = GetPageResource("SelectionModeLabel");
            DisplayModeLabel.Text = GetPageResource("DisplayModeLabel");
            ColumnNumberLabel.Text = GetPageResource("ColumnNumberLabel");
            RandomizeAnswersLabel.Text = GetPageResource("RandomizeAnswersLabel");
            AnswerRatingLabel.Text = GetPageResource("AnswerRatingLabel");
            MinSelectionLabel.Text = GetPageResource("MinSelectionLabel");
            MaxSelectionAllowed.Text = GetPageResource("MaxSelectionAllowed");
            UpdateQuestionButton.Text = GetPageResource("UpdateQuestionButton");
            EditAnswersButton.Text = GetPageResource("EditAnswersButton");
            QuestionPipeAliasLabel.Text = GetPageResource("QuestionPipeAliasLabel");
            ExportXMLButton.Text = GetPageResource("ExportXMLButton");
            RepeatModeLabel.Text = GetPageResource("RepeatModeLabel");
            DeleteRepeatSectionLabel.Text = GetPageResource("DeleteRepeatSectionLabel");
            AddRepeatSectionLabel.Text = GetPageResource("AddRepeatSectionLabel");
            EditRepeatSectionLabel.Text = GetPageResource("EditRepeatSectionLabel");
            UpdateRepeatSectionLabel.Text = GetPageResource("UpdateRepeatSectionLabel");
            MaxSectionAllowedLabel.Text = GetPageResource("MaxSectionAllowedLabel");
            GridAnswersLabel.Text = GetPageResource("GridAnswersLabel");
            RepeatableSectionsLabel.Text = GetPageResource("RepeatableSectionsLabel");
            UpdateSectionsButton.Text = GetPageResource("UpdateSectionsButton");
            EditionLanguageLabel.Text = GetPageResource("EditionLanguageLabel");
            AliasLabel.Text = GetPageResource("AliasLabel");
            HelpTextLabel.Text = GetPageResource("HelpTextLabel");
            ShowHelpTextLabel.Text = GetPageResource("ShowHelpTextLabel");
            GroupLabel.Text = GetPageResource("GroupLabel");
            SubGroupLabel.Text = GetPageResource("SubGroupLabel");

            MinSelectionDropDownList.ToolTip = ((PageBase)Page).GetPageHelpfiles("RequiredMarkerSettings");
            MaxAllowedDropDownList.ToolTip = ((PageBase)Page).GetPageHelpfiles("RequiredMarkerSettings");

            if (!Page.IsPostBack)
            {
                ColumnDropdownlist.Items.Insert(0, new ListItem(GetPageResource("NoSelectionRequiredOption"), "0"));
                MinSelectionDropDownList.Items.Insert(0, new ListItem(GetPageResource("NoSelectionRequiredOption"), "0"));
                MaxAllowedDropDownList.Items.Insert(0, new ListItem(GetPageResource("UnlimitedSelectionOption"), "0"));

                MaxSectionAllowedDropdownlist.Items.Insert(0, new ListItem(GetPageResource("MaxSectionAllowedUnlimitedOption"), "0"));
                RepeatModeDropdownlist.Items.Add(new ListItem(GetPageResource("RepeatNoneOption"), ((int)RepeatableSectionMode.None).ToString()));
                RepeatModeDropdownlist.Items.Add(new ListItem(GetPageResource("RepeatSectionOption"), ((int)RepeatableSectionMode.FullAnswers).ToString()));
                RepeatModeDropdownlist.Items.Add(new ListItem(GetPageResource("GridRepeatSectionOption"), ((int)RepeatableSectionMode.GridAnswers).ToString()));
            }
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
                throw new FormatException(GetPageResource("InvalidQuestion"));
            }
        }

        /// <summary>
        /// Get the current DB stats and fill 
        /// the label with them
        /// </summary>
        private void FillDefaultFields()
        {
            DisplayModeDropDownList.DataSource = new Questions().GetQuestionLayoutModes();
            DisplayModeDropDownList.DataMember = "LayoutModes";
            DisplayModeDropDownList.DataTextField = "Description";
            DisplayModeDropDownList.DataValueField = "LayoutModeID";
            DisplayModeDropDownList.DataBind();
            TranslateListControl(DisplayModeDropDownList);

            SelectionModeDropDownList.DataSource = new Questions().GetSelectableQuestionSelectionModes();
            SelectionModeDropDownList.DataMember = "QuestionSelectionModes";
            SelectionModeDropDownList.DataTextField = "Description";
            SelectionModeDropDownList.DataValueField = "QuestionSelectionModeID";
            SelectionModeDropDownList.DataBind();
            TranslateListControl(SelectionModeDropDownList);
        }

        /// <summary>
        /// Set the forms to match DB question options
        /// </summary>
        private void BindQuestionOptions()
        {
            // Retrieve the original question values
            QuestionData question = new Questions().GetQuestionById(_questionId, LanguagesDropdownlist.SelectedValue);
            QuestionData.QuestionsRow questionRow = question.Questions[0];

            QuestionExtraLinks1.Initialize(questionRow.SurveyId, questionRow.QuestionId, questionRow.DisplayOrder, questionRow.PageNumber);
 
            // Assign them to the form
            QuestionFreeTextBox.Text = questionRow.QuestionText;
            txtQuestionID.Text = questionRow.QuestionIdText;
            DisplayModeDropDownList.SelectedValue =
                (questionRow.IsLayoutModeIdNull()) ? ((int)QuestionLayoutMode.Horizontal).ToString() : questionRow.LayoutModeId.ToString();
            ColumnDropdownlist.SelectedValue = questionRow.ColumnsNumber.ToString();
            SelectionModeDropDownList.SelectedValue = questionRow.SelectionModeId.ToString();
            MinSelectionDropDownList.SelectedValue =
                (questionRow.IsMinSelectionRequiredNull()) ? "0" : questionRow.MinSelectionRequired.ToString();
            MaxAllowedDropDownList.SelectedValue =
                (questionRow.IsMaxSelectionAllowedNull()) ? "0" : questionRow.MaxSelectionAllowed.ToString();

             //JL: Enable or disable the Min Max dropdown list depending on selectionmodedropdownlist option selected by user
             EnableDisableMinMaxDropDownList(); 

            RandomizeCheckBox.Checked = questionRow.RandomizeAnswers;
            RatingCheckbox.Checked = questionRow.RatingEnabled;

            RatingCheckbox.Checked = questionRow.RatingEnabled;

            EnableDisableGroupFields(RatingCheckbox.Checked);
            BindQuestionGroups();

            PipeAliasTextBox.Text = questionRow.QuestionPipeAlias;
            RandomizeCheckBox.Enabled = !RatingCheckbox.Checked;

            ViewState["LibraryId"] = questionRow.IsLibraryIdNull() ? -1 : questionRow.LibraryId;
            if (!questionRow.IsLibraryIdNull() && !NSurveyUser.Identity.IsAdmin)
            {
                CheckRight(NSurveyRights.ManageLibrary, true);
            }

            // Can the question have answers
            if (((QuestionTypeMode)questionRow.TypeMode & QuestionTypeMode.Answerable) > 0)
            {
                AnswerOptionsPlaceholder.Visible = true;
                RepeatSectionOptionPlaceHolder.Visible = true;
                EditAnswersButton.Enabled = true;
                MultipleSelection = ((QuestionTypeMode)questionRow.TypeMode & QuestionTypeMode.MultipleAnswers) > 0;
            }
            else
            {
                RepeatSectionOptionPlaceHolder.Visible = false;
                AnswerOptionsPlaceholder.Visible = false;
                EditAnswersButton.Enabled = false;
                new Question().DeleteQuestionSectionOptions(_questionId);
            }

            if (questionRow.RatingEnabled)
                BindQuestionGroups();
            else
            {
                ddlGroup.Enabled = true;
                ddlSubGroup.Enabled = true;
            }

            txtAlias.Text = questionRow.Alias;
            txtHelpText.Text = questionRow.HelpText;
            chbShowHelpText.Checked = (questionRow.ShowHelpText) ? (bool)questionRow.ShowHelpText : false;
        }

       

        private void BindQuestionGroups()
        {

            QuestionData question = new Questions().GetQuestionById(_questionId, LanguagesDropdownlist.SelectedValue);
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
            ddlGroup.Items.Add(new ListItem(GetPageResource("DdlNoSelect"), "-1"));
            ddlSubGroup.Items.Clear();
            ddlSubGroup.Items.Add(new ListItem(GetPageResource("DdlNoSelect"), "-1"));

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

        /// <summary>
        /// Set the forms to match DB question options
        /// </summary>
        private void BindQuestionSectionOptions()
        {
            QuestionSectionOptionData sectionOptions = new Questions().GetQuestionSectionOptions(_questionId, LanguagesDropdownlist.SelectedValue);
            if (sectionOptions.QuestionSectionOptions.Rows.Count > 0)
            {
                RepeatModeDropdownlist.SelectedValue = sectionOptions.QuestionSectionOptions[0].RepeatableSectionModeId.ToString();
                MaxSectionAllowedDropdownlist.SelectedValue = sectionOptions.QuestionSectionOptions[0].MaxSections.ToString();
                AddSectionLinkTextBox.Text = sectionOptions.QuestionSectionOptions[0].AddSectionLinkText;
                DeleteSectionLinkTextBox.Text = sectionOptions.QuestionSectionOptions[0].DeleteSectionLinkText;
                UpdateSectionLinkTextBox.Text = sectionOptions.QuestionSectionOptions[0].UpdateSectionLinkText;
                EditSectionLinkTextBox.Text = sectionOptions.QuestionSectionOptions[0].EditSectionLinkText;
            }

            SetSectionUiState();
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
                else surveyLanguages = new MultiLanguages().GetSurveyLanguages(getLibraryId(), Constants.Constants.EntityLibrary);
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
                EditionLanguageLabel.Visible = true;
            }
            else
            {
                LanguagesDropdownlist.Visible = false;
                EditionLanguageLabel.Visible = false;
            }
        }

        protected void OnddlGroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindQuestionGroups();
        }

        protected void OnBackButton(object sender, System.EventArgs e)
        {

            Response.Redirect(UINavigator.LibraryTemplateReturnUrl() == null ?
                UINavigator.SurveyContentBuilderLink + (_questionId == -1 ? string.Empty : "?" + Constants.Constants.ScrollQuestionQstr + "=" + _questionId.ToString()) : UINavigator.LibraryTemplateReturnUrl());
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
            this.RatingCheckbox.CheckedChanged += new System.EventHandler(this.RatingCheckbox_CheckedChanged);
            this.UpdateQuestionButton.Click += new System.EventHandler(this.UpdateQuestionButton_Click);
            this.EditAnswersButton.Click += new System.EventHandler(this.EditAnswersButton_Click);
            this.ExportXMLButton.Click += new System.EventHandler(this.ExportXMLButton_Click);
            this.AnswersListBox.SelectedIndexChanged += new System.EventHandler(this.AnswersListBox_SelectedIndexChanged);
            this.GridAnswersListBox.SelectedIndexChanged += new System.EventHandler(this.GridAnswersListBox_SelectedIndexChanged);
            this.UpdateSectionsButton.Click += new System.EventHandler(this.UpdateSectionsButton_Click);

            //JL:
            this.SelectionModeDropDownList.SelectedIndexChanged += new System.EventHandler(this.SelectionModeDropDownList_SelectedIndexChanged);

            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


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
                updatedQuestion.QuestionId = _questionId;
                updatedQuestion.SurveyId = getSurveyId();
                updatedQuestion.QuestionText = QuestionFreeTextBox.Text.Length > 3900 ?
                   Server.HtmlDecode(QuestionFreeTextBox.Text.Substring(0, 3900)) : Server.HtmlDecode(QuestionFreeTextBox.Text);
                updatedQuestion.ColumnsNumber = int.Parse(ColumnDropdownlist.SelectedValue);
                updatedQuestion.MinSelectionRequired = int.Parse(MinSelectionDropDownList.SelectedValue);
                updatedQuestion.MaxSelectionAllowed = int.Parse(MaxAllowedDropDownList.SelectedValue);
                updatedQuestion.LayoutModeId = byte.Parse(DisplayModeDropDownList.SelectedValue);
                updatedQuestion.SelectionModeId = byte.Parse(SelectionModeDropDownList.SelectedValue);
                updatedQuestion.RandomizeAnswers = RatingCheckbox.Checked ? false : RandomizeCheckBox.Checked;
                updatedQuestion.RatingEnabled = RatingCheckbox.Checked;
                updatedQuestion.QuestionPipeAlias = PipeAliasTextBox.Text;
                updatedQuestion.QuestionIdText = txtQuestionID.Text;
                updatedQuestion.ShowHelpText = chbShowHelpText.Checked;
                updatedQuestion.Alias = txtAlias.Text;
                updatedQuestion.HelpText = txtHelpText.Text;
                updatedQuestion.SetQuestionGroupIDNull();
                if (ddlGroup.SelectedIndex != 0)
                    updatedQuestion.QuestionGroupID = int.Parse(ddlGroup.SelectedValue);
                if (ddlSubGroup.SelectedIndex != 0)
                    updatedQuestion.QuestionGroupID = int.Parse(ddlSubGroup.SelectedValue);
                if (!RatingCheckbox.Checked) updatedQuestion.SetQuestionGroupIDNull();
                question.Questions.AddQuestionsRow(updatedQuestion);

                new Question().UpdateQuestion(question, LanguagesDropdownlist.SelectedValue);

                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("OptionsUpdatedMessage"));


                BindQuestionOptions();
            }
        }

        private void EnableDisableGroupFields(bool enableFlag)
        {
            ddlGroup.Visible = enableFlag;
            ddlSubGroup.Visible = enableFlag;
            GroupLabel.Visible = enableFlag;
            SubGroupLabel.Visible = enableFlag;
        }

        protected void RatingCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableGroupFields(RatingCheckbox.Checked);
            BindQuestionGroups();
        }

        private void ExportXMLButton_Click(object sender, System.EventArgs e)
        {
            Response.Charset = "utf-8";
            Response.ContentType = "application/octet-stream";

            Response.ContentType = "text/xml";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"nsurvey_question" + _questionId + ".xml\"");

            NSurveyQuestion questionForm = new Questions().GetQuestionForExport(_questionId);
            questionForm.WriteXml(Response.OutputStream, System.Data.XmlWriteMode.IgnoreSchema);
            Response.End();
        }

        int _questionId;

        private void EditAnswersButton_Click(object sender, System.EventArgs e)
        {
            UINavigator.NavigateToSingleQuestionAnswersEdit(getSurveyId(), _questionId, (int)ViewState["LibraryId"], MenuIndex);
        }

        private void UpdateSectionsButton_Click(object sender, System.EventArgs e)
        {
            if (int.Parse(RepeatModeDropdownlist.SelectedValue) == (int)RepeatableSectionMode.None)
            {
                // Deletes section options
                new Question().DeleteQuestionSectionOptions(_questionId);
                DeleteSectionLinkTextBox.Text = string.Empty;
                AddSectionLinkTextBox.Text = string.Empty;
                UpdateSectionLinkTextBox.Text = string.Empty;
                EditSectionLinkTextBox.Text = string.Empty;
                AnswersListBox.Items.Clear();
                GridAnswersListBox.Items.Clear();
            }
            else
            {
                // creates a BE and update the options
                QuestionSectionOptionData sectionOptions = new QuestionSectionOptionData();
                QuestionSectionOptionData.QuestionSectionOptionsRow updatedSectionOption =
                    sectionOptions.QuestionSectionOptions.NewQuestionSectionOptionsRow();

                // Set the updated fields
                updatedSectionOption.QuestionId = _questionId;
                updatedSectionOption.RepeatableSectionModeId = int.Parse(RepeatModeDropdownlist.SelectedValue);
                updatedSectionOption.DeleteSectionLinkText = DeleteSectionLinkTextBox.Text;
                updatedSectionOption.AddSectionLinkText = AddSectionLinkTextBox.Text;
                updatedSectionOption.UpdateSectionLinkText = UpdateSectionLinkTextBox.Text;
                updatedSectionOption.EditSectionLinkText = EditSectionLinkTextBox.Text;
                updatedSectionOption.MaxSections = int.Parse(MaxSectionAllowedDropdownlist.SelectedValue);
                sectionOptions.QuestionSectionOptions.AddQuestionSectionOptionsRow(updatedSectionOption);
                new Question().UpdateQuestionSectionOptions(sectionOptions, LanguagesDropdownlist.SelectedValue);
            }

            MessageLabel.Visible = true;
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SectionOptionsUpdatedMessage"));

            BindQuestionSectionOptions();
        }


        /// <summary>
        /// Set to section ui state in the correct
        /// state based on the current selected section 
        /// repeat mode
        /// </summary>
        private void SetSectionUiState()
        {
            if (int.Parse(RepeatModeDropdownlist.SelectedValue) == (int)RepeatableSectionMode.FullAnswers)
            {
                UpdateRepeatSectionLabel.Visible = false;
                UpdateSectionLinkTextBox.Visible = false;
                DeleteRepeatSectionLabel.Visible = true;
                MaxSectionAllowedLabel.Visible = true;
                MaxSectionAllowedDropdownlist.Visible = true;
                AddRepeatSectionLabel.Visible = true;
                AddSectionLinkTextBox.Visible = true;
                DeleteSectionLinkTextBox.Visible = true;
                GridAnswersLabel.Visible = false;
                GridAnswersListBox.Visible = false;
                AnswersListBox.Visible = false;
                GridAnswersChoiceLabel.Visible = false;
                EditSectionLinkTextBox.Visible = false;
                EditRepeatSectionLabel.Visible = false;

            }
            else if (int.Parse(RepeatModeDropdownlist.SelectedValue) == (int)RepeatableSectionMode.GridAnswers)
            {
                UpdateRepeatSectionLabel.Visible = true;
                DeleteRepeatSectionLabel.Visible = true;
                AddRepeatSectionLabel.Visible = true;
                AddSectionLinkTextBox.Visible = true;
                UpdateSectionLinkTextBox.Visible = true;
                DeleteSectionLinkTextBox.Visible = true;
                GridAnswersLabel.Visible = true;
                GridAnswersListBox.Visible = true;
                AnswersListBox.Visible = true;
                GridAnswersChoiceLabel.Visible = true;
                EditSectionLinkTextBox.Visible = true;
                EditRepeatSectionLabel.Visible = true;
                MaxSectionAllowedLabel.Visible = true;
                MaxSectionAllowedDropdownlist.Visible = true;


                AnswersListBox.DataSource = new Answers().GetAnswersList(_questionId);
                AnswersListBox.DataMember = "Answers";
                AnswersListBox.DataTextField = "AnswerText";
                AnswersListBox.DataValueField = "AnswerId";
                AnswersListBox.DataBind();

                int[] gridAnswers = new Questions().GetQuestionSectionGridAnswers(_questionId);
                for (int i = 0; i < gridAnswers.Length; i++)
                {
                    ListItem answerItem = AnswersListBox.Items.FindByValue(gridAnswers[i].ToString());
                    if (answerItem != null)
                    {
                        GridAnswersListBox.Items.Add(new ListItem(answerItem.Text, answerItem.Value));
                        AnswersListBox.Items.Remove(answerItem);
                    }
                }
            }
            else
            {
                EditRepeatSectionLabel.Visible = false;
                UpdateRepeatSectionLabel.Visible = false;
                DeleteRepeatSectionLabel.Visible = false;
                AddRepeatSectionLabel.Visible = false;
                AddSectionLinkTextBox.Visible = false;
                UpdateSectionLinkTextBox.Visible = false;
                DeleteSectionLinkTextBox.Visible = false;
                EditSectionLinkTextBox.Visible = false;
                GridAnswersLabel.Visible = false;
                GridAnswersListBox.Visible = false;
                AnswersListBox.Visible = false;
                GridAnswersChoiceLabel.Visible = false;
                MaxSectionAllowedLabel.Visible = false;
                MaxSectionAllowedDropdownlist.Visible = false;
            }
        }

        private void AnswersListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            new Question().AddQuestionSectionGridAnswers(_questionId, int.Parse(AnswersListBox.SelectedItem.Value));
            GridAnswersListBox.Items.Add(new ListItem(AnswersListBox.SelectedItem.Text, AnswersListBox.SelectedItem.Value));
            AnswersListBox.Items.Remove(AnswersListBox.SelectedItem);
        }

        private void GridAnswersListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            new Question().DeleteQuestionSectionGridAnswers(_questionId, int.Parse(GridAnswersListBox.SelectedItem.Value));
            AnswersListBox.Items.Add(new ListItem(GridAnswersListBox.SelectedItem.Text, GridAnswersListBox.SelectedItem.Value));
            GridAnswersListBox.Items.Remove(GridAnswersListBox.SelectedItem);
        }

        private void LanguagesDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            BindQuestionOptions();
            BindQuestionSectionOptions();
        }

        // code working ok, but wrong effect: min/ max selection options also used to create mandatory question
        private void EnableDisableMinMaxDropDownList()
        {
            //Disable the min and max level settings by user for single selections such as radio and dropdown
            //as it is not necessary
            ////if ((int.Parse(SelectionModeDropDownList.SelectedValue) == (int)QuestionSelectionMode.RadioButton) ||
            //   (int.Parse(SelectionModeDropDownList.SelectedValue) == (int)QuestionSelectionMode.DropDownList) ||
            //   (int.Parse(SelectionModeDropDownList.SelectedValue) == (int)QuestionSelectionMode.Static) 
            ////    )
            //{
            //    MinSelectionDropDownList.Enabled = false;
            //    MaxAllowedDropDownList.Enabled = false;

            //    MinSelectionDropDownList.SelectedValue = "0";
            //    MaxAllowedDropDownList.SelectedValue = "0";

            //}
            //else
            //{
            //    MinSelectionDropDownList.Enabled = true;
            //    MaxAllowedDropDownList.Enabled = true;
            //}
        }


        protected void SelectionModeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableMinMaxDropDownList();
        } 

    }
}
