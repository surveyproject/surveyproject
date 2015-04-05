namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// User interface for the user DAL.
    /// </summary>
    public interface IUser
    {
        void AddUser(NSurveyUserData newUser);
        void AddUserSettings(UserSettingData newSettings);
        void DeleteUserById(int userId);
        int GetAdminCount();
        UserData GetAllUsersList();
        NSurveyUserData GetNSurveyUserData(string userName, string password);
        NSurveyUserData GetUserById(int userId);
        int GetUserByIdFromUserName(string userName);
        int[] GetUserSecurityRights(int userId);
        UserSettingData GetUserSettings(int userId);
        bool IsAdministrator(int userId);
        void UpdateUser(NSurveyUserData updatedUser);
        void UpdateUserSettings(UserSettingData updatedSettings);
    }
}

