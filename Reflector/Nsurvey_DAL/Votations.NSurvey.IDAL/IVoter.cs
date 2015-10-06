namespace Votations.NSurvey.IDAL
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Voter interface for the voter DAL.
    /// </summary>
    public interface IVoter
    {
        void AddVoter(VoterAnswersData voterAnswers);
        void AddVoterInvitation(int surveyId, string email, bool anonymousEntry, string UId);
        bool CheckIfVoterIPExists(int surveyId, string voterIP);
        bool CheckIfVoterUIdExists(int surveyId, string uId);
        void DeleteInvitationLog(int invitationLogId);
        void DeleteUnvalidatedVoters(int surveyId);
        void DeleteVoterAnswers(int voterId);
        void DeleteVoterById(int id);
        void DeleteVoterInvitation(int surveyId, int emailId);
        void DeleteVoterInvitation(int surveyId, string email);
        void DeleteVoterPageAnswers(int surveyId, int voterId, int pageNumber);
        void DeleteVoterQuestionAnswers(int voterId, int questionId);
        void DeleteVoterResumeSession(int surveyId, string resumeUId);
        void DeleteVoters(int surveyId);
        int GetDayStats(int surveyId, DateTime statDay);
        string GetEmailUId(int surveyId, string email);
        CSVExportData GetForCSVExport(int surveyId, DateTime startDate, DateTime endDate);
        NSurveyVoter GetForExport(int surveyId, DateTime startDate, DateTime endDate);
        InvitationLogData GetInvitationLogs(int surveyId, int pageNumber, int pageSize, out int totalRecords);
        VoterStatisticsData GetMonthlyStats(int surveyId, int month, int year);
        int GetUnvalidatedVotersCount(int surveyId);
        VoterAnswersData GetVoterAnswers(int voterId);
        DataSet GetVotersCompleteEntries(int surveyId, int pageNumber, int pageSize, DateTime startDate, DateTime endDate);
        VoterData GetVotersInvitationAnswered(int surveyId, int pageNumber, int pageSize, out int totalRecords);
        InvitationQueueData GetVotersInvitationQueue(int surveyId, int pageNumber, int pageSize, out int totalRecords);
        DataSet GetVotersTextEntries(int surveyId, int pageNumber, int pageSize, DateTime startDate, DateTime endDate);
        bool HasUserNameVoted(int surveyId, string userName);
        int IsUIdValid(string UId);
        void LogInvitationError(InvitationLogData invitationLog);
        VoterAnswersData ResumeVoterAnswers(int surveyId, string resumeUid);
        void SetVoterUId(int voterId, string uId);
        void UpdateVoter(VoterAnswersData voterAnswers);
        void UpdateVoterUserName(int voterId, string userName);
        void ImportVoter(int SurveyId,NSurveyVoter importVoter);
    }
}

