namespace Votations.NSurvey.SQLServerDAL
{
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    //using Microsoft.ApplicationBlocks.Data;

    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Votations.NSurvey.IDAL;

    using System.Data;
    using System.Data.SqlClient;
    using Votations.NSurvey.Data;

    public class QuestionGroups : IQuestionGroup
    {

        public QuestionGroupsData GetAll(string langCode)
        {
            QuestionGroupsData groups = new QuestionGroupsData();

            //SqlParameter[] spParameters = new SqlParameter[1];
            //spParameters[0] = new SqlParameter("@LanguageCode", langCode);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@LanguageCode", langCode).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionGroupGetAll", groups, new string[] { "QuestionGroups" }, commandParameters.ToArray());

            return groups;
        }

        public QuestionGroupsData GetByQuestionId(int questionId)
        {
            QuestionGroupsData groups = new QuestionGroupsData();

            //SqlParameter[] spParameters = new SqlParameter[1];
            //spParameters[0] = new SqlParameter("QuestionId", questionId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("QuestionId", questionId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spQuestionGroupGetByQuestionID", groups, new string[] { "QuestionGroups" }, commandParameters.ToArray());

            return groups;
        }

        public void AddNewGroup(string name, int? parentGroupId, string langId)
        {
            //SqlParameter[] spParameters = new SqlParameter[4];
            //spParameters[0] = new SqlParameter("@GroupName", name);
            //spParameters[1] = new SqlParameter("@ParentGroupID", parentGroupId);
            //spParameters[1].IsNullable = true;
            //spParameters[2] = new SqlParameter("@LanguageCode", langId);
            //spParameters[3] = new SqlParameter("@QuestionGroupID", SqlDbType.Int);
            //spParameters[3].Direction = ParameterDirection.Output;

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@GroupName", name).SqlValue);
                commandParameters.Add(new SqlParameter("@ParentGroupID", parentGroupId) { IsNullable = true}.SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", langId).SqlValue);
                commandParameters.Add(new SqlParameter("@QuestionGroupID", SqlDbType.Int) { Direction = ParameterDirection.Output }.SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spQuestionGroupAddNew", commandParameters.ToArray());
        }

        public void UpdateDisplayOrder(int questionGroupId, QuestionGroupDisplayOrder order)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@QuestionGroupId", questionGroupId);
            //spParameters[1] = new SqlParameter("@Up", (int)order);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionGroupId", questionGroupId).SqlValue);
                commandParameters.Add(new SqlParameter("@Up", (int)order).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spQuestionGroupUpdateDisplayID", commandParameters.ToArray());
        }


        public void UpdateGroup(int questionGroupId, int? parentGroupId, string name, string langId)
        {
            //SqlParameter[] spParameters = new SqlParameter[4];
            //spParameters[0] = new SqlParameter("@GroupName", name);
            //spParameters[1] = new SqlParameter("@QuestionGroupID", questionGroupId);
            //spParameters[2] = new SqlParameter("@ParentGroupID", parentGroupId);
            //spParameters[3] = new SqlParameter("@LanguageCode", langId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@GroupName", name).SqlValue);
                commandParameters.Add(new SqlParameter("@QuestionGroupId", questionGroupId).SqlValue);
                commandParameters.Add(new SqlParameter("@ParentGroupID", parentGroupId).SqlValue);
                commandParameters.Add(new SqlParameter("@LanguageCode", langId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spQuestionGroupUpdate", commandParameters.ToArray());
        }


        public void DeleteGroup(int questionGroupId)
        {
            SqlParameter[] spParameters = new SqlParameter[1];
            spParameters[0] = new SqlParameter("@QuestionGroupID", questionGroupId);

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@QuestionGroupId", questionGroupId).SqlValue);
            }
            
            DbConnection.db.ExecuteNonQuery("vts_spQuestionGroupDelete", commandParameters.ToArray());
        }
    }
}
