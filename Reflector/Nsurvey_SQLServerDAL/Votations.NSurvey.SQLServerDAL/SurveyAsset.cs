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
    public class SurveyAsset : ISurveyAsset
    {

        public SurveyAssetData GetAllSurveyAssets(int surveyId)
        {
            SurveyAssetData surveyAssetData = new SurveyAssetData();

            DbConnection.db.LoadDataSet("vts_spSurveyAssetGetAll", surveyAssetData, new string[] { "SurveyAsset" });

            return surveyAssetData;
        }

        public void SurveyAssetAdd(int surveyId,string assetType,string name)
        {
            //SqlParameter[] spParameters = new SqlParameter[3];
            //spParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //spParameters[1] = new SqlParameter("@AssetType",  assetType);
            //spParameters[2] = new SqlParameter("@Name",name);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@AssetType", assetType).SqlValue);
                commandParameters.Add(new SqlParameter("@Name", name).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyAssetAddNew", commandParameters.ToArray());
            
        }

        public void SurveyAssetDelete(int assetId)
        {            
        //    SqlParameter[] spParameters = new SqlParameter[1];
        //    spParameters[0]	 = new SqlParameter("@AssetId", assetId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@AssetId", assetId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spSurveyAssetDelete", commandParameters.ToArray());
        }
    }
}