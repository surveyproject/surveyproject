using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Votations.NSurvey;
using Votations.NSurvey.WebAdmin;
using System.Web.UI.WebControls;
using Votations.NSurvey.WebControlsFactories;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebControls.UI;
using Votations.NSurvey.Enums;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using System.Web.UI.HtmlControls;
using Votations.NSurvey.Constants;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class CampaignStart : PageBase
    {
        protected SurveyLayoutData _userSettings;

        protected void Page_Init(object sender, EventArgs e)
        {
                       
            Votations.NSurvey.SQLServerDAL.SurveyLayout u = new Votations.NSurvey.SQLServerDAL.SurveyLayout();
            _userSettings = u.SurveyLayoutGet(((PageBase)Page).SurveyId);

            if (!(_userSettings == null || _userSettings.SurveyLayout.Count == 0))
            {

                if (!string.IsNullOrEmpty(_userSettings.SurveyLayout[0].SurveyCss))
                {
                    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                    HtmlGenericControl css2 = new HtmlGenericControl("link");
                    css2.Attributes.Add("rel", "stylesheet");
                    css2.Attributes.Add("type", "text/css");
                    css2.Attributes.Add("href", ResolveUrl(UserSettingsConstants.CssStoragePath + "/" + SurveyId.ToString() + "/" + _userSettings.SurveyLayout[0].SurveyCss));
                    Page.Header.Controls.Add(css2);
                }

                //else
                //{
                //Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                //HtmlGenericControl css = new HtmlGenericControl("link");
                //css.Attributes.Add("rel", "stylesheet");
                //css.Attributes.Add("type", "text/css");
                //css.Attributes.Add("href", VirtualPathUtility.ToAbsolute("~/Content/surveyadmin/surveypreview.css"));
                //Page.Header.Controls.Add(css);
                //}

                    //this.SurveyHeaderCustom.Text = HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyHeaderText);
                    //this.SurveyFooterCustom.Text = HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyFooterText);

            }
            //else
            //{
            //    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            //    HtmlGenericControl css = new HtmlGenericControl("link");
            //    css.Attributes.Add("rel", "stylesheet");
            //    css.Attributes.Add("type", "text/css");
            //    css.Attributes.Add("href", VirtualPathUtility.ToAbsolute("~/Content/surveyadmin/surveypreview.css"));
            //    Page.Header.Controls.Add(css);
            //}


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UITabList.SetCampaignTabs((MsterPageTabs)Page.Master, UITabList.CampaignTabs.CampaignStart);
            if (!IsPostBack)
            {
                SetupSecurity();
                LocalizePage();
                SurveyPreview.SurveyId = SurveyId;
            }

        }

        private void SetupSecurity()
        {
            if (!(CheckRight(NSurveyRights.AccessASPNetCode, false)))
                UINavigator.NavigateToAccessDenied(getSurveyId(), 1);
        }
        private void LocalizePage()
        {
            SurveyCodePreviewTitle.Text = GetPageResource("SurveyCodePreviewTitle");
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
            this.Init += new EventHandler(this.Page_Init);
        }
        #endregion
    }
}