namespace Votations.NSurvey.BusinessRules
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the business rules that are used for Survey IP Range
    /// </summary>
    public class SurveyIPRange
    {
       
        public void UpdateIPRange(SurveyIPRangeData data)
        {
            SurveyIPRangeFactory.Create().UpdateSurveyIPRange(data);
        }

        public SurveyIPRangeData GetAllSurveyIPranges(int surveyId)
        {
           return  SurveyIPRangeFactory.Create().GetAllSurveyIPRanges(surveyId);
        }

        public void DeleteSurveyIPRangeById(int surveyIPId)
        {
            SurveyIPRangeFactory.Create().DeleteSurveyIPRangeById(surveyIPId);
        }

        public void AddNewSurveyIPRange(SurveyIPRangeData data)
        {
            SurveyIPRangeFactory.Create().AddNewSurveyIPRange(data);
        }

    }
}

