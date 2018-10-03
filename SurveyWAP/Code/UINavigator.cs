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
using Votations.NSurvey.Web;
using Votations.NSurvey.Data;
using Votations.NSurvey.Web.Security;


namespace Votations.NSurvey.WebAdmin
{
    /// <summary>
    /// Classes to manage the page navigation logic: hyperlinks to all webpages of the webapplication
    /// </summary>
    public class UINavigator
    {
        public static readonly string AdminRoot = "~/NSurveyAdmin";

        public static readonly string CreateSurveyLink = AdminRoot + "/SurveyList.aspx?tabindex=1";
        public static readonly string ListSurveyLink = AdminRoot + "/SurveyList.aspx";
        public static readonly string FilterEditorLink = AdminRoot + "/FilterEditor.aspx";

        public static readonly string EditSingleQuestionLink = AdminRoot + "/EditSingleQuestion.aspx";
        public static readonly string EditMatrixQuestionLink = AdminRoot + "/editmatrixquestion.aspx";
        public static readonly string SurveyOptionsLink = AdminRoot + "/surveyoptions.aspx";
        public static readonly string SurveyPrivacyLink = AdminRoot + "/surveyprivacy.aspx";
        public static readonly string SurveyLayoutLink = AdminRoot + "/SurveyLayout.aspx";
        public static readonly string SurveySecurityLink = AdminRoot + "/surveysecurity.aspx";
        public static readonly string SurveyTokenSecurityLink = AdminRoot + "/SurveyTokenSecurity.aspx";
        public static readonly string SurveyIPSecurityLink = AdminRoot + "/SurveyIPSecurity.aspx";
        public static readonly string SurveyContentBuilderLink = AdminRoot + "/surveycontentbuilder.aspx";
        public static readonly string QuestionGroupsLink = AdminRoot + "/QuestionGroups.aspx";
        public static readonly string InsertQuestionLink = AdminRoot + "/InsertQuestion.aspx";
        public static readonly string EditPageBranching = AdminRoot + "/EditPageBranching.aspx";
        public static readonly string SurveyStats = AdminRoot + "/GlobalStats.aspx";
        public static readonly string TypeEditor = AdminRoot + "/TypeEditor.aspx";
        public static readonly string TypeCreator = AdminRoot + "/TypeCreator.aspx";
        public static readonly string FilterEditor = AdminRoot + "/FilterEditor.aspx";
        public static readonly string FilterCreator = AdminRoot + "/FilterCreator.aspx";
        public static readonly string VoterReport = AdminRoot + "/VoterReport.aspx";
        public static readonly string EditVoterReport = AdminRoot + "/EditVoterReport.aspx";
        public static readonly string MailingPoll = AdminRoot + "/MailingPoll.aspx";
        public static readonly string MailingPollStatus = AdminRoot + "/MailingPollStatus.aspx";
        public static readonly string SurveyMailing = AdminRoot + "/SurveyMailing.aspx";
        public static readonly string ExportData = AdminRoot + "/ExportData.aspx";
        public static readonly string MailingStatus = AdminRoot + "/MailingStatus.aspx";
        public static readonly string ResultsReportHyperlink = AdminRoot + "/ResultsReporting.aspx";
        public static readonly string FieldsReportHyperlink = AdminRoot + "/FieldReporting.aspx";
        public static readonly string SSRSReportHyperlink = AdminRoot + "/ResultsSsrs.aspx";
        public static readonly string ASPNETCode = AdminRoot + "/Controlcode.aspx";
        public static readonly string InsertSecurityAddInLink = AdminRoot + "/InsertSecurityAddIn.aspx";
        public static readonly string CrossTabHyperLink = AdminRoot + "/ResultsCrossTabulation.aspx";
        public static readonly string AnswerEditorHyperLink = AdminRoot + "/EditSingleQuestionAnswers.aspx";
        public static readonly string MessageConditionEditorHyperLink = AdminRoot + "/ConditionalEndMessage.aspx";
        public static readonly string LibraryDirectoryHyperLink = AdminRoot + "/LibraryDirectory.aspx";
        public static readonly string LibraryCreateHyperLink = AdminRoot + "/LibraryDirectory.aspx?tabindex=1";
        public static readonly string UsersManagerHyperLink = AdminRoot + "/UsersManager.aspx";
        //public static readonly string UserCreatorHyperLink = AdminRoot + "/UserCreator.aspx";
        public static readonly string RolesManagerHyperLink = AdminRoot + "/UsersManager.aspx?tabindex=1";
        public static readonly string AccessDeniedHyperLink = AdminRoot + "/AccessDenied.aspx";

