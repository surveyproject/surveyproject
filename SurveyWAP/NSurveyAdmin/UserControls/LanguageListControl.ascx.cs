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
    public partial class LanguageListControl : UserControl
	{
		protected System.Web.UI.WebControls.DropDownList LanguageDropDownList;

		public string LanguageDescription
		{
			get { return LanguageDropDownList.SelectedItem.Text; } 
		}


		/// <summary>
		/// Id of the selected language
		/// </summary>
		public int LanguageId
		{
			get { return (ViewState["LanguageId"]==null) ? -1 : int.Parse(ViewState["LanguageId"].ToString()); }
			set { ViewState["LanguageId"] = value; }
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
			MultiLanguageMode languageMode = new MultiLanguages().GetMultiLanguageMode(((PageBase)Page).SurveyId);
			if (languageMode != MultiLanguageMode.None)
			{
				MultiLanguageData surveyLanguages = new MultiLanguages().GetSurveyLanguages(((PageBase)Page).SurveyId);
				LanguageDropDownList.DataSource = surveyLanguages;
				LanguageDropDownList.DataMember = "MultiLanguages";
				LanguageDropDownList.DataTextField = "LanguageDescription";
				LanguageDropDownList.DataValueField = "LanguageCode";
				LanguageDropDownList.DataBind();
				((PageBase)Page).TranslateListControl(LanguageDropDownList);
				if (LanguageDropDownList.Items.FindByValue(LanguageId.ToString()) != null)
				{
					LanguageDropDownList.SelectedValue = LanguageId.ToString();
				}
				this.Visible = true;
			}
			else
			{
				this.Visible = false;
			}

		}


		private void Page_Load(object sender, System.EventArgs e)
		{
            BindDropDown();
			string query = '?' + Request.QueryString.ToString();
			int surveyParamIndex = query.IndexOf("languageid=");
			
			if (surveyParamIndex > 0)
			{
				query = query.Substring(0, query.IndexOf("languageid=")); 
			}
			else if (query.Length > 0)
			{
				query = query + '&';
			}
		
			LanguageDropDownList.Attributes.Add("OnChange","document.location.href = '" +Context.Request.FilePath + query + "languageid='+this[this.selectedIndex].value;return false;");
		}

	}
}
