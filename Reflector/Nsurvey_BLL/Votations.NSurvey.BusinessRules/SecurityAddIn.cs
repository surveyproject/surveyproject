namespace Votations.NSurvey.BusinessRules
{
    using System;
    using Votations.NSurvey.DALFactory;

    /// <summary>
    /// Provides the business rules for the security addins's data.
    /// </summary>
    public class SecurityAddIn
    {
        /// <summary>
        /// Adds the given security addin to the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInId"></param>
        /// <param name="addInOrder"></param>
        public void AddSecurityAddInToSurvey(int surveyId, int addInId, int addInOrder)
        {
            SecurityAddInFactory.Create().AddSecurityAddInToSurvey(surveyId, addInId, addInOrder);
        }

        /// <summary>
        /// Deletes the given security addin from the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInOrder"></param>
        public void DeleteWebSecurityAddIn(int surveyId, int addInId)
        {
            SecurityAddInFactory.Create().DeleteWebSecurityAddIn(surveyId, addInId);
        }

        /// <summary>
        /// Disables the given addins
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInId"></param>
        public void DisableWebSecurityAddIn(int surveyId, int addInId)
        {
            SecurityAddInFactory.Create().DisableWebSecurityAddIn(surveyId, addInId);
        }

        /// <summary>
        /// Enables the given addins
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInId"></param>
        public void EnableWebSecurityAddIn(int surveyId, int addInId)
        {
            SecurityAddInFactory.Create().EnableWebSecurityAddIn(surveyId, addInId);
        }

        /// <summary>
        /// Move the addin priority down
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInOrder"></param>
        public void MoveWebSecurityAddInDown(int surveyId, int addInId)
        {
            SecurityAddInFactory.Create().MoveWebSecurityAddInDown(surveyId, addInId);
        }

        /// <summary>
        /// Move the addin priority up
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInOrder"></param>
        public void MoveWebSecurityAddInUp(int surveyId, int addInId)
        {
            SecurityAddInFactory.Create().MoveWebSecurityAddInUp(surveyId, addInId);
        }
    }
}

