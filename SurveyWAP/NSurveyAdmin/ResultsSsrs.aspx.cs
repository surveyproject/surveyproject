/**************************************************************************************************
	Survey™ Project: copyright (c) 2009-2017, W3DevPro™ (https://github.com/surveyproject)	

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
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Overview of SSRS reports as created through SSRS based on SP data
    /// </summary>
    public partial class ResultsSsrs : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UITabList.SetResultsTabs((MsterPageTabs)Page.Master, UITabList.ResultTabs.Reports);

            SetupSecurity();
            LocalizePage();

            if (!Page.IsPostBack)
            {

                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;

                //SsrsDataGrid.CurrentPageIndex = 1;

                BindData();
            }
        }


        private void SetupSecurity()
        {
            //CheckRight(NSurveyRights.AccessReports, true);
            CheckRight(NSurveyRights.AccessSsrsReports, true);
        }

        private void LocalizePage()
        {
            FieldReportTitle.Text = GetPageResource("SsrsReportTitle");

            ((ButtonColumn)SsrsDataGrid.Columns[0]).HeaderText = GetPageResource("DetailsColumn");
            //((ButtonColumn)SsrsDataGrid.Columns[0]).Text = GetPageResource("DetailsColumn");
        }

        private DataTable CreateDataTable()
        {
            // get ssrs files from directory
            string varPath = Server.MapPath("~/NSurveyReports/");
            string[] fileFullNames = Directory.GetFiles(varPath, "Ssrs*.aspx");

            DataTable dt = new DataTable();
            dt.Columns.Add("FileName", typeof(System.String));
            dt.Columns.Add("Size", typeof(System.String));
            dt.Columns.Add("Modified", typeof(System.String));
            DataRow dr = null;
            DirectoryInfo dir = new DirectoryInfo(varPath);

            // Iterate through the datatable,
            // adding file to a new row, along with the  filesize to each row
            foreach (FileInfo fi in dir.GetFiles("Ssrs*.aspx"))
            {
                dr = dt.NewRow();
                dr[0] = fi.Name.ToString();
                dr[1] = fi.Length.ToString("N0");  //'N0'formats the number with commas
                dr[2] = fi.LastWriteTimeUtc.ToString();
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// Get the current DB stats and fill 
        /// the label with them
        /// </summary>
        private void BindData()
        {
            int totalPages = 0,
                totalRecords = 0;

                //Add datasource to datagrid:
                SsrsDataGrid.DataSource = CreateDataTable();
                SsrsDataGrid.DataKeyField = "FileName";
                SsrsDataGrid.DataBind();
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
            this.SsrsDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.BindItemData);
            this.SsrsDataGrid.SelectedIndexChanged += new System.EventHandler(this.ViewSsrsReport);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void BindItemData(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litReportDescription = (Literal)e.Item.FindControl("litReportDescription");
                litReportDescription.Text = GetPageResource(e.Item.Cells[2].Text.Split('.')[0]) ;
            }


            // Hides or shows voter id, ip and startdate
            e.Item.Cells[2].Visible = true;
            e.Item.Cells[3].Visible = true;
            //e.Item.Cells[4].Visible = true;

            //e.Item.Cells[4].Visible = false;
            //e.Item.Cells[5].Visible = false;

            //e.Item.Cells[e.Item.Cells.Count - 1].Visible = false;

            //if (e.Item.ItemType == ListItemType.Header)
            //{
            //    for (int i = 0; i < e.Item.Cells.Count; i++)
            //    {
            //        // Get only the name
            //        e.Item.Cells[i].Text = (e.Item.Cells[i].Text.Split('_'))[0];
            //        e.Item.Cells[i].Wrap = false;
            //    }
            //}
            //else
            //{
            //    // Remove time from date
            //    e.Item.Cells[2].Text = (e.Item.Cells[2].Text.Split(' '))[0];
            //    for (int i = 0; i < e.Item.Cells.Count; i++)
            //    {
            //        e.Item.Cells[i].Wrap = false;
            //    }
            //}

        }

        private void ViewSsrsReport(object sender, System.EventArgs e)
        {
            string fileName = SsrsDataGrid.DataKeys[SsrsDataGrid.SelectedIndex].ToString();  
            // new - goto Ssrs report
            UINavigator.NavigateToSsrsReport(fileName, MenuIndex);
        }

    }
}