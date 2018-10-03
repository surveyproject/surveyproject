using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
    public partial class EditCssXml : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.CssXml);
            SetupSecurity();

            LocalizePage();

            if (!IsPostBack)
            {
                this.BindGrid();
            }
            
        }

        private void SetupSecurity()
        {
            //CheckRight(NSurveyRights.SurveyLayoutRight, true);

            if (NSurveyUser.Identity.IsAdmin == false)
            {
                UINavigator.NavigateToAccessDenied(getSurveyId(), MenuIndex);
            }
        }


        private void BindGrid()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.ReadXml(Server.MapPath("~/XmlData/Css/SurveyBox.xml"));
            if (ds != null && ds.HasChanges())
            {
                CssXmlGridView.DataSource = ds;
                CssXmlGridView.DataBind();
            }
            else
            {
                CssXmlGridView.DataBind();
            }

        }

        protected void CssXmlGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CssXmlGridView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void CssXmlGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BindGrid();
            DataSet ds = CssXmlGridView.DataSource as DataSet;
            ds.Tables[0].Rows[CssXmlGridView.Rows[e.RowIndex].DataItemIndex].Delete();
            ds.WriteXml(Server.MapPath("~/XmlData/Css/SurveyBox.xml"));
            BindGrid();
        }

        protected void CssXmlGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int i = CssXmlGridView.Rows[e.RowIndex].DataItemIndex;
            string Value = (CssXmlGridView.Rows[e.RowIndex].FindControl("txtValue") as TextBox).Text;
            //string Version = (CssXmlGridView.Rows[e.RowIndex].FindControl("txtVersion") as TextBox).Text;
            //string Id = (CssXmlGridView.Rows[e.RowIndex].FindControl("txtId") as TextBox).Text;
            //string Name = (CssXmlGridView.Rows[e.RowIndex].FindControl("txtName") as TextBox).Text;

            CssXmlGridView.EditIndex = -1;
            BindGrid();

            DataSet ds = (DataSet)CssXmlGridView.DataSource;
            ds.Tables[0].Rows[i]["value"] = Value;
            //ds.Tables[0].Rows[i]["version"] = Version;
            //ds.Tables[0].Rows[i]["id"] = Id;
            //ds.Tables[0].Rows[i]["name"] = Name;

            ds.WriteXml(Server.MapPath("~/XmlData/Css/SurveyBox.xml"));
            BindGrid();
        }

        protected void CssXmlGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CssXmlGridView.EditIndex = -1;
            BindGrid();
        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CssXmlGridView.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        private void LocalizePage()
        {
        }
    }
}