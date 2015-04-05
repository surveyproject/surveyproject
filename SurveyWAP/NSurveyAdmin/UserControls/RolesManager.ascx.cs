using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    public partial class RolesManager : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupSecurity();
            LocalizePage();

            if (!Page.IsPostBack)
            {
                BindFields();
            }

            if (RolesDropDownList.SelectedValue != "0")
            {
                RolesOptions.RoleId = int.Parse(RolesDropDownList.SelectedValue);
                RolesOptions.Visible = true;
            }

            RolesOptions.OptionChanged += new EventHandler(RolesManager_OptionChanged);
        }

        private void SetupSecurity()
        {
            ((PageBase)Page).CheckRight(NSurveyRights.AccessUserManager, true);
            ((PageBase)Page).IsSingleUserMode(true);
        }

        private void LocalizePage()
        {
            RolesManagerTitle.Text = ((PageBase)Page).GetPageResource("RolesManagerTitle");
            CreateRoleHyperLink.Text = ((PageBase)Page).GetPageResource("CreateRoleHyperLink");
            RolesToEditLabel.Text = ((PageBase)Page).GetPageResource("RolesToEditLabel");

            //RolesManagerHelp.Text = ((PageBase)Page).GetPageHelpfiles("RolesManagerHelp");
        }

        /// <summary>
        /// Get the current DB data and fill 
        /// the fields with them
        /// </summary>
        private void BindFields()
        {
            // Header.SurveyId = SurveyId;
            ((Wap)((PageBase)Page).Master).HeaderControl.SurveyId = ((PageBase)Page).getSurveyId();
            //CreateRoleHyperLink.NavigateUrl = UINavigator.RolesManagerHyperLink + "?surveyid=" + ((PageBase)Page).SurveyId + "&menuindex=" + ((PageBase)Page).MenuIndex;

            RolesDropDownList.DataSource = new Roles().GetAllRolesList();
            RolesDropDownList.DataMember = "Roles";
            RolesDropDownList.DataTextField = "RoleName";
            RolesDropDownList.DataValueField = "RoleId";
            RolesDropDownList.DataBind();

            if (RolesDropDownList.Items.Count > 0)
            {
                RolesDropDownList.Items.Insert(0,
                    new ListItem(((PageBase)Page).GetPageResource("SelectRoleMessage"), "0"));
            }
            else
            {
                RolesDropDownList.Items.Insert(0,
                    new ListItem(((PageBase)Page).GetPageResource("CreateARoleMessage"), "0"));
            }
        }

        protected void OnNewRole(object sender, EventArgs e)
        {
            RolesOptions.RoleId = -1;
            RolesOptions.Visible = true;

            BindFields();
        }

        /// <summary>
        ///	Triggered by the type option user control
        ///	in case anything was changed we will need to
        ///	rebind to display the updates to the user 
        /// </summary>
        private void RolesManager_OptionChanged(object sender, EventArgs e)
        {
            BindFields();
        }


        protected void User_IndexChanged(object sender, System.EventArgs e)
        {
            if (RolesDropDownList.SelectedValue != "0")
            {
                RolesOptions.RoleId = int.Parse(RolesDropDownList.SelectedValue);
                RolesOptions.BindFields();
            }
            else
            {
                RolesOptions.RoleId = -1;
                RolesOptions.Visible = false;
            }
        }
    }
}