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

namespace Votations.NSurvey.WebAdmin.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Footer.
	/// </summary>
	public partial class FooterControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Literal VersionLiteral;
	
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit (e);
			InitializeComponent();
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			VersionLiteral.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}
	}
}
