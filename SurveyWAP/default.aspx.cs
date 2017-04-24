using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Votations.NSurvey.WebAdmin
{
    public partial class _default :  PageBase
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


        protected void Page_Load(object sender, EventArgs e)
        {
            LocalizePage();
        }

        private void LocalizePage()
        {

            Slogan.InnerHtml = GetPageResource("SpFrontPageSlogan");

        }

    }
}