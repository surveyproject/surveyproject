namespace Votations.NSurvey.UserProvider
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Web.Security;

    /// <summary>
    /// Implements the default provider to support 
    /// nsurvey's form authentication
    /// </summary>
    public class FormUserProvider : INSurveyUserProvider, IUserProvider
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
            int index = userName.IndexOf("|");
            if (index < 0)
            {
                return new NSurveyFormPrincipal(new NSurveyFormIdentity("anonymous", -1, null, null, null, false, false, false), null);
            }
            string[] strArray = userName.Substring(0, index).Split(new char[] { ',' });
            if (strArray.Length < 6)
            {
                return new NSurveyFormPrincipal(new NSurveyFormIdentity("anonymous", -1, null, null, null, false, false, false), null);
            }
            return new NSurveyFormPrincipal(new NSurveyFormIdentity(strArray[0], int.Parse(strArray[1]), strArray[2], strArray[3], strArray[4],
                bool.Parse(strArray[5]), bool.Parse(strArray[6]), true), userName.Substring(index + 1).Split(new char[] { ',' }));
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
    }
}

