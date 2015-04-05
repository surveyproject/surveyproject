using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.Resources;
using System.Web.UI.HtmlControls;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// This was renamed from Survey to SurveyMain as it causes conflict
    /// </summary>
    public partial class SurveyMobile : System.Web.UI.Page
    {
        private int GetIdFromUrl()
        {
            if (Request.PathInfo.Length == 0)
            {
                MessageLabel.Text = ResourceManager.GetString("InvalidSurveyUrl");
                MessageLabel.Visible = true;
                return -1;
            }
            string friendlyName = Request.PathInfo.Substring(1);
            int id = new Surveys().GetSurveyIdFromFriendlyName(friendlyName);
            if (id <= 0)
            {
                MessageLabel.Text = ResourceManager.GetString("InvalidSurveyUrl");
                MessageLabel.Visible = true;
                return -1;
            }
            return id;
        }
        private int GetIdFromQueryStr()
        {
            Guid guid;
            if (Guid.TryParse(Request[Votations.NSurvey.Constants.Constants.QRYSTRGuid], out guid))
            {
                int id = new Surveys().GetSurveyIdFromGuid(guid);
                if (id <= 0)
                {
                    MessageLabel.Text = ResourceManager.GetString("InvalidSurveyGuid");
                    MessageLabel.Visible = true; return -1;
                }
                else return id;
            }
            else
            {
                MessageLabel.Text = ResourceManager.GetString("InvalidSurveyGuid");
                MessageLabel.Visible = true; return -1;
            }
        }
        private int GetSurveyId()
        {
            if (Request[Votations.NSurvey.Constants.Constants.QRYSTRGuid] != null) return GetIdFromQueryStr();
            return GetIdFromUrl();
        }
        void Page_Load(Object sender, EventArgs e)
        {

            SurveyLayoutData _userSettings;
            //    Survey.SurveyId = int.Parse(Request["SurveyId"]);
            int id = GetSurveyId();
            if (id == -1) { SurveyControl.SurveyId = 0; SurveyControl.Visible = false; return; }
            SurveyControl.SurveyId = id;

            Votations.NSurvey.SQLServerDAL.SurveyLayout u = new Votations.NSurvey.SQLServerDAL.SurveyLayout();

            _userSettings = u.SurveyLayoutGet(SurveyControl.SurveyId);
            if (!(_userSettings == null || _userSettings.SurveyLayout.Count == 0))
            {
                if (!string.IsNullOrEmpty(_userSettings.SurveyLayout[0].SurveyCss))
                {
                    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                    HtmlGenericControl css = new HtmlGenericControl("link");
                    css.Attributes.Add("rel", "stylesheet");
                    css.Attributes.Add("type", "text/css");
                    css.Attributes.Add("href", ResolveUrl(Votations.NSurvey.Constants.UserSettingsConstants.CssStoragePath + "/" +SurveyControl.SurveyId.ToString()+"/" + _userSettings.SurveyLayout[0].SurveyCss));
                    Page.Header.Controls.Add(css);
                }

               // this.SurveyHeaderCustom.Text = HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyHeaderText);
               // this.SurveyFooterCustom.Text = HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyFooterText);
            }

           // jQuery (necessary for Bootstrap's JavaScript plugins) + answerfieldslideritem.cs
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            HtmlGenericControl javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/JavaScript/01_jquery/jquery-1.11.1.js"));
            Page.Header.Controls.Add(javascriptControl);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/JavaScript/ui/jquery-ui-1.10.4.js"));
            Page.Header.Controls.Add(javascriptControl);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

        }
    }
}