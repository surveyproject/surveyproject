using Votations.NSurvey.Data;
using System.Collections.Generic;
namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    public interface ISurveyToken
    {
        SurveyTokenData GetAllSurveyTokens(int surveyID);
        void DeleteSurveyTokensByIdMultiple(IEnumerable<int> Ids);
        void AddSurveyTokensMultiple(int surveyId,DateTime creationDate, IEnumerable<string> tokens);
        bool ValidateToken(int surveyID, string token);
        void UpdateToken(int surveyID, string token,int voterId);
    }
}

