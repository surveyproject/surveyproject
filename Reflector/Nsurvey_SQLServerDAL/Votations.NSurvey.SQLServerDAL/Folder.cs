namespace Votations.NSurvey.SQLServerDAL
{
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    public class Folder : IFolder
    {
        public FolderData GetAllFolders()
        {
            FolderData folderData = new FolderData();
            return folderData;
        }


        public void AddFolder(int parentFolderId, string folderName)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0]	 = new SqlParameter("@ParentFolderId", parentFolderId);            
            //spParameters[1]	 = new SqlParameter("@FolderName", SqlDbType.VarChar, 200, folderName);
            //spParameters[1].Value = folderName;

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@ParentFolderId", parentFolderId).SqlValue);
                sqlParams.Add(new SqlParameter("@FolderName", folderName).SqlValue);
            }


            DbConnection.db.ExecuteNonQuery("vts_spFolderAddNew", sqlParams.ToArray());
            
        }

        public void UpdateFolder(int ?parentFolderId, int folderId, string folderName)
        {
            //SqlParameter[] spParameters = new SqlParameter[3];
            //spParameters[0] = new SqlParameter("@FolderId", folderId);
            //spParameters[1] = new SqlParameter("@FolderName", SqlDbType.VarChar, 200);
            //spParameters[1].Value = folderName;
            //spParameters[2] = new SqlParameter("@ParentFolderId", parentFolderId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@FolderId", folderId).SqlValue);
                sqlParams.Add(new SqlParameter("@FolderName", folderName).SqlValue);
                sqlParams.Add(new SqlParameter("@ParentFolderId", parentFolderId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spFolderUpdate", sqlParams.ToArray());
        }

        public void DeleteFolderById(int id)
        {            
            //SqlParameter[] spParameters = new SqlParameter[1];
            //spParameters[0]	 = new SqlParameter("@FolderId", id);

            var spParameters = new SqlParameter("@FolderId", id).SqlValue;

            DbConnection.db.ExecuteNonQuery("vts_spFolderDelete", spParameters);
        }

        public FolderData GetTreeNodes(int UserId)
        {
            FolderData folderData = new FolderData();

            //SqlParameter[] spParameters = new SqlParameter[1];
            //spParameters[0] = new SqlParameter("@UserId", UserId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@UserId", UserId).SqlValue);
            }

            //SqlHelper.FillDataset(SqlHelper.DbConnectionString, CommandType.StoredProcedure, "vts_spTreeNodesGetAll", folderData, new string[] { "TreeNodes" },spParameters);
            DbConnection.db.LoadDataSet("vts_spTreeNodesGetAll", folderData, new string[] { "TreeNodes" }, sqlParams.ToArray());

            return folderData;
        }


        public void MoveFolder(int? parentFolderId, int folderId)
        {
            //SqlParameter[] spParameters = new SqlParameter[2];
            //spParameters[0] = new SqlParameter("@ParentFolderId", parentFolderId);
            //spParameters[1] = new SqlParameter("@FolderId", folderId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@ParentFolderId", parentFolderId).SqlValue);
                sqlParams.Add(new SqlParameter("@FolderId", folderId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spFolderMove", sqlParams.ToArray());            
        }

        public FolderData GetFolderById(int folderId)
        {
            FolderData folderData = new FolderData();

            //SqlParameter[] spParameters = new SqlParameter[1];
            //spParameters[0] = new SqlParameter("@FolderId", folderId);

            ArrayList sqlParams = new ArrayList();
            {
                sqlParams.Add(new SqlParameter("@FolderId", folderId).SqlValue);
            }


           // SqlHelper.FillDataset(SqlHelper.DbConnectionString, CommandType.StoredProcedure, "vts_spFolderGetByFolderId", folderData, new string[] { "TreeNodes" }, spParameters);

            DbConnection.db.LoadDataSet("vts_spFolderGetByFolderId", folderData, new string[] { "TreeNodes" }, sqlParams.ToArray());

            return folderData;
        }
    }
}
