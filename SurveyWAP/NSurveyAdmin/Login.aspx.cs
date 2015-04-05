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
using System.Web.Security;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.UserProvider;
using Votations.NSurvey.WebAdmin.Code;
namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Auth the login
    /// </summary>
    public partial class Login : PageBase
    {
    //    protected System.Web.UI.WebControls.Label MessageLabel;
    //    protected System.Web.UI.WebControls.Button ValidateCredentialsButton;
    //    protected System.Web.UI.WebControls.TextBox LoginTextBox;
    //    protected System.Web.UI.WebControls.Literal NSurveyAuthenticationTitle;
    //    protected System.Web.UI.WebControls.Literal LoginLabel;
    //    protected System.Web.UI.WebControls.Literal PasswordLabel;
          protected System.Web.UI.WebControls.Literal Disclaimer;
    //    protected System.Web.UI.WebControls.Literal Introduction;
    //    protected System.Web.UI.WebControls.Literal HelpGifTitle;
    //    protected System.Web.UI.WebControls.TextBox PasswordTextBox;
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
            //this.ValidateCredentialsButton.Click += new System.EventHandler(this.ValidateCredentialsButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            //MessageLabel.Visible = false;
            LocalizePage();
        }

        private void LocalizePage()
        {
            //NSurveyAuthenticationTitle.Text = GetPageResource("NSurveyAuthenticationTitle");
            //LoginLabel.Text = GetPageResource("LoginLabel");
            //PasswordLabel.Text = GetPageResource("PasswordLabel");
            //ValidateCredentialsButton.Text = GetPageResource("ValidateCredentialsButton");
            Disclaimer.Text = GetPageResource("Disclaimer");
            //Introduction.Text = GetPageResource("Introduction");
            //HelpGifTitle.Text = GetPageResource("HelpGifTitle");
        }


        //private void ValidateCredentialsButton_Click(object sender, System.EventArgs e)
        //{
        //    string enteredPwd = PasswordTextBox.Text.Trim();
        //    string enteredUname = LoginTextBox.Text.Trim();
        //    if (enteredUname.Length > 0 && enteredPwd.Length > 0)
        //    {
        //        string encryptedPwd;

        //        int? id = new Users().GetUserByIdFromUserName(LoginTextBox.Text);


        //        if ((id ?? 0) > 0)
        //        {
        //            var sec = new LoginSecurity();
        //            var user = new Users().GetUserById(id ?? 0);
        //            string pwd = user.Users[0].Password;
        //            string salt = user.Users[0].IsPasswordSaltNull() ? null : user.Users[0].PasswordSalt;
        //            if (string.IsNullOrEmpty(salt))// Unhashed old style .Create salted password and update
        //            {
        //                encryptedPwd = new User().EncryptUserPassword(enteredPwd);
        //                salt = sec.CreateSaltKey(5);
        //            }
        //            else
        //            {
        //                salt = user.Users[0].PasswordSalt;
        //                encryptedPwd = sec.CreatePasswordHash(enteredPwd, salt);
        //            }

        //            if (user.Users[0].Password == encryptedPwd)
        //            {

        //                var authUser = user;
        //                UserSettingData userSettings = new Users().GetUserSettings(authUser.Users[0].UserId);

        //                if (userSettings.UserSettings.Rows.Count > 0)
        //                {
        //                    System.Text.StringBuilder userInfos = new System.Text.StringBuilder();
        //                    userInfos.Append(authUser.Users[0].UserName + ",");
        //                    userInfos.Append(authUser.Users[0].UserId + ",");
        //                    userInfos.Append(authUser.Users[0].FirstName + ",");
        //                    userInfos.Append(authUser.Users[0].LastName + ",");
        //                    userInfos.Append(authUser.Users[0].Email + ",");
        //                    userInfos.Append(userSettings.UserSettings[0].IsAdmin + ",");
        //                    userInfos.Append(userSettings.UserSettings[0].GlobalSurveyAccess);

        //                    userInfos.Append("|");

        //                    int[] userRights = new Users().GetUserSecurityRights(authUser.Users[0].UserId);
        //                    for (int i = 0; i < userRights.Length; i++)
        //                    {
        //                        userInfos.Append(userRights[i].ToString());
        //                        if (i + 1 < userRights.Length)
        //                        {
        //                            userInfos.Append(",");
        //                        }

        //                    }

        //                    if (authUser.Users[0].IsPasswordSaltNull())
        //                    {
        //                        authUser.Users[0].PasswordSalt = salt;
        //                        authUser.Users[0].Password = sec.CreatePasswordHash(enteredPwd, salt);
        //                        ((INSurveyUserProvider)_userProvider).UpdateUser(authUser);
        //                    }

        //                    FormsAuthentication.SetAuthCookie(userInfos.ToString(), false);

        //                    var x = UserFactory.Create().CreatePrincipal(userInfos.ToString());


        //                    // ((Wap)this.Master).isTreeStale = true;

        //                    SelectedFolderId = null;
        //                    // ((Wap)this.Master).RebuildTree();
        //                    UINavigator.NavigateToFirstAccess(x, -1);
        //                }
        //            }
        //        }
        //    }

        //    MessageLabel.Visible = true;
        //    ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidLoginPasswordMessage"));
        //}

    }

}
