namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provides the method to access the security addins's data.
    /// </summary>
    public class SecurityAddIns
    {
        /// <summary>
        /// Return a AddIn object that reflects the database addin data
        /// </summary>
        /// <param name="addInId">Id of the addin you need</param>
        /// <returns>A WebSecurityAddInData object with the current database values</returns>
        public WebSecurityAddInData GetAddInById(int surveyId, int addInId)
        {
            return SecurityAddInFactory.Create().GetSurveyAddInById(surveyId, addInId);
        }

        /// <summary>
        /// Returns the available addins (addins that are not already in use) 
        /// for the survey
        /// </summary>
        /// <param name="surveyId"></param>
        public WebSecurityAddInData GetAvailableAddIns(int surveyId)
        {
            return SecurityAddInFactory.Create().GetAvailableAddIns(surveyId);
        }

        /// <summary>
        /// Get all ony security addins that are enabled for the survey
        /// </summary>
        public WebSecurityAddInData GetEnabledWebSecurityAddIns(int surveyId)
        {
            return SecurityAddInFactory.Create().GetEnabledWebSecurityAddIns(surveyId);
        }

        /// <summary>
        /// Get a list of all available actions
        /// </summary>
        /// <returns></returns>
        public UnAuthentifiedUserActionData GetUnAuthentifiedUserActions()
        {
            return SecurityAddInFactory.Create().GetUnAuthentifiedUserActions();
        }

        /// <summary>
        /// Get all security addins available for the survey
        /// </summary>
        public WebSecurityAddInData GetWebSecurityAddIns(int surveyId)
        {
            return SecurityAddInFactory.Create().GetWebSecurityAddIns(surveyId);
        }
    }
}

