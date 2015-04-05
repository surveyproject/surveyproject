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

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Auth the login
	/// </summary>
    public partial class AccessDenied : PageBase
	{
		protected System.Web.UI.WebControls.Label AccessMessageLabel;

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
			this.Load += new System.EventHandler(this.Page_Load);

		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			LocalizePage();
		}

		private void LocalizePage()
		{
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("AccessMessageLabel"));
		}
	}

}
