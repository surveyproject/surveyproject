namespace Votations.NSurvey.DataAccess
{
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using Votations.NSurvey;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provides the method to access the voter's data.
    /// </summary>
    public class Voters
    {
        /// <summary>
        /// Checks if the given IP has already been registered
        /// in the expiration time lapse
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="voterIP"></param>
        /// <param name="IPExpires"></param>
        /// <returns></returns>
        public bool CheckIfVoterIPExists(int surveyId, string voterIP)
        {
            return VoterFactory.Create().CheckIfVoterIPExists(surveyId, voterIP);
        }

        /// <summary>
        /// Checks if the given UID has already been registered
        /// </summary>
        /// <returns></returns>
        public bool CheckIfVoterUIdExists(int surveyId, string uId)
        {
            return VoterFactory.Create().CheckIfVoterUIdExists(surveyId, uId);
        }

        /// <summary>
        /// Returns a CSV formatted string with all voters answers
        /// </summary>
        public CSVExportData GetCSVExport(int surveyId, DateTime startDate, DateTime endDate)
        {
            return VoterFactory.Create().GetForCSVExport(surveyId, startDate, endDate);
        }

        /// <summary>
        /// Get number of voters for the given date
        /// </summary>
        public int GetDayStats(int surveyId, DateTime statDay)
        {
            return VoterFactory.Create().GetDayStats(surveyId, statDay);
        }

        /// <summary>
        /// returns the guid from the queue for the given email and survey id
        /// </summary>
        public string GetEmailUId(int surveyId, string email)
        {
            return VoterFactory.Create().GetEmailUId(surveyId, email);
        }

        /// <summary>
        /// Returns all the answers of the voters
        /// </summary>
        public NSurveyVoter GetForExport(int surveyId, DateTime startDate, DateTime endDate)
        {
            NSurveyVoter voter2 = VoterFactory.Create().GetForExport(surveyId, startDate, endDate);
            foreach (NSurveyVoter.QuestionRow row in voter2.Question)
            {
                row.QuestionText = Regex.Replace(row.QuestionText, "<[^>]*>", " ");
            }
            return voter2;
        }

        /// <summary>
        /// Returns all the logged message during a mailing
        /// </summary>
        public InvitationLogData GetInvitationLogs(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            return VoterFactory.Create().GetInvitationLogs(surveyId, pageNumber, pageSize, out totalRecords);
        }

        /// <summary>
        /// Get voter's statistics for the given month and year
        /// </summary>
        public VoterStatisticsData GetMonthlyStats(int surveyId, int month, int year)
        {
            return VoterFactory.Create().GetMonthlyStats(surveyId, month, year);
        }

        /// <summary>
        /// returns the number of unvalidated entries (saved progress)
        /// </summary>
        public int GetUnvalidatedVotersCount(int surveyId)
        {
            return VoterFactory.Create().GetUnvalidatedVotersCount(surveyId);
        }

        /// <summary>
        /// Get voter's answers and voter details
        /// </summary>
        /// <param name="voterId"></param>
        /// <returns>voterAnswers</returns>
        public VoterAnswersData GetVoterAnswers(int voterId)
        {
            VoterAnswersData voterAnswers = VoterFactory.Create().GetVoterAnswers(voterId);
            if (voterAnswers.Voters.Rows.Count == 0)
            {
                throw new VoterNotFoundException();
            }
            return voterAnswers;
        }

        /// <summary>
        /// Returns all the entries (text / selections) of the voters
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetVotersCompleteEntries(int surveyId, int pageNumber, int pageSize, DateTime startDate, DateTime endDate)
        {
            return VoterFactory.Create().GetVotersCompleteEntries(surveyId, pageNumber, pageSize, startDate, endDate);
        }

        /// <summary>
        /// Returns all voters who have answered an invitation 
        /// </summary>
        public VoterData GetVotersInvitationAnswered(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            return VoterFactory.Create().GetVotersInvitationAnswered(surveyId, pageNumber, pageSize, out totalRecords);
        }

        /// <summary>
        /// Returns the invitation queue of the survey
        /// </summary>
        public InvitationQueueData GetVotersInvitationQueue(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            return VoterFactory.Create().GetVotersInvitationQueue(surveyId, pageNumber, pageSize, out totalRecords);
        }

        /// <summary>
        /// Returns all the text entries of the voters
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetVotersTextEntries(int surveyId, int pageNumber, int pageSize, DateTime startDate, DateTime endDate)
        {
            return VoterFactory.Create().GetVotersTextEntries(surveyId, pageNumber, pageSize, startDate, endDate);
        }

        /// <summary>
        /// Check if the username has already taken the survey
        /// </summary>
        public bool HasUserNameVoted(int surveyId, string userName)
        {
            return VoterFactory.Create().HasUserNameVoted(surveyId, userName);
        }

        /// <summary>
        /// Get the voter's data and answers to resume a session
        /// </summary>
        public VoterAnswersData ResumeVoterAnswers(int surveyId, string resumeUid)
        {
            VoterAnswersData data = VoterFactory.Create().ResumeVoterAnswers(surveyId, resumeUid);
            if (data.Voters.Rows.Count == 0)
            {
                return null;
            }
            return data;
        }
    }
}

