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
    public class RegularExpression : IRegularExpression
    {
        /// <summary>
        /// Adds a new regular expression to the database
        /// </summary>
        public void AddRegularExpression(RegularExpressionData newRegularExpression, int userId)
        {
            SqlConnection sqlConnection = new SqlConnection(DbConnection.NewDbConnectionString);
            DbConnection.db.UpdateDataSet(newRegularExpression, "RegularExpressions", this.GetInsertRegularExpressionCommand(sqlConnection, null, userId), new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Remove the regular expression 
        /// </summary>
        public void DeleteRegularExpressionById(int regularExpressionId)
        {
            //DbConnection.db.ExecuteNonQuery("vts_spRegularExpressionDelete", new SqlParameter[] { new SqlParameter("@RegularExpressionId", regularExpressionId) });

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RegularExpressionID", regularExpressionId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spRegularExpressionDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Retrieves all regular expression from the database
        /// </summary>
        public RegularExpressionData GetAllRegularExpressionsList()
        {
            RegularExpressionData dataSet = new RegularExpressionData();
            DbConnection.db.LoadDataSet("vts_spRegularExpressionGetList", dataSet, new string[] { "RegularExpressions" });
            return dataSet;
        }

        /// <summary>
        /// Retrieves all regular expression from the database assigned to a user
        /// </summary>
        public RegularExpressionData GetEditableRegularExpressionsListOfUser(int userId)
        {
            RegularExpressionData dataSet = new RegularExpressionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spRegularExpressionGetEditableListForUser", dataSet, new string[] { "RegularExpressions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Creates and return an insert command for an answer type
        /// </summary>
        public SqlCommand GetInsertRegularExpressionCommand(SqlConnection sqlConnection, SqlTransaction sqlTransaction, int userId)
        {
            SqlCommand command = (sqlTransaction == null) ? new SqlCommand("vts_spRegularExpressionAddNew", sqlConnection) : new SqlCommand("vts_spRegularExpressionAddNew", sqlConnection, sqlTransaction);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@UserId", userId));
            command.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 0xff, "Description"));
            command.Parameters.Add(new SqlParameter("@RegExpression", SqlDbType.VarChar, 0x7d0, "RegExpression"));
            command.Parameters.Add(new SqlParameter("@RegExMessage", SqlDbType.VarChar, 0x7d0, "RegExMessage"));
            command.Parameters.Add(new SqlParameter("@RegularExpressionId", SqlDbType.Int, 4, "RegularExpressionId"));
            command.Parameters["@RegularExpressionId"].Direction = ParameterDirection.Output;
            return command;
        }

        /// <summary>
        /// Retrieves regular expression details from the database
        /// </summary>
        public RegularExpressionData GetRegularExpressionById(int regularExpressionId)
        {
            RegularExpressionData dataSet = new RegularExpressionData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RegularExpressionId", regularExpressionId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spRegularExpressionGetDetails", dataSet, new string[] { "RegularExpressions", "SecurityRights" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Retrieves all regular expression from the database assigned to a user
        /// </summary>
        public RegularExpressionData GetRegularExpressionsOfUser(int userId, int surveyId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@UserId", userId), 
            //    new SqlParameter("@SurveyId", surveyId) };


            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@UserId", userId).SqlValue);
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            RegularExpressionData dataSet = new RegularExpressionData();
            DbConnection.db.LoadDataSet("vts_spRegularExpressionGetListForUser", dataSet, new string[] { "RegularExpressions" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Set regular expression to be built in
        /// </summary>
        public void SetBuiltInRegularExpression(int regularExpressionId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@RegularExpressionID", regularExpressionId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spRegularExpressionSetBuiltIn", commandParameters.ToArray());
        }

        /// <summary>
        /// Updates regular expressions data
        /// </summary>
        public void UpdateRegularExpression(RegularExpressionData updatedRegularExpression)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spRegularExpressionUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@RegularExpressionId", SqlDbType.Int, 4, "RegularExpressionId"));
            insertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 0xff, "Description"));
            insertCommand.Parameters.Add(new SqlParameter("@RegExpression", SqlDbType.VarChar, 0x7d0, "RegExpression"));
            insertCommand.Parameters.Add(new SqlParameter("@RegExMessage", SqlDbType.VarChar, 0x7d0, "RegExMessage"));
            DbConnection.db.UpdateDataSet(updatedRegularExpression, "RegularExpressions", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }
    }
}

