namespace Votations.NSurvey.Security
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Add in that handles the ASP.net security 
    /// context (forms, windows, custom ...) to detect if a 
    /// user is logged in and retrieve the username from the aspnet context
    /// </summary>
    public class ASPNetContextSecurityAddIn : IWebSecurityAddIn
    {
        private int _addInDbId;
        private Table _adminTable;
        private string _description;
        private bool _disabled;
        private string _languageCode;
        private int _order;
        private int _surveyId;
        private StateBag _viewState;

        public event UserAuthenticatedEventHandler UserAuthenticated;

        /// <summary>
        /// Can return keys/values of the custom 
        /// stored data during the ProcessVoterData. 
        /// At this time these data are retrieved to
        /// be shown in individual reports and for results export
        /// </summary>
        public NameValueCollection GetAddInVoterData(int voterId)
        {
            return null;
        }

        /// <summary>
        /// Must create and return the control 
        /// that will show the administration interface
        /// If none is available returns null
        /// </summary>
        public Control GetAdministrationInterface(Style controlStyle)
        {
            this._adminTable = new Table();
            this._adminTable.ControlStyle.CopyFrom(controlStyle);
            this._adminTable.Width = Unit.Percentage(100);
            TableCell cell = new TableCell();
            TableRow row = new TableRow();
            cell.ColumnSpan = 2;
            cell.Text = ResourceManager.GetString("ASPNETSecurityAddinDescription", this.LanguageCode);
            row.Cells.Add(cell);
            this._adminTable.Rows.Add(row);
            cell = new TableCell();
            row = new TableRow();
            CheckBox child = new CheckBox();
            child.Checked = new Surveys().AspSecurityAllowsMultipleSubmissions(this.SurveyId);
            Label label = new Label();
            label.ControlStyle.Font.Bold = true;
            label.Text = ResourceManager.GetString("MultipleSubmissionsLabel", this.LanguageCode);
            cell.Width = Unit.Percentage(50);
            cell.Controls.Add(label);
            row.Cells.Add(cell);
            cell = new TableCell();
            child.CheckedChanged += new EventHandler(this.OnCheckBoxChange);
            child.AutoPostBack = true;
            cell.Controls.Add(child);
            cell.Width = Unit.Percentage(50);
            row.Cells.Add(cell);
            this._adminTable.Rows.Add(row);
            return this._adminTable;
        }

        /// <summary>
        /// Must create and return the control 
        /// that will show the logon interface.
        /// If none is available returns null
        /// </summary>
        public Control GetLoginInterface(Style controlStyle)
        {
            return null;
        }

        /// <summary>
        /// Method called once an addin has been added to a survey
        /// Can be used to set default values, settings for the addin
        /// </summary>
        public void InitOnSurveyAddition()
        {
        }

        /// <summary>
        /// Check if the current user has given
        /// the correct credentials
        /// </summary>
        public bool IsAuthenticated()
        {
            bool flag = false;
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return flag;
            }
            if (((HttpContext.Current.User.Identity.Name.Length <= 0) || new Voters().HasUserNameVoted(this.SurveyId, HttpContext.Current.User.Identity.Name)) && !new Surveys().AspSecurityAllowsMultipleSubmissions(this.SurveyId))
            {
                return flag;
            }
            return true;
        }

        protected virtual void OnCheckBoxChange(object sender, EventArgs e)
        {
            new Survey().UpdateAspSecuritySettings(this.SurveyId, ((CheckBox) sender).Checked);
        }

        /// <summary>
        /// Method to handle voter data once it has been stored in the database
        /// </summary>
        /// <param name="voter">Voter information as saved in the database and its answers</param>
        public void ProcessVoterData(VoterAnswersData voter)
        {
            voter.Voters[0].ContextUserName = HttpContext.Current.User.Identity.Name;
            new Voter().UpdateVoterUserName(voter.Voters[0].VoterId, HttpContext.Current.User.Identity.Name);
        }

        /// <summary>
        /// Method called once an addin has been remove from a survey
        /// Can be used to remove useless settings for the addin
        /// </summary>
        public void UnInitOnSurveyRemoval()
        {
        }

        public int AddInDbId
        {
            get
            {
                return this._addInDbId;
            }
            set
            {
                this._addInDbId = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public bool Disabled
        {
            get
            {
                return this._disabled;
            }
            set
            {
                this._disabled = value;
            }
        }

        /// <summary>
        /// Contains the current user language
        /// choice in a multi-language survey
        /// </summary>
        public string LanguageCode
        {
            get
            {
                return this._languageCode;
            }
            set
            {
                this._languageCode = value;
            }
        }

        public int Order
        {
            get
            {
                return this._order;
            }
            set
            {
                this._order = value;
            }
        }

        public int SurveyId
        {
            get
            {
                return this._surveyId;
            }
            set
            {
                this._surveyId = value;
            }
        }

        public StateBag ViewState
        {
            get
            {
                return this._viewState;
            }
            set
            {
                this._viewState = value;
            }
        }
    }
}

