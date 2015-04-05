namespace Votations.NSurvey.BusinessRules
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Contains the business rules that are used for Survey IP Range
    /// </summary>
    public class SurveyToken
    {
       
        public SurveyTokenData GetAllSurveyTokens(int surveyId)
        {
            return SurveyTokenFactory.Create().GetAllSurveyTokens(surveyId);
        }
        public void AddSurveyTokensMultiple(int surveyId, DateTime creationDate, int count)
        {
           

          SurveyTokenFactory.Create().AddSurveyTokensMultiple(surveyId, creationDate,  Enumerable.Range(1, count).Select(x => Guid.NewGuid().ToString()));
        }
        public void DeleteSurveyTokensByIdMultiple(IList<int> deleteCandidates)
        {


            SurveyTokenFactory.Create().DeleteSurveyTokensByIdMultiple(deleteCandidates);
        }

        public bool ValidateToken(int surveyID, string token)
        {
            return SurveyTokenFactory.Create().ValidateToken(surveyID, token);
        }
        public void UpdateToken(int surveyID, string token, int voterId)
        {
            SurveyTokenFactory.Create().UpdateToken(surveyID, token, voterId);
        }

    }
}

