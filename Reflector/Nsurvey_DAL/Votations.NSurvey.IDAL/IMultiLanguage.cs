namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// interface for the multi languages DAL.
    /// </summary>
    public interface IMultiLanguage
    {
        string CheckSurveyLanguage(int surveyId, string languageCode);
        void DeleteSurveyLanguage(int surveyId, string languageCode, string Entity);
        void DisableMultiLanguage(int surveyId);
        MultiLanguageMode GetMultiLanguageMode(int surveyId);
        MultiLanguageData GetMultiLanguages();
        MultiLanguageData GetSurveyLanguages(int surveyId, string Entity);
        void UpdateMultiLanguage(int surveyId, MultiLanguageMode languageMode, string variableName);
        void UpdateSurveyLanguage(int surveyId, string languageCode, bool defaultLanguage, string Entity);
    }
}

