namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    public interface IFolder
    {
        FolderData GetAllFolders();
        void AddFolder(int parentFolderId, string folderName);
        void UpdateFolder(int? parentFolderId, int folderId, string folderName);
        void MoveFolder(int? parentFolderId, int folderId);
        void DeleteFolderById(int id);
        FolderData GetTreeNodes(int userId);
        FolderData GetFolderById(int folderId);
    }
}

