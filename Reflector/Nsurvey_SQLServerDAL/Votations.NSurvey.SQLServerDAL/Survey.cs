
/**************************************************************************************************

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

************************************************************************************************/



namespace Votations.NSurvey.SQLServerDAL
{

    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class Survey: ISurvey
    {
        /// <summary>
        /// Survey Class Constructor : empty
        /// </summary>
        static Survey ()
        {
                       
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public SurveyData GetAllSurveysByFolderId(int? folderId)
        {
            SurveyData surveyData = new SurveyData();

            DbConnection.db.LoadDataSet("vts_spSurveyGetListByFolderId", surveyData, new string[] { "Surveys" }, new SqlParameter("@FolderID", folderId).SqlValue);

            return surveyData;
        }

        public SurveyData GetAllSurveys()
        {
            SurveyData surveyData = new SurveyData();
           DbConnection.db.LoadDataSet("vts_spSurveyGetAllDetails", surveyData, new string[] { "Surveys" });
            return surveyData;
        }

        /// <summary>
        /// Get a survey list with only the survey id and title
        /// </summary>
        /// <returns></returns>
        public SurveyData GetAllSurveysList()
        {
            SurveyData surveyData = new SurveyData();
           DbConnection.db.LoadDataSet("vts_spSurveyGetList", surveyData, new string[] { "Surveys" });
            return surveyData;
        }

        /// <summary>
        /// Get a survey list with only the survey id and title with matching title
        /// </summary>
        /// <returns></returns>
        public SurveyData GetAllSurveysByTitle(string title, int? folderID, int userID)
        {

            //SqlCommand command = new SqlCommand("vts_spSurveyGetListByTitle");
            //command.CommandType = CommandType.StoredProcedure;
                        
            SurveyData surveyData = new SurveyData();

            //SqlParameter[] spParameters = new SqlParameter[3];
            //spParameters[0] = command.Parameters.AddWithValue("@SurveyTitle", title);
            //spParameters[1] = command.Parameters.AddWithValue("@FolderId", folderID);
            //spParameters[2] = command.Parameters.AddWithValue("@UserId", userID);

            //spParameters[0] = new SqlParameter("@SurveyTitle", title);
            //spParameters[1] = new SqlParameter("@FolderId", folderID);
            //spParameters[2] = new SqlParameter("@UserId", userID);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyTitle", title).SqlValue);
                sqlParams.Add(new SqlParameter("@FolderId", folderID).SqlValue);
                sqlParams.Add(new SqlParameter("@UserId", userID).SqlValue);
            }

           DbConnection.db.LoadDataSet("vts_spSurveyGetListByTitle", surveyData, new string[] { "Surveys" }, sqlParams.ToArray());
            //db.LoadDataSet("vts_spSurveyGetListByTitle", surveyData, new string[] { "Surveys" }, spParameters);
            return surveyData;
        }


        /// <summary>
        /// Clone the given survey and returns its clone
        /// </summary>
        /// <param name="surveyId">Id of the survey you want to clone</param>
        /// <returns>A SurveyData dataset object with the current database values of the clone</returns>
        public SurveyData CloneSurvey(int surveyId)
        {
            SurveyData surveyData = new SurveyData();
           DbConnection.db.LoadDataSet("vts_spSurveyClone", surveyData, new string[] { "Surveys" }, new SqlParameter("@SurveyID", surveyId).SqlValue);
            return surveyData;
        }


        /// <summary>
        /// Get all surveys that have been flaged as archived
        /// </summary>
        public SurveyData GetArchivedSurveys()
        {
            SurveyData surveyData = new SurveyData();
           DbConnection.db.LoadDataSet("vts_spSurveysGetArchive", surveyData, new string[] { "Surveys" });
            return surveyData;
        }

        /// <summary>
        /// Return a survey object that reflects the database survey
        /// </summary>
        /// <param name="surveyId">Id of the survey you need</param>
        /// <returns>A survey object with the current database values</returns>
        public SurveyData GetSurveyById(int surveyId, string languageCode)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);

            //spParameters[0] = new SqlParameter();
            //spParameters[0].ParameterName = "@SurveyId";
            //spParameters[0].SqlDbType = SqlDbType.Variant;
            //spParameters[0].Value = surveyId;


            //spParameters[1] = new SqlParameter("@LanguageCode", languageCode);

