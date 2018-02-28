using System;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// webapplication default webpage
    /// </summary>
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
            ViewLogo();
        }

        private void LocalizePage()
        {

            Slogan.InnerHtml = GetPageResource("SpFrontPageSlogan");

        }

        public void ViewLogo()
        {
            if (((PageBase)Page).NSurveyUser.Identity.UserId == -1)
            {
                logoDiv.Visible = true;
                return;
            }
            logoDiv.Visible = false;
        }
    }
}