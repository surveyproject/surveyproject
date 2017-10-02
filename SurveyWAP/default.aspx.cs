using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.WebAdmin.Code;

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

        //Note: Error handling in Global.config file:

        //protected override void OnError(EventArgs e)
        //{
        //    Exception exc = Server.GetLastError().GetBaseException();

        //    if (Server.GetLastError().GetBaseException() is System.Web.HttpRequestValidationException)
        //    {
        //        Response.Clear();
        //        Response.Write("Invalid characters.");
        //        Response.StatusCode = 200;
        //        Response.End();

        //    }
        //}

    }
}