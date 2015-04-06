using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.SQLServerDAL;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class SurveyList : PageBase
    {
        protected int selectedTabIndex;

        private int GetDefaultIfApplicable()
        {
            foreach (var survey in new Surveys().GetAssignedSurveysList(((PageBase)Page).NSurveyUser.Identity.UserId).Surveys)
                if (survey.DefaultSurvey) return survey.SurveyId;
            return -1;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SelectedFolderId ==null  && GetDefaultIfApplicable()>-1){
                int surveyId = ((PageBase)Page).SurveyId; //  Use default if possible
            }

            if (!string.IsNullOrEmpty(Request.Params["tabindex"]))
            {
                string[] idx = Request.Params["tabindex"].Split(',');
                selectedTabIndex = int.Parse(idx[idx.Length - 1]);
            }
            if (selectedTabIndex > 0) { SurveyOption.isCreationMode = true; SurveyOption.isListMode = false; SurveyOption.Visible = true; }
            else
            { SurveyOption.isCreationMode = false; SurveyOption.isListMode = true; }

            SetupSecurity();
            LocalizePage();

            if (!Page.IsPostBack)
            {
                FillGrid();
            }
        }

        private void LocalizePage()
        {
            btnSearch.Text = GetPageResource("Search");
            SearchTitle.Text = GetPageResource("SearchTitle");
            SurveyListPageNrLiteral.Text = GetPageResource("SurveyListPageNrLiteral");

        }

        public void FillGrid()
        {
            SurveyData data = new Surveys().GetAllSurveysByTitle(txtSearchField.Text, SelectedFolderId, ((PageBase)Page).NSurveyUser.Identity.UserId);
            gridSurveys.DataSource = null;

            //commented out??
            if (!string.IsNullOrEmpty(SurveyListOrder))
            {
                data.Surveys.DefaultView.Sort = SurveyListOrder;
                gridSurveys.DataSource = data.Surveys.DefaultView;
            } else
                gridSurveys.DataSource = data.Surveys.OrderByDescending(x => x.Activated).ThenBy(x => x.Title).ToList();
 
            gridSurveys.DataBind();
   
        }

        //gridview paging: not used
        protected void gridSurveys_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridSurveys.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        
        private void SetupSecurity() {
            if(selectedTabIndex ==0)
                ((PageBase)Page).CheckRight(NSurveyRights.AccessSurveyList, true);

            if(selectedTabIndex==1)
                ((PageBase)Page).CheckRight(NSurveyRights.CreateSurvey, true);
        }

        protected void gvSurveys_Sorting(object sender, GridViewSortEventArgs e)
        {
            SurveyListOrder = e.SortExpression;
            FillGrid();            
        }

        public string GetStatus(bool active)
        {
            return (active) ?  GetPageResource("SurveyActive") : GetPageResource("SurveyInactive");
        }

        public string GetSurveyName(int id, string title)
        {
            //return string.Format("{0} {1}", GetPageResource("SurveyTitle"), id);
            return string.Format("{0}", title);
        }

        public string GetSurveyLink(int id)
        {
            return ResolveClientUrl("~/NSurveyAdmin/SurveyOptions.aspx");
        }

        public void OnSurveyDesign(Object sender, CommandEventArgs e)
        {
            SurveyId = int.Parse(e.CommandArgument.ToString());
            ((Wap)Master).HeaderControl.SurveyId = SurveyId;
            ((PageBase)Page).SurveyId = SurveyId;
            Response.Redirect(string.Format("{0}?surveyid={1}&menuindex={2}", UINavigator.SurveyContentBuilderLink, SurveyId, (int)((PageBase)Page).MenuIndex));
        }

        public void OnSurveyStats(Object sender, CommandEventArgs e)
        {
            SurveyId = int.Parse(e.CommandArgument.ToString());
            ((PageBase)Page).SurveyId = SurveyId;
            ((Wap)Master).HeaderControl.SurveyId = SurveyId;
            Response.Redirect(string.Format("{0}?surveyid={1}&menuindex={2}", UINavigator.SurveyStats, SurveyId, (int)((PageBase)Page).MenuIndex));
        }
             

        public void OnSurveySecurity(Object sender, CommandEventArgs e)
        {
            SurveyId = int.Parse(e.CommandArgument.ToString());
            ((PageBase)Page).SurveyId = SurveyId;
            ((Wap)Master).HeaderControl.SurveyId = SurveyId;            
            Response.Redirect(string.Format("{0}?surveyid={1}&menuindex={2}", UINavigator.SurveySecurityLink, SurveyId, (int)((PageBase)Page).MenuIndex));
        }

        public void OnSurveyEdit(Object sender, CommandEventArgs e)
        {
            SurveyId = int.Parse(e.CommandArgument.ToString());
            ((PageBase)Page).SurveyId = SurveyId;
            ((Wap)Master).HeaderControl.SurveyId = SurveyId;
            Response.Redirect(string.Format("{0}?surveyid={1}&menuindex={2}", UINavigator.SurveyOptionsLink, SurveyId, (int)((PageBase)Page).MenuIndex));
        }

        public void OnSurveyFilter(Object sender, EventArgs e)
        {
            SelectedFolderId = null;
            FillGrid();
        }
           
    
    }
}
