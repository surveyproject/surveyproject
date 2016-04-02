namespace Votations.NSurvey.Security
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using System.Text.RegularExpressions;
    
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Protects the survey access through a password
    /// </summary>
    public class PasswordWebSecurityAddIn : IWebSecurityAddIn
    {
        private int _addInDbId;
        private string _description;
        private bool _disabled;
        private string _languageCode;
        private int _order;
        private Table _passwordTable = new Table();
        private TextBox _passwordTextBox;
        private int _surveyId;
        private StateBag _viewState;

        private Table _table = new Table();


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
            //Table table = new Table();
            this._table.ControlStyle.CopyFrom(controlStyle); //CSS questionBuilder
            this._table.Width = Unit.Percentage(100);

            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            // row 1. - 2 cells
            row = new TableRow();
            this._table.Rows.Add(row);     

            cell = new TableCell();
            row.Cells.Add(cell);

            Label label = new Label();
            label.Width = Unit.Pixel(125);
            label.ControlStyle.Font.Bold = true;
            label.Text = ResourceManager.GetString("PasswordLabel");
            cell.Controls.Add(label);

            cell = new TableCell();
            row.Cells.Add(cell);      

            this._passwordTextBox = new TextBox();
            this._passwordTextBox.Width = Unit.Pixel(150);
            //this._passwordTextBox.Attributes.Add("value", new Surveys().GetSurveyPassword(this.SurveyId));
            this._passwordTextBox.Attributes.Add("value", "password set");
            this._passwordTextBox.Attributes.Add("MaxLength", "12"); 
            this._passwordTextBox.TextMode = TextBoxMode.Password;

            cell.Controls.Add(this._passwordTextBox);

            
            // row 2. - 2 cells
            //row = new TableRow();
            //this._table.Rows.Add(row);

            cell = new TableCell();
            //cell.ColumnSpan = 2;
            row.Cells.Add(cell);

            Button child = new Button();
            child.Text = ResourceManager.GetString("ApplyChangesButton", this.LanguageCode);
            child.CssClass = "btn btn-primary btn-xs bw";
            child.Click += new EventHandler(this.OnUpdateClick);
            cell.Controls.Add(child);

            //cell = new TableCell();
            //row.Cells.Add(cell);

            Label errorlabel = new Label();
            errorlabel.Width = Unit.Pixel(125);
            errorlabel.ControlStyle.Font.Bold = true;
            errorlabel.Text = " ";
            cell.Controls.Add(errorlabel);


            return this._table;
        }

        /// <summary>
        /// Must create and return the control 
        /// that will show the logon interface.
        /// If none is available returns null
        /// </summary>
        public Control GetLoginInterface(Style controlStyle)
        {
            TableCell cell = new TableCell();
            TableRow row = new TableRow();
            Button child = new Button { CssClass = "btn btn-primary btn-xs bw" };
            Panel panel = new Panel { CssClass = "passwordMessage" };
            //TODO JJ 
            this._passwordTextBox = new TextBox();
            this._passwordTable.ControlStyle.CssClass = "tablelddl ";
            //this._passwordTable.Width = Unit.Percentage(100.0);
            child.Text = ResourceManager.GetString("SubmitPassword", this.LanguageCode);
            child.Click += new EventHandler(this.OnValidatePassword);
            this._passwordTextBox.TextMode = TextBoxMode.Password;
            this._passwordTextBox.CssClass = "lddl";

            panel.Controls.Add(new LiteralControl(ResourceManager.GetString("EnterPasswordMessage", this.LanguageCode)));
            //cell.Controls.Add(new LiteralControl(ResourceManager.GetString("EnterPasswordMessage", this.LanguageCode)));
            cell.Controls.Add(panel);
            row.Cells.Add(cell);
            //this._passwordTable.Rows.Add(row);
            //cell = new TableCell();
            //row = new TableRow();
            panel = new Panel { CssClass = "passwordTextBox" };
            panel.Controls.Add(this._passwordTextBox);
            cell.Controls.Add(panel);

            panel = new Panel { CssClass = "passwordSubmitButton" };
            panel.Controls.Add(child);

            cell.Controls.Add(panel);
            row.Cells.Add(cell);
            this._passwordTable.Rows.Add(row);

            Panel wrapper = new Panel { CssClass = "questionContainerWrapper" };
            Panel subpanel = new Panel { CssClass = "questionContainer" };
            wrapper.Controls.Add(subpanel);
            subpanel.Controls.Add(this._passwordTable);

            return wrapper;
        }

        /// <summary>
        /// Method called once an addin has been added to a survey
        /// Can be used to set default values, settings for the addin
        /// </summary>
        public void InitOnSurveyAddition()
        {
            new Survey().UpdateAccessPassword(this.SurveyId, "");
        }

        /// <summary>
        /// Check if the current user has given
        /// the correct credentials
        /// </summary>
        public bool IsAuthenticated()
        {
            return this.IsPasswordValid;
        }

        /// <summary>
        /// Encrypts the surveys's password
        /// </summary>
        /// <param name="password">password</param>
        /// <returns>encrypted password</returns>
        public string EncryptSurveyPassword(string password)
        {
            //return FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");

            //string salt = GenerateSalt();

            //start hash creation:
            System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();

            // password string to bytes
            byte[] sha256Bytes = System.Text.Encoding.UTF8.GetBytes(password);

            //password bytes to hash
            byte[] cryString = sha256.ComputeHash(sha256Bytes);

            // start final encrypted password
            string sha256Str = string.Empty;

            // create final encrypted password: bytes to hex string
            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString("X");
            }

            // concatenate hashed password + salt
            // sha256Str = sha256Str + salt;

            return sha256Str;
        }

        protected virtual void OnUpdateClick(object sender, EventArgs e)
        {
            // check password format
            if (Regex.IsMatch(this._passwordTextBox.Text, @"(?=^.{8,12}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"))
            {

             new Survey().UpdateAccessPassword(this.SurveyId, EncryptSurveyPassword(this._passwordTextBox.Text));
             this._passwordTextBox.Attributes.Add("value", "password set");   

            }
            else
            {
                TableCell cell = new TableCell();
                TableRow row = new TableRow();
                cell.Text = GetErrorMessage(ResourceManager.GetString("PasswordRequiredMessage", this.LanguageCode));
                cell.ColumnSpan = 2;
                row.Cells.Add(cell);
                this._table.Rows.AddAt(0, row);
            }
        }

        private string GetErrorMessage(string msgname)
        {
            return "<span class='ErrorMessage' >" + msgname + @"</span>";
        }

        protected virtual void OnValidatePassword(object sender, EventArgs e)
        {
            if (new Surveys().IsSurveyPasswordValid(this.SurveyId, EncryptSurveyPassword(this._passwordTextBox.Text)))
            {
                this.IsPasswordValid = true;
                if (this.UserAuthenticated != null)
                {
                    this.UserAuthenticated(this, null);
                }
            }
            else
            {
                TableCell cell = new TableCell();
                TableRow row = new TableRow();
                cell.Text = GetErrorMessage( ResourceManager.GetString("InvalidPasswordMessage", this.LanguageCode));
                cell.ColumnSpan = 2;
                row.Cells.Add(cell);
                this._passwordTable.Rows.AddAt(0, row);
            }
        }

        /// <summary>
        /// Method to handle voter data once it has been stored in the database
        /// </summary>
        /// <param name="voter">Voter information as saved in the database and its answers</param>
        public void ProcessVoterData(VoterAnswersData voter)
        {
        }

        /// <summary>
        /// Method called once an addin has been remove from a survey
        /// Can be used to remove useless settings for the addin
        /// </summary>
        public void UnInitOnSurveyRemoval()
        {
            new Survey().UpdateAccessPassword(this.SurveyId, "");
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
        /// Return the current UId of the visitor
        /// </summary>
        protected virtual bool IsPasswordValid
        {
            get
            {
                if (this.ViewState == null)
                {
                    return false;
                }
                return ((this.ViewState["tbPasswordValid"] != null) && ((bool) this.ViewState["tbPasswordValid"]));
            }
            set
            {
                if (this.ViewState != null)
                {
                    this.ViewState["tbPasswordValid"] = value;
                }
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

