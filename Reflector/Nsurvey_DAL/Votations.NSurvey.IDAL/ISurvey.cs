namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Interface for the survey DAL
    /// </summary>
    public interface ISurvey
    {
        void AddBranchingRule(BranchingRuleData newBranchingRule);
        void AddMessageCondition(MessageConditionData newMessageCondition);
        void AddSurvey(SurveyData newSurvey);
        bool AspSecurityAllowsMultipleSubmissions(int surveyId);
        void AssignUserToSurvey(int surveyId, int userId);
        bool CheckSurveyUser(int surveyId, int userId);
        SurveyData CloneSurvey(int surveyId);
        void DeleteBranchingRuleById(int branchingRuleId);
        void DeleteMessageConditionById(int messageConditionId);
        void DeletePageBreak(int surveyId, int pageNumber);
        void DeleteQuotaSettings(int surveyId);
        void DeleteSurveyById(int surveyId);
        SurveyData GetActivatedSurvey();
        SurveyData GetAllSurveys();
        SurveyData GetAllSurveysList();
        SurveyData GetArchivedSurveys();
        SurveyData GetAssignedSurveysList(int userId);
        int GetCookieExpiration(int surveyId);
        int GetFirstSurveyId();
        int GetSurveyIdFromGuid(Guid guid);
        int GetSurveyIdFromFriendlyName(string name);
        int GetFirstSurveyId(int userId);
        NSurveyForm GetFormForExport(int surveyId);
        int GetIPExpiration(int surveyId);
        int GetPagesNumber(int surveyId);
        SurveyEntryQuotaData GetQuotaSettings(int surveyId);
        BranchingRuleData GetSurveyBranchingRules(int surveyId);
        SurveyData GetSurveyById(int surveyId, string languageCode);
        MessageConditionData GetSurveyMessageCondition(int messageConditionId);
        MessageConditionData GetSurveyMessageConditions(int surveyId);
        NotificationModeData GetSurveyNotificationModes();
        BranchingRuleData GetSurveyPageBranchingRules(int surveyId, int pageNumber);
        BranchingRuleData GetSurveyPageBranchingRulesDetails(int surveyId, int pageNumber);
        PageOptionData GetSurveyPageOptions(int surveyId, int pageNumber);
        string GetSurveyPassword(int surveyId);
        PipeData GetSurveyPipeDataFromQuestionId(int questionId);
        ProgressModeData GetSurveyProgressModes();
        ResumeModeData GetSurveyResumeModes();
        int GetSurveyUnAuthentifiedUserAction(int surveyId);
        SurveyData GetUnAssignedSurveysList(int userId);
        bool HasPageBranching(int surveyId, int pageNumber);
        void ImportSurveys(NSurveyForm importSurveys, int userId,int folderId);
        void IncreaseQuotaEntries(int surveyId);
        void IncrementResultsViews(int surveyId, int incrementNumber);
        void IncrementSurveyViews(int survyId, int incrementNumber);
        void InsertPageBreak(int surveyId, int displayOrder);
        bool IsSurveyEmailInviteOnly(int surveyId);
        bool IsSurveySaveTokenUserData(int surveyId);
        bool IsSurveyPasswordValid(int surveyId, string password);
        bool IsSurveyScored(int surveyId);
        void MovePageBreakDown(int surveyId, int pageNumber);
        void MovePageBreakUp(int surveyId, int pageNumber);
        bool NSurveyAllowsMultipleSubmissions(int surveyId);
        void ResetQuotaEntries(int surveyId);
        void ResetVotes(int surveyId);
        bool SurveyExists(string title);
        int TotalOfSurveys();
        void UnAssignUserFromSurvey(int surveyId, int userId);
        void UpdateAccessPassword(int surveyId, string accessPassword);
        void UpdateAspSecuritySettings(int surveyId, bool allowMultipleSubmissions);
        void UpdateCookieExpiration(int surveyId, int cookieExpires);
        void UpdateIPExpiration(int surveyId, int ipExpires);
        void UpdateMessageCondition(MessageConditionData updatedMessageCondition);
        void UpdateNSurveySecuritySettings(int surveyId, bool allowMultipleNSurveySubmissions);
        void UpdateOnlyInvited(int surveyId, bool onlyInvited);
        void UpdateSaveTokenUserData(int surveyId, bool saveData);
        void UpdateQuotaSettings(SurveyEntryQuotaData surveyQuota);
        void UpdateSurvey(SurveyData updatedSurvey, string languageCode);
        void UpdateSurveyPageOptions(PageOptionData updatedPageOptions);
        void UpdateUnAuthentifiedUserActions(int surveyId, int UnAuthentifiedUserActionId);

        void SetFolderId(int? folderId, int surveyId);
        SurveyData GetAllSurveysByTitle(string title, int? folderID,int userID);

        void SetFriendlyName(int surveyId, string friendlyName);
   
    }
}

