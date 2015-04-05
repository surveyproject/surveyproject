using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.WebAdmin.UserControls;

    public partial class MultiLanguagesEdit : System.Web.UI.UserControl
    {
        /// <summary>
        /// Id of the survey to edit
        /// if no id is given put the 
        /// usercontrol in creation mode
        /// </summary>
        public int SurveyId
        {
            get { return (ViewState["SurveyID"] == null) ? -1 : int.Parse(ViewState["SurveyID"].ToString()); }
            set
            {
                ViewState["SurveyID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LocalizePage();
            MessageLabel.Visible = false;

            if (!Page.IsPostBack)
            {
                SurveyList.SurveyId = SurveyId;
                SurveyList.BindDropDown();
                // Header.SurveyId = SurveyId;
                ((Wap)((PageBase)Page).Master).HeaderControl.SurveyId = SurveyId;
                FillFields();
            }
        }


        private void LocalizePage()
        {
            MultiLanguagesTitle.Text = ((PageBase)Page).GetPageResource("MultiLanguagesTitle");
            EnableMultiLanguagesLabel.Text = ((PageBase)Page).GetPageResource("EnableMultiLanguagesLabel");
            MultiLanguagesModeLabel.Text = ((PageBase)Page).GetPageResource("MultiLanguagesModeLabel");
            EnabledLanguagesLabel.Text = ((PageBase)Page).GetPageResource("EnabledLanguagesLabel");
            DefaultLanguageLabel.Text = ((PageBase)Page).GetPageResource("DefaultLanguageLabel");
            VariableNameLabel.Text = ((PageBase)Page).GetPageResource("VariableNameLabel");
            VariableNameUpdateButton.Text = ((PageBase)Page).GetPageResource("VariableNameUpdateButton");
            VariableNameInfoLabel.Text = ((PageBase)Page).GetPageResource("VariableNameInfoLabel");
            if (!Page.IsPostBack)
            {
                MultiLanguagesModeDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("UserSelectionOption"), ((int)MultiLanguageMode.UserSelection).ToString()));
                MultiLanguagesModeDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("BrowserDetectionOption"), ((int)MultiLanguageMode.BrowserDetection).ToString()));
                MultiLanguagesModeDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("QueryStringLanguageOption"), ((int)MultiLanguageMode.QueryString).ToString()));
                MultiLanguagesModeDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("CookieLanguageOption"), ((int)MultiLanguageMode.Cookie).ToString()));
                MultiLanguagesModeDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("SessionLanguageOption"), ((int)MultiLanguageMode.Session).ToString()));
            }
        }


        /// <summary>
        /// Get the current DB stats and fill 
        /// the label with them
        /// </summary>
        private void FillFields()
        {
            MultiLanguageData surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);

            SurveyData surveySettings = new Surveys().GetSurveyById(SurveyId, "");
            if ((MultiLanguageMode)surveySettings.Surveys[0].MultiLanguageModeId != MultiLanguageMode.None)
            {
                MultiLanguagesPlaceHolder.Visible = true;
                MultiLanguagesCheckBox.Checked = true;
                MultiLanguagesModeDropDownList.SelectedValue = surveySettings.Surveys[0].MultiLanguageModeId.ToString();

                if (int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.Cookie ||
                    int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.QueryString ||
                    int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.Session)
                {
                    VariableNameLabel.Visible = true;
                    VariableNameTextBox.Visible = true;
                    VariableNameTextBox.Text = surveySettings.Surveys[0].MultiLanguageVariable;
                    VariableNameUpdateButton.Visible = true;
                    VariableNameInfoLabel.Visible = true;
                }
                else
                {
                    VariableNameLabel.Visible = false;
                    VariableNameTextBox.Text = string.Empty;
                    VariableNameTextBox.Visible = false;
                    VariableNameUpdateButton.Visible = false;
                    VariableNameInfoLabel.Visible = false;
                }
            }
            else
            {
                MultiLanguagesPlaceHolder.Visible = false;
                MultiLanguagesCheckBox.Checked = false;
            }

            DisabledLanguagesListBox.DataSource = new MultiLanguages().GetMultiLanguages();
            DisabledLanguagesListBox.DataMember = "MultiLanguages";
            DisabledLanguagesListBox.DataTextField = "LanguageDescription";
            DisabledLanguagesListBox.DataValueField = "LanguageCode";
            DisabledLanguagesListBox.DataBind();

            EnabledLanguagesListBox.DataSource = surveyLanguages;
            EnabledLanguagesListBox.DataMember = "MultiLanguages";
            EnabledLanguagesListBox.DataTextField = "LanguageDescription";
            EnabledLanguagesListBox.DataValueField = "LanguageCode";
            EnabledLanguagesListBox.DataBind();

            foreach (ListItem enabledItem in EnabledLanguagesListBox.Items)
            {
                ListItem disabledItem = DisabledLanguagesListBox.Items.FindByValue(enabledItem.Value);
                if (disabledItem != null)
                {
                    DisabledLanguagesListBox.Items.Remove(disabledItem);
                }
            }

            DefaultLanguageDropdownlist.Items.Clear();
            foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
            {
                ListItem defaultItem = new ListItem(language.LanguageDescription, language.LanguageCode);
                if (language.DefaultLanguage)
                {
                    defaultItem.Selected = true;
                }

                DefaultLanguageDropdownlist.Items.Add(defaultItem);
            }

            ((PageBase)Page).TranslateListControl(DefaultLanguageDropdownlist);
            ((PageBase)Page).TranslateListControl(DisabledLanguagesListBox);
            ((PageBase)Page).TranslateListControl(EnabledLanguagesListBox);
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
            this.MultiLanguagesCheckBox.CheckedChanged += new System.EventHandler(this.MultiLanguagesCheckBox_CheckedChanged);
            this.MultiLanguagesModeDropDownList.SelectedIndexChanged += new System.EventHandler(this.MultiLanguagesModeDropDownList_SelectedIndexChanged);
            this.VariableNameUpdateButton.Click += new System.EventHandler(this.VariableNameUpdateButton_Click);
            this.DisabledLanguagesListBox.SelectedIndexChanged += new System.EventHandler(this.DisabledLanguagesListBox_SelectedIndexChanged);
            this.EnabledLanguagesListBox.SelectedIndexChanged += new System.EventHandler(this.EnabledLanguagesListBox_SelectedIndexChanged);
            this.DefaultLanguageDropdownlist.SelectedIndexChanged += new System.EventHandler(this.DefaultLanguageDropdownlist_SelectedIndexChanged);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        protected void DisabledLanguagesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            new MultiLanguage().UpdateSurveyLanguage(SurveyId, DisabledLanguagesListBox.SelectedValue, false);
            FillFields();
            MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("LanguageEnabledMessage"));
        }

        protected void EnabledLanguagesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (EnabledLanguagesListBox.SelectedValue != DefaultLanguageDropdownlist.SelectedValue)
            {
                new MultiLanguage().DeleteSurveyLanguage(SurveyId, EnabledLanguagesListBox.SelectedValue);
                FillFields();
                MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("LanguageDisabledMessage"));
            }
            else
            {
                MessageLabel.Visible = true;
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("OneDefaultLanguageRequiredMessage"));
            }
        }

        protected void MultiLanguagesCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (MultiLanguagesCheckBox.Checked)
            {
                new MultiLanguage().UpdateMultiLanguage(SurveyId, MultiLanguageMode.UserSelection, null);
                new MultiLanguage().UpdateSurveyLanguage(SurveyId, "en-US", true);
            }
            else
            {
                new MultiLanguage().DisableMultiLanguage(SurveyId);
            }

            FillFields();
        }

        protected void MultiLanguagesModeDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            VariableNameTextBox.Text = string.Empty;

            if (int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.Cookie ||
                int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.QueryString ||
                int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.Session)
            {
                VariableNameLabel.Visible = true;
                VariableNameTextBox.Visible = true;
                VariableNameUpdateButton.Visible = true;
                VariableNameInfoLabel.Visible = true;
            }
            else
            {
                VariableNameLabel.Visible = false;
                VariableNameTextBox.Visible = false;
                VariableNameUpdateButton.Visible = false;
                VariableNameInfoLabel.Visible = false;
            }

            new MultiLanguage().UpdateMultiLanguage(SurveyId, ((MultiLanguageMode)int.Parse(MultiLanguagesModeDropDownList.SelectedValue)), null);
        }

        protected void DefaultLanguageDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Adds the new language as default
            new MultiLanguage().UpdateSurveyLanguage(SurveyId, DefaultLanguageDropdownlist.SelectedValue, true);

            // Reset all other default items  
            foreach (ListItem item in DefaultLanguageDropdownlist.Items)
            {
                if (!item.Selected)
                {
                    new MultiLanguage().UpdateSurveyLanguage(SurveyId, item.Value, false);
                }
            }
        }

        protected void VariableNameUpdateButton_Click(object sender, System.EventArgs e)
        {
            new MultiLanguage().UpdateMultiLanguage(SurveyId, ((MultiLanguageMode)int.Parse(MultiLanguagesModeDropDownList.SelectedValue)), VariableNameTextBox.Text);
        }
    }
}
