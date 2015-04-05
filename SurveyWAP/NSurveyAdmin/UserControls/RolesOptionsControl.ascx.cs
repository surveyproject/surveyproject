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

using Votations.NSurvey.WebAdmin;

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

	/// <summary>
	/// Survey data CU methods
	/// </summary>
    public partial class RolesOptionsControl : UserControl
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Button ApplyChangesButton;
		protected System.Web.UI.WebControls.Label RoleNameLabel;
		protected System.Web.UI.WebControls.Label RoleRightsLabel;
		protected System.Web.UI.WebControls.TextBox RoleNameTextBox;
		protected System.Web.UI.WebControls.Button CreateNewRoleButton;
		protected System.Web.UI.WebControls.Button DeleteRoleButton;
		protected System.Web.UI.WebControls.Label RolesOptionTitleLabel;
		protected System.Web.UI.WebControls.CheckBoxList RightsCheckBoxList;
		public event EventHandler OptionChanged;

		/// <summary>
		/// Id of the user to edit
		/// if no id is given put the 
		/// usercontrol in creation mode
		/// </summary>
		public int RoleId
		{
			get { return (ViewState["RoleId"]==null) ? -1 : int.Parse(ViewState["RoleId"].ToString()); }
			set { 
                ViewState["RoleId"] = value; 
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
			
			
		}

		private void LocalizePage()
		{
			CreateNewRoleButton.Text = ((PageBase)Page).GetPageResource("CreateNewRoleButton");
			ApplyChangesButton.Text = ((PageBase)Page).GetPageResource("ApplyChangesButton");
			DeleteRoleButton.Text = ((PageBase)Page).GetPageResource("DeleteRoleButton");
			RoleNameLabel.Text = ((PageBase)Page).GetPageResource("RoleNameLabel");
			RoleRightsLabel.Text = ((PageBase)Page).GetPageResource("RoleRightsLabel");
			
		}


		/// <summary>
		/// Setup the control in creation mode
		/// </summary>
		private void SwitchToCreationMode()
		{
			// Creation mode
			RolesOptionTitleLabel.Text = ((PageBase)Page).GetPageResource("NewRoleTitle");;
			CreateNewRoleButton.Visible = true;
			ApplyChangesButton.Visible = false;
			DeleteRoleButton.Visible = false;
            RoleNameTextBox.Text = string.Empty;
            foreach (ListItem item in RightsCheckBoxList.Items)
            {
                item.Selected = false;
            }
			if (!Page.IsPostBack)
			{
				BindRights();
			}
		}

		/// <summary>
		/// Setup the control in edition mode
		/// </summary>
		private void SwitchToEditionMode()
		{
            DeleteRoleButton.Attributes.Add("onClick",
                "javascript:if(confirm('" + ((PageBase)Page).GetPageResource("DeleteRoleConfirmationMessage") + "')== false) return false;");
            
			RolesOptionTitleLabel.Text = ((PageBase)Page).GetPageResource("EditRoleTitle");
			CreateNewRoleButton.Visible = false;
			ApplyChangesButton.Visible = true;
			DeleteRoleButton.Visible = true;
		}
		
		/// <summary>
		/// Get the current DB data and fill 
		/// the fields with them
		/// </summary>
		public void BindFields()
		{
//			// Retrieve the role data
			RoleData roleData = new Roles().GetRoleById(RoleId);
			RoleData.RolesRow role = roleData.Roles[0];
			RoleNameTextBox.Text  = role.RoleName;
			BindRights();
			foreach (RoleData.SecurityRightsRow right in roleData.SecurityRights.Rows)
			{
				foreach (ListItem item in RightsCheckBoxList.Items)
				{
					if (item.Value == right.SecurityRightId.ToString())
					{
						item.Selected = true;
					}
				}
			}
		}

		private void BindRights()
		{
			RightsCheckBoxList.DataSource = new Roles().GetSecurityRightList();
			RightsCheckBoxList.DataMember = "SecurityRights";
			RightsCheckBoxList.DataTextField = "description";
			RightsCheckBoxList.DataValueField = "SecurityRightId";
			RightsCheckBoxList.DataBind();
			((PageBase)Page).TranslateListControl(RightsCheckBoxList);
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
			this.CreateNewRoleButton.Click += new System.EventHandler(this.CreateNewRoleButton_Click);
			this.ApplyChangesButton.Click += new System.EventHandler(this.ApplyChangesButton_Click);
			this.DeleteRoleButton.Click += new System.EventHandler(this.DeleteRoleButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);
            this.PreRender += new EventHandler(RolesOptionsControl_PreRender);

		}

        void RolesOptionsControl_PreRender(object sender, EventArgs e)
        {
            // Check if any answer type id has been assigned
            if (RoleId == -1)
            {
                SwitchToCreationMode();
            }
            else
            {
                SwitchToEditionMode();
            }
        }
		#endregion

		private void CreateNewRoleButton_Click(object sender, System.EventArgs e)
		{
			if (ValidateFieldOptions())
			{
				RoleData roleData = new RoleData();
				RoleData.RolesRow newRole = roleData.Roles.NewRolesRow();
				newRole.RoleName = RoleNameTextBox.Text;
				roleData.Roles.Rows.Add(newRole);
				new Role().AddRole(roleData);
				AddRightsToRole(newRole.RoleId);
				UINavigator.NavigateToRoleManager(((PageBase)Page).getSurveyId(), ((PageBase)Page).MenuIndex);
			}

		}

		/// <summary>
		/// Validate all fields to make sure 
		/// no errors has occured
		/// </summary>
		private bool ValidateFieldOptions()
		{		
			if (RoleNameTextBox.Text.Length == 0)
			{
				MessageLabel.Visible = true;
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("RoleNameRequiredMessage"));
				return false;
			}

			return true;
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
				RoleData roleData = new RoleData();
				RoleData.RolesRow newRole = roleData.Roles.NewRolesRow();
				newRole.RoleId = RoleId;
				newRole.RoleName = RoleNameTextBox.Text;
				roleData.Roles.Rows.Add(newRole);
				new Role().UpdateRole(roleData);

				AddRightsToRole(RoleId);
				
//				// Notifiy containers that data has changed
				OnOptionChanged();
				MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("RoleUpdatedMessage"));
			}
		}


		private void AddRightsToRole(int roleId)
		{
			new Role().DeleteRoleRights(roleId);
			foreach (ListItem item in RightsCheckBoxList.Items)
			{
				if (item.Selected)
				{
					new Role().AddRightToRole(roleId, int.Parse(item.Value));
				}
			}
		}
		

		private void DeleteRoleButton_Click(object sender, System.EventArgs e)
		{
			new Role().DeleteRoleById(RoleId);
			RoleId = -1;
			Visible = false;
			OnOptionChanged();
		}

        protected void CreateNewRoleButton_Click1(object sender, EventArgs e)
        {

        }
	}
}
