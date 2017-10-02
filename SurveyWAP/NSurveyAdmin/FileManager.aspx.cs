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
	/// Display the files uploaded by the user
	/// </summary>
    public partial class FileManager : PageBase
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
		protected System.Web.UI.WebControls.DataGrid ValidatedFilesDataGrid;
		protected System.Web.UI.WebControls.LinkButton PreviousValidatedPageLinkButton;
		protected System.Web.UI.WebControls.LinkButton NextValidatedPageLinkButton;
		protected System.Web.UI.WebControls.Literal ExportFilesTitle;
		protected System.Web.UI.WebControls.Label ServerPathLabel;
		protected System.Web.UI.WebControls.TextBox ServerPathTextBox;
		protected System.Web.UI.WebControls.Button ExportFilesButton;
		protected System.Web.UI.WebControls.Literal PathExampleLabel;
		protected System.Web.UI.WebControls.Label CreateGroupsLabel;
		protected System.Web.UI.WebControls.DropDownList FileGroupsDropDownList;
		protected System.Web.UI.WebControls.PlaceHolder FileExportPlaceHolder;
		protected SurveyListControl SurveyList;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetResultsTabs((MsterPageTabs)Page.Master, UITabList.ResultTabs.FileManager);

			SetupSecurity();
			LocalizePage();
		
			if (!Page.IsPostBack)
			{
		
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				ValidatedFilesDataGrid.CurrentPageIndex = 1;
				PreviousValidatedPageLinkButton.Enabled = false;
				BindValidateAnswerFiles();
				BindData();
			}

			DeleteFilesButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +GetPageResource("DeleteFilesConfirmationMessage")+ "')== false) return false;");

			ExportFilesButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +GetPageResource("ExportFilesConfirmationMessage")+ "')== false) return false;");

		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessFileManager, true);
			FileExportPlaceHolder.Visible = CheckRight(NSurveyRights.ExportFiles, false);
		}

		private void LocalizePage()
		{
			UploadedFilesTitle.Text = GetPageResource("UploadedFilesTitle");
			((ButtonColumn)ValidatedFilesDataGrid.Columns[1]).HeaderText = GetPageResource("FileDownloadButton");
            ((ButtonColumn)ValidatedFilesDataGrid.Columns[1]).Text = GetPageResource("FileDownloadButton");
			((ButtonColumn)ValidatedFilesDataGrid.Columns[2]).HeaderText = GetPageResource("BoundColumnPostedBy");
			((ButtonColumn)ValidatedFilesDataGrid.Columns[2]).Text = GetPageResource("FileVoterDetailsButton");
			((BoundColumn)ValidatedFilesDataGrid.Columns[3]).HeaderText = GetPageResource("BoundColumnFileName");
			((BoundColumn)ValidatedFilesDataGrid.Columns[4]).HeaderText = GetPageResource("BoundColumnFileType");
			((TemplateColumn)ValidatedFilesDataGrid.Columns[5]).HeaderText = GetPageResource("BoundColumnFileSize");
            ((BoundColumn)ValidatedFilesDataGrid.Columns[6]).HeaderText = GetPageResource("BoundColumnUploadDate");
            ((BoundColumn)ValidatedFilesDataGrid.Columns[7]).HeaderText = GetPageResource("GroupGuid");
            ((BoundColumn)ValidatedFilesDataGrid.Columns[8]).HeaderText = GetPageResource("VoterID");

            CreateGroupsLabel.Text = GetPageResource("CreateGroupsLabel");
			ExportFilesTitle.Text = GetPageResource("ExportFilesTitle");
			ServerPathLabel.Text = GetPageResource("ServerPathLabel");
			ExportFilesButton.Text = GetPageResource("ExportFilesButton");
			PathExampleLabel.Text = string.Format(GetPageResource("PathExampleLabel"), Server.MapPath("/").ToLower());
			PreviousValidatedPageLinkButton.Text = GetPageResource("PreviousValidatedPageLinkButton");
			NextValidatedPageLinkButton.Text = GetPageResource("NextValidatedPageLinkButton");
			DeleteFilesButton.Text = GetPageResource("DeleteFilesButton");
		}

		private void BindData()
		{
			FileGroupsDropDownList.Items.Add(new ListItem(GetPageResource("NoSubDirectoryOption"), "1"));
			FileGroupsDropDownList.Items.Add(new ListItem(GetPageResource("VoterSubDirectoryOption"), "2"));
			FileGroupsDropDownList.Items.Add(new ListItem(GetPageResource("GuidSubDirectoryOption"), "3"));
		}

		/// <summary>
		/// Get the current DB uploaded files of validated users
		/// bind the datagrid and set the correct pages
		/// </summary>
		private void BindValidateAnswerFiles()
		{
			int totalRecords = 0, totalPages = 0;
			ValidatedFilesDataGrid.DataMember = "Files";
			ValidatedFilesDataGrid.DataKeyField = "FileId";
			ValidatedFilesDataGrid.DataSource = 
				new Answers().GetValidatedFileAnswers(SurveyId, ValidatedFilesDataGrid.CurrentPageIndex, 25, out totalRecords);
			ValidatedFilesDataGrid.DataBind();
			CurrentPendingPageLabel.Text = ValidatedFilesDataGrid.CurrentPageIndex.ToString();

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
				PreviousValidatedPageLinkButton.Enabled = false;
				NextValidatedPageLinkButton.Enabled = false;
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
			this.ValidatedFilesDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.ValidatedFilesDataGrid_ItemCommand);
			this.ValidatedFilesDataGrid.SelectedIndexChanged += new System.EventHandler(this.ValidatedFilesDataGrid_SelectedIndexChanged);
			this.PreviousValidatedPageLinkButton.Click += new System.EventHandler(this.PreviousValidatedPageLinkButton_Click);
			this.NextValidatedPageLinkButton.Click += new System.EventHandler(this.NextValidatedPageLinkButton_Click_1);
			this.DeleteFilesButton.Click += new System.EventHandler(this.DeleteFilesButton_Click);
			this.ExportFilesButton.Click += new System.EventHandler(this.ExportFilesButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void PreviousValidatedPageLinkButton_Click(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentPendingPageLabel.Text);
			int totalPages = int.Parse(TotalPendingPagesLabel.Text);

			// Decrement the current page index.
			if (currentPage > 1)
			{
				ValidatedFilesDataGrid.CurrentPageIndex--;

				// Get the data for the DataGrid.
				BindValidateAnswerFiles();

				// Should we disable the previous link?
				if (ValidatedFilesDataGrid.CurrentPageIndex == 1)
				{

					PreviousValidatedPageLinkButton.Enabled = false;
				}

				// Should we enable the next link?
				if (totalPages > 1)
				{

					NextValidatedPageLinkButton.Enabled = true;
				}
			}	
		}

		private void NextValidatedPageLinkButton_Click_1(object sender, System.EventArgs e)
		{
			// Declare local data members.
			int currentPage = int.Parse(CurrentPendingPageLabel.Text);
			int totalPages = int.Parse(TotalPendingPagesLabel.Text);

			// Increment the current page index.
			if (currentPage < totalPages)
			{

				ValidatedFilesDataGrid.CurrentPageIndex++;

				// Get the data for the DataGrid.
				BindValidateAnswerFiles();

				// Should we disable the previous link?
				if (ValidatedFilesDataGrid.CurrentPageIndex <= totalPages)
				{

					PreviousValidatedPageLinkButton.Enabled = true;
				}

				// Should we enable the next link?
				if (ValidatedFilesDataGrid.CurrentPageIndex == totalPages || totalPages == 1)
				{

					NextValidatedPageLinkButton.Enabled = false;
				}
			}	
		}

		private void ValidatedFilesDataGrid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UINavigator.NavigateToVoterReport(SurveyId,
				int.Parse(ValidatedFilesDataGrid.Items[ValidatedFilesDataGrid.SelectedIndex].Cells[8].Text.ToString()), 
				MenuIndex);
		}

		private void DeleteFilesButton_Click(object sender, System.EventArgs e)
		{
			RowSelectorColumn rsc = ValidatedFilesDataGrid.Columns[0] as RowSelectorColumn;
			int deletedRows = 0;
			foreach( Int32 selectedIndex in rsc.SelectedIndexes ) 
			{
				new Answer().DeleteAnswerFile(int.Parse(ValidatedFilesDataGrid.DataKeys[selectedIndex].ToString()),
					ValidatedFilesDataGrid.Items[selectedIndex].Cells[7].Text);
				deletedRows++;
			}

			// All rows of the page deleted, switch to previous page
			if (deletedRows == ValidatedFilesDataGrid.Items.Count && ValidatedFilesDataGrid.CurrentPageIndex > 1)
			{
				ValidatedFilesDataGrid.CurrentPageIndex--;
			}

			BindValidateAnswerFiles();
		}


		/// <summary>
		/// Sends the file to the client
		/// </summary>
        private void SendFile(int fileId, string groupGuid) 
		{
			FileData files = new Answers().GetAnswerFile(fileId, groupGuid);

			if (files.Files.Rows.Count > 0)
			{
				FileData.FilesRow file = (FileData.FilesRow)files.Files.Rows[0];
				Byte[] fileData = new Answers().GetAnswerFileData(fileId, groupGuid);

				// Clear buffer and send the file
				Context.Response.Clear();
				Context.Response.ContentType = file.FileType != null && file.FileType.Length > 0 ?
				file.FileType : "application/octet-stream";
				Context.Response.AddHeader("Content-Disposition", "attachment; filename=\""+file.FileName+"\"");
				Context.Response.BinaryWrite(fileData);
				Context.Response.End();
			}
		}

		private void ExportFilesButton_Click(object sender, System.EventArgs e)
		{
			// Check if the user has correctly ended his path
			if (ServerPathTextBox.Text.Length > 0 && 
				ServerPathTextBox.Text[ServerPathTextBox.Text.Length-1] != '\\')
			{
					ServerPathTextBox.Text += '\\';
			}

			if (ServerPathTextBox.Text.Length == 0)
			{
                ((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("PleaseEnterPathMessage"));
				MessageLabel.Visible = true;
				return;
			}
			else if (!Directory.Exists(ServerPathTextBox.Text))
			{
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("PathNotExistMessage"));
				MessageLabel.Visible = true;
				return;
			}
	
			int exportedSize = new Answer().ExportAnswerFilesToDirectory(SurveyId, ServerPathTextBox.Text, (FileExportMode)int.Parse(FileGroupsDropDownList.SelectedValue)); 

            ((PageBase)Page).ShowNormalMessage(MessageLabel,string.Format(GetPageResource("FilesWrittenMessage"), 
				(Math.Round((double)exportedSize/1048576*100000)/100000).ToString("0.##"), ServerPathTextBox.Text));
			MessageLabel.Visible = true;
		}

        protected void ValidatedFilesDataGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                SendFile(int.Parse(ValidatedFilesDataGrid.DataKeys[e.Item.ItemIndex].ToString()),
                    ValidatedFilesDataGrid.Items[e.Item.ItemIndex].Cells[7].Text);
            }
        }
	}

}
