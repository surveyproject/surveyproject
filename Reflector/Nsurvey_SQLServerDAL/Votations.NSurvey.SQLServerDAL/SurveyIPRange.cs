namespace Votations.NSurvey.SQLServerDAL
{
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    //using Microsoft.ApplicationBlocks.Data;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    using System.Data;
    using System.Data.SqlClient;

    public class SurveyIPRange :ISurveyIPRange
    {
        #region ISurveyIPRange Members

        public SurveyIPRangeData GetAllSurveyIPRanges(int surveyID)
        {
            SurveyIPRangeData surveyIPRangeData = new SurveyIPRangeData();


            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyID).SqlValue);
            }

            DbConnection.db.LoadDataSet( "vts_spIPRangeGetForSurvey",surveyIPRangeData, new string[]{"surveyIPRange"},commandParameters.ToArray());

            return surveyIPRangeData;
        }

        public void UpdateSurveyIPRange(SurveyIPRangeData updIPRange)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand command = new SqlCommand("vts_spSurveyIPRangeUpdate", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SurveyIPRangeID", updIPRange.SurveyIPRange[0].SurveyIPRangeId));
            command.Parameters.Add(new SqlParameter("@SurveyID", updIPRange.SurveyIPRange[0].SurveyId));
            command.Parameters.Add(new SqlParameter("@IPStart", updIPRange.SurveyIPRange[0].IPStart));
            command.Parameters.Add(new SqlParameter("@IPEnd", updIPRange.SurveyIPRange[0].IPEnd));

            DbConnection.db.UpdateDataSet(updIPRange, "SurveyIPRange", command, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        public void AddNewSurveyIPRange(SurveyIPRangeData updIPRange)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand command = new SqlCommand("vts_spSurveyIPRangeAddNew", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SurveyID", updIPRange.SurveyIPRange[0].SurveyId));
            command.Parameters.Add(new SqlParameter("@IPStart", updIPRange.SurveyIPRange[0].IPStart));
            command.Parameters.Add(new SqlParameter("@IPEnd", updIPRange.SurveyIPRange[0].IPEnd));

            DbConnection.db.UpdateDataSet(updIPRange, "SurveyIPRange", command, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        public void DeleteSurveyIPRangeById(int surveyIPId)
        {                    
            //SqlParameter[] spParameters = new SqlParameter[1];
            //spParameters[0]	 = new SqlParameter("@SurveyIPRangeId", surveyIPId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyIPRangeId", surveyIPId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spSurveyIPRangeDelete", commandParameters.ToArray());
        }
        

        #endregion
    }
}
