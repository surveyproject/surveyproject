/**************************************************************************************************
	Survey™ Project changes: copyright (c) 2009-2017, W3DevPro™ (https://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)


	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

************************************************************************************************/
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Votations.NSurvey.Constants;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.WebControls;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Generates the a sample asp.net code
    /// </summary>
    public partial class TakeSurvey : PageBase
    {
        //protected SurveyListControl SurveyList;
        protected System.Web.UI.WebControls.Label MessageLabel;
        new protected HeaderControl Header;
        protected System.Web.UI.WebControls.TextBox tbNetSource;
        protected System.Web.UI.WebControls.Literal TakeSurveyTitle;
        protected SurveyBox SurveyPreview;
        protected SurveyLayoutData _userSettings;


        public bool SurveyStarted;

        protected void Page_Init(object sender, EventArgs e)
        {

        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            SetupSecurity();
            LocalizePage();
            if (!IsPostBack)
            {
                ShowSurveyDDL();
            }

            UITabList.SetTakeSurveyTabs((MsterPageTabs)Page.Master, UITabList.TakeSurveyTabs.TakeSurvey);

            Votations.NSurvey.SQLServerDAL.SurveyLayout u = new Votations.NSurvey.SQLServerDAL.SurveyLayout();
            _userSettings = u.SurveyLayoutGet(((PageBase)Page).getSurveyId());

            if (!(_userSettings == null || _userSettings.SurveyLayout.Count == 0))
            {
                if (!string.IsNullOrEmpty(_userSettings.SurveyLayout[0].SurveyCss))
                {
                    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                    HtmlGenericControl css2 = new HtmlGenericControl("link");
                    css2.Attributes.Add("rel", "stylesheet");
                    css2.Attributes.Add("type", "text/css");
                    css2.Attributes.Add("href", ResolveUrl(UserSettingsConstants.CssStoragePath + "/" + SurveyId.ToString() + "/" + _userSettings.SurveyLayout[0].SurveyCss));
                    Page.Header.Controls.Add(css2);

                }

            }

            // add surveymobile CSS
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            HtmlGenericControl css3 = new HtmlGenericControl("link");
            css3.Attributes.Add("rel", "stylesheet");
            css3.Attributes.Add("type", "text/css");
            css3.Attributes.Add("href", ResolveUrl("~/Content/surveyform/surveymobile.css"));
            Page.Header.Controls.Add(css3);


        }

        private void InitiateSurvey(int surveyId=-1)
        {
            if (surveyId>-1 ||(ddlSurveys.Visible == true && ddlSurveys.SelectedValue != "-1"))
            {
                ((PageBase)Page).SurveyId =surveyId>-1?surveyId: int.Parse(ddlSurveys.SelectedValue);
               
            }
            else return;
   
            SurveyStarted = true;
            ddlSurveys.Visible = false;
            phSurveysDll.Visible = false;
            ChooseSurveyLabel.Visible = false;
       
            SurveyPreview.Visible = true;
            
            // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = ((PageBase)Page).getSurveyId();
                SurveyPreview.SurveyId = ((PageBase)Page).getSurveyId();

            if (!Page.IsPostBack)
            {

                // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
                SurveyPreview.SurveyId = SurveyId;
            }

        }

        private void ShowSurveyDDL()
        {
            var surveys=new Surveys().GetAssignedSurveysList(((PageBase)Page).NSurveyUser.Identity.UserId);
         
            if (surveys.Surveys.Count == 1) { InitiateSurvey(surveys.Surveys[0].SurveyId); return; }
            if (surveys.Surveys.Count == 0) { int s = ((PageBase)Page).SurveyId; return; }
            ddlSurveys.Items.Clear();
            ddlSurveys.Items.Add(new ListItem(GetPageResource("DDLSelectValue"), "-1"));
            ddlSurveys.AppendDataBoundItems = true;
            ddlSurveys.DataSource = surveys.Surveys;
            ddlSurveys.DataTextField = "Title";
            ddlSurveys.DataValueField = "SurveyId";
            ddlSurveys.DataBind();
            ddlSurveys.Visible = true;
            ChooseSurveyLabel.Visible = true;
        }

        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.TakeSurvey, true);
        }

        private void LocalizePage()
        {
            TakeSurveyTitle.Text = GetPageResource("TakeSurveyTitle");
            ChooseSurveyLabel.Text = GetPageResource("ChooseSurveyLabel");
            
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }



        #endregion

        protected void ddlSurveys_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlSurveys.SelectedValue != "-1")
            {
                ((PageBase)Page).SurveyId = int.Parse(ddlSurveys.SelectedValue); 
                InitiateSurvey(int.Parse(ddlSurveys.SelectedValue));
                
            }

        }

    }

}
