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
    public class Filter : Votations.NSurvey.IDAL.IFilter
    {
        /// <summary>
        /// Adds a new filter
        /// </summary>
        /// <param name="newFilter"></param>
        public void AddFilter(FilterData newFilter)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spFilterAddNew", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@SurveyId", SqlDbType.Int, 4, "SurveyId"));
            insertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200, "Description"));
            insertCommand.Parameters.Add(new SqlParameter("@LogicalOperatorTypeID", SqlDbType.Int, 4, "LogicalOperatorTypeID"));
            insertCommand.Parameters.Add(new SqlParameter("@FilterId", SqlDbType.Int, 4, "FilterId"));
            insertCommand.Parameters.Add(new SqlParameter("@ParentFilterId", SqlDbType.Int, 4, "ParentFilterId"));
            insertCommand.Parameters["@FilterId"].Direction = ParameterDirection.Output;
            DbConnection.db.UpdateDataSet(newFilter, "Filters", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Adds a new filter rule
        /// </summary>
        /// <param name="newRule"></param>
        public void AddRule(FilterRuleData newRule)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spFilterRuleAddNew", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@FilterId", SqlDbType.Int, 4, "FilterID"));
            insertCommand.Parameters.Add(new SqlParameter("@AnswerId", SqlDbType.Int, 4, "AnswerId"));
            insertCommand.Parameters.Add(new SqlParameter("@QuestionId", SqlDbType.Int, 4, "QuestionId"));
            insertCommand.Parameters.Add(new SqlParameter("@TextFilter", SqlDbType.NVarChar, 0xfa0, "TextFilter"));
            insertCommand.Parameters.Add(new SqlParameter("@FilterRuleID", SqlDbType.Int, 4, "FilterRuleID"));
            insertCommand.Parameters["@FilterRuleID"].Direction = ParameterDirection.Output;
            DbConnection.db.UpdateDataSet(newRule, "FilterRules", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Deletes the given filter
        /// </summary>
        /// <param name="filterId"></param>
        public void DeleteFilter(int filterId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@FilterId", filterId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spFilterDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes the given filter rule
        /// </summary>
        /// <param name="ruleId"></param>
        public void DeleteRule(int ruleId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@FilterRuleId", ruleId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spFilterRuleDelete", commandParameters.ToArray());
        }

        public FilterData GetFilterById(int filterId)
        {
            FilterData dataSet = new FilterData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@FilterId", filterId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spFilterGetDetails", dataSet, new string[] { "Filters" }, commandParameters.ToArray());
            return dataSet;
        }

        public FilterData GetFilters(int surveyId)
        {
            FilterData dataSet = new FilterData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spFilterGetForSurvey", dataSet, new string[] { "Filters" }, commandParameters.ToArray());
            return dataSet;
        }

        public FilterData GetFiltersByParent(int surveyId, int parentId)
        {
            FilterData dataSet = new FilterData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyID", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@ParentID", parentId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spFilterGetForSurveyByParent", dataSet, new string[] { "Filters" }, commandParameters.ToArray());
            return dataSet;
        }

        public FilterRuleData GetRulesForFilter(int filterId)
        {
            FilterRuleData dataSet = new FilterRuleData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@FilterId", filterId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spFilterRuleGetForFilter", dataSet, new string[] { "FilterRules" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Update the filter options
        /// </summary>
        /// <param name="updatedFilter"></param>
        public void UpdateFilter(FilterData updatedFilter)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spFilterUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@FilterId", SqlDbType.Int, 4, "FilterId"));
            insertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 200, "Description"));
            insertCommand.Parameters.Add(new SqlParameter("@LogicalOperatorTypeID", SqlDbType.SmallInt, 4, "LogicalOperatorTypeID"));
            insertCommand.Parameters.Add(new SqlParameter("@ParentFilterId", SqlDbType.Int, 4, "ParentFilterId"));
            DbConnection.db.UpdateDataSet(updatedFilter, "Filters", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }

        public FilterData GetEmptyFilter()
        {
            var dataSet = new FilterData();
            dataSet.Filters.AddFiltersRow(0, "", 0, 0);

            return dataSet;
        }
    }
}

