namespace Votations.NSurvey.BusinessRules
{
    using System;
    using System.Security.Cryptography;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Contains the business rules that are used for a Voter
    /// </summary>
    public class Voter
    {
        /// <summary>
        /// Adds a new voter
        /// </summary>
        /// <param name="voterAnswers">Voter and all his answers information</param>
        public void AddVoter(VoterAnswersData voterAnswers)
        {
            VoterFactory.Create().AddVoter(voterAnswers);
        }

        /// <summary>
        /// Deletes the log of the invitation
        /// </summary>
        /// <param name="invitationLogId"></param>
        public void DeleteInvitationLog(int invitationLogId)
        {
            VoterFactory.Create().DeleteInvitationLog(invitationLogId);
        }

        /// <summary>
        /// Deletes all unvalidated entries
        /// </summary>
        public void DeleteUnvalidatedVoters(int surveyId)
        {
            VoterFactory.Create().DeleteUnvalidatedVoters(surveyId);
        }

        /// <summary>
        /// Delete all answer a voter gave for the survey
        /// </summary>
        public void DeleteVoterAnswers(int voterId)
        {
            VoterFactory.Create().DeleteVoterAnswers(voterId);
        }

        /// <summary>
        /// Delete a voter and all its answers
        /// </summary>
        /// <param name="voterId"></param>
        /// <returns></returns>
        public void DeleteVoterById(int voterId)
        {
            VoterFactory.Create().DeleteVoterById(voterId);
        }

        /// <summary>
        /// Deletes the email from the survey invitation queue
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="emailId"></param>
        public void DeleteVoterInvitation(int surveyId, int emailId)
        {
            VoterFactory.Create().DeleteVoterInvitation(surveyId, emailId);
        }

        /// <summary>
        /// Deletes the email from the survey invitation queue
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="emailId"></param>
        public void DeleteVoterInvitation(int surveyId, string email)
        {
            VoterFactory.Create().DeleteVoterInvitation(surveyId, email);
        }

        /// <summary>
        /// Delete all answer a voter gave for the page
        /// </summary>
        public void DeleteVoterPageAnswers(int surveyId, int voterId, int pageNumber)
        {
            VoterFactory.Create().DeleteVoterPageAnswers(surveyId, voterId, pageNumber);
        }

        /// <summary>
        /// Delete all answer a voter gave for the question
        /// </summary>
        public void DeleteVoterQuestionAnswers(int voterId, int questionId)
        {
            VoterFactory.Create().DeleteVoterQuestionAnswers(voterId, questionId);
        }

        /// <summary>
        /// Deletes a voter resume session
        /// from the database
        /// </summary>
        public void DeleteVoterResumeSession(int surveyId, string resumeUId)
        {
            VoterFactory.Create().DeleteVoterResumeSession(surveyId, resumeUId);
        }

        /// <summary>
        /// Delete all voter and all its answers for the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public void DeleteVoters(int surveyId)
        {
            VoterFactory.Create().DeleteVoters(surveyId);
        }

        /// <summary>
        /// returns a unique identifier
        /// </summary>
        public string GenerateResumeUId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// returns a unique identifier of a given length
        /// </summary>
        public string GenerateResumeUId(int uIdLength)
        {
            string str = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            byte[] data = new byte[uIdLength];
            char[] chArray = new char[uIdLength];
            int length = str.Length;
            new RNGCryptoServiceProvider().GetBytes(data);
            for (int i = 0; i < uIdLength; i++)
            {
                chArray[i] = str[data[i] % length];
            }
            return new string(chArray);
        }

        /// <summary>
        /// returns a unique identifier
        /// </summary>
        public string GenerateUId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Queue an invitation request for a future voter and 
        /// returns the generate UID
        /// </summary>
        public string GenerateVoterInvitationUId(int surveyId, string email, bool anonymousEntry)
        {
            string emailUId = new Voters().GetEmailUId(surveyId, email);
            if (emailUId == null)
            {
                emailUId = this.GenerateUId();
                VoterFactory.Create().AddVoterInvitation(surveyId, email, anonymousEntry, emailUId);
            }
            return emailUId;
        }

        /// <summary>
        /// Check if the given Uid is valid
        /// </summary>
        /// <param name="UId"></param>
        /// <returns>returns the Survey id of the Uid if its valid else returns -1</returns>
        public int IsUIdValid(string UId)
        {
            return VoterFactory.Create().IsUIdValid(UId);
        }

        /// <summary>
        /// logs the exception that occured for the invitation in the db
        /// </summary>
        public void LogInvitationError(InvitationLogData invitationLog)
        {
            VoterFactory.Create().LogInvitationError(invitationLog);
        }

        /// <summary>
        /// Saves the current voter progress and his answers to the database 
        /// </summary>
        public void SaveVoterProgress(VoterAnswersData voterAnswers)
        {
            IVoter voter = VoterFactory.Create();
            VoterAnswersData data = new VoterAnswersData();
            data.Merge(voterAnswers, false);
            voter.AddVoter(data);
        }

        /// <summary>
        /// Set the give uid to the voter
        /// </summary>
        public void SetVoterUId(int voterId, string uId)
        {
            VoterFactory.Create().SetVoterUId(voterId, uId);
        }

        /// <summary>
        /// Updates voter's answer
        /// </summary>
        /// <param name="voterAnswers">Voter and all his answers information</param>
        public void UpdateVoter(VoterAnswersData updatedVoterAnswers)
        {
            VoterFactory.Create().UpdateVoter(updatedVoterAnswers);
        }

        /// <summary>
        /// Update the asp.net username of the voter
        /// </summary>
        public void UpdateVoterUserName(int voterId, string userName)
        {
            VoterFactory.Create().UpdateVoterUserName(voterId, userName);
        }
        public void ImportVoter(int surveyId,NSurveyVoter importVoter)
        {
            VoterFactory.Create().ImportVoter(surveyId,importVoter);
        }
    }
}

