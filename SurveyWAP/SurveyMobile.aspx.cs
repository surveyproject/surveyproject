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

        /// <summary>
        /// defaultCSS control.
        /// </summary>
        protected global::System.Web.UI.HtmlControls.HtmlLink defaultCSS;

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


        protected override void OnInit(EventArgs e)
        {
            //SP 25: loading of css and script files in page header moved to surveybox.cs
        }


        void Page_Load(Object sender, EventArgs e)
        {
                
            //in case of custom Layout add user CSS file:
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
                    defaultCSS.Visible = false;

                    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                    HtmlGenericControl css = new HtmlGenericControl("link");
                    css.Attributes.Add("rel", "stylesheet");
                    css.Attributes.Add("type", "text/css");
                    css.Attributes.Add("href", ResolveUrl(Votations.NSurvey.Constants.UserSettingsConstants.CssStoragePath + "/" +SurveyControl.SurveyId.ToString()+"/" + _userSettings.SurveyLayout[0].SurveyCss));
                    Page.Header.Controls.Add(css);
                }

            }
            

        }
    }
}