namespace Votations.NSurvey.UserProvider
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// To those who wants to implement an other user provider 
    /// than the default one
    /// </summary>
    public interface INSurveyUserProvider : IUserProvider
    {
        void AddUser(NSurveyUserData newUser);
        void DeleteUserById(int userId);
        int GetUserByIdFromUserName(string userName);
        void UpdateUser(NSurveyUserData updatedUser);
    }
}

