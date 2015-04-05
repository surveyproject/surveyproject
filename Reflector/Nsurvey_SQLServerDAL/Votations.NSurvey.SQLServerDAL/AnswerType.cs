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
    using System.Data.SqlClient;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class AnswerType : IAnswerType
    {
        /// <summary>
        /// Adds a new answer type in the database
        /// </summary>
        /// <param name="newAnswerType">Answer type object with information about what to add. Only Id must be ommited</param>
        public void AddAnswerType(AnswerTypeData newAnswerType, int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            DbConnection.db.UpdateDataSet(newAnswerType, "AnswerTypes", this.GetInsertAnswerTypeCommand(sqlConnection, null, userId), new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Assign the answer type to the user
        /// </summary>
        public void AssignAnswerTypeToUser(int answerTypeId, int userId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@AnswerTypeId", answerTypeId), 
            //    new SqlParameter("@UserId", userId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@AnswerTypeId", answerTypeId).SqlValue);
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spUserAnswerTypeAssignUser", commandParameters.ToArray());
        }

        /// <summary>
        /// Remove the answer type from the database
        /// </summary>
        /// <param name="answerTypeId">Answer type to delete from the database</param>
        public void DeleteAnswerType(int answerTypeId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@AnswerTypeId", answerTypeId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerTypeDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Return an answer type object that reflects the database answer type
        /// </summary>
        /// <param name="answerTypeId">Id of the answer type you need</param>
        /// <returns>An answer type object with the current database values</returns>
        public AnswerTypeData GetAnswerTypeById(int answerTypeId)
        {
            AnswerTypeData dataSet = new AnswerTypeData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@AnswerTypeId", answerTypeId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spAnswerTypeGetDetails", dataSet, new string[] { "AnswerTypes" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns all the answer type available
        /// </summary>
        public AnswerTypeData GetAnswerTypes()
        {
            AnswerTypeData dataSet = new AnswerTypeData();            
            DbConnection.db.LoadDataSet("vts_spAnswerTypeGetAll", dataSet, new string[] { "AnswerTypes" });
            return dataSet;
        }

        /// <summary>
        /// Returns a list of answer types list
        /// </summary>
        public AnswerTypeData GetAnswerTypesList()
        {
            AnswerTypeData dataSet = new AnswerTypeData();
            DbConnection.db.LoadDataSet("vts_spAnswerTypeGetList", dataSet, new string[] { "AnswerTypes" });
            return dataSet;
        }

        /// <summary>
        /// Returns a list of answer types available to the user
        /// </summary>
        public AnswerTypeData GetAssignedAnswerTypesList(int userId, int surveyId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@UserId", userId), 
            //    new SqlParameter("@SurveyId", surveyId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            AnswerTypeData dataSet = new AnswerTypeData();
            DbConnection.db.LoadDataSet("vts_spAnswerTypeGetListForUser", dataSet, new string[] { "AnswerTypes" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Returns a list of answer types that can be edited from the
        /// admin interface
        /// </summary>
        public AnswerTypeData GetEditableAnswerTypesList()
        {
            AnswerTypeData dataSet = new AnswerTypeData();
            DbConnection.db.LoadDataSet( "vts_spAnswerTypeGetEditableList", dataSet, new string[] { "AnswerTypes" });
            return dataSet;
        }

        /// <summary>
        /// Returns a list of answer types available to the user and that can 
        /// be edited from the admin interface
        /// </summary>
        public AnswerTypeData GetEditableAssignedAnswerTypesList(int userId)
        {
            AnswerTypeData dataSet = new AnswerTypeData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spAnswerTypeGetEditableListForUser", dataSet, new string[] { "AnswerTypes" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Creates and return an insert command for an answer type
        /// </summary>
        public SqlCommand GetInsertAnswerTypeCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int userId)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spAnswerTypeAddNew", sqlConnection) : new SqlCommand("vts_spAnswerTypeAddNew", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserId", userId));
            command.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200, "Description"));
            command.Parameters.Add(new SqlParameter("@XmlDataSource", SqlDbType.VarChar, 200, "XmlDataSource"));
            command.Parameters.Add(new SqlParameter("@DataSource", SqlDbType.NVarChar, 0xfa0, "DataSource"));
            command.Parameters.Add(new SqlParameter("@TypeMode", SqlDbType.Int, 4, "TypeMode"));
            command.Parameters.Add(new SqlParameter("@FieldWidth", SqlDbType.Int, 4, "FieldWidth"));
            command.Parameters.Add(new SqlParameter("@FieldHeight", SqlDbType.Int, 4, "FieldHeight"));
            command.Parameters.Add(new SqlParameter("@FieldLength", SqlDbType.Int, 4, "FieldLength"));
            command.Parameters.Add(new SqlParameter("@PublicFieldResults", SqlDbType.Bit, 1, "PublicFieldResults"));
            command.Parameters.Add(new SqlParameter("@JavascriptFunctionName", SqlDbType.VarChar, 0x3e8, "JavascriptFunctionName"));
            command.Parameters.Add(new SqlParameter("@JavascriptErrorMessage", SqlDbType.VarChar, 0x3e8, "JavascriptErrorMessage"));
            command.Parameters.Add(new SqlParameter("@JavascriptCode", SqlDbType.VarChar, 0x1f40, "JavascriptCode"));
            command.Parameters.Add(new SqlParameter("@AnswerTypeID", SqlDbType.Int, 4, "AnswerTypeID"));
            command.Parameters.Add(new SqlParameter("@TypeAssembly", SqlDbType.VarChar, 200, "TypeAssembly"));
            command.Parameters.Add(new SqlParameter("@TypeNameSpace", SqlDbType.VarChar, 200, "TypeNameSpace"));
            command.Parameters["@AnswerTypeID"].Direction = ParameterDirection.Output;
            return command;
        }

        /// <summary>
        /// Check if the answer type is in use by an answer
        /// </summary>
        public bool IsAnswerTypeInUse(int answerTypeId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@AnswerTypeID", answerTypeId).SqlValue);
            }


            if (DbConnection.db.ExecuteScalar("vts_spAnswerTypeIsInUse", commandParameters.ToArray()) == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Makes the answer type as builtin
        /// </summary>
        public void SetBuiltInAnswerType(int answerTypeId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@AnswerTypeID", answerTypeId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spAnswerTypeSetBuiltIn", commandParameters.ToArray());
        }

        /// <summary>
        /// Update the answer type in the database
        /// </summary>
        /// <param name="updatedAnswerType">Answer type to update, must specify the answer type id</param>
        public void UpdateAnswerType(AnswerTypeData updatedAnswerType)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spAnswerTypeUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@AnswerTypeID", SqlDbType.Int, 4, "AnswerTypeID"));
            insertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200, "Description"));
            insertCommand.Parameters.Add(new SqlParameter("@XmlDataSource", SqlDbType.VarChar, 200, "XmlDataSource"));
            insertCommand.Parameters.Add(new SqlParameter("@DataSource", SqlDbType.NVarChar, 0xfa0, "DataSource"));
            insertCommand.Parameters.Add(new SqlParameter("@TypeMode", SqlDbType.Int, 4, "TypeMode"));
            insertCommand.Parameters.Add(new SqlParameter("@FieldWidth", SqlDbType.Int, 4, "FieldWidth"));
            insertCommand.Parameters.Add(new SqlParameter("@FieldHeight", SqlDbType.Int, 4, "FieldHeight"));
            insertCommand.Parameters.Add(new SqlParameter("@FieldLength", SqlDbType.Int, 4, "FieldLength"));
            insertCommand.Parameters.Add(new SqlParameter("@PublicFieldResults", SqlDbType.Bit, 1, "PublicFieldResults"));
            insertCommand.Parameters.Add(new SqlParameter("@JavascriptFunctionName", SqlDbType.VarChar, 0x3e8, "JavascriptFunctionName"));
            insertCommand.Parameters.Add(new SqlParameter("@JavascriptErrorMessage", SqlDbType.VarChar, 0x3e8, "JavascriptErrorMessage"));
            insertCommand.Parameters.Add(new SqlParameter("@JavascriptCode", SqlDbType.VarChar, 0x1f40, "JavascriptCode"));
            insertCommand.Parameters.Add(new SqlParameter("@TypeAssembly", SqlDbType.VarChar, 200, "TypeAssembly"));
            insertCommand.Parameters.Add(new SqlParameter("@TypeNameSpace", SqlDbType.VarChar, 200, "TypeNameSpace"));
            DbConnection.db.UpdateDataSet(updatedAnswerType, "AnswerTypes", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }
    }
}

