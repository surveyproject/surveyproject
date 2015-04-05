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
    using System.Runtime.InteropServices;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class SurveyLayout : ISurveyLayout
    {
        #region ISurveyLayout Members
         
        public SurveyLayoutData SurveyLayoutGet(int surveyId,string languageCode=null)
        {
            SurveyLayoutData sld = new SurveyLayoutData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            }

            DbConnection.db.LoadDataSet( "vts_spSurveylayoutGet", sld, new string[] { "SurveyLayout" }, commandParameters.ToArray());

            return sld;
        }

        public void SurveyLayoutUpdate(SurveyLayoutData sld, string languageCode = null)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand command = new SqlCommand("vts_spSurveyLayoutUpdate", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SurveyID",sld.SurveyLayout[0].SurveyId));
            command.Parameters.Add(new SqlParameter("@SurveyHeaderText", sld.SurveyLayout[0].SurveyHeaderText));
            command.Parameters.Add(new SqlParameter("@SurveyFooterText", sld.SurveyLayout[0].SurveyFooterText));
            command.Parameters.Add(new SqlParameter("@SurveyCss", sld.SurveyLayout[0].SurveyCss));
            command.Parameters.Add(new SqlParameter("@LanguageCode", languageCode));
            
         //   insertCommand.Parameters.Add(new SqlParameter("@SurveyId", SqlDbType.Int, 0xff, "RoleName"));

            DbConnection.db.UpdateDataSet(sld, "SurveyLayout", command, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
         //   SqlHelper.ExecuteNonQuery(SqlHelper.DbConnectionString, CommandType.StoredProcedure, "vts_spRoleSecurityRightAddNew", commandParameters);
        }

        #endregion
    }
}