        public static readonly string HelpFilesHyperLink = AdminRoot + "/Help/default.aspx";
        public static readonly string HelpOptionsHyperLink = AdminRoot + "/Help/index.aspx";
        public static readonly string HelpAboutHyperLink = AdminRoot + "/Help/About.aspx";

        public static readonly string LogOutHyperLink = AdminRoot + "/LogOut.aspx";
        //public static readonly string LoginHyperLink = AdminRoot + "/LogIn.aspx";
        public static readonly string LoginHyperLink = "~/default.aspx";

        public static readonly string EditSkipLogicHyperLink = AdminRoot + "/EditSkipLogicRules.aspx";
        public static readonly string TakeSurveyHyperLink = AdminRoot + "/TakeSurvey.aspx";
        public static readonly string RegExEditorHyperLink = AdminRoot + "/EditRegEx.aspx";
        public static readonly string ImportUsersHyperLink = AdminRoot + "/UsersManager.aspx?tabindex=2";
        public static readonly string FileManagerHyperLink = AdminRoot + "/FileManager.aspx";
        public static readonly string MailingLogHyperLink = AdminRoot + "/MailingLog.aspx";
        public static readonly string MultiLanguagesHyperLink = AdminRoot + "/MultiLanguages.aspx";
        public static readonly string SurveyListHyperLink = AdminRoot + "/SurveyList.aspx";
        public static readonly string DataImportHyperLink = AdminRoot + "/DataImport.aspx";

        // Survey 2.0 Release Additions
        public static readonly string CampaignPreViewHyperLink = AdminRoot + "/CampaignStart.aspx";
        public static readonly string MailingHyperlink = AdminRoot + "/SurveyMailing.aspx";
        public static readonly string LibraryTemplateHyperlink = AdminRoot + "/LibraryTemplates.aspx";
        public static readonly string WarningScreenHyperlink = AdminRoot + "/WarningScreen.aspx";
        
        // Survey 2.4 Release Additions
        public static readonly string GeneralSettingsLink = AdminRoot + "/Settings.aspx";
        public static readonly string CssXmlHyperLink = AdminRoot + "/EditCssXml.aspx";

        // Survey 2.5 Release Additions
        public static readonly string ErrorLogLink = AdminRoot + "/ErrorLog.aspx";


        public static void NavigateToSurveyOptions(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("surveyoptions.aspx?surveyid={0}&menuindex={1}", surveyId, menuIndex));
        }

