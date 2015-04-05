using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.UserProvider;
using Votations.NSurvey.WebAdmin.UserControls;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class SurveySettings : PageBase
    {
        public int selectedTabIndex = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get selected tab
            if (!string.IsNullOrEmpty(Request.Params["tabindex"]))
            {
                string[] idx = Request.Params["tabindex"].Split(',');
                selectedTabIndex = int.Parse(idx[idx.Length - 1]);
            }

            if (!Page.IsPostBack)
            {
                //SurveyOption.SurveyId = SurveyId;
                //multiLanguagesEdit.SurveyId = SurveyId;
            }
        }
    }
}
