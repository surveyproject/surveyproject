/**************************************************************************************************
	Survey changes: copyright (c) 2010, W3DevPro TM (http://survey.codeplex.com)	

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
using Votations.NSurvey;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using CKEditor;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Handle all the updates of the survey data
	/// </summary>
    public partial class SurveyPrivacy : PageBase
	{
		protected System.Web.UI.WebControls.Literal PrivacySettingsTitle;
		protected System.Web.UI.WebControls.Label RedirectionURLLabel;
		protected System.Web.UI.WebControls.TextBox RedirectionURLTextBox;
		protected System.Web.UI.WebControls.Label ThanksMessageLabel;
//		protected FreeTextBoxControls.FreeTextBox ThankYouFreeTextBox;
//      protected CKEditor.NET.CKEditorControl ThankYouFreeTextBox;
		protected System.Web.UI.WebControls.Button ApplyPrivacyButton;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Literal ThanksMessageConditionTitle;
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label EvaluationMessageConditionInfo;
		protected System.Web.UI.WebControls.Button AddNewConditionHyperLink;
		protected System.Web.UI.WebControls.Label EditionLanguageLabel;
		protected System.Web.UI.WebControls.DropDownList LanguagesDropdownlist;
		protected SurveyMessageConditionsControl SurveyMessageConditons;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            UITabList.SetSurveyInfoTabs((MsterPageTabs)Page.Master, UITabList.SurveyInfoTabs.SurveyInfoCompletion);

            SetupSecurity();
			LocalizePage();
			SurveyMessageConditons.SurveyId = SurveyId;

			if (!Page.IsPostBack)
			{
		
				// Header.SurveyId = SurveyId; 
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				BindFields();
				BindLanguages();
			}

           //CKEditor settings:
           //ThankYouCKEditor.config.toolbar = "Simple";
           //ThankYouCKEditor.config.uiColor = "#DDDDDD";
            ThankYouCKEditor.config.enterMode = CKEditor.NET.EnterMode.BR;
            ThankYouCKEditor.config.skin = "moonocolor";

            // In case of fixed default language, e.g. Dutch:
            // ThankYouCKEditor.config.language = "nl";

            ThankYouCKEditor.config.toolbar = new object[]
			{
				new object[] { "Source", "-", "NewPage", "Preview", "-", "Templates" },
				new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt" },
				new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
				new object[] { "Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField" },
				"/",
				new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
				new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
				new object[] { "BidiLtr", "BidiRtl" },
				new object[] { "Link", "Unlink", "Anchor" },
				new object[] { "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe" },
				"/",
				new object[] { "Styles", "Format", "Font", "FontSize" },
				new object[] { "TextColor", "BGColor" },
				new object[] { "Maximize", "ShowBlocks", "-", "About" }
			};

			
		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessPrivacySettings, true);
		}

		private void LocalizePage()
		{
			PrivacySettingsTitle.Text = GetPageResource("PrivacySettingsTitle");
			RedirectionURLLabel.Text  = GetPageResource("RedirectionURLLabel");
			ApplyPrivacyButton.Text = GetPageResource("ApplyPrivacyButton");
			ThanksMessageConditionTitle.Text = GetPageResource("ThanksMessageConditionTitle");

			AddNewConditionHyperLink.Text = GetPageResource("AddNewConditionHyperLink");

			//AddNewConditionHyperLink.NavigateUrl = string.Format("{0}?surveyid={1}&menuindex={2}", UINavigator.MessageConditionEditorHyperLink, SurveyId, MenuIndex);
            AddNewConditionHyperLink.PostBackUrl = string.Format("{0}?surveyid={1}&menuindex={2}", UINavigator.MessageConditionEditorHyperLink, SurveyId, MenuIndex);

            EvaluationMessageConditionInfo.Text = GetPageResource("EvaluationMessageConditionInfo");
          //  ((PageBase)Page).ShowNormalMessage(MessageLabel,((PageBase)Page).GetPageResource("ThanksMessageLabel"));
			EditionLanguageLabel.Text = GetPageResource("EditionLanguageLabel");
            ThanksMessageLabel.Text = GetPageResource("ThanksMessageLabel");
            RedirectionURLTextBox.ToolTip = ((PageBase)Page).GetPageHelpfiles("ResultsReportRedirectHelp");
		}

		/// <summary>
		/// Get the current DB data and fill 
		/// the fields with them
		/// </summary>
		private void BindFields()
		{

			// Retrieve the survey data
			SurveyData surveyData = new Surveys().GetSurveyById(SurveyId, LanguagesDropdownlist.SelectedValue);
			SurveyData.SurveysRow survey = surveyData.Surveys[0];

			RedirectionURLTextBox.Text = survey.RedirectionURL;
			//ThankYouFreeTextBox.Text = survey.ThankYouMessage;
            ThankYouCKEditor.Text = survey.ThankYouMessage;

			SurveyMessageConditons.BindData();
		}

		private void BindLanguages()
		{
			MultiLanguageMode languageMode = new MultiLanguages().GetMultiLanguageMode(SurveyId);
			if (languageMode != MultiLanguageMode.None)
			{
				MultiLanguageData surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);
				LanguagesDropdownlist.Items.Clear();
				foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
				{
					ListItem defaultItem = new ListItem(GetPageResource(language.LanguageDescription), language.LanguageCode);
					if (language.DefaultLanguage)
					{
						defaultItem.Value = "";
						defaultItem.Text += " " + GetPageResource("LanguageDefaultText");
					}

					LanguagesDropdownlist.Items.Add(defaultItem);
				}

				LanguagesDropdownlist.Visible = true;
				EditionLanguageLabel.Visible = true;
			}
			else
			{
				LanguagesDropdownlist.Visible = false;
				EditionLanguageLabel.Visible = false;
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
			this.LanguagesDropdownlist.SelectedIndexChanged += new System.EventHandler(this.LanguagesDropdownlist_SelectedIndexChanged);
			this.ApplyPrivacyButton.Click += new System.EventHandler(this.ApplyPrivacyButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        // OLD Freetextbox code:
        //private void ApplyPrivacyButton_Click(object sender, System.EventArgs e)
        //{
        //    SurveyData surveyData = new Surveys().GetSurveyById(SurveyId, LanguagesDropdownlist.SelectedValue);
        //    surveyData.Surveys[0].RedirectionURL = RedirectionURLTextBox.Text;
        //    surveyData.Surveys[0].ThankYouMessage = ThankYouFreeTextBox.Text.Length > 3900 ?
        //        ThankYouFreeTextBox.Text.Substring(0, 3900) : ThankYouFreeTextBox.Text;

        //    // Update the DB
        //    new Survey().UpdateSurvey(surveyData, LanguagesDropdownlist.SelectedValue);

        //    ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyUpdatedMessage"));
        //    MessageLabel.Visible = true;
        //}

        //New section for CKeditor - 2013/06/18:
        private void ApplyPrivacyButton_Click(object sender, System.EventArgs e)
        {
            SurveyData surveyData = new Surveys().GetSurveyById(SurveyId, LanguagesDropdownlist.SelectedValue);
            surveyData.Surveys[0].RedirectionURL = RedirectionURLTextBox.Text;
            surveyData.Surveys[0].ThankYouMessage = ThankYouCKEditor.Text.Length > 3900 ?
                ThankYouCKEditor.Text.Substring(0, 3900) : ThankYouCKEditor.Text;

            // Update the DB
            new Survey().UpdateSurvey(surveyData, LanguagesDropdownlist.SelectedValue);

            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("SurveyUpdatedMessage"));
            MessageLabel.Visible = true;
        }




		private void LanguagesDropdownlist_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindFields();
		}


	}

}
