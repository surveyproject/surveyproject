namespace Votations.NSurvey.UserProvider
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Web.Security;
    using System.Collections;
    using System.Collections.Generic;
    using System.DirectoryServices;
    /// <summary>
    /// Implements the default provider to support 
    /// nsurvey's form authentication
    /// </summary>
    public class ADUserProvider : INSurveyUserProvider, IUserProvider
    {
        public void AddUser(NSurveyUserData newUser)
        {
            new User().AddUser(newUser);
        }

        /// <summary>
        /// Creates a new principal based on the provided username
        /// </summary>
        public INSurveyPrincipal CreatePrincipal(string userName)
        {
            if (this.SingleUserMode)
            {
                return new NSurveyFormPrincipal(new NSurveyFormIdentity("nsurvey_admin", 0, null, null, null, true, true, false), null);
            }
            if ((userName == null) || (userName.Length == 0))
            {
                return new NSurveyFormPrincipal(new NSurveyFormIdentity("anonymous", -1, null, null, null, false, false, false), null);
            }
            //Scenario 1: AD user exists in the database :  UserInfo and rights can be retrieved from the database ( import or creation of valid users...)
            int? id = new Users().GetUserByIdFromUserName(userName);
            if ((id ?? 0) > 0)
            {
                var user = new Users().GetUserById(id ?? 0);
                var authUser = user;
                UserSettingData userSettings = new Users().GetUserSettings(authUser.Users[0].UserId);

                if (userSettings.UserSettings.Rows.Count > 0)
                {
                    List<string> userRightsStr = new List<string>();
                    int[] userRights = new Users().GetUserSecurityRights(authUser.Users[0].UserId);
                    for (int i = 0; i < userRights.Length; i++)
                    {
                        userRightsStr.Add(userRights[i].ToString());
                    }
                    return new NSurveyFormPrincipal(new NSurveyFormIdentity(authUser.Users[0].UserName, authUser.Users[0].UserId, authUser.Users[0].FirstName, authUser.Users[0].LastName, authUser.Users[0].Email,
                     userSettings.UserSettings[0].IsAdmin, userSettings.UserSettings[0].GlobalSurveyAccess,true), userRightsStr.ToArray());
                }
            }
            //TODO : Scenario 2: User doesn't exists in the database
            //Extract as much data from AD ( normally everything to even email should be possible... (and create user in the database for statistics?) 
            //Determine rights based on it's group memberships or it's own rights if present in the database

            
            return new NSurveyFormPrincipal(new NSurveyFormIdentity("anonymous", -1, null, null, null, false, false, false), null);

        }
        public string[] GetUsergroups(string user)
        {
            user = RemoveDomainName(user);
            DirectoryEntry searchRoot = (DirectoryEntry)null;
            try
            {
                searchRoot = new DirectoryEntry(string.Format("LDAP://{0}", (object)this.ActiveDirectoryDomain));
                List<string> list = new List<string>();
                DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot);
                using (directorySearcher)
                {
                    directorySearcher.Filter = string.Format("(&(objectClass=user)(name={0}))", (object)user);
                    try
                    {
                        foreach (SearchResult searchResult in directorySearcher.FindAll())
                        {
                            try
                            {
                                foreach (object obj in (ReadOnlyCollectionBase)searchResult.Properties["memberOf"])
                                {
                                    string str1 = obj.ToString();
                                    int num = str1.IndexOf(",");
                                    string str2 = str1.Substring(3, checked(num - 3));
                                    list.Add(str2);
                                }
                            }
                            finally
                            {

                            }
                        }
                    }
                    finally
                    {

                    }
                }
                searchRoot.Close();
                return list.ToArray();
            }
            finally
            {
                if (searchRoot != null)
                    searchRoot.Dispose();
            }
        }

        private string RemoveDomainName(string user)
        {
            //When Logged in through AD the DomainName is at the fromt
            if(user.StartsWith(ActiveDirectoryDomain))
            {
                int skipDivider = 1;
                return user.Substring(ActiveDirectoryDomain.Length + skipDivider);
            }
            return user;
        }

        public void DeleteUserById(int userId)
        {
            new User().DeleteUserById(userId);
        }

        /// <summary>
        /// Retrieves all users from the database
        /// </summary>
        public UserData GetAllUsersList()
        {
            return new Users().GetAllUsersList();
        }

        public int GetUserByIdFromUserName(string userName)
        {
            return new Users().GetUserByIdFromUserName(userName);
        }

        public void UpdateUser(NSurveyUserData updatedUser)
        {
            new User().UpdateUser(updatedUser);
        }

        /// <summary>
        /// Did the user disable / enable authentication 
        /// for this provider using the web.config settings
        /// </summary>
        private bool SingleUserMode
        {
            get
            {
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                string str = config["FormUserProviderSingleMode"];
                return ((str == null) || bool.Parse(str));
            }
        }
        private string ActiveDirectoryDomain
        {
            get
            {
                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                return = config["ADUserProviderDomainName"];
            }
        }
    }
}

