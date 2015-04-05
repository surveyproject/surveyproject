using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.BusinessRules;

using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Auth the login
    /// </summary>
    public partial class HelpFiles : PageBase
    {

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            SetupSecurity();
            LocalizePage();
            UITabList.SetHelpTabs((MsterPageTabs)Page.Master, UITabList.HelpTabs.HelpFiles);

            if (!Page.IsPostBack)
            {
                ((Wap)Master.Master).HeaderControl.SurveyId = getSurveyId();
            }
        }

        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.AccessHelpFiles, true);
            IsSingleUserMode(true);
        }


        private void LocalizePage()
        {

        }

    }

}
