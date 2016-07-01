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
    /// Add in that handles the email security 
    /// A user will need to have a valid authentication
    /// code in order to access the survey
    /// </summary>
    public class TokenSecurityAddIn : IWebSecurityAddIn
    {
        private int _addInDbId;
        private Table _adminTable;
        private string _description;
        private bool _disabled;
        private string _languageCode;
        private int _order;
        private int _surveyId;
        private Table _tokenTable;
        private TextBox _tokenTextBox;
        private StateBag _viewState;
        private bool _isAuthenticated =false;

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
            CheckBox child = new CheckBox();
            child.Checked = new Surveys().IsSurveySaveTokenUserData(this.SurveyId);
            Label label = new Label();
            label.ControlStyle.Font.Bold = true;
            label.Text = ResourceManager.GetString("SaveVoterDataLabel", this.LanguageCode);
            cell.Controls.Add(label);
            row.Cells.Add(cell);
            cell = new TableCell();
            child.CheckedChanged += new EventHandler(this.OnCheckBoxChange);
            child.AutoPostBack = true;
            cell.Controls.Add(child);
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
            this._tokenTable = new Table();
            _tokenTable.CssClass = CssXmlManager.GetString("EnterTokenTable");
            _tokenTable.ID = "etTbl";

            TableCell cell = new TableCell();
            TableRow row = new TableRow();

            Button child = new Button();
            child.CssClass = CssXmlManager.GetString("EnterTokenButton");
            child.ID = "etBtn";
            child.Click += new EventHandler(this.OnTokenSubmit);

            this._tokenTextBox = new TextBox();
            _tokenTextBox.ID = "etTBI";
            _tokenTextBox.CssClass = CssXmlManager.GetString("EnterTokenTextBoxInsert");

            //this._tokenTable.ControlStyle.Font.CopyFrom(controlStyle.Font);
            //this._tokenTable.Width = Unit.Percentage(100.0);
            child.Text = ResourceManager.GetString("SubmitToken");

            cell.CssClass = CssXmlManager.GetString("EnterTokenMessage");
            cell.ID = "etMsg";
            cell.Controls.Add(new LiteralControl(ResourceManager.GetString("EnterTokenMessage", this.LanguageCode)));

            row.Cells.Add(cell);
            this._tokenTable.Rows.Add(row);

            cell = new TableCell();
            cell.CssClass = CssXmlManager.GetString("EnterTokenTextBox");
            cell.ID = "etTB";

            row = new TableRow();
            cell.Controls.Add(this._tokenTextBox);
            cell.Controls.Add(child);
            row.Cells.Add(cell);
            this._tokenTable.Rows.Add(row);

            return this._tokenTable;
        }

      
        /// <summary>
        /// Method called once an addin has been added to a survey
        /// Can be used to set default values, settings for the addin
        /// </summary>
        public void InitOnSurveyAddition()
        {
            new Survey().UpdateSaveTokenUserDate(this.SurveyId, true);
        }

        /// <summary>
        /// Check if the current user has given
        /// the correct credentials
        /// </summary>
        public bool IsAuthenticated()
        {
            return (this.VisitorToken != null) && (new SurveyToken().ValidateToken(this.SurveyId, this.VisitorToken));
        
        }

        protected virtual void OnCheckBoxChange(object sender, EventArgs e)
        {
            new Survey().UpdateSaveTokenUserDate(this.SurveyId, ((CheckBox) sender).Checked);
        }
        private string GetErrorMessage(string msgname)
        {
            //return "<span style=\"Color:Red;\" >"+msgname+@"</span>" ;
            return "<div id=\"tsEM\" class=\"" + CssXmlManager.GetString("TokensecurityErrorMessage") + "\">" + msgname + @"</div>";
        }
        protected virtual void OnTokenSubmit(object sender, EventArgs e)
        {
           // this.UserAuthenticated(this, null);
            if (this._tokenTextBox.Text.Length > 0 && new SurveyToken().ValidateToken(this.SurveyId,this._tokenTextBox.Text))
            {
                this.VisitorToken = this._tokenTextBox.Text;
                _isAuthenticated = true;
                if (this.UserAuthenticated != null)
                {
                    this.UserAuthenticated(this, null);
                }
            }
            else
            {
                TableCell cell = new TableCell();
                TableRow row = new TableRow();
                cell.Text =GetErrorMessage( ResourceManager.GetString("InvalidTokenMessage", this.LanguageCode));
                row.Cells.Add(cell);
                this._tokenTable.Rows.AddAt(0, row);
            }
        }

        /// <summary>
        /// Method to handle voter data once it has been stored in the database
        /// </summary>
        /// <param name="voter">Voter information as saved in the database and its answers</param>
        public void ProcessVoterData(VoterAnswersData voter)
        {
            if ((this.VisitorToken != null))
                new SurveyToken().UpdateToken(this.SurveyId, this.VisitorToken,
                    new Surveys().IsSurveySaveTokenUserData(this.SurveyId)?-1: voter.Voters[0].VoterId);
        }
        /// <summary>
        /// Method called once an addin has been remove from a survey
        /// Can be used to remove useless settings for the addin
        /// </summary>
        public void UnInitOnSurveyRemoval()
        {
            new Survey().UpdateOnlyInvited(this.SurveyId, false);
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

        /// <summary>
        /// Return the current UId of the visitor
        /// </summary>
        protected virtual string VisitorToken
        {
            get
            {
                if (this.ViewState == null)
                {
                    return null;
                }
                if (this.ViewState["Token"] != null)
                {
                    return this.ViewState["Token"].ToString();
                }
                return HttpContext.Current.Request["Token"];
            }
            set
            {
                if (this.ViewState != null)
                {
                    this.ViewState["Token"] = value;
                }
            }
        }
    }
}

