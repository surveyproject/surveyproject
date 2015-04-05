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
using Votations.NSurvey.Resources;
using System.Collections;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Allows to edit / create new language Xml files
	/// </summary>
    public partial class EditMultiLanguages : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label ResultsDisplayTimesLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Button ResetVotesButton;
		protected System.Web.UI.WebControls.Literal SurveyCreationDateLabel;
		protected System.Web.UI.WebControls.Literal SurveyResultsDisplayTimesLabel;
		protected System.Web.UI.WebControls.PlaceHolder LanguageTextPlaceHolder;
		protected System.Web.UI.WebControls.ListBox LanguageTextListBox;
		protected System.Web.UI.WebControls.Literal MessagesTranslatorLabel;

		private void Page_Load(object sender, System.EventArgs e)
		{

			SetupSecurity();
			LocalizePage();

			if (!Page.IsPostBack)
			{
				// Header.SurveyId = SurveyId;
                ((Wap)Master).HeaderControl.SurveyId = SurveyId;
				FillFields();
			}

			ResetVotesButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +GetPageResource("ResetVotesConfirmationMessage")+ "')== false) return false;");

		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessSecuritySettings, true);
			ResetVotesButton.Visible = CheckRight(NSurveyRights.ResetVotes, false);
		}

		private void LocalizePage()
		{
			SurveyCreationDateLabel.Text = GetPageResource("SurveyCreationDateLabel");
			ResetVotesButton.Text = GetPageResource("ResetVotesButton");
		}


		/// <summary>
		/// Get the current DB stats and fill 
		/// the label with them
		/// </summary>
		private void FillFields()
		{
			Hashtable resources = ResourceManager.LoadResources("en");
			LanguageTextListBox.DataSource = resources;
			LanguageTextListBox.DataTextField = "Key";
			LanguageTextListBox.DataValueField = "Key";
			LanguageTextListBox.DataBind();
			Response.Write(LanguageTextListBox.Items.Count);
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
			this.ResetVotesButton.Click += new System.EventHandler(this.ResetVotesButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ResetVotesButton_Click(object sender, System.EventArgs e)
		{
			new Voter().DeleteVoters(SurveyId);
			FillFields();
		}

	}

}
