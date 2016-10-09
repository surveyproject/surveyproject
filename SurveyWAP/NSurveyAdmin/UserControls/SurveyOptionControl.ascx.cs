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


namespace Votations.NSurvey.WebAdmin.UserControls
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Resources;
    using Microsoft.VisualBasic;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Web;
    using Votations.NSurvey.WebAdmin.NSurveyAdmin;
    /// <summary>
    /// Survey data CU methods
    /// </summary>
    public partial class SurveyOptionControl : UserControl
    {
        public event EventHandler OptionChanged;
        protected System.Web.UI.WebControls.PlaceHolder EditUi;
        protected System.Web.UI.WebControls.TextBox TitleTextBox;
        protected System.Web.UI.WebControls.CheckBox ActiveCheckBox;
        protected System.Web.UI.WebControls.CheckBox ArchiveCheckBox;
        protected System.Web.UI.WebControls.TextBox CloseDateTextbox;
        protected System.Web.UI.WebControls.Label DateFormatLabel;
        protected System.Web.UI.WebControls.TextBox OpeningDateTextBox;
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.Literal SurveyInformationTitle;
        protected System.Web.UI.WebControls.Label SurveyTitleLabel;
        protected System.Web.UI.WebControls.Label OpeningDateLabel;
        protected System.Web.UI.WebControls.Label CloseDateLabel;
        protected System.Web.UI.WebControls.Label ActiveSurveyLabel;
        protected System.Web.UI.WebControls.Label ArchivedLabel;
        protected System.Web.UI.WebControls.Literal DateForExampleInfo;
        protected System.Web.UI.WebControls.Label ProgressDisplayLabel;
        protected System.Web.UI.WebControls.Label EnableNavigationLabel;
        protected System.Web.UI.WebControls.CheckBox EnableNavigationCheckbox;
        //protected System.Web.UI.WebControls.DropDownList DropDownList1;
        protected System.Web.UI.WebControls.DropDownList ProgressDisplayDropDownList;
        protected System.Web.UI.WebControls.Label EntryNotificationLabel;
        protected System.Web.UI.WebControls.Label EnableResumeLabel;
        protected System.Web.UI.WebControls.Label ToEmailNotificationLabel;
        protected System.Web.UI.WebControls.Label FromEmailNotificationLabel;
        protected System.Web.UI.WebControls.TextBox EmailFromTextbox;
        protected System.Web.UI.WebControls.TextBox EmailToTextBox;
        protected System.Web.UI.WebControls.Label NotificationSubjectLabel;
        protected System.Web.UI.WebControls.TextBox EmailSubjectTextbox;
        protected System.Web.UI.WebControls.PlaceHolder NotificationPlaceHolder;
        protected System.Web.UI.WebControls.Literal NotificationSettingsTitle;
        protected System.Web.UI.WebControls.DropDownList EntryNotificationDropdownlist;
        protected System.Web.UI.WebControls.Literal PrivacySettingsTitle;
        protected System.Web.UI.WebControls.Label ScoredLabel;
        protected System.Web.UI.WebControls.CheckBox ScoredCheckbox;
        protected System.Web.UI.WebControls.DropDownList ResumeModeDropdownlist;
        protected System.Web.UI.WebControls.Button ImportXMLButton;
        protected System.Web.UI.WebControls.Button ExportSurveyButton;
        protected System.Web.UI.WebControls.Button CloneButton;
        protected System.Web.UI.WebControls.Button DeleteButton;
        protected System.Web.UI.WebControls.Button ApplyChangesButton;
        protected System.Web.UI.WebControls.Button CreateSurveyButton;
        protected System.Web.UI.WebControls.PlaceHolder SurveyImportPlaceHolder;
        protected System.Web.UI.WebControls.Label ImportSurveyTitle;
        protected System.Web.UI.WebControls.Label DisableQuestionNumbering;
        protected System.Web.UI.WebControls.CheckBox QuestionNumberingCheckbox;
        protected System.Web.UI.HtmlControls.HtmlInputFile ImportFile;
        protected System.Web.UI.WebControls.Literal HelpGifTitle;
        // protected System.Web.UI.WebControls.Literal SettingsTitleHelp;

        /// <summary>
        /// Id of the survey to edit
        /// if no id is given put the 
        /// usercontrol in creation mode
        /// </summary>
        public int SurveyId
        {
            get { return ((PageBase)Page).getSurveyId(); }
            set { ((PageBase)Page).SurveyId = value; }
        }

        public bool isCreationMode { get; set; }
        public bool isListMode { get; set; }

        private void Page_Load(object sender, System.EventArgs e)
        {

            LocalizePage();

            MessageLabel.Visible = false;
            DeleteButton.Attributes.Add("onClick",
                            "javascript:if(confirm('" + ((PageBase)Page).GetPageResource("DeleteSurveyConfirmationMessage") + "')== false) return false;");


            // Check if any survey has been assigned
            if (isCreationMode || isListMode)
            {
                SwitchToCreationMode();
            }
            else
            {
                SwitchToEditionMode();
            }

            SetupSecurity();
        }

        private void SetupDeleteConfirm()
        {
        }
        private void SetupSecurity()
        {
            if (!isListMode && !isCreationMode)
            {
                ((PageBase)Page).CheckRight(NSurveyRights.AccessSurveySettings, true);
                this.ApplyChangesButton.Enabled = ((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.ApplySurveySettings);
            }
        }
        private void LocalizePage()
        {
            SurveyInformationTitle.Text = ((PageBase)Page).GetPageResource("SurveyInformationTitle");
            SurveyInformationTitle2.Text = ((PageBase)Page).GetPageResource("SurveyInformationTitle2");
            SurveyTitleLabel.Text = ((PageBase)Page).GetPageResource("SurveyTitleLabel");
            OpeningDateLabel.Text = ((PageBase)Page).GetPageResource("OpeningDateLabel");
            DateForExampleInfo.Text = ((PageBase)Page).GetPageResource("DateForExampleInfo");
            CloseDateLabel.Text = ((PageBase)Page).GetPageResource("CloseDateLabel");
            ActiveSurveyLabel.Text = ((PageBase)Page).GetPageResource("ActiveSurveyLabel");
            ArchivedLabel.Text = ((PageBase)Page).GetPageResource("ArchivedLabel");
            CloneButton.Text = ((PageBase)Page).GetPageResource("CloneButton");
            ApplyChangesButton.Text = ((PageBase)Page).GetPageResource("ApplyChangesButton");
            DeleteButton.Text = ((PageBase)Page).GetPageResource("DeleteButton");
            CreateSurveyButton.Text = ((PageBase)Page).GetPageResource("CreateSurveyButton");
            EntryNotificationLabel.Text = ((PageBase)Page).GetPageResource("EntryNotificationLabel");
            ProgressDisplayLabel.Text = ((PageBase)Page).GetPageResource("ProgressDisplayLabel");
            EnableNavigationLabel.Text = ((PageBase)Page).GetPageResource("EnableNavigationLabel");
            EnableResumeLabel.Text = ((PageBase)Page).GetPageResource("EnableResumeLabel");
            NotificationSettingsTitle.Text = ((PageBase)Page).GetPageResource("NotificationSettingsTitle");
            NotificationSubjectLabel.Text = ((PageBase)Page).GetPageResource("NotificationSubjectLabel");
            FromEmailNotificationLabel.Text = ((PageBase)Page).GetPageResource("FromEmailNotificationLabel");
            ToEmailNotificationLabel.Text = ((PageBase)Page).GetPageResource("ToEmailNotificationLabel");
            ScoredLabel.Text = ((PageBase)Page).GetPageResource("ScoredLabel");
            ExportSurveyButton.Text = ((PageBase)Page).GetPageResource("ExportSurveyButton");
            ImportSurveyTitle.Text = ((PageBase)Page).GetPageResource("ImportSurveyTitle");
            ImportXMLButton.Text = ((PageBase)Page).GetPageResource("ImportXMLButton");
            ImportSurveyLiteral.Text = ((PageBase)Page).GetPageResource("ImportSurveyLiteral");
            DisableQuestionNumbering.Text = ((PageBase)Page).GetPageResource("DisableQuestionNumbering");
            DefaultSurveyLabel.Text = ((PageBase)Page).GetPageResource("DefaultSurvey");
            //New Helpfiles xml texts:
            NotificationsSettingsHelp.Text = ((PageBase)Page).GetPageHelpfiles("NotificationsSettingsHelp");
            //HelpGifTitle.Text = ((PageBase)Page).GetPageResource("HelpGifTitle");

            TitleTextBox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsTitleHelp");
            OpeningDateTextBox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsOpeningDateHelp");
            CloseDateTextbox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsCloseDateHelp");
            ProgressDisplayDropDownList.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsProgressHelp");
            QuestionNumberingCheckbox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsQnumberingHelp");

            ActiveCheckBox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsActiveHelp");
            DefaultSurveyCheckBox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsDefaultHelp");
            ArchiveCheckBox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsArchiveHelp");
            ScoredCheckbox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsScoredHelp");
            EnableNavigationCheckbox.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsNavigationHelp");
            ResumeModeDropdownlist.ToolTip = ((PageBase)Page).GetPageHelpfiles("SettingsResumeHelp");

            // QuestionNumberingCheckbox.Attributes.Add("title", ((PageBase)Page).GetPageHelpfiles("SettingsQnumberingHelp")) ;
        }

        /// <summary>
        /// Setup the control in creation mode
        /// </summary>
        private void SwitchToCreationMode()
        {
            // Creation mode
            SurveyImportPlaceHolder.Visible =
                ((PageBase)Page).CheckRight(NSurveyRights.AllowXmlImport, false);
            CreateSurveyButton.Visible = true;
            ApplyChangesButton.Visible = false;
            DeleteButton.Visible = false;
            CloneButton.Visible = false;
            ExportSurveyButton.Visible = false;
            EditUi.Visible = false;

        }

        /// <summary>
        /// Setup the control in edition mode
        /// </summary>
        private void SwitchToEditionMode()
        {
            ExportSurveyButton.Visible = ((PageBase)Page).CheckRight(Votations.NSurvey.Data.NSurveyRights.ExportSurveyXml, false);
            CreateSurveyButton.Visible = false;
            ApplyChangesButton.Visible = ((PageBase)Page).CheckRight(Votations.NSurvey.Data.NSurveyRights.ApplySurveySettings, false);
            DeleteButton.Visible = ((PageBase)Page).CheckRight(Votations.NSurvey.Data.NSurveyRights.DeleteSurvey, false);
            CloneButton.Visible = ((PageBase)Page).CheckRight(Votations.NSurvey.Data.NSurveyRights.CloneSurvey, false);
            SurveyImportPlaceHolder.Visible = false;

            EditUi.Visible = true;

            if (!IsPostBack)
            {
                BindFields();
            }
        }

        /// <summary>
        /// Get the current DB data and fill 
        /// the fields with them
        /// </summary>
        private void BindFields()
        {


            DateFormatLabel.Text = (DateTime.UtcNow).ToShortDateString();

            // Retrieve the survey data
            SurveyData surveyData = new Surveys().GetSurveyById(SurveyId, "");
            SurveyData.SurveysRow survey = surveyData.Surveys[0];

            // Assigns the retrieved data to the correct fields
            TitleTextBox.Text = survey.Title;
            ActiveCheckBox.Checked = survey.Activated;
            ArchiveCheckBox.Checked = survey.Archive;
            ScoredCheckbox.Checked = survey.Scored;
            QuestionNumberingCheckbox.Checked = survey.QuestionNumberingDisabled;
            OpeningDateTextBox.Text = survey.IsOpenDateNull() ? null : survey.OpenDate.ToShortDateString();
            CloseDateTextbox.Text = survey.IsCloseDateNull() ? null : survey.CloseDate.ToShortDateString();
            EnableNavigationCheckbox.Checked = survey.NavigationEnabled;
            EmailFromTextbox.Text = survey.EmailFrom;
            EmailToTextBox.Text = survey.EmailTo;
            EmailSubjectTextbox.Text = survey.EmailSubject;
            ViewState["ThanksMessage"] = survey.ThankYouMessage;
            DefaultSurveyCheckBox.Checked = survey.DefaultSurvey;

            EntryNotificationDropdownlist.DataSource = new Surveys().GetSurveyNotificationModes();
            EntryNotificationDropdownlist.DataTextField = "description";
            EntryNotificationDropdownlist.DataValueField = "NotificationModeId";

            EntryNotificationDropdownlist.DataBind();
            ((PageBase)Page).TranslateListControl(EntryNotificationDropdownlist);
            EntryNotificationDropdownlist.SelectedValue = survey.NotificationModeID.ToString();

            if ((NotificationMode)survey.NotificationModeID !=
                NotificationMode.None)
            {
                NotificationPlaceHolder.Visible = true;
            }
            else
            {
                NotificationPlaceHolder.Visible = false;
            }

            ProgressDisplayDropDownList.DataSource = new Surveys().GetSurveyProgressModes();
            ProgressDisplayDropDownList.DataTextField = "description";
            ProgressDisplayDropDownList.DataValueField = "ProgressDisplayModeId";
            ProgressDisplayDropDownList.DataBind();
            ((PageBase)Page).TranslateListControl(ProgressDisplayDropDownList);
            ProgressDisplayDropDownList.SelectedValue = survey.ProgressDisplayModeID.ToString();

            ResumeModeDropdownlist.DataSource = new Surveys().GetSurveyResumeModes();
            ResumeModeDropdownlist.DataTextField = "description";
            ResumeModeDropdownlist.DataValueField = "ResumeModeId";
            ResumeModeDropdownlist.DataBind();
            ((PageBase)Page).TranslateListControl(ResumeModeDropdownlist);
            ResumeModeDropdownlist.SelectedValue = survey.ResumeModeID.ToString();
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
            this.EntryNotificationDropdownlist.SelectedIndexChanged += new System.EventHandler(this.EntryNotificationDropdownlist_SelectedIndexChanged);
            this.CreateSurveyButton.Click += new System.EventHandler(this.CreateSurveyButton_Click);
            this.ApplyChangesButton.Click += new System.EventHandler(this.ApplyChangesButton_Click);
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            this.CloneButton.Click += new System.EventHandler(this.CloneButton_Click);
            this.ExportSurveyButton.Click += new System.EventHandler(this.ExportSurveyButton_Click);
            this.ImportXMLButton.Click += new System.EventHandler(this.ImportXMLButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void EntryNotificationDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if ((NotificationMode)int.Parse(EntryNotificationDropdownlist.SelectedValue) !=
                NotificationMode.None)
            {
                NotificationPlaceHolder.Visible = true;
            }
            else
            {
                NotificationPlaceHolder.Visible = false;
            }
        }


        /// <summary>
        /// Apply the survey data changes to the DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyChangesButton_Click(object sender, System.EventArgs e)
        {
            if (OpeningDateTextBox.Text.Length != 0 &&
                !Information.IsDate(OpeningDateTextBox.Text))
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidOpeningDate"));
                MessageLabel.Visible = true;
                return;
            }

            if (CloseDateTextbox.Text.Length != 0 &&
                !Information.IsDate(CloseDateTextbox.Text))
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidClosingDate"));
                MessageLabel.Visible = true;
                return;
            }

            if (((NotificationMode)int.Parse(EntryNotificationDropdownlist.SelectedValue)) !=
                NotificationMode.None)
            {
                if (EmailFromTextbox.Text.Length == 0 || !((PageBase)Page).isEmail(EmailFromTextbox.Text))
                {
                    ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidFromEmail"));
                    MessageLabel.Visible = true;
                    return;
                }

                if (EmailToTextBox.Text.Length == 0 || !((PageBase)Page).isEmail(EmailToTextBox.Text))
                {
                    ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidToEmail"));
                    MessageLabel.Visible = true;
                    return;
                }
                NotificationPlaceHolder.Visible = true;
            }
            else
            {
                EmailFromTextbox.Text = string.Empty;
                EmailToTextBox.Text = string.Empty;
                EmailSubjectTextbox.Text = string.Empty;
                NotificationPlaceHolder.Visible = false;
            }

            try
            {
                // Creates a new entity and assign
                // the updated values
                SurveyData surveyData = new SurveyData();
                SurveyData.SurveysRow surveyRow = surveyData.Surveys.NewSurveysRow();
                surveyRow.SurveyId = SurveyId;
                surveyRow.Title = TitleTextBox.Text;
                surveyRow.Activated = ActiveCheckBox.Checked;
                surveyRow.Archive = ArchiveCheckBox.Checked;
                surveyRow.Scored = ScoredCheckbox.Checked;
                surveyRow.QuestionNumberingDisabled = QuestionNumberingCheckbox.Checked;
                surveyRow.NavigationEnabled = EnableNavigationCheckbox.Checked;
                surveyRow.ResumeModeID = int.Parse(ResumeModeDropdownlist.SelectedValue);
                surveyRow.ProgressDisplayModeID = int.Parse(ProgressDisplayDropDownList.SelectedValue);
                surveyRow.NotificationModeID = int.Parse(EntryNotificationDropdownlist.SelectedValue);
                surveyRow.DefaultSurvey = DefaultSurveyCheckBox.Checked;
                surveyRow.ThankYouMessage = ViewState["ThanksMessage"] == null ? null : ViewState["ThanksMessage"].ToString();

                if (((NotificationMode)surveyRow.NotificationModeID) !=
                    NotificationMode.None)
                {
                    surveyRow.EmailFrom = EmailFromTextbox.Text;
                    surveyRow.EmailTo = EmailToTextBox.Text;
                    surveyRow.EmailSubject = EmailSubjectTextbox.Text;
                }

                if (OpeningDateTextBox.Text.Length != 0)
                {
                    surveyRow.OpenDate = DateTime.Parse(OpeningDateTextBox.Text);
                }
                if (CloseDateTextbox.Text.Length != 0)
                {
                    surveyRow.CloseDate = DateTime.Parse(CloseDateTextbox.Text);
                }
                surveyData.Surveys.AddSurveysRow(surveyRow);

                // Update the DB
                // GB 2016 fix: dna (does not apply) added + SP vts_spSurveyUpdate modified
                new Survey().UpdateSurvey(surveyData, "dna");

                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyUpdatedMessage"));
                MessageLabel.Visible = true;

                ((Wap)this.Page.Master.Master).isTreeStale = true;
                ((Wap)this.Page.Master.Master).RebuildTree();

                // Let the subscribers know that something changed
                OnOptionChanged();
            }

            catch (SurveyExistsFoundException ex)
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyExistsInFolder"));
                MessageLabel.Visible = true;
            }
        }

        private void CreateSurveyButton_Click(object sender, System.EventArgs e)
        {
            // Creates a new entity and assign
            // the new values
            SurveyData surveyData = new SurveyData();
            SurveyData.SurveysRow surveyRow = surveyData.Surveys.NewSurveysRow();

            if (TitleTextBox.Text.Length == 0)
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingSurveyTitleMessage"));
                MessageLabel.Visible = true;
                return;
            }

            surveyRow.Title = TitleTextBox.Text;

            surveyRow.Activated = false;
            surveyRow.Archive = false;
            surveyRow.AccessPassword = string.Empty;
            surveyRow.SurveyDisplayTimes = 0;
            surveyRow.ResultsDisplayTimes = 0;
            surveyRow.SetOpenDateNull();
            surveyRow.SetCloseDateNull();
            surveyRow.CreationDate = DateTime.UtcNow;
            surveyRow.IPExpires = 1440;
            surveyRow.CookieExpires = 1440;
            surveyRow.OnlyInvited = false;
            surveyRow.Scored = false;
            surveyRow.QuestionNumberingDisabled = false;
            surveyRow.FolderId = ((PageBase)Page).SelectedFolderId;
            surveyRow.ProgressDisplayModeID = (int)ProgressDisplayMode.PagerNumbers;
            surveyRow.ResumeModeID = (int)ResumeMode.NotAllowed;
            surveyData.Surveys.AddSurveysRow(surveyRow);

            try
            {
                // Add the survey in the DB
                new Survey().AddSurvey(surveyData);
                AssignSurveyToUser(surveyData.Surveys[0].SurveyId);
                ((PageBase)Page).SurveyId = surveyData.Surveys[0].SurveyId;
                ((Wap)Page.Master).isTreeStale = true;
                //This messes up the tree astructure etc so Stay where you are
                UINavigator.NavigateToSurveyBuilder(surveyRow.SurveyId, 4);
            }
            catch (SurveyExistsFoundException ex)
            {
                string x = ex.Message;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyExistsInFolder"));
                MessageLabel.Visible = true;
            }
        }

        /// <summary>
        /// If a valid user is logged in assign the survey 
        /// to his account
        /// </summary>
        private void AssignSurveyToUser(int surveyId)
        {
            // Can we assign the survey to the current user
            if (!((PageBase)Page).IsSingleUserMode(false))
            {
                new Survey().AssignUserToSurvey(surveyId,
                    ((PageBase)Page).NSurveyUser.Identity.UserId);
            }
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            // Delete survey from the DB
            new Survey().DeleteSurveyById(SurveyId);
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyDeletedMessage"));

            MessageLabel.Visible = true;
            ((PageBase)Page).SurveyDeleteActions(SurveyId);
            UINavigator.NavigateToSurveyOptions(-1, ((PageBase)Page).MenuIndex);
        }

        protected void OnOptionChanged()
        {
            if (OptionChanged != null)
            {
                OptionChanged(this, EventArgs.Empty);
            }
        }

        private void CloneButton_Click(object sender, System.EventArgs e)
        {
            SurveyData clonedSurvey = new Survey().CloneSurvey(SurveyId);

            //Retrieve the new survey ID
            SurveyId = clonedSurvey.Surveys[0].SurveyId;

            // Assign the cloned survey to the user
            AssignSurveyToUser(SurveyId);

            // Update the form fields
            BindFields();
            ((Wap)Page.Master.Master).isTreeStale = true;
            ((Wap)Page.Master.Master).RebuildTree();
            // Let the subscribers know that something has changed
            OnOptionChanged();

        }

        private void ExportSurveyButton_Click(object sender, System.EventArgs e)
        {
            NSurveyForm surveyForm = null;


            Response.Charset = "utf-8";
            Response.ContentType = "application/octet-stream";

            Response.ContentType = "text/xml";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"SurveyExport" + SurveyId + ".xml\"");

            surveyForm = new Surveys().GetFormForExport(SurveyId);
            surveyForm.WriteXml(Response.OutputStream, System.Data.XmlWriteMode.IgnoreSchema);

            Response.End();
        }

        private void ImportXMLButton_Click(object sender, System.EventArgs e)
        {
            if (ImportFile.PostedFile != null)
            {
                NSurveyForm importedSurveys = new NSurveyForm();
                try
                {

                    importedSurveys.ReadXml(ImportFile.PostedFile.InputStream);
                    if (importedSurveys.Survey.Rows.Count > 0)
                    {
                        // Prevents SQL injection from custom hand written datasources Sql answer types in the import Xml 
                        if (!GlobalConfig.SqlBasedAnswerTypesAllowed || !(((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).CheckRight(NSurveyRights.SqlAnswerTypesEdition, false)))
                        {
                            foreach (NSurveyForm.AnswerTypeRow answerType in importedSurveys.AnswerType)
                            {
                                answerType.DataSource = null;
                            }
                        }

                        new Survey().ImportSurveys(importedSurveys, ((PageBase)Page).NSurveyUser.Identity.UserId, ((PageBase)Page).SelectedFolderId ?? -1);
                        Surveys srv = new Surveys();
                        srv.SetFolderId(((PageBase)Page).SelectedFolderId, importedSurveys.Survey[0].SurveyID);

                        AssignSurveyToUser(importedSurveys.Survey[0].SurveyID);
                        SurveyId = importedSurveys.Survey[0].SurveyID;
                        UINavigator.NavigateToSurveyBuilder(importedSurveys.Survey[0].SurveyID, 4);
                        ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyImported"));
                    }

                    else ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyNotImported"));


                    MessageLabel.Visible = true;
                }
                catch (Exception ex)
                {
                    if (ex.Message == "DUPLICATEFOLDER")
                        ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyImportDuplicate") );
                    else
                        ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("Exception") + "  " + ex.Message);
                    MessageLabel.Visible = true;
                }
            }
        }

        protected void ApplyChangesButton_Click1(object sender, EventArgs e)
        {

        }
    }
}
