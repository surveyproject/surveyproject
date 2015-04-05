using System;
using System.Collections.Generic;
using System.Linq;
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
using Votations.NSurvey.WebAdmin.Code;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    public partial class UserImport : System.Web.UI.UserControl
    {
        
        public bool ShowMessage { get { return ViewState["SHOWMESSAGE"] != null; } set { ViewState["SHOWMESSAGE"] = (value ? "X" : null); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            LocalizePage();

            SetupSecurity();

            if (ShowMessage) ShowMessage = false;
            else MessageLabel.Visible = false;
            
            if (!Page.IsPostBack)
            {
            
                // Header.SurveyId = SurveyId;
                ((Wap)((PageBase)Page).Master).HeaderControl.SurveyId = ((PageBase)Page).getSurveyId();
                BindSurveyDropDownLists();
            }	
        }


        private void SetupSecurity()
        {
            // Check if we can edit extended properties
            if ((_userProvider is INSurveyUserProvider))
            {
                ((PageBase)Page).CheckRight(NSurveyRights.AccessUserManager, true);
            }
            else
            {
                UINavigator.NavigateToAccessDenied(((PageBase)Page).getSurveyId(), ((PageBase)Page).MenuIndex);
            }
        }


        private void LocalizePage()
        {
            ImportUsersButton.Text = ((PageBase)Page).GetPageResource("ImportUsersButton");
            ImportUsersTitle.Text = ((PageBase)Page).GetPageResource("ImportUsersTitle");
            ImportUserLabel.Text = ((PageBase)Page).GetPageResource("ImportUserLabel");
            AssignAllSurveysLabel.Text = ((PageBase)Page).GetPageResource("AssignAllSurveysLabel");
            UserSurveyAssignedLabel.Text = ((PageBase)Page).GetPageResource("UserSurveyAssignedLabel");
            UnAssignedSurveysLabel.Text = ((PageBase)Page).GetPageResource("UnAssignedSurveysLabel");
            AssignedSurveysLabel.Text = ((PageBase)Page).GetPageResource("AssignedSurveysLabel");
            RolesLabel.Text = ((PageBase)Page).GetPageResource("RolesLabel");
            AvailableRolesLabel.Text = ((PageBase)Page).GetPageResource("AvailableRolesLabel");
            UserRolesLabel.Text = ((PageBase)Page).GetPageResource("UserRolesLabel");
            UserIsAdministratorLabel.Text = ((PageBase)Page).GetPageResource("UserIsAdministratorLabel");
            ImportInfo1Label.Text = ((PageBase)Page).GetPageResource("ImportInfo1Label");
            ImportInfo2Label.Text = ((PageBase)Page).GetPageResource("ImportInfo2Label");
            ImportInfo3Label.Text = ((PageBase)Page).GetPageResource("ImportInfo3Label");
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
            int importCount = 0;
            var sec = new LoginSecurity();
           
            for (int i = 0; i < users.Length; i++)
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
                     
                        string password = user[1].Trim();

                        newUser.PasswordSalt =sec.CreateSaltKey(5);
                        newUser.Password = sec.CreatePasswordHash(password, newUser.PasswordSalt);

                        newUser.Email = user[4].Length > 0 && re.IsMatch(user[4].Trim()) ?
                            user[4].Trim() : null;
                        newUser.FirstName = user[3].Length > 0 ? user[3].Trim() : null;
                        newUser.LastName = user[2].Length > 0 ? user[2].Trim() : null;
                        userData.Users.Rows.Add(newUser);
                        ((INSurveyUserProvider)_userProvider).AddUser(userData);
                        if (userData.Users[0].UserId > 0) importCount++;
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
            if(importCount>0)
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("UserImportedMessage"));
            else
            ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("NoUserImportedMessage"));
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
            UserSurveysListBox.Items.Add(new ListItem(SurveysListBox.SelectedItem.Text, SurveysListBox.SelectedItem.Value));
            SurveysListBox.Items.Remove(SurveysListBox.SelectedItem);
        }

        private void UserSurveysListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SurveysListBox.Items.Add(new ListItem(UserSurveysListBox.SelectedItem.Text, UserSurveysListBox.SelectedItem.Value));
            UserSurveysListBox.Items.Remove(UserSurveysListBox.SelectedItem);
        }

        private void RolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UserRolesListBox.Items.Add(new ListItem(RolesListBox.SelectedItem.Text, RolesListBox.SelectedItem.Value));
            RolesListBox.Items.Remove(RolesListBox.SelectedItem);

        }

        private void UserRolesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            RolesListBox.Items.Add(new ListItem(UserRolesListBox.SelectedItem.Text, UserRolesListBox.SelectedItem.Value));
            UserRolesListBox.Items.Remove(UserRolesListBox.SelectedItem);
        }

        protected void ImportUsersButton_Click1(object sender, EventArgs e)
        {

        }
    }
}
