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
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Security;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the survey security plugins
	/// </summary>
    public partial class SurveySecurity : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Literal SurveySecurityTitle;
		protected System.Web.UI.WebControls.PlaceHolder AddInListPlaceHolder;
		protected System.Web.UI.WebControls.Label UnAuthentifiedUserActionLabel;
		protected System.Web.UI.WebControls.DropDownList ActionsDropDownList;
		protected System.Web.UI.WebControls.PlaceHolder SecurityOptionsPlaceHolder;
		protected SurveyListControl SurveyList;

		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetSecurityTabs((MsterPageTabs)Page.Master, UITabList.SecurityTabs.Form);

			SetupSecurity();
			LocalizePage();

			if (!Page.IsPostBack)
			{
				
			
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				FillFields();
			}



			BuildAddInList();
		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessSecuritySettings, true);
		}

		private void BuildAddInList()
		{		
			WebSecurityAddInCollection _securityAddIns;
			Control adminControl;
			Table addInContainer = new Table();
			addInContainer.Width = Unit.Percentage(100);
			Table addInTable = new Table();
// moved to css files:
//			addInTable.CellSpacing = 2;
//			addInTable.CellPadding = 4;
			addInTable.CssClass = "questionBuilder";
			Style controlStyle = new Style();
			controlStyle.CssClass = "addinsLayout";
			_securityAddIns = WebSecurityAddInFactory.CreateWebSecurityAddInCollection(new SecurityAddIns().GetWebSecurityAddIns(SurveyId), ViewState, null);
			
			if (_securityAddIns.Count == 0)
			{
				SecurityOptionsPlaceHolder.Visible = false;
				// No Addins
				addInTable.Rows.Add(BuildAddInOptionsRow(null, _securityAddIns.Count));
			}
			else
			{
				SecurityOptionsPlaceHolder.Visible = true;
				for (int i=0;i<_securityAddIns.Count;i++)
				{
					addInTable.Rows.Add(BuildAddInOptionsRow(_securityAddIns[i], _securityAddIns.Count));
					adminControl = _securityAddIns[i].GetAdministrationInterface(controlStyle);
					if (adminControl != null)
					{
						addInTable.Rows.Add(BuildRow(adminControl));
					}
					else
					{
						addInTable.Rows.Add(BuildRow(new LiteralControl(GetPageResource("AddInAdminNotAvailableMessage"))));
					}
					addInContainer.Rows.Add(BuildRow(addInTable));
				
					AddInListPlaceHolder.Controls.Add(addInContainer);

					// Creates a new page
					addInContainer = new Table();
					addInContainer.Width = Unit.Percentage(100);
					addInTable = new Table();
// moved to css file
//					addInTable.CellSpacing = 2;
//					addInTable.CellPadding = 4;
					addInTable.CssClass = "questionBuilder";
				}
			}

			addInContainer.Rows.Add(BuildRow(addInTable));
			AddInListPlaceHolder.Controls.Add(addInContainer);
		}

		/// <summary>
		/// Builds a row with the options available for an addin
		/// </summary>
		/// <returns>a tablerow instance with the options</returns>
		private TableRow BuildAddInOptionsRow(IWebSecurityAddIn addIn, int totalAddIns)
		{
			// Creates a new addin options control
			SecurityAddInOptionsControl addInOptionsControl = 
				(SecurityAddInOptionsControl )LoadControl("UserControls/SecurityAddInOptionsControl.ascx");

			addInOptionsControl.SecurityAddIn = addIn;
			addInOptionsControl.SurveyId = SurveyId;
			addInOptionsControl.TotalAddIns = totalAddIns;
			return BuildRow(addInOptionsControl);
		}

		private TableRow BuildRow(Control child)
		{
			TableRow row = new TableRow();
			TableCell cell  = new TableCell();
			cell.Controls.Add(child);
			row.Cells.Add(cell);
			return row;
		}

		private void LocalizePage()
		{
			SurveySecurityTitle.Text = GetPageResource("SurveySecurityTitle");
			UnAuthentifiedUserActionLabel.Text = GetPageResource("UnAuthentifiedUserActionLabel");
		}


		/// <summary>
		/// Get the current DB stats and fill 
		/// the label with them
		/// </summary>
		private void FillFields()
		{
			ActionsDropDownList.DataSource = new SecurityAddIns().GetUnAuthentifiedUserActions();
			ActionsDropDownList.DataTextField = "Description";
			ActionsDropDownList.DataValueField = "UnAuthentifiedUserActionId";
			ActionsDropDownList.DataBind();
			TranslateListControl(ActionsDropDownList);
			ActionsDropDownList.SelectedValue = new Surveys().GetSurveyUnAuthentifiedUserAction(SurveyId).ToString();
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
			this.ActionsDropDownList.SelectedIndexChanged += new System.EventHandler(this.ActionsDropDownList_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ActionsDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			new Survey().UpdateUnAuthentifiedUserActions(SurveyId, int.Parse(ActionsDropDownList.SelectedValue));
		}




	}

}
