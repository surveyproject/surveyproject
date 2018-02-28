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
using Microsoft.VisualBasic;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Web;
using Votations.NSurvey.Web.Security;
namespace Votations.NSurvey.WebAdmin.UserControls
{


    /// <summary>
    /// Headercontrol including Menu items
    /// </summary>
    public partial class HeaderControl : System.Web.UI.UserControl
    {
        /*
		protected System.Web.UI.WebControls.HyperLink FieldReportHyperlink;
		protected System.Web.UI.WebControls.HyperLink Hyperlink1;
		protected System.Web.UI.WebControls.HyperLink Hyperlink2;
        */

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            //this.MainMenuDataList.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.MainMenuDataList_ItemDataBound);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            LocalizePage();
        }

        private void LocalizePage()
        {

            _menuIndex = (Request["menuindex"] == null) ? -1 : Convert.ToInt32(Request["menuindex"]);

            PageBase page = (PageBase)Page;
            INSurveyPrincipal user = page.NSurveyUser;

            //if (page.IsSingleUserMode(false) && user.Identity.IsAuthenticated)
            if (user.Identity.IsAuthenticated)
                    MenuUserName.Visible = true;
                    MenuUserName.Text = page.GetPageResource("LoggedInAs") + " " +page.NSurveyUser.Identity.FirstName +" "+ page.NSurveyUser.Identity.LastName;

            if (!page.IsSingleUserMode(false) && user.Identity.IsAuthenticated)
            {
                LogoutButton.Visible = true;
                LogoutButton.ToolTip = page.GetPageResource("LogOutHyperlink");
//                LogoutButton.Text = page.NSurveyUser.Identity.Name + page.GetPageResource("LoggedInAs");

            }


 //           if (user.Identity.IsAdmin || user.HasRight(NSurveyRights.AccessHelpFiles))
 //           {
 //                HelpButton.Visible = true;
 //               HelpButton.Text = page.GetPageResource("HelpFilesHyperlink");
 //           }

            if (mnuMain.Items.Count > 0) return; // If menu is already populated return

            int menuIndex = 0;


            //Setup Hierarchical Menu based on users Access Rights

            #region Surveys Submenu
            // Setup  "Surveys" SubMenu

            if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSurveySettings)
              || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessMultiLanguages) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessPrivacySettings)
                || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.SurveyLayoutRight) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSurveyList) ||
                ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.CreateSurvey) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessStats)
                || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSecuritySettings) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.TokenSecurityRight))
            {
                mnuMain.Items.Add(new MenuItem(((PageBase)Page).GetPageResource("SurveysHyperLink"), null, null, string.Empty));
                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSurveyList))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("ListSurveyHyperLink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.ListSurveyLink, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.CreateSurvey))
                {


                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("NewSurveyHyperLink"), null, null, string.Format("{0}&surveyid={1}", UINavigator.CreateSurveyLink, SurveyId)));

                }

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessStats))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyStatsHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveyStats, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSurveySettings)
                  || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessMultiLanguages) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessPrivacySettings)
                    || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.SurveyLayoutRight))
                {
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyOptionsHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveyOptionsLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin)
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                            Add(new MenuItem(((PageBase)Page).GetPageResource("GeneralSettingsHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.GeneralSettingsLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSurveySettings))
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                            Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyInformationHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveyOptionsLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessMultiLanguages))
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                            Add(new MenuItem(((PageBase)Page).GetPageResource("MultiLanguagesHyperLink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.MultiLanguagesHyperLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessPrivacySettings))
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                            Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyInfoCompletion"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveyPrivacyLink, SurveyId)));
                   
                }
                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSecuritySettings) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.TokenSecurityRight))
                {
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("SurveySecurityHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveySecurityLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessSecuritySettings))
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                            Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyFormSecurityHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveySecurityLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.TokenSecurityRight))
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                            Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyTokenSecurityHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveyTokenSecurityLink, SurveyId)));
                    mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                                Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyIPRangeSecurityHyperlink"), null, null, string.Format("{0}", UINavigator.SurveyIPSecurityLink)));
              
                
                }


                menuIndex++;
            }
            #endregion

            #region Designer
            if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessFormBuilder)
                || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessLibrary) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.ManageLibrary)
                || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessQuestionGroupRight) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessAnswerTypeEditor)
                || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessRegExEditor) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.SurveyLayoutRight)
                ) 
            {
                mnuMain.Items.Add(new MenuItem(((PageBase)Page).GetPageResource("DesignerHyperlink"), null, null, String.Empty));
                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessFormBuilder))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyBuilderHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveyContentBuilderLink, SurveyId)));


                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessLibrary)
                  || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.ManageLibrary))
                {
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("LocalLibraryHyperLink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.LibraryDirectoryHyperLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessLibrary))
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                                          Add(new MenuItem(((PageBase)Page).GetPageResource("LibraryListHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.LibraryDirectoryHyperLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.ManageLibrary))
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                                         Add(new MenuItem(((PageBase)Page).GetPageResource("CreateLibraryHyperlink"), null, null, string.Format("{0}&surveyid={1}", UINavigator.LibraryCreateHyperLink, SurveyId)));
                }


                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessQuestionGroupRight))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("QuestionGroupsHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.QuestionGroupsLink, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessAnswerTypeEditor))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("TypeBuilderHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.TypeEditor, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessRegExEditor))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("RegExLibHyperLink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.RegExEditorHyperLink, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.SurveyLayoutRight))
                {

                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("SurveyLayoutHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveyLayoutLink, SurveyId)));

                    if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.SurveyLayoutRight))
                        mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                            Add(new MenuItem(((PageBase)Page).GetPageResource("CssXmlHyperLink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.CssXmlHyperLink, SurveyId)));
                }

                menuIndex++;
            }

            #endregion

            #region Results

            if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessReports)
                || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.CreateResultsFilter) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessFileManager)
                || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.DataImportRight) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessExport) ||
                ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessFieldEntries) )
            {
                mnuMain.Items.Add(new MenuItem(((PageBase)Page).GetPageResource("ResultsSMHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.ResultsReportHyperlink, SurveyId)));


                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessReports))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("ResultsReportHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.ResultsReportHyperlink, SurveyId)));


                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.CreateResultsFilter))
                    mnuMain.Items[menuIndex].ChildItems[mnuMain.Items[menuIndex].ChildItems.Count - 1].ChildItems.
                        Add(new MenuItem(((PageBase)Page).GetPageResource("FiltersHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.FilterEditor, SurveyId)));


                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessFieldEntries))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("ResponsesHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.FieldsReportHyperlink, SurveyId)));


                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessFileManager))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("FileManagerHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.FileManagerHyperLink, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessExport))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("DataExportHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.ExportData, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.DataImportRight))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("DataImportHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.DataImportHyperLink, SurveyId)));
                menuIndex++;
            }
            #endregion

            #region Campaigns
            if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.TakeSurvey)
                 || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessASPNetCode) || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessMailing))
            {
                // mnuMain.Items.Add(new MenuItem(((PageBase)Page).GetPageResource("CampaignSMHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.CampaignPreViewHyperLink, SurveyId)));
                mnuMain.Items.Add(new MenuItem(((PageBase)Page).GetPageResource("CampaignSMHyperlink"), null, null, String.Empty));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin 
                    || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessASPNetCode) )
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("CampaignPreviewHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.CampaignPreViewHyperLink, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessASPNetCode))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("ASPNETCodeHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.ASPNETCode, SurveyId)));

                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessMailing))
                {
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("MailingHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.SurveyMailing, SurveyId)));

                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("MailingStatusHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.MailingStatus, SurveyId)));

                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("MailingLogHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.MailingLogHyperLink, SurveyId)));
                }
                if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.TakeSurvey))
                    mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("TakeSurveyHyperLink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.TakeSurveyHyperLink, SurveyId)));

                menuIndex++;
            }
            #endregion

            #region Accounts
            if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessUserManager))
            {

                mnuMain.Items.Add(new MenuItem(((PageBase)Page).GetPageResource("AccountSMHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.UsersManagerHyperLink, SurveyId)));

                mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("UsersManagerHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.UsersManagerHyperLink, SurveyId)));

                mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("RolesManagerHyperlink"), null, null, string.Format("{0}&surveyid={1}", UINavigator.RolesManagerHyperLink, SurveyId)));

                mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("ImportUsersHyperlink"), null, null, string.Format("{0}&surveyid={1}", UINavigator.ImportUsersHyperLink, SurveyId)));

                menuIndex++;
            }
            #endregion

            #region Help
            if (((PageBase)Page).NSurveyUser.Identity.IsAdmin || ((PageBase)Page).NSurveyUser.HasRight(NSurveyRights.AccessHelpFiles))
            {

                mnuMain.Items.Add(new MenuItem(((PageBase)Page).GetPageResource("HelpSMHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.HelpOptionsHyperLink, SurveyId)));

                mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("HelpOptionsHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.HelpOptionsHyperLink, SurveyId)));

                mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("HelpFilesHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.HelpFilesHyperLink, SurveyId)));

                mnuMain.Items[menuIndex].ChildItems.Add(new MenuItem(((PageBase)Page).GetPageResource("HelpAboutHyperlink"), null, null, string.Format("{0}?surveyid={1}", UINavigator.HelpAboutHyperLink, SurveyId)));

                menuIndex++;
            }
            #endregion

            //Add Take Survey
            //   mnuMain.Items.Add(new MenuItem(((PageBase)Page).GetPageResource("TakeSurveyHyperLink"), null, null, string.Format("{0}?surveyid={1}",UINavigator.TakeSurveyHyperLink,SurveyId)));

        }


        public void LogoutButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(UINavigator.LogOutHyperLink);
        }

//        public void HelpButton_Click(object sender, EventArgs e)
//        {
//            Response.Redirect(string.Format("{0}?surveyid={1}&menuindex={2}", UINavigator.HelpFilesHyperLink, SurveyId, (int)NavigationMenuItems.HelpFiles));
//        }

        /// <summary>
        /// Surveyid can be taken from URL 
        /// </summary>
        public int SurveyId
        {
            get { return ((PageBase)Page).getSurveyId(); }
            //    get { return (ViewState["SurveyID"] == null) ? -1 : int.Parse(ViewState["SurveyID"].ToString()); }
            set { ((PageBase)Page).SurveyId = value; }
        }


        int _menuIndex = -1;

    }
}

