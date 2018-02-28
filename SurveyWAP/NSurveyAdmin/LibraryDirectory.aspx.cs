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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Web;
using System.Web.UI.HtmlControls;

namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Display the template directories
    /// </summary>
    public partial class LibraryDirectory: PageBase
    {
        protected System.Web.UI.WebControls.Label MessageLabel;
        new protected HeaderControl Header;
        protected System.Web.UI.WebControls.Literal AddLibraryTitle;
        protected System.Web.UI.WebControls.Literal LibraryNameLabel;
        protected System.Web.UI.WebControls.TextBox LibraryNameTextBox;
        protected System.Web.UI.WebControls.Button AddNewLibraryButton;
        protected System.Web.UI.WebControls.Button AddLibraryButton;
        protected System.Web.UI.WebControls.Button UpdateLibraryButton;
        protected System.Web.UI.WebControls.Button DeleteLibraryButton;
        protected System.Web.UI.WebControls.DataList LibraryDataList;
        protected System.Web.UI.WebControls.PlaceHolder editplaceholder;
        protected System.Web.UI.WebControls.Literal LibraryTitle;

        public int selectedTabIndex = 0;
        public int editMode = 0;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // This tab can be Edit or List 
            Tab1.InnerText = GetPageResource("LibraryListHyperlink");

            LocalizePage();
            if(!IsPostBack)
            this.DataBind();
            // Get selected tab
            if (!string.IsNullOrEmpty(Request.Params["tabindex"]))
            {
                string[] idx = Request.Params["tabindex"].Split(',');
                selectedTabIndex = int.Parse(idx[idx.Length - 1]);
            }

            editMode = int.Parse((string.IsNullOrEmpty(Request.Params["editmode"])) ? "0" : Request.Params["editmode"]);

            if (selectedTabIndex == 1)
                editMode = 0;

            ToggleEdit();
            SetupSecurity();
            
            if (!Page.IsPostBack)
            {
                // Header.SurveyId = SurveyId;
                ((Wap)Master).HeaderControl.SurveyId = getSurveyId();// It is OK If survey Id does not exist
                BindData();
            }
        }

        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.AccessLibrary, true);
            if (selectedTabIndex > 0) CheckRight(NSurveyRights.ManageLibrary, true);

            // if (Tab2.Visible) CheckRight(NSurveyRights.ManageLibrary, true);
            //AddNewLibraryButton.Visible = NSurveyUser.Identity.IsAdmin || CheckRight(NSurveyRights.ManageLibrary, false);
        }

        /// <summary>
        /// Get the current DB stats and fill 
        /// the label with them
        /// </summary>
        private void BindData()
        {
            // Retrieve the library data
            LibraryData libraryData =
                new Libraries().GetLibraries();
            gridLibraries.DataSource = libraryData;
            gridLibraries.DataMember = "Libraries";
            gridLibraries.DataBind();
        }

        public void EditLibrary(object sender, CommandEventArgs e)
        {
            ((PageBase)Page).LibraryId = int.Parse((string)e.CommandArgument);
            Response.Redirect(Page.ResolveUrl("~/NSurveyAdmin/LibraryTemplates.aspx?libraryid=" + (string)e.CommandArgument));
        }

        public void EditLibraryProperties(object sender, CommandEventArgs e)
        {
            CheckRight(NSurveyRights.ManageLibrary, true);
            editMode = 1;
            ToggleEdit();
            Tab1.InnerHtml = GetPageResource("LibraryTabEdit");
            lbnEdit.LibraryEditMode = true;
            lbnEdit.LibraryId = int.Parse((string)e.CommandArgument);
            lbnEdit.FillData();
        }

        public void EditBackButton(object sender, CommandEventArgs e)
        {
            editMode = 0;
            ToggleEdit();
        }

        protected void ToggleEdit()
        {
            LibraryList.Visible = (editMode == 0);
            LibraryEdit.Visible = (editMode == 1);
        }



        //-------------------------------------------------------------------------------------------------------------------

        private void LocalizePage()
        {

         
            gridLibraries.Columns[0].HeaderText = GetPageResource("LibraryName");
            gridLibraries.Columns[1].HeaderText = GetPageResource("Description");
            gridLibraries.Columns[2].HeaderText = GetPageResource("NumberOfQuestions");
            gridLibraries.Columns[3].HeaderText = GetPageResource("EditText");

           LibraryDirectoryLegend.Text = GetPageResource("LibraryDirectoryLegend");
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
            /*this.LibraryDataList.ItemCreated += new System.Web.UI.WebControls.DataListItemEventHandler(this.LibraryDataList_ItemCreated);
            this.LibraryDataList.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.LibraryDataList_ItemCommand);
            this.AddNewLibraryButton.Click += new System.EventHandler(this.AddNewLibraryButton_Click);
            this.AddLibraryButton.Click += new System.EventHandler(this.AddLibraryButton_Click);
            this.UpdateLibraryButton.Click += new System.EventHandler(this.UpdateLibraryButton_Click);
            this.DeleteLibraryButton.Click += new System.EventHandler(this.DeleteLibraryButton_Click);*/
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        private void AddLibraryButton_Click(object sender, System.EventArgs e)
        {
            if (LibraryNameTextBox.Text.Length == 0)
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingLibraryNameMessage"));
                MessageLabel.Visible = true;
                return;
            }
            else
            {
                LibraryData libraryData = new LibraryData();
                LibraryData.LibrariesRow library = libraryData.Libraries.NewLibrariesRow();
                library.LibraryName = LibraryNameTextBox.Text;
                libraryData.Libraries.Rows.Add(library);
                new Library().AddLibrary(libraryData);
                LibraryNameTextBox.Text = string.Empty;
                BindData();
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("AddedLibraryNameMessage"));
                MessageLabel.Visible = true;
            }
        }


        private void LibraryDataList_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            LibraryData library = new Libraries().GetLibraryById(int.Parse(e.CommandArgument.ToString()));
            LibraryNameTextBox.Text = library.Libraries[0].LibraryName;
            ViewState["LibraryId"] = library.Libraries[0].LibraryId;
            AddLibraryButton.Visible = false;
            AddLibraryTitle.Text = GetPageResource("UpdateLibraryTitle");
            UpdateLibraryButton.Visible = true;
            DeleteLibraryButton.Visible = true;
            editplaceholder.Visible = true;
        }

        private void UpdateLibraryButton_Click(object sender, System.EventArgs e)
        {
            if (LibraryNameTextBox.Text.Length == 0)
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingLibraryNameMessage"));
                MessageLabel.Visible = true;
                editplaceholder.Visible = true;
                return;
            }
            else
            {
                LibraryData libraryData = new LibraryData();
                LibraryData.LibrariesRow library = libraryData.Libraries.NewLibrariesRow();
                library.LibraryId = int.Parse(ViewState["LibraryId"].ToString());
                library.LibraryName = LibraryNameTextBox.Text;
                libraryData.Libraries.Rows.Add(library);
                new Library().UpdateLibrary(libraryData);
                LibraryNameTextBox.Text = string.Empty;
                BindData();
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("UpdatedLibraryNameMessage"));
                MessageLabel.Visible = true;
                editplaceholder.Visible = false;
            }
        }

        private void DeleteLibraryButton_Click(object sender, System.EventArgs e)
        {
            new Library().DeleteLibrary(int.Parse(ViewState["LibraryId"].ToString()));
            BindData();
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("DeletedLibraryNameMessage"));
            MessageLabel.Visible = true;
            editplaceholder.Visible = false;
            LibraryNameTextBox.Text = string.Empty;
        }

        private void AddNewLibraryButton_Click(object sender, System.EventArgs e)
        {
            UpdateLibraryButton.Visible = false;
            DeleteLibraryButton.Visible = false;
            AddLibraryButton.Visible = true;
            AddLibraryTitle.Text = GetPageResource("AddLibraryTitle");
            editplaceholder.Visible = true;
        }

        private void LibraryDataList_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            e.Item.FindControl("EditDirectoryHyperLink").Visible =
                NSurveyUser.Identity.IsAdmin || CheckRight(NSurveyRights.ManageLibrary, false);
        }

        protected void gridLibraries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }

}
