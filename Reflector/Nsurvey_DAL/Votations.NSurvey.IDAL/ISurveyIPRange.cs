
namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    public interface ISurveyIPRange
    {
        SurveyIPRangeData GetAllSurveyIPRanges(int surveyID);
        void UpdateSurveyIPRange(SurveyIPRangeData ipData);
        void DeleteSurveyIPRangeById(int surveyIPId);
        void AddNewSurveyIPRange(SurveyIPRangeData ipData);
    }
}

