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
    public class IPRangeSecurityAddIn : IWebSecurityAddIn
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
            // CheckBox child = new CheckBox();
            //  child.Checked = new Surveys().IsSurveyOnlyInvited(this.SurveyId);
            Label label = new Label();
            label.ControlStyle.Font.Bold = true;
            label.Text = ResourceManager.GetString("IPRangeSecurityInEffect", this.LanguageCode);
            cell.Controls.Add(label);
            row.Cells.Add(cell);
            cell = new TableCell();
            // child.CheckedChanged += new EventHandler(this.OnCheckBoxChange);
            //  child.AutoPostBack = true;
            //  cell.Controls.Add(child);
            row.Cells.Add(cell);
            this._adminTable.Rows.Add(row);
            return this._adminTable;
        }
        private string GetErrorMessage(string msgname)
        {
            return "<div style=\"Color:Red;\" >" + msgname + @"</div>";
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
            new Survey().UpdateOnlyInvited(this.SurveyId, true);
        }

        /// <summary>
        /// Check if the current user has given
        /// the correct credentials
        /// </summary>
        public bool IsAuthenticated()
        {
            return IsValidIP();
        }

        private bool IsValidIP()
        {
            SurveyIPRangeData ranges = new SurveyIPRange().GetAllSurveyIPranges(this._surveyId);
            if (ranges.SurveyIPRange.Count == 0) return true;
            // long curreneIP = Ip4ToInt(HttpContext.Current.Request.UserHostAddress);
            long curreneIP = Ip4ToInt((HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            foreach (SurveyIPRangeData.SurveyIPRangeRow row in ranges.SurveyIPRange)
                if (curreneIP >= Ip4ToInt(row.IPStart) && curreneIP <= Ip4ToInt(row.IPEnd)) return true;
            return false;
        }

        long Ip4ToInt(string ip)
        {
            string[] parts = ip.Split('.');


            System.Net.IPAddress oIP = System.Net.IPAddress.Parse(ip);
            byte[] byteIP = oIP.GetAddressBytes();


            uint retval = (uint)byteIP[0] << 24;
            retval += (uint)byteIP[1] << 16;
            retval += (uint)byteIP[2] << 8;
            retval += (uint)byteIP[3];
            return (long)retval;
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

