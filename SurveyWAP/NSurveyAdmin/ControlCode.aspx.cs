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
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Votations.NSurvey;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.WebControls;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Generates the a sample asp.net code
    /// </summary>
    public partial class ControlCode : PageBase
    {
        protected SurveyListControl SurveyList;
        protected System.Web.UI.WebControls.Label MessageLabel;
        new protected HeaderControl Header;
        protected System.Web.UI.WebControls.TextBox tbNetSource;
        protected System.Web.UI.WebControls.HyperLink CodeHyperLink;
        protected System.Web.UI.WebControls.Literal ControlCodeTitle;
        //        protected System.Web.UI.WebControls.Literal FriendlyUrl;
        protected System.Web.UI.WebControls.Literal PageDirectiveInfo;
        protected System.Web.UI.WebControls.Literal TagPrefixInfo;
        protected System.Web.UI.WebControls.Literal QuickLinkInfo;



        private void Page_Load(object sender, System.EventArgs e)
        {
            UITabList.SetCampaignTabs((MsterPageTabs)Page.Master, UITabList.CampaignTabs.Web);
            MessageLabel.Visible = false;
            SetupSecurity();
            LocalizePage();
            ShowFriendlyUrl();
            if (!Page.IsPostBack)
            {

                // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
                var survey = new Surveys().GetSurveyById(SurveyId, null).Surveys[0];
                Guid guid = survey.SurveyGuid;
                if (!survey.IsFriendlyNameNull())
                    txtFriendly.Text = survey.FriendlyName;
                CodeHyperLink.NavigateUrl = GetUrl("?surveyid=" + guid.ToString());
                CodeMobileHyperLink.NavigateUrl = GetMobileUrl("?surveyid=" + guid.ToString());

                CodeHyperLink.Text = CodeHyperLink.NavigateUrl;
                CodeMobileHyperLink.Text = CodeMobileHyperLink.NavigateUrl;
            }
            SetupTextArea();
        }

        private string GetUrl(string urlpart)
        {
            string[] standardPorts = { "80", "443"};
            string conn = "http";
            if (Request.IsSecureConnection) conn = "https";
            if (Request.ServerVariables["SERVER_PORT"] != null && !standardPorts.Contains(Request.ServerVariables["SERVER_PORT"]))
            {
                if (HttpContext.Current.Request.ApplicationPath != "/")
                {
                    return string.Format(conn + "://{0}:{1}{2}/survey.aspx{3}",
                        Request.ServerVariables["SERVER_NAME"], Request.ServerVariables["SERVER_PORT"], HttpContext.Current.Request.ApplicationPath, urlpart);

                }
                else
                {
                    return string.Format(conn + "://{0}:{1}/survey.aspx{2}",
                        Request.ServerVariables["SERVER_NAME"], Request.ServerVariables["SERVER_PORT"], urlpart);
                }
            }
            else
            {
                if (HttpContext.Current.Request.ApplicationPath != "/")
                {
                    return string.Format(conn + "://{0}{1}/survey.aspx{2}",
                        Request.ServerVariables["SERVER_NAME"], HttpContext.Current.Request.ApplicationPath, urlpart);
                }
                else
                {
                    return string.Format(conn + "://{0}/survey.aspx{1}",
                        Request.ServerVariables["SERVER_NAME"], urlpart);
                }
            }
        }

        private string GetMobileUrl(string urlpart)
        {
            string[] standardPorts = { "80", "443" };
            string conn = "http";
            if (Request.IsSecureConnection) conn = "https";
            if (Request.ServerVariables["SERVER_PORT"] != null && !standardPorts.Contains(Request.ServerVariables["SERVER_PORT"]))
            {
                if (HttpContext.Current.Request.ApplicationPath != "/")
                {
                    return string.Format(conn + "://{0}:{1}{2}/surveymobile.aspx{3}",
                        Request.ServerVariables["SERVER_NAME"], Request.ServerVariables["SERVER_PORT"], HttpContext.Current.Request.ApplicationPath, urlpart);

                }
                else
                {
                    return string.Format(conn + "://{0}:{1}/surveymobile.aspx{2}",
                        Request.ServerVariables["SERVER_NAME"], Request.ServerVariables["SERVER_PORT"], urlpart);
                }
            }
            else
            {
                if (HttpContext.Current.Request.ApplicationPath != "/")
                {
                    return string.Format(conn + "://{0}{1}/surveymobile.aspx{2}",
                        Request.ServerVariables["SERVER_NAME"], HttpContext.Current.Request.ApplicationPath, urlpart);
                }
                else
                {
                    return string.Format(conn + "://{0}/surveymobile.aspx{1}",
                        Request.ServerVariables["SERVER_NAME"], urlpart);
                }
            }
        }

        private void ShowFriendlyUrl()
        {
            var survey = new Surveys().GetSurveyById(SurveyId, null).Surveys[0];

            if (!survey.IsFriendlyNameNull())
            {
                friendlyUrlLink.Visible = true;
                friendlyUrlLink.Text = GetUrl("/" + survey.FriendlyName);
                friendlyUrlLink.NavigateUrl = GetUrl("/" + survey.FriendlyName);

                friendlyMobileUrlLink.Visible = true;
                friendlyMobileUrlLink.Text = GetMobileUrl("/" + survey.FriendlyName);
                friendlyMobileUrlLink.NavigateUrl = GetMobileUrl("/" + survey.FriendlyName);


            }
            else
            {
                friendlyUrlLink.Visible = false;
                friendlyMobileUrlLink.Visible = false;
            }
        }


        void SetupTextArea()
        {
            string html = @"
<head runat=""server"">
    <link href=""{0}"" type=""text/css"" rel=""stylesheet"" />
</head>
<body>
    <form id=""form1"" runat=""server"">
    <div>
<table width=""100%"">
        <tr>
            <td>
                {1}
            </td>
        </tr>
    </table>
{2}
 <table width=""100%"">
        <tr>
            <td>
                {3}
            </td>
        </tr>
    </table>
</div>
    </form>
</body>
   ";

            SurveyLayoutData _userSettings;

            Votations.NSurvey.SQLServerDAL.SurveyLayout u = new Votations.NSurvey.SQLServerDAL.SurveyLayout();
            _userSettings = u.SurveyLayoutGet(SurveyId);
            if (!(_userSettings == null || _userSettings.SurveyLayout.Count == 0))
            {
                string css = string.Empty;
                if (!string.IsNullOrEmpty(_userSettings.SurveyLayout[0].SurveyCss))
                    css = ResolveUrl(Votations.NSurvey.Constants.UserSettingsConstants.CssStoragePath + "/" + _userSettings.SurveyLayout[0].SurveyCss);

                string header = HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyHeaderText);
                string footer = HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyFooterText);
                string str = string.Format(taCode.InnerText, SurveyId);
                taCode.InnerText = string.Format(html, css, header, str, footer);
            }

        }
        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.AccessASPNetCode, true);
        }

        private void LocalizePage()
        {
            ControlCodeTitle.Text = GetPageResource("ControlCodeTitle");
            SurveyUrl.Text = GetPageResource("SurveyUrl");
            PageDirectiveInfo.Text = GetPageResource("PageDirectiveInfo");
            TagPrefixInfo.Text = Server.HtmlEncode(GetPageResource("TagPrefixInfo"));
            QuickLinkInfo.Text = GetPageResource("QuickLinkInfo");

            friendlyLabel.Text = GetPageResource("FriendlyLabel");
            btnFriendly.Text = GetPageResource("BtnFriendly");
            FriendlyUrl.Text = GetPageResource("FriendlyUrl");
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
            this.Load += new System.EventHandler(this.Page_Load);
            btnFriendly.Click += new EventHandler(btnFriendly_Click);

        }

        void btnFriendly_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFriendly.Text)) return;
            new Surveys().SetFriendlyName(SurveyId, txtFriendly.Text);
            ShowFriendlyUrl();
            ShowNormalMessage(MessageLabel, GetPageResource("FriendlyNameUpdatedMsg"));
        }
        #endregion

    }

}
