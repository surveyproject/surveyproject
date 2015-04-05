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
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.UserProvider;
using Votations.NSurvey.WebAdmin.UserControls;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the user editor
	/// </summary>
    public partial class UsersManager : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		new protected HeaderControl Header;
        Votations.NSurvey.SQLServerDAL.User UsersData;
        public int selectedTabIndex = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetupSecurity();
			LocalizePage();

            // Get selected tab
            if (!string.IsNullOrEmpty(Request.Params["tabindex"]))
            {
                string[] idx = Request.Params["tabindex"].Split(',');
                selectedTabIndex = int.Parse(idx[idx.Length - 1]);
            }
            if (selectedTabIndex > 0) btnBack.Visible = false;
            UsersData = new Votations.NSurvey.SQLServerDAL.User();
            SwitchModeDependingOnprevTab();
        

            if (!Page.IsPostBack)
            {
                BindFields();
            }
            else BindGrid(); // postback could be import users
		}
        private void SwitchModeDependingOnprevTab()
        {
            if (ViewState["UMPREVTAB"] != null && Convert.ToInt32(ViewState["UMPREVTAB"])>0 && selectedTabIndex==0) 
            SwitchToListMode();
            ViewState["UMPREVTAB"] = selectedTabIndex;
        }

       
		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessUserManager, true);
			IsSingleUserMode(true);
		}

		private void LocalizePage()
		{
            UserListTitleLabel.Text = ((PageBase)Page).GetPageResource("UserListTitle");
            UserlistFilterOptionLiteral.Text = ((PageBase)Page).GetPageResource("UserlistFilterOptionLiteral");
		}

        public bool IsAdmin(int userId)
        {
            return UsersData.IsAdministrator(userId);
        }

        public void OnUserEdit(Object sender, CommandEventArgs e)
        {
            UsersOptionsControl1.UserId = int.Parse(e.CommandArgument.ToString());                        
            UsersOptionsControl1.BindFields();
            phUsersList.Visible = false;
            btnBack.Visible = true;
        }

        public void EditBackButton(object sender, CommandEventArgs e)
        {
            SwitchToListMode();
        }

        private void SwitchToListMode()
        {
            UsersOptionsControl1.UserId = -1;
            UsersOptionsControl1.BindFields();
            phUsersList.Visible = true;
            BindGrid();
            btnBack.Visible = false;
        }

		/// <summary>
		/// Get the current DB data and fill 
		/// the fields with them
		/// </summary>
		private void BindFields()
		{
			// Header.SurveyId = SurveyId;
            ((Wap)Master).HeaderControl.SurveyId = getSurveyId();
			
            BindGrid();
            btnApplyfilter.Text = GetPageResource("UsersTabApplyFilter");
		}

        private void BindGrid()
        {
            int ?isAdmin = chkAdmin.Checked ? 1: (int?)null;
            gvUsers.DataSource = UsersData.GetAllUsersListByFilter(txtUserName.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, isAdmin);
            gvUsers.DataBind();

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            GridViewRow pagerRow = (GridViewRow)gvUsers.BottomPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }

        public void gvUsers_PageIndexChanged(Object sender, EventArgs e)
        {
            BindGrid();
        }

        public void gvUsers_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
        }

        public void OnApplyFilter(Object sender, EventArgs e)
        {
            BindGrid();
        }

		/// <summary>
		///	Triggered by the type option user control
		///	in case anything was changed we will need to
		///	rebind to display the updates to the user 
		/// </summary>
		private void UsersManager_OptionChanged(object sender, EventArgs e)
		{
			BindFields();
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
        
		private IUserProvider _userProvider = UserFactory.Create();
	}

}