            //spParameters[1] = new SqlParameter();
            //spParameters[1].ParameterName = "@LanguageCode";
            //spParameters[1].SqlDbType = SqlDbType.NVarChar;
            //spParameters[1].IsNullable = true;
            //spParameters[1].Value = languageCode ?? (object)DBNull.Value;


            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", Convert.ToInt32(surveyId)).SqlValue);
                sqlParams.Add(new SqlParameter("@LanguageCode", languageCode ?? (object)DBNull.Value).SqlValue);
            }

            SurveyData surveyData = new SurveyData();
           DbConnection.db.LoadDataSet("vts_spSurveyGetDetails", surveyData, new string[] { "Surveys" }, sqlParams.ToArray());
            return surveyData;
        }

        /// <summary>
        /// Check if a survey exists in the database
        /// </summary>
        /// <returns>true if the survey exists</returns>
        public bool SurveyExists(string title)
        {
            //if (db.ExecuteScalar("vts_spSurveyExists", new SqlParameter("@Title", title)) != null)
            if (DbConnection.db.ExecuteScalar("vts_spSurveyExists", new SqlParameter("@Title", title).SqlValue) != null)

                return true;
            else
                return false;
        }

        /// <summary>
        /// Check if the given password is valid for the survey
        /// </summary>
        /// <param name="surveyId">Id of the protected survey</param>
        /// <param name="password">the password to check for</param>
        public bool IsSurveyPasswordValid(int surveyId, string password)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@password", password);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@password", password).SqlValue);
            }


            if (DbConnection.db.ExecuteScalar("vts_spSurveyValidatePassword", sqlParams.ToArray()) != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Return a survey object that reflects the current activated survey. 
        /// IE: The survey that shows up when the surveybox has a surveyid value of 
        /// 0
        /// </summary>
        /// <returns>A survey object with the current database values</returns>
        public SurveyData GetActivatedSurvey()
        {
            SurveyData surveyData = new SurveyData();
           DbConnection.db.LoadDataSet("vts_spSurveyGetActivated", surveyData, new string[] { "Surveys" });
            return surveyData;
        }

        /// <summary>
        /// Get a survey list not assigned to the user
        /// </summary>
        /// <returns></returns>
        public SurveyData GetUnAssignedSurveysList(int userId)
        {
            SurveyData surveyData = new SurveyData();
           DbConnection.db.LoadDataSet("vts_spSurveyGetUnAssignedListForUser", surveyData, new string[] { "Surveys" }, new SqlParameter("@UserId", userId).SqlValue);
            return surveyData;
        }

        /// <summary>
        /// Get a survey list assigned to the user
        /// </summary>
        /// <returns></returns>
        public SurveyData GetAssignedSurveysList(int userId)
        {
            SurveyData surveyData = new SurveyData();

           DbConnection.db.LoadDataSet("vts_spSurveyGetAssignedListForUser", surveyData, new string[] { "Surveys" }, new SqlParameter("@UserId", userId).SqlValue);
            return surveyData;
        }


        /// <summary>
        /// Returns the total of surveys stored in the database
        /// </summary>
        /// <returns>Total of surveys</returns>
        public int TotalOfSurveys()
        {
            return int.Parse(DbConnection.db.ExecuteScalar("vts_spGetSurveyTotal").ToString());
        }


        /// <summary>
        /// Remove the survey and all its question / answers from the database
        /// </summary>
        /// <param name="surveyId">Survey to delete from the database</param>
        public void DeleteSurveyById(int surveyId)
        {
           DbConnection.db.ExecuteNonQuery("vts_spSurveyDeleteById", new SqlParameter("@SurveyID", surveyId).SqlValue);
        }

        /// <summary>
        /// Sets all answers and vote count to 0
        /// </summary>
        /// <param name="surveyId">Survey ID</param>
        public void ResetVotes(int surveyId)
        {
           DbConnection.db.ExecuteNonQuery("vts_spSurveyResetVotes", new SqlParameter("@SurveyID", surveyId).SqlValue);
        }

        /// <summary>
        /// Assigns a new user to a survey
        /// </summary>
        public void AssignUserToSurvey(int surveyId, int userId)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@UserId", userId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@UserId", userId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spUserSurveyAssignUser", sqlParams.ToArray());
        }

        /// <summary>
        /// UnAssigns a user from a survey
        /// </summary>
        public void UnAssignUserFromSurvey(int surveyId, int userId)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@UserId", userId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spUserSurveyUnAssignUser", sqlParams.ToArray());
        }

        /// <summary>
        /// Insert a new page break
        /// </summary>
        /// <param name="surveyId">Survey ID</param>
        /// <param name="displayOrder">Where we will insert the page break</param>
        public void InsertPageBreak(int surveyId, int displayOrder)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@DisplayOrder", displayOrder);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@DisplayOrder", displayOrder).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyInsertPageBreak", sqlParams.ToArray());
        }
        /// <summary>
        /// Move a page break up
        /// </summary>
        /// <param name="surveyId">ID of the survey to which we add the page break</param>
        /// <param name="pageNumber">Page break to move up</param>
        public void MovePageBreakUp(int surveyId, int pageNumber)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@PageNumber", pageNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@PageNumber", pageNumber).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyMovePageBreakUp", sqlParams.ToArray());
        }

        /// <summary>
        /// Move a page break Down
        /// </summary>
        /// <param name="surveyId">ID of the survey to which we add the page break</param>
        /// <param name="pageNumber">Page break to move down</param>
        public void MovePageBreakDown(int surveyId, int pageNumber)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@PageNumber", pageNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@PageNumber", pageNumber).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyMovePageBreakDown", sqlParams.ToArray());
        }

        /// <summary>
        /// Deletes a page break
        /// </summary>
        /// <param name="surveyId">Survey ID</param>
        /// <param name="pageNumber">Page numbe to delete</param>
        public void DeletePageBreak(int surveyId, int pageNumber)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@PageNumber", pageNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@PageNumber", pageNumber).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyDeletePageBreak", sqlParams.ToArray());
        }

        /// <summary>
        /// Retrieves the first survey ID available in the DB
        /// </summary>
        /// <returns>
        /// If found the survey ID else -1
        /// </returns>
        public int GetFirstSurveyId()
        {
            Object scalarValue = DbConnection.db.ExecuteScalar("vts_spSurveyGetFirstID");
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return -1;
            }
            else
            {
                return int.Parse(scalarValue.ToString());
            }
        }

        /// <summary>
        /// Retrieves the first survey ID for user available in the DB
        /// </summary>
        /// <returns>
        /// If found the survey ID else -1
        /// </returns>
        public int GetFirstSurveyId(int userId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyGetFirstIDForUser", new SqlParameter("@UserId", userId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return -1;
            }
            else
            {
                return int.Parse(scalarValue.ToString());
            }
        }

        /// <summary>
        /// Check if the user has this survey assigned
        /// </summary>
        public bool CheckSurveyUser(int surveyId, int userId)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@UserId", userId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyCheckUserAssigned", sqlParams.ToArray());
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns the total of pages in the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public int GetPagesNumber(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyGetPagesNumber", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return 1;
            }
            else
            {
                return int.Parse(scalarValue.ToString());
            }
        }
        /// <summary>
        /// Returns the total of pages in the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public int GetSurveyIdFromGuid(Guid id)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyGetSurveyIdFromGuid", new SqlParameter("@SurveyGuID", id).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return -1;
            }
            else
            {
                return int.Parse(scalarValue.ToString());
            }
        }

        public int GetSurveyIdFromFriendlyName(string name)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyGetSurveyIdFromFriendlyName", new SqlParameter("@FriendlyName", name).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return -1;
            }
            else
            {
                return int.Parse(scalarValue.ToString());
            }
        }

        /// <summary>
        /// Adds a new survey to the database
        /// </summary>
        /// <param name="newSurvey">Survey object with information about what to add. Only Id must be ommited</param>
        public void AddSurvey(SurveyData newSurvey)
        {

            try
            {
                SqlConnection dbConnection = new SqlConnection(DbConnection.NewDbConnectionString);
                SqlCommand addNewSurvey = GetInsertSurveyCommand(dbConnection, null);
               //SqlHelper.UpdateDataset(addNewSurvey, new SqlCommand(), new SqlCommand(), newSurvey, "Surveys", true);
               DbConnection.db.UpdateDataSet(newSurvey, "Surveys", addNewSurvey, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional );
            }
            catch (SqlException ex)
            {
                if (ex.Message == "DUPLICATEFOLDER") throw new SurveyExistsFoundException();
                throw;
            }
        }

        /// <summary>
        /// Creates and return an insert command for a survey
        /// </summary>
        public SqlCommand GetInsertSurveyCommand(SqlConnection sqlConnection,
            SqlTransaction sqlTransaction)
        {
            SqlCommand addNewSurvey = (sqlTransaction == null) ?
                new SqlCommand("vts_spSurveyAddNew", sqlConnection) : new SqlCommand("vts_spSurveyAddNew", sqlConnection, sqlTransaction);
            addNewSurvey.CommandType = CommandType.StoredProcedure;
            addNewSurvey.Parameters.Add(new SqlParameter("@CreationDate", DateTime.Now));
            addNewSurvey.Parameters.Add(new SqlParameter("@OpenDate", SqlDbType.DateTime, 8, "OpenDate"));
            addNewSurvey.Parameters.Add(new SqlParameter("@CloseDate", SqlDbType.DateTime, 8, "CloseDate"));
            addNewSurvey.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 255, "Title"));
            addNewSurvey.Parameters.Add(new SqlParameter("@AccessPassword", SqlDbType.VarChar, 255, "AccessPassword"));
            addNewSurvey.Parameters.Add(new SqlParameter("@SurveyDisplayTimes", SqlDbType.Int, 4, "SurveyDisplayTimes"));
            addNewSurvey.Parameters.Add(new SqlParameter("@ResultsDisplayTimes", SqlDbType.Int, 4, "ResultsDisplayTime"));
            addNewSurvey.Parameters.Add(new SqlParameter("@Archive", SqlDbType.Bit, 1, "Archive"));
            addNewSurvey.Parameters.Add(new SqlParameter("@Activated", SqlDbType.Bit, 1, "Activated"));
            addNewSurvey.Parameters.Add(new SqlParameter("@IPExpires", SqlDbType.Int, 4, "IPExpires"));
            addNewSurvey.Parameters.Add(new SqlParameter("@CookieExpires", SqlDbType.Int, 4, "CookieExpires"));
            addNewSurvey.Parameters.Add(new SqlParameter("@OnlyInvited", SqlDbType.Bit, 1, "OnlyInvited"));
            addNewSurvey.Parameters.Add(new SqlParameter("@Scored", SqlDbType.Bit, 1, "Scored"));
            addNewSurvey.Parameters.Add(new SqlParameter("@NavigationEnabled", SqlDbType.Bit, 1, "NavigationEnabled"));
            addNewSurvey.Parameters.Add(new SqlParameter("@ProgressDisplayModeId", SqlDbType.Int, 4, "ProgressDisplayModeId"));
            addNewSurvey.Parameters.Add(new SqlParameter("@ResumeModeId", SqlDbType.Int, 4, "ResumeModeId"));
            addNewSurvey.Parameters.Add(new SqlParameter("@QuestionNumberingDisabled", SqlDbType.Bit, 1, "QuestionNumberingDisabled"));
            addNewSurvey.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int, 4, "SurveyID"));
            addNewSurvey.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int, 4, "FolderId"));
            addNewSurvey.Parameters["@SurveyID"].Direction = ParameterDirection.Output;

            return addNewSurvey;
        }


        /// <summary>
        /// Add a new branching rule to the survey
        /// </summary>
        public void AddBranchingRule(BranchingRuleData newBranchingRule)
        {

            SqlConnection dbConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand addNewRule = new SqlCommand("vts_spSurveyBranchingRuleAddNew", dbConnection);
            addNewRule.CommandType = CommandType.StoredProcedure;
            addNewRule.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int, 4, "PageNumber"));
            addNewRule.Parameters.Add(new SqlParameter("@ExpressionOperator", SqlDbType.Int, 4, "ExpressionOperator"));
            addNewRule.Parameters.Add(new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID"));
            addNewRule.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            addNewRule.Parameters.Add(new SqlParameter("@TargetPageNumber", SqlDbType.Int, 4, "TargetPageNumber"));
            addNewRule.Parameters.Add(new SqlParameter("@TextFilter", SqlDbType.NVarChar, 4000, "TextFilter"));
            addNewRule.Parameters.Add(new SqlParameter("@ConditionalOperator", SqlDbType.Int, 4, "ConditionalOperator"));
            addNewRule.Parameters.Add(new SqlParameter("@Score", SqlDbType.Int, 4, "Score"));
            addNewRule.Parameters.Add(new SqlParameter("@ScoreMax", SqlDbType.Int, 4, "ScoreMax"));
            addNewRule.Parameters.Add(new SqlParameter("@BranchingRuleID", SqlDbType.Int, 4, "BranchingRuleID"));
            addNewRule.Parameters["@BranchingRuleID"].Direction = ParameterDirection.Output;

            //SqlHelper.UpdateDataset(addNewRule, new SqlCommand(), new SqlCommand(), newBranchingRule, "BranchingRules", true);
           DbConnection.db.UpdateDataSet(newBranchingRule, "BranchingRules", addNewRule, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Add a new condition to show a given thank you message
        /// </summary>
        public void AddMessageCondition(MessageConditionData newMessageCondition)
        {
            SqlConnection dbConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand addNewCondition = new SqlCommand("vts_spSurveyMessageConditionAddNew", dbConnection);
            addNewCondition.CommandType = CommandType.StoredProcedure;
            addNewCondition.Parameters.Add(new SqlParameter("@SurveyId", SqlDbType.Int, 4, "SurveyId"));
            addNewCondition.Parameters.Add(new SqlParameter("@MessageConditionalOperator", SqlDbType.Int, 4, "MessageConditionalOperator"));
            addNewCondition.Parameters.Add(new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID"));
            addNewCondition.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            addNewCondition.Parameters.Add(new SqlParameter("@TextFilter", SqlDbType.NVarChar, 4000, "TextFilter"));
            addNewCondition.Parameters.Add(new SqlParameter("@ThankYoumessage", SqlDbType.NVarChar, 4000, "ThankYoumessage"));
            addNewCondition.Parameters.Add(new SqlParameter("@ConditionalOperator", SqlDbType.Int, 4, "ConditionalOperator"));
            addNewCondition.Parameters.Add(new SqlParameter("@ExpressionOperator", SqlDbType.Int, 4, "ExpressionOperator"));
            addNewCondition.Parameters.Add(new SqlParameter("@Score", SqlDbType.Int, 4, "Score"));
            addNewCondition.Parameters.Add(new SqlParameter("@ScoreMax", SqlDbType.Int, 4, "ScoreMax"));
            addNewCondition.Parameters.Add(new SqlParameter("@MessageConditionId", SqlDbType.Int, 4, "MessageConditionId"));
            addNewCondition.Parameters["@MessageConditionId"].Direction = ParameterDirection.Output;

           DbConnection.db.UpdateDataSet(newMessageCondition, "MessageConditions", addNewCondition, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Updates a message condition
        /// </summary>
        public void UpdateMessageCondition(MessageConditionData updatedMessageCondition)
        {
            SqlConnection dbConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand updateCondition = new SqlCommand("vts_spSurveyMessageConditionUpdate", dbConnection);
            updateCondition.CommandType = CommandType.StoredProcedure;
            updateCondition.Parameters.Add(new SqlParameter("@MessageConditionID", SqlDbType.Int, 4, "MessageConditionId"));
            updateCondition.Parameters.Add(new SqlParameter("@MessageConditionalOperator", SqlDbType.Int, 4, "MessageConditionalOperator"));
            updateCondition.Parameters.Add(new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID"));
            updateCondition.Parameters.Add(new SqlParameter("@AnswerID", SqlDbType.Int, 4, "AnswerID"));
            updateCondition.Parameters.Add(new SqlParameter("@TextFilter", SqlDbType.NVarChar, 4000, "TextFilter"));
            updateCondition.Parameters.Add(new SqlParameter("@ThankYoumessage", SqlDbType.NVarChar, 4000, "ThankYoumessage"));
            updateCondition.Parameters.Add(new SqlParameter("@ConditionalOperator", SqlDbType.Int, 4, "ConditionalOperator"));
            updateCondition.Parameters.Add(new SqlParameter("@ExpressionOperator", SqlDbType.Int, 4, "ExpressionOperator"));
            updateCondition.Parameters.Add(new SqlParameter("@Score", SqlDbType.Int, 4, "Score"));
            updateCondition.Parameters.Add(new SqlParameter("@ScoreMax", SqlDbType.Int, 4, "ScoreMax"));

           DbConnection.db.UpdateDataSet(updatedMessageCondition, "MessageConditions", updateCondition, new SqlCommand(), updateCondition, UpdateBehavior.Transactional);
        }

        /// <summary>
        /// returns the thank you message contions for the given survey
        /// </summary>
        public MessageConditionData GetSurveyMessageConditions(int surveyId)
        {
            MessageConditionData messageCondition = new MessageConditionData();
           DbConnection.db.LoadDataSet("vts_spSurveyMessageConditionsGetAll", messageCondition, new string[] { "MessageConditions" }, new SqlParameter("@SurveyID", surveyId).SqlValue);
            return messageCondition;
        }

        /// <summary>
        /// Deletes the message condition
        /// </summary>
        public void DeleteMessageConditionById(int messageConditionId)
        {
           DbConnection.db.ExecuteNonQuery("vts_spSurveyMessageConditionDeleteByID", new SqlParameter("@MessageConditionID", messageConditionId).SqlValue);
        }

        /// <summary>
        /// Increment the number of time a survey has been viewed
        /// </summary>
        /// <param name="surveyId">Id of the survey to increment views</param>
        /// <param name="incrementNumber">number of views to add</param>
        public void IncrementSurveyViews(int surveyId, int incrementNumber)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@CountNumber", incrementNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@CountNumber", incrementNumber).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyIncrementViews", sqlParams.ToArray());
        }

        /// <summary>
        /// Increment the number of time the survey's result has been viewed
        /// </summary>
        /// <param name="surveyId">Id of the survey to increment result view</param>
        /// <param name="incrementNumber">number of views to add</param>
        public void IncrementResultsViews(int surveyId, int incrementNumber)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@CountNumber", incrementNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@CountNumber", incrementNumber).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spSurveyIncrementResultsViews", sqlParams.ToArray());
        }

        /// <summary>
        /// returns the branching rules for the given page of the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public BranchingRuleData GetSurveyPageBranchingRules(int surveyId, int pageNumber)
        {
            BranchingRuleData branchingRule = new BranchingRuleData();

            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@pageNumber", pageNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@pageNumber", pageNumber).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spSurveyBranchingRuleGetForPage", branchingRule, new string[] { "BranchingRules" }, sqlParams.ToArray());

            return branchingRule;
        }

        /// <summary>
        /// returns the branching rules for the given page with all
        /// their details
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public BranchingRuleData GetSurveyPageBranchingRulesDetails(int surveyId, int pageNumber)
        {
            BranchingRuleData branchingRule = new BranchingRuleData();

            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@pageNumber", pageNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@pageNumber", pageNumber).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spSurveyBranchingRuleGetDetailsForPage", branchingRule, new string[] { "BranchingRules" }, sqlParams.ToArray());

            return branchingRule;

        }

        /// <summary>
        /// returns the branching rules for the given survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public BranchingRuleData GetSurveyBranchingRules(int surveyId)
        {
            BranchingRuleData branchingRule = new BranchingRuleData();
           DbConnection.db.LoadDataSet("vts_spSurveyBranchingRuleGetAll", branchingRule, new string[] { "BranchingRules" }, new SqlParameter("@SurveyID", surveyId).SqlValue);
            return branchingRule;
        }

        /// <summary>
        /// Deletes the branching rules 
        /// </summary>
        public void DeleteBranchingRuleById(int branchingRuleId)
        {
           DbConnection.db.ExecuteNonQuery("vts_spSurveyBranchingRuleDeleteByID", new SqlParameter("@BranchingRuleID", branchingRuleId).SqlValue);
        }

        /// <summary>
        /// Update the database with the given survey
        /// </summary>
        /// <param name="updatedSurvey">survey to update, must contain the surveyid</param>
        public void UpdateSurvey(SurveyData updatedSurvey, string languageCode)
        {
            try
            {
                SqlConnection dbConnection = new SqlConnection(DbConnection.NewDbConnectionString);
                SqlCommand updateSurvey = new SqlCommand("vts_spSurveyUpdate", dbConnection);
                updateSurvey.CommandType = CommandType.StoredProcedure;

                updateSurvey.Parameters.AddWithValue("@LanguageCode", languageCode);
                updateSurvey.Parameters.Add("@SurveyID", SqlDbType.Int, 4, "SurveyID");
                updateSurvey.Parameters.Add(new SqlParameter("@OpenDate", SqlDbType.DateTime, 8, "OpenDate"));
                updateSurvey.Parameters.Add(new SqlParameter("@CloseDate", SqlDbType.DateTime, 8, "CloseDate"));
                updateSurvey.Parameters.Add("@Title", SqlDbType.NVarChar, 255, "Title");
                updateSurvey.Parameters.Add("@ThankYouMessage", SqlDbType.NVarChar, 4000, "ThankYouMessage");
                updateSurvey.Parameters.Add("@RedirectionURL", SqlDbType.NVarChar, 1024, "RedirectionURL");
                updateSurvey.Parameters.Add("@Archive", SqlDbType.Bit, 1, "Archive");
                updateSurvey.Parameters.Add("@Activated", SqlDbType.Bit, 1, "Activated");
                updateSurvey.Parameters.Add("@ResumeModeID", SqlDbType.Int, 1, "ResumeModeID");
                updateSurvey.Parameters.Add("@NavigationEnabled", SqlDbType.Bit, 1, "NavigationEnabled");
                updateSurvey.Parameters.Add("@ProgressDisplayModeId", SqlDbType.Int, 4, "ProgressDisplayModeId");
                updateSurvey.Parameters.Add("@NotificationModeId", SqlDbType.Int, 4, "NotificationModeId");
                updateSurvey.Parameters.Add("@Scored", SqlDbType.Bit, 1, "Scored");
                updateSurvey.Parameters.Add("@QuestionNumberingDisabled", SqlDbType.Bit, 1, "QuestionNumberingDisabled");
                updateSurvey.Parameters.Add("@DefaultSurvey", SqlDbType.Bit, 1, "DefaultSurvey");

                DbConnection.db.UpdateDataSet(updatedSurvey, "Surveys", updateSurvey, updateSurvey, new SqlCommand(), UpdateBehavior.Transactional);
            }
            catch (SqlException ex)
            {
                if (ex.Message == "DUPLICATEFOLDER") throw new SurveyExistsFoundException();
                throw;
            }

            if ((NotificationMode)updatedSurvey.Surveys[0].NotificationModeID != NotificationMode.None)
            {
                //SqlParameter[] spParameters = new SqlParameter[4];
                //spParameters[0] = new SqlParameter("@SurveyId", updatedSurvey.Surveys[0].SurveyId);
                //spParameters[1] = new SqlParameter("@EmailFrom", updatedSurvey.Surveys[0].EmailFrom);
                //spParameters[2] = new SqlParameter("@EmailTo", updatedSurvey.Surveys[0].EmailTo);
                //spParameters[3] = new SqlParameter("@EmailSubject", updatedSurvey.Surveys[0].EmailSubject);

                ArrayList sqlParams = new ArrayList();
                {
                    sqlParams.Add(new SqlParameter("@SurveyId", updatedSurvey.Surveys[0].SurveyId).SqlValue);
                    sqlParams.Add(new SqlParameter("@EmailFrom", updatedSurvey.Surveys[0].EmailFrom).SqlValue);
                    sqlParams.Add(new SqlParameter("@EmailTo", updatedSurvey.Surveys[0].EmailTo).SqlValue);
                    sqlParams.Add(new SqlParameter("@EmailSubject", updatedSurvey.Surveys[0].EmailSubject).SqlValue);
                }

               DbConnection.db.ExecuteNonQuery("vts_spEmailNotificationSettingsAddNew", sqlParams.ToArray());
            }
            else
            {
                //SqlParameter[] spParameters = new SqlParameter[4];
                //spParameters[0] = new SqlParameter("@SurveyId", updatedSurvey.Surveys[0].SurveyId);

                ArrayList sqlParams = new ArrayList();
                {
                    sqlParams.Add(new SqlParameter("@SurveyId", updatedSurvey.Surveys[0].SurveyId).SqlValue);
                }

                DbConnection.db.ExecuteNonQuery("vts_spEmailNotificationSettingsDelete", sqlParams.ToArray());
            }
        }

        /// <summary>
        /// Get the number of minutes after which the security cookie 
        /// expires
        /// </summary>
        public int GetCookieExpiration(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyGetCookieExpiration", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return int.Parse(scalarValue.ToString());
            }
        }

        /// <summary>
        /// Update the cookie expiration time
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="cookieExpires"></param>
        public void UpdateCookieExpiration(int surveyId, int cookieExpires)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@CookieExpires", cookieExpires);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@CookieExpires", cookieExpires).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyCookieExpirationUpdate", sqlParams.ToArray());
        }

        /// <summary>
        /// Update the ip expiration time
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="ipExpires"></param>
        public void UpdateIPExpiration(int surveyId, int ipExpires)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@IPExpires", ipExpires);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@IPExpires", ipExpires).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spSurveyIPExpirationUpdate", sqlParams.ToArray());
        }

        /// <summary>
        /// Update the survey access password
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="accessPassword"></param>
        public void UpdateAccessPassword(int surveyId, string accessPassword)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@AccessPassword", accessPassword);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@AccessPassword", accessPassword).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyAccessPasswordUpdate", sqlParams.ToArray());
        }

        /// <summary>
        /// Get the number of minutes after which the security IP 
        /// expires
        /// </summary>
        public int GetIPExpiration(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyIPExpirationGet", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return int.Parse(scalarValue.ToString());
            }
        }


        /// <summary>
        /// Get the access password of the survey
        /// </summary>
        public string GetSurveyPassword(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyAccessPasswordGet", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return null;
            }
            else
            {
                return scalarValue.ToString();
            }
        }

        /// <summary>
        /// Update the survey only invited status
        /// </summary>
        public void UpdateOnlyInvited(int surveyId, bool onlyInvited)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@OnlyInvited", onlyInvited);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@OnlyInvited", onlyInvited).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyOnlyInvitedUpdate", sqlParams.ToArray());
        }

        /// <summary>
        /// Update the survey only invited status
        /// </summary>
        public void UpdateSaveTokenUserData(int surveyId, bool saveData)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@SaveData", saveData);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@SaveData", saveData).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveySaveTokenUserDataUpdate", sqlParams.ToArray());
        }

        /// <summary>
        /// Get the invited status of the survey
        /// </summary>
        public bool IsSurveyEmailInviteOnly(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyOnlyInvitedGet", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(scalarValue);
            }
        }

        /// <summary>
        /// Get Save Token User status of the survey
        /// </summary>
        public bool IsSurveySaveTokenUserData(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveySaveTokenUserDataGet", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(scalarValue);
            }
        }

        /// <summary>
        /// Updates the survey to a new unAuthentified user action
        /// </summary>
        /// <returns></returns>
        public void UpdateUnAuthentifiedUserActions(int surveyId, int unAuthentifiedUserActionId)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@UnAuthentifiedUserActionId", unAuthentifiedUserActionId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@UnAuthentifiedUserActionId", unAuthentifiedUserActionId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyUpdateUnAuthentifiedUserAction", sqlParams.ToArray());
        }

        /// <summary>
        /// Get the actions that is set for the given survey
        /// </summary>
        public int GetSurveyUnAuthentifiedUserAction(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyGetUnAuthentifiedUserAction", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return -1;
            }
            else
            {
                return int.Parse(scalarValue.ToString());
            }
        }

        /// <summary>
        /// Returns true if the page has any branching rule
        /// associated with it
        /// </summary>
        public bool HasPageBranching(int surveyId, int pageNumber)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyID", surveyId);
            //spParameters[1] = new SqlParameter("@pageNumber", pageNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@pageNumber", pageNumber).SqlValue);
            }

            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyHasPageBranching", sqlParams.ToArray());
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns a list of available progress modes
        /// </summary>
        public ProgressModeData GetSurveyProgressModes()
        {
            ProgressModeData progressModeData = new ProgressModeData();
           DbConnection.db.LoadDataSet("vts_spProgressModeGetAll", progressModeData, new string[] { "ProgressDisplayModes" });
            return progressModeData;
        }

        /// <summary>
        /// Returns a list of available email notification mode
        /// </summary>
        public NotificationModeData GetSurveyNotificationModes()
        {
            NotificationModeData notificationModeData = new NotificationModeData();
           DbConnection.db.LoadDataSet("vts_spNotificationModeGetAll", notificationModeData, new string[] { "NotificationModes" });
            return notificationModeData;
        }

        /// <summary>
        /// Retrieves the options that were setup for the page
        /// </summary>
        public PageOptionData GetSurveyPageOptions(int surveyId, int pageNumber)
        {
            PageOptionData pageOptions = new PageOptionData();

            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@pageNumber", pageNumber);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@pageNumber", pageNumber).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spPageOptionGetDetails", pageOptions, new string[] { "PageOptions" }, sqlParams.ToArray());
            return pageOptions;
        }

        /// <summary>
        /// Update the options that were setup for the page
        /// </summary>
        public void UpdateSurveyPageOptions(PageOptionData updatedPageOptions)
        {
            SqlConnection dbConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand updatePageOptions = new SqlCommand("vts_spPageOptionUpdate", dbConnection);
            updatePageOptions.CommandType = CommandType.StoredProcedure;
            updatePageOptions.Parameters.Add("@SurveyID", SqlDbType.Int, 4, "SurveyID");
            updatePageOptions.Parameters.Add("@PageNumber", SqlDbType.Int, 4, "PageNumber");
            updatePageOptions.Parameters.Add("@RandomizeQuestions", SqlDbType.Bit, 1, "RandomizeQuestions");
            updatePageOptions.Parameters.Add("@EnableSubmitButton", SqlDbType.Bit, 1, "EnableSubmitButton");
          DbConnection.db.UpdateDataSet(updatedPageOptions, "PageOptions", updatePageOptions, new SqlCommand(), updatePageOptions, UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Returns a list of available resume modes
        /// </summary>
        public ResumeModeData GetSurveyResumeModes()
        {
            ResumeModeData resumeModeData = new ResumeModeData();
           DbConnection.db.LoadDataSet("vts_spResumeModeGetAll", resumeModeData, new string[] { "ResumeModes" });
            return resumeModeData;
        }

        /// <summary>
        /// returns the thank you message conditions for the given survey
        /// </summary>
        public MessageConditionData GetSurveyMessageCondition(int messageConditionId)
        {
            MessageConditionData messageCondition = new MessageConditionData();
           DbConnection.db.LoadDataSet("vts_spSurveyMessageConditionsGetDetails", messageCondition, new string[] { "MessageConditions" }, new SqlParameter("@MessageConditionId", messageConditionId).SqlValue);
            return messageCondition;
        }

        /// <summary>
        /// Check if survey has been set to be scored
        /// </summary>
        public bool IsSurveyScored(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyIsScored", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(scalarValue);
            }
        }

        /// <summary>
        ///	Check if the survey allows multiple submission
        ///	for the same username 
        /// </summary>
        public bool AspSecurityAllowsMultipleSubmissions(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyAllowMultipleASPNetVotes", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(scalarValue);
            }
        }

        /// <summary>
        /// Update the security settings
        /// </summary>
        public void UpdateAspSecuritySettings(int surveyId, bool allowMultipleSubmissions)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@AllowMultipleSubmissions", allowMultipleSubmissions);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@AllowMultipleSubmissions", allowMultipleSubmissions).SqlValue);
            }

           DbConnection.db.ExecuteNonQuery("vts_spSurveyUpdateASPNetSubmissions", sqlParams.ToArray());
        }

        /// <summary>
        ///	Check if the survey allows multiple submission
        ///	for the same nsurvey username 
        /// </summary>
        public bool NSurveyAllowsMultipleSubmissions(int surveyId)
        {
            Object scalarValue =DbConnection.db.ExecuteScalar("vts_spSurveyAllowMultipleNSurveyVotes", new SqlParameter("@SurveyID", surveyId).SqlValue);
            if (scalarValue == null || scalarValue == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(scalarValue);
            }
        }

        /// <summary>
        /// Update the nsurvey security settings
        /// </summary>
        public void UpdateNSurveySecuritySettings(int surveyId, bool allowMultipleNSurveySubmissions)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@allowMultipleNSurveySubmissions", allowMultipleNSurveySubmissions);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@allowMultipleNSurveySubmissions", allowMultipleNSurveySubmissions).SqlValue);
            }

           DbConnection.db.ExecuteNonQuery("vts_spSurveyUpdateNSurveySubmissions", sqlParams.ToArray());
        }

        /// <summary>
        /// Returns all the data need to handle 
        /// and process question / answer piping
        /// </summary>
        public PipeData GetSurveyPipeDataFromQuestionId(int questionId)
        {
            PipeData pipeData = new PipeData();
           DbConnection.db.LoadDataSet("vts_spSurveyGetPipeDataFromQuestionId", pipeData, new string[] { "Questions", "Answers" }, new SqlParameter("@QuestionId", questionId).SqlValue);
            return pipeData;
        }

        /// <summary>
        /// Returns all the survey, questions, answers
        /// for a survey
        /// </summary>
        public NSurveyForm GetFormForExport(int surveyId)
        {
            NSurveyForm surveyForm = new NSurveyForm();
           DbConnection.db.LoadDataSet("vts_spSurveyGetForExport", surveyForm, new string[] { "AnswerType", "RegularExpression", "Survey", "Question", "Answer", "AnswerConnection", "ChildQuestion", "AnswerProperty", "QuestionSectionOption", "QuestionSectionGridAnswer", "Surveylanguage", "MultiLanguageText", "QuestionGroup" }, new SqlParameter("@SurveyId", surveyId).SqlValue);
            return surveyForm;
        }

        /// <summary>
        /// Import the given surveys into the DB
        /// </summary>
        public void ImportSurveys(NSurveyForm importSurveys, int userId, int folderId)
        {
            // Prepare connection and transaction
            SqlConnection dbConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            dbConnection.Open();
            SqlTransaction insertTransaction = dbConnection.BeginTransaction();


            // Setup the insert commands for the survey, answer types, questions and answers
            SqlCommand addNewAnswerType = new AnswerType().GetInsertAnswerTypeCommand(dbConnection, insertTransaction, userId);
            SqlCommand addNewRegularExpression = new RegularExpression().GetInsertRegularExpressionCommand(dbConnection, insertTransaction, userId);
            SqlCommand addNewSurvey = GetInsertSurveyCommand(dbConnection, insertTransaction);
            SqlCommand addNewQuestion = new Question().GetInsertQuestionCommand(dbConnection, insertTransaction);
            SqlCommand addNewChildQuestion = new Question().GetInsertChildQuestionCommand(dbConnection, insertTransaction);
            SqlCommand addNewAnswer = new Answer().GetInsertAnswerCommand(dbConnection, insertTransaction);
            SqlCommand addNewAnswerConnection = new Answer().GetInsertAnswerConnectionCommand(dbConnection, insertTransaction);
            SqlCommand addNewAnswerProperty = new Answer().GetInsertAnswerPropertyCommand(dbConnection, insertTransaction);
            SqlCommand addNewQuestionSection = new Question().GetInsertQuestionSectionCommand(dbConnection, insertTransaction, "");
            SqlCommand addNewQuestionSectionGridAnswers = new Question().GetInsertQuestionSectionGridAnswersCommand(dbConnection, insertTransaction);

            // Save the data in the DB
            try
            {
                importSurveys.Survey[0].FolderId = folderId;
                addNewSurvey.Parameters.Add(new SqlParameter("@MultiLanguageModeId", SqlDbType.Int, 4, "MultiLanguageModeId"));
                addNewSurvey.Parameters.Add(new SqlParameter("@ThankYouMessage", SqlDbType.NVarChar, 4000, "ThankYouMessage"));
               DbConnection.db.UpdateDataSet(importSurveys, "AnswerType", addNewAnswerType, new SqlCommand(), addNewAnswerType, UpdateBehavior.Transactional);
               DbConnection.db.UpdateDataSet(importSurveys, "RegularExpression", addNewRegularExpression, new SqlCommand(), addNewRegularExpression, UpdateBehavior.Transactional);
               DbConnection.db.UpdateDataSet(importSurveys, "Survey", addNewSurvey, new SqlCommand(), addNewSurvey, UpdateBehavior.Transactional);

                // Add Question groups so we can attach them to questions
                var groups = new QuestionGroups();
                var defl = importSurveys.SurveyLanguage.SingleOrDefault(x => x.DefaultLanguage);
                string defaultLang = defl == null ? null : defl.LanguageCode;
                defaultLang = defaultLang ?? "en-US";
                if (defaultLang != null) //Load groups only if default language is not null
                {
                    var existingGroups = groups.GetAll(defaultLang).QuestionGroups;
                    foreach (var qgrp in importSurveys.QuestionGroup
                        .OrderBy(x => x.IsParentGroupIdNull() ? 0 : 1)) //Load parent groups first
                    {
                        var grpHere = existingGroups.FirstOrDefault(x => x.GroupName == qgrp.GroupName);

                        int groupIdHere;
                        if (grpHere == null)
                        {
                            int? parentGroupId = null;
                            if (!qgrp.IsParentGroupIdNull()) //Has Parent Group
                            {
                                var pgrp = importSurveys.QuestionGroup.SingleOrDefault(x => x.Id == qgrp.ParentGroupId);
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
                        importSurveys.Question
                              .Where(x =>!x.IsQuestionGroupIdNull() && (x.QuestionGroupId == qgrp.OldId))
                              .ToList().ForEach(x => x.QuestionGroupId = groupIdHere);

                    }
                }

               DbConnection.db.UpdateDataSet(importSurveys, "Question", addNewQuestion, new SqlCommand(), addNewQuestion, UpdateBehavior.Transactional);
               DbConnection.db.UpdateDataSet(importSurveys, "Answer", addNewAnswer, new SqlCommand(), addNewAnswer, UpdateBehavior.Transactional);
               DbConnection.db.UpdateDataSet(importSurveys, "AnswerConnection", addNewAnswerConnection, new SqlCommand(), addNewAnswerConnection, UpdateBehavior.Transactional);
               DbConnection.db.UpdateDataSet(importSurveys, "ChildQuestion", addNewChildQuestion, new SqlCommand(), addNewChildQuestion, UpdateBehavior.Transactional);
               DbConnection.db.UpdateDataSet(importSurveys, "AnswerProperty", addNewAnswerProperty, new SqlCommand(), addNewAnswerProperty, UpdateBehavior.Transactional);
               DbConnection.db.UpdateDataSet(importSurveys, "QuestionSectionOption", addNewQuestionSection, new SqlCommand(), addNewQuestionSection, UpdateBehavior.Transactional);
                int newSurveyId = importSurveys.Survey[0].SurveyID;

               DbConnection.db.UpdateDataSet(importSurveys, "QuestionSectionGridAnswer", addNewQuestionSectionGridAnswers, new SqlCommand(), addNewQuestionSectionGridAnswers, UpdateBehavior.Transactional);
                insertTransaction.Commit();
                var multiLanguage = new MultiLanguage();
                foreach (var lang in importSurveys.SurveyLanguage)
                    multiLanguage.UpdateSurveyLanguage(newSurveyId, lang.LanguageCode, lang.DefaultLanguage, Constants.Constants.EntitySurvey);

                var localGroups = groups.GetAll(defaultLang).QuestionGroups;
                foreach (var langText in importSurveys.MultiLanguageText)
                {
                    //Process Survey level
                    if (langText.LanguageMessageTypeId == 10)
                    {
                        var impGrp = importSurveys.QuestionGroup.SingleOrDefault(x => x.OldId == langText.LanguageItemId);
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

                    if (langText.LanguageMessageTypeId == 4 || langText.LanguageMessageTypeId == 5)
                        multiLanguage.AddMultiLanguageText(newSurveyId, langText.LanguageCode, langText.LanguageMessageTypeId, langText.ItemText);

                    if (langText.LanguageMessageTypeId == 3 || langText.LanguageMessageTypeId == 11 || langText.LanguageMessageTypeId == 12)
                        multiLanguage.AddMultiLanguageText(importSurveys.Question.Single(x => x.OldQuestionId == langText.LanguageItemId).QuestionId
                            , langText.LanguageCode, langText.LanguageMessageTypeId, langText.ItemText);

                    if (langText.LanguageMessageTypeId == 1 || langText.LanguageMessageTypeId == 2 || langText.LanguageMessageTypeId == 13)
                        multiLanguage.AddMultiLanguageText(importSurveys.Answer.Single(x => x.OldAnswerId == langText.LanguageItemId).AnswerId
                            , langText.LanguageCode, langText.LanguageMessageTypeId, langText.ItemText);

                }


                dbConnection.Close();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        /// <summary>
        /// Increase the current entries number calculated
        /// against the max. quota
        /// </summary>
        public void IncreaseQuotaEntries(int surveyId)
        {
           DbConnection.db.ExecuteNonQuery("vts_spSurveyEntryQuotaIncreaseEntry", new SqlParameter("@SurveyId", surveyId).SqlValue);
        }

        /// <summary>
        /// Resets the current entries number calculated
        /// against the max. quota
        /// </summary>
        public void ResetQuotaEntries(int surveyId)
        {
           DbConnection.db.ExecuteNonQuery("vts_spSurveyEntryQuotaReset", new SqlParameter("@SurveyId", surveyId).SqlValue);
        }

        /// <summary>
        /// Deletes survey's quotas
        /// </summary>
        public void DeleteQuotaSettings(int surveyId)
        {
           DbConnection.db.ExecuteNonQuery("vts_spSurveyEntryDelete", new SqlParameter("@SurveyId", surveyId).SqlValue);
        }

        /// <summary>
        /// Retrieves the quota of the survey
        /// </summary>
        public SurveyEntryQuotaData GetQuotaSettings(int surveyId)
        {
            SurveyEntryQuotaData entryQuota = new SurveyEntryQuotaData();
           DbConnection.db.LoadDataSet("vts_spSurveyEntryQuotaGetDetails", entryQuota, new string[] { "SurveyEntryQuotas" }, new SqlParameter("@SurveyId", surveyId).SqlValue);
            return entryQuota;
        }

        /// <summary>
        /// Updates or creates if it doesnt exists 
        /// quota settings for the survey
        /// </summary>
        public void UpdateQuotaSettings(SurveyEntryQuotaData surveyQuota)
        {
            SqlConnection dbConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand updateQuota = new SqlCommand("vts_spSurveyEntryQuotaUpdate", dbConnection);
            updateQuota.CommandType = CommandType.StoredProcedure;
            updateQuota.Parameters.Add(new SqlParameter("@SurveyId", SqlDbType.Int, 4, "SurveyId"));
            updateQuota.Parameters.Add(new SqlParameter("@MaxReachedMessage", SqlDbType.NVarChar, 4000, "MaxReachedMessage"));
            updateQuota.Parameters.Add(new SqlParameter("@MaxEntries", SqlDbType.Int, 4, "MaxEntries"));
           DbConnection.db.UpdateDataSet(surveyQuota, "SurveyEntryQuotas", updateQuota, new SqlCommand(), updateQuota, UpdateBehavior.Transactional);
        }


        public void SetFolderId(int? folderId, int surveyId)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@FolderId", folderId);
            //spParameters[1] = new SqlParameter("@SurveyId", surveyId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@FolderId", folderId).SqlValue);
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

           DbConnection.db.ExecuteNonQuery("vts_spSurveySetFolder", sqlParams.ToArray());
        }

        /// <summary>
        /// Disable survey's multi language features
        /// </summary>
        /// <param name="surveyId"></param>
        public void SetFriendlyName(int surveyId, string friendlyName)
        {
            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                sqlParams.Add(new SqlParameter("@FriendlyName", friendlyName).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyUpdateFriendlyName", sqlParams.ToArray());
        }
    }
}
