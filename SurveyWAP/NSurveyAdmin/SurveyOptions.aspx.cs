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
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Data;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Handle all the updates of the survey data
	/// </summary>
	public partial class SurveyOptions : PageBase
	{
		protected SurveyOptionControl SurveyOption;
		new protected HeaderControl Header;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            SetupSecurity();
            UITabList.SetSurveyInfoTabs((MsterPageTabs)Page.Master, UITabList.SurveyInfoTabs.SurveyInfo);

			if (!Page.IsPostBack)
			{
				// Init the user control with the current survey id
				SurveyOption.SurveyId = SurveyId; 
				// Header.SurveyId = SurveyId; 
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
			}
			
			SurveyOption.OptionChanged += new EventHandler(SurveyOption_OptionChanged);
		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessSurveySettings, true);
         
		}

		/// <summary>
		///	Triggered by the survey option user control
		///	in case anything was changed we will need to
		///	rebind to display the updates to the user 
		/// </summary>
		void SurveyOption_OptionChanged(object sender, EventArgs e)
		{
			// Retrieve the new control survey ID
			SurveyId = ((SurveyOptionControl)sender).SurveyId;
	
			// In case the control changed the current survey
			// Header.SurveyId = SurveyId; 
            ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


	}

}
