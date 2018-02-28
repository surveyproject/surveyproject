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
using System.Web.Security;
using System.Text.RegularExpressions;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.UserProvider;
using Votations.NSurvey.WebAdmin.Code;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    /// <summary>
    /// Auth the login
    /// </summary>
    public partial class LoginBox : System.Web.UI.UserControl
    {
        //protected System.Web.UI.WebControls.Label MessageLabel;
        //protected System.Web.UI.WebControls.Button ValidateCredentialsButton;
        protected System.Web.UI.WebControls.TextBox LoginTextBox;
        //protected System.Web.UI.WebControls.Literal NSurveyAuthenticationTitle;
        //protected System.Web.UI.WebControls.Literal LoginLabel;
        //protected System.Web.UI.WebControls.Literal PasswordLabel;
        //protected System.Web.UI.WebControls.Literal Disclaimer;
        //protected System.Web.UI.WebControls.Literal Introduction;
        protected System.Web.UI.WebControls.Literal HelpGifTitle;
        protected System.Web.UI.WebControls.TextBox PasswordTextBox;
        private IUserProvider _userProvider = UserFactory.Create();

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.ValidateCredentialsButton.Click += new System.EventHandler(this.ValidateCredentialsButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            MessageLabel.Visible = false;
            LocalizePage();
        }

        private void LocalizePage()
        {
            NSurveyAuthenticationTitle.Text = ((PageBase)Page).GetPageResource("NSurveyAuthenticationTitle");
            //LoginLabel.Text = ((PageBase)Page).GetPageResource("LoginLabel");
            //PasswordLabel.Text = ((PageBase)Page).GetPageResource("PasswordLabel");

            LoginTextBox.ToolTip = ((PageBase)Page).GetPageResource("LoginLabel");
            PasswordTextBox.ToolTip = ((PageBase)Page).GetPageResource("PasswordLabel");

            ValidateCredentialsButton.Text = ((PageBase)Page).GetPageResource("ValidateCredentialsButton");
            ValidateCredentialsButton.ToolTip = ((PageBase)Page).GetPageResource("ValidateCredentialsButtonTooltip");
            //Disclaimer.Text = ((PageBase)Page).GetPageResource("Disclaimer");
            //Introduction.Text = ((PageBase)Page).GetPageResource("Introduction");
            //HelpGifTitle.Text = ((PageBase)Page).GetPageResource("HelpGifTitle");
        }


        private void ValidateCredentialsButton_Click(object sender, System.EventArgs e)
        {
            string enteredPwd = PasswordTextBox.Text.Trim();
            string enteredUname = LoginTextBox.Text.Trim();

            if (enteredUname.Length > 0 && enteredPwd.Length > 0)
            {
                string encryptedPwd;

                int? id = new Users().GetUserByIdFromUserName(LoginTextBox.Text);


                if ((id ?? 0) > 0)
                {
                    var sec = new LoginSecurity();
                    var user = new Users().GetUserById(id ?? 0);
                    string pwd = user.Users[0].Password;
                    string salt = user.Users[0].IsPasswordSaltNull() ? null : user.Users[0].PasswordSalt;

                    if (string.IsNullOrEmpty(salt))// Unhashed old style .Create salted password and update
                    {
                        encryptedPwd = new User().EncryptUserPassword(enteredPwd);
                        salt = sec.CreateSaltKey(5);
                        // update of password and salt in DB does not happen!
                        // next line 125 saved password compared to encryptedpwd = not true -> error!

                    }
                    else
                    {
                        salt = user.Users[0].PasswordSalt;
                        encryptedPwd = sec.CreatePasswordHash(enteredPwd, salt);
                    }

                    if (user.Users[0].Password == encryptedPwd)
                    {

                        var authUser = user;
                        UserSettingData userSettings = new Users().GetUserSettings(authUser.Users[0].UserId);

                        if (userSettings.UserSettings.Rows.Count > 0)
                        {
                            System.Text.StringBuilder userInfos = new System.Text.StringBuilder();
                            userInfos.Append(authUser.Users[0].UserName + ",");
                            userInfos.Append(authUser.Users[0].UserId + ",");
                            userInfos.Append(authUser.Users[0].FirstName + ",");
                            userInfos.Append(authUser.Users[0].LastName + ",");
                            userInfos.Append(authUser.Users[0].Email + ",");
                            userInfos.Append(userSettings.UserSettings[0].IsAdmin + ",");
                            userInfos.Append(userSettings.UserSettings[0].GlobalSurveyAccess);

                            userInfos.Append("|");

                            int[] userRights = new Users().GetUserSecurityRights(authUser.Users[0].UserId);
                            for (int i = 0; i < userRights.Length; i++)
                            {
                                userInfos.Append(userRights[i].ToString());
                                if (i + 1 < userRights.Length)
                                {
                                    userInfos.Append(",");
                                }

                            }

                            if (authUser.Users[0].IsPasswordSaltNull())
                            {
                                authUser.Users[0].PasswordSalt = salt;
                                authUser.Users[0].Password = sec.CreatePasswordHash(enteredPwd, salt);

                                ((INSurveyUserProvider)_userProvider).UpdateUser(authUser);
                            }

                            // add logindate:
                            authUser.Users[0].LastLogin = DateTime.Now;
                            ((INSurveyUserProvider)_userProvider).UpdateUser(authUser);

                            FormsAuthentication.SetAuthCookie(userInfos.ToString(), false);

                            var x = UserFactory.Create().CreatePrincipal(userInfos.ToString());


                            // ((Wap)this.Master).isTreeStale = true;

                            ((PageBase)Page).SelectedFolderId = null;
                            // ((Wap)this.Master).RebuildTree();
                            UINavigator.NavigateToFirstAccess(x, -1);
                        }
                    }
                }
                else if (id == -2)
                // if vts_tbuser is empty create an admin account:
                {
                    NSurveyUserData userData = new NSurveyUserData();
                    NSurveyUserData.UsersRow newUser = userData.Users.NewUsersRow();

                    if (_userProvider is INSurveyUserProvider)
                    {
                        //if (PasswordTextBox.Text.Length == 0)  
                        // Password: length min. 8 - max. 12; min. 1 small, 1 capital, 1 special, 1 number required
                        if (!Regex.IsMatch(PasswordTextBox.Text.Trim(), @"(?=^.{8,12}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$"))

                        {
                            MessageLabel.Visible = true;
                            ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("PasswordRulesMessage"));
                            return;
                        }

                        newUser.UserName = LoginTextBox.Text.Trim();
                        var sec = new LoginSecurity();
                        newUser.PasswordSalt = sec.CreateSaltKey(5);
                        newUser.Password = sec.CreatePasswordHash(PasswordTextBox.Text.Trim(), newUser.PasswordSalt);
                        //newUser.Email = EmailTextBox.Text;
                        newUser.FirstName = "SurveyProject";
                        newUser.LastName = "Administrator";

                        userData.Users.Rows.Add(newUser);
                        ((INSurveyUserProvider)_userProvider).AddUser(userData);
                    }

                    if (userData.Users.Rows.Count > 0)
                    {
                        UserSettingData userSettings = new UserSettingData();
                        UserSettingData.UserSettingsRow newUserSettings = userSettings.UserSettings.NewUserSettingsRow();
                        newUserSettings.UserId = userData.Users[0].UserId;
                        newUserSettings.IsAdmin = true;
                        newUserSettings.GlobalSurveyAccess = true;
                        userSettings.UserSettings.Rows.Add(newUserSettings);
                        new User().AddUserSettings(userSettings);
                    }

                   // after creating the new admin account show confirmation message:
                    MessageLabel.Visible = true;
                    ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("AdminCreatedMessage"));
                    return;
                }


            }

            MessageLabel.Visible = true;
            ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidLoginPasswordMessage"));
        }

    }

}
