namespace Votations.NSurvey.Web.Security
{
    using System;

    /// <summary>
    /// NSurveyIdentity interface for those who need to create 
    /// a custom nsurvey identity type
    /// </summary>
    public interface INSurveyIdentity
    {
        string AuthenticationType { get; }

        bool HasAllSurveyAccess { get; }

        bool IsAdmin { get; }

        bool IsAuthenticated { get; }

        string Name { get; }

        int UserId { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}

