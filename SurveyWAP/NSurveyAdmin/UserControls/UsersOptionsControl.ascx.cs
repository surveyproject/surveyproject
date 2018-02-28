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

namespace Votations.NSurvey.WebAdmin.UserControls
{
	using System;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using Votations.NSurvey;
	using Votations.NSurvey.Data;
	using Votations.NSurvey.DataAccess;
	using Votations.NSurvey.BusinessRules;
	using Votations.NSurvey.Enums;
	using Microsoft.VisualBasic;
	using System.Text.RegularExpressions;
	using Votations.NSurvey.UserProvider;
	using Votations.NSurvey.Web;
    using Votations.NSurvey.WebAdmin;
    using Votations.NSurvey.WebAdmin.Code;
    using System.Linq;
	/// <summary>
	/// Survey data CU methods
	/// </summary>
    public partial class UsersOptionsControl : UserControl
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label UserOptionTitleLabel;
		protected System.Web.UI.WebControls.Button ApplyChangesButton;
		protected System.Web.UI.WebControls.Button DeleteUserButton;
		protected System.Web.UI.WebControls.Label UserNameLabel;
		protected System.Web.UI.WebControls.Label UserPasswordLabel;
		protected System.Web.UI.WebControls.Label UserFirstNameLabel;
		protected System.Web.UI.WebControls.Label UserLastNameLabel;
		protected System.Web.UI.WebControls.Label UserEmailLabel;
		protected System.Web.UI.WebControls.Label UserIsAdministratorLabel;
		protected System.Web.UI.WebControls.TextBox UserNameTextBox;
		protected System.Web.UI.WebControls.TextBox FirstNameTextBox;
		protected System.Web.UI.WebControls.TextBox LastNameTextBox;
		protected System.Web.UI.WebControls.TextBox EmailTextBox;
		protected System.Web.UI.WebControls.CheckBox IsAdminCheckBox;
		protected System.Web.UI.WebControls.CheckBox HasSurveyAccessCheckBox;
		protected System.Web.UI.WebControls.Literal RolesLabel;
		protected System.Web.UI.WebControls.ListBox RolesListBox;
		protected System.Web.UI.WebControls.ListBox UserRolesListBox;
		protected System.Web.UI.WebControls.ListBox SurveysListBox;
		protected System.Web.UI.WebControls.ListBox UserSurveysListBox;
		protected System.Web.UI.WebControls.Literal AvailableRolesLabel;
		protected System.Web.UI.WebControls.Literal UserRolesLabel;
		protected System.Web.UI.WebControls.TextBox PasswordTextBox;
		protected System.Web.UI.WebControls.PlaceHolder ExtendedSettingsPlaceHolder;
		protected System.Web.UI.WebControls.Literal UserSurveyAssignedLabel;
		protected System.Web.UI.WebControls.Literal UnAssignedSurveysLabel;
		protected System.Web.UI.WebControls.Literal AssignedSurveysLabel;
		protected System.Web.UI.WebControls.Label AssignAllSurveysLabel;
		protected System.Web.UI.WebControls.PlaceHolder NSurveyUserPlaceHolder;
		protected System.Web.UI.WebControls.Button CreateNewUserButton;
		public event EventHandler OptionChanged;

		/// <summary>
		/// Id of the user to edit
		/// if no id is given put the 
		/// usercontrol in creation mode
		/// </summary>
		public int UserId
		{
			get { return (ViewState["UserId"]==null) ? -1 : int.Parse(ViewState["UserId"].ToString()); }
            set { 
                ViewState["UserId"] = value;
                if (value < 0)
                    SwitchToCreationMode();
                else
                    SwitchToEditionMode(); 
            }
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			MessageLabel.Visible = false;
			LocalizePage();
			
			// Check if any answer type id has been assigned
			if (UserId == -1)
			{
				SwitchToCreationMode();
			}
			else
			{
				SwitchToEditionMode();
			}
		}

