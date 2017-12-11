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
using System.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.WebAdmin.UserControls;

using Votations.NSurvey.WebAdmin.NSurveyAdmin;
using System.Collections.Generic;
using System.Text;

namespace Votations.NSurvey.WebAdmin
{
	/// <summary>
	/// Display the survey statistics
	/// </summary>
    public partial class GlobalStats : PageBase
	{
		protected System.Web.UI.WebControls.Label MessageLabel;
		protected System.Web.UI.WebControls.Label CreationDateLabel;
		protected System.Web.UI.WebControls.Label LastEntryDateLabel;
		protected System.Web.UI.WebControls.Label DisplayTimesLabel;
		//protected System.Web.UI.WebControls.Label ResultsDisplayTimesLabel;
		protected System.Web.UI.WebControls.Label NumberOfVotersLabel;
		new protected HeaderControl Header;
		protected System.Web.UI.WebControls.Button ResetVotesButton;
		protected System.Web.UI.WebControls.Calendar StatsCalendar;
		protected System.Web.UI.WebControls.Literal SurveyStatisticsTitle;
		protected System.Web.UI.WebControls.Literal SurveyCreationDateLabel;
		protected System.Web.UI.WebControls.Literal LastSurveyEntryLabel;
		protected System.Web.UI.WebControls.Literal SurveyDisplayTimesLabel;
		protected System.Web.UI.WebControls.Literal SurveyNumberOfVotesLabel;
		protected System.Web.UI.WebControls.Literal SurveyMonthlyStatsLabel;
		//protected System.Web.UI.WebControls.Literal SurveyResultsDisplayTimesLabel;
		protected System.Web.UI.WebControls.Literal SaveProgressEntriesLabel;
		protected System.Web.UI.WebControls.Label NumberSaveProgressEntriesLabel;
		protected System.Web.UI.WebControls.Button DeleteSaveProgressLinkButton;
		//protected SurveyListControl SurveyList;


		private void Page_Load(object sender, System.EventArgs e)
		{
			SetupSecurity();
			LocalizePage();
            UITabList.SetStatsTabs((MsterPageTabs)Page.Master, UITabList.StatsTabs.Statistics);


			if (!Page.IsPostBack)
			{
				
                // Header.SurveyId = SurveyId;
                ((Wap)Master.Master).HeaderControl.SurveyId = SurveyId;
				FillFields();

                ShowMultiLanguages();
                ShowOpenCloseDates();

            }

			ResetVotesButton.Attributes.Add("onClick",
				"javascript:if(confirm('" +GetPageResource("ResetVotesConfirmationMessage")+ "')== false) return false;");

		}

		private void SetupSecurity()
		{
			CheckRight(NSurveyRights.AccessStats, true);
			ResetVotesButton.Visible = CheckRight(NSurveyRights.ResetVotes, false);
			DeleteSaveProgressLinkButton.Visible = CheckRight(NSurveyRights.DeleteUnvalidateEntries, false);
		}

		private void LocalizePage()
		{
			SurveyStatisticsTitle.Text = GetPageResource("SurveyStatisticsTitle");
			SurveyCreationDateLabel.Text = GetPageResource("SurveyCreationDateLabel");
			LastSurveyEntryLabel.Text = GetPageResource("LastSurveyEntryLabel");
			SurveyDisplayTimesLabel.Text = GetPageResource("SurveyDisplayTimesLabel");
			//SurveyResultsDisplayTimesLabel.Text = GetPageResource("SurveyResultsDisplayTimesLabel");
			SurveyNumberOfVotesLabel.Text = GetPageResource("SurveyNumberOfVotesLabel");
			SurveyMonthlyStatsLabel.Text = GetPageResource("SurveyMonthlyStatsLabel");
			ResetVotesButton.Text = GetPageResource("ResetVotesButton");
			SaveProgressEntriesLabel.Text = GetPageResource("SaveProgressEntriesLabel");
			DeleteSaveProgressLinkButton.Text = GetPageResource("DeleteSaveProgressLinkButton");

            MultilanguagesLiteral.Text = GetPageResource("MultilanguagesStatsLiteral");

		}

        private void ShowMultiLanguages()
        {
            SurveyData surveyData =
            new Surveys().GetSurveyById(SurveyId, "");

            if (surveyData.Surveys[0].MultiLanguageModeId > 0)
            {
                MultiLanguagePlaceholder.Visible = true;
            }
            else
            {
                MultiLanguagePlaceholder.Visible = false;
            }

        }


