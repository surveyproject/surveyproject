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
    public class SecurityAddIn : ISecurityAddIn
    {
        /// <summary>
        /// Adds the given security addin to the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInId"></param>
        /// <param name="addInOrder"></param>
        public void AddSecurityAddInToSurvey(int surveyId, int addInId, int addInOrder)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@addInId", addInId), 
            //    new SqlParameter("@addInOrder", addInOrder) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@addInId", addInId).SqlValue);
                commandParameters.Add(new SqlParameter("@addInOrder", addInOrder).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spWebSecurityAddInsAddForSurvey", commandParameters.ToArray());
        }

        /// <summary>
        /// Deletes the given security addin from the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInOrder"></param>
        public void DeleteWebSecurityAddIn(int surveyId, int addInId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@addInId", addInId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@addInId", addInId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spWebSecurityAddInsDeleteForSurvey", commandParameters.ToArray());
        }

        /// <summary>
        /// Disables the given addins
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInId"></param>
        public void DisableWebSecurityAddIn(int surveyId, int addInId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@addInId", addInId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@addInId", addInId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spWebSecurityAddInsDisableForSurvey", commandParameters.ToArray());
        }

        /// <summary>
        /// Enables the given addins
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInId"></param>
        public void EnableWebSecurityAddIn(int surveyId, int addInId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@addInId", addInId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@addInId", addInId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spWebSecurityAddInsEnableForSurvey", commandParameters.ToArray());
        }

        /// <summary>
        /// Returns the available addins (addins that are not already in use) 
        /// for the survey
        /// </summary>
        /// <param name="surveyId"></param>
        public WebSecurityAddInData GetAvailableAddIns(int surveyId)
        {
            WebSecurityAddInData dataSet = new WebSecurityAddInData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spWebSecurityAddInsGetAvailableList", dataSet, new string[] { "WebSecurityAddIns" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Get only security addins that are enabled for the survey
        /// </summary>
        public WebSecurityAddInData GetEnabledWebSecurityAddIns(int surveyId)
        {
            WebSecurityAddInData dataSet = new WebSecurityAddInData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spWebSecurityAddInsGetEnabled", dataSet, new string[] { "WebSecurityAddIns" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Return a AddIn object that reflects the database addin data
        /// </summary>
        /// <param name="addInId">Id of the addin you need</param>
        /// <returns>A WebSecurityAddInData object with the current database values</returns>
        public WebSecurityAddInData GetSurveyAddInById(int surveyId, int addInId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@addInId", addInId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@addInId", addInId).SqlValue);
            }

            WebSecurityAddInData dataSet = new WebSecurityAddInData();
            DbConnection.db.LoadDataSet("vts_spWebSecurityAddInsSurveyGetDetails", dataSet, new string[] { "WebSecurityAddIns" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Get a list of all available actions
        /// </summary>
        /// <returns></returns>
        public UnAuthentifiedUserActionData GetUnAuthentifiedUserActions()
        {
            UnAuthentifiedUserActionData dataSet = new UnAuthentifiedUserActionData();
            DbConnection.db.LoadDataSet("vts_spUnAuthentifiedUserActionGetAll", dataSet, new string[] { "UnAuthentifiedUserActions" });
            return dataSet;
        }

        /// <summary>
        /// Get all security addins available for the survey
        /// </summary>
        public WebSecurityAddInData GetWebSecurityAddIns(int surveyId)
        {
            WebSecurityAddInData dataSet = new WebSecurityAddInData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spWebSecurityAddInsGetAll", dataSet, new string[] { "WebSecurityAddIns" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Move the addin priority down
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInOrder"></param>
        public void MoveWebSecurityAddInDown(int surveyId, int addInId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@addInId", addInId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@addInId", addInId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spWebSecurityAddInsMoveDown", commandParameters.ToArray());
        }

        /// <summary>
        /// Move the addin priority up
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="addInOrder"></param>
        public void MoveWebSecurityAddInUp(int surveyId, int addInId)
        {
            //SqlParameter[] commandParameters = new SqlParameter[] 
            //{ new SqlParameter("@surveyid", surveyId), 
            //    new SqlParameter("@addInId", addInId) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@surveyid", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@addInId", addInId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spWebSecurityAddInsMoveUp", commandParameters.ToArray());
        }
    }
}

