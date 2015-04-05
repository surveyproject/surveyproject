namespace Votations.NSurvey.Web.Security
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// NSurveyPrincipal interface for those who wants 
    /// to create a custom nsurvey principal 
    /// </summary>
    public interface INSurveyPrincipal
    {
        bool HasRight(NSurveyRights right);

        INSurveyIdentity Identity { get; }
    }
}

