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
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Votations.NSurvey.DataAccess;
	using Votations.NSurvey.Data;
	using Votations.NSurvey.Web;
	using Votations.NSurvey.WebAdmin.UserControls;

	/// <summary>
	/// List all the surveys
	/// </summary>
    public partial class SurveyListControl : UserControl
	{

		
		protected System.Web.UI.WebControls.DropDownList SurveyDropDownList;

		public string SurveyTitle 
		{
			get { return SurveyDropDownList.SelectedItem.Text; } 
		}


		/// <summary>
		/// Id of the selected survey 
		/// </summary>
		public int SurveyId
		{
			get { return (ViewState["SurveyID"]==null) ? -1 : int.Parse(ViewState["SurveyID"].ToString()); }
			set { ViewState["SurveyID"] = value; }
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
		
		// Bind data to page controls
		public void BindDropDown()
		{
			SurveyData surveys;
			if (((PageBase)Page).NSurveyUser.Identity.IsAdmin ||
				((PageBase)Page).NSurveyUser.Identity.HasAllSurveyAccess)
			{
				surveys = new Surveys().GetAllSurveysList();
			}
			else
			{
				surveys = new Surveys().GetAssignedSurveysList(((PageBase)Page).NSurveyUser.Identity.UserId);
			}
			
			SurveyDropDownList.DataSource = surveys;
			SurveyDropDownList.DataMember = "surveys";
			SurveyDropDownList.DataTextField = "title";
			SurveyDropDownList.DataValueField = "surveyid";
			SurveyDropDownList.DataBind();
			if ( SurveyDropDownList.Items.FindByValue(SurveyId.ToString()) != null)
			{
				SurveyDropDownList.SelectedValue = SurveyId.ToString();
			}
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			string query = '?' + Request.QueryString.ToString();
			int surveyParamIndex = query.IndexOf("surveyid=");
			
			if (surveyParamIndex > 0)
			{
				query = query.Substring(0, query.IndexOf("surveyid=")); 
			}
			else if (query.Length > 0)
			{
				query = query + '&';
			}
		
			SurveyDropDownList.Attributes.Add("onchange","document.location.href = '" +Context.Request.FilePath + query + "surveyid='+this[this.selectedIndex].value+'&menuindex="+((PageBase)Page).MenuIndex+"';return false;");
		}

	}
}
