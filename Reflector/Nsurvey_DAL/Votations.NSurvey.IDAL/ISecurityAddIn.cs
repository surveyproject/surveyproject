namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Interface for the web security addins DAL
    /// </summary>
    public interface ISecurityAddIn
    {
        void AddSecurityAddInToSurvey(int surveyId, int addInId, int addInOrder);
        void DeleteWebSecurityAddIn(int surveyId, int addInId);
        void DisableWebSecurityAddIn(int surveyId, int addInId);
        void EnableWebSecurityAddIn(int surveyId, int addInId);
        WebSecurityAddInData GetAvailableAddIns(int surveyId);
        WebSecurityAddInData GetEnabledWebSecurityAddIns(int surveyId);
        WebSecurityAddInData GetSurveyAddInById(int surveyId, int addInId);
        UnAuthentifiedUserActionData GetUnAuthentifiedUserActions();
        WebSecurityAddInData GetWebSecurityAddIns(int surveyId);
        void MoveWebSecurityAddInDown(int surveyId, int addInId);
        void MoveWebSecurityAddInUp(int surveyId, int addInId);
    }
}

