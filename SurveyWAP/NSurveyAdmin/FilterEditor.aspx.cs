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
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;


namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Filter edition page
	/// </summary>
    public partial class FilterEditor : PageBase
	{
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.DropDownList FilterDropDownList;
		protected System.Web.UI.WebControls.HyperLink CreateFilterHyperLink;
		protected SurveyListControl SurveyList;
		protected System.Web.UI.WebControls.Literal FilterBuilderTitle;
		protected System.Web.UI.WebControls.Label SelectFilteraLabel;
		protected FilterOptionControl FilterOption;

		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetResultsTabs((MsterPageTabs)Page.Master, UITabList.ResultTabs.Filters);

			SetupSecurity();
			LocalizePage();

			if (!Page.IsPostBack)
			{
		
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				
				// Init the user control with the current survey id
				FilterOption.SurveyId = SurveyId; 
				BindFields();
			}

			if (FilterDropDownList.SelectedValue != "0")
			{
				FilterOption.FilterId = int.Parse(FilterDropDownList.SelectedValue);
				FilterOption.Visible = true;			
			}
			else
			{
				FilterOption.Visible = false;
			}
			
			FilterOption.OptionChanged += new EventHandler(FilterOption_OptionChanged);
		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.CreateResultsFilter, true);
		}

		private void LocalizePage()
		{
			FilterBuilderTitle.Text = GetPageResource("FilterBuilderTitle");
			SelectFilteraLabel.Text = GetPageResource("SelectFilteraLabel");
			CreateFilterHyperLink.Text = GetPageResource("CreateFilterHyperLink");
		}

		/// <summary>
		///	Triggered by the filter option user control
		///	in case anything was changed we will need to
		///	rebind to display the updates to the user 
		/// </summary>
		void FilterOption_OptionChanged(object sender, EventArgs e)
		{
			BindFields();
		}


		private void BindFields()
		{
			CreateFilterHyperLink.NavigateUrl = UINavigator.FilterCreator + "?surveyid="+SurveyId+"&menuindex="+MenuIndex;
			FilterDropDownList.DataSource = new Filters().GetFilters(SurveyId);
			FilterDropDownList.DataMember = "Filters";
			FilterDropDownList.DataTextField = "Description";
			FilterDropDownList.DataValueField = "FilterID";
			FilterDropDownList.DataBind();
			FilterDropDownList.Items.Insert(0, 
				new ListItem(GetPageResource("SelectFilterMessage"),"0"));
			if (FilterDropDownList.Items.Count < 2)
			{
				FilterDropDownList.Visible = false;
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
			this.FilterDropDownList.SelectedIndexChanged += new System.EventHandler(this.FilterDropDownList_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void FilterDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FilterOption.BindFields();
		}

	}

}
