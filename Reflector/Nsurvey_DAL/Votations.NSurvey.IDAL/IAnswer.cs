namespace Votations.NSurvey.IDAL
{
    using System;
    using System.Runtime.InteropServices;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Interface for the answer DAL.
    /// </summary>
    public interface IAnswer
    {
        void AddAnswer(AnswerData newAnswer);
        void AddMatrixAnswer(AnswerData newAnswer);
        void DeleteAnswer(int answerId);
        void DeleteAnswerFile(int fileId, string groupGuid);
        void DeleteAnswerProperties(int answerId);
        void DeleteMatrixAnswer(int answerId);
        AnswerData GetAnswerById(int answerId, string languageCode);
        FileData GetAnswerFile(int fileId, string groupGuid);
        int GetAnswerFileCount(string groupGuid);
        byte[] GetAnswerFileData(int fileId, string groupGuid);
        AnswerData GetAnswers(int questionId, string languageCode);
        AnswerData GetAnswersList(int questionId);
        int GetAnswersScoreTotal(string answersIdValues);
        int GetAnswerTypeMode(int answerId);
        FileData GetGuidFiles(string groupGuid);
        AnswerData GetPublishersList(int answerId);
        AnswerData GetSelectableAnswers(int questionId);
        AnswerData GetSubscriptionList(int answerId);
        FileData GetUnValidatedFileAnswers(int surveyId, int pageNumber, int pageSize, out int totalRecords);
        FileData GetValidatedFileAnswers(int surveyId, int pageNumber, int pageSize, out int totalRecords);
        void MoveAnswerDown(int answerId);
        void MoveAnswerUp(int answerId);
        byte[] RestoreAnswerProperties(int answerId);
        int StoreAnswerFile(string groupGuid, string fileName, int fileSize, string fileType, byte[] fileData, int uploadedFileTimeOut, int sessionUploadedFileTimeOut);
        void StoreAnswerProperties(int answerId, byte[] properties);
        void SubscribeToPublisher(int publisherAnswerId, int subscriberAnswerId);
        void UnSubscribeFromPublisher(int publisherAnswerId, int subscriberAnswerId);
        void UpdateAnswer(AnswerData updatedAnswer, string languageCode);
        void UpdateMatrixAnswer(AnswerData updatedAnswer, string languageCode);
    }
}