		private void LocalizePage()
		{
			CreateNewUserButton.Text = ((PageBase)Page).GetPageResource("CreateNewUserButton");
			ApplyChangesButton.Text = ((PageBase)Page).GetPageResource("ApplyChangesButton");
			DeleteUserButton.Text = ((PageBase)Page).GetPageResource("DeleteUserButton");
			UserPasswordLabel.Text = ((PageBase)Page).GetPageResource("UserPasswordLabel");
			UserNameLabel.Text = ((PageBase)Page).GetPageResource("UserNameLabel");
			UserFirstNameLabel.Text = ((PageBase)Page).GetPageResource("UserFirstNameLabel");
			UserLastNameLabel.Text = ((PageBase)Page).GetPageResource("UserLastNameLabel");
			UserEmailLabel.Text = ((PageBase)Page).GetPageResource("UserEmailLabel");
			AssignAllSurveysLabel.Text = ((PageBase)Page).GetPageResource("AssignAllSurveysLabel");
			UserSurveyAssignedLabel.Text = ((PageBase)Page).GetPageResource("UserSurveyAssignedLabel");
			UnAssignedSurveysLabel.Text = ((PageBase)Page).GetPageResource("UnAssignedSurveysLabel");
			AssignedSurveysLabel.Text = ((PageBase)Page).GetPageResource("AssignedSurveysLabel");
			RolesLabel.Text = ((PageBase)Page).GetPageResource("RolesLabel");
			AvailableRolesLabel.Text = ((PageBase)Page).GetPageResource("AvailableRolesLabel");
			UserRolesLabel.Text = ((PageBase)Page).GetPageResource("UserRolesLabel");
			UserIsAdministratorLabel.Text = ((PageBase)Page).GetPageResource("UserIsAdministratorLabel");
		}


		/// <summary>
		/// Setup the control in creation mode
		/// </summary>
		private void SwitchToCreationMode()
		{
			if (_userProvider is INSurveyUserProvider)
			{
				// Creation mode
				UserOptionTitleLabel.Text = ((PageBase)Page).GetPageResource("NewUserTitle");;
				CreateNewUserButton.Visible = true;
				ApplyChangesButton.Visible = false;
				DeleteUserButton.Visible = false;
				ExtendedSettingsPlaceHolder.Visible = false;
				NSurveyUserPlaceHolder.Visible = true;
                this.Visible = true;
			}
			else if (this.Visible)
			{
				throw new ApplicationException("Specified user provider doesn't support creation of users");
			}

			HasSurveyAccessCheckBox.AutoPostBack = false;
		}

		/// <summary>
		/// Setup the control in edition mode
		/// </summary>
		private void SwitchToEditionMode()
		{
			DeleteUserButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +((PageBase)Page).GetPageResource("DeleteUserConfirmationMessage")+ "')== false) return false;");

