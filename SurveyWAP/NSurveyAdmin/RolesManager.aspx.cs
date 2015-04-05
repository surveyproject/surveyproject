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
using Votations.NSurvey.WebAdmin.UserControls;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the user editor
	/// </summary>
	public partial class RolesManager : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Literal RolesManagerTitle;
		protected System.Web.UI.WebControls.Literal RolesToEditLabel;
		protected System.Web.UI.WebControls.DropDownList RolesDropDownList;
		protected System.Web.UI.WebControls.HyperLink CreateRoleHyperLink;
		protected RolesOptionsControl RolesOptions;

		private void Page_Load(object sender, System.EventArgs e)
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
			CheckRight(NSurveyRights.AccessUserManager, true);
			IsSingleUserMode(true);
		}

		private void LocalizePage()
		{
			RolesManagerTitle.Text = GetPageResource("RolesManagerTitle");
			CreateRoleHyperLink.Text = GetPageResource("CreateRoleHyperLink");
			RolesToEditLabel.Text = GetPageResource("RolesToEditLabel");

            RolesManagerHelp.Text = GetPageHelpfiles("RolesManagerHelp");
		}

		/// <summary>
		/// Get the current DB data and fill 
		/// the fields with them
		/// </summary>
		private void BindFields()
		{
			// Header.SurveyId = SurveyId;
            ((Wap)Master).HeaderControl.SurveyId = SurveyId;
			CreateRoleHyperLink.NavigateUrl = UINavigator.RolesManagerHyperLink + "?surveyid="+SurveyId+"&menuindex=" + MenuIndex;

			RolesDropDownList.DataSource = new Roles().GetAllRolesList();
			RolesDropDownList.DataMember = "Roles";
			RolesDropDownList.DataTextField = "RoleName";
			RolesDropDownList.DataValueField = "RoleId";
			RolesDropDownList.DataBind();

			if (RolesDropDownList.Items.Count > 0)
			{
				RolesDropDownList.Items.Insert(0, 
					new ListItem(GetPageResource("SelectRoleMessage"),"0"));
			}
			else
			{
				RolesDropDownList.Items.Insert(0, 
					new ListItem(GetPageResource("CreateARoleMessage"),"0"));
			}
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
			this.RolesDropDownList.SelectedIndexChanged += new System.EventHandler(this.User_IndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void User_IndexChanged(object sender, System.EventArgs e)
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
