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
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Setup for nsurvey's multi languages features
	/// </summary>
    public partial class MultiLanguagesPage : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label ResultsDisplayTimesLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Literal SurveyResultsDisplayTimesLabel;
		protected System.Web.UI.WebControls.Literal MultiLanguagesTitle;
		protected System.Web.UI.WebControls.Label EnableMultiLanguagesLabel;
		protected System.Web.UI.WebControls.Label EnabledLanguagesLabel;
		protected System.Web.UI.WebControls.CheckBox MultiLanguagesCheckBox;
		protected System.Web.UI.WebControls.ListBox DisabledLanguagesListBox;
		protected System.Web.UI.WebControls.ListBox EnabledLanguagesListBox;
		protected System.Web.UI.WebControls.Label MultiLanguagesModeLabel;
		protected System.Web.UI.WebControls.PlaceHolder MultiLanguagesPlaceHolder;
		protected System.Web.UI.WebControls.DropDownList MultiLanguagesModeDropDownList;
		protected System.Web.UI.WebControls.Label DefaultLanguageLabel;
		protected System.Web.UI.WebControls.DropDownList DefaultLanguageDropdownlist;
		protected System.Web.UI.WebControls.Label VariableNameLabel;
		protected System.Web.UI.WebControls.TextBox VariableNameTextBox;
		protected System.Web.UI.WebControls.Button VariableNameUpdateButton;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label VariableNameInfoLabel;
		protected SurveyListControl SurveyList;

		private void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetSurveyInfoTabs((MsterPageTabs)Page.Master, UITabList.SurveyInfoTabs.SurveyInfoMultiLanguage);

			SetupSecurity();
			LocalizePage();
			MessageLabel.Visible = false;
			
			if (!Page.IsPostBack)
			{
			
				// Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				FillFields();
			}

		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessMultiLanguages, true);
		}

		private void LocalizePage()
		{
			MultiLanguagesTitle.Text = GetPageResource("MultiLanguagesTitle");
			EnableMultiLanguagesLabel.Text = GetPageResource("EnableMultiLanguagesLabel");
			MultiLanguagesModeLabel.Text = GetPageResource("MultiLanguagesModeLabel");
			EnabledLanguagesLabel.Text = GetPageResource("EnabledLanguagesLabel");
			DefaultLanguageLabel.Text = GetPageResource("DefaultLanguageLabel");
			VariableNameLabel.Text = GetPageResource("VariableNameLabel");
			VariableNameUpdateButton.Text = GetPageResource("VariableNameUpdateButton");
			VariableNameInfoLabel.Text = GetPageResource("VariableNameInfoLabel");
			if (!Page.IsPostBack)
			{
				MultiLanguagesModeDropDownList.Items.Add(new ListItem(GetPageResource("UserSelectionOption"), ((int)MultiLanguageMode.UserSelection).ToString()));
				MultiLanguagesModeDropDownList.Items.Add(new ListItem(GetPageResource("BrowserDetectionOption"), ((int)MultiLanguageMode.BrowserDetection).ToString()));
				MultiLanguagesModeDropDownList.Items.Add(new ListItem(GetPageResource("QueryStringLanguageOption"), ((int)MultiLanguageMode.QueryString).ToString()));
				MultiLanguagesModeDropDownList.Items.Add(new ListItem(GetPageResource("CookieLanguageOption"), ((int)MultiLanguageMode.Cookie).ToString()));
				MultiLanguagesModeDropDownList.Items.Add(new ListItem(GetPageResource("SessionLanguageOption"), ((int)MultiLanguageMode.Session).ToString()));
			}
		}


		/// <summary>
		/// Get the current DB stats and fill 
		/// the label with them
		/// </summary>
		private void FillFields()
		{
			MultiLanguageData surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);
			
			SurveyData surveySettings = new Surveys().GetSurveyById(SurveyId, "");
			if ((MultiLanguageMode)surveySettings.Surveys[0].MultiLanguageModeId != MultiLanguageMode.None)
			{
				MultiLanguagesPlaceHolder.Visible = true;
				MultiLanguagesCheckBox.Checked = true;
				MultiLanguagesModeDropDownList.SelectedValue = surveySettings.Surveys[0].MultiLanguageModeId.ToString();

				if (int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.Cookie || 
					int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.QueryString ||
					int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.Session)
				{
					VariableNameLabel.Visible = true;
					VariableNameTextBox.Visible = true;
					VariableNameTextBox.Text = surveySettings.Surveys[0].MultiLanguageVariable;
					VariableNameUpdateButton.Visible = true;
					VariableNameInfoLabel.Visible = true;
				}
				else
				{
					VariableNameLabel.Visible = false;
					VariableNameTextBox.Text = string.Empty;
					VariableNameTextBox.Visible = false;
					VariableNameUpdateButton.Visible = false;
					VariableNameInfoLabel.Visible = false;
				}
			}
			else
			{
				MultiLanguagesPlaceHolder.Visible = false;
				MultiLanguagesCheckBox.Checked = false;
			}
			
			DisabledLanguagesListBox.DataSource = new MultiLanguages().GetMultiLanguages();
			DisabledLanguagesListBox.DataMember = "MultiLanguages";
			DisabledLanguagesListBox.DataTextField = "LanguageDescription";
			DisabledLanguagesListBox.DataValueField = "LanguageCode";
			DisabledLanguagesListBox.DataBind();

			EnabledLanguagesListBox.DataSource = surveyLanguages;
			EnabledLanguagesListBox.DataMember = "MultiLanguages";
			EnabledLanguagesListBox.DataTextField = "LanguageDescription";
			EnabledLanguagesListBox.DataValueField = "LanguageCode";
			EnabledLanguagesListBox.DataBind();

			foreach (ListItem enabledItem in EnabledLanguagesListBox.Items)
			{
				ListItem disabledItem = DisabledLanguagesListBox.Items.FindByValue(enabledItem.Value);
				if (disabledItem != null)
				{
					DisabledLanguagesListBox.Items.Remove(disabledItem);
				}
			}

			DefaultLanguageDropdownlist.Items.Clear();
			foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
			{
				ListItem defaultItem = new ListItem(language.LanguageDescription, language.LanguageCode);
				if (language.DefaultLanguage)
				{
					defaultItem.Selected = true;
				}

				DefaultLanguageDropdownlist.Items.Add(defaultItem);
			}

			TranslateListControl(DefaultLanguageDropdownlist,true);
			TranslateListControl(DisabledLanguagesListBox,true);
			TranslateListControl(EnabledLanguagesListBox,true);

           
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
			this.MultiLanguagesCheckBox.CheckedChanged += new System.EventHandler(this.MultiLanguagesCheckBox_CheckedChanged);
			this.MultiLanguagesModeDropDownList.SelectedIndexChanged += new System.EventHandler(this.MultiLanguagesModeDropDownList_SelectedIndexChanged);
			this.VariableNameUpdateButton.Click += new System.EventHandler(this.VariableNameUpdateButton_Click);
			this.DisabledLanguagesListBox.SelectedIndexChanged += new System.EventHandler(this.DisabledLanguagesListBox_SelectedIndexChanged);
			this.EnabledLanguagesListBox.SelectedIndexChanged += new System.EventHandler(this.EnabledLanguagesListBox_SelectedIndexChanged);
			this.DefaultLanguageDropdownlist.SelectedIndexChanged += new System.EventHandler(this.DefaultLanguageDropdownlist_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DisabledLanguagesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			new MultiLanguage().UpdateSurveyLanguage(SurveyId, DisabledLanguagesListBox.SelectedValue, false);
			FillFields();
			MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("LanguageEnabledMessage"));
		}

		private void EnabledLanguagesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (EnabledLanguagesListBox.SelectedValue != DefaultLanguageDropdownlist.SelectedValue)
			{
				new MultiLanguage().DeleteSurveyLanguage(SurveyId, EnabledLanguagesListBox.SelectedValue);
				FillFields();
				MessageLabel.Visible = true;
((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("LanguageDisabledMessage"));
			}
			else
			{
				MessageLabel.Visible = true;
((PageBase)Page).ShowErrorMessage(MessageLabel,((PageBase)Page).GetPageResource("OneDefaultLanguageRequiredMessage"));
			}
		}

		private void MultiLanguagesCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (MultiLanguagesCheckBox.Checked)
			{
				new MultiLanguage().UpdateMultiLanguage(SurveyId, MultiLanguageMode.UserSelection, null);
				new MultiLanguage().UpdateSurveyLanguage(SurveyId, "en-US", true);
			}
			else
			{
				new MultiLanguage().DisableMultiLanguage(SurveyId);
			}
			
			FillFields();
		}

		private void MultiLanguagesModeDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			VariableNameTextBox.Text = string.Empty;

			if (int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.Cookie || 
				int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.QueryString ||
				int.Parse(MultiLanguagesModeDropDownList.SelectedValue) == (int)MultiLanguageMode.Session)
			{
				VariableNameLabel.Visible = true;
				VariableNameTextBox.Visible = true;
				VariableNameUpdateButton.Visible = true;
				VariableNameInfoLabel.Visible = true;
			}
			else
			{
				VariableNameLabel.Visible = false;
				VariableNameTextBox.Visible = false;
				VariableNameUpdateButton.Visible = false;
				VariableNameInfoLabel.Visible = false;
			}

			new MultiLanguage().UpdateMultiLanguage(SurveyId, ((MultiLanguageMode)int.Parse(MultiLanguagesModeDropDownList.SelectedValue)), null);	
		}

		private void DefaultLanguageDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Adds the new language as default
			new MultiLanguage().UpdateSurveyLanguage(SurveyId, DefaultLanguageDropdownlist.SelectedValue, true);

			// Reset all other default items  
			foreach (ListItem item in DefaultLanguageDropdownlist.Items)
			{
				if (!item.Selected)
				{
					new MultiLanguage().UpdateSurveyLanguage(SurveyId, item.Value, false);
				}
			}
		}

		private void VariableNameUpdateButton_Click(object sender, System.EventArgs e)
		{
			new MultiLanguage().UpdateMultiLanguage(SurveyId, ((MultiLanguageMode)int.Parse(MultiLanguagesModeDropDownList.SelectedValue)), VariableNameTextBox.Text);	
		}
	}
}
