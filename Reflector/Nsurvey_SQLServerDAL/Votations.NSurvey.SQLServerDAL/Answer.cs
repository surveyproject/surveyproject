namespace Votations.NSurvey.SQLServerDAL
{
    //using Microsoft.ApplicationBlocks.Data;

    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class Answer : IAnswer
    {
        /// <summary>
        /// Adds a new answer to the question specified by the question id property in the database
        /// </summary>
        /// <param name="newAnswer">Answer object with information about what to add. Only Id must be ommited</param>
        public void AddAnswer(AnswerData newAnswer)
        {
            SqlConnection sqlConnection = new SqlConnection(DbConnection.NewDbConnectionString);
           // DbConnection.db.UpdateDataSet(this.GetInsertAnswerCommand(sqlConnection, null), new SqlCommand(), new SqlCommand(), newAnswer, "Answers", true);
            DbConnection.db.UpdateDataSet(newAnswer, "Answers", this.GetInsertAnswerCommand(sqlConnection, null), new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Adds a new answer to the matrix question specified by the question id property in the database
        /// </summary>
        /// <param name="newAnswer">Answer object with information about what to add. Only Id must be ommited</param>
        public void AddMatrixAnswer(AnswerData newAnswer)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spAnswerMatrixAddNew", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@AnswerText", SqlDbType.VarChar, 0xfa0, "AnswerText"));
            insertCommand.Parameters.Add(new SqlParameter("@ImageURL", SqlDbType.VarChar, 0x3e8, "ImageURL"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerTypeID", SqlDbType.Int, 4, "AnswerTypeID"));
            insertCommand.Parameters.Add(new SqlParameter("@ParentQuestionID", SqlDbType.Int, 4, "QuestionID"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            insertCommand.Parameters["@AnswerID"].Direction = ParameterDirection.Output;
            DbConnection.db.UpdateDataSet(newAnswer, "Answers", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Remove the answer from the database
        /// </summary>
        /// <param name="answerId">Answer to delete from the database</param>
        /// <returns>true if successfull</returns>
        public void DeleteAnswer(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerID", answerId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerDelete", sqlParams.ToArray());
        }

        /// <summary>
        /// delete all file data
        /// </summary>
        public void DeleteAnswerFile(int fileId, string groupGuid)
        {
            //SqlParameter[] commandParameters = new SqlParameter[5];
            //commandParameters[0] = new SqlParameter("@FileId", SqlDbType.Int, 4);
            //commandParameters[0].Value = fileId;
            //commandParameters[1] = new SqlParameter("@GroupGuid", SqlDbType.NVarChar, 40);
            //commandParameters[1].Value = groupGuid;

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@FileId", fileId).SqlValue);
                sqlParams.Add(new SqlParameter("@GroupGuid", groupGuid).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spFileDelete", sqlParams.ToArray());
        }

        /// <summary>
        /// Deletes the persisted answer properties from the DB
        /// </summary>
        public void DeleteAnswerProperties(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerId", answerId).SqlValue);
            }

            DbConnection.db.ExecuteScalar("vts_spAnswerPropertyDelete", sqlParams.ToArray());
        }

        /// <summary>
        /// Remove one answer column of the matrix from the database
        /// </summary>
        /// <param name="answerId">Answer column to delete from the database</param>
        public void DeleteMatrixAnswer(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerId", answerId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerMatrixDelete", sqlParams.ToArray());
        }

        /// <summary>
        /// Return an answer object that reflects the database answer
        /// </summary>
        /// <param name="answerId">Id of the answer you need</param>
        /// <returns>An answer object with the current database values</returns>
        public AnswerData GetAnswerById(int answerId, string languageCode)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AnswerId", answerId), new SqlParameter("@LanguageCode", languageCode) };

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerId", answerId).SqlValue);
                sqlParams.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            }
            
            AnswerData dataSet = new AnswerData();
            DbConnection.db.LoadDataSet("vts_spAnswerGetDetails", dataSet, new string[] { "Answers" }, sqlParams.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Get all details of the file
        /// </summary>
        public FileData GetAnswerFile(int fileId, string groupGuid)
        {
            FileData dataSet = new FileData();

            //SqlParameter[] commandParameters = new SqlParameter[2];
            //commandParameters[0] = new SqlParameter("@FileId", SqlDbType.Int, 4);
            //commandParameters[0].Value = fileId;
            //commandParameters[1] = new SqlParameter("@GroupGuid", SqlDbType.NVarChar, 40);
            //commandParameters[1].Value = groupGuid;

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@FileId", fileId).SqlValue);
                sqlParams.Add(new SqlParameter("@GroupGuid", groupGuid).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spFileGetDetails", dataSet, new string[] { "Files" }, sqlParams.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Get the count of files in a group
        /// </summary>
        public int GetAnswerFileCount(string groupGuid)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@GroupGuid", groupGuid).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spFileGetGroupCount", sqlParams.ToArray());
            if (obj2 != null)
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        /// <summary>
        /// Returns the stream of the requested file
        /// </summary>
        public byte[] GetAnswerFileData(int fileId, string groupGuid)
        {
            //SqlParameter[] commandParameters = new SqlParameter[2];
            //commandParameters[0] = new SqlParameter("@FileId", SqlDbType.Int, 4);
            //commandParameters[0].Value = fileId;
            //commandParameters[1] = new SqlParameter("@GroupGuid", SqlDbType.NVarChar, 40);
            //commandParameters[1].Value = groupGuid;

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@FileId", fileId).SqlValue);
                sqlParams.Add(new SqlParameter("@GroupGuid", groupGuid).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spFileGetData", sqlParams.ToArray());
            if (obj2 != null)
            {
                return (byte[]) obj2;
            }
            return new byte[0];
        }

        /// <summary>
        /// Get all answers from a give question
        /// </summary>
        /// <param name="questionId">question which is owning the answers</param>
        /// <param name="languageCode">Language in which to return the answers, -1 act as the default language</param>
        /// <returns></returns>
        public AnswerData GetAnswers(int questionId, string languageCode)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@QuestionId", questionId), new SqlParameter("@LanguageCode", languageCode) };

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
                sqlParams.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            }
            
            AnswerData dataSet = new AnswerData();
            DbConnection.db.LoadDataSet("vts_spQuestionGetAnswers", dataSet, new string[] { "Answers" }, sqlParams.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Get a light list of answers for the given question
        /// </summary>
        /// <param name="questionId">question which is owning the answers</param>
        /// <returns></returns>
        public AnswerData GetAnswersList(int questionId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
            }

            AnswerData dataSet = new AnswerData();
            DbConnection.db.LoadDataSet("vts_spQuestionGetAnswersList", dataSet, new string[] { "Answers" }, sqlParams.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Return the total score of the given answers
        /// </summary>
        /// <param name="answersIdValues">CSV of answer ids to get the total score from</param>
        /// <returns></returns>
        public int GetAnswersScoreTotal(string answersIdValues)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerIDCSV", answersIdValues).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spAnswerGetScoreTotal", sqlParams.ToArray());
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        /// <summary>
        /// Returns the answer type mode of the answer 
        /// </summary>
        public int GetAnswerTypeMode(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerId", answerId).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spAnswerGetAnswerTypeMode", sqlParams.ToArray());
            if (obj2 != null)
            {
                return (int) obj2;
            }
            return 0;
        }

        /// <summary>
        /// Get a list of all files associated with this guidto
        /// </summary>
        public FileData GetGuidFiles(string groupGuid)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@GroupGuid", groupGuid).SqlValue);
            }

            FileData dataSet = new FileData();
            DbConnection.db.LoadDataSet("vts_spFileGetListForGuid", dataSet, new string[] { "Files" }, sqlParams.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Creates and return an insert command for an answer
        /// </summary>
        public SqlCommand GetInsertAnswerCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spAnswerAddNew", sqlConnection) : new SqlCommand("vts_spAnswerAddNew", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID"));
            command.Parameters.Add(new SqlParameter("@AnswerText", SqlDbType.NVarChar, 0xfa0, "AnswerText"));
            command.Parameters.Add(new SqlParameter("@DefaultText", SqlDbType.NVarChar, 0xfa0, "DefaultText"));
            command.Parameters.Add(new SqlParameter("@AnswerPipeAlias", SqlDbType.NVarChar, 0xff, "AnswerPipeAlias"));
            command.Parameters.Add(new SqlParameter("@ImageURL", SqlDbType.NVarChar, 0x3e8, "ImageURL"));
            command.Parameters.Add(new SqlParameter("@AnswerTypeID", SqlDbType.Int, 4, "AnswerTypeID"));
            command.Parameters.Add(new SqlParameter("@RatePart", SqlDbType.Bit, 1, "RatePart"));
            command.Parameters.Add(new SqlParameter("@Selected", SqlDbType.Bit, 1, "Selected"));
            command.Parameters.Add(new SqlParameter("@ScorePoint", SqlDbType.Int, 4, "ScorePoint"));
            command.Parameters.Add(new SqlParameter("@DisplayOrder", SqlDbType.Int, 4, "DisplayOrder"));
            command.Parameters.Add(new SqlParameter("@RegularExpressionId", SqlDbType.Int, 4, "RegularExpressionId"));
            command.Parameters.Add(new SqlParameter("@Mandatory", SqlDbType.Bit, 1, "Mandatory"));
            command.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            command.Parameters.Add(new SqlParameter("@AnswerIDText", SqlDbType.NVarChar, 255, "AnswerIDText"));
            command.Parameters.Add(new SqlParameter("@AnswerAlias", SqlDbType.NVarChar, 255, "AnswerAlias"));
            command.Parameters.Add(new SqlParameter("@SliderRange", SqlDbType.NVarChar, 3, "SliderRange"));
            command.Parameters.Add(new SqlParameter("@SliderValue", SqlDbType.Int, 6, "SliderValue"));
            command.Parameters.Add(new SqlParameter("@SliderMin", SqlDbType.Int, 6, "SliderMin"));
            command.Parameters.Add(new SqlParameter("@SliderMax", SqlDbType.Int, 6, "SliderMax"));
            command.Parameters.Add(new SqlParameter("@SliderAnimate", SqlDbType.Bit, 1, "SliderAnimate"));
            command.Parameters.Add(new SqlParameter("@SliderStep", SqlDbType.Int, 6, "SliderStep"));
            command.Parameters["@AnswerID"].Direction = ParameterDirection.Output;
            return command;
        }

        /// <summary>
        /// Creates and return an insert command for an answer connection
        /// </summary>
        public SqlCommand GetInsertAnswerConnectionCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spAnswerConnectionSubscribeToPublisher", sqlConnection) : new SqlCommand("vts_spAnswerConnectionSubscribeToPublisher", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@PublisherAnswerId", SqlDbType.Int, 4, "PublisherAnswerId"));
            command.Parameters.Add(new SqlParameter("@SubscriberAnswerId", SqlDbType.Int, 4, "SubscriberAnswerId"));
            return command;
        }

        /// <summary>
        /// Creates and return an insert command for an answer property
        /// </summary>
        public SqlCommand GetInsertAnswerPropertyCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spAnswerPropertyStore", sqlConnection) : new SqlCommand("vts_spAnswerPropertyStore", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerId"));
            command.Parameters.Add(new SqlParameter("@Properties", SqlDbType.Image));
            command.Parameters["@Properties"].SourceColumn = "Properties";
            return command;
        }

        /// <summary>
        /// Get a list of answers that can be subscribed to
        /// </summary>
        public AnswerData GetPublishersList(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerId", answerId).SqlValue);
            }

            AnswerData dataSet = new AnswerData();
            DbConnection.db.LoadDataSet("vts_spAnswerGetPublishersList", dataSet, new string[] { "Answers" }, sqlParams.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Get all answers for a given question that can be selected 
        /// </summary>
        /// <param name="questionId">question which is owning the answers</param>
        /// <returns></returns>
        public AnswerData GetSelectableAnswers(int questionId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@QuestionID", questionId).SqlValue);
            }

            AnswerData dataSet = new AnswerData();
            DbConnection.db.LoadDataSet("vts_spQuestionGetSelectableAnswers", dataSet, new string[] { "Answers" }, sqlParams.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Get a list of answers to which the answer has subscribed to
        /// </summary>
        public AnswerData GetSubscriptionList(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerId", answerId).SqlValue);
            }

            AnswerData dataSet = new AnswerData();
            DbConnection.db.LoadDataSet("vts_spAnswerGetSubscriptionList", dataSet, new string[] { "Answers" }, sqlParams.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all the files that have not yet been validated
        /// </summary>
        public FileData GetUnValidatedFileAnswers(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            FileData dataSet = new FileData();
            
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{   new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@CurrentPage", pageNumber), 
            //    new SqlParameter("@PageSize", pageSize), 
            //    new SqlParameter("@TotalRecords", SqlDbType.Int) 
            //};

            //commandParameters[3].Direction = ParameterDirection.Output;

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@CurrentPage", pageNumber).SqlValue);
                sqlParams.Add(new SqlParameter("@PageSize", pageSize).SqlValue);
                sqlParams.Add(new SqlParameter("@TotalRecords", SqlDbType.Int){Direction = ParameterDirection.Output }.SqlValue);
            }
            
            DbConnection.db.LoadDataSet("vts_spFileUnValidatedGetAll", dataSet, new string[] { "Files" }, sqlParams.ToArray());

            totalRecords = dataSet.Files.Rows.Count;
            //totalRecords = int.Parse(sqlParams[3].ToString());
            return dataSet;
        }

        /// <summary>
        /// Returns all the files that have already been validated
        /// </summary>
        public FileData GetValidatedFileAnswers(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            FileData dataSet = new FileData();

            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyID", surveyId), 
            //    new SqlParameter("@CurrentPage", pageNumber), 
            //    new SqlParameter("@PageSize", pageSize), 
            //    new SqlParameter("@TotalRecords", SqlDbType.Int) };
            //commandParameters[3].Direction = ParameterDirection.Output;

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@CurrentPage", pageNumber).SqlValue);
                sqlParams.Add(new SqlParameter("@PageSize", pageSize).SqlValue);
                sqlParams.Add(new SqlParameter("@TotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output }.SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spFileValidatedGetAll", dataSet, new string[] { "Files" }, sqlParams.ToArray());

            totalRecords = dataSet.Files.Rows.Count;
            //totalRecords = int.Parse(sqlParams[3].ToString());
            return dataSet;
        }

        /// <summary>
        /// Moves down the answer's display position 
        /// </summary>
        /// <param name="answerId"></param>
        public void MoveAnswerDown(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerID", answerId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerMoveDown", sqlParams.ToArray());
        }

        /// <summary>
        /// Moves up the answer's display position 
        /// </summary>
        /// <param name="answerId"></param>
        public void MoveAnswerUp(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerID", answerId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerMoveUp", sqlParams.ToArray());
        }

        /// <summary>
        /// Returns the stream of the requested answer properties
        /// </summary>
        public byte[] RestoreAnswerProperties(int answerId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerID", answerId).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spAnswerPropertyRestore", sqlParams.ToArray());
            if (obj2 != null)
            {
                return (byte[]) obj2;
            }
            return null;
        }

        /// <summary>
        /// Stores a file in the database
        /// </summary>
        public int StoreAnswerFile(string groupGuid, string fileName, int fileSize, string fileType, byte[] fileData, int uploadedFileTimeOut, int sessionUploadedFileTimeOut)
        {
            //SqlParameter[] commandParameters = new SqlParameter[7];
            //commandParameters[0] = new SqlParameter("@GroupGuid", SqlDbType.NVarChar, 40);
            //commandParameters[0].Value = groupGuid;
            //commandParameters[1] = new SqlParameter("@FileName", SqlDbType.NVarChar, 0x400);
            //commandParameters[1].Value = fileName;
            //commandParameters[2] = new SqlParameter("@FileSize", SqlDbType.Int, 4);
            //commandParameters[2].Value = fileSize;
            //commandParameters[3] = new SqlParameter("@SessionUploadedFileTimeOut", SqlDbType.Int, 4);
            //commandParameters[3].Value = sessionUploadedFileTimeOut;
            //commandParameters[4] = new SqlParameter("@UploadedFileTimeOut", SqlDbType.Int, 4);
            //commandParameters[4].Value = uploadedFileTimeOut;
            //commandParameters[5] = new SqlParameter("@FileType", SqlDbType.NVarChar, 0x400);
            //commandParameters[5].Value = fileType;
            //commandParameters[6] = new SqlParameter("@FileData", SqlDbType.Image, fileData.Length);
            //commandParameters[6].Value = fileData;

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@GroupGuid", SqlDbType.NVarChar, 40) { Value = groupGuid }.SqlValue);
                sqlParams.Add(new SqlParameter("@FileName", SqlDbType.NVarChar, 0x400) { Value = fileName }.SqlValue);
                sqlParams.Add(new SqlParameter("@FileSize", SqlDbType.Int, 4) { Value = fileSize }.SqlValue);
                sqlParams.Add(new SqlParameter("@FileType", SqlDbType.NVarChar, 0x400) { Value = fileType }.SqlValue);
                sqlParams.Add(new SqlParameter("@FileData", SqlDbType.Image, fileData.Length) { Value = fileData }.SqlValue);
                sqlParams.Add(new SqlParameter("@UploadedFileTimeOut", SqlDbType.Int, 4) { Value = uploadedFileTimeOut }.SqlValue);
                sqlParams.Add(new SqlParameter("@SessionUploadedFileTimeOut", SqlDbType.Int, 4) { Value = sessionUploadedFileTimeOut }.SqlValue);
            }


            object obj2 = DbConnection.db.ExecuteScalar("vts_spFileAddNew", sqlParams.ToArray());

            if (obj2 != null)
            {
                return int.Parse(obj2.ToString());
            }
            return -1;
        }

        /// <summary>
        /// Stores a file in the database
        /// </summary>
        public void StoreAnswerProperties(int answerId, byte[] properties)
        {
        //    SqlParameter[] commandParameters = new SqlParameter[2];
        //    commandParameters[0] = new SqlParameter("@AnswerID", SqlDbType.Int, 4);
        //    commandParameters[0].Value = answerId;
        //    commandParameters[1] = new SqlParameter("@Properties", SqlDbType.Image, properties.Length);
        //    commandParameters[1].Value = properties;

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4){Value = answerId}.SqlValue);
                sqlParams.Add(new SqlParameter("@Properties", SqlDbType.Image, properties.Length){ Value = properties }.SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerPropertyStore", sqlParams.ToArray());
        }

        /// <summary>
        /// Subscribe to a new answer publisher
        /// </summary>
        public void SubscribeToPublisher(int publisherAnswerId, int subscriberAnswerId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{   new SqlParameter("@PublisherAnswerId", publisherAnswerId), 
            //    new SqlParameter("@SubscriberAnswerId", subscriberAnswerId) 
            //};

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@PublisherAnswerId", publisherAnswerId).SqlValue);
                sqlParams.Add(new SqlParameter("@SubscriberAnswerId", subscriberAnswerId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spAnswerConnectionSubscribeToPublisher", sqlParams.ToArray());
        }

        /// <summary>
        /// Unsubscribe from the given answer publisher
        /// </summary>
        public void UnSubscribeFromPublisher(int publisherAnswerId, int subscriberAnswerId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{   new SqlParameter("@PublisherAnswerId", publisherAnswerId), 
            //    new SqlParameter("@SubscriberAnswerId", subscriberAnswerId) 
            //};

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@PublisherAnswerId", publisherAnswerId).SqlValue);
                sqlParams.Add(new SqlParameter("@SubscriberAnswerId", subscriberAnswerId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerConnectionUnSubscribeFromPublisher", sqlParams.ToArray());
        }

        /// <summary>
        /// Update the answer in the database
        /// </summary>
        /// <param name="updatedAnswer">Answer to update, must specify the answer id</param>
        public void UpdateAnswer(AnswerData updatedAnswer, string languageCode)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spAnswerUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@LanguageCode", languageCode));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerText", SqlDbType.NVarChar, 0xfa0, "AnswerText"));
            insertCommand.Parameters.Add(new SqlParameter("@DefaultText", SqlDbType.NVarChar, 0xfa0, "DefaultText"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerPipeAlias", SqlDbType.NVarChar, 0xff, "AnswerPipeAlias"));
            insertCommand.Parameters.Add(new SqlParameter("@ImageURL", SqlDbType.NVarChar, 0x3e8, "ImageURL"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerTypeID", SqlDbType.Int, 4, "AnswerTypeID"));
            insertCommand.Parameters.Add(new SqlParameter("@RatePart", SqlDbType.Bit, 1, "RatePart"));
            insertCommand.Parameters.Add(new SqlParameter("@Selected", SqlDbType.Bit, 1, "Selected"));
            insertCommand.Parameters.Add(new SqlParameter("@ScorePoint", SqlDbType.Int, 4, "ScorePoint"));
            insertCommand.Parameters.Add(new SqlParameter("@RegularExpressionId", SqlDbType.Int, 4, "RegularExpressionId"));
            insertCommand.Parameters.Add(new SqlParameter("@Mandatory", SqlDbType.Bit, 1, "Mandatory"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerIDText", SqlDbType.NVarChar, 255, "AnswerIDText"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerAlias", SqlDbType.NVarChar, 255, "AnswerAlias"));
            insertCommand.Parameters.Add(new SqlParameter("@SliderRange", SqlDbType.NVarChar, 3, "SliderRange"));
            insertCommand.Parameters.Add(new SqlParameter("@SliderValue", SqlDbType.Int, 6, "SliderValue"));
            insertCommand.Parameters.Add(new SqlParameter("@SliderMin", SqlDbType.Int, 6, "SliderMin"));
            insertCommand.Parameters.Add(new SqlParameter("@SliderMax", SqlDbType.Int, 6, "SliderMax"));
            insertCommand.Parameters.Add(new SqlParameter("@SliderAnimate", SqlDbType.Bit, 1, "SliderAnimate"));
            insertCommand.Parameters.Add(new SqlParameter("@SliderStep", SqlDbType.Int, 6, "SliderStep"));
            DbConnection.db.UpdateDataSet(updatedAnswer, "Answers", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Update the matrix answer in the database
        /// </summary>
        /// <param name="updatedAnswer">Answer to update, must specify the answer id</param>
        public void UpdateMatrixAnswer(AnswerData updatedAnswer, string languageCode)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spAnswerMatrixUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerText", SqlDbType.NVarChar, 0xfa0, "AnswerText"));
            insertCommand.Parameters.Add(new SqlParameter("@ImageURL", SqlDbType.NVarChar, 0x3e8, "ImageURL"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerTypeID", SqlDbType.Int, 4, "AnswerTypeID"));
            insertCommand.Parameters.Add(new SqlParameter("@RatePart", SqlDbType.Bit, 1, "RatePart"));
            insertCommand.Parameters.Add(new SqlParameter("@Mandatory", SqlDbType.Bit, 1, "Mandatory"));
            insertCommand.Parameters.Add(new SqlParameter("@LanguageCode", languageCode));
            DbConnection.db.UpdateDataSet(updatedAnswer, "Answers", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }
    }
}

