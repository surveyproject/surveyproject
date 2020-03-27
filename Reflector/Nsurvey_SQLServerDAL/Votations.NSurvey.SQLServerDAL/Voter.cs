namespace Votations.NSurvey.SQLServerDAL
{
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    //using Microsoft.ApplicationBlocks.Data;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;
    using System.Linq;

    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class Voter : IVoter
    {
        /// <summary>
        /// Add a new voter and his answers to the database 
        /// </summary>
        public void AddVoter(VoterAnswersData voterAnswers)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand insertCommand = new SqlCommand("vts_spVoterAddNew", connection, transaction);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int, 4, "SurveyID"));
            insertCommand.Parameters.Add(new SqlParameter("@IPSource", SqlDbType.NVarChar, 50, "IPSource"));
            insertCommand.Parameters.Add(new SqlParameter("@VoteDate", SqlDbType.DateTime, 8, "VoteDate"));
            insertCommand.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 8, "StartDate"));
            insertCommand.Parameters.Add(new SqlParameter("@Validated", SqlDbType.Bit, 1, "Validated"));
            insertCommand.Parameters.Add(new SqlParameter("@UId", SqlDbType.VarChar, 50, "UId"));
            insertCommand.Parameters.Add(new SqlParameter("@VoterID", SqlDbType.Int, 4, "VoterID"));
            insertCommand.Parameters.Add(new SqlParameter("@ResumeUId", SqlDbType.VarChar, 50, "ResumeUId"));
            insertCommand.Parameters.Add(new SqlParameter("@ProgressSaveDate", SqlDbType.DateTime, 8, "ProgressSaveDate"));
            insertCommand.Parameters.Add(new SqlParameter("@ResumeAtPageNumber", SqlDbType.Int, 4, "ResumeAtPageNumber"));
            insertCommand.Parameters.Add(new SqlParameter("@ResumeQuestionNumber", SqlDbType.Int, 4, "ResumeQuestionNumber"));
            insertCommand.Parameters.Add(new SqlParameter("@ResumeHighestPageNumber", SqlDbType.Int, 4, "ResumeHighestPageNumber"));
            insertCommand.Parameters.Add(new SqlParameter("@LanguageCode", SqlDbType.VarChar, 50, "LanguageCode"));
            insertCommand.Parameters["@VoterID"].Direction = ParameterDirection.Output;

            SqlCommand command2 = new SqlCommand("vts_spVoterAnswersAddNew", connection, transaction);
            command2.CommandType = CommandType.StoredProcedure;
            command2.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            command2.Parameters.Add(new SqlParameter("@AnswerText", SqlDbType.NVarChar, 4000, "AnswerText"));
            command2.Parameters.Add(new SqlParameter("@VoterID", SqlDbType.Int, 4, "VoterID"));
            command2.Parameters.Add(new SqlParameter("@SectionNumber", SqlDbType.Int, 4, "SectionNumber"));
            try
            {
                //SqlHelper.UpdateDataset(insertCommand, new SqlCommand(), insertCommand, voterAnswers, "Voters", false);
                //SqlHelper.UpdateDataset(command2, new SqlCommand(), command2, voterAnswers, "VotersAnswers", false);

                DbConnection.db.UpdateDataSet(voterAnswers, "Voters", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(voterAnswers, "VotersAnswers", command2, new SqlCommand(), command2, UpdateBehavior.Transactional);
                transaction.Commit();
                connection.Close();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
        }

        public void ImportVoter(int SurveyId,NSurveyVoter voterImport)
        {
          //QuestionIdText and AnsweridText must be supplied for every answer
 
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();

            for (int voterIndex = 0; voterIndex < voterImport.Voter.Count; voterIndex++)
            {
                var voter = voterImport.Voter[voterIndex];
                voter.SurveyID = SurveyId;
                //Create a new structure with voter and answers for the voter
                NSurveyVoter voterAnswers = new NSurveyVoter();
                voterAnswers.EnforceConstraints = false;

                voterAnswers.Voter.ImportRow(voter);
                voterAnswers.Answer.Columns.Add("SurveyId", typeof(int));
                voterAnswers.Answer.Columns.Add("QuestionText", typeof(string));
                var answers = voterImport.Answer.Where(x => x.VoterId == voter.VoterID).ToList();
                for (int answerIndex = 0; answerIndex < answers.Count(); answerIndex++)
                {
                    var currAnswer = answers[answerIndex];
                    voterAnswers.Answer.ImportRow(currAnswer);
                }

                foreach (var dr in voterAnswers.Answer)
                {
                   dr["QuestionText"] = voterImport.Question.Single(x => x.QuestionId == dr.QuestionId &&
                                                        x.VoterID == dr.VoterId).QuestionText;
                    dr["SurveyId"] = SurveyId;
                }

                SqlCommand insertCommand = new SqlCommand("vts_spVoterImport", connection, transaction);
                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int, 4, "SurveyID"));
                insertCommand.Parameters.Add(new SqlParameter("@IPSource", SqlDbType.NVarChar, 50, "IPSource"));
                insertCommand.Parameters.Add(new SqlParameter("@VoteDate", SqlDbType.DateTime, 8, "VoteDate"));
                insertCommand.Parameters.Add(new SqlParameter("@VoterID", SqlDbType.Int, 4, "VoterID"));
                insertCommand.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 8, "StartDate"));
                insertCommand.Parameters.Add(new SqlParameter("@LanguageCode", SqlDbType.NVarChar, 50, "LanguageCode"));

                insertCommand.Parameters["@VoterID"].Direction = ParameterDirection.Output;

                SqlCommand command2 = new SqlCommand("vts_spVoterAnswersImport", connection, transaction);               
                command2.CommandType = CommandType.StoredProcedure;
                command2.Parameters.Add(new SqlParameter("@Answer", SqlDbType.NVarChar, -1, "Answer"));
                command2.Parameters.Add(new SqlParameter("@VoterAnswer", SqlDbType.NVarChar, -1, "VoterAnswer"));
                command2.Parameters.Add(new SqlParameter("@VoterID", SqlDbType.Int, 4, "VoterID"));
                command2.Parameters.Add(new SqlParameter("@QuestionDisplayOrder", SqlDbType.Int, 4, "QuestionDisplayOrder"));
                command2.Parameters.Add(new SqlParameter("@AnswerDisplayOrder", SqlDbType.Int, 4, "AnswerDisplayOrder"));
                command2.Parameters.Add(new SqlParameter("@SectionNumber", SqlDbType.Int, 4, "SectionNumber"));
                command2.Parameters.Add(new SqlParameter("@SurveyId", SqlDbType.Int, 4, "SurveyId"));
                command2.Parameters.Add(new SqlParameter("@QuestionText", SqlDbType.NVarChar, -1, "QuestionText"));

                try
                {
                    DbConnection.db.UpdateDataSet(voterAnswers, "Voter", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);

                    int voterId = voterAnswers.Voter[0].VoterID;
                    foreach (var dr in voterAnswers.Answer) dr.VoterId = voterId;
                    
                    DbConnection.db.UpdateDataSet(voterAnswers, "Answer", command2, new SqlCommand(), command2, UpdateBehavior.Transactional);

                }
                catch (SqlException exception)
                {
                    //transaction.Rollback();
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }

                        //connection.Close();
                        throw exception;
                }
            }
            transaction.Commit();
            connection.Close();
        }

        /// <summary>
        /// Queue an invitation request for a future voter
        /// </summary>
        public void AddVoterInvitation(int surveyId, string email, bool anonymousEntry, string UId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@Email", email), 
            //    new SqlParameter("@AnonymousEntry", anonymousEntry), 
            //    new SqlParameter("@Uid", UId) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@Email", email).SqlValue);
                commandParameters.Add(new SqlParameter("@AnonymousEntry", anonymousEntry).SqlValue);
                commandParameters.Add(new SqlParameter("@Uid", UId).SqlValue);
            }                       
            
            DbConnection.db.ExecuteNonQuery("vts_spVoterInvitationQueueAddNew", commandParameters.ToArray());
        }

        /// <summary>
        /// 
        /// Checks if the given IP has already been registered
        /// in the expiration time lapse
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="voterIP"></param>
        /// <param name="IPExpires"></param>
        /// <returns></returns>
        public bool CheckIfVoterIPExists(int surveyId, string voterIP)
        {
            //SqlParameter[] commandParameters = new SqlParameter[3];
            //commandParameters[0] = new SqlParameter("@SurveyID", surveyId);
            //commandParameters[1] = new SqlParameter("@IP", voterIP);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@IP", voterIP).SqlValue);
            }

            if (DbConnection.db.ExecuteScalar("vts_spVoterCheckIfIPExists", commandParameters.ToArray()) == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the given UID has already been registered
        /// </summary>
        /// <returns></returns>
        public bool CheckIfVoterUIdExists(int surveyId, string uId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[3];
            //commandParameters[0] = new SqlParameter("@SurveyID", surveyId);
            //commandParameters[1] = new SqlParameter("@UID", uId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@UID", uId).SqlValue);
            }

            if (DbConnection.db.ExecuteScalar("vts_spVoterCheckIfUIDExists", commandParameters.ToArray()) == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes the log of the invitation
        /// </summary>
        /// <param name="invitationLogId"></param>
        public void DeleteInvitationLog(int invitationLogId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@InvitationLogId", invitationLogId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery( "vts_spInvitationLogDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes all unvalidated entries
        /// </summary>
        public void DeleteUnvalidatedVoters(int surveyId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spVoterDeleteUnvalidated", commandParameters.ToArray());
        }

        /// <summary>
        /// Delete all answer a voter gave for the survey
        /// </summary>
        public void DeleteVoterAnswers(int voterId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@voterId", voterId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery( "vts_spVoterDeleteAnswers", commandParameters.ToArray());
        }

        /// <summary>
        /// Delete a voter and all its answers
        /// </summary>
        /// <param name="voterId"></param>
        /// <returns></returns>
        public void DeleteVoterById(int voterId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@voterId", voterId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery( "vts_spVoterDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes the email from the survey invitation queue
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="emailId"></param>
        public void DeleteVoterInvitation(int surveyId, int emailId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@EmailID", emailId) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@EmailID", emailId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery( "vts_spVoterInvitationQueueDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes the email from the survey invitation queue
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="emailId"></param>
        public void DeleteVoterInvitation(int surveyId, string email)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SurveyID", surveyId), new SqlParameter("@Email", email) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@Email", email).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery( "vts_spVoterInvitationQueueDeleteByEmail", commandParameters.ToArray());
        }

        /// <summary>
        /// Delete all answer a voter gave for the page
        /// </summary>
        public void DeleteVoterPageAnswers(int surveyId, int voterId, int pageNumber)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyId", surveyId), 
            //    new SqlParameter("@VoterId", voterId), 
            //    new SqlParameter("@PageNumber", pageNumber) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@VoterId", voterId).SqlValue);
                commandParameters.Add(new SqlParameter("@PageNumber", pageNumber).SqlValue);
            }


            DbConnection.db.ExecuteNonQuery( "vts_spVoterDeletePageAnswers", commandParameters.ToArray());
        }

        /// <summary>
        /// Delete all answer a voter gave for the question
        /// </summary>
        public void DeleteVoterQuestionAnswers(int voterId, int questionId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[3];
            //commandParameters[0] = new SqlParameter("@VoterId", voterId);
            //commandParameters[1] = new SqlParameter("@QuestionId", questionId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@VoterId", voterId).SqlValue);
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery( "vts_spVoterDeleteQuestionAnswers", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes a voter resume session
        /// from the database
        /// </summary>
        public void DeleteVoterResumeSession(int surveyId, string resumeUId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[3];
            //commandParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //commandParameters[1] = new SqlParameter("@ResumeUID", resumeUId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@ResumeUID", resumeUId).SqlValue);
            }
        
            DbConnection.db.ExecuteNonQuery( "vts_spVoterDeleteResumeSession", commandParameters.ToArray());
        }

        /// <summary>
        /// Delete all voter and all its answers for the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public void DeleteVoters(int surveyId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }
            
            //DbConnection.db.ExecuteNonQuery( "vts_spVoterDeleteAll", commandParameters.ToArray()) ;

            SqlDatabase db = new SqlDatabase(DbConnection.NewDbConnectionString);
            System.Data.Common.DbCommand cmd = db.GetStoredProcCommand("vts_spVoterDeleteAll", commandParameters.ToArray());
            cmd.CommandTimeout = 120; //in sec. - default: 30
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Get number of voters for the given date
        /// </summary>
        public int GetDayStats(int surveyId, DateTime statDay)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyId", surveyId), 
            //    new SqlParameter("@StatDay", statDay) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@StatDay", statDay).SqlValue);
            }
                        
            object obj2 = DbConnection.db.ExecuteScalar("vts_spVoterGetDailyStat", commandParameters.ToArray());
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        /// <summary>
        /// returns the guid from the queue for the given email and survey id
        /// </summary>
        public string GetEmailUId(int surveyId, string email)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SurveyID", surveyId), new SqlParameter("@Email", email) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@Email", email).SqlValue);
            }
             
            object obj2 = DbConnection.db.ExecuteScalar("vts_spVoterInvitationQueueGetUId", commandParameters.ToArray());
            if (obj2 == null)
            {
                return null;
            }
            return obj2.ToString();
        }

        /// <summary>
        /// Returns all data needed to create a CSV
        /// </summary>
        public CSVExportData GetForCSVExport(int surveyId, DateTime startDate, DateTime endDate)
        {
            CSVExportData dataSet = new CSVExportData();

            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@StartDate", startDate), 
            //    new SqlParameter("@EndDate", endDate) };
            
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@StartDate", startDate).SqlValue);
                commandParameters.Add(new SqlParameter("@EndDate", endDate).SqlValue);
            }
            
            DbConnection.db.LoadDataSet("vts_spVoterExportCSVData ", dataSet, new string[] { "ExportAnswers", "Voters", "VoterAnswers" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all the answers of the voters
        /// </summary>
        public NSurveyVoter GetForExport(int surveyId, DateTime startDate, DateTime endDate)
        {
            NSurveyVoter dataSet = new NSurveyVoter();

            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@StartDate", startDate), 
            //    new SqlParameter("@EndDate", endDate) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@StartDate", startDate).SqlValue);
                commandParameters.Add(new SqlParameter("@EndDate", endDate).SqlValue);
            }
            
            DbConnection.db.LoadDataSet("vts_spVoterForExport", dataSet, new string[] { "Voter", "Question", "Answer" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all the invitation logs
        /// </summary>
        public InvitationLogData GetInvitationLogs(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            InvitationLogData dataSet = new InvitationLogData();

            //ArrayList commandParameters = new ArrayList();
            //{
            //    commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            //    commandParameters.Add(new SqlParameter("@CurrentPage", pageNumber).SqlValue);
            //    commandParameters.Add(new SqlParameter("@PageSize", pageSize).SqlValue);
            //    commandParameters.Add(new SqlParameter("@TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output }.SqlValue);
            //}

            //DbConnection.db.LoadDataSet("vts_spInvitationLogGetAll", dataSet, new string[] { "InvitationLogs" }, commandParameters.ToArray());
            //totalRecords = dataSet.InvitationLogs.Rows.Count;

            DbCommand dbCommand = DbConnection.db.GetStoredProcCommand("vts_spInvitationLogGetAll");
            DbConnection.db.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 0);

            DbConnection.db.AddInParameter(dbCommand, "@SurveyID", DbType.Int32, surveyId);
            DbConnection.db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int32, pageNumber);
            DbConnection.db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
            DbConnection.db.LoadDataSet(dbCommand, dataSet, "InvitationLogs");

            totalRecords = (int)DbConnection.db.GetParameterValue(dbCommand, "@TotalRecords");

            return dataSet;
        }

        /// <summary>
        /// Get voter's statistics for the given month and year
        /// </summary>
        public VoterStatisticsData GetMonthlyStats(int surveyId, int month, int year)
        {
            VoterStatisticsData dataSet = new VoterStatisticsData();

            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@Month", month), 
            //    new SqlParameter("@Year", year) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@Month", month).SqlValue);
                commandParameters.Add(new SqlParameter("@Year", year).SqlValue);
            }
            
            DbConnection.db.LoadDataSet("vts_spvoterGetMonthlyStats", dataSet, new string[] { "VoterStats" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// returns the number of unvalidated entries (saved progress)
        /// </summary>
        public int GetUnvalidatedVotersCount(int surveyId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spVoterGetUnvalidatedCount", commandParameters.ToArray());
            if (obj2 == null)
            {
                return 0;
            }
            return int.Parse(obj2.ToString());
        }

        /// <summary>
        /// Get voter's answers and voter details
        /// </summary>
        /// <param name="voterId"></param>
        /// <returns></returns>
        public VoterAnswersData GetVoterAnswers(int voterId)
        {
            VoterAnswersData dataSet = new VoterAnswersData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@VoterID", voterId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spVoterGetAnswers", dataSet, new string[] { "Voters", "VotersAnswers" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all the entries text / selections of the voters
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetVotersCompleteEntries(int surveyId, int pageNumber, int pageSize, DateTime startDate, DateTime endDate)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@CurrentPage", pageNumber), 
            //    new SqlParameter("@PageSize", pageSize), 
            //    new SqlParameter("@StartDate", startDate), 
            //    new SqlParameter("@EndDate", endDate) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@CurrentPage", pageNumber).SqlValue);
                commandParameters.Add(new SqlParameter("@PageSize", pageSize).SqlValue);
                commandParameters.Add(new SqlParameter("@StartDate", startDate).SqlValue);
                commandParameters.Add(new SqlParameter("@EndDate", endDate).SqlValue);
            }

            return DbConnection.db.ExecuteDataSet("vts_spVoterGetFullPivot", commandParameters.ToArray());
        }

        /// <summary>
        /// Returns all voters who have answered an invitation 
        /// </summary>
        public VoterData GetVotersInvitationAnswered(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            VoterData dataSet = new VoterData();
            DbCommand dbCommand = DbConnection.db.GetStoredProcCommand("vts_spVoterInvitationAnsweredGetAll");
            DbConnection.db.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 0);
            DbConnection.db.AddInParameter(dbCommand, "@SurveyId", DbType.Int32, surveyId);
            DbConnection.db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int32, pageNumber);
            DbConnection.db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
            DbConnection.db.LoadDataSet(dbCommand, dataSet, "Voters");
            totalRecords = (int)DbConnection.db.GetParameterValue(dbCommand, "@TotalRecords");
            return dataSet;
            

            
        }

        /// <summary>
        /// Returns the invitation queue of the survey
        /// </summary>
        public InvitationQueueData GetVotersInvitationQueue(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            InvitationQueueData dataSet = new InvitationQueueData();

            
            DbCommand dbCommand = DbConnection.db.GetStoredProcCommand("vts_spVoterInvitationQueueGetAll");
            DbConnection.db.AddOutParameter(dbCommand, "@TotalRecords", DbType.Int32, 0);
            DbConnection.db.AddInParameter(dbCommand, "@SurveyId", DbType.Int32, surveyId);
            DbConnection.db.AddInParameter(dbCommand, "@CurrentPage", DbType.Int32, pageNumber);
            DbConnection.db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, pageSize);
            DbConnection.db.LoadDataSet(dbCommand, dataSet, "InvitationQueues");
            totalRecords = (int)DbConnection.db.GetParameterValue(dbCommand, "@TotalRecords"); 
            return dataSet;
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
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@CurrentPage", pageNumber), 
            //    new SqlParameter("@PageSize", pageSize), 
            //    new SqlParameter("@StartDate", startDate), 
            //    new SqlParameter("@EndDate", endDate) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@CurrentPage", pageNumber).SqlValue);
                commandParameters.Add(new SqlParameter("@PageSize", pageSize).SqlValue);
                commandParameters.Add(new SqlParameter("@StartDate", startDate).SqlValue);
                commandParameters.Add(new SqlParameter("@EndDate", endDate).SqlValue);
            }

            return DbConnection.db.ExecuteDataSet("vts_spVoterGetPivotTextEntries", commandParameters.ToArray());
        }


        /// <summary>
        /// Returns all the text entries of SP User as a voter
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="userId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetVotersTextIndivEntries(int surveyId, int userId, int pageNumber, int pageSize, DateTime startDate, DateTime endDate)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@CurrentPage", pageNumber), 
            //    new SqlParameter("@PageSize", pageSize), 
            //    new SqlParameter("@StartDate", startDate), 
            //    new SqlParameter("@EndDate", endDate) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
                commandParameters.Add(new SqlParameter("@CurrentPage", pageNumber).SqlValue);
                commandParameters.Add(new SqlParameter("@PageSize", pageSize).SqlValue);
                commandParameters.Add(new SqlParameter("@StartDate", startDate).SqlValue);
                commandParameters.Add(new SqlParameter("@EndDate", endDate).SqlValue);
            }

            return DbConnection.db.ExecuteDataSet("vts_spVoterGetPivotTextIndivEntries", commandParameters.ToArray());
        }


        /// <summary>
        /// Return all Voter data to show on Ssrs Report Test
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllVotersSsrsTest()
        {
            return DbConnection.db.ExecuteDataSet("vts_spReportSsrsVoterGetAll");
        }
        
        /// <summary>
        /// Check if the username has already taken the survey
        /// </summary>
        public bool HasUserNameVoted(int surveyId, string userName)
        {
            //SqlParameter[] commandParameters = new SqlParameter[3];
            //commandParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //commandParameters[1] = new SqlParameter("@UserName", userName);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@UserName", userName).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spVoterGetByUserName", commandParameters.ToArray());
            return ((obj2 != null) && (obj2 != DBNull.Value));
        }

        /// <summary>
        /// Check if the given Uid is valid
        /// </summary>
        /// <param name="UId"></param>
        /// <returns>returns the Survey id of the Uid if its valid else returns -1</returns>
        public int IsUIdValid(string UId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UId", UId).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spInvitationUidIsValid", commandParameters.ToArray());
            if (obj2 == null)
            {
                return -1;
            }
            return int.Parse(obj2.ToString());
        }

        /// <summary>
        /// logs the exception that occured for the invitation in the db
        /// </summary>
        public void LogInvitationError(InvitationLogData invitationLog)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spInvitationLogAddNew", connection);

            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int, 4, "SurveyID"));
            insertCommand.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 0x9b, "Email"));
            insertCommand.Parameters.Add(new SqlParameter("@ExceptionMessage", SqlDbType.NVarChar, 0x400, "ExceptionMessage"));
            insertCommand.Parameters.Add(new SqlParameter("@ExceptionType", SqlDbType.NVarChar, 0xff, "ExceptionType"));
            insertCommand.Parameters.Add(new SqlParameter("@ErrorDate", SqlDbType.DateTime, 8, "ErrorDate"));
            insertCommand.Parameters.Add(new SqlParameter("@InvitationLogId", SqlDbType.Int, 4, "InvitationLogId"));
            insertCommand.Parameters["@InvitationLogId"].Direction = ParameterDirection.Output;
            insertCommand.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.Int, 4, "EmailId"));
            insertCommand.Parameters["@EmailId"].Direction = ParameterDirection.Output;

            DbConnection.db.UpdateDataSet(invitationLog, "InvitationLogs", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Get the voter's data and answers to resume a session
        /// </summary>
        public VoterAnswersData ResumeVoterAnswers(int surveyId, string resumeUid)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@ResumeUid", resumeUid) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@ResumeUid", resumeUid).SqlValue);
            }

            VoterAnswersData dataSet = new VoterAnswersData();
            
            //SqlHelper.FillDatasetWithoutChangesAccept(DbConnection.NewDbConnectionString, CommandType.StoredProcedure, "vts_spVoterResumeSession", dataSet, new string[] { "Voters", "VotersAnswers" }, commandParameters);
            // Note: FillDatasetWithoutChangesAccept: similar to FillDataset method except for the extra line:
            //adapter.AcceptChangesDuringFill = false;

            //AcceptChanges() : Commits all the changes made to this DataSet since it was loaded or since the last time AcceptChanges was called.
            //AcceptChangesDuringFill: Gets or sets a value indicating whether AcceptChanges is called on a DataRow after it is added to the DataTable during any of the Fill operations.
            //If false, AcceptChanges is not called, and the newly added rows are treated as inserted rows: rowstate = added instead of unchanged.

           DbConnection.db.LoadDataSetWithoutAcceptChanges("vts_spVoterResumeSession", dataSet, new string[] { "Voters", "VotersAnswers" }, commandParameters.ToArray());

               return dataSet;

        }

        /// <summary>
        /// Set the give uid to the voter
        /// </summary>
        public void SetVoterUId(int voterId, string uId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[3];
            //commandParameters[0] = new SqlParameter("@VoterId", voterId);
            //commandParameters[1] = new SqlParameter("@UID", uId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@VoterId", voterId).SqlValue);
                commandParameters.Add(new SqlParameter("@UID", uId).SqlValue);
            }
           
            DbConnection.db.ExecuteNonQuery( "vts_spVoterUIdAddNew", commandParameters.ToArray());
        }

        /// <summary>
        /// Updates voter's answer
        /// </summary>
        /// <param name="voterAnswers">Voter and all his answers information</param>
        public void UpdateVoter(VoterAnswersData updatedVoterAnswers)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlCommand insertCommand = new SqlCommand("vts_spVoterAnswersAddNew", connection, transaction);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerText", SqlDbType.NVarChar, 4000, "AnswerText"));
            insertCommand.Parameters.Add(new SqlParameter("@VoterID", SqlDbType.Int, 4, "VoterID"));
            insertCommand.Parameters.Add(new SqlParameter("@SectionNumber", SqlDbType.Int, 4, "SectionNumber"));
            try
            {
                ArrayList commandParameters = new ArrayList();
                {
                    commandParameters.Add(new SqlParameter("@voterId", updatedVoterAnswers.Voters[0].VoterId).SqlValue);
                }

                DbConnection.db.ExecuteNonQuery("vts_spVoterDeleteAnswers", commandParameters.ToArray());

                DbConnection.db.UpdateDataSet(updatedVoterAnswers, "VotersAnswers", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
                transaction.Commit();
                connection.Close();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
        }

        /// <summary>
        /// Update the asp.net username of the voter
        /// </summary>
        public void UpdateVoterUserName(int voterId, string userName)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@VoterId", voterId), 
            //    new SqlParameter("@UserName", userName) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@VoterId", voterId).SqlValue);
                commandParameters.Add(new SqlParameter("@UserName", userName).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery( "vts_spVoterUpdateUserName", commandParameters.ToArray());
        }
    }
}

