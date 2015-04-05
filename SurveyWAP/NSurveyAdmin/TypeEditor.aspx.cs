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
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the type editor
	/// </summary>
    public partial class TypeEditor : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.DropDownList TypesDropDownList;
		protected System.Web.UI.WebControls.HyperLink CreateTypeHyperLink;
		protected System.Web.UI.WebControls.Literal AnswerTypeBuilderTitle;
		protected System.Web.UI.WebControls.Label TypeToEditLabel;
		protected System.Web.UI.WebControls.Literal BuiltInTypeNotEditedLabel;
		protected TypeOptionControl TypeOption;

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetupSecurity();
			LocalizePage();
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.AnswerTypeEdit);

			if (!Page.IsPostBack)
			{
				BindFields();
			}

			if (TypesDropDownList.SelectedValue != "0")
			{
				TypeOption.AnswerTypeId = int.Parse(TypesDropDownList.SelectedValue);
				TypeOption.Visible = true;
			}

			TypeOption.OptionChanged += new EventHandler(TypeOption_OptionChanged);
		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessAnswerTypeEditor, true);
			BuiltInTypeNotEditedLabel.Visible = !NSurveyUser.Identity.IsAdmin;
		}


		private void LocalizePage()
		{
			AnswerTypeBuilderTitle.Text = GetPageResource("AnswerTypeBuilderTitle");
			TypeToEditLabel.Text = GetPageResource("TypeToEditLabel");
			AnswerTypeBuilderTitle.Text = GetPageResource("AnswerTypeBuilderTitle");
			BuiltInTypeNotEditedLabel.Text = GetPageResource("BuiltInTypeNotEditedLabel");
			AnswerTypeBuilderTitle.Text = GetPageResource("AnswerTypeBuilderTitle");
			CreateTypeHyperLink.Text = GetPageResource("CreateTypeHyperLink");
		}

		/// <summary>
		/// Get the current DB data and fill 
		/// the fields with them
		/// </summary>
		private void BindFields()
		{
			// Header.SurveyId = SurveyId;
            ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
			CreateTypeHyperLink.NavigateUrl = UINavigator.TypeCreator + "?surveyid="+SurveyId+"&menuindex=" + MenuIndex;
			if (NSurveyUser.Identity.IsAdmin)
			{
				TypesDropDownList.DataSource = new AnswerTypes().GetEditableAnswerTypesList();
			}
			else
			{
				TypesDropDownList.DataSource = new AnswerTypes().GetEditableAssignedAnswerTypesList(NSurveyUser.Identity.UserId);
			}
			
			TypesDropDownList.DataMember = "AnswerTypes";
			TypesDropDownList.DataTextField = "Description";
			TypesDropDownList.DataValueField = "AnswerTypeID";
			TypesDropDownList.DataBind();
			TranslateListControl(TypesDropDownList);

			if (TypesDropDownList.Items.Count > 0)
			{
				TypesDropDownList.Items.Insert(0, 
					new ListItem(GetPageResource("SelectTypeMessage"),"0"));
			}
			else
			{
				TypesDropDownList.Items.Insert(0, 
					new ListItem(GetPageResource("CreateATypeMessage"),"0"));
			}
		}


		/// <summary>
		///	Triggered by the type option user control
		///	in case anything was changed we will need to
		///	rebind to display the updates to the user 
		/// </summary>
		private void TypeOption_OptionChanged(object sender, EventArgs e)
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
			this.TypesDropDownList.SelectedIndexChanged += new System.EventHandler(this.Type_IndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void CreateTypeButton_Click(object sender, System.EventArgs e)
		{
			TypeOption.Visible = true;
		}

		private void Type_IndexChanged(object sender, System.EventArgs e)
		{
			TypeOption.BindFields();
		}

	}

}
