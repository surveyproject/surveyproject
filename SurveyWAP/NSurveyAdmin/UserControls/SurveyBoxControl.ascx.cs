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

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    public partial class SurveyBoxControl : System.Web.UI.UserControl
    {


        //private int GetSurveyId()
        //{
        //    if (Request[Votations.NSurvey.Constants.Constants.QRYSTRGuid] != null) return GetIdFromQueryStr();
        //    return GetIdFromUrl();
        //}

        void Page_Load(Object sender, EventArgs e)
        {

            SurveyLayoutData _userSettings;
            //    Survey.SurveyId = int.Parse(Request["SurveyId"]);
            //int id = GetSurveyId();
            int id = 1012;

            if (id == -1) 
            { 
                SurveyControl.SurveyId = 0; 
                SurveyControl.Visible = false; 
                return; 
            }
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
                    css.Attributes.Add("href", ResolveUrl(Votations.NSurvey.Constants.UserSettingsConstants.CssStoragePath + "/" + SurveyControl.SurveyId.ToString() + "/" + _userSettings.SurveyLayout[0].SurveyCss));
                    Page.Header.Controls.Add(css);
                }

                // this.SurveyHeaderCustom.Text = HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyHeaderText);
                // this.SurveyFooterCustom.Text = HttpUtility.HtmlDecode(_userSettings.SurveyLayout[0].SurveyFooterText);
            }

            // jQuery (necessary for Bootstrap's JavaScript plugins) + answerfieldslideritem.cs
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            HtmlGenericControl javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-3.5.1.min.js"));
            Page.Header.Controls.Add(javascriptControl);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            javascriptControl = new HtmlGenericControl("script");
            javascriptControl.Attributes.Add("type", "text/Javascript");
            javascriptControl.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-ui-1.12.1.min.js"));
            Page.Header.Controls.Add(javascriptControl);

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

        }
    }
}