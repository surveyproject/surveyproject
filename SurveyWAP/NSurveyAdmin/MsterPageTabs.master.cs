using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class MsterPageTabs : System.Web.UI.MasterPage
    {
        public NameValueCollection DisplayTabs = new NameValueCollection();
        public int selectedTabIndex = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get selected tab
            if (!string.IsNullOrEmpty(Request.Params["mti"]))
            {
                string[] idx = Request.Params["mti"].Split(',');
                selectedTabIndex = int.Parse(idx[idx.Length - 1]);

                if (Request.Params["mtinvoke"] == "1")
                    Response.Redirect(DisplayTabs[selectedTabIndex]);
            }
 
        }


    }
}