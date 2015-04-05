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
    using System.Linq;
    using Microsoft.SqlServer.Server;


    public class SurveyToken : ISurveyToken
    {

        #region ISurveyToken Members

        public SurveyTokenData GetAllSurveyTokens(int surveyID)
        {
            SurveyTokenData surveyTokenData = new SurveyTokenData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyID).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spTokenGetForSurvey", surveyTokenData, new string[] { "surveyToken" }, commandParameters.ToArray());

            return surveyTokenData;
        }

        public bool ValidateToken(int surveyID, string token)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyID).SqlValue);
                commandParameters.Add(new SqlParameter("@token", token).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar( "vts_spSurveyTokenValidate", commandParameters.ToArray());
            if (obj2 == null)
            {
                return false;
            }
            return int.Parse(obj2.ToString()) ==0?false:true;
        }


        public void UpdateToken(int surveyID, string token,int voterId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@token", token).SqlValue);
                commandParameters.Add(new SqlParameter("@voterId", voterId).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spSurveyTokenUpdate", commandParameters.ToArray());
        }


        public void DeleteSurveyTokensByIdMultiple(IEnumerable<int> ids)
        {
            SqlMetaData[] tab = { new SqlMetaData("value", SqlDbType.Int) };
            List<SqlDataRecord> idList =
            ids.Select(x => { var y = new SqlDataRecord(tab); y.SetInt32(0, x); return y; }).ToList<SqlDataRecord>();

            SqlParameter p = new SqlParameter("@tblTokenIdList", SqlDbType.Structured);
            p.Direction = ParameterDirection.Input;
            p.TypeName = "dbo.IntTableType";
            p.Value = idList;

            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(DbConnection.NewDbConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "vts_spSurveyTokenDeleteMultiple";
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddSurveyTokensMultiple(int surveyId, DateTime creationDate, IEnumerable<string> tokens)
        {
            SqlMetaData[] tab = { new SqlMetaData("value", SqlDbType.VarChar, 40) };
            List<SqlDataRecord> tokenList =
            tokens.Select(x => { var y = new SqlDataRecord(tab); y.SetSqlString(0, x); return y; }).ToList<SqlDataRecord>();

            SqlParameter p = new SqlParameter("@tblTokenList", SqlDbType.Structured);
            p.Direction = ParameterDirection.Input;
            p.TypeName = "dbo.VarcharTableType";
            p.Value = tokenList;

            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(DbConnection.NewDbConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "vts_spSurveyTokenAddMultiple";
                cmd.Parameters.Add(p);
                cmd.Parameters.Add(new SqlParameter("@SurveyID", surveyId));
                cmd.Parameters.Add(new SqlParameter("@CreationDate", creationDate));

                cmd.ExecuteNonQuery();
            }
            //   SqlHelper.ExecuteNonQuery(SqlHelper.DbConnectionString, "vts_spSurveyTokenAddMultiple", pars);

        }


        #endregion
    }
}