        private void ShowOpenCloseDates()
        {
            SurveyData surveyData =
            new Surveys().GetSurveyById(SurveyId, "");

            if (!surveyData.Surveys[0].IsOpenDateNull() || !surveyData.Surveys[0].IsCloseDateNull())
            {
                OpenCloseDatePlaceHolder.Visible = true;
            }
            else
            {
                OpenCloseDatePlaceHolder.Visible = false;
            }

        }



        private string ListMultiLanguages()
        {
            MultiLanguageData surveyLanguages;
            surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);

            var index = 0;
            StringBuilder builder = new StringBuilder();        

            foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
            {
                string defaultItem = " " + GetPageResource(language.LanguageDescription);

                if (index >= 0)                    
                    builder.Append(defaultItem).Append(",");
                index++;
            }

            return builder.ToString().TrimEnd(',');
        }


		/// <summary>
		/// Get the current DB stats and fill 
		/// the label with them
		/// </summary>
		private void FillFields()
		{
			// Retrieve the survey data
			SurveyData surveyData = 
				new Surveys().GetSurveyById(SurveyId, "");

            // Assigns the retrieved data to the correct labels

            SurveyTitleLabel.Text = surveyData.Surveys[0].Title;

            //survey active status
            if (surveyData.Surveys[0].Activated)
            {
                SurveyStatusLabel.Text = GetPageResource("SurveyActive");
                SurveyStatusLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            } else
            {
                SurveyStatusLabel.Text = GetPageResource("SurveyInactive");
                SurveyStatusLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            }

            // ML active languages 
            if (!String.IsNullOrEmpty(surveyData.Surveys[0].MultiLanguageModeId.ToString()) )
            {
                MultilanguagesLabel.Text = ListMultiLanguages();
            }
            else
            {
                MultilanguagesLabel.Text = "-";
            }


            // Open and Close Dates
            OpenCloseDateLiteral.Text = GetPageResource("OpeningCloseDateLiteral");

            if (!surveyData.Surveys[0].IsOpenDateNull())
            {
            OpenDateLabel.Text = String.IsNullOrEmpty(surveyData.Surveys[0].OpenDate.ToString()) ? "No Opendate" : "Open: " + surveyData.Surveys[0].OpenDate.ToShortDateString();
            }
            else
            {
                OpenDateLabel.Text = GetPageResource("DateNotSet");
            }

            //OpenDateLabel.Text = String.IsNullOrEmpty(oDate.ToString()) ? "No Opendate" : oDate.ToString();

            if (!surveyData.Surveys[0].IsCloseDateNull())
            {
                CloseDateLabel.Text = String.IsNullOrEmpty(surveyData.Surveys[0].CloseDate.ToString()) ? "No Closedate" :"Close: " + surveyData.Surveys[0].CloseDate.ToShortDateString();
            } else
            {
                CloseDateLabel.Text = GetPageResource("DateNotSet");
            }

            CreationDateLabel.Text = surveyData.Surveys[0].CreationDate.ToString();
			LastEntryDateLabel.Text = 
				surveyData.Surveys[0].IsLastEntryDateNull() ? GetPageResource("NoEntriesRecordedInfo") : surveyData.Surveys[0].LastEntryDate.ToString();

			DisplayTimesLabel.Text = surveyData.Surveys[0].SurveyDisplayTimes.ToString();
			//ResultsDisplayTimesLabel.Text = surveyData.Surveys[0].ResultsDisplayTimes.ToString();
			NumberOfVotersLabel.Text = surveyData.Surveys[0].VoterNumber.ToString();
			NumberSaveProgressEntriesLabel.Text = new Voters().GetUnvalidatedVotersCount(SurveyId).ToString();
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
			this.DeleteSaveProgressLinkButton.Click += new System.EventHandler(this.DeleteSaveProgressLinkButton_Click);
			this.StatsCalendar.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.StatsCalendar_DayRender);
			this.ResetVotesButton.Click += new System.EventHandler(this.ResetVotesButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ResetVotesButton_Click(object sender, System.EventArgs e)
		{
			new Voter().DeleteVoters(SurveyId);
			FillFields();
		}

		private void StatsCalendar_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
		{	
			int voterNumber = new Voters().GetDayStats(SurveyId, e.Day.Date);
			if (voterNumber > 0)
			{
				e.Cell.Controls.Add(new LiteralControl(string.Format("<br /><b>" + GetPageResource("EntriesNumber") + "</b>", voterNumber)));
			}
			else
			{
				e.Cell.Controls.Add(new LiteralControl(string.Format("<br />&nbsp;")));
			}
		}

		private void DeleteSaveProgressLinkButton_Click(object sender, System.EventArgs e)
		{
			new Voter().DeleteUnvalidatedVoters(SurveyId);
			FillFields();
		}
	}

}
