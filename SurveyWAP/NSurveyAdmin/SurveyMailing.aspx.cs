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
using System.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Emailing;
using Votations.NSurvey.Web;
using Votations.NSurvey.Web.Security;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Build message and send it to the given email	list
    /// </summary>
    public partial class SurveyMailing : PageBase
    {
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.Label CreationDateLabel;
        protected System.Web.UI.WebControls.Label LastEntryDateLabel;
        new protected HeaderControl Header;
        protected System.Web.UI.WebControls.Button ResetVotesButton;
        protected System.Web.UI.WebControls.Calendar StatsCalendar;
        protected System.Web.UI.WebControls.CheckBox AnonymousEntriesCheckBox;
        protected System.Web.UI.WebControls.Button SendInvitationButton;
        protected System.Web.UI.WebControls.Label PendingEmailsLabel;
        protected System.Web.UI.WebControls.Label AnsweredEmailsLabel;
        protected System.Web.UI.WebControls.TextBox MailingListTextBox;
//        protected FreeTextBoxControls.FreeTextBox InvitationMessageTextBox;
        //		protected System.Web.UI.WebControls.TextBox InvitationMessageTextBox;
        protected System.Web.UI.WebControls.TextBox FromTextBox;
        protected System.Web.UI.WebControls.TextBox SubjectTextBox;
        protected System.Web.UI.WebControls.TextBox FromNameTextbox;
        protected System.Web.UI.WebControls.Literal InvitationMailingTitle;
        protected System.Web.UI.WebControls.Label FromLabel;
        protected System.Web.UI.WebControls.Label FromNameLabel;
        protected System.Web.UI.WebControls.Label SubjectLabel;
        protected System.Web.UI.WebControls.Label InvitationMessageLabel;
        protected System.Web.UI.WebControls.Label EmailInvitationLabel;
        protected System.Web.UI.WebControls.Label AnonymousLabel;
        protected SurveyListControl SurveyList;

        private void Page_Load(object sender, System.EventArgs e)
        {
            UITabList.SetCampaignTabs((MsterPageTabs)Page.Master, UITabList.CampaignTabs.Mailing);
            SetupSecurity();
            LocalizePage();
          
            if (!Page.IsPostBack)
            {

                // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
                FillFields();
            }

            MailingCKEditor.config.enterMode = CKEditor.NET.EnterMode.BR;
            MailingCKEditor.config.skin = "moonocolor";

            MailingCKEditor.config.toolbar = new object[]
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
            CheckRight(NSurveyRights.AccessMailing, true);
        }

        private void LocalizePage()
        {
            InvitationMailingTitle.Text = GetPageResource("InvitationMailingTitle");
            FromLabel.Text = GetPageResource("FromLabelMail");
            FromNameLabel.Text = GetPageResource("FromNameLabel");
            SubjectLabel.Text = GetPageResource("SubjectLabel");
            if (!Page.IsPostBack)
            {
                SubjectTextBox.Text = GetPageResource("SubjectTextBox");
                FromNameTextbox.Text = GetPageResource("FromNameTextBox");
                if (NSurveyUser.Identity is NSurveyFormIdentity)
                {
                    FromNameTextbox.Text =
                        ((NSurveyFormIdentity)NSurveyUser.Identity).FirstName + " " +
                        ((NSurveyFormIdentity)NSurveyUser.Identity).LastName;
                    FromTextBox.Text = ((NSurveyFormIdentity)NSurveyUser.Identity).Email;
                }
            }
          //  ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvitationMessageLabel"));
            InvitationMessageLabel.Text = GetPageResource("InvitationMessageLabel");
            EmailInvitationLabel.Text = GetPageResource("EmailInvitationLabel");
            AnonymousLabel.Text = GetPageResource("AnonymousLabel");
            SendInvitationButton.Text = GetPageResource("SendInvitationButton");
        }

        /// <summary>
        /// Bind the fields
        /// </summary>
        private void FillFields()
        {
            string surveyLink;
            string firstNamelastname = GetPageResource("FromNameTextBox");
            var survey = new Surveys().GetSurveyById(SurveyId, null).Surveys[0];

            if (Request.ServerVariables["SERVER_PORT"] != null && Request.ServerVariables["SERVER_PORT"] != "80")
            {
                if (HttpContext.Current.Request.ApplicationPath != "/")
                {
                    surveyLink = string.Format("http://{0}:{1}{2}/surveymobile.aspx?surveyid={3}&uid=[--invitationid-]",
                        Request.ServerVariables["SERVER_NAME"], Request.ServerVariables["SERVER_PORT"], HttpContext.Current.Request.ApplicationPath, survey.SurveyGuid);
                }
                else
                {
                    surveyLink = string.Format("http://{0}:{1}/surveymobile.aspx?surveyid={2}&uid=[--invitationid-]",
                        Request.ServerVariables["SERVER_NAME"], Request.ServerVariables["SERVER_PORT"], survey.SurveyGuid);
                }
            }
            else
            {
                if (HttpContext.Current.Request.ApplicationPath != "/")
                {
                    surveyLink = string.Format("http://{0}{1}/surveymobile.aspx?surveyid={2}&uid=[--invitationid-]",
                        Request.ServerVariables["SERVER_NAME"], HttpContext.Current.Request.ApplicationPath, survey.SurveyGuid);
                }
                else
                {
                    surveyLink = string.Format("http://{0}/surveymobile.aspx?surveyid={1}&uid=[--invitationid-]",
                        Request.ServerVariables["SERVER_NAME"], survey.SurveyGuid);
                }
            }

            if (NSurveyUser.Identity is NSurveyFormIdentity)
            {
                firstNamelastname =
                    ((NSurveyFormIdentity)NSurveyUser.Identity).FirstName + " " + ((NSurveyFormIdentity)NSurveyUser.Identity).LastName;
                
            }

            MailingCKEditor.Text =
                string.Format(GetPageResource("InvitationMessageTextBox"), survey.Title, surveyLink, firstNamelastname);
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
            this.SendInvitationButton.Click += new System.EventHandler(this.SendInvitationButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void SendInvitationButton_Click(object sender, System.EventArgs e)
        {
            //Init Async call
            ASyncMailing mail = new ASyncMailing();
            ASyncMailingState mailingState = new ASyncMailingState(mail, Session.SessionID);
            AsyncCallback cb = new AsyncCallback(MailingCallback);

            // Build message to send
            EmailingMessage message;
            message = new EmailingMessage();
            message.FromEmail = FromTextBox.Text;
            message.FromName = FromNameTextbox.Text;
            message.Subject = SubjectTextBox.Text;
            message.Body = HttpUtility.HtmlDecode(MailingCKEditor.Text);
            message.Format = EmailFormat.Html;

            // Dummy session variable to init the session in case
            // it was not initialized
            Session["IamADummy"] = "StartIt";

            // Start the mailing
            mail.BeginMailIt(Session.SessionID, SurveyId,
                AnonymousEntriesCheckBox.Checked, MailingListTextBox.Text,
                message, cb, mailingState);

            // Open the poll page while the mailing continues
            UINavigator.NavigateToMailingPoll(SurveyId, MenuIndex);
        }

        public void MailingCallback(IAsyncResult ar)
        {
            // Restore orginial state
            ASyncMailingState mailingState = (ASyncMailingState)ar.AsyncState;

            //retrieve the object on which End needs to be called
            ASyncMailing mailing = mailingState.AsyncMailing;
            mailing.EndMailIt(ar);

            // Let the world know that mailing is finished
            SyncDataStore.SetRecords(mailingState.SessionId, true);
        }
    }
}
