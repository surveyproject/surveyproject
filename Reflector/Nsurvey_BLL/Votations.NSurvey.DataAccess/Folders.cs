namespace Votations.NSurvey.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    public class Folders
    {
        public void AddFolder(int parentFolderId, string folderName)
        {
            FolderFactory.Create().AddFolder(parentFolderId, folderName);
        }

        public void DeleteFolderById(int id)
        {
            FolderFactory.Create().DeleteFolderById(id);
        }

        public void UpdateFolder(int? parentFolderId, int folderId, string folderName)
        {
            FolderFactory.Create().UpdateFolder(parentFolderId,folderId, folderName);
        }

        public void MoveFolder(int? parentFolderId, int folderId)
        {
            FolderFactory.Create().MoveFolder(parentFolderId, folderId);
        }

        public FolderData GetAllFolders()
        {
            return FolderFactory.Create().GetAllFolders();
        }

        public FolderData GetTreeNodes(int userId)
        {
            return FolderFactory.Create().GetTreeNodes(userId);
        }

        public FolderData GetFolderById(int folderId)
        {
            return FolderFactory.Create().GetFolderById(folderId);
        }
    }
}
