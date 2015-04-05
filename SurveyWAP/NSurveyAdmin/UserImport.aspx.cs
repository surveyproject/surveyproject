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
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.Data;
using Votations.NSurvey.UserProvider;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.UserControls;
using System.Text.RegularExpressions;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the user creator
	/// </summary>
    public partial class UserImport : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Literal ImportUserLabel;
		protected System.Web.UI.WebControls.Literal UserIsAdministratorLabel;
		protected System.Web.UI.WebControls.CheckBox IsAdminCheckBox;
		protected System.Web.UI.WebControls.Literal AssignAllSurveysLabel;
		protected System.Web.UI.WebControls.CheckBox HasSurveyAccessCheckBox;
		protected System.Web.UI.WebControls.Literal UserSurveyAssignedLabel;
		protected System.Web.UI.WebControls.Literal UnAssignedSurveysLabel;
		protected System.Web.UI.WebControls.Literal AssignedSurveysLabel;
		protected System.Web.UI.WebControls.ListBox SurveysListBox;
		protected System.Web.UI.WebControls.ListBox UserSurveysListBox;
		protected System.Web.UI.WebControls.Literal RolesLabel;
		protected System.Web.UI.WebControls.Literal AvailableRolesLabel;
		protected System.Web.UI.WebControls.Literal UserRolesLabel;
		protected System.Web.UI.WebControls.ListBox RolesListBox;
		protected System.Web.UI.WebControls.ListBox UserRolesListBox;
		protected System.Web.UI.WebControls.TextBox ImportUsersTextBox;
		protected System.Web.UI.WebControls.Button ImportUsersButton;
		protected System.Web.UI.WebControls.Label ImportUsersTitle;
		protected System.Web.UI.WebControls.Label ImportInfo1Label;
		protected System.Web.UI.WebControls.Label ImportInfo2Label;
		protected System.Web.UI.WebControls.Label ImportInfo3Label;
		new protected HeaderControl Header;

		private void Page_Load(object sender, System.EventArgs e)
		{
			LocalizePage();

			SetupSecurity();
			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master).HeaderControl.SurveyId = SurveyId;
				BindSurveyDropDownLists();
			}	
		}

		private void SetupSecurity()
		{
			// Check if we can edit extended properties
			if ((_userProvider is INSurveyUserProvider))
			{
				CheckRight(NSurveyRights.AccessUserManager, true);
			}
			else
			{
				UINavigator.NavigateToAccessDenied(SurveyId, MenuIndex);
			}
		}


		private void LocalizePage()
		{
			ImportUsersButton.Text = GetPageResource("ImportUsersButton");
			ImportUsersTitle.Text = GetPageResource("ImportUsersTitle");
			ImportUserLabel.Text = GetPageResource("ImportUserLabel");
			AssignAllSurveysLabel.Text = GetPageResource("AssignAllSurveysLabel");
			UserSurveyAssignedLabel.Text = GetPageResource("UserSurveyAssignedLabel");
			UnAssignedSurveysLabel.Text = GetPageResource("UnAssignedSurveysLabel");
			AssignedSurveysLabel.Text = GetPageResource("AssignedSurveysLabel");
			RolesLabel.Text = GetPageResource("RolesLabel");
			AvailableRolesLabel.Text = GetPageResource("AvailableRolesLabel");
			UserRolesLabel.Text = GetPageResource("UserRolesLabel");
			UserIsAdministratorLabel.Text = GetPageResource("UserIsAdministratorLabel");
			ImportInfo1Label.Text = GetPageResource("ImportInfo1Label");
			ImportInfo2Label.Text = GetPageResource("ImportInfo2Label");
			ImportInfo3Label.Text = GetPageResource("ImportInfo3Label");
		}


		private void BindSurveyDropDownLists()
		{
			SurveysListBox.DataSource = new Surveys().GetAllSurveysList();
			SurveysListBox.DataMember = "Surveys";
			SurveysListBox.DataTextField = "Title";
			SurveysListBox.DataValueField = "SurveyId";
			SurveysListBox.DataBind();

			RolesListBox.DataSource = new Roles().GetAllRolesList();
			RolesListBox.DataMember = "Roles";
			RolesListBox.DataTextField = "RoleName";
			RolesListBox.DataValueField = "RoleId";
			RolesListBox.DataBind();

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
			this.HasSurveyAccessCheckBox.CheckedChanged += new System.EventHandler(this.HasSurveyAccessCheckBox_CheckedChanged);
			this.SurveysListBox.SelectedIndexChanged += new System.EventHandler(this.SurveysListBox_SelectedIndexChanged);
			this.UserSurveysListBox.SelectedIndexChanged += new System.EventHandler(this.UserSurveysListBox_SelectedIndexChanged);
			this.RolesListBox.SelectedIndexChanged += new System.EventHandler(this.RolesListBox_SelectedIndexChanged);
			this.UserRolesListBox.SelectedIndexChanged += new System.EventHandler(this.UserRolesListBox_SelectedIndexChanged);
			this.ImportUsersButton.Click += new System.EventHandler(this.ImportUsersButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private IUserProvider _userProvider = UserFactory.Create();

		private void ImportUsersButton_Click(object sender, System.EventArgs e)
		{
			Regex re = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

			string[] users = ImportUsersTextBox.Text.Split('\n');
			for (int i=0; i<users.Length;i++)
			{
				string[] user = users[i].Split(',');
				if (user.Length > 4 && user[0].Trim().Length > 0 && user[1].Trim().Length > 0)
				{
					// Check if user already exists in the db
					if (new Users().GetUserByIdFromUserName(user[0]) == -1)
					{
						NSurveyUserData userData = new NSurveyUserData();
						NSurveyUserData.UsersRow newUser = userData.Users.NewUsersRow();
						newUser.UserName = user[0].Trim();
						newUser.Password = new User().EncryptUserPassword(user[1].Trim());
						newUser.Email = user[4].Length > 0 && re.IsMatch(user[4].Trim()) ? 					
							user[4].Trim() : null;
						newUser.FirstName = user[3].Length>0 ? user[3].Trim() : null;
						newUser.LastName = user[2].Length>0 ? user[2].Trim() : null;
						userData.Users.Rows.Add(newUser);
						((INSurveyUserProvider)_userProvider).AddUser(userData);
				
						AddUserSettings(userData.Users[0].UserId);
						AddUserRoles(userData.Users[0].UserId);
						if (!HasSurveyAccessCheckBox.Checked)
						{
							AddUserSurveys(userData.Users[0].UserId);
						}
					}
				}
			}

			MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("UserImportedMessage"));
			ImportUsersTextBox.Text = string.Empty;
			UserRolesListBox.Items.Clear();
			UserSurveysListBox.Items.Clear();
			BindSurveyDropDownLists();
		}

		private void AddUserSettings(int userId)
		{
			UserSettingData userSettings = new UserSettingData();
			UserSettingData.UserSettingsRow newUserSettings = userSettings.UserSettings.NewUserSettingsRow();
			newUserSettings.UserId = userId;
			newUserSettings.IsAdmin = IsAdminCheckBox.Checked;
			newUserSettings.GlobalSurveyAccess = HasSurveyAccessCheckBox.Checked;
			userSettings.UserSettings.Rows.Add(newUserSettings);
			new User().AddUserSettings(userSettings);
		}

		private void AddUserRoles(int userId)
		{
			foreach (ListItem item in UserRolesListBox.Items)
			{
				new Role().AddRoleToUser(int.Parse(item.Value), userId);
			}
		}

		private void AddUserSurveys(int userId)
		{
			foreach (ListItem item in UserSurveysListBox.Items)
			{
				new Survey().AssignUserToSurvey(int.Parse(item.Value), userId);
			}
		}

		private void HasSurveyAccessCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			SurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
			UserSurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
		}

		private void SurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UserSurveysListBox.Items.Add(new ListItem(SurveysListBox.SelectedItem.Text,  SurveysListBox.SelectedItem.Value));
			SurveysListBox.Items.Remove(SurveysListBox.SelectedItem);
		}

		private void UserSurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SurveysListBox.Items.Add(new ListItem(UserSurveysListBox.SelectedItem.Text,  UserSurveysListBox.SelectedItem.Value));
			UserSurveysListBox.Items.Remove(UserSurveysListBox.SelectedItem);
		}

		private void RolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UserRolesListBox.Items.Add(new ListItem(RolesListBox.SelectedItem.Text,  RolesListBox.SelectedItem.Value));
			RolesListBox.Items.Remove(RolesListBox.SelectedItem);
		
		}

		private void UserRolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			RolesListBox.Items.Add(new ListItem(UserRolesListBox.SelectedItem.Text,  UserRolesListBox.SelectedItem.Value));
			UserRolesListBox.Items.Remove(UserRolesListBox.SelectedItem);
		}
	}
}
