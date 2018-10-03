using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Votations.NSurvey.WebAdmin;

namespace Votations.NSurvey
{
    /// <summary>
    /// Webapplication MasterPage code
    /// </summary>
    public partial class Wap : System.Web.UI.MasterPage
    {
        protected Image spLogo;
        //protected Panel logoPanel;

        public bool isTreeStale
        {
            set
            {
                this.surveyTree1.isTreeStale = true;
            }
        }

        public void RebuildTree()
        {
            this.surveyTree1.RebuildTree();
        }


        protected void Page_Init(object sender, System.EventArgs e)
        {

            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            //Ie 8 and lower have an issue with the "Cache-Control no-cache" and "Cache-Control store-cache" headers.
            //The work around is allowing private caching only but immediately expire it.
            if ((Request.Browser.Browser.ToLower() == "ie") && (Request.Browser.MajorVersion < 9))
            {
                Response.Cache.SetCacheability(HttpCacheability.Private);
                Response.Cache.SetMaxAge(TimeSpan.FromMilliseconds(1));
            }
            else
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);//IE set to not cache
                Response.Cache.SetNoStore();//Firefox/Chrome not to cache
                Response.Cache.SetExpires(DateTime.Now); //for safe measure expire it immediately
            }



            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            // Add CSS Files

            //Jquery UI 1.11.4 css:
            HtmlGenericControl css = new HtmlGenericControl("link");            
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");
            css.Attributes.Add("href", ResolveUrl("~/Content/themes/base/all.css"));            
            Page.Header.Controls.Add(css);

            //Tipsy help balloons css: 
            css = new HtmlGenericControl("link");
            css.Attributes.Add("rel", "stylesheet");
            css.Attributes.Add("type", "text/css");
            css.Attributes.Add("href", ResolveUrl("~/Scripts/Javascript/tooltip/stylesheets/tipsy.css"));
            Page.Header.Controls.Add(css);


            //Add JQuery Scripts

            //JQuery v. 3.3.1. Min:
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            HtmlGenericControl javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-3.3.1.min.js"));
            Page.Header.Controls.Add(javascriptControl);

            //JQuery Migrate Plugin - to test upgrades
            //Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            //javascriptControl = new HtmlGenericControl("script");
            //javascriptControl.Attributes.Add("type", "text/Javascript");
            //javascriptControl.Attributes.Add("src", ResolveUrl("https://code.jquery.com/jquery-migrate-3.0.0.js"));
            //Page.Header.Controls.Add(javascriptControl);


            //JQuery UI v. 1.12.1 min :
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-ui-1.12.1.min.js"));
            Page.Header.Controls.Add(javascriptControl);

            //JQuery UI localized files - used on datepicker:
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            //javascriptControl.Attributes.Add("src", ResolveUrl(string.Format("~/Scripts/js/jquery.ui.datepicker-{0}.js",lang)));
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-ui-i18n.min.js"));
            Page.Header.Controls.Add(javascriptControl);
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            //JQuery Tips Help Balloons:
            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/Javascript/tooltip/javascripts/jquery.tipsy.js"));
            Page.Header.Controls.Add(javascriptControl);
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            //JQuery Bootstrap JS
            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/bootstrap.min.js"));
            Page.Header.Controls.Add(javascriptControl);
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));


            //JQuery Mobile devices Modernizr
            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/modernizr-2.8.3.js"));
            Page.Header.Controls.Add(javascriptControl);
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));




       }

        protected void Page_Load(object sender, EventArgs e)
        {
            LocalizePage();
            ViewBanners();
        }

        private void LocalizePage()
        {

            Disclaimer.Text = ((PageBase)Page).GetPageResource("Disclaimer");
            Theme.InnerHtml = ((PageBase)Page).GetPageResource("SpFrontPageTheme");
            LdTitle.InnerHtml = ((PageBase)Page).GetPageResource("DisclaimerTitle");
            ldPopupTitle.InnerHtml = ((PageBase)Page).GetPageResource("DisclaimerTitle");
        }

        /// <summary>
        /// Hide or show banners, SP logo and footer on opening page depending on authorisation: logged in or not
        /// </summary>
        public void ViewBanners()
        {
            if (((PageBase)Page).NSurveyUser.Identity.UserId == -1)
            {
                banners.Visible = true;
                Footercontrol1.Visible = true;
                surveyTree1.Visible = false;
                return;
            }

                banners.Visible = false;
                Footercontrol1.Visible = false;
                logoText.Attributes.Add("class", "logoTextLogedin");
        }

        /// <summary>
        /// Headcontrol loads header.ascx webcontrol including menu, logout and logo
        /// </summary>
        public Votations.NSurvey.WebAdmin.UserControls.HeaderControl HeaderControl

        {
            get
            {
                return Headercontrol1;
            }

        }

        /// <summary>
        /// Loginbox control loads login.ascx webcontrol including login entry fields
        /// </summary>
        public Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls.LoginBox LoginBox
        {
            get
            {
                return LoginBox1;
            }

        }



    }
}