namespace Votations.NSurvey.SQLServerDAL
{
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    // using Microsoft.ApplicationBlocks.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;
    using System.Linq;
    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class Question : IQuestion
    {
        /// <summary>
        /// Adds a new child question to the parent question specified by the parent questio id in the database
        /// </summary>
        /// <param name="newChildQuestion">Question object with information about what to add. Only Id must be ommited</param>
        public void AddChildQuestion(MatrixChildQuestionData newChildQuestion)
        {
            SqlConnection sqlConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            DbConnection.db.UpdateDataSet(newChildQuestion, "ChildQuestions", this.GetInsertChildQuestionCommand(sqlConnection, null), new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Adds a new question to the survey specified by the survey id in the database
        /// </summary>
        /// <param name="newQuestion">Question object with information about what to add. Only Id must be ommited</param>
        public void AddQuestion(QuestionData newQuestion)
        {
            SqlConnection sqlConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            DbConnection.db.UpdateDataSet(newQuestion, "Questions", this.GetInsertQuestionCommand(sqlConnection, null), new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Adds a new answer to be shown in the section grid
        /// </summary>
        public void AddQuestionSectionGridAnswers(int questionId, int answerId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{   new SqlParameter("@QuestionId", questionId), 
            //    new SqlParameter("@AnswerId", answerId) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@AnswerId", answerId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spQuestionSectionGridAnswerAddNew", commandParameters.ToArray());
        }

        /// <summary>
        /// Add a new skip logic rule
        /// </summary>
        public void AddSkipLogicRule(SkipLogicRuleData newSkipLogicRule)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spQuestionSkipLogicRuleAddNew", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@SkipQuestionId", SqlDbType.Int, 4, "SkipQuestionId"));
            insertCommand.Parameters.Add(new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            insertCommand.Parameters.Add(new SqlParameter("@TextFilter", SqlDbType.NVarChar, 0xfa0, "TextFilter"));
            insertCommand.Parameters.Add(new SqlParameter("@ConditionalOperator", SqlDbType.Int, 4, "ConditionalOperator"));
            insertCommand.Parameters.Add(new SqlParameter("@ExpressionOperator", SqlDbType.Int, 4, "ExpressionOperator"));
            insertCommand.Parameters.Add(new SqlParameter("@Score", SqlDbType.Int, 4, "Score"));
            insertCommand.Parameters.Add(new SqlParameter("@ScoreMax", SqlDbType.Int, 4, "ScoreMax"));
            insertCommand.Parameters.Add(new SqlParameter("@SkipLogicRuleID", SqlDbType.Int, 4, "SkipLogicRuleID"));
            insertCommand.Parameters["@SkipLogicRuleID"].Direction = ParameterDirection.Output;
            DbConnection.db.UpdateDataSet(newSkipLogicRule, "SkipLogicRules", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Check if the user has this question assigned
        /// </summary>
        public bool CheckQuestionUser(int questionId, int userId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@QuestionId", questionId), 
            //    new SqlParameter("@UserId", userId) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }
            
            object obj2 = DbConnection.db.ExecuteScalar("vts_spQuestionCheckUserAssigned", commandParameters.ToArray());
            return ((obj2 != null) && (obj2 != DBNull.Value));
        }

        /// <summary>
        /// Clones a question in the DB and returns the cloned question object 
        /// </summary>
        /// <param name="questionId">Id of the question you want to clone</param>
        /// <returns>A questiondata object with the cloned question</returns>
        public QuestionData CloneQuestionById(int questionId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionCloneById", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Copy a question to the given target survey
        /// </summary>
        public void CopyQuestionById(int questionId, int targetSurveyId, int targetDisplayOrder, int targetPageNumber)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@QuestionId", questionId), 
            //    new SqlParameter("@NewSurveyId", targetSurveyId), 
            //    new SqlParameter("@DisplayOrder", targetDisplayOrder), 
            //    new SqlParameter("@PageNumber", targetPageNumber), 
            //    new SqlParameter("@QuestionCopyId", SqlDbType.Int) 
            //};
            //commandParameters[4].Direction = ParameterDirection.Output;

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@NewSurveyId", targetSurveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@DisplayOrder", targetDisplayOrder).SqlValue);
                commandParameters.Add(new SqlParameter("@PageNumber", targetPageNumber).SqlValue);
                commandParameters.Add(new SqlParameter("@QuestionCopyId", SqlDbType.Int) { Direction = ParameterDirection.Output}.SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spQuestionCopy", commandParameters.ToArray());
            //return int.Parse(commandParameters[4].ToString());
        }

        /// <summary>
        /// Copy a question to the library
        /// </summary>
        public void CopyQuestionToLibrary(int questionId, int libraryId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@QuestionId", questionId), 
            //    new SqlParameter("@LibraryId", libraryId), 
            //    new SqlParameter("@QuestionCopyId", SqlDbType.Int) 
            //};
            //commandParameters[2].Direction = ParameterDirection.Output;

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@LibraryId", libraryId).SqlValue);
                commandParameters.Add(new SqlParameter("@QuestionCopyId", SqlDbType.Int){Direction = ParameterDirection.Output}.SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spQuestionCopyToLibrary", commandParameters.ToArray());
            //return int.Parse(commandParameters[2].ToString());
        }

        /// <summary>
        /// Remove all the matrix answers from the database
        /// </summary>
        /// <param name="parentQuestionId">Matrix question id</param>
        public void DeleteMatrixAnswers(int parentQuestionId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@ParentQuestionID", parentQuestionId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerMatrixDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Remove the question and all its answers from the database
        /// </summary>
        /// <param name="questionId">Question to delete from the database</param>
        public void DeleteQuestionById(int questionId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionID", questionId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spQuestionDeleteById", commandParameters.ToArray());
        }

        /// <summary>
        /// Delete an answer to be shown in the section grid
        /// </summary>
        public void DeleteQuestionSectionGridAnswers(int questionId, int answerId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{   new SqlParameter("@QuestionId", questionId), 
            //    new SqlParameter("@AnswerId", answerId) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionID", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@AnswerId", answerId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spQuestionSectionGridAnswerDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes the question's sections option
        /// </summary>
        public void DeleteQuestionSectionOptions(int questionId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionID", questionId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spQuestionSectionOptionDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes the skip logic rule
        /// </summary>
        public void DeleteSkipLogicRuleById(int skipLogicRuleId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SkipLogicRuleId", skipLogicRuleId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spQuestionSkipLogicRuleDeleteByID", commandParameters.ToArray());
        }

        /// <summary>
        /// Returns a question list that can be answered 
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableQuestionList(int surveyId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionanswerableList", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns a question list of the given page that can be answered 
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableQuestionList(int surveyId, int pageNumber)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@pageNumber", pageNumber) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@pageNumber", pageNumber).SqlValue);
            }
            
            QuestionData dataSet = new QuestionData();
            DbConnection.db.LoadDataSet("vts_spQuestionanswerableListForPage", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns a question list of the given page range that can be answered 
        /// </summary>
        public QuestionData GetAnswerableQuestionListInPageRange(int surveyId, int startPageNumber, int endPageNumber)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@StartPageNumber", startPageNumber), 
            //    new SqlParameter("@EndPageNumber", endPageNumber) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@StartPageNumber", startPageNumber).SqlValue);
                commandParameters.Add(new SqlParameter("@EndPageNumber", endPageNumber).SqlValue);
            }
            
            QuestionData dataSet = new QuestionData();
            DbConnection.db.LoadDataSet("vts_spQuestionanswerableListForPageRange", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns a question list that can be answered without their childs
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableQuestionListWithoutChilds(int surveyId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionAnswerableListWithoutChilds", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns the questions that can be answered without their childs
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableQuestionWithoutChilds(int surveyId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionAnswerableWithoutChilds", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns a question list that can be answered and that don't have any questions
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableSingleQuestionListWithoutChilds(int surveyId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionSingleAnswerableListWithoutChilds", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// returns a results set with the compare question's answers number of voter 
        /// that have also answered the base question answer
        /// </summary>
        public DataSet GetCrossTabResults(int compareQuestionId, int baseAnswerId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@CompareQuestionId", compareQuestionId), 
            //    new SqlParameter("@BaseQuestionAnswerId", baseAnswerId) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@CompareQuestionId", compareQuestionId).SqlValue);
                commandParameters.Add(new SqlParameter("@BaseQuestionAnswerId", baseAnswerId).SqlValue);
            }

            return DbConnection.db.ExecuteDataSet("vts_spQuestionGetCrossTabResults", commandParameters.ToArray());
        }

        /// <summary>
        /// Creates and return an insert command for a question
        /// </summary>
        public SqlCommand GetInsertChildQuestionCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spQuestionChildAddNew", sqlConnection) : new SqlCommand("vts_spQuestionChildAddNew", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ParentQuestionId", SqlDbType.Int, 4, "ParentQuestionID"));
            command.Parameters.Add(new SqlParameter("@QuestionText", SqlDbType.NVarChar, 0xfa0, "QuestionText"));
            command.Parameters.Add(new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID"));
            command.Parameters["@QuestionID"].Direction = ParameterDirection.Output;
            return command;
        }

        /// <summary>
        /// Creates and return an insert command for a question
        /// </summary>
        public SqlCommand GetInsertQuestionCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spQuestionAddNew", sqlConnection) : new SqlCommand("vts_spQuestionAddNew", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int, 4, "SurveyId"));
            command.Parameters.Add(new SqlParameter("@LibraryId", SqlDbType.Int, 4, "LibraryId"));
            command.Parameters.Add(new SqlParameter("@SelectionModeId", SqlDbType.Int, 4, "SelectionModeId"));
            command.Parameters.Add(new SqlParameter("@LayoutModeId", SqlDbType.Int, 4, "LayoutModeId"));
            command.Parameters.Add(new SqlParameter("@DisplayOrder", SqlDbType.Int, 4, "DisplayOrder"));
            command.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int, 4, "PageNumber"));
            command.Parameters.Add(new SqlParameter("@QuestionText", SqlDbType.NVarChar, -1, "QuestionText"));
            command.Parameters.Add(new SqlParameter("@ColumnsNumber", SqlDbType.Int, 4, "ColumnsNumber"));
            command.Parameters.Add(new SqlParameter("@QuestionPipeAlias", SqlDbType.NVarChar, 0xff, "QuestionPipeAlias"));
            command.Parameters.Add(new SqlParameter("@MinSelectionRequired", SqlDbType.Int, 4, "MinSelectionRequired"));
            command.Parameters.Add(new SqlParameter("@MaxSelectionAllowed", SqlDbType.Int, 4, "MaxSelectionAllowed"));
            command.Parameters.Add(new SqlParameter("@RandomizeAnswers", SqlDbType.Bit, 1, "RandomizeAnswers"));
            command.Parameters.Add(new SqlParameter("@RatingEnabled", SqlDbType.Bit, 1, "RatingEnabled"));
            command.Parameters.Add(new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionId"));
            command.Parameters.Add(new SqlParameter("@QuestionIDText", SqlDbType.NVarChar, 255, "QuestionIDText"));
            command.Parameters.Add(new SqlParameter("@Alias", SqlDbType.NVarChar, 255, "Alias"));
            command.Parameters.Add(new SqlParameter("@HelpText", SqlDbType.NVarChar, 255, "HelpText"));
            command.Parameters.Add(new SqlParameter("@ShowHelpText", SqlDbType.Bit, 1, "ShowHelpText"));
            command.Parameters.Add(new SqlParameter("@QuestionGroupId", SqlDbType.Int, 4, "QuestionGroupId"));

            //command.Parameters["@SurveyID"].IsNullable = true;
            command.Parameters["@QuestionID"].Direction = ParameterDirection.Output;
            return command;
        }

        /// <summary>
        /// Creates and return an insert command for a question's section
        /// </summary>
        public SqlCommand GetInsertQuestionSectionCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction, string languageCode)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spQuestionSectionOptionUpdate", sqlConnection) : new SqlCommand("vts_spQuestionSectionOptionUpdate", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@LanguageCode", languageCode));
            command.Parameters.Add(new SqlParameter("@QuestionId", SqlDbType.Int, 4, "QuestionId"));
            command.Parameters.Add(new SqlParameter("@RepeatableSectionModeId", SqlDbType.Int, 4, "RepeatableSectionModeId"));
            command.Parameters.Add(new SqlParameter("@AddSectionLinkText", SqlDbType.NVarChar, 0xff, "AddSectionLinkText"));
            command.Parameters.Add(new SqlParameter("@DeleteSectionLinkText", SqlDbType.NVarChar, 0xff, "DeleteSectionLinkText"));
            command.Parameters.Add(new SqlParameter("@EditSectionLinkText", SqlDbType.NVarChar, 0xff, "EditSectionLinkText"));
            command.Parameters.Add(new SqlParameter("@UpdateSectionLinkText", SqlDbType.NVarChar, 0xff, "UpdateSectionLinkText"));
            command.Parameters.Add(new SqlParameter("@MaxSections", SqlDbType.Int, 4, "MaxSections"));
            return command;
        }

        /// <summary>
        /// Creates and return an insert command for a question's grid section answers
        /// </summary>
        public SqlCommand GetInsertQuestionSectionGridAnswersCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spQuestionSectionGridAnswerAddNew", sqlConnection) : new SqlCommand("vts_spQuestionSectionGridAnswerAddNew", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@QuestionId", SqlDbType.Int, 4, "QuestionId"));
            command.Parameters.Add(new SqlParameter("@AnswerId", SqlDbType.Int, 4, "AnswerId"));
            return command;
        }

        /// <summary>
        /// Returns a question list for the given library 
        /// that can be answered and that don't have any child questions
        /// </summary>
        /// <returns>A question object collection</returns>
        public QuestionData GetLibraryAnswerableSingleQuestionListWithoutChilds(int libraryId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@LibraryId", libraryId).SqlValue);
            }
            
            DbConnection.db.LoadDataSet("vts_spQuestionLibrarySingleAnswerableListWithoutChilds", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question listed in the library
        /// </summary>
        public QuestionData GetLibraryQuestionList(int libraryId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@LibraryId", libraryId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionsGetListForLibrary", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question listed in the library without their child questions
        /// </summary>
        public QuestionData GetLibraryQuestionListWithoutChilds(int libraryId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@LibraryId", libraryId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionsGetListForLibraryWithoutChilds", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question listed in the library with their details
        /// </summary>
        public QuestionData GetLibraryQuestions(int libraryId, string languageCode)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@LibraryId", libraryId).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionsGetForLibrary", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question childs from the question
        /// </summary>
        /// <param name="parentQuestionId">Question id from which you want to retrieve the child questions</param>
        /// <returns>A question object collection</returns>
        public MatrixChildQuestionData GetMatrixChildQuestions(int parentQuestionId, string languageCode)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@ParentQuestionId", parentQuestionId), 
            //    new SqlParameter("@LanguageCode", languageCode) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@ParentQuestionId", parentQuestionId).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            }            
            
            MatrixChildQuestionData dataSet = new MatrixChildQuestionData();
            DbConnection.db.LoadDataSet("vts_spQuestionsGetMatrixChilds", dataSet, new string[] { "ChildQuestions", "Answers" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns the question until next page break is encountered
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the paged questions</param>
        /// <param name="pageNumber">Page number to retrieve</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetPagedQuestions(int surveyId, int pageNumber, string languageCode)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyId", surveyId), 
            //    new SqlParameter("@PageNumber", pageNumber), 
            //    new SqlParameter("@LanguageCode", languageCode) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@PageNumber", pageNumber).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            }   
            
            QuestionData dataSet = new QuestionData();
            DbConnection.db.LoadDataSet("vts_spQuestionsGetPagedForSurvey", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question's answer subscribers
        /// </summary>
        public AnswerConnectionData GetQuestionAnswerConnections(int questionId)
        {
            AnswerConnectionData dataSet = new AnswerConnectionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionGetAnswerConnection", dataSet, new string[] { "AnswerConnections" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Return a question object that reflects the database question
        /// </summary>
        /// <param name="questionId">Id of the question you need</param>
        /// <returns>A questiondata object with the current database values</returns>
        public QuestionData GetQuestionById(int questionId, string languageCode)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@QuestionId", questionId), 
            //    new SqlParameter("@LanguageCode", languageCode) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            }                
           
            QuestionData dataSet = new QuestionData();
            DbConnection.db.LoadDataSet("vts_spQuestionGetDetails", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question details, answers and answer types
        /// </summary>
        public NSurveyQuestion GetQuestionForExport(int questionId)
        {
            NSurveyQuestion dataSet = new NSurveyQuestion();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
            }   

            DbConnection.db.LoadDataSet("vts_spQuestionGetForExport", dataSet,
                new string[] { "AnswerType", "RegularExpression", "Question", "Answer", "AnswerConnection", "ChildQuestion", "AnswerProperty", 
                    "QuestionSectionOption", "QuestionSectionGridAnswer","MultiLanguageText","QuestionGroups" },
                    commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question including their childs
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions and childs</param>
        /// <returns>A question object collection with all their childs</returns>
        public QuestionData GetQuestionHierarchy(int surveyId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }   

            DbConnection.db.LoadDataSet("vts_spQuestionGetHierarchyForSurvey", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Return the layout modes available
        /// </summary>
        /// <returns>A questionlayoutmodedata object with the current layout values</returns>
        public LayoutModeData GetQuestionLayoutModes()
        {
            LayoutModeData dataSet = new LayoutModeData();
            DbConnection.db.LoadDataSet("vts_spLayoutModeGetAll", dataSet, new string[] { "LayoutModes" });
            return dataSet;
        }

        /// <summary>
        /// Returns a question list of all available questions
        /// in the given page range
        /// </summary>
        public QuestionData GetQuestionListForPageRange(int surveyId, int startPageNumber, int endPageNumber)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@startPage", startPageNumber), 
            //    new SqlParameter("@endPage", endPageNumber) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@startPage", startPageNumber).SqlValue);
                commandParameters.Add(new SqlParameter("@endPage", endPageNumber).SqlValue);
            }   
            
            QuestionData dataSet = new QuestionData();
            DbConnection.db.LoadDataSet("vts_spQuestionsGetPageRangeForSurvey", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns a question list with only text, questionid and display order field 
        /// from the given survey that have at leat one selectable answer type
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetQuestionListWithSelectableAnswers(int surveyId)
        {
            QuestionData dataSet = new QuestionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            } 

            DbConnection.db.LoadDataSet("vts_spQuestionListWithSelectableAnswers", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns a question list with only text, questionid and display order field 
        /// from the given survey that have at leat one selectable answer type
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <param name="pageNumber">Page from which we need the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetQuestionListWithSelectableAnswers(int surveyId, int pageNumber)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@pageNumber", pageNumber) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@pageNumber", pageNumber).SqlValue);
            }             
            
            QuestionData dataSet = new QuestionData();
            DbConnection.db.LoadDataSet("vts_spQuestionListWithSelectableAnswersForPage", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns the question and its answers results
        /// </summary>
        public QuestionResultsData GetQuestionResults(int questionId, int filterId, string sortOrder, string languageCode, DateTime startDate, DateTime endDate)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@questionId", questionId), 
            //    new SqlParameter("@filterId", filterId), 
            //    new SqlParameter("@sortOrder", sortOrder), 
            //    new SqlParameter("@LanguageCode", languageCode), 
            //    new SqlParameter("@StartDate", startDate), 
            //    new SqlParameter("@EndDate", endDate) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@questionId", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@filterId", filterId).SqlValue);
                commandParameters.Add(new SqlParameter("@sortOrder", sortOrder).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
                commandParameters.Add(new SqlParameter("@StartDate", startDate).SqlValue);
                commandParameters.Add(new SqlParameter("@EndDate", endDate).SqlValue);
            } 
            
            
            QuestionResultsData dataSet = new QuestionResultsData();
            DbConnection.db.LoadDataSet("vts_spQuestionGetResults", dataSet, new string[] { "Questions", "Answers" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question from the given survey
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetQuestions(int surveyId, string languageCode)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@SurveyId", surveyId), 
            //    new SqlParameter("@LanguageCode", languageCode) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            } 
            
            QuestionData dataSet = new QuestionData();
            DbConnection.db.LoadDataSet("vts_spQuestionsGetForSurvey", dataSet, new string[] { "Questions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all question and all their answers from the given survey
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions and answers</param>
        /// <returns>A question object collection with all answers</returns>
        public QuestionsAnswersData GetQuestionsAnswers(int surveyId)
        {
            QuestionsAnswersData dataSet = new QuestionsAnswersData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            } 

            DbConnection.db.LoadDataSet("vts_spQuestionsAnswersGetForSurvey", dataSet, new string[] { "Questions", "Answers" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Get all answerids to show in the grid
        /// </summary>
        public int[] GetQuestionSectionGridAnswers(int questionId)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
            }

            DataSet set = DbConnection.db.ExecuteDataSet("vts_spQuestionSectionGridAnswerGet", sqlParams.ToArray());
            int[] numArray = new int[set.Tables[0].Rows.Count];
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                numArray[i] = int.Parse(set.Tables[0].Rows[i].ItemArray[0].ToString());
            }
            return numArray;
        }

        /// <summary>
        /// Return the options available for the question's section
        /// </summary>
        public QuestionSectionOptionData GetQuestionSectionOptions(int questionId, string languageCode)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@QuestionId", questionId), 
            //    new SqlParameter("@LanguageCode", languageCode) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionId", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            } 
            
            QuestionSectionOptionData dataSet = new QuestionSectionOptionData();
            DbConnection.db.LoadDataSet("vts_spQuestionSectionOptionGetDetails", dataSet, new string[] { "QuestionSectionOptions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Retrieves the skip logic rules for this question
        /// </summary>
        public SkipLogicRuleData GetQuestionSkipLogicRules(int questionId)
        {
            SkipLogicRuleData dataSet = new SkipLogicRuleData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SkipQuestionId", questionId).SqlValue);
            } 

            DbConnection.db.LoadDataSet("vts_spQuestionSkipLogicRuleGetAll", dataSet, new string[] { "SkipLogicRules" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Return the selection mode available for user selection
        /// </summary>
        /// <returns>A questionselectionmode object with the current selections options</returns>
        public QuestionSelectionModeData GetSelectableQuestionSelectionModes()
        {
            QuestionSelectionModeData dataSet = new QuestionSelectionModeData();
            DbConnection.db.LoadDataSet("vts_spQuestionSelectionModeGetSelectable", dataSet, new string[] { "QuestionSelectionModes" });
            return dataSet;
        }

        /// <summary>
        /// returns a results set with the total of compare question's answers number of voter 
        /// that have answered or not answered the base question answers
        /// </summary>
        public DataSet GetTotalCrossTabResults(int compareQuestionId, int baseQuestionId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@CompareQuestionId", compareQuestionId), 
            //    new SqlParameter("@BaseQuestionId", baseQuestionId) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@CompareQuestionId", compareQuestionId).SqlValue);
                commandParameters.Add(new SqlParameter("@BaseQuestionId", baseQuestionId).SqlValue);
            } 
            
            return DbConnection.db.ExecuteDataSet("vts_spQuestionGetCrossTabTotalResults", commandParameters.ToArray());
        }

        /// <summary>
        /// returns a results set with the compare question's answers number of voter 
        /// that have not answered the base question answers
        /// </summary>
        public DataSet GetUnansweredCrossTabResults(int compareQuestionId, int baseQuestionId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@CompareQuestionId", compareQuestionId), 
            //    new SqlParameter("@BaseQuestionId", baseQuestionId) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@CompareQuestionId", compareQuestionId).SqlValue);
                commandParameters.Add(new SqlParameter("@BaseQuestionId", baseQuestionId).SqlValue);
            } 
            
            return DbConnection.db.ExecuteDataSet("vts_spQuestionGetCrossTabUnansweredResults", commandParameters.ToArray());
        }

        /// <summary>
        /// Import the given questions into the DB
        /// </summary>
        public void ImportQuestions(NSurveyQuestion importQuestions, int userId)
        {

            SqlConnection sqlConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand insertCommand = new AnswerType().GetInsertAnswerTypeCommand(sqlConnection, sqlTransaction, userId);
            SqlCommand command2 = new RegularExpression().GetInsertRegularExpressionCommand(sqlConnection, sqlTransaction, userId);
            SqlCommand insertQuestionCommand = this.GetInsertQuestionCommand(sqlConnection, sqlTransaction);
            SqlCommand insertChildQuestionCommand = this.GetInsertChildQuestionCommand(sqlConnection, sqlTransaction);
            SqlCommand insertAnswerCommand = new Answer().GetInsertAnswerCommand(sqlConnection, sqlTransaction);
            SqlCommand insertAnswerConnectionCommand = new Answer().GetInsertAnswerConnectionCommand(sqlConnection, sqlTransaction);
            SqlCommand insertAnswerPropertyCommand = new Answer().GetInsertAnswerPropertyCommand(sqlConnection, sqlTransaction);
            SqlCommand command8 = this.GetInsertQuestionSectionCommand(sqlConnection, sqlTransaction, "");
            SqlCommand insertQuestionSectionGridAnswersCommand = this.GetInsertQuestionSectionGridAnswersCommand(sqlConnection, sqlTransaction);
            try
            {

              // FWS 2016/03/14: next line never used, but causes exception error on import xml question to library;
              //  int surveyId = importQuestions.Question.First().SurveyId;

                // Add Question groups so we can attach them to questions

                string defaultLang = null;

                var groups = new QuestionGroups();
                var existingGroups = groups.GetAll(defaultLang).QuestionGroups;
                foreach (var qgrp in importQuestions.QuestionGroups
                    .OrderBy(x => x.IsParentGroupIdNull() ? 0 : 1)) //Load parent groups first
                {
                    var grpHere = existingGroups.FirstOrDefault(x => x.GroupName == qgrp.GroupName);

                    int groupIdHere;
                    if (grpHere == null)
                    {
                        int? parentGroupId = null;
                        if (!qgrp.IsParentGroupIdNull()) //Has Parent Group
                        {
                            var pgrp = importQuestions.QuestionGroups.SingleOrDefault(x => x.ID == qgrp.ParentGroupId);
                            if (pgrp != null)
                            {
                                var exPar = groups.GetAll(defaultLang).QuestionGroups
                                      .FirstOrDefault(x => x.GroupName == pgrp.GroupName);
                                if (exPar != null) parentGroupId = exPar.ID;
                            }
                        }
                        groups.AddNewGroup(qgrp.GroupName, parentGroupId, defaultLang);

                        var grt = groups.GetAll(defaultLang).QuestionGroups
                            .FirstOrDefault(x => x.GroupName == qgrp.GroupName);
                        groupIdHere = grt.ID;
                    }
                    else
                    {
                        groupIdHere = grpHere.ID;
                    }
                    importQuestions.Question
                          .Where(x => x.QuestionGroupId == qgrp.OldId)
                          .ToList().ForEach(x => x.QuestionGroupId = groupIdHere);

                }

                DbConnection.db.UpdateDataSet(importQuestions, "AnswerType", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(importQuestions, "RegularExpression", command2, new SqlCommand(), command2, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(importQuestions, "Question", insertQuestionCommand, new SqlCommand(), insertQuestionCommand, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(importQuestions, "Answer", insertAnswerCommand, new SqlCommand(), insertAnswerCommand, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(importQuestions, "AnswerConnection", insertAnswerConnectionCommand, new SqlCommand(), insertAnswerConnectionCommand, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(importQuestions, "ChildQuestion", insertChildQuestionCommand, new SqlCommand(), insertChildQuestionCommand, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(importQuestions, "AnswerProperty", insertAnswerPropertyCommand, new SqlCommand(), insertAnswerPropertyCommand, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(importQuestions, "QuestionSectionOption", command8, new SqlCommand(), command8, UpdateBehavior.Transactional);
                DbConnection.db.UpdateDataSet(importQuestions, "QuestionSectionGridAnswer", insertQuestionSectionGridAnswersCommand, new SqlCommand(), insertQuestionSectionGridAnswersCommand, UpdateBehavior.Transactional);
                var multiLanguage = new MultiLanguage();
                int newQuestionId = importQuestions.Question[0].QuestionId;
                foreach (var langText in importQuestions.MultiLanguageText)
                {
                    var localGroups = groups.GetAll(defaultLang).QuestionGroups;
                    //Process Survey level
                    if (langText.LanguageMessageTypeId == 10)
                    {
                        var impGrp = importQuestions.QuestionGroups.SingleOrDefault(x => x.OldId == langText.LanguageItemId);
                        if (impGrp != null)
                        {
                            var localGrp = localGroups.SingleOrDefault(x => x.GroupName == impGrp.GroupName);
                            try
                            {
                                if (localGrp != null)
                                    multiLanguage.AddMultiLanguageText(localGrp.ID, langText.LanguageCode, langText.LanguageMessageTypeId, langText.ItemText);
                            }
                            catch (Exception ex) { }
                        }
                    }

                    if (langText.LanguageMessageTypeId == 3 || langText.LanguageMessageTypeId == 11 || langText.LanguageMessageTypeId == 12)
                        multiLanguage.AddMultiLanguageText(newQuestionId
                            , langText.LanguageCode, langText.LanguageMessageTypeId, langText.ItemText);

                    if (langText.LanguageMessageTypeId == 1 || langText.LanguageMessageTypeId == 2 || langText.LanguageMessageTypeId == 13)
                        multiLanguage.AddMultiLanguageText(importQuestions.Answer.Single(x => x.OldId == langText.LanguageItemId).AnswerId
                            , langText.LanguageCode, langText.LanguageMessageTypeId, langText.ItemText);

                }
                sqlTransaction.Commit();
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                sqlTransaction.Rollback();
                throw exception;
            }
        }


        /// <summary>
        /// Move the give question's display position down
        /// </summary>
        /// <param name="questionId">ID of the question to change the position</param>
        public void MoveQuestionPositionDown(int questionId)
        {

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionID", questionId).SqlValue);
            } 

            DbConnection.db.ExecuteNonQuery("vts_spQuestionMoveDown", commandParameters.ToArray());
        }

        /// <summary>
        /// Move the give question's display position up
        /// </summary>
        /// <param name="questionId">ID of the question to change the position</param>
        public void MoveQuestionPositionUp(int questionId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionID", questionId).SqlValue);
            } 

            DbConnection.db.ExecuteNonQuery("vts_spQuestionMoveUp", commandParameters.ToArray());
        }

        /// <summary>
        /// Update the child question in the database
        /// </summary>
        /// <param name="updatedChildQuestion">question to update, must contain the question id</param>
        public void UpdateChildQuestion(MatrixChildQuestionData updatedChildQuestion, string languageCode)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spQuestionChildUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@ChildQuestionId", SqlDbType.Int, 4, "QuestionId"));
            insertCommand.Parameters.Add(new SqlParameter("@QuestionText", SqlDbType.NVarChar, 0xfa0, "QuestionText"));
            insertCommand.Parameters.Add(new SqlParameter("@LanguageCode", languageCode));
            DbConnection.db.UpdateDataSet(updatedChildQuestion, "ChildQuestions", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Update the question in the database
        /// </summary>
        /// <param name="updatedQuestion">question to update, must contain the question id</param>
        public void UpdateQuestion(QuestionData updatedQuestion, string languageCode)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spQuestionUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@LanguageCode", languageCode));
            insertCommand.Parameters.Add(new SqlParameter("@QuestionId", SqlDbType.Int, 4, "QuestionId"));
            insertCommand.Parameters.Add(new SqlParameter("@SelectionModeId", SqlDbType.Int, 4, "SelectionModeId"));
            insertCommand.Parameters.Add(new SqlParameter("@LayoutModeId", SqlDbType.Int, 4, "LayoutModeId"));
            insertCommand.Parameters.Add(new SqlParameter("@ColumnsNumber", SqlDbType.Int, 4, "ColumnsNumber"));
            insertCommand.Parameters.Add(new SqlParameter("@QuestionText", SqlDbType.NVarChar, -1 , "QuestionText")); // -1 = (max)
            insertCommand.Parameters.Add(new SqlParameter("@QuestionPipeAlias", SqlDbType.NVarChar, 0xff, "QuestionPipeAlias"));
            insertCommand.Parameters.Add(new SqlParameter("@MinSelectionRequired", SqlDbType.Int, 4, "MinSelectionRequired"));
            insertCommand.Parameters.Add(new SqlParameter("@MaxSelectionAllowed", SqlDbType.Int, 4, "MaxSelectionAllowed"));
            insertCommand.Parameters.Add(new SqlParameter("@RandomizeAnswers", SqlDbType.Bit, 1, "RandomizeAnswers"));
            insertCommand.Parameters.Add(new SqlParameter("@RatingEnabled", SqlDbType.Bit, 1, "RatingEnabled"));
            insertCommand.Parameters.Add(new SqlParameter("@QuestionGroupId", SqlDbType.Int, 4, "QuestionGroupId"));
            insertCommand.Parameters.Add(new SqlParameter("@ShowHelpText", SqlDbType.Bit, 1, "ShowHelpText"));
            insertCommand.Parameters.Add(new SqlParameter("@HelpText", SqlDbType.NVarChar, 4000, "HelpText"));
            insertCommand.Parameters.Add(new SqlParameter("@Alias", SqlDbType.NVarChar, 255, "Alias"));
            insertCommand.Parameters.Add(new SqlParameter("@QuestionIdText", SqlDbType.NVarChar, 255, "QuestionIdText"));
            DbConnection.db.UpdateDataSet(updatedQuestion, "Questions", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }

        /// <summary>
        /// updates a section options, creates it if it doesnt exists
        /// </summary>
        public void UpdateQuestionSectionOptions(QuestionSectionOptionData sectionOptions, string languageCode)
        {
            SqlConnection sqlConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = this.GetInsertQuestionSectionCommand(sqlConnection, null, languageCode);
            DbConnection.db.UpdateDataSet(sectionOptions, "QuestionSectionOptions", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Move question in display order for current library one position up
        /// </summary>
        /// <param name="questionId">question id</param>
        public void MoveQuestionUp(int questionId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@QuestionID", questionId), 
            //    new SqlParameter("@UpdateUp", 1) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionID", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@UpdateUp", Convert.ToBoolean(1)).SqlValue);
            } 

            DbConnection.db.ExecuteNonQuery("vts_spQuestionOrderUpdate", commandParameters.ToArray());
        }

        /// <summary>
        /// Move question in display order for current library one position down
        /// </summary>
        /// <param name="questionId">question id</param>
        public void MoveQuestionDown(int questionId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@QuestionID", questionId), 
            //    new SqlParameter("@UpdateUp", 0) 
            //};

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionID", questionId).SqlValue);
                commandParameters.Add(new SqlParameter("@UpdateUp", Convert.ToBoolean(0)).SqlValue);
            } 

            DbConnection.db.ExecuteNonQuery("vts_spQuestionOrderUpdate", commandParameters.ToArray());
        }
    }
}

