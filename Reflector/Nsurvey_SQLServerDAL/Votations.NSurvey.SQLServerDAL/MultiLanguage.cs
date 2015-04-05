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
    using System.Data.SqlTypes;

    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;
    
    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class MultiLanguage : IMultiLanguage
    {
        /// <summary>
        /// Check if the language code is enabled for the survey
        /// and if its not returns the default enabled language code
        /// </summary>
        public string CheckSurveyLanguage(int surveyId, string languageCode)
        {
            //SqlParameter[] commandParameters = new SqlParameter[2];
            //commandParameters[0] = new SqlParameter("@SurveyId", surveyId);
            //commandParameters[1] = new SqlParameter("@LanguageCode", languageCode);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", languageCode).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spMutliLanguageCheckForSurvey", commandParameters.ToArray());
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                return obj2.ToString();
            }
            return "";
        }

        /// <summary>
        /// Deletes a language from the survey
        /// </summary>
        public void DeleteSurveyLanguage(int surveyId, string languageCode,string Entity)
        {
            //SqlParameter[] commandParameters = new SqlParameter[3];
            //commandParameters[0] = new SqlParameter("@SurveyId", SqlDbType.Int, 4);
            //commandParameters[0].Value = surveyId;
            //commandParameters[1] = new SqlParameter("@LanguageCode", SqlDbType.NVarChar, 50);
            //commandParameters[1].Value = languageCode;
            //commandParameters[2] = new SqlParameter("@Entity", SqlDbType.VarChar, 20);
            //commandParameters[2].Value = Entity;

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", SqlDbType.Int, 4) {Value = surveyId }.SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", SqlDbType.NVarChar, 50) { Value = languageCode }.SqlValue);
                commandParameters.Add(new SqlParameter("@Entity", SqlDbType.VarChar, 20) { Value = Entity }.SqlValue);
            }


            DbConnection.db.ExecuteNonQuery("vts_spMutliLanguageDeleteForSurvey", commandParameters.ToArray());
        }

        /// <summary>
        /// Disable survey's multi language features
        /// </summary>
        /// <param name="surveyId"></param>
        public void DisableMultiLanguage(int surveyId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spSurveyMultiLanguageModeClearSettings", commandParameters.ToArray());
        }

        /// <summary>
        /// Returns current mode
        /// </summary>
        public MultiLanguageMode GetMultiLanguageMode(int surveyId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
            }

            object obj2 = DbConnection.db.ExecuteScalar("vts_spSurveyMultiLanguageModeGet", commandParameters.ToArray());
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                return (MultiLanguageMode) int.Parse(obj2.ToString());
            }
            return MultiLanguageMode.None;
        }

        /// <summary>
        /// Returns all languages that can be enabled
        /// </summary>
        public MultiLanguageData GetMultiLanguages()
        {
            MultiLanguageData dataSet = new MultiLanguageData();
            DbConnection.db.LoadDataSet("vts_spMultiLanguageGetAll", dataSet, new string[] { "MultiLanguages" });
            return dataSet;
        }

        /// <summary>
        /// Returns all enabled languages
        /// </summary>
        public MultiLanguageData GetSurveyLanguages(int surveyId, string Entity)
        {
            MultiLanguageData dataSet = new MultiLanguageData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@Entity", Entity).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spMutliLanguageGetEnabledForSurvey", dataSet, new string[] { "MultiLanguages" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Updates the current multi language mode that defines how 
        /// the user selects his language
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="languageMode"></param>
        public void UpdateMultiLanguage(int surveyId, MultiLanguageMode languageMode, string variableName)
        {
            //SqlParameter[] commandParameters = new SqlParameter[]
            //{ new SqlParameter("@SurveyId", surveyId), 
            //    new SqlParameter("@MultiLanguageModeId", languageMode), 
            //    new SqlParameter("@MultiLanguageVariable", variableName) };

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@MultiLanguageModeId", SqlDbType.Int, 4) { Value = Convert.ToInt32(languageMode)}.SqlValue);                
                commandParameters.Add(new SqlParameter("@MultiLanguageVariable", variableName).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spSurveyMultiLanguageModeUpdate", commandParameters.ToArray());
        }

        /// <summary>
        /// Adds a language to a survey
        /// </summary>
        public void UpdateSurveyLanguage(int surveyId, string languageCode, bool defaultLanguage, string Entity)
        {
            //SqlParameter[] commandParameters = new SqlParameter[4];
            //commandParameters[0] = new SqlParameter("@SurveyId", SqlDbType.Int, 4);
            //commandParameters[0].Value = surveyId;
            //commandParameters[1] = new SqlParameter("@LanguageCode", SqlDbType.NVarChar, 50);
            //commandParameters[1].Value = languageCode;
            //commandParameters[2] = new SqlParameter("@DefaultLanguage", SqlDbType.Bit, 1);
            //commandParameters[2].Value = defaultLanguage;
            //commandParameters[3] = new SqlParameter("@Entity", SqlDbType.VarChar, 20);
            //commandParameters[3].Value = Entity;
            
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", SqlDbType.Int, 4) { Value = surveyId }.SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", SqlDbType.NVarChar, 50) { Value = languageCode }.SqlValue);
                commandParameters.Add(new SqlParameter("@DefaultLanguage", SqlDbType.Bit, 1) { Value = defaultLanguage}.SqlValue);
                //commandParameters.Add(new SqlParameter("@DefaultLanguage", defaultLanguage).SqlValue);
                commandParameters.Add(new SqlParameter("@Entity", SqlDbType.VarChar, 20) { Value = Entity }.SqlValue);
            }                        

            DbConnection.db.ExecuteNonQuery("vts_spMutliLanguageAddForSurvey", commandParameters.ToArray());
        }

        /// <summary>
        /// Adds a language to a survey
        /// </summary>
        public void AddMultiLanguageText(int languageItemId, string languageCode, int LanguageMessageTypeId ,string ItemText)
        {
            //SqlParameter[] commandParameters = new SqlParameter[4];
            //commandParameters[0] = new SqlParameter("@LanguageItemId", SqlDbType.Int, 4);
            //commandParameters[0].Value = languageItemId;
            //commandParameters[1] = new SqlParameter("@LanguageCode", SqlDbType.NVarChar, 50);
            //commandParameters[1].Value = languageCode;
            //commandParameters[2] = new SqlParameter("@LanguageMessageTypeId", SqlDbType.Int,4);
            //commandParameters[2].Value = LanguageMessageTypeId;
            //commandParameters[3] = new SqlParameter("@ItemText", SqlDbType.VarChar, 4000);
            //commandParameters[3].Value = ItemText;

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@LanguageItemId", SqlDbType.Int, 4) { Value = languageItemId }.SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", SqlDbType.NVarChar, 50) { Value = languageCode }.SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageMessageTypeId", SqlDbType.Int, 4) { Value = LanguageMessageTypeId }.SqlValue);
                commandParameters.Add(new SqlParameter("@ItemText", SqlDbType.VarChar, 4000) { Value = ItemText }.SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spMultiLanguageTextAdd", commandParameters.ToArray());
        }

    }
}