			UserOptionTitleLabel.Text = ((PageBase)Page).GetPageResource("EditUserTitle");
			CreateNewUserButton.Visible = false;
			ApplyChangesButton.Visible = true;
			DeleteUserButton.Visible = !(UserId == ((PageBase)Page).NSurveyUser.Identity.UserId) && _userProvider is INSurveyUserProvider;
			ExtendedSettingsPlaceHolder.Visible = true;
			HasSurveyAccessCheckBox.AutoPostBack = true;
		}
		
		/// <summary>
		/// Get the current DB data and fill 
		/// the fields with them
		/// </summary>
		public void BindFields()
		{
            if (UserId < 0)
            {
                ViewState["UserName"] = string.Empty;
                UserNameTextBox.Text = string.Empty;
                FirstNameTextBox.Text = string.Empty;
                LastNameTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                NSurveyUserPlaceHolder.Visible = true;
                IsAdminCheckBox.Checked = false;
                HasSurveyAccessCheckBox.Checked = false;
                SurveysListBox.Enabled = false;
                UserSurveysListBox.Enabled = false;

                return;
            }

			// Check if we can edit extended properties
			if (_userProvider is INSurveyUserProvider)
			{
				NSurveyUserPlaceHolder.Visible = true;

				// Retrieve the user data
				NSurveyUserData userData = new Users().GetUserById(UserId);
				NSurveyUserData.UsersRow user = userData.Users[0];
				ViewState["UserName"] = user.UserName;
				UserNameTextBox.Text = user.UserName;
				FirstNameTextBox.Text = user.FirstName;
				LastNameTextBox.Text = user.LastName;
				EmailTextBox.Text = user.Email;
                // attempt to repopulate the PWTB
                //PasswordTextBox.Text = user.Password;
			}
			else
			{
				NSurveyUserPlaceHolder.Visible = false;
			}

			UserSettingData userSettings = new Users().GetUserSettings(UserId);
			if (userSettings.UserSettings.Rows.Count > 0)
			{
				IsAdminCheckBox.Checked = userSettings.UserSettings[0].IsAdmin;
				HasSurveyAccessCheckBox.Checked = userSettings.UserSettings[0].GlobalSurveyAccess;
			}
			else
			{
				IsAdminCheckBox.Checked = false;
				HasSurveyAccessCheckBox.Checked = false;
			}
			SurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
			UserSurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
			BindSurveyDropDownLists();
		}


		private void BindSurveyDropDownLists()
		{
			SurveysListBox.DataSource = new Surveys().GetUnAssignedSurveysList(UserId).Surveys.OrderBy(x=>x.Title);
			SurveysListBox.DataMember = "Surveys";
			SurveysListBox.DataTextField = "Title";
			SurveysListBox.DataValueField = "SurveyId";
			SurveysListBox.DataBind();

            UserSurveysListBox.DataSource = new Surveys().GetAssignedSurveysList(UserId).Surveys.OrderBy(x => x.Title);
			UserSurveysListBox.DataMember = "Surveys";
			UserSurveysListBox.DataTextField = "Title";
			UserSurveysListBox.DataValueField = "SurveyId";
			UserSurveysListBox.DataBind();

			UserRolesListBox.DataSource = new Roles().GetRolesOfUser(UserId);
			UserRolesListBox.DataMember = "Roles";
			UserRolesListBox.DataTextField = "RoleName";
			UserRolesListBox.DataValueField = "RoleId";
			UserRolesListBox.DataBind();

			RolesListBox.DataSource = new Roles().GetUnassignedRolesToUser(UserId);
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.HasSurveyAccessCheckBox.CheckedChanged += new System.EventHandler(this.HasSurveyAccessCheckBox_CheckedChanged);
			this.SurveysListBox.SelectedIndexChanged += new System.EventHandler(this.SurveysListBox_SelectedIndexChanged);
			this.UserSurveysListBox.SelectedIndexChanged += new System.EventHandler(this.UserSurveysListBox_SelectedIndexChanged);
			this.RolesListBox.SelectedIndexChanged += new System.EventHandler(this.RolesListBox_SelectedIndexChanged);
			this.UserRolesListBox.SelectedIndexChanged += new System.EventHandler(this.UserRolesListBox_SelectedIndexChanged);
			this.CreateNewUserButton.Click += new System.EventHandler(this.CreateUserButton_Click);
			this.ApplyChangesButton.Click += new System.EventHandler(this.ApplyChangesButton_Click);
			this.DeleteUserButton.Click += new System.EventHandler(this.DeleteUserButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CreateUserButton_Click(object sender, System.EventArgs e)
		{
			if (ValidateFieldOptions())
			{
				NSurveyUserData userData = new NSurveyUserData();
				NSurveyUserData.UsersRow newUser = userData.Users.NewUsersRow();

				if (_userProvider is INSurveyUserProvider)
				{
					//if (PasswordTextBox.Text.Length == 0)                    
                    if (!Regex.IsMatch(PasswordTextBox.Text, @"(?=^.{8,12}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"))

					{
						MessageLabel.Visible = true;
                        ((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("PasswordRequiredMessage"));
						return;
					}

					newUser.UserName = UserNameTextBox.Text;
                    var sec = new LoginSecurity();
                    newUser.PasswordSalt = sec.CreateSaltKey(5);
					newUser.Password = sec.CreatePasswordHash(PasswordTextBox.Text,newUser.PasswordSalt);
					newUser.Email = EmailTextBox.Text;
					newUser.FirstName = FirstNameTextBox.Text;
					newUser.LastName = LastNameTextBox.Text;
					userData.Users.Rows.Add(newUser);
					((INSurveyUserProvider)_userProvider).AddUser(userData);
				}

				if (userData.Users.Rows.Count > 0)
				{
					UserSettingData userSettings = new UserSettingData();
					UserSettingData.UserSettingsRow newUserSettings = userSettings.UserSettings.NewUserSettingsRow();
					newUserSettings.UserId = userData.Users[0].UserId;
					newUserSettings.IsAdmin = IsAdminCheckBox.Checked;
					newUserSettings.GlobalSurveyAccess = HasSurveyAccessCheckBox.Checked;
					userSettings.UserSettings.Rows.Add(newUserSettings);
					new User().AddUserSettings(userSettings);
				}
				UINavigator.NavigateToUserManager(((PageBase)Page).getSurveyId(),((PageBase)Page).MenuIndex);
			}

		}

		/// <summary>
		/// Validate all fields to make sure 
		/// no errors has occured
		/// </summary>
		private bool ValidateFieldOptions()
		{		
			if (!(_userProvider is INSurveyUserProvider))
			{
				return true;
			}

			if (UserNameTextBox.Text.Length == 0)
			{
				MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("UserNameRequiredMessage"));
				RePopulatePasswordBox();
				return false;
			}

			int userNameId = new Users().GetUserByIdFromUserName(UserNameTextBox.Text);
			if (userNameId != -1 && userNameId != UserId)
			{
				MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("UserNameTakenMessage"));
				RePopulatePasswordBox();
				return false;
			}

			Regex re = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + 
				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
			if (EmailTextBox.Text.Length > 0 && 
				!re.IsMatch(EmailTextBox.Text))
			{
				MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("InvalidEmailMessage"));
				RePopulatePasswordBox();
				return false;
			}

			return true;
		}

		private void RePopulatePasswordBox()
		{
			PasswordTextBox.Attributes.Add("value", PasswordTextBox.Text);
		}
		

		protected void OnOptionChanged()
		{
			if (OptionChanged != null)
			{
				OptionChanged(this, EventArgs.Empty);
			}
		}

		private void ApplyChangesButton_Click(object sender, System.EventArgs e)
		{
			if (ValidateFieldOptions())
			{
				if (new Users().IsAdministrator(UserId) && !IsAdminCheckBox.Checked &&  new Users().GetAdminCount() == 1)
				{
					MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("CannotDeleteLastAdminMessage"));
					return;
				}

				if (_userProvider is INSurveyUserProvider)
				{
					NSurveyUserData userData = new NSurveyUserData();
					NSurveyUserData.UsersRow updatedUser = userData.Users.NewUsersRow();
					updatedUser.UserId = UserId;
					updatedUser.UserName = UserNameTextBox.Text;

					// if no password was specified the old one will be kept
                    if (PasswordTextBox.Text.Length > 0)
                    {
                        if (!Regex.IsMatch(PasswordTextBox.Text, @"(?=^.{8,12}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"))
                        {
                            MessageLabel.Visible = true;
                            ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("PasswordRequiredMessage"));
                            return;
                        }
                        else
                        {
                            var sec = new LoginSecurity();
                            updatedUser.PasswordSalt = sec.CreateSaltKey(5);
                            updatedUser.Password = sec.CreatePasswordHash(PasswordTextBox.Text, updatedUser.PasswordSalt);
                        }

                    }
                    else
                    {
                        updatedUser.Password = null;
                        updatedUser.PasswordSalt = null;
                    }
				
					updatedUser.Email = EmailTextBox.Text;
					updatedUser.FirstName = FirstNameTextBox.Text;
					updatedUser.LastName = LastNameTextBox.Text;
					userData.Users.Rows.Add(updatedUser);
					((INSurveyUserProvider)_userProvider).UpdateUser(userData);
				}

				UserSettingData userSettings = new UserSettingData();
				UserSettingData.UserSettingsRow newUserSettings = userSettings.UserSettings.NewUserSettingsRow();
				newUserSettings.UserId = UserId;
				newUserSettings.IsAdmin = IsAdminCheckBox.Checked;
				newUserSettings.GlobalSurveyAccess = HasSurveyAccessCheckBox.Checked;
				userSettings.UserSettings.Rows.Add(newUserSettings);
				new User().UpdateUserSettings(userSettings);

				// Notifiy containers that data has changed
				OnOptionChanged();

                BindSurveyDropDownLists();
				MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("UserUpdatedMessage"));
			}
		}

		private void DeleteUserButton_Click(object sender, System.EventArgs e)
		{
			if (new Users().IsAdministrator(UserId) && new Users().GetAdminCount() == 1)
			{
				MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("CannotDeleteLastAdminMessage"));
				return;
			}

			((INSurveyUserProvider)_userProvider).DeleteUserById(UserId);
			UserId = -1;
			Visible = false;
			OnOptionChanged();
            UINavigator.NavigateToUserManager(((PageBase)Page).getSurveyId(), ((PageBase)Page).MenuIndex);
		}

		private void HasSurveyAccessCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			SurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
			UserSurveysListBox.Enabled = !HasSurveyAccessCheckBox.Checked;
		}

		private void SurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			new Survey().AssignUserToSurvey(int.Parse(SurveysListBox.SelectedValue), UserId);
			BindSurveyDropDownLists();
		}

		private void UserSurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			new Survey().UnAssignUserFromSurvey(int.Parse(UserSurveysListBox.SelectedValue), UserId);
			BindSurveyDropDownLists();
		}

		private void RolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			new Role().AddRoleToUser(int.Parse(RolesListBox.SelectedValue), UserId);
			BindSurveyDropDownLists();
		}

		private void UserRolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			new Role().DeleteUserRole(int.Parse(UserRolesListBox.SelectedValue), UserId);
			BindSurveyDropDownLists();
		}

		private IUserProvider _userProvider = UserFactory.Create();

        protected void DeleteUserButton_Click1(object sender, EventArgs e)
        {

        }
	}
}
