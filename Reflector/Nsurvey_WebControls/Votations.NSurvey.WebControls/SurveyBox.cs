using System.Globalization;

namespace Votations.NSurvey.WebControls
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    // temp
    using System.Timers;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Enums;
    using Votations.NSurvey.Reporting;
    using Votations.NSurvey.Resources;
    using Votations.NSurvey.Security;
    using Votations.NSurvey.WebControls.UI;
    using Votations.NSurvey.WebControlsFactories;

    /// <summary>
    /// Renders the box with the survey's questions / answer.
    /// </summary>
    [ToolboxData("<{0}:SurveyBox runat=server></{0}:SurveyBox>"), ToolboxBitmap(typeof(Bitmap), "SurveyBox.bmp"), Designer(typeof(SurveyBoxDesigner))]
    public class SurveyBox : WebControl, INamingContainer
    {
        private Style _answerStyle;
        private Style _buttonStyle;
        private Style _confirmationMessageStyle;
        private Style _footStyle;
        private Style _matrixAlternatingItemStyle;
        private Style _matrixHeaderStyle;
        private Style _matrixItemStyle;
        private Style _matrixStyle;
        private Style _questionStyle;
        private Style _questionValidationMarkStyle;
        private Style _questionValidationMessageStyle;
        private Style _sectionGridAnswersAlternatingItemStyle;
        private Style _sectionGridAnswersHeaderStyle;
        private Style _sectionGridAnswersItemStyle;
        private Style _sectionGridAnswersStyle;
        private Style _sectionOptionStyle;

        private string _buttonText = null;
        private int _cellPadding;
        private int _cellSpacing;
        private HttpContext _context = HttpContext.Current;
        private bool _didPostBack = false;
        private string _emailFrom;
        private string _emailSubject;
        private string _emailTo;
        private bool _enableNavigation = true;
        private bool _enableSubmitButton = false;
        private bool _enableValidation = false;
        private bool _isDesignTime = true;
        private bool _isScored = false;
        private DropDownList _languageDropDownList;
        private string _languageVariable;
        private MultiLanguageMode _multiLanguageMode = MultiLanguageMode.None;
        private string _nextPageText = null;
        private NotificationMode _notificationMode = NotificationMode.None;
        private string _previousPageText = null;
        private bool _previousQuestionNumbering = false;
        private ProgressDisplayMode _progressMode = ProgressDisplayMode.PagerNumbers;
        private StringBuilder _questionClientValidationCalls = new StringBuilder();

        private Table _questionContainer = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ
        private Panel _questionPanel = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetMainPercentagePanel();//GB

        private int _questionNumbers = 1;
        private QuestionItemCollection _questions;
        private string _questionValidationMark = "*";
        private bool _randomQuestions = false;
        private string _redirectionURL;
        private ArrayList _requiredQuestions = new ArrayList();
        private ResumeMode _resumeMode = ResumeMode.NotAllowed;
        private string _resumeProgressText = null;
        private Table _resumeTable;
        private TextBox _resumeUIdTextBox;
        private string _saveProgressText = null;
        private WebSecurityAddInCollection _securityAddIns = new WebSecurityAddInCollection();
        private bool _selectionsAreRequired = false;
        private bool _showOnlyPercent = false;
        private bool _showQuestionNumbers = true;
        private long _startTime = DateTime.Now.Ticks;
        private Button _submitButton = new Button();
        private int _surveyId = -1;
        private SurveyData.SurveysRow _surveyRow;
        private string _surveyTitle;
        private string _thankYouMessage;
        private UnAuthentifiedUserAction _unAuthentifiedUserAction = UnAuthentifiedUserAction.GeneralWarning;
        private VoterAnswersData currentVisitorAnswerSet = new VoterAnswersData();

        /// <summary>
        /// Allows gathering user's DB id and his answers 
        /// values after they has been saved in the DB
        /// </summary>
        public event FormEventHandler FormSubmitted;

        /// <summary>
        /// Allows gathering / changing of user's answers 
        /// values before they get saved in the DB
        /// </summary>
        public event FormEventHandler FormSubmitting;

        /// <summary>
        /// Allows gathering / changing of user's answers
        /// when he did the page switch
        /// </summary>
        public event FormNavigationEventHandler PageChanged;

        /// <summary>
        /// Allows gathering / changing of user's answers
        /// when he's going to do a page switch
        /// </summary>
        public event FormNavigationEventHandler PageChanging;

        /// <summary>
        /// Allows gathering / changing of surveybox's question
        /// properties after they get bound to their control
        /// and answer items controls have benn generated
        /// </summary>
        public event FormQuestionEventHandler QuestionBound;

        /// <summary>
        /// Allows gathering / changing of surveybox's question
        /// properties before they get bound to their control
        /// and answer items controls generated
        /// </summary>
        public event FormQuestionEventHandler QuestionCreated;

        /// <summary>
        /// Allows gathering / changing of user's answers
        /// when the session has been resumed
        /// </summary>
        public event FormSessionEventHandler SessionResumed;

        /// <summary>
        /// Allows gathering / changing of user's answers
        /// when the session is resuming
        /// </summary>
        public event FormSessionEventHandler SessionResuming;

        /// <summary>
        /// Allows gathering / changing of user's answers
        /// when the session has been saved
        /// </summary>
        public event FormSessionEventHandler SessionSaved;

        /// <summary>
        /// Allows gathering / changing of user's answers
        /// when the session is saving
        /// </summary>
        public event FormSessionEventHandler SessionSaving;

        /// <summary>
        /// Allows gathering / changing of user's data 
        /// values after they have been created
        /// </summary>
        public event FormEventHandler VoterDataCreated;

        /// <summary>
        /// Web control constructor
        /// </summary>
        public SurveyBox(): base(HtmlTextWriterTag.Div)
        {
            if (this.Context != null)
            {
                this._isDesignTime = false;
            }
        }

        protected virtual void BuildCloseDate()
        {
            BuildMessageBox(string.Format(ResourceManager.GetString("ClosedMessage", this.LanguageCode), this._surveyRow.CloseDate.ToShortDateString()), CssXmlManager.GetString("ClosedDateSurveyMessage"));
        }

        protected virtual void BuildClosedSurvey()
        {
            BuildMessageBox(ResourceManager.GetString("ClosedSurveyMessage", this.LanguageCode), CssXmlManager.GetString("ClosedSurveyMessage"));
        }

        protected virtual void BuildInactiveSurvey()
        {
            BuildMessageBox(ResourceManager.GetString("InactiveSurveyMessage", this.LanguageCode), CssXmlManager.GetString("InactiveSurveyMessage"));
        }

        protected virtual void BuildSecurityGeneralWarning()
        {
            BuildMessageBox(ResourceManager.GetString("SecurityGeneralWarning", this.LanguageCode), CssXmlManager.GetString("InactiveSurveyMessage"));
        }

        protected virtual void BuildOpenDate ()
        {
            BuildMessageBox(string.Format(ResourceManager.GetString("OpenMessage", this.LanguageCode), this._surveyRow.OpenDate.ToShortDateString()), CssXmlManager.GetString("NotYetOpenedSurveyMessage"));
        }

        /// <summary>
        /// Generic messagebox - SP v. 2.4
        /// </summary>
        /// <param name="cssClass">CSS Styling</param>
        /// <param name="message">Message text</param>
        /// <remarks>Used on buildopendate(), buildinactivesurvey(), buildclosedsurvey() etc.</remarks>

        private void BuildMessageBox(string message, string cssClass)
        {
            Panel wrapper = new Panel { CssClass = CssXmlManager.GetString("MessageBoxContainerWrapper"), ID="mbCW" };
            Panel subpanel = new Panel { CssClass = CssXmlManager.GetString("MessageBoxContainer"), ID="mbC" };

            Panel messagePanel = new Panel { CssClass = cssClass, ID="mbMP" };
            messagePanel.Controls.Add(new LiteralControl(message));

            wrapper.Controls.Add(subpanel);
            subpanel.Controls.Add(messagePanel);
            
            this.Controls.Clear();

            AddHeaderFooter(this.Controls, wrapper);
        }

        /// <summary>
        /// Pagenumber & percentage progress shown on page footer of survey
        /// </summary>
        /// <param name="submitControl">SubmitControl</param>
        /// <param name="enabled">enabled</param>
        /// <returns>submitControl</returns>
        protected virtual Panel BuildFooterRow(Panel submitControl, bool enabled)
        {

            Panel panel = new Panel { CssClass = CssXmlManager.GetString("PageNumberPanel"), ID="pNP" };
            Label label = new Label { CssClass = CssXmlManager.GetString("PageNumberLabel"), ID="pN" };

            var t = HighestPageNumber;
            if (t < CurrentPageIndex) t = CurrentPageIndex;

            if (this._progressMode == ProgressDisplayMode.PagerNumbers)
            {
                label.Text = string.Format(ResourceManager.GetString("PageNumber", this.LanguageCode), this.CurrentPageIndex, this.TotalPageNumber);
                for (int i = 1; i <= this.TotalPageNumber; i++)
                {
                    if (enabled)
                    {
                        if (i == this.CurrentPageIndex)
                        {
                            panel.Controls.Add(new Panel { CssClass = CssXmlManager.GetString("PageNumberCurrent") });
                            continue;
                        }
                        if (i <= t)
                        {
                            panel.Controls.Add(new Panel { CssClass = CssXmlManager.GetString("PageNumberVisited") });
                        }
                        if (i > t) panel.Controls.Add(new Panel { CssClass = CssXmlManager.GetString("PageNumberNonactive") });
                    }
                    else
                    {
                        panel.Controls.Add(new Panel { CssClass = CssXmlManager.GetString("PageNumberVisited") });
                    }
                }
            }
            else if (this._progressMode == ProgressDisplayMode.ProgressPercentage)
            {
                double num = (((double)(this.CurrentPageIndex)) / ((double)this.TotalPageNumber)) * 100.0;
                label.Text = (num == 0.0) ? (0 + "%") : (num.ToString("##.##") + "%");
            }

            panel.Controls.Add(label);
            submitControl.Controls.Add(panel);
            return submitControl;
        }

        /// <summary>
        /// Builds the userlanguage request form
        /// </summary>
        protected virtual void BuildLanguageRequest()
        {
            TableCell cell = new TableCell();
            TableRow row = new TableRow();
            Button child = new Button {CssClass = CssXmlManager.GetString("LanguageRequestButton"), ID="lrB"};
            Table table = new Table();
            this._languageDropDownList = new DropDownList {CssClass = CssXmlManager.GetString("LanguageRequestDDL"), ID="lrDDL"};
            table.ControlStyle.CssClass = CssXmlManager.GetString("LanguageRequestTable");
            table.ID = "lrT";
            
            Panel wrapper = new Panel { CssClass = CssXmlManager.GetString("MultiLanguageContainerWrapper"), ID="lrqCW" };
            Panel subpanel = new Panel { CssClass = CssXmlManager.GetString("MultiLanguageContainer"), ID="lrqC" };
            wrapper.Controls.Add(subpanel);
            subpanel.Controls.Add(table);

            Panel panel = new Panel { CssClass = CssXmlManager.GetString("LanguageRequestMessage"), ID="lrlM" };
            //cell.Controls.Add(new LiteralControl(ResourceManager.GetString("SelectLanguageMessage")));
            panel.Controls.Add(new LiteralControl(ResourceManager.GetString("SelectLanguageMessage")));
            cell.Controls.Add(panel);

            new MultiLanguages().GetSurveyLanguages(this.SurveyId);
            foreach (MultiLanguageData.MultiLanguagesRow row2 in new MultiLanguages().GetSurveyLanguages(this.SurveyId).MultiLanguages)
            {
                ListItem item = new ListItem(LanguageCodesManager.GetString(row2.LanguageDescription), row2.LanguageCode);
                /* TODO JJ Remove setting to ""
                if (row2.DefaultLanguage)
                {
                    item.Value = "";
                }
                 * */
                this._languageDropDownList.Items.Add(item);
            }
            LanguageCodesManager.TranslateListControl(this._languageDropDownList);

            panel = new Panel { CssClass = CssXmlManager.GetString("LanguageDropDownListPanel"), ID="lrlDDLP" };
            panel.Controls.Add(this._languageDropDownList);
            cell.Controls.Add(panel);

            panel = new Panel { CssClass = CssXmlManager.GetString("LanguageButtonPanel"), ID="lrlBP" };
            panel.Controls.Add(child);

            child.Text = ResourceManager.GetString("SubmitLanguageButton");
            child.Click += new EventHandler(this.OnLanguageSubmit);
            cell.Controls.Add(panel);
            row.Cells.Add(cell);

            table.Rows.Add(row);

            this.Controls.Clear();
            
            AddHeaderFooter(this.Controls, wrapper);
        }

        private void AddHeaderFooter(ControlCollection parentControl, Control control) {
            Votations.NSurvey.SQLServerDAL.SurveyLayout u = new Votations.NSurvey.SQLServerDAL.SurveyLayout();
            var _userSettings = u.SurveyLayoutGet(this.SurveyId, this.LanguageCode ?? GetLanguageCode());
            if (!(_userSettings == null || _userSettings.SurveyLayout.Count == 0)) {
                parentControl.Add(BuildCustomHeaderOrFooter(HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyHeaderText)));
                parentControl.Add(control);
                parentControl.Add(BuildCustomHeaderOrFooter(HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyFooterText)));
            }
            else {
                parentControl.Add(control);
            }
        }

        private string GetLanguageCode() {
            var languages = new MultiLanguages().GetSurveyLanguages(this.SurveyId).MultiLanguages;
            CultureInfo browserLanguage = null;
            if (HttpContext.Current.Request.UserLanguages != null) {
                browserLanguage = new CultureInfo(HttpContext.Current.Request.UserLanguages[0]);
            }
            string mainLanguage = "";

            string browserLanguageName = "";
            if (browserLanguage != null) {
                browserLanguageName = browserLanguage.Name;
            }

            foreach (MultiLanguageData.MultiLanguagesRow lng in languages) {
                if (lng.LanguageCode == browserLanguageName) {
                    return browserLanguageName;
                }
                if (lng.DefaultLanguage) {
                    mainLanguage = lng.LanguageCode;
                }
            }

            return mainLanguage;
        }

        protected virtual Panel BuildNextPageButtonRow(bool enabled)
        {

            Panel submitControl = new Panel { CssClass = CssXmlManager.GetString("NextPageButtonRowPanel"), ID="npBRP" };
            Panel panel;

            Panel subPanel = new Panel { CssClass = CssXmlManager.GetString("NextPageNavigationPanel"), ID = "npNP" };

            //submitbutton
            this._submitButton.ControlStyle.CopyFrom(this.ButtonStyle);
            this._submitButton.CssClass = CssXmlManager.GetString("NextPageSubmitButton");
            this._submitButton.ID = "npSB";
            this._submitButton.Text = (this.NextPageText == null) ? ResourceManager.GetString("NextPage", this.LanguageCode) : this.NextPageText;
            this._submitButton.Click += new EventHandler(this.OnPageChangeClick);
            if (this.EnableValidation)
            {
                this._submitButton.Attributes.Add("OnClick", string.Format("return {0}{1}();", GlobalConfig.SurveyValidationFunction, this.ID));
            }
            _submitButton.Enabled = enabled;
            panel = new Panel { CssClass = CssXmlManager.GetString("NextPageSubmitButtonPanel"), ID="npSBP" };
            panel.Controls.Add(this._submitButton);
            subPanel.Controls.Add(panel);

            submitControl.Controls.Add(subPanel);

            //previous page button
            if (this._enableNavigation)
            {
                panel = new Panel { CssClass = CssXmlManager.GetString("PreviousPageButtonPanel"), ID = "ppBP" };
                panel.Controls.Add(this.BuildPreviousPageButton(CurrentPageIndex > 1 && enabled));
                subPanel.Controls.Add(panel);
            }

            subPanel = this.BuildFooterRow(subPanel, enabled);


            //resume button
            if (this._resumeMode != ResumeMode.NotAllowed && enabled)
            {
                if ((this._resumeMode == ResumeMode.Manual) && (this.CurrentPageIndex == 1))
                {
                    panel = new Panel { CssClass = CssXmlManager.GetString("NextPageResumeProgressButtonPanel"), ID="npRPBP" };
                    panel.Controls.Add(this.BuildResumeProgressButton());
                    submitControl.Controls.Add(panel);
                }
                panel = new Panel { CssClass = CssXmlManager.GetString("NextPageSaveProgressButtonPanel"), ID = "npSPBP" };
                panel.Controls.Add(this.BuildSaveProgressButton());
                submitControl.Controls.Add(panel);
            }

            return submitControl;
        }

        protected virtual Button BuildPreviousPageButton(bool enabled)
        {
            // && (this.CurrentPageIndex > 1)
            Button button = new Button();
            button.ID = "ppbB";
            button.ControlStyle.CopyFrom(this.ButtonStyle);
            button.CssClass = CssXmlManager.GetString("PreviousPageButton");
            button.Text = (this.PreviousPageText == null) ? ResourceManager.GetString("PreviousPage", this.LanguageCode) : this.PreviousPageText;
            button.Click += new EventHandler(this.OnPreviousPageChangeClick);
            button.Enabled = enabled;
            button.Visible = enabled;
            return button;
        }

        protected virtual Button BuildResumeProgressButton()
        {
            Button button = new Button();
            button.ID = "RPB";
            button.ControlStyle.CopyFrom(this.ButtonStyle);
            button.CssClass = CssXmlManager.GetString("ResumeProgressButton");
            button.Text = (this.ResumeProgressText == null) ? ResourceManager.GetString("ResumeProgress", this.LanguageCode) : this.ResumeProgressText;
            button.Click += new EventHandler(this.OnResumeProgressClick);
            return button;
        }

        /// <summary>
        /// method to add the jquery and css files to the head section of the webpage on page initialisation (oninit())
        /// </summary>
        protected virtual void AddHeaderScriptCss()
        {
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            Page.Header.Controls.Add(new LiteralControl("<!--[if lt IE 9]><script src='Scripts/html5shiv.min.js'></script><script src='Scripts/respond.min.js'></script><![endif]-->"));

            //Jquery CSS files
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            //Jquery UI 1.11.4 css:
            HtmlGenericControl css = new HtmlGenericControl("link");
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");
            css.Attributes.Add("href", ResolveUrl(GlobalConfig.ContentFilesPath + "themes/base/base.css"));
            Page.Header.Controls.Add(css);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            //Bootstrap css:
            css = new HtmlGenericControl("link");
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");
            css.Attributes.Add("href", ResolveUrl(GlobalConfig.ContentFilesPath + "bootstrap.min.css"));
            Page.Header.Controls.Add(css);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            //SurveyForm css:
            //css = new HtmlGenericControl("link");
            //css.Attributes.Add("rel", "stylesheet");
            //css.Attributes.Add("type", "text/css");
            //css.Attributes.Add("ID", "defaultCSS");
            //css.Attributes.Add("runat", "server");
            //css.Attributes.Add("href", ResolveUrl(GlobalConfig.CSSFilesPath + "/surveymobile.css"));
            //Page.Header.Controls.Add(css);

            //Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            //jQuery(necessary for Bootstrap's JavaScript plugins) + answerfieldslideritem, answerfieldcalendar

            HtmlGenericControl javascriptControl = new HtmlGenericControl("script");

            javascriptControl.Attributes.Add("id", "jq311");
            javascriptControl.Attributes.Add("src", ResolveUrl(GlobalConfig.ScriptFilesPath + "/jquery-3.3.1.min.js"));
            Page.Header.Controls.Add(javascriptControl);

            //For development purposes only: upgrade jquery

            //Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            //javascriptControl = new HtmlGenericControl("script");
            //javascriptControl.Attributes.Add("id", "jqmigrate");
            //javascriptControl.Attributes.Add("src", ResolveUrl("https://code.jquery.com/jquery-migrate-3.0.0.js"));
            //Page.Header.Controls.Add(javascriptControl);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("src", ResolveUrl(GlobalConfig.ScriptFilesPath + "/jquery-ui-1.12.1.min.js"));
            Page.Header.Controls.Add(javascriptControl);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("src", ResolveUrl(GlobalConfig.ScriptFilesPath + "/jquery-ui-i18n.min.js"));
            Page.Header.Controls.Add(javascriptControl);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("src", ResolveUrl(GlobalConfig.ScriptFilesPath + "/modernizr-2.8.3.js"));
            Page.Header.Controls.Add(javascriptControl);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));


        }



        protected virtual void BuildResumeRequest()
        {
            TableCell cell = new TableCell();
            TableRow row = new TableRow();
            Button child = new Button {CssClass = CssXmlManager.GetString("ResumeRequestUidButton"), ID = "rrUB"};
            Button button2 = new Button {CssClass = CssXmlManager.GetString("ResumeRequestCancelButton"), ID ="rrCB"};
            this._resumeTable = new Table();
            _resumeTable.ControlStyle.CssClass = CssXmlManager.GetString("ResumeRequestTable");
            _resumeTable.ID = "rct";
            this._resumeUIdTextBox = new TextBox();

            cell.CssClass = CssXmlManager.GetString("EnterResumeUIdMessage");
            cell.ID = "erUM";
            cell.Controls.Add(new LiteralControl(ResourceManager.GetString("EnterResumeUIdMessage", this.LanguageCode)));
            row.Cells.Add(cell);
            this._resumeTable.Rows.Add(row);

            cell = new TableCell();
            row = new TableRow();
            this._resumeUIdTextBox.Columns = 20;
            cell.Controls.Add(this._resumeUIdTextBox);
            child.Text = ResourceManager.GetString("SubmitResumeUId", this.LanguageCode);
            child.Click += new EventHandler(this.OnResumeUIdSubmit);
            button2.Click += new EventHandler(this.OnCancelResumeSubmit);
            button2.Text = ResourceManager.GetString("SubmitCancelResume", this.LanguageCode);
            cell.Controls.Add(child);
            cell.Controls.Add(button2);
            row.Cells.Add(cell);
            this._resumeTable.Rows.Add(row);

            this.Controls.Clear();
            this.Controls.Add(this._resumeTable);
        }

        protected virtual TableRow BuildRow(string cellText, Style rowStyle)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            if ((cellText != null) && (cellText.Length > 0))
            {
                cell.Text = cellText;
                cell.Wrap = true;//JJ Answer should wrap
            }
            row.Cells.Add(cell);
            row.ControlStyle.CopyFrom(rowStyle);
            return row;
        }

        //TODO SP25
        protected virtual Panel BuildPanelRow(string cellText, Style rowStyle)
        {
            Panel PanelRow = new Panel();
            Label cell = new Label();

            if ((cellText != null) && (cellText.Length > 0))
            {
                cell.Text = cellText;
                //cell.Wrap = true;
            }

            PanelRow.Controls.Add(cell);
            cell.ControlStyle.CopyFrom(rowStyle);

            return PanelRow;
        }

        protected virtual TableRow BuildRow(Control child, Style rowStyle)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            if (child != null)
            {
                cell.Controls.Add(child);
            }
            row.Cells.Add(cell);
            row.ControlStyle.CopyFrom(rowStyle);
            return row;
        }

        protected virtual Button BuildSaveProgressButton()
        {
            Button button = new Button();
            button.ID = "spB";
            button.ControlStyle.CopyFrom(this.ButtonStyle);
            button.CssClass = CssXmlManager.GetString("SaveProgressButton");
            button.Text = (this.SaveProgressText == null) ? ResourceManager.GetString("SaveProgress", this.LanguageCode) : this.SaveProgressText;
            button.Click += new EventHandler(this.OnSaveProgressClick);
            return button;
        }

        protected virtual Panel BuildSubmitButtonRow()
        {
            Panel submitControl = new Panel { CssClass = CssXmlManager.GetString("BuildSubmitButtonPanel"), ID="bsbP" };
            Panel panel;

            Panel subPanel = new Panel { CssClass = CssXmlManager.GetString("BuildSubmitButtonSubPanel"), ID="bsbSP" };

            // submit button
            this._submitButton.ControlStyle.CopyFrom(this.ButtonStyle);
            this._submitButton.CssClass = CssXmlManager.GetString("SurveySubmitButton");
            this._submitButton.ID = "ssB";
            this._submitButton.Text = (this.ButtonText == null) ? ResourceManager.GetString("SubmitSurvey", this.LanguageCode) : this.ButtonText;

                // to prevent fast/ double clicking enter button:
            this._submitButton.OnClientClick = "var e=this;setTimeout(function(){e.disabled=true;},0);return true;";

            this._submitButton.Click += new EventHandler(this.OnAnswersSubmit);

            if (this.EnableValidation)
            {
                this._submitButton.Attributes.Add("OnClick", string.Format("return {0}{1}();", GlobalConfig.SurveyValidationFunction, this.ID));
            }
            

            panel = new Panel { CssClass = CssXmlManager.GetString("SurveySubmitButtonPanel"), ID="ssbP" };
            panel.Controls.Add(this._submitButton);
            subPanel.Controls.Add(panel);

            submitControl.Controls.Add(subPanel);

            //add previous page button in case Navigation is enabled
            if (this._enableNavigation)
            {
                panel = new Panel { CssClass = CssXmlManager.GetString("BuildSubmitPPBSubPanel"), ID = "bsbPPBP" };
                panel.Controls.Add(this.BuildPreviousPageButton(CurrentPageIndex > 1));
                subPanel.Controls.Add(panel);
            }

            subPanel = this.BuildFooterRow(subPanel, true);


            //resume button
            if ((this._resumeMode == ResumeMode.Manual) && (this.TotalPageNumber == 1))
            {
                panel = new Panel { CssClass = CssXmlManager.GetString("NextPageResumeProgressButtonPanel"), ID = "npRPBP" };
                panel.Controls.Add(this.BuildResumeProgressButton());
                submitControl.Controls.Add(panel);
            }
            if (this._resumeMode != ResumeMode.NotAllowed)
            {
                panel = new Panel { CssClass = CssXmlManager.GetString("NextPageSaveProgressButtonPanel"), ID = "npSPBP" };
                panel.Controls.Add(this.BuildSaveProgressButton());
                submitControl.Controls.Add(panel);
            }

            return submitControl;
        }

        private Control BuildCustomHeaderOrFooter(string text)
        {
            Literal ctl = new Literal();
            ctl.Mode = LiteralMode.PassThrough;
            ctl.Text = text;
            return ctl;
        }

        protected virtual void BuildSurvey()
        {
            if (!HttpContext.Current.Request.Path.Contains(Votations.NSurvey.Constants.Constants.PreviewPage)
                && (this._surveyRow.IsActivatedNull() || !this._surveyRow.Activated))
            {
                BuildInactiveSurvey();
            }
            else
                if (this.SecuritySetup(true))
                {
                    this.EnableValidation = false;
                }
                else if (this.ViewState["ResumeInProgress"] != null)
                {
                    this.BuildResumeRequest();
                }
                else if (!this._surveyRow.IsOpenDateNull() && (DateTime.Now < this._surveyRow.OpenDate))
                {
                    this.BuildOpenDate();
                }
                else if (!this._surveyRow.IsCloseDateNull() && (DateTime.Now > this._surveyRow.CloseDate))
                {
                    this.BuildCloseDate();
                }
                else if (this.CreateVoterData())
                {
                    this.BuildSurveyBox(false);
                    this._questionContainer.Rows[1].ControlStyle.CopyFrom(this.ConfirmationMessageStyle);
                    this._questionContainer.Rows[1].Cells[0].Controls.Add(new LiteralControl(ResourceManager.GetString("SuccessResumeUId", this.LanguageCode)));
                }
                else
                {
                    this.BuildSurveyBox(true);
                }
        }


        /// <summary>
        /// Builds the HTML to show the survey box
        /// </summary>
        protected virtual void BuildSurveyBox(bool enableQuestionDefaults)
        {   
            QuestionData data;
            this._questions = new QuestionItemCollection();

            //TODO SP25
            this._questionPanel = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetMainPercentagePanel();

            Votations.NSurvey.SQLServerDAL.SurveyLayout u = new Votations.NSurvey.SQLServerDAL.SurveyLayout();
            var _userSettings = u.SurveyLayoutGet(this.SurveyId, this.LanguageCode);

            if (!(_userSettings == null || _userSettings.SurveyLayout.Count == 0))
            {
                this.Controls.Add(BuildCustomHeaderOrFooter(HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyHeaderText)));
            }

            Panel subpanel = new Panel { CssClass = CssXmlManager.GetString("BuildSurveyBoxQCSubPanel"), ID="bsbQCSP" };

            //TODO SP25
            subpanel.Controls.Add(this._questionPanel);

            this.Controls.Add(subpanel);


            PageOptionData surveyPageOptions = new Surveys().GetSurveyPageOptions(this.SurveyId, this.CurrentPageIndex);
            if (surveyPageOptions.PageOptions.Rows.Count > 0)
            {
                this._enableSubmitButton = surveyPageOptions.PageOptions[0].EnableSubmitButton;
                this._randomQuestions = surveyPageOptions.PageOptions[0].RandomizeQuestions;
            }
            if (this._randomQuestions)
            {
                data = new Questions().GetRandomPagedQuestions(this.SurveyId, this.CurrentPageIndex, this.GetRandomSeedForQuestion(), this.LanguageCode);
            }
            else
            {
                data = new Questions().GetPagedQuestions(this.SurveyId, this.CurrentPageIndex, this.LanguageCode);
            }
            foreach (QuestionData.QuestionsRow row2 in data.Questions.Rows)
            {
                if (!new Question().SkipQuestion(row2.QuestionId, this.VoterAnswers, this._isScored))
                {

                    //TODO sp25
                    Panel panelrow = new Panel();
                    this._questionPanel.Controls.Add(panelrow);

                    this.InsertQuestion(row2, panelrow, enableQuestionDefaults);
                }
                else
                {
                    this.CleanVoterQuestionAnswers(QuestionItemFactory.Create(row2, null, this.UniqueID, this.GetRandomSeedForQuestion(), (VoterAnswersData.VotersAnswersDataTable)this.VoterAnswers.VotersAnswers.Copy(), enableQuestionDefaults));
                }
            }
            if (this._enableNavigation && this._previousQuestionNumbering)
            {
                this.ReverseQuestionsNumbers();
            }
            if ((this.CurrentPageIndex < this.TotalPageNumber) && !this._enableSubmitButton)
            {
                subpanel.Controls.Add(this.BuildNextPageButtonRow(true));
            }
            else
            {
                subpanel.Controls.Add(this.BuildSubmitButtonRow());
            }

            if (!(_userSettings == null || _userSettings.SurveyLayout.Count == 0))
            {
                this.Controls.Add(BuildCustomHeaderOrFooter(HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyFooterText)));
            }
            this.ViewState["SubmitButtonUID"] = this._submitButton.UniqueID;
        }

        /// <summary>
        /// Renders the thank you message box 
        /// </summary>
        protected virtual void BuildThanksBox(bool showConditionalMessage)
        {
            this.EnableValidation = false;
            string text = null;
            Table child = new Table {CssClass = CssXmlManager.GetString("BuildThankBoxCssTable"), ID="bsbcT"};

            Panel wrapper = new Panel {CssClass = CssXmlManager.GetString("BuildThankBoxCssPanel"), ID= "bsbcP" };
            Panel subpanel = new Panel {CssClass = CssXmlManager.GetString("BuildThankBoxCssSubPanel"), ID="bsbcSP"};
            wrapper.Controls.Add(subpanel);
            subpanel.Controls.Add(child);

            if (showConditionalMessage)
            {
                text = new Survey().GetThanksMessage(this.SurveyId, this.VoterAnswers, this._isScored);
            }

            child.Rows.Add(text == null
                ? this.BuildRow(new LiteralControl("<div id=\"tyMsg\" class=\"" + CssXmlManager.GetString("ThankYouMessage") + "\">" + this._thankYouMessage + "</div>"), null)
                : this.BuildRow(new LiteralControl(text), null));
            
            this.Controls.Clear();
            subpanel.Controls.Add(this.BuildNextPageButtonRow(false));

            AddHeaderFooter(this.Controls, wrapper);
        }

        /// <summary>
        /// Clean previous existing voter answers to avoid duplicates 
        /// or unchecked answer getting check when user uses
        /// navigation or resume modes
        /// </summary>
        private void CleanVoterQuestionAnswers(QuestionItem questionControl)
        {
            MatrixQuestion question = questionControl as MatrixQuestion;
            if ((question != null) && (question.DataSource != null))
            {
                this.DeleteVoterAnswers(questionControl.QuestionId);
                MatrixChildQuestionData dataSource = (MatrixChildQuestionData)question.DataSource;
                for (int i = 0; i < dataSource.ChildQuestions.Rows.Count; i++)
                {
                    this.DeleteVoterAnswers(dataSource.ChildQuestions[i].QuestionId);
                }
            }
            else
            {
                this.DeleteVoterAnswers(questionControl.QuestionId);
            }
        }

        protected override void CreateChildControls()
        {
            if (!this._isDesignTime)
            {
                this.GetSurveyData();
                if (this.SetMultiLanguage())
                {
                    this.BuildLanguageRequest();
                }
                else
                {
                    this.InitSecurityAddIns();
                    this.BuildSurvey();
                }
            }
        }

        protected virtual bool CreateVoterData()
        {
            bool flag = false;
            if (this.VoterAnswers.Voters.Rows.Count == 0)
            {
                if ((this._resumeMode != ResumeMode.Automatic) || !this.ResumeSession(this.LoadResumeUidFromMedium()))
                {
                    VoterAnswersData.VotersRow row = this.VoterAnswers.Voters.NewVotersRow();
                    row.SurveyId = this.SurveyId;
                    row.StartDate = DateTime.Now;
                    row.LanguageCode = this.LanguageCode;
                    row.Validated = false;
                    row.IPSource = (this.Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) ? this.Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : this.Context.Request.ServerVariables["REMOTE_ADDR"];
                    this.VoterAnswers.Voters.AddVotersRow(row);
                    this.OnVoterDataCreated(new FormItemEventArgs(this.VoterAnswers));
                }
                else
                {
                    flag = true;
                }
                new Survey().IncrementSurveyViews(this.SurveyId, 1);
            }
            return flag;
        }

        /// <summary>
        /// Deletes the voter answers of a question
        /// this is required to avoid having selected answers
        /// that where unselected by user after restoring state 
        /// </summary>
        /// <param name="questionId">question from which we want to delete the voter answers</param>
        protected virtual void DeleteVoterAnswers(int questionId)
        {
            VoterAnswersData.VotersAnswersRow[] rowArray = (VoterAnswersData.VotersAnswersRow[])this.VoterAnswers.VotersAnswers.Select("QuestionId=" + questionId);
            for (int i = 0; i < rowArray.Length; i++)
            {
                rowArray[i].Delete();
            }
        }

        /// <summary>
        /// Generates the complete client side javascript that will
        /// call the question's client side generated validation 
        /// methods
        /// </summary>
        protected virtual string GenerateClientSideValidationCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<script type=\"text/javascript\"><!--{0}function {1}{2}(){{/*alert('start survey validation');*/", Environment.NewLine, GlobalConfig.SurveyValidationFunction, this.ID));
            builder.Append(this._questionClientValidationCalls);
            builder.Append("return true;}//--></script>");
            return builder.ToString();
        }

        /// <summary>
        /// Return the current random seed for the question which 
        /// requires answer randomization
        /// </summary>
        public virtual int GetRandomSeedForQuestion()
        {
            if (this.ViewState["RandomSeed"] == null)
            {
                this.ViewState["RandomSeed"] = DateTime.Now.Millisecond;
            }
            return int.Parse(this.ViewState["RandomSeed"].ToString());
        }

        /// <summary>
        /// Retrieve all survey data from the db
        /// </summary>
        protected virtual void GetSurveyData()
        {
            SurveyData surveyById = new Surveys().GetSurveyById(this.SurveyId, this.LanguageCode);
            this._surveyRow = surveyById.Surveys[0];
            this._surveyTitle = this._surveyRow.Title;
            this._redirectionURL = this._surveyRow.RedirectionURL;
            this._unAuthentifiedUserAction = (UnAuthentifiedUserAction)this._surveyRow.UnAuthentifiedUserActionID;
            this._enableNavigation = this._surveyRow.NavigationEnabled;
            this._progressMode = (ProgressDisplayMode)this._surveyRow.ProgressDisplayModeID;
            this._notificationMode = (NotificationMode)this._surveyRow.NotificationModeID;
            this._resumeMode = (ResumeMode)this._surveyRow.ResumeModeID;
            this._emailFrom = this._surveyRow.EmailFrom;
            this._emailTo = this._surveyRow.EmailTo;
            this._emailSubject = this._surveyRow.EmailSubject;
            this._isScored = this._surveyRow.Scored;
            this._showQuestionNumbers = !this._surveyRow.QuestionNumberingDisabled;
            this._multiLanguageMode = (MultiLanguageMode)this._surveyRow.MultiLanguageModeId;
            this._languageVariable = this._surveyRow.MultiLanguageVariable;
            this.TotalPageNumber = this._surveyRow.TotalPageNumber;
            if ((this._surveyRow.ThankYouMessage != null) && (this._surveyRow.ThankYouMessage.Length != 0))
            {
                this._thankYouMessage = this._surveyRow.ThankYouMessage;
            }
            else
            {
                this._thankYouMessage = ResourceManager.GetString("ThankYouMessage", this.LanguageCode);
            }
        }

        /// <summary>
        /// Get the security addins that where setup for this survey
        /// and retrieve create their type instance from the security factory
        /// </summary>
        protected virtual void InitSecurityAddIns()
        {
            this._securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetEnabledWebSecurityAddIns(this.SurveyId), this.ViewState, this.LanguageCode);
            for (int i = 0; i < this._securityAddIns.Count; i++)
            {
                this._securityAddIns[i].UserAuthenticated += new UserAuthenticatedEventHandler(this.OnUserAuthenticated);
            }
        }

        /// <summary>
        /// Get the webcontrol instance of the question BE and 
        /// adds it to the control hierarchie
        /// </summary>
        /// <param name="question"></param>
        protected virtual void InsertQuestion(QuestionData.QuestionsRow question, Control container, bool enableQuestionDefaults)
        {
            QuestionItem child = QuestionItemFactory.Create(question, this.LanguageCode, this.UniqueID, this.GetRandomSeedForQuestion(), (VoterAnswersData.VotersAnswersDataTable)this.VoterAnswers.VotersAnswers.Copy(), enableQuestionDefaults);
            child.AnswerStyle = this.AnswerStyle;
            child.QuestionStyle = this.QuestionStyle;
            child.QuestionNumber = 0;
            if (child is SectionQuestion)
            {
                ((SectionQuestion)child).SectionOptionStyle = this.SectionOptionStyle;
            }
            ActiveQuestion question2 = child as ActiveQuestion;
            if (question2 != null)
            {
                if (this._showQuestionNumbers)
                {
                    question2.QuestionNumber = this.QuestionNumber + this._questionNumbers;
                }
                question2.EnableClientSideValidation = this.EnableValidation;
                question2.EnableServerSideValidation = this._didPostBack;
                question2.ValidationMarkStyle = this.QuestionValidationMarkStyle;
                question2.ValidationMark = this.QuestionValidationMark;
                question2.ValidationMessageStyle = this.QuestionValidationMessageStyle;
                question2.ConfirmationMessageStyle = this.ConfirmationMessageStyle;
                question2.AnswerPosted += new AnswerPostedEventHandler(this.OnAnswerPost);
                question2.SelectionRequired += new SelectionRequiredEventHandler(this.OnSelectionRequired);
                question2.SelectionOverflow += new SelectionOverflowEventHandler(this.OnSelectionRequired);
                question2.InvalidAnswers += new InvalidAnswersEventHandler(this.OnInvalidAnswers);
                question2.ClientScriptGenerated += new ClientScriptGeneratedEventHandler(this.OnClientScriptGeneration);
                this._questionNumbers++;
            }
            MatrixQuestion question3 = child as MatrixQuestion;
            if (question3 != null)
            {
                question3.MatrixHeaderStyle = this.MatrixHeaderStyle;
                question3.MatrixItemStyle = this.MatrixItemStyle;
                question3.MatrixAlternatingItemStyle = this.MatrixAlternatingItemStyle;
                question3.MatrixStyle = this.MatrixStyle;
            }
            SectionQuestion question4 = child as SectionQuestion;
            if (question4 != null)
            {
                question4.EnableGridSectionClientSideValidation = this.EnableValidation;
                question4.EnableGridSectionServerSideValidation = this.EnableValidation;
                question4.SectionGridAnswersHeaderStyle = this.SectionGridAnswersHeaderStyle;
                question4.SectionGridAnswersItemStyle = this.SectionGridAnswersItemStyle;
                question4.SectionGridAnswersAlternatingItemStyle = this.SectionGridAnswersAlternatingItemStyle;
                question4.SectionGridAnswersStyle = this.SectionGridAnswersStyle;
            }
            container.Controls.Add(child);
            this.CleanVoterQuestionAnswers(child);
            this.OnQuestionCreated(new FormQuestionEventArgs(child));
            if (!this._previousQuestionNumbering)
            {
                this.OnQuestionBound(new FormQuestionEventArgs(child));
            }
            this._questions.Add(child);
        }

        /// <summary>
        /// Loads the resume unique identifier and 
        /// if any available returns it else return null
        /// </summary>
        protected virtual string LoadResumeUidFromMedium()
        {
            HttpCookie cookie = new HttpCookie("NSurveyCookieEnabledCheck", "1");
            this._context.Response.Cookies.Add(cookie);
            if (this._context.Request.Cookies["NSurveyResumeSurvey" + this.SurveyId] != null)
            {
                return this._context.Request.Cookies["NSurveyResumeSurvey" + this.SurveyId].Value;
            }
            return null;
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            if ((this.ViewState["SubmitButtonUID"] != null) && (this.Page.Request.Form[this.ViewState["SubmitButtonUID"].ToString()] != null))
            {
                this._didPostBack = true;
            }
        }

        /// <summary>
        /// Store the answers received in a 
        /// temporary storage
        /// </summary>
        /// <param name="sender">The question that raised the event</param>
        /// <param name="e">Answers posted with the question</param>
        protected virtual void OnAnswerPost(object sender, QuestionItemAnswersEventArgs e)
        {
            int voterId = this.VoterAnswers.Voters[0].VoterId;
            foreach (PostedAnswerData data in e.Answers)
            {
                VoterAnswersData.VotersAnswersRow row = this.currentVisitorAnswerSet.VotersAnswers.NewVotersAnswersRow();
                row.VoterId = voterId;
                row.AnswerId = data.AnswerId;
                row.QuestionId = data.Item.QuestionId;
                row.AnswerText = data.FieldText;
                row.SectionNumber = data.SectionNumber;
                row.TypeMode = (int)data.TypeMode;
                this.currentVisitorAnswerSet.EnforceConstraints = false;
                this.currentVisitorAnswerSet.VotersAnswers.AddVotersAnswersRow(row);
            }
        }

        /// <summary>
        /// On click event of Submit button
        /// </summary>
        /// <param name="sender">sender: submitbutton</param>
        /// <param name="e">raised event</param>
        protected virtual void OnAnswersSubmit(object sender, EventArgs e)
        {

                if (!this._selectionsAreRequired)
                {
                        this.SubmitAnswersToDb();

                        this.CurrentPageIndex = 1;
                        this.QuestionNumber = 0;
                        this.HighestPageNumber = 0;
                }

            //TODO SP2.5 remove test:
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert", "document.getElementById('LoadMessage').style.display = 'block';", true);
            
        }


        protected virtual void OnCancelResumeSubmit(object sender, EventArgs e)
        {
            this.ViewState["ResumeInProgress"] = null;
            this.Controls.Clear();
            this.BuildSurveyBox(false);
        }

        /// <summary>
        /// Build the client side calls to the questions
        /// that requires client script validation
        /// </summary>
        protected virtual void OnClientScriptGeneration(object sender, QuestionItemClientScriptEventArgs e)
        {
            ActiveQuestion question = sender as ActiveQuestion;
            if (question != null)
            {
                if (e.Section != null)
                {
                    this._questionClientValidationCalls.Append(string.Format("if (typeof({0}{1}) == 'function' && !{0}{1}()){{return false;}}", GlobalConfig.QuestionValidationFunction, e.Section.UniqueID.Replace(":", "_")));
                }
                else
                {
                    this._questionClientValidationCalls.Append(string.Format("if (typeof({0}{1}) == 'function' && !{0}{1}()){{return false;}}", GlobalConfig.QuestionValidationFunction, question.UniqueID.Replace(":", "_")));
                }
            }
        }

        /// <summary>
        /// Post an event when user data and answers 
        /// has been saved in the database
        /// </summary>
        /// <param name="e">The form answers and user info</param>
        protected virtual void OnFormSubmitted(FormItemEventArgs e)
        {
            if (this.FormSubmitted != null)
            {
                this.FormSubmitted(this, e);
            }
        }

        /// <summary>
        /// Post an event when user data and answers 
        /// has been saved in the database
        /// </summary>
        /// <param name="e">The form answers and user info</param>
        protected virtual void OnFormSubmitting(FormItemEventArgs e)
        {
            if (this.FormSubmitting != null)
            {
                this.FormSubmitting(this, e);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!this._isDesignTime)
            {
                AddHeaderScriptCss();

                this._submitButton.ID = "SurveySubmit";
                this.ViewState["LastAuthenticatedAddIn"] = -1;

                this._questionContainer.Rows.Add(this.BuildRow("", null));
                this._questionContainer.Rows[0].EnableViewState = false;
                this._questionContainer.Rows.Add(this.BuildRow("", null));
                this._questionContainer.Rows[1].EnableViewState = false;
            }
        }

        /// <summary>
        /// Set the control SelectionsAreRequired flag to true
        /// and stores the sender for futher processing
        /// </summary>
        /// <param name="sender">The question that raised the event</param>
        /// <param name="e">Answers, if any, posted with the question</param>
        protected virtual void OnInvalidAnswers(object sender, QuestionItemInvalidAnswersEventArgs e)
        {
            if (sender is QuestionItem)
            {
                this._selectionsAreRequired = true;
            }
        }

        protected virtual void OnLanguageSubmit(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.LanguageCode = this._languageDropDownList.SelectedValue;
            this.InitSecurityAddIns();
            this.BuildSurvey();
        }

        /// <summary>
        /// If all min/max selections are completed, 
        /// store the temp answers into the final storage 
        /// and loads the next set of questions and render the new page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPageChangeClick(object sender, EventArgs e)
        {
            if (!this._selectionsAreRequired)
            {
                if (this.currentVisitorAnswerSet.VotersAnswers.Rows.Count > 0)
                {
                    this.VoterAnswers.Merge(this.currentVisitorAnswerSet, false);
                }
                int targetPage = new Survey().GetNextPage(this.SurveyId, this.CurrentPageIndex, this.VoterAnswers, this._isScored);
                int currentPageIndex = this.CurrentPageIndex;
                int questionNumber = this.QuestionNumber;
                if (targetPage == -1)
                {
                    foreach (QuestionData.QuestionsRow row in new Questions().GetQuestionListForPageRange(this.SurveyId, this.CurrentPageIndex + 1, this.TotalPageNumber).Questions.Rows)
                    {
                        this.DeleteVoterAnswers(row.QuestionId);
                    }
                    this.SubmitAnswersToDb();
                }
                else
                {
                    if (targetPage > (this.CurrentPageIndex + 1))
                    {
                        foreach (QuestionData.QuestionsRow row2 in new Questions().GetQuestionListForPageRange(this.SurveyId, this.CurrentPageIndex + 1, targetPage - 1).Questions.Rows)
                        {
                            this.DeleteVoterAnswers(row2.QuestionId);
                        }
                    }
                    this.CurrentPageIndex = targetPage;
                    this.QuestionNumber += this._questionNumbers - 1;
                    this._questionNumbers = 1;
                    this.OnPageChanging(new FormNavigationEventArgs(this.VoterAnswers, currentPageIndex, targetPage, questionNumber, this.QuestionNumber, FormNavigationMode.Forward));
                    this.Controls.Clear();
                    this._questionClientValidationCalls = new StringBuilder();
                    if (this.CurrentPageIndex > this.HighestPageNumber)
                    {
                        this.BuildSurveyBox(true);
                        this.HighestPageNumber = this.CurrentPageIndex;
                    }
                    else
                    {
                        this.BuildSurveyBox(false);
                    }
                    this.OnPageChanged(new FormNavigationEventArgs(this.VoterAnswers, currentPageIndex, targetPage, questionNumber, this.QuestionNumber, FormNavigationMode.Forward));
                }
            }
        }

        /// <summary>
        /// Post an event when a page has changed
        /// </summary>
        /// <param name="e">The form answers, user and navigation info</param>
        protected virtual void OnPageChanged(FormNavigationEventArgs e)
        {
            if (this.PageChanged != null)
            {
                this.PageChanged(this, e);
            }
        }

        /// <summary>
        /// Post an event when a page is going to change
        /// </summary>
        /// <param name="e">The form answers, user and navigation info</param>
        protected virtual void OnPageChanging(FormNavigationEventArgs e)
        {
            if (this.PageChanging != null)
            {
                this.PageChanging(this, e);
            }
        }

        /// <summary>
        /// store the temp answers into the final storage 
        /// and loads the previous page set of questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPreviousPageChangeClick(object sender, EventArgs e)
        {
            int targetPage = new Survey().GetPreviousPage(this.SurveyId, this.CurrentPageIndex, this.VoterAnswers, this._isScored);
            int currentPageIndex = this.CurrentPageIndex;
            if (this.currentVisitorAnswerSet.VotersAnswers.Rows.Count > 0)
            {
                this.VoterAnswers.Merge(this.currentVisitorAnswerSet, false);
            }
            this.CurrentPageIndex = targetPage;
            this.OnPageChanging(new FormNavigationEventArgs(this.VoterAnswers, currentPageIndex, targetPage, this.QuestionNumber, -1, FormNavigationMode.Backward));
            this._previousQuestionNumbering = true;
            this._questionNumbers = 1;
            this.Controls.Clear();
            this._questionClientValidationCalls = new StringBuilder();
            this.BuildSurveyBox(false);
            this.OnPageChanged(new FormNavigationEventArgs(this.VoterAnswers, currentPageIndex, targetPage, this.QuestionNumber, -1, FormNavigationMode.Backward));
        }

        /// <summary>
        /// Post an event when a question has been bound
        /// </summary>
        /// <param name="e">The form question</param>
        protected virtual void OnQuestionBound(FormQuestionEventArgs e)
        {
            if (this.QuestionBound != null)
            {
                this.QuestionBound(this, e);
            }
        }

        /// <summary>
        /// Post an event when a new question has been added to the surveybox
        /// </summary>
        /// <param name="e">The form question</param>
        protected virtual void OnQuestionCreated(FormQuestionEventArgs e)
        {
            if (this.QuestionCreated != null)
            {
                this.QuestionCreated(this, e);
            }
        }

        /// <summary>
        /// Shows the resume interface to let the user
        /// enter his resume code and resume the survey
        /// if he entered the correct code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnResumeProgressClick(object sender, EventArgs e)
        {
            if (this.currentVisitorAnswerSet.VotersAnswers.Rows.Count > 0)
            {
                this.VoterAnswers.Merge(this.currentVisitorAnswerSet, false);
            }
            this.ViewState["ResumeInProgress"] = 1;
            this.BuildResumeRequest();
        }

        //On resume submit error message
        private string GetErrorMessage(string msgname)
        {
            return "<div id=\"ruSE\" class=\"" + CssXmlManager.GetString("OnResumeUidSubmitError") + "\">" + msgname + @"</div>";
        }

        protected virtual void OnResumeUIdSubmit(object sender, EventArgs e)
        {
            if (this.ResumeSession(this._resumeUIdTextBox.Text))
            {
                this.ViewState["ResumeInProgress"] = null;
                this.Controls.Clear();
                this.BuildSurveyBox(false);
                this._questionContainer.Rows[1].ControlStyle.CopyFrom(this.ConfirmationMessageStyle);
                this._questionContainer.Rows[1].ID = "rrRow";
                this._questionContainer.Rows[1].Cells[0].Controls.Add(new LiteralControl(ResourceManager.GetString("SuccessResumeUId", this.LanguageCode)));
            }
            else
            {
                TableCell cell = new TableCell();
                TableRow row = new TableRow();
                cell.Text = GetErrorMessage(ResourceManager.GetString("InvalidResumeUId", this.LanguageCode));
                cell.ColumnSpan = 2;
                row.Cells.Add(cell);

                this._resumeTable.Rows.AddAt(0, row);
            }
        }

        /// <summary>
        /// store the temp answers into the final storage 
        /// and save the current voter's answer state in the DB 
        /// to allow a resume when the voter comes back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSaveProgressClick(object sender, EventArgs e)
        {
            if (this.currentVisitorAnswerSet.VotersAnswers.Rows.Count > 0)
            {
                this.VoterAnswers.Merge(this.currentVisitorAnswerSet, false);
            }
            if (!this.VoterAnswers.Voters[0].IsResumeUIDNull())
            {
                new Voter().DeleteVoterResumeSession(this.SurveyId, this.VoterAnswers.Voters[0].ResumeUID);
            }
            else
            {
                this.VoterAnswers.Voters[0].ResumeUID = new Voter().GenerateResumeUId(8);
            }
            this.VoterAnswers.Voters[0].ProgressSaveDate = DateTime.Now;
            this.VoterAnswers.Voters[0].ResumeAtPageNumber = this.CurrentPageIndex;
            this.VoterAnswers.Voters[0].ResumeQuestionNumber = this.QuestionNumber;
            this.VoterAnswers.Voters[0].ResumeHighestPageNumber = this.HighestPageNumber;
            this.OnSessionSaving(new FormSessionEventArgs(this.VoterAnswers, this.VoterAnswers.Voters[0].ResumeUID));
            if (this._resumeMode == ResumeMode.Automatic)
            {
                string text = this.SaveResumeUidToMedium(this.VoterAnswers.Voters[0].ResumeUID);
                if (text == null)
                {
                    new Voter().SaveVoterProgress(this.VoterAnswers);
                    this._questionContainer.Rows[1].ControlStyle.CopyFrom(this.ConfirmationMessageStyle);
                    this._questionContainer.Rows[1].ID = "rcRow";
                    this._questionContainer.Rows[1].Cells[0].Controls.Add(new LiteralControl(ResourceManager.GetString("ProgressSaved", this.LanguageCode)));
                    this.OnSessionSaved(new FormSessionEventArgs(this.VoterAnswers, this.VoterAnswers.Voters[0].ResumeUID));
                }
                else
                {
                    this._questionContainer.Rows[1].ControlStyle.CopyFrom(this.QuestionValidationMessageStyle);
                    this._questionContainer.Rows[1].ID = "rcRow";
                    this._questionContainer.Rows[1].Cells[0].Controls.Add(new LiteralControl(text));
                }
            }
            else
            {
                this._questionContainer.Rows[1].ControlStyle.CopyFrom(this.ConfirmationMessageStyle);
                this._questionContainer.Rows[1].ID = "rcRow";
                this._questionContainer.Rows[1].Cells[0].Controls.Add(new LiteralControl(string.Format(ResourceManager.GetString("ManualProgressSaved", this.LanguageCode), this.VoterAnswers.Voters[0].ResumeUID)));
                new Voter().SaveVoterProgress(this.VoterAnswers);
                this.OnSessionSaved(new FormSessionEventArgs(this.VoterAnswers, this.VoterAnswers.Voters[0].ResumeUID));
            }
        }

        /// <summary>
        /// Set the control SelectionsAreRequired flag to true
        /// and stores the sender for futher processing
        /// </summary>
        /// <param name="sender">The question that raised the event</param>
        /// <param name="e">Answers, if any, posted with the question</param>
        protected virtual void OnSelectionRequired(object sender, QuestionItemAnswersEventArgs e)
        {
            if (sender is QuestionItem)
            {
                this._selectionsAreRequired = true;
            }
        }

        /// <summary>
        /// Post an event when a session has been resumed
        /// </summary>
        /// <param name="e">The form answers, user and navigation info</param>
        protected virtual void OnSessionResumed(FormSessionEventArgs e)
        {
            if (this.SessionResumed != null)
            {
                this.SessionResumed(this, e);
            }
        }

        /// <summary>
        /// Post an event when a session is going to be resumed
        /// </summary>
        /// <param name="e">The form answers, user and navigation info</param>
        protected virtual void OnSessionResuming(FormSessionEventArgs e)
        {
            if (this.SessionResuming != null)
            {
                this.SessionResuming(this, e);
            }
        }

        /// <summary>
        /// Post an event when a session has been saved
        /// </summary>
        /// <param name="e">The form answers, user and navigation info</param>
        protected virtual void OnSessionSaved(FormSessionEventArgs e)
        {
            if (this.SessionSaved != null)
            {
                this.SessionSaved(this, e);
            }
        }

        /// <summary>
        /// Post an event when a session is going to be saved
        /// </summary>
        /// <param name="e">The form answers, user and navigation info</param>
        protected virtual void OnSessionSaving(FormSessionEventArgs e)
        {
            if (this.SessionSaving != null)
            {
                this.SessionSaving(this, e);
            }
        }

        private void OnUserAuthenticated(object sender, UserAuthenticatedEventArgs e)
        {
            this.CurrentSecurityAddIn++;
            this.Controls.Clear();
            this.BuildSurvey();
        }

        /// <summary>
        /// Post an event when user data have been created
        /// but at this point they aren't stored in the database 
        /// so the voter id is not the one that will be saved 
        /// in the database
        /// </summary>
        /// <param name="e">The form answers and user info</param>
        protected virtual void OnVoterDataCreated(FormItemEventArgs e)
        {
            if (this.VoterDataCreated != null)
            {
                this.VoterDataCreated(this, e);
            }
        }

        /// <summary>
        /// Generates the client validation
        /// script while rendering the page
        /// </summary>
        protected override void Render(HtmlTextWriter writer)
        {
            if (!this._isDesignTime)
            {
                if (this.EnableValidation)
                {
                    writer.Write(this.GenerateClientSideValidationCode());
                }
                TimeSpan span = new TimeSpan(DateTime.Now.Ticks - this._startTime);
                this.Context.Trace.Warn("survey execution time was : " + span);
            }
            base.Render(writer);
        }

        /// <summary>
        /// Renders the control in the designer
        /// </summary>
        public void RenderAtDesignTime()
        {
            if (this._isDesignTime)
            {
                this.Controls.Clear();
                if (this.SurveyId == -1)
                {
                    Literal child = new Literal();
                    child.Text = "Please set the SurveyId property first!";
                    this.Controls.Add(child);
                }
                else
                {
                    ArrayList list = new ArrayList();
                    list.Add("#Answer 1#");
                    list.Add("#Answer 2#");
                    list.Add("#Answer 3#");
                    list.Add("#Answer 4#");
                    list.Add("#Answer 5#");
                    Table table = new Table();
                    new TableRow();
                    new TableCell();
                    TableRow row = new TableRow();
                    TableCell cell = new TableCell();
                    Button button = new Button();
                    Button button2 = new Button();
                    PlaceHolder holder = new PlaceHolder();
                    new Literal();
                    table.Rows.Add(this.BuildRow("Error Message", this.QuestionValidationMessageStyle));
                    table.Rows.Add(this.BuildRow("Confirmation message", this.ConfirmationMessageStyle));
                    row.ControlStyle.CopyFrom(this.QuestionStyle);
                    cell.Controls.Add(new LiteralControl("1. #Question#"));
                    Label label = new Label();
                    label.Text = this.QuestionValidationMark;
                    label.ControlStyle.CopyFrom(this.QuestionValidationMarkStyle);
                    cell.Controls.Add(label);
                    row.Cells.Add(cell);
                    table.Rows.Add(row);
                    RadioButtonList list2 = new RadioButtonList();
                    list2.Width = Unit.Percentage(100.0);
                    list2.DataSource = list;
                    list2.ControlStyle.CopyFrom(this.AnswerStyle);
                    list2.DataBind();
                    table.Rows.Add(this.BuildRow(list2, this.AnswerStyle));
                    button.ControlStyle.CopyFrom(this.ButtonStyle);
                    button.Text = (this.ButtonText == null) ? "Submit" : this.ButtonText;
                    button2.ControlStyle.CopyFrom(this.ButtonStyle);
                    button2.Text = (this.NextPageText == null) ? "Next page >>" : this.NextPageText;
                    holder.Controls.Add(button);
                    holder.Controls.Add(button2);
                    holder.Controls.Add(new LiteralControl(string.Format("Page {0} / {1}", 1, 10)));
                    table.Rows.Add(this.BuildRow(holder, this.FootStyle));
                    table.CellPadding = this.CellPadding;
                    table.CellSpacing = this.CellSpacing;
                    table.Width = base.Width;
                    table.Height = base.Height;
                    table.BackColor = base.BackColor;
                    table.ID = "DtTable" ;
                    this.Controls.Add(table);
                }
            }
        }

        /// <summary>
        /// Try to resume a saved session
        /// </summary>
        /// <returns></returns>
        protected virtual bool ResumeSession(string resumeUId)
        {
            if (resumeUId != null)
            {
                this.OnSessionResuming(new FormSessionEventArgs(this.VoterAnswers, resumeUId));
                VoterAnswersData dataSet = new Voters().ResumeVoterAnswers(this.SurveyId, resumeUId);
                if ((dataSet != null) && (dataSet.Voters.Rows.Count > 0))
                {
                    this.VoterAnswers.Clear();
                    this.VoterAnswers.Merge(dataSet, false);
                    VoterAnswersData.VotersRow row = this.VoterAnswers.Voters[0];
                    this.CurrentPageIndex = row.ResumeAtPageNumber;
                    this.QuestionNumber = row.ResumeQuestionNumber;
                    this.HighestPageNumber = row.IsResumeHighestPageNumberNull() ? row.ResumeAtPageNumber : row.ResumeHighestPageNumber;
                    row.IPSource = (this.Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) ? this.Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : this.Context.Request.ServerVariables["REMOTE_ADDR"];
                    this.LanguageCode = row.LanguageCode;
                    this.OnSessionResumed(new FormSessionEventArgs(this.VoterAnswers, resumeUId));
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Renumber the questions from the 
        /// number that was available on the previous page
        /// </summary>
        protected virtual void ReverseQuestionsNumbers()
        {
            this.QuestionNumber -= this._questionNumbers - 1;
            this._questionNumbers = 1;
            foreach (QuestionItem item in this._questions)
            {
                if ((item is ActiveQuestion) && this._showQuestionNumbers)
                {
                    item.QuestionNumber = this.QuestionNumber + this._questionNumbers;
                    this._questionNumbers++;
                }
                this.OnQuestionBound(new FormQuestionEventArgs(item));
            }
        }

        /// <summary>
        /// Save the resume unique identifier, returns 
        /// an error message in case the save failed
        /// </summary>
        protected virtual string SaveResumeUidToMedium(string guid)
        {
            if (this._context.Request.Cookies["NSurveyCookieEnabledCheck"] == null)
            {
                return ResourceManager.GetString("ProgressNotSaved", this.LanguageCode);
            }
            HttpCookie cookie = new HttpCookie("NSurveyResumeSurvey" + this.SurveyId, guid);
            cookie.Expires = DateTime.Now.AddDays(30.0);
            this._context.Response.Cookies.Add(cookie);
            return null;
        }

        /// <summary>
        /// Setup the security context using the current addin
        /// </summary>
        protected virtual bool SecuritySetup(bool enableUserActions)
        {
            bool flag = false;
            if ((this._securityAddIns.Count == 0) || (this.CurrentSecurityAddIn >= this._securityAddIns.Count))
            {
                return false;
            }
            if (((int)this.ViewState["LastAuthenticatedAddIn"]) != this.CurrentSecurityAddIn)
            {
                flag = this._securityAddIns[this.CurrentSecurityAddIn].IsAuthenticated();
                this.ViewState["LastAuthenticatedAddIn"] = this.CurrentSecurityAddIn;
            }
            if (!flag)
            {
                Control loginInterface = this._securityAddIns[this.CurrentSecurityAddIn].GetLoginInterface(base.ControlStyle);
                if (loginInterface != null)
                {
                    this.Controls.Add(loginInterface);
                }
                else if (enableUserActions)
                {
                    if (this._unAuthentifiedUserAction == UnAuthentifiedUserAction.HideSurvey)
                    {
                        this.Visible = false;
                    }
                    else if (this._unAuthentifiedUserAction == UnAuthentifiedUserAction.GeneralWarning)
                    {
                        BuildSecurityGeneralWarning();

                    } else if (this._unAuthentifiedUserAction == UnAuthentifiedUserAction.ShowThankYouMessage)
                    {
                        this.BuildThanksBox(false);

                    }  else
                    {
                        BuildSecurityGeneralWarning();
                    }

                }
                return true;
            }
            this.CurrentSecurityAddIn++;
            return this.SecuritySetup(enableUserActions);
        }

        /// <summary>
        /// Check and set the multi language 
        /// features if there are enabled
        /// </summary>
        /// <returns>true if we need to show the dropdownlist selection to user </returns>
        protected virtual bool SetMultiLanguage()
        {
            bool flag = false;
            if (this.LanguageCode == null)
            {
                switch (this._multiLanguageMode)
                {
                    case MultiLanguageMode.None:
                        this.LanguageCode = "";
                        break;

                    case MultiLanguageMode.UserSelection:
                        flag = true;
                        break;

                    case MultiLanguageMode.BrowserDetection:
                        this.LanguageCode = "";
                        if (this._context.Request.UserLanguages != null)
                        {
                            string str = this._context.Request.UserLanguages[0];
                            if (str != null)
                            {
                                if (str.Length < 3)
                                {
                                    str = str + "-" + str.ToUpper();
                                }
                                this.LanguageCode = str;
                            }
                        }
                        break;

                    case MultiLanguageMode.QueryString:
                        this.LanguageCode = "";
                        if ((this._languageVariable != null) && (this._context.Request[this._languageVariable] != null))
                        {
                            this.LanguageCode = this._context.Request[this._languageVariable];
                        }
                        break;

                    case MultiLanguageMode.Cookie:
                        this.LanguageCode = "";
                        if ((this._languageVariable != null) && (this._context.Request.Cookies[this._languageVariable] != null))
                        {
                            this.LanguageCode = this._context.Request.Cookies[this._languageVariable].Value;
                        }
                        break;

                    case MultiLanguageMode.Session:
                        this.LanguageCode = "";
                        if ((this._languageVariable != null) && (this._context.Session[this._languageVariable] != null))
                        {
                            this.LanguageCode = this._context.Session[this._languageVariable].ToString();
                        }
                        break;
                }
                if (((this.LanguageCode != null) && (this.LanguageCode.Length > 0)) && !flag)
                {
                    this.LanguageCode = new MultiLanguage().CheckSurveyLanguage(this.SurveyId, this.LanguageCode);
                }
            }
            return flag;
        }

        /// <summary>
        /// On clicking the submitbutton and raising the click event:
        /// Submits the answers to the database and ends the survey
        /// Depending on notification mode sends email message (3 options) to administrator
        /// Optionally redirects to new url
        /// Optionally shows thank you message to surveytaker (buildthankbox)
        /// </summary>
        protected virtual void SubmitAnswersToDb()
        {
            this.CurrentSecurityAddIn = 0;
            this.ViewState["LastAuthenticatedAddIn"] = -1;

            if (!this.SecuritySetup(false))
            {
                if (this.currentVisitorAnswerSet.VotersAnswers.Rows.Count > 0)
                {
                    this.VoterAnswers.Merge(this.currentVisitorAnswerSet, false);
                }
                this.VoterAnswers.Voters[0].Validated = true;
                this.VoterAnswers.Voters[0].VoteDate = DateTime.Now;
                if (!this.VoterAnswers.Voters[0].IsResumeUIDNull())
                {
                    new Voter().DeleteVoterResumeSession(this.SurveyId, this.VoterAnswers.Voters[0].ResumeUID);
                }

                this.OnFormSubmitting(new FormItemEventArgs(this.VoterAnswers));
                new Voter().AddVoter(this.VoterAnswers);

                this.OnFormSubmitted(new FormItemEventArgs(this.VoterAnswers));
                this.UnLoadSecurityAddIns(this.VoterAnswers);

                if (this._notificationMode != NotificationMode.None)
                {
                    new VoterTextReportGenerator(this.VoterAnswers, this.SurveyId).SendReport(this._notificationMode, this._surveyTitle, this._emailFrom, this._emailTo, this._emailSubject);
                }
            }

            if ((this._redirectionURL != null) && (this._redirectionURL.Length != 0))
            {
                HttpContext.Current.Session["voterid"] = this.VoterAnswers.Voters[0].VoterId;
                this.Context.Response.Redirect(this._redirectionURL);

                // url + voterid for resultsreport (not save - voterid in url can be manipulated:
                //this.Context.Response.Redirect(this._redirectionURL+ "&voterid="+ this.VoterAnswers.Voters[0].VoterId);
            }
            else
            {
                //TODO SP2.5 remove test:
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "myalert2", "document.getElementById('LoadMessage').style.display = 'none';", true);
                this.BuildThanksBox(true);
            }

            // clear all voteranswers from viewstate
            this.VoterAnswers.Clear();
            ViewState.Clear();
        }

        /// <summary>
        /// Unloads the available security addins and allow them
        /// to make some operations after the voter and its answers
        /// has been stored in the database
        /// </summary>
        protected virtual void UnLoadSecurityAddIns(VoterAnswersData voterData)
        {
            this._securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetEnabledWebSecurityAddIns(this.SurveyId), this.ViewState, this.LanguageCode);
            for (int i = 0; i < this._securityAddIns.Count; i++)
            {
                this._securityAddIns[i].ProcessVoterData(voterData);
            }
        }

        /// <summary>
        /// Sets the style for the answers
        /// </summary>
        [DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty)]
        public Style AnswerStyle
        {
            get
            {
                if (this._answerStyle != null)
                {
                    return this._answerStyle;
                }
                return new Style();
            }
            set
            {
                this._answerStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the submit button
        /// </summary>
        [Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style ButtonStyle
        {
            get
            {
                if (this._buttonStyle != null)
                {
                    return this._buttonStyle;
                }
                return new Style();
            }
            set
            {
                this._buttonStyle = value;
            }
        }

        /// <summary>
        /// Text of the submit vote button
        /// </summary>
        [DefaultValue("Submit survey"), Category("Text"), PersistenceMode(PersistenceMode.Attribute), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true)]
        public string ButtonText
        {
            get
            {
                return this._buttonText;
            }
            set
            {
                this._buttonText = value;
            }
        }

        /// <summary>
        /// CellPadding of the survey box table
        /// </summary>
        [DefaultValue("0"), Category("Layout"), Bindable(true)]
        public int CellPadding
        {
            get
            {
                return this._cellPadding;
            }
            set
            {
                this._cellPadding = value;
            }
        }

        /// <summary>
        /// CellSpacing of the survey box table
        /// </summary>
        [Category("Layout"), Bindable(true), DefaultValue("0")]
        public int CellSpacing
        {
            get
            {
                return this._cellSpacing;
            }
            set
            {
                this._cellSpacing = value;
            }
        }

        /// <summary>
        /// Style of the confirmation messages of the survey and questions
        /// </summary>
        [DefaultValue((string)null), Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public Style ConfirmationMessageStyle
        {
            get
            {
                if (this._confirmationMessageStyle != null)
                {
                    return this._confirmationMessageStyle;
                }
                return new Style();
            }
            set
            {
                this._confirmationMessageStyle = value;
            }
        }

        /// <summary>
        /// Override control collection and return base collection
        /// </summary>
        public override ControlCollection Controls
        {
            get
            {
                this.EnsureChildControls();
                return base.Controls;
            }
        }

        /// <summary>
        /// Page to display
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                if (this.ViewState["CurrentPageIndex"] != null)
                {
                    return int.Parse(this.ViewState["CurrentPageIndex"].ToString());
                }
                return 1;
            }
            set
            {
                this.ViewState["CurrentPageIndex"] = value;
            }
        }

        /// <summary>
        /// Security AddIn to show
        /// </summary>
        protected int CurrentSecurityAddIn
        {
            get
            {
                if (this.ViewState["CurrentSecurityAddIn"] != null)
                {
                    return int.Parse(this.ViewState["CurrentSecurityAddIn"].ToString());
                }
                return 0;
            }
            set
            {
                this.ViewState["CurrentSecurityAddIn"] = value;
            }
        }

        /// <summary>
        /// Enable client side validation of some answers 
        /// </summary>
        public bool EnableValidation
        {
            get
            {
                return this._enableValidation;
            }
            set
            {
                this._enableValidation = value;
            }
        }

        /// <summary>
        /// Sets the style for the results footer
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Styles"), DefaultValue((string)null), PersistenceMode(PersistenceMode.InnerProperty)]
        public Style FootStyle
        {
            get
            {
                if (this._footStyle != null)
                {
                    return this._footStyle;
                }
                return new Style();
            }
            set
            {
                this._footStyle = value;
            }
        }

        protected int HighestPageNumber
        {
            get
            {
                if (this.ViewState["HighestPageNumber"] != null)
                {
                    return int.Parse(this.ViewState["HighestPageNumber"].ToString());
                }
                return 0;
            }
            set
            {
                this.ViewState["HighestPageNumber"] = value;
            }
        }

        /// <summary>
        /// In which language should it run ?
        /// </summary>
        [PersistenceMode(PersistenceMode.Attribute), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(true), Category("General"), DefaultValue("")]
        public string LanguageCode
        {
            get
            {
                if (this.ViewState["LanguageCode"] != null)
                {
                    return this.ViewState["LanguageCode"].ToString();
                }
                return null;
            }
            set
            {
                this.ViewState["LanguageCode"] = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix items 
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Styles"), DefaultValue((string)null)]
        public Style MatrixAlternatingItemStyle
        {
            get
            {
                if (this._matrixAlternatingItemStyle != null)
                {
                    return this._matrixAlternatingItemStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixAlternatingItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix header
        /// </summary>
        [DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), Category("Styles")]
        public Style MatrixHeaderStyle
        {
            get
            {
                if (this._matrixHeaderStyle != null)
                {
                    return this._matrixHeaderStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixHeaderStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix items 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null)]
        public Style MatrixItemStyle
        {
            get
            {
                if (this._matrixItemStyle != null)
                {
                    return this._matrixItemStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the matrix table 
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty), Category("Styles"), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style MatrixStyle
        {
            get
            {
                if (this._matrixStyle != null)
                {
                    return this._matrixStyle;
                }
                return new Style();
            }
            set
            {
                this._matrixStyle = value;
            }
        }

        /// <summary>
        /// Text of the next page button
        /// </summary>
        [Category("Text"), Bindable(true), DefaultValue("Next Page &gt;&gt;")]
        public string NextPageText
        {
            get
            {
                return this._nextPageText;
            }
            set
            {
                this._nextPageText = value;
            }
        }

        /// <summary>
        /// Keeps track of each page number
        /// that was shown. Used when navigation
        /// is activated to disable / enable the 
        /// default answer values on pages that were 
        /// already visited or nor. 
        /// </summary>
        protected Hashtable PageNavigationHistory
        {
            get
            {
                if (this.ViewState["PageNavigationHistory"] != null)
                {
                    return (Hashtable)this.ViewState["PageNavigationHistory"];
                }
                return new Hashtable();
            }
            set
            {
                this.ViewState["PageNavigationHistory"] = value;
            }
        }

        /// <summary>
        /// Text of the previous question button
        /// </summary>
        [DefaultValue("&lt;&lt; Previous page"), Bindable(true), Category("Text")]
        public string PreviousPageText
        {
            get
            {
                return this._previousPageText;
            }
            set
            {
                this._previousPageText = value;
            }
        }

        /// <summary>
        /// Current question numbering state
        /// </summary>
        protected int QuestionNumber
        {
            get
            {
                if (this.ViewState["QuestionNumber"] != null)
                {
                    return int.Parse(this.ViewState["QuestionNumber"].ToString());
                }
                return 0;
            }
            set
            {
                this.ViewState["QuestionNumber"] = value;
            }
        }

        /// <summary>
        /// Sets the style for the question
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null)]
        public Style QuestionStyle
        {
            get
            {
                if (this._questionStyle != null)
                {
                    return this._questionStyle;
                }
                return new Style();
            }
            set
            {
                this._questionStyle = value;
            }
        }

        /// <summary>
        /// Text of the submit vote button
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Text"), Bindable(true), PersistenceMode(PersistenceMode.Attribute), DefaultValue("*")]
        public string QuestionValidationMark
        {
            get
            {
                return this._questionValidationMark;
            }
            set
            {
                this._questionValidationMark = value;
            }
        }

        /// <summary>
        /// Style of the validation mark of a question
        /// </summary>
        [DefaultValue((string)null), Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style QuestionValidationMarkStyle
        {
            get
            {
                if (this._questionValidationMarkStyle != null)
                {
                    return this._questionValidationMarkStyle;
                }
                return new Style();
            }
            set
            {
                this._questionValidationMarkStyle = value;
            }
        }

        /// <summary>
        /// Style of the validation error messages of a question
        /// </summary>
        [Category("Styles"), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public Style QuestionValidationMessageStyle
        {
            get
            {
                if (this._questionValidationMessageStyle != null)
                {
                    return this._questionValidationMessageStyle;
                }
                return new Style();
            }
            set
            {
                this._questionValidationMessageStyle = value;
            }
        }

        /// <summary>
        /// Text of the resume progress button
        /// </summary>
        [DefaultValue("Resume"), Bindable(true), Category("Text")]
        public string ResumeProgressText
        {
            get
            {
                return this._resumeProgressText;
            }
            set
            {
                this._resumeProgressText = value;
            }
        }

        /// <summary>
        /// Text of the save progress button
        /// </summary>
        [Category("Text"), DefaultValue("Save progress"), Bindable(true)]
        public string SaveProgressText
        {
            get
            {
                return this._saveProgressText;
            }
            set
            {
                this._saveProgressText = value;
            }
        }

        /// <summary>
        /// Sets the style for the section's answer grid overview items 
        /// </summary>
        [DefaultValue((string)null), PersistenceMode(PersistenceMode.InnerProperty), Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style SectionGridAnswersAlternatingItemStyle
        {
            get
            {
                if (this._sectionGridAnswersAlternatingItemStyle != null)
                {
                    return this._sectionGridAnswersAlternatingItemStyle;
                }
                return new Style();
            }
            set
            {
                this._sectionGridAnswersAlternatingItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the section's answer grid header
        /// </summary>
        [Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style SectionGridAnswersHeaderStyle
        {
            get
            {
                if (this._sectionGridAnswersHeaderStyle != null)
                {
                    return this._sectionGridAnswersHeaderStyle;
                }
                return new Style();
            }
            set
            {
                this._sectionGridAnswersHeaderStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the section's answer grid overview items 
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty), Category("Styles"), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style SectionGridAnswersItemStyle
        {
            get
            {
                if (this._sectionGridAnswersItemStyle != null)
                {
                    return this._sectionGridAnswersItemStyle;
                }
                return new Style();
            }
            set
            {
                this._sectionGridAnswersItemStyle = value;
            }
        }

        /// <summary>
        /// Sets the style for the section's answer grid table 
        /// </summary>
        [Category("Styles"), DefaultValue((string)null), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style SectionGridAnswersStyle
        {
            get
            {
                if (this._sectionGridAnswersStyle != null)
                {
                    return this._sectionGridAnswersStyle;
                }
                return new Style();
            }
            set
            {
                this._sectionGridAnswersStyle = value;
            }
        }

        /// <summary>
        /// Style used for the question's mark
        /// </summary>
        [Category("Styles"), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty)]
        public Style SectionOptionStyle
        {
            get
            {
                if (this._sectionOptionStyle != null)
                {
                    return this._sectionOptionStyle;
                }
                return (this._sectionOptionStyle = new Style());
            }
            set
            {
                this._sectionOptionStyle = value;
            }
        }

        /// <summary>
        /// Show only percents and not vote details
        /// </summary>
        [Category("General"), Bindable(true), DefaultValue("false")]
        public bool ShowOnlyPercent
        {
            get
            {
                return this._showOnlyPercent;
            }
            set
            {
                this._showOnlyPercent = value;
            }
        }

        /// <summary>
        /// Id of the survey you wish to render
        /// and gets or sets it to the viewstate
        /// </summary>
        [Category("General"), DefaultValue("-1"), PersistenceMode(PersistenceMode.Attribute), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public int SurveyId
        {
            get
            {
                if (!this._isDesignTime)
                {
                    return (int)this.ViewState["SurveyBox:SurveyId"];
                }
                return this._surveyId;
            }
            set
            {
                if (!this._isDesignTime)
                {
                    if (value == 0)
                    {
                        SurveyData activatedSurvey = new Surveys().GetActivatedSurvey();
                        if (activatedSurvey != null)
                        {
                            this.ViewState["SurveyBox:SurveyId"] = activatedSurvey.Surveys[0].SurveyId;
                        }
                        else
                        {
                            this.ViewState["SurveyBox:SurveyId"] = -1;
                        }
                    }
                    else
                    {
                        this.ViewState["SurveyBox:SurveyId"] = value;
                    }
                }
                else
                {
                    this._surveyId = value;
                }
            }
        }

        /// <summary>
        /// Total of questions pages in the survey
        /// and adds it to the viewstate
        /// </summary>
        public int TotalPageNumber
        {
            get
            {
                if (this.ViewState["TotalPageNumber"] != null)
                {
                    return int.Parse(this.ViewState["TotalPageNumber"].ToString());
                }
                return 0;
            }
            set
            {
                this.ViewState["TotalPageNumber"] = value;
            }
        }

        /// <summary>
        /// Contains all the answers to all
        /// questions given by the current voter
        /// and adds the VoterAnswersData Dataset to the viewstate
        /// </summary>
        public VoterAnswersData VoterAnswers
        {
            get
            {
                if (this.ViewState["VoterAnswers"] == null)
                {
                    this.ViewState["VoterAnswers"] = new VoterAnswersData();
                }
                return (VoterAnswersData)this.ViewState["VoterAnswers"];
            }
            set
            {
                this.ViewState["VoterAnswers"] = value;
            }
        }

    }
}

