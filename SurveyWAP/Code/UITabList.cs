/**************************************************************************************************
	Survey TM Project changes: copyright (c) 2013, Fryslan Webservices TM (http://survey.codeplex.com)	


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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Votations.NSurvey.Resources;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;

namespace Votations.NSurvey.WebAdmin
{
    public static class UITabList
    {
        public enum DesignerTabs { FormBuilder = 0, QuestionGroups = 1, AnswerTypeEdit = 2, RegexLib = 3, SurveyInfoLayout = 4}
        public static void SetDesignerTabs(MsterPageTabs masterPage, DesignerTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("DesignerformBuilder"), UINavigator.SurveyContentBuilderLink);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("DesignerformQuestionGroups"), UINavigator.QuestionGroupsLink);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("DesignerformAnswerTypeEdit"), UINavigator.TypeEditor);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("DesignerformRegexLib"), UINavigator.RegExEditorHyperLink);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("SurveyInfoLayout"), UINavigator.SurveyLayoutLink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum StatsTabs { Statistics = 0 }
        public static void SetStatsTabs(MsterPageTabs masterPage, StatsTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("SurveyStatisticsTitle"), UINavigator.SurveyStats);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum HelpTabs { HelpOptions = 0, HelpFiles = 1 }
        public static void SetHelpTabs(MsterPageTabs masterPage, HelpTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("HelpOptionsHyperlink"), UINavigator.HelpOptionsHyperLink);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("HelpFilesHyperlink"), UINavigator.HelpFilesHyperLink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum LibraryTemplatesTabs { LibraryTemplates = 0 }
        public static void SetLibraryTemplatesTabs(MsterPageTabs masterPage, LibraryTemplatesTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("LibraryTemplateHyperlink"), UINavigator.LibraryTemplateHyperlink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum ConditionalEndMessageTabs { EndMessages = 0 }
        public static void SetConditionalEndMessageTabs(MsterPageTabs masterPage, ConditionalEndMessageTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("ConditionalEndMessageHyperlink"), UINavigator.MessageConditionEditorHyperLink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum TakeSurveyTabs { TakeSurvey = 0 }
        public static void SetTakeSurveyTabs(MsterPageTabs masterPage, TakeSurveyTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("TakeSurveyHyperLink"), UINavigator.TakeSurveyHyperLink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum InsertSecurityTabs { InsertSecurity = 0 }
        public static void SetInsertSecurityTabs(MsterPageTabs masterPage, InsertSecurityTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("InsertSecurityHyperLink"), UINavigator.InsertSecurityAddInLink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum CreateAnswerTypeTabs { Answertype = 0 }
        public static void SetCreateAnswerTypeTabs(MsterPageTabs masterPage, CreateAnswerTypeTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("CreateAnswerTypeHyperLink"), UINavigator.TypeCreator);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum VoterReportTabs { VoterReport = 0 }
        public static void SetVoterReportTabs(MsterPageTabs masterPage, VoterReportTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("VoterReportHyperLink"), UINavigator.VoterReport);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum EditVoterReportTabs { EditVoterReport = 0 }
        public static void SetEditVoterReportTabs(MsterPageTabs masterPage, EditVoterReportTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("EditVoterReportHyperLink"), UINavigator.EditVoterReport);
            masterPage.selectedTabIndex = (int)selectedTab;
        }
        
        public enum SurveyInfoTabs { SurveyInfo = 0, SurveyInfoMultiLanguage = 1, SurveyInfoCompletion = 2 }
        public static void SetSurveyInfoTabs(MsterPageTabs masterPage, SurveyInfoTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("SurveyInfo"), UINavigator.SurveyOptionsLink);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("SurveyInfoMultiLanguage"), UINavigator.MultiLanguagesHyperLink);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("SurveyInfoCompletion"), UINavigator.SurveyPrivacyLink);

            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum ResultTabs { Reports = 0, Filters = 1, FileManager = 2, DataExport = 3, DataImport = 4 }
        public static void SetResultsTabs(MsterPageTabs masterPage, ResultTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add("Reports", UINavigator.ResultsReportHyperlink);
            masterPage.DisplayTabs.Add("Filters", UINavigator.FilterEditor);
            masterPage.DisplayTabs.Add("File Manager", UINavigator.FileManagerHyperLink);
            masterPage.DisplayTabs.Add("Data Export", UINavigator.ExportData);
            masterPage.DisplayTabs.Add("Data Import", UINavigator.DataImportHyperLink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }


        public enum SecurityTabs { Form = 0, Token = 1, IPRange=2}
        public static void SetSecurityTabs(MsterPageTabs masterPage, SecurityTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add("Form Security", UINavigator.SurveySecurityLink);
            masterPage.DisplayTabs.Add("Token Security", UINavigator.SurveyTokenSecurityLink);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("IPRangeFormTitle"), UINavigator.SurveyIPSecurityLink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }

        public enum CampaignTabs { CampaignStart = 0, Web = 1, Mailing = 2, MailingStatus = 3, MailingLog=4 }
        public static void SetCampaignTabs(MsterPageTabs masterPage, CampaignTabs selectedTab)
        {
            masterPage.DisplayTabs.Clear();
            masterPage.DisplayTabs.Add(ResourceManager.GetString("CampaignPreviewHyperlink"), UINavigator.CampaignPreViewHyperLink);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("ASPNETCodeHyperlink"), UINavigator.ASPNETCode);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("MailingHyperlink"), UINavigator.SurveyMailing);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("MailingStatusHyperlink"), UINavigator.MailingStatus);
            masterPage.DisplayTabs.Add(ResourceManager.GetString("MailingLogHyperlink"), UINavigator.MailingLogHyperLink);
            masterPage.selectedTabIndex = (int)selectedTab;
        }
    }
}