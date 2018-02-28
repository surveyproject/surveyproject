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
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Security;
using Microsoft.VisualBasic;
using Votations.NSurvey.Resources;

using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Insert a new security add in to the survey
	/// </summary>
    public partial class InsertSecurityAddIn : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Literal SurveyAddAdinTitle;
		protected System.Web.UI.WebControls.Button AddAddinButton;
		protected System.Web.UI.WebControls.DropDownList SecurityAddInDropDownList;
		protected System.Web.UI.WebControls.Label AvailableAddInsLabel;

		private void Page_Load(object sender, System.EventArgs e)
		{
			LocalizePage();
            UITabList.SetInsertSecurityTabs((MsterPageTabs)Page.Master, UITabList.InsertSecurityTabs.InsertSecurity);


			_addInOrder = 
				Information.IsNumeric(Request["AddInOrder"]) && int.Parse(Request["AddInOrder"])>0 ? int.Parse(Request["AddInOrder"]) : 1;


			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				FillFields();
			}
		}

		private void LocalizePage()
		{
			SurveyAddAdinTitle.Text = GetPageResource("SurveyAddAdinTitle");
			AddAddinButton.Text = GetPageResource("AddAddinButton");
			AvailableAddInsLabel.Text = GetPageResource("AvailableAddInsLabel");

		}


		/// <summary>
		/// Get the current DB stats and fill 
		/// the label with them
		/// </summary>
		private void FillFields()
		{
			SecurityAddInDropDownList.DataSource = new SecurityAddIns().GetAvailableAddIns(SurveyId);
			SecurityAddInDropDownList.DataTextField = "Description";
			SecurityAddInDropDownList.DataValueField = "WebSecurityAddInId";
			SecurityAddInDropDownList.DataBind();
			foreach (ListItem item in SecurityAddInDropDownList.Items)
			{
				string translatedText = ResourceManager.GetString(item.Text);
				item.Text = translatedText == null ? item.Text : translatedText;
			}

			if (SecurityAddInDropDownList.Items.Count == 0)
			{
				AddAddinButton.Enabled = false;
				SecurityAddInDropDownList.Items.Add(new ListItem(GetPageResource("NoAddInAvailableMessage"),"-1"));
			}
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
			this.AddAddinButton.Click += new System.EventHandler(this.AddAddinButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void AddAddinButton_Click(object sender, System.EventArgs e)
		{
			new SecurityAddIn().AddSecurityAddInToSurvey(SurveyId, 
				int.Parse(SecurityAddInDropDownList.SelectedValue), _addInOrder);

			WebSecurityAddInData.WebSecurityAddInsRow addInRow = 
				(WebSecurityAddInData.WebSecurityAddInsRow)new SecurityAddIns().GetAddInById(SurveyId, int.Parse(SecurityAddInDropDownList.SelectedValue)).WebSecurityAddIns.Rows[0];
			try
			{
				IWebSecurityAddIn addedAddin =  WebSecurityAddInFactory.Create(addInRow, ViewState, null);
				addedAddin.InitOnSurveyAddition();
			}
			catch (Exception)
			{
				new SecurityAddIn().DeleteWebSecurityAddIn(SurveyId,int.Parse(SecurityAddInDropDownList.SelectedValue)); 
				throw;
			}
			UINavigator.NavigateToSurveySecurity(SurveyId, MenuIndex);
		}

		int _addInOrder = 1;
	}

}
