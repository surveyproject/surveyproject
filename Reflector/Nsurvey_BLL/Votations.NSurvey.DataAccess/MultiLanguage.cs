namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Constants;
    /// <summary>
    /// Contains the business rules multi language database data.
    /// </summary>
    public class MultiLanguage
    {
        /// <summary>
        /// Check if the language code is enabled for the survey
        /// and if its not returns the default enabled language code
        /// </summary>
        public string CheckSurveyLanguage(int surveyId, string languageCode)
        {
            return MultiLanguageFactory.Create().CheckSurveyLanguage(surveyId, languageCode);
        }

        /// <summary>
        /// Deletes a language from the survey
        /// </summary>
        public void DeleteSurveyLanguage(int surveyId, string languageCode,string Entity=Constants.EntitySurvey)
        {
            MultiLanguageFactory.Create().DeleteSurveyLanguage(surveyId, languageCode,Entity);
        }

        /// <summary>
        /// Disable survey's multi language features
        /// </summary>
        /// <param name="surveyId"></param>
        public void DisableMultiLanguage(int surveyId)
        {
            MultiLanguageFactory.Create().DisableMultiLanguage(surveyId);
        }

        /// <summary>
        /// Updates the current multi language mode that defines how 
        /// the user selects his language
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="languageMode"></param>
        public void UpdateMultiLanguage(int surveyId, MultiLanguageMode languageMode, string variableName)
        {
            MultiLanguageFactory.Create().UpdateMultiLanguage(surveyId, languageMode, variableName);
        }

        /// <summary>
        /// Adds a language to a survey
        /// </summary>
        public void UpdateSurveyLanguage(int surveyId, string languageCode, bool defaultLanguage, string Entity=Constants.EntitySurvey)
        {
            MultiLanguageFactory.Create().UpdateSurveyLanguage(surveyId, languageCode, defaultLanguage, Entity);
        }
    }
}