        public static void NavigateToSurveyPrivacyOptions(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("surveyprivacy.aspx?surveyid={0}&menuindex={1}", surveyId, menuIndex));
        }

        public static void NavigateToSurveyBuilder(int surveyId, int menuIndex,int questionId=-1)
        {
            HttpContext.Current.Response.Redirect(String.Format("surveycontentbuilder.aspx?surveyid={0}&menuindex={1}", surveyId, menuIndex)+
                (questionId==-1?string.Empty:"&"+Constants.Constants.ScrollQuestionQstr+"="+questionId.ToString()));
        }

        public static void NavigateToSingleQuestionEdit(int surveyId, int questionId, int libraryId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&questionid={2}&libraryid={3}&menuindex={4}",
                EditSingleQuestionLink, surveyId, questionId, libraryId, menuIndex));
        }

        public static void NavigateToSingleQuestionAnswersEdit(int surveyId, int questionId, int libraryId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&questionid={2}&libraryid={3}&menuindex={4}",
                AnswerEditorHyperLink, surveyId, questionId, libraryId, menuIndex));
        }

        public static void NavigateToMatrixQuestionEdit(int surveyId, int parentQuestionId, int libraryId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&parentquestionid={2}&libraryid={3}&menuindex={4}",
                EditMatrixQuestionLink, surveyId, parentQuestionId, libraryId, menuIndex));
        }

        public static void NavigateToVoterReport(int surveyId, int voterId, int menuIndex)
        {
            HttpContext.Current.Server.Transfer(String.Format("{0}?surveyid={1}&voterid={2}&menuindex={3}", VoterReport, surveyId, voterId, menuIndex));
            // HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&voterid={2}&menuindex={3}", VoterReport, surveyId, voterId, menuIndex));
        }

        public static void NavigateToSsrsReport(string fileName, int menuIndex)
        {
           //HttpContext.Current.Server.Transfer(String.Format("~/NSurveyReports/{0}?&menuindex={1}", fileName, menuIndex));
            HttpContext.Current.Response.Redirect(String.Format("~/NSurveyReports/{0}?&menuindex={1}", fileName, menuIndex));
        }

        public static void NavigateToEditVoterReport(int surveyId, int voterId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&voterid={2}&menuindex={3}", EditVoterReport, surveyId, voterId, menuIndex));
        }

        public static void NavigateToInsertQuestion(int surveyId, int displayOrder, int page, int libraryId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&displayorder={2}&page={3}&libraryid={4}&menuindex={5}",
                InsertQuestionLink, surveyId, displayOrder, page, libraryId, menuIndex));
        }

        public static void NavigateToTypeEditor(int menuIndex)
        {
            HttpContext.Current.Response.Redirect(TypeEditor + "?menuindex=" + menuIndex);
        }

        public static void NavigateToTypeCreator(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(TypeCreator + "?surveyid" + surveyId + "?menuindex=" + menuIndex);
            
        }

        public static void NavigateToFilterEditor(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&menuindex={2}", FilterEditor, surveyId, menuIndex));
        }

        public static void NavigateToMailingPoll(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&menuindex={2}", MailingPoll, surveyId, menuIndex), false);
        }

        public static void NavigateToLibraryDirectory(int surveyId, int menuIndex, int tabIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?tabindex={1}", LibraryDirectoryHyperLink, tabIndex), false);
        }

        public static void NavigateToLibraryTemplates(int surveyId, int libraryId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&libraryid={2}&menuindex={3}", LibraryTemplateHyperlink, surveyId, libraryId, menuIndex), false);
        }

        public static void NavigateToSurveyCreation()
        {
            HttpContext.Current.Response.Redirect("NSurveyAdmin/SurveyList.aspx?tabindex=1");
        }

        public static void NavigateToCurrentUrl()
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        }

        public static void NavigateToSurveySecurity(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("surveysecurity.aspx?surveyid={0}&menuindex={1}", surveyId, menuIndex));
        }

        public static void NavigateToUserManager(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&menuindex={2}", UsersManagerHyperLink, surveyId, menuIndex));
        }

        public static void NavigateToRoleManager(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}&surveyid={1}&menuindex={2}", RolesManagerHyperLink, surveyId, menuIndex));
        }

        public static void NavigateToAccessDenied(int surveyId, int menuIndex)
        {
            HttpContext.Current.Response.Redirect(String.Format("{0}?surveyid={1}&menuindex={2}", AccessDeniedHyperLink, surveyId, menuIndex));
        }

        public static void NavigateToLogin()
        {
            HttpContext.Current.Response.Redirect(LoginHyperLink);
        }

        /// <summary>
        /// Navigates to the first url a user has access to
        /// </summary>
        public static void NavigateToFirstAccess(INSurveyPrincipal user, int surveyId)
        {
            string destURL = AccessDeniedHyperLink;

            if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessSurveyList))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyListHyperLink, surveyId, (int)NavigationMenuItems.SurveyList);
            
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessStats))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyStats, surveyId, (int)NavigationMenuItems.SurveyStats);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessReports))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", ResultsReportHyperlink, surveyId, (int)NavigationMenuItems.ResultsReport);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessSurveySettings))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyOptionsLink, surveyId, (int)NavigationMenuItems.SurveyOptions);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.CreateSurvey))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", CreateSurveyLink, surveyId, (int)NavigationMenuItems.NewSurvey);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessFormBuilder))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyContentBuilderLink, surveyId, (int)NavigationMenuItems.SurveyBuilder);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessMailing))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyMailing, surveyId, (int)NavigationMenuItems.Mailing);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessASPNetCode))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", ASPNETCode, surveyId, (int)NavigationMenuItems.ASPNetCode);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.TakeSurvey))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", TakeSurveyHyperLink, surveyId, (int)NavigationMenuItems.TakeSurvey);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessLibrary))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", LibraryDirectoryHyperLink, surveyId, (int)NavigationMenuItems.TheLibrary);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessSecuritySettings))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveySecurityLink, surveyId, (int)NavigationMenuItems.SurveySecurity);

            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessUserManager))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", UsersManagerHyperLink, surveyId, (int)NavigationMenuItems.UsersManager);

            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessHelpFiles))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", HelpFilesHyperLink, surveyId, (int)NavigationMenuItems.HelpFiles);
            else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.SurveyLayoutRight))
                destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyLayoutLink, surveyId, (int)NavigationMenuItems.SurveyLayout);
            //else if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessSurveyList))
            //   destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyListHyperLink, surveyId, (int)NavigationMenuItems.SurveyList);

            // global override redirect url
            // option one: leads to unwanted SurveyTaker Not authorised message
            //destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyListHyperLink, surveyId, (int)NavigationMenuItems.SurveyOptions);
            //destURL = string.Format("{0}?surveyid={1}&menuindex={2}", SurveyListHyperLink, surveyId, (int)NavigationMenuItems.SurveyList);
            // option two: option to avoid SurveyTaker NA message; however why should you do a global override anyway? just leave it out.
            //destURL = string.Format("{0}?surveyid={1}&menuindex={2}", TakeSurveyHyperLink, surveyId, (int)NavigationMenuItems.TakeSurvey);
            HttpContext.Current.Response.Redirect(destURL);
        }

        /*
         * Survey 2.0 Release Additions 
         */

        /// <summary>
        /// For a given parameter name return the expression.
        /// For example for SuryeyId return SurveyId=22
        /// 
        /// </summary>
        /// <param name="parameterNames"></param>
        /// <returns></returns>
        public static string getRequestParameterExpression(FormParameters[] parameterNames)
        {
            string retval = string.Empty, s;

            foreach (FormParameters param in parameterNames)
            {
                if (HttpContext.Current.Request[param.ToString()] != null)
                    s = param + "=" + HttpContext.Current.Request[param.ToString()];
                else s = param + "=-1";
                retval = retval.Length == 0 ? s : retval + "&" + s;
            }
            return (retval.Length > 0) ? "?" + retval : retval;
        }


        /// <summary>
        /// 
        /// If Library Id parameter exists and is not -1 return URL else return null
        /// </summary>
        /// <returns></returns>
        public static string LibraryTemplateReturnUrl()
        {
            if (GetParameterValue(FormParameters.libraryid) == null || GetParameterValue(FormParameters.libraryid) == "-1") return null;
            return UINavigator.LibraryTemplateHyperlink + getRequestParameterExpression(new FormParameters[] { FormParameters.libraryid, FormParameters.surveyid });
        }

        public static string GetParameterValue(FormParameters paramName)
        {
            return HttpContext.Current.Request.Params[paramName.ToString()];
        }

        public static bool isParameterSupplied(PageBase pbase, FormParameters paramName)
        {

            if (paramName == FormParameters.folderid)
                if (pbase.SelectedFolderId == -1) return false;
                else return true;

            if (GetParameterValue(paramName) == null || GetParameterValue(paramName) == "-1") return false;

            return true;


        }
        /// <summary>
        /// Return true if all the Parameters in the List are available
        /// </summary>
        /// <param name="paramNames"></param>
        /// <returns></returns>
        public static bool AreParametersSupplied(PageBase pBase, FormParameters[] paramNamesList)
        {
            foreach (FormParameters par in paramNamesList)
                if (!isParameterSupplied(pBase, par)) return false;

            return true;
        }

        public static void NavigateToWarningScreen()
        {
            HttpContext.Current.Response.Redirect(UINavigator.WarningScreenHyperlink);
        }

        public static void NavigateToTakeSurveyScreen(string languageCode)
        {
            HttpContext.Current.Response.Redirect(string.Format("{0}?languagecode={1}", UINavigator.TakeSurveyHyperLink, languageCode));
        }

    }
}
