using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class WarningScreen : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblWarning.Text = GetPageResource("NoSurveysAvailableMessage"); 
               
            }
        }

        
    }
}