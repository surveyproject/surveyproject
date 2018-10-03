//-----------------------------------------------------------------------
// <copyright file="PageBase.cs" company="W3DevPro">
//
// Survey™ Project changes: copyright (c) 2009-2017, W3DevPro™ (https://github.com/surveyproject) 
//
// NSurvey - The web survey and form engine
// Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//
// </copyright>
//-----------------------------------------------------------------------

namespace Votations.NSurvey.WebAdmin
{
    using Microsoft.VisualBasic;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;
    using Votations.NSurvey.UserProvider;
    using Votations.NSurvey.Web;
    using Votations.NSurvey.WebAdmin.UserControls;
    using Votations.NSurvey.Web.Security;
    using System.Collections.Generic;

    /// <summary>
    /// This page contains logic common to all SP Web pages.
    /// Info: http://www.4guysfromrolla.com/articles/041305-1.aspx 
    /// </summary>
    public class PageBase : Page
    {
       
        /// <summary>
        /// Creates new nsurvey context from the current user name
        /// </summary>
        public PageBase()
        {
            if (Context != null)
            {
                if (User == null || User.Identity == null)
                {
                    NSurveyContext.Current.User =
                      UserFactory.Create().CreatePrincipal(null);
                }
                else
                {
                    // Creates new nsurvey context
                    // from the current user name
                    NSurveyContext.Current.User =
                      UserFactory.Create().CreatePrincipal(User.Identity.Name);
                }
            }
        }

        /// <summary>
        /// Selected FolderId from the SurveyTree treeview control
        /// </summary>
        public int? SelectedFolderId
        {
            get { return (int?)Session["FolderID"]; }
            set { Session["FolderID"] = value; }
        }

        /// <summary>
        ///  Sort order of the Surveylist Gridview 
        /// </summary>
        public string SurveyListOrder
        {
            get { return (string)Session["SurveyOrder"]; }
            set { Session["SurveyOrder"] = value; }
        }

        /// <summary>
        /// Action on deleting a survey. Used on the SurveyOptionControl webpage
        /// Whenever a survey is deleted this should be called
        /// </summary>
        /// <param name="deletedSurveyId"></param>
        public void SurveyDeleteActions(int deletedSurveyId)
        {
            if (getSurveyId() == -1) return; // No action
            if (getSurveyId() == deletedSurveyId) SurveyId = -1;
        }

        private int GetDefaultIfApplicable()
        {
            foreach (var survey in new Surveys().GetAssignedSurveysList(((PageBase)Page).NSurveyUser.Identity.UserId).Surveys)
                if (survey.DefaultSurvey) return survey.SurveyId;
            return -1;

        }

        /// <summary>
        /// JJ No more requirement to set SurveyId by picking active Survey from Database . SurveyId can only be set from Tree Control
        /// </summary>
        public int SurveyId
        {
            get
            {

                if (Session[SessionParameters.SurveyId.ToString()] == null) _surveyId = -1;
                else _surveyId = (int)Session[SessionParameters.SurveyId.ToString()];

                if (_surveyId == -1)
                {
                    _surveyId = GetDefaultIfApplicable();
                    if (_surveyId > -1)
                    {
                        Session[SessionParameters.SurveyId.ToString()] = _surveyId;
                        var master = (this.Master == null) ? null : (this.Master is Wap) ? this.Master : this.Master.Master;
                        if (master != null)
                        {
                            ((Wap)master).isTreeStale = true;

                        }
                    }
                    //  ValidateSurveyId();

                    if (_surveyId == -1)
                    {
                        if (CheckRight(NSurveyRights.CreateSurvey, false))
                        {
                            UINavigator.NavigateToWarningScreen();

                            //  Response.Write("<a href=/NSurveyAdmin/SurveyList.aspx?tabindex=1>" + GetPageResource("NoSurveysAvailableMessage") + "</a>");
                        }
                        else
                        {
                            if (NSurveyUser.Identity.UserId == -1)
                            {
                                Response.Write(GetPageResource("NoAnonymousRightsMessage"));

                            }
                            //else if(NSurveyUser.Identity.UserId == 0)
                            //        {

                            //    UINavigator.NavigateToWarningScreen();
                            //        }
                            else
                            {
                                UINavigator.NavigateToWarningScreen();
                            }
                        }
                        Response.End();
                    }
                }

                return _surveyId;
            }
            set
            {
                _surveyId = value; Session[SessionParameters.SurveyId.ToString()] = value;

                var master = (this.Master == null) ? null : (this.Master is Wap) ? this.Master : this.Master.Master;
                if (master != null)
                {
                    // ((Wap)master).isTreeStale = true;

                }
            }
        }

        /// <summary>
        /// Survey Title
        /// </summary>
        public string SurveyTitle
        {
            get
            {
                if (Session[SessionParameters.SurveyTitle.ToString()] == null) return null;
                else return (string)Session[SessionParameters.SurveyTitle.ToString()];
            }
            set
            {
                Session[SessionParameters.SurveyTitle.ToString()] = value;
            }
        }

        /// <summary>
        /// Question Library LibraryID
        /// </summary>
        public int LibraryId
        {
            get
            {
                if (Session[SessionParameters.LibraryId.ToString()] == null) return -1;
                else return (int)Session[SessionParameters.LibraryId.ToString()];
            }
            set
            {
                Session[SessionParameters.LibraryId.ToString()] = value;
            }
        }

        /// <summary>
        /// NSurveyUser
        /// </summary>
        public INSurveyPrincipal NSurveyUser
        {
            get { return NSurveyContext.Current.User; }
        }

        /// <summary>
        /// MenuIndex
        /// </summary>
        public int MenuIndex
        {
            get
            {
                return Request["menuindex"] != null && Information.IsNumeric(Request["menuindex"]) ?
                  Convert.ToInt32(Request["menuindex"]) : -1;
            }
        }
        /// <summary>
        /// GetPageResource: refers to language files in XmlData/Languages directory
        /// Directory path determined in web.config file Nsurvey Settings
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public string GetPageResource(string resourceName)
        {
            return ResourceManager.GetString(resourceName);
        }

        /// <summary>
        /// GetPageHelpfiles: refers to Helpfiles in XmlData/Helpfiles directory
        /// Directory path determined in web.config file Nsurvey Settings
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public string GetPageHelpfiles(string resourceName)
        {
            return HelpfilesManager.GetString(resourceName);
        }

        /// <summary>
        /// GetPageHelpfiles: refers to Helpfiles in XmlData/Helpfiles directory
        /// Directory path determined in web.config file Nsurvey Settings
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public string GetPageLanguageCodes(string resourceName)
        {
            return LanguageCodesManager.GetString(resourceName);
        }


        /// <summary>
        /// ValidateSurveyId
        /// </summary>
        void ValidateSurveyId()
        {
            if (NSurveyUser.Identity.IsAdmin ||
              NSurveyUser.Identity.HasAllSurveyAccess)
            {
                _surveyId = Information.IsNumeric(Request["SurveyID"]) && int.Parse(Request["SurveyID"]) != -1 ?
                  int.Parse(Request["SurveyID"]) : new Surveys().GetFirstSurveyId();
            }
            else
            {
                _surveyId = Information.IsNumeric(Request["SurveyID"]) && int.Parse(Request["SurveyID"]) != -1 ?
                  int.Parse(Request["SurveyID"]) : new Surveys().GetFirstSurveyId(NSurveyUser.Identity.UserId);

                // Makes sure the user is the owner of the survey
                if (Information.IsNumeric(Request["SurveyID"]) && _surveyId != -1 &&
                  !new Survey().CheckSurveyUser(_surveyId, NSurveyUser.Identity.UserId))
                {
                    _surveyId = new Surveys().GetFirstSurveyId(NSurveyUser.Identity.UserId);
                }
            }

        }

        /// <summary>
        /// Look up the give item texts in the resource file 
        /// to translate them
        /// </summary>
        /// <param name="unTranslatedListControl"></param>
        /// <param name="reOrder"></param>
        public void TranslateListControl(ListControl unTranslatedListControl, bool reOrder = false)
        {
            List<ListItem> items = new List<ListItem>();
            string translatedText;
            foreach (ListItem item in unTranslatedListControl.Items)
            {
                translatedText = GetPageResource(item.Text);
                item.Text = translatedText == null ? item.Text : translatedText;
                if (reOrder) items.Add(item);
            }
            if (reOrder)
            {
                StringComparer comp = StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentUICulture, false);
                unTranslatedListControl.Items.Clear();
                items = items.OrderBy(x => x.Text, comp).ToList();
                items.ForEach(x => unTranslatedListControl.Items.Add(x));

            }
        }



        /// <summary>
        /// Translate the Roles/ RoleRights list:
        /// Look up the give item texts in the resource file 
        /// to translate them
        /// </summary>
        /// <param name="unTranslatedListControl"></param>
        /// <param name="reOrder"></param>
        public void TranslateRoleRightsListControl(ListControl unTranslatedListControl, bool reOrder = false)
        {
            List<ListItem> items = new List<ListItem>();
            string translatedText;
            foreach (ListItem item in unTranslatedListControl.Items)
            {
                translatedText = GetPageResource(item.Text.Substring(100).Trim());
                item.Text = translatedText == null ? item.Text : item.Text.Remove(100,35) + "<span class='chckPrivsLabel'>" + translatedText + "</span>" ;
                if (reOrder) items.Add(item);
            }
            if (reOrder)
            {
                StringComparer comp = StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentUICulture, false);
                unTranslatedListControl.Items.Clear();
                items = items.OrderBy(x => x.Text, comp).ToList();
                items.ForEach(x => unTranslatedListControl.Items.Add(x));

            }
        }


        /// <summary>
        /// Translate (multi) Languagages drop down lists:
        /// Look up the give item texts in the resource file 
        /// to translate them
        /// </summary>
        /// <param name="unTranslatedListControl"></param>
        /// <param name="reOrder"></param>
        public void LanguageTranslateListControl(ListControl unTranslatedListControl, bool reOrder = false)
        {
            List<ListItem> items = new List<ListItem>();
            string translatedText;
            foreach (ListItem item in unTranslatedListControl.Items)
            {
                translatedText = GetPageLanguageCodes(item.Text);
                item.Text = translatedText == null ? item.Text : translatedText;
                if (reOrder) items.Add(item);
            }
            if (reOrder)
            {
                StringComparer comp = StringComparer.Create(System.Threading.Thread.CurrentThread.CurrentUICulture, false);
                unTranslatedListControl.Items.Clear();
                items = items.OrderBy(x => x.Text, comp).ToList();
                items.ForEach(x => unTranslatedListControl.Items.Add(x));

            }
        }


        /// <summary>
        /// isEmail check validity of email address format based on regular expression
        /// </summary>
        /// <param name="inputEmail"></param>
        /// <returns>true or false</returns>
        public bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// Check if the current user has the given right
        /// else if specified redirect to the access denied page
        /// </summary>
        /// <param name="right"></param>
        /// <param name="accessDeniedRedirect"></param>
        /// <returns>true or false</returns>
        public bool CheckRight(NSurveyRights right, bool accessDeniedRedirect)
        {
            if (NSurveyUser.Identity.IsAdmin ||
              NSurveyUser.HasRight(right))
            {
                return true;
            }
            else if (accessDeniedRedirect)
            {
                UINavigator.NavigateToAccessDenied(getSurveyId(), MenuIndex);
            }

            return false;
        }

        /// <summary>
        /// IsSingleUserMode boolean
        /// </summary>
        /// <remarks>Check if the system was setup in single user mode</remarks>
        /// <param name="redirectIfSingle"></param>
        /// <returns>true or false</returns>

        public bool IsSingleUserMode(bool redirectIfSingle)
        {
            // Did the system setup a dummy admin
            if (NSurveyUser.Identity.UserId == 0 &&
              NSurveyUser.Identity.IsAdmin)
            {
                if (redirectIfSingle)
                {
                    UINavigator.NavigateToAccessDenied(SurveyId, MenuIndex);
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// _surveyId
        /// </summary>
        private int _surveyId = -1;

        /// <summary>
        /// JJ There are 2 ways to get SurveyId from PageBase. If you use SurveyId property if survey is not set it redirects to No access form
        /// However the tree control and Menu system in header user control request SurveyId and at that point it is valid to have no SurveyId set.If some other form 
        /// requests surveyid it is through the SurveyId property a redirection can Occur
        /// </summary>
        /// <returns></returns>
        public int getSurveyId()
        {
            if (Session[SessionParameters.SurveyId.ToString()] == null) return -1;
            else return (int)Session[SessionParameters.SurveyId.ToString()];
        }

        /// <summary>
        /// Message (information and confirmation) shown on succesfull operations
        /// </summary>
        /// <param name="l">Label</param>
        /// <param name="s">Text String from XML language file</param>
        public void ShowNormalMessage(Label l, string s)
        {
            l.Visible = true;
            l.CssClass = CssXmlManager.GetString("SuccessMessage");
            l.Text = s;

        }

        /// <summary>
        /// Error Message shown on failed operations
        /// </summary>
        /// <param name="l">Label</param>
        /// <param name="s">Text String from XML language file</param>
        public void ShowErrorMessage(Label l, string s)
        {
            l.Visible = true;
            l.CssClass = CssXmlManager.GetString("ErrorMessage");
            l.Text = s;

        }
    }
}
