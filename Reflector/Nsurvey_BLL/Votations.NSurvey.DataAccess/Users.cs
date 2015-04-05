namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provides the method to access the user's data.
    /// </summary>
    public class Users
    {
        /// <summary>
        /// Retrieves the number of admins in the database
        /// </summary>
        public int GetAdminCount()
        {
            return UserFactory.Create().GetAdminCount();
        }

        /// <summary>
        /// Retrieves all users from the database
        /// </summary>
        public UserData GetAllUsersList()
        {
            return UserFactory.Create().GetAllUsersList();
        }

        /// <summary>
        /// Retrieves the user if any available
        /// </summary>
        public NSurveyUserData GetNSurveyUserData(string userName, string password)
        {
            return UserFactory.Create().GetNSurveyUserData(userName, password);
        }

        /// <summary>
        /// Retrieves users details from the database
        /// </summary>
        public NSurveyUserData GetUserById(int userId)
        {
            return UserFactory.Create().GetUserById(userId);
        }

        /// <summary>
        /// Retrieves the user id, returns -1 if not found
        /// </summary>
        public int GetUserByIdFromUserName(string userName)
        {
            return UserFactory.Create().GetUserByIdFromUserName(userName);
        }

        /// <summary>
        /// Retrieves an array of the security rights of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int[] GetUserSecurityRights(int userId)
        {
            return UserFactory.Create().GetUserSecurityRights(userId);
        }

        /// <summary>
        /// Get user settings 
        /// </summary>
        public UserSettingData GetUserSettings(int userId)
        {
            return UserFactory.Create().GetUserSettings(userId);
        }

        /// <summary>
        /// Checks if the given user is an administrator
        /// </summary>
        public bool IsAdministrator(int userId)
        {
            return UserFactory.Create().IsAdministrator(userId);
        }
    }
}

