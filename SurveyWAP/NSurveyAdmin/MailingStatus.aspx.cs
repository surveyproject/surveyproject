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
	/// Display the survey statistics
	/// </summary>
    public partial class MailingStatus : PageBase
	{
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Button DeleteRequestButton;
		protected System.Web.UI.WebControls.DataGrid PendingEmailsDataGrid;
		protected System.Web.UI.WebControls.DataGrid AnsweredEmailsDatagrid;
		protected System.Web.UI.WebControls.Label CurrentPendingPageLabel;
		protected System.Web.UI.WebControls.Label TotalPendingPagesLabel;
		protected System.Web.UI.WebControls.LinkButton PreviousPendingPageLinkButton;
		protected System.Web.UI.WebControls.LinkButton NextPendingPageLinkButton;
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.PlaceHolder pendinglist;
		protected System.Web.UI.WebControls.PlaceHolder NoPending;
		protected System.Web.UI.WebControls.Label CurrentAnsweredPageLabel;
		protected System.Web.UI.WebControls.Label TotalAnsweredPagesLabel;
		protected System.Web.UI.WebControls.LinkButton PreviousAnsweredPageLinkButton;
		protected System.Web.UI.WebControls.LinkButton NextAnsweredPageLinkButton;
		protected System.Web.UI.WebControls.PlaceHolder NoAnswersPlaceHolder;
		protected System.Web.UI.WebControls.Button DeleteVoterButton;
		protected System.Web.UI.WebControls.PlaceHolder AnswersPlaceHolder;
		protected System.Web.UI.WebControls.Literal PleaseWaitInfo;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.Literal Literal3;
		protected System.Web.UI.WebControls.Literal PendingEmailsTitle;
		protected System.Web.UI.WebControls.Literal NoEmailInvitationInfo;
		protected System.Web.UI.WebControls.Literal ValidatedEmailTitle;
		protected System.Web.UI.WebControls.Literal NoEmailAnswered;
		protected SurveyListControl SurveyList;
		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetCampaignTabs((MsterPageTabs)Page.Master, UITabList.CampaignTabs.MailingStatus);
			SetupSecurity();
			LocalizePage();

			if (!Page.IsPostBack)
			{

				// Header.SurveyId = SurveyId;
             ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				//PendingEmailsDataGrid.CurrentPageIndex = 1;
				//AnsweredEmailsDatagrid.CurrentPageIndex = 1;
				PreviousPendingPageLinkButton.Enabled = false;
				BindPendingData();
				BindAnsweredData();
			}

			DeleteVoterButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +GetPageResource("DeleteVoterConfirmationMessage")+ "')== false) return false;");

		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessMailing, true);
		}

		private void LocalizePage()
		{
			PendingEmailsTitle.Text = GetPageResource("PendingEmailsTitle");
			((BoundColumn)PendingEmailsDataGrid.Columns[1]).HeaderText = GetPageResource("BoundColumnEmail");
			((BoundColumn)PendingEmailsDataGrid.Columns[2]).HeaderText = GetPageResource("BoundColumnRequestDate");
			PreviousPendingPageLinkButton.Text = GetPageResource("PreviousPendingPageLinkButton");
			NextPendingPageLinkButton.Text = GetPageResource("NextPendingPageLinkButton");
			DeleteRequestButton.Text = GetPageResource("DeleteRequestButton");
			NoEmailInvitationInfo.Text = GetPageResource("NoEmailInvitationInfo");
			ValidatedEmailTitle.Text = GetPageResource("ValidatedEmailTitle");
			NoEmailAnswered.Text = GetPageResource("NoEmailAnswered");
            AnsweredEmailsDatagrid.Columns[1].HeaderText = GetPageResource("MailStatusDetails");
			((BoundColumn)AnsweredEmailsDatagrid.Columns[2]).HeaderText = GetPageResource("BoundColumnEmail");
			((BoundColumn)AnsweredEmailsDatagrid.Columns[3]).HeaderText = GetPageResource("BoundColumnAnswerDate");
			PreviousAnsweredPageLinkButton.Text = GetPageResource("PreviousPendingPageLinkButton");
			NextAnsweredPageLinkButton.Text = GetPageResource("NextPendingPageLinkButton");
			DeleteVoterButton.Text = GetPageResource("DeleteVoterButton");
		}

		/// <summary>
		/// Get the current DB pending invitations and
		/// bind the datagrid and set the correct pages
		/// </summary>
		private void BindPendingData()
		{

            int totalRecords = 0,
                totalPages = 0;
            
                
                PendingEmailsDataGrid.DataMember = "InvitationQueues";
                PendingEmailsDataGrid.DataSource = new Voters().GetVotersInvitationQueue(SurveyId, CurrentPageIndex, PendingEmailsDataGrid.PageSize, out totalRecords);
                PendingEmailsDataGrid.DataBind();
                      
			CurrentPendingPageLabel.Text = (CurrentPageIndex+1 ).ToString();

			if (totalRecords > 0)
			{
				NoPending.Visible = false;
				pendinglist.Visible = true;

                if ((totalRecords % (PendingEmailsDataGrid.PageSize)) == 0)
				{
                    totalPages = totalRecords / (PendingEmailsDataGrid.PageSize);
				}
				else
				{
                    totalPages = (totalRecords / (PendingEmailsDataGrid.PageSize)) + 1;
				}
				TotalPendingPagesLabel.Text = totalPages.ToString();
			}
			else
			{
				NoPending.Visible = true;
				pendinglist.Visible = false;
				TotalPendingPagesLabel.Text = "1";
				CurrentPendingPageLabel.Text = "1";
			}

			// Should we enable the next link?
			if (totalPages == 1 || totalRecords == 0)
			{
				PreviousPendingPageLinkButton.Enabled = false;
				NextPendingPageLinkButton.Enabled = false;
			}
		}

		/// <summary>
		/// Get the current DB answered invitations and
		/// bind the datagrid and set the correct pages
		/// </summary>
		private void BindAnsweredData()
		{
			int totalRecords = 0,
				totalPages = 0;
			AnsweredEmailsDatagrid.DataMember = "Voters";
			AnsweredEmailsDatagrid.DataKeyField = "VoterID";
            AnsweredEmailsDatagrid.DataSource = new Voters().GetVotersInvitationAnswered(SurveyId, CurrentPageIndex, AnsweredEmailsDatagrid.PageSize, out totalRecords);
			AnsweredEmailsDatagrid.DataBind();
			CurrentAnsweredPageLabel.Text = (CurrentPageIndex+1).ToString();

			if (totalRecords > 0)
			{
				NoAnswersPlaceHolder.Visible = false;
				AnswersPlaceHolder.Visible = true;

                if ((totalRecords % (AnsweredEmailsDatagrid.PageSize) )== 0)
				{
                    totalPages = totalRecords / (AnsweredEmailsDatagrid.PageSize);
				}
				else
				{
                    totalPages = (totalRecords / (AnsweredEmailsDatagrid.PageSize)) + 1;
				}
				TotalAnsweredPagesLabel.Text = totalPages.ToString();
			}
			else
			{
				NoAnswersPlaceHolder.Visible = true;
				AnswersPlaceHolder.Visible = false;
				TotalAnsweredPagesLabel.Text = "1";
				CurrentAnsweredPageLabel.Text = "1";
			}

			// Should we disable the next link?
			if (totalPages == 1 || totalRecords == 0)
			{
				PreviousAnsweredPageLinkButton.Enabled = false;
				NextAnsweredPageLinkButton.Enabled = false;
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
			this.PreviousPendingPageLinkButton.Click += new System.EventHandler(this.PreviousPendingPageLinkButton_Click);
			this.NextPendingPageLinkButton.Click += new System.EventHandler(this.NextPendingPageLinkButton_Click);
			this.DeleteRequestButton.Click += new System.EventHandler(this.DeleteRequestButton_Click);
			this.AnsweredEmailsDatagrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.AnsweredEmailsDatagrid_ItemDataBound);
			this.PreviousAnsweredPageLinkButton.Click += new System.EventHandler(this.PreviousAnsweredPageLinkButton_Click);
			this.NextAnsweredPageLinkButton.Click += new System.EventHandler(this.NextAnsweredPageLinkButton_Click);
			this.DeleteVoterButton.Click += new System.EventHandler(this.DeleteVoterButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void NextPendingPageLinkButton_Click(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentPendingPageLabel.Text);
			int totalPages = int.Parse(TotalPendingPagesLabel.Text);

			// Increment the current page index.
			if (currentPage < totalPages)
			{

			   //PendingEmailsDataGrid.CurrentPageIndex++;
                CurrentPageIndex++;
				// Get the data for the DataGrid.

                BindPendingData();
				// Should we disable the previous link?
				if (CurrentPageIndex +1<= totalPages)
				{

					PreviousPendingPageLinkButton.Enabled = true;
				}

				// Should we enable the next link?
				if (CurrentPageIndex +1== totalPages || totalPages == 1)
				{

					NextPendingPageLinkButton.Enabled = false;
				}
                
			}		
		}
        private int CurrentPageIndex
        {
            get
            {
                if (ViewState["CurrentPageIndex"] == null) return 0;
                else  return  int.Parse(ViewState["CurrentPageIndex"].ToString());
            }
            set
            {
                ViewState["CurrentPageIndex"] = value;
            }
        }
		private void PreviousPendingPageLinkButton_Click(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentPendingPageLabel.Text);
			int totalPages = int.Parse(TotalPendingPagesLabel.Text);

			// Decrement the current page index.
			if (currentPage >= 1)
			{

				//PendingEmailsDataGrid.CurrentPageIndex--;
                CurrentPageIndex--;
				// Get the data for the DataGrid.
				BindPendingData();

				// Should we disable the previous link?
				if (CurrentPageIndex+1 == 1)
				{

					PreviousPendingPageLinkButton.Enabled = false;
				}

				// Should we enable the next link?
				if (totalPages > 1)
				{

					NextPendingPageLinkButton.Enabled = true;
				}
			}	
		}

		private void DeleteRequestButton_Click(object sender, System.EventArgs e)
		{
			RowSelectorColumn rsc = PendingEmailsDataGrid.Columns[0] as RowSelectorColumn;
			int deletedRows = 0;
			foreach( Int32 selectedIndex in rsc.SelectedIndexes ) 
			{
				new Voter().DeleteVoterInvitation(int.Parse(PendingEmailsDataGrid.Items[selectedIndex].Cells[3].Text),
					int.Parse(PendingEmailsDataGrid.Items[selectedIndex].Cells[4].Text));
				deletedRows++;
			}

			// All rows of the page deleted, switch to previous page
			if (deletedRows == PendingEmailsDataGrid.Items.Count && PendingEmailsDataGrid.CurrentPageIndex > 1)
			{
				PendingEmailsDataGrid.CurrentPageIndex--;
			}

			BindPendingData();
		}

		private void PreviousAnsweredPageLinkButton_Click(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentAnsweredPageLabel.Text);
			int totalPages = int.Parse(TotalAnsweredPagesLabel.Text);

			// Decrement the current page index.
			if (currentPage >= 1)
			{

				//AnsweredEmailsDatagrid.CurrentPageIndex--;
                CurrentPageIndex--;

				// Get the data for the DataGrid.
				BindAnsweredData();

				// Should we disable the previous link?
				if (CurrentPageIndex+1 == 1)
				{

					PreviousAnsweredPageLinkButton.Enabled = false;
				}

				// Should we enable the next link?
				if (totalPages > 1)
				{

					NextAnsweredPageLinkButton.Enabled = true;
				}
			}
		}

		private void NextAnsweredPageLinkButton_Click(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentAnsweredPageLabel.Text);
			int totalPages = int.Parse(TotalAnsweredPagesLabel.Text);

			// Increment the current page index.
			if (currentPage < totalPages)
			{

				//AnsweredEmailsDatagrid.CurrentPageIndex++;
                CurrentPageIndex++;

				// Get the data for the DataGrid.
				BindAnsweredData();

				// Should we disable the previous link?
				if (CurrentPageIndex+1 <= totalPages)
				{

					PreviousAnsweredPageLinkButton.Enabled = true;
				}

				// Should we enable the next link?
				if (CurrentPageIndex+1 == totalPages || totalPages == 1)
				{

					NextAnsweredPageLinkButton.Enabled = false;
				}
			}		
		}

		private void AnsweredEmailsDatagrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				HyperLink reportLink = new HyperLink();
				reportLink.NavigateUrl = string.Format("{0}?surveyid={1}&voterid={2}",
					UINavigator.VoterReport,SurveyId, DataBinder.Eval(e.Item.DataItem,"VoterId"));
					//reportLink.Text = GetPageResource("ViewReport");
                reportLink.Text = "<img src='../Images/view.png'>";
				e.Item.Cells[1].Controls.Add(reportLink);
			}
		}

		private void DeleteVoterButton_Click(object sender, System.EventArgs e)
		{
			RowSelectorColumn rsc = AnsweredEmailsDatagrid.Columns[0] as RowSelectorColumn;
			int deletedRows = 0;
			foreach( Int32 selectedIndex in rsc.SelectedIndexes ) 
			{
				new Voter().DeleteVoterById(int.Parse(AnsweredEmailsDatagrid.DataKeys[selectedIndex].ToString()));
				deletedRows++;
			}

			// All rows of the page deleted, switch to previous page
			if (deletedRows == AnsweredEmailsDatagrid.Items.Count && PendingEmailsDataGrid.CurrentPageIndex > 1)
			{
				AnsweredEmailsDatagrid.CurrentPageIndex--;
			}

			BindAnsweredData();
		}
	}

}
