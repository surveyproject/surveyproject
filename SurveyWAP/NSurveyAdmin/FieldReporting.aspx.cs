/**************************************************************************************************
	Survey changes: copyright (c) 2010, W3DevPro TM (http://survey.codeplex.com)	

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
using System.Data;
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
	/// Display the survey fields answers
	/// </summary>
    public partial class FieldReporting : PageBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid FieldReportDataGrid;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Label CurrentPageLabel;
		protected System.Web.UI.WebControls.LinkButton PreviousPageLinkButton;
		protected System.Web.UI.WebControls.LinkButton NextPageLinkButton;
		protected System.Web.UI.WebControls.Label TotalPagesLabel;
		protected System.Web.UI.WebControls.Literal FieldReportTitle;
		protected SurveyListControl SurveyList;

		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetResultsTabs((MsterPageTabs)Page.Master, UITabList.ResultTabs.Reports);

			SetupSecurity();
			LocalizePage();

			if (!Page.IsPostBack)
			{
			
				// Header.SurveyId = SurveyId;	
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				FieldReportDataGrid.CurrentPageIndex = 1;
				PreviousPageLinkButton.Enabled = false;
				BindData();
			}

		}

        protected void rbListSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblReports.SelectedValue)
            {
                case "GR": Response.Redirect(UINavigator.ResultsReportHyperlink); break;
                case "TR": Response.Redirect(UINavigator.FieldsReportHyperlink); break;
                case "CTR": Response.Redirect(UINavigator.CrossTabHyperLink); break;
            }
        }

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessFieldEntries, true);
		}

		private void LocalizePage()
        {   
			FieldReportTitle.Text = GetPageResource("FieldReportTitle");
			PreviousPageLinkButton.Text = GetPageResource("PreviousPageLinkButton");
			NextPageLinkButton.Text = GetPageResource("NextPageLinkButton");
			//((ButtonColumn)FieldReportDataGrid.Columns[0]).Text = GetPageResource("DetailsColumn");
            ((ButtonColumn)FieldReportDataGrid.Columns[0]).HeaderText = GetPageResource("DetailsColumn");
			//((ButtonColumn)FieldReportDataGrid.Columns[1]).Text = GetPageResource("DeleteColumn");
            ((ButtonColumn)FieldReportDataGrid.Columns[1]).HeaderText = GetPageResource("DeleteColumn");
			((ButtonColumn)FieldReportDataGrid.Columns[1]).Visible = CheckRight(NSurveyRights.DeleteVoterEntries, false);
            TranslateListControl(rblReports);
		}

		/// <summary>
		/// Get the current DB stats and fill 
		/// the label with them
		/// </summary>
		private void BindData()
		{
			int totalPages = 0,
				totalRecords = 0;
			
			DataSet textEntries = new Voters().GetVotersTextEntries(SurveyId, FieldReportDataGrid.CurrentPageIndex, 25, new DateTime(2004,1,1), DateTime.UtcNow);

			FieldReportDataGrid.DataSource = textEntries ;
			FieldReportDataGrid.DataKeyField = "VoterID";
			FieldReportDataGrid.DataBind();

			if (textEntries.Tables[0].Rows.Count != 0)
			{
				totalRecords = int.Parse(textEntries.Tables[0].Rows[0]["TotalRecords"].ToString());
				CurrentPageLabel.Text = FieldReportDataGrid.CurrentPageIndex.ToString();
				if (textEntries.Tables[0].Rows.Count > 0)
				{
					if ((totalRecords%25) == 0)
					{
						totalPages = totalRecords/25;
					}
					else
					{
						totalPages = (totalRecords/25) + 1;
					}
				
				}
				TotalPagesLabel.Text = totalPages.ToString();
			}

			// Should we enable the next link?
			if (totalPages == 1 || totalRecords == 0)
			{
				PreviousPageLinkButton.Enabled = false;
				NextPageLinkButton.Enabled = false;
			}
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
			this.FieldReportDataGrid.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.FieldReportDataGrid_DeleteCommand);
			this.FieldReportDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.BindItemData);
			this.FieldReportDataGrid.SelectedIndexChanged += new System.EventHandler(this.ViewDetails);
			this.PreviousPageLinkButton.Click += new System.EventHandler(this.LoadPreviousPage);
			this.NextPageLinkButton.Click += new System.EventHandler(this.LoadNextPage);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BindItemData(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// Hides or shows voter id, ip and startdate
			e.Item.Cells[2].Visible = true;
			e.Item.Cells[4].Visible = false;
			e.Item.Cells[5].Visible = false;
			if (!new Surveys().IsSurveyScored(SurveyId))
			{
				e.Item.Cells[6].Visible = false;
			}

			e.Item.Cells[e.Item.Cells.Count-1].Visible = false;
			
			if (e.Item.ItemType == ListItemType.Header)
			{
				for (int i=0; i<e.Item.Cells.Count;i++)
				{
					// Get only the name
					e.Item.Cells[i].Text = (e.Item.Cells[i].Text.Split('_'))[0];
					e.Item.Cells[i].Wrap = false;
				}
			}
			else
			{
				// Remove time from date
				e.Item.Cells[3].Text = (e.Item.Cells[3].Text.Split(' '))[0];
				for (int i=0; i<e.Item.Cells.Count;i++)
				{
					e.Item.Cells[i].Wrap = false;
				}
			}
		}

		private void LoadNextPage(object sender, System.EventArgs e)
		{

			// Declare local data members.
			int currentPage = int.Parse(CurrentPageLabel.Text);
			int totalPages = int.Parse(TotalPagesLabel.Text);

			// Increment the current page index.
			if (currentPage < totalPages)
			{

				FieldReportDataGrid.CurrentPageIndex++;

				// Get the data for the DataGrid.
				BindData();

				// Should we disable the previous link?
				if (FieldReportDataGrid.CurrentPageIndex <= totalPages)
				{

					PreviousPageLinkButton.Enabled = true;
				}

				// Should we enable the next link?
				if (FieldReportDataGrid.CurrentPageIndex == totalPages)
				{

					NextPageLinkButton.Enabled = false;
				}
			}

		
		}

		private void LoadPreviousPage(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentPageLabel.Text);
			int totalPages = int.Parse(TotalPagesLabel.Text);

			// Decrement the current page index.
			if (currentPage > 1)
			{

				FieldReportDataGrid.CurrentPageIndex--;

				// Get the data for the DataGrid.
				BindData();

				// Should we disable the previous link?
				if (FieldReportDataGrid.CurrentPageIndex == 1)
				{

					PreviousPageLinkButton.Enabled = false;
				}

				// Should we enable the next link?
				if (totalPages > 1)
				{
					NextPageLinkButton.Enabled = true;
				}
			}		
		}

		private void ViewDetails(object sender, System.EventArgs e)
		{
			UINavigator.NavigateToVoterReport(SurveyId,
				int.Parse(FieldReportDataGrid.DataKeys[FieldReportDataGrid.SelectedIndex].ToString()), 
				MenuIndex);
		}
	
		private void FieldReportDataGrid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			new Voter().DeleteVoterById(int.Parse(FieldReportDataGrid.DataKeys[e.Item.ItemIndex].ToString()));
			// Last item of the page deleted ?
			if (FieldReportDataGrid.Items.Count == 1)
			{
				FieldReportDataGrid.CurrentPageIndex--;
			}
			BindData();
		}
	}

}
