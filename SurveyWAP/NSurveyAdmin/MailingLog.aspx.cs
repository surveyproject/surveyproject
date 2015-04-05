/**************************************************************************************************
	Survey changes: copyright (c) 2010, Fryslan Webservices TM (http://survey.codeplex.com)	

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
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using MetaBuilders.WebControls;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the mailing logs of the survey
	/// </summary>
    public partial class MailingLog : PageBase
	{
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Label CurrentPendingPageLabel;
		protected System.Web.UI.WebControls.Label TotalPendingPagesLabel;
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Literal PleaseWaitInfo;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.Literal Literal3;
		protected System.Web.UI.WebControls.Button DeleteFilesButton;
		protected System.Web.UI.WebControls.Literal UploadedFilesTitle;
		protected System.Web.UI.WebControls.DataGrid MailingLogDataGrid;
		protected System.Web.UI.WebControls.LinkButton PreviousLogPageLinkButton;
		protected System.Web.UI.WebControls.LinkButton NextLogPageLinkButton;
		protected System.Web.UI.WebControls.DataGrid ValidatedFilesDataGrid;
		protected System.Web.UI.WebControls.LinkButton PreviousValidatedPageLinkButton;
		protected System.Web.UI.WebControls.LinkButton NextValidatedPageLinkButton;
		protected System.Web.UI.WebControls.Literal MailingLogTitle;
		protected System.Web.UI.WebControls.Button DeleteLogsButton;
		protected SurveyListControl SurveyList;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetCampaignTabs((MsterPageTabs)Page.Master, UITabList.CampaignTabs.MailingLog);
			SetupSecurity();
			LocalizePage();
			MessageLabel.Visible = false;

			if (!Page.IsPostBack)
			{
				
				// Header.SurveyId = SurveyId;
               ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				MailingLogDataGrid.CurrentPageIndex = 1;
				PreviousLogPageLinkButton.Enabled = false;
				BindInvitationLogs();
			}

			DeleteLogsButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +GetPageResource("DeleteLogsButtonConfirmationMessage")+ "')== false) return false;");
		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessMailing, true);
		}

		private void LocalizePage()
		{
			MailingLogTitle.Text = GetPageResource("MailingLogTitle");
			((BoundColumn)MailingLogDataGrid.Columns[1]).HeaderText = GetPageResource("BoundColumnLogEmail");
			((BoundColumn)MailingLogDataGrid.Columns[2]).HeaderText = GetPageResource("BoundColumnLogExceptionType");
			((BoundColumn)MailingLogDataGrid.Columns[3]).HeaderText = GetPageResource("BoundColumnLogExceptionMessage");
			((BoundColumn)MailingLogDataGrid.Columns[4]).HeaderText = GetPageResource("BoundColumnErrorDate");
			PreviousLogPageLinkButton.Text = GetPageResource("PreviousLogPageLinkButton");
			NextLogPageLinkButton.Text = GetPageResource("NextLogPageLinkButton");
			DeleteLogsButton.Text = GetPageResource("DeleteLogsButton");
		}


		/// <summary>
		/// Get the current DB mailing log files and bind 
		/// the datagrid and set the correct pages
		/// </summary>
		private void BindInvitationLogs()
		{
			int totalRecords = 0,
				totalPages = 0;
			MailingLogDataGrid.DataMember = "InvitationLogs";
			MailingLogDataGrid.DataKeyField = "InvitationLogId";
			MailingLogDataGrid.DataSource = 
				new Voters().GetInvitationLogs(SurveyId, MailingLogDataGrid.CurrentPageIndex,25, out totalRecords);
			MailingLogDataGrid.DataBind();
			CurrentPendingPageLabel.Text = MailingLogDataGrid.CurrentPageIndex.ToString();

			if (totalRecords > 0)
			{
				if ((totalRecords%25) == 0)
				{
					totalPages = totalRecords/25;
				}
				else
				{
					totalPages = (totalRecords/25) + 1;
				}
				TotalPendingPagesLabel.Text = totalPages.ToString();
			}
			else
			{
				TotalPendingPagesLabel.Text = "1";
				CurrentPendingPageLabel.Text = "1";
			}

			// Should we enable the next link?
			if (totalPages == 1 || totalRecords == 0)
			{
				PreviousLogPageLinkButton.Enabled = false;
				NextLogPageLinkButton.Enabled = false;
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
			this.PreviousLogPageLinkButton.Click += new System.EventHandler(this.PreviousLogPageLinkButton_Click);
			this.NextLogPageLinkButton.Click += new System.EventHandler(this.NextLogPageLinkButton_Click);
			this.DeleteLogsButton.Click += new System.EventHandler(this.DeleteLogsButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void PreviousLogPageLinkButton_Click(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentPendingPageLabel.Text);
			int totalPages = int.Parse(TotalPendingPagesLabel.Text);

			// Decrement the current page index.
			if (currentPage > 1)
			{
				MailingLogDataGrid.CurrentPageIndex--;

				// Get the data for the DataGrid.
				BindInvitationLogs();

				// Should we disable the previous link?
				if (MailingLogDataGrid.CurrentPageIndex == 1)
				{

					PreviousLogPageLinkButton.Enabled = false;
				}

				// Should we enable the next link?
				if (totalPages > 1)
				{

					NextLogPageLinkButton.Enabled = true;
				}
			}	
		}


		private void NextLogPageLinkButton_Click(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentPendingPageLabel.Text);
			int totalPages = int.Parse(TotalPendingPagesLabel.Text);

			// Increment the current page index.
			if (currentPage < totalPages)
			{

				MailingLogDataGrid.CurrentPageIndex++;

				// Get the data for the DataGrid.
				BindInvitationLogs();

				// Should we disable the previous link?
				if (MailingLogDataGrid.CurrentPageIndex <= totalPages)
				{

					PreviousLogPageLinkButton.Enabled = true;
				}

				// Should we enable the next link?
				if (MailingLogDataGrid.CurrentPageIndex == totalPages || totalPages == 1)
				{

					NextLogPageLinkButton.Enabled = false;
				}
			}	
		}

		private void DeleteLogsButton_Click(object sender, System.EventArgs e)
		{
			RowSelectorColumn rsc = MailingLogDataGrid.Columns[0] as RowSelectorColumn;
			int deletedRows = 0;
			foreach( Int32 selectedIndex in rsc.SelectedIndexes ) 
			{
				new Voter().DeleteInvitationLog(int.Parse(MailingLogDataGrid.DataKeys[selectedIndex].ToString()));
				deletedRows++;
			}

			// All rows of the page deleted, switch to previous page
			if (deletedRows == MailingLogDataGrid.Items.Count && MailingLogDataGrid.CurrentPageIndex > 1)
			{
				MailingLogDataGrid.CurrentPageIndex--;
			}

			BindInvitationLogs();
		
		}

	}

}
