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
    public class EmailWebSecurityAddIn : IWebSecurityAddIn
    {
        private int _addInDbId;
        private Table _adminTable;
        private string _description;
        private bool _disabled;
        private string _languageCode;
        private int _order;
        private int _surveyId;
        private Table _uIdTable;
        private TextBox _uIdTextBox;
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
            CheckBox child = new CheckBox();
            child.Checked = new Surveys().IsSurveyOnlyInvited(this.SurveyId);
            Label label = new Label();
            label.ControlStyle.Font.Bold = true;
            label.Text = ResourceManager.GetString("OnlyInvitedEmailsLabel", this.LanguageCode);
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
            this._uIdTable = new Table();
            TableCell cell = new TableCell();
            TableRow row = new TableRow();
            Button child = new Button();
            child.Click += new EventHandler(this.OnUIdSubmit);

            this._uIdTextBox = new TextBox();
            this._uIdTextBox.CssClass = "lddl";
            //this._uIdTable.ControlStyle.Font.CopyFrom(controlStyle.Font);
            this._uIdTable.ControlStyle.CssClass = "tablelddl ";
            this._uIdTable.Width = Unit.Percentage(100.0);
            child.Text = ResourceManager.GetString("SubmitUId");
            child.CssClass = "btn btn-primary btn-xs bw";

            cell.Controls.Add(new LiteralControl(ResourceManager.GetString("EnterUIdMessage", this.LanguageCode)));
            row.Cells.Add(cell);
            this._uIdTable.Rows.Add(row);
            cell = new TableCell();
            row = new TableRow();
            cell.Controls.Add(this._uIdTextBox);
            cell.Controls.Add(child);
            row.Cells.Add(cell);
            this._uIdTable.Rows.Add(row);
            return this._uIdTable;
        }

        /// <summary>
        /// Method to check if the user has the right 
        /// to post answers to the form. This method
        /// is called before the voter has been stored
        /// in the database
        /// </summary>
        public bool hasAnswered()
        {
            return ((this.VisitorUId != null) && (new Voter().IsUIdValid(this.VisitorUId) == this.SurveyId));
        }

        /// <summary>
        /// Method called once an addin has been added to a survey
        /// Can be used to set default values, settings for the addin
        /// </summary>
        public void InitOnSurveyAddition()
        {
            new Survey().UpdateOnlyInvited(this.SurveyId, true);
        }

        /// <summary>
        /// Check if the current user has given
        /// the correct credentials
        /// </summary>
        public bool IsAuthenticated()
        {
            bool flag = (this.VisitorUId != null) && (new Voter().IsUIdValid(this.VisitorUId) == this.SurveyId);
            return ((!new Surveys().IsSurveyOnlyInvited(this.SurveyId) && (flag || (this.VisitorUId == null))) || (flag && !new Voters().CheckIfVoterUIdExists(this.SurveyId, this.VisitorUId)));
        }

        protected virtual void OnCheckBoxChange(object sender, EventArgs e)
        {
            new Survey().UpdateOnlyInvited(this.SurveyId, ((CheckBox) sender).Checked);
        }
        private string GetErrorMessage(string msgname)
        {
            return "<div class='ErrorMessage icon-warning-sign' >" + msgname + @"</div>";
        }
        protected virtual void OnUIdSubmit(object sender, EventArgs e)
        {
            if ((this._uIdTextBox.Text.Length > 0) && (new Voter().IsUIdValid(this._uIdTextBox.Text) == this.SurveyId))
            {
                this.VisitorUId = this._uIdTextBox.Text;
                if (this.UserAuthenticated != null)
                {
                    this.UserAuthenticated(this, null);
                }
            }
            else
            {
                TableCell cell = new TableCell();
                TableRow row = new TableRow();
                cell.Text =GetErrorMessage( ResourceManager.GetString("InvalidUIdMessage", this.LanguageCode));
                row.Cells.Add(cell);
                this._uIdTable.Rows.AddAt(0, row);
            }
        }

        /// <summary>
        /// Method to handle voter data once it has been stored in the database
        /// </summary>
        /// <param name="voter">Voter information as saved in the database and its answers</param>
        public void ProcessVoterData(VoterAnswersData voter)
        {
            if ((this.VisitorUId != null) && (new Voter().IsUIdValid(this.VisitorUId) == this.SurveyId))
            {
                new Voter().SetVoterUId(voter.Voters[0].VoterId, this.VisitorUId);
            }
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
        protected virtual string VisitorUId
        {
            get
            {
                if (this.ViewState == null)
                {
                    return null;
                }
                if (this.ViewState["UId"] != null)
                {
                    return this.ViewState["UId"].ToString();
                }
                return HttpContext.Current.Request["UId"];
            }
            set
            {
                if (this.ViewState != null)
                {
                    this.ViewState["UId"] = value;
                }
            }
        }
    }
}

