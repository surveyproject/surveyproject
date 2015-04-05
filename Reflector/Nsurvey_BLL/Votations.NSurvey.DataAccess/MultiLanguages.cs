namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the methods to access multi language database data.
    /// </summary>
    public class MultiLanguages
    {
        /// <summary>
        /// Returns current mode
        /// </summary>
        public MultiLanguageMode GetMultiLanguageMode(int surveyId)
        {
            return MultiLanguageFactory.Create().GetMultiLanguageMode(surveyId);
        }

        /// <summary>
        /// Get all languages available
        /// </summary>
        public MultiLanguageData GetMultiLanguages()
        {
            return MultiLanguageFactory.Create().GetMultiLanguages();
        }

        /// <summary>
        /// Get all enabled languages for the survey
        /// </summary>
        public MultiLanguageData GetSurveyLanguages(int surveyId, string Entity=Constants.Constants.EntitySurvey)
        {
            return MultiLanguageFactory.Create().GetSurveyLanguages(surveyId, Entity);
        }
    }
}

