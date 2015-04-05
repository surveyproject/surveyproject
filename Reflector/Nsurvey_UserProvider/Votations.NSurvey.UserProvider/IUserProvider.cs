namespace Votations.NSurvey.UserProvider
{
    using System;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Web.Security;

    /// <summary>
    /// To those who wants to implement an other user provider 
    /// than the default one
    /// </summary>
    public interface IUserProvider
    {
        INSurveyPrincipal CreatePrincipal(string userName);
        UserData GetAllUsersList();
    }
}

