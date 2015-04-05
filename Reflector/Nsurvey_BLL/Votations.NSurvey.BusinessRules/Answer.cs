namespace Votations.NSurvey.BusinessRules
{
    using System;
    using System.Collections;
    using System.IO;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;

    /// <summary>
    /// Contains the business rules that are used for an answer.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Adds a new answer to the question specified by the question id property in the database
        /// </summary>
        /// <param name="newAnswer">Answer object with information about what to add. Only Id must be ommited</param>
        public void AddAnswer(AnswerData newAnswer)
        {
            AnswerFactory.Create().AddAnswer(newAnswer);
        }

        /// <summary>
        /// Adds a new answer to the matrix question specified by the question id property in the database
        /// </summary>
        /// <param name="newAnswer">Answer object with information about what to add. Only Id must be ommited</param>
        public void AddMatrixAnswer(AnswerData newAnswer)
        {
            AnswerFactory.Create().AddMatrixAnswer(newAnswer);
        }

        /// <summary>
        /// Remove the answer from the database
        /// </summary>
        /// <param name="answerId">Answer to delete from the database</param>
        public void DeleteAnswer(int answerId)
        {
            AnswerFactory.Create().DeleteAnswer(answerId);
        }

        /// <summary>
        /// delete all file data
        /// </summary>
        public void DeleteAnswerFile(int fileId, string groupGuid)
        {
            AnswerFactory.Create().DeleteAnswerFile(fileId, groupGuid);
        }

        /// <summary>
        /// Deletes the persisted answer properties from the DB
        /// </summary>
        public void DeleteAnswerProperties(int answerId)
        {
            AnswerFactory.Create().DeleteAnswerProperties(answerId);
        }

        /// <summary>
        /// Remove one answer column of the matrix from the database
        /// </summary>
        /// <param name="answerId">Answer column to delete from the database</param>
        public void DeleteMatrixAnswer(int answerId)
        {
            AnswerFactory.Create().DeleteMatrixAnswer(answerId);
        }

        /// <summary>
        /// Export the files of the survey to the directory and 
        /// returns the total of bytes exported
        /// </summary>
        public int ExportAnswerFilesToDirectory(int surveyId, string path, FileExportMode exportMode)
        {
            int num;
            int num2 = 0;
            Hashtable savedFiles = new Hashtable();
            foreach (FileData.FilesRow row in new Answers().GetValidatedFileAnswers(surveyId, 1, 10, out num).Files)
            {
                string str = path;
                if (exportMode == FileExportMode.VoterFileGroup)
                {
                    if (!Directory.Exists(path + row.VoterId))
                    {
                        Directory.CreateDirectory(path + row.VoterId);
                    }
                    str = path + row.VoterId + '/';
                }
                else if (exportMode == FileExportMode.GuidFileGroup)
                {
                    if (!Directory.Exists(path + row.GroupGuid))
                    {
                        Directory.CreateDirectory(path + row.GroupGuid);
                    }
                    str = path + row.GroupGuid + '/';
                }
                byte[] answerFileData = new Answers().GetAnswerFileData(row.FileId, row.GroupGuid);
                this.SaveFileToDisk(str, row.FileName, answerFileData, savedFiles);
                num2 += answerFileData.Length;
            }
            return num2;
        }

        /// <summary>
        /// Moves down the answer's display position 
        /// </summary>
        /// <param name="answerId"></param>
        public void MoveAnswerDown(int answerId)
        {
            AnswerFactory.Create().MoveAnswerDown(answerId);
        }

        /// <summary>
        /// Moves up the answer's display position 
        /// </summary>
        /// <param name="answerId"></param>
        public void MoveAnswerUp(int answerId)
        {
            AnswerFactory.Create().MoveAnswerUp(answerId);
        }

        private void SaveFileToDisk(string path, string fileName, byte[] fileData, Hashtable savedFiles)
        {
            if (savedFiles.ContainsKey(path + fileName))
            {
                int num = int.Parse(savedFiles[path + fileName].ToString()) + 1;
                savedFiles[fileName] = num;
                fileName = num.ToString() + '_' + fileName;
            }
            else
            {
                savedFiles.Add(path + fileName, 1);
            }
            FileStream stream = new FileStream(path + fileName, FileMode.Create, FileAccess.Write);
            stream.Write(fileData, 0, fileData.Length);
            stream.Close();
        }

        /// <summary>
        /// Stores a file in the database
        /// </summary>
        public int StoreAnswerFile(string groupGuid, string fileName, int fileSize, string fileType, byte[] fileData, int uploadedFileTimeOut, int sessionUploadedFileTimeOut)
        {
            return AnswerFactory.Create().StoreAnswerFile(groupGuid, fileName, fileSize, fileType, fileData, uploadedFileTimeOut, sessionUploadedFileTimeOut);
        }

        /// <summary>
        /// Stores properties in the database
        /// </summary>
        public void StoreAnswerProperties(int answerId, byte[] properties)
        {
            AnswerFactory.Create().StoreAnswerProperties(answerId, properties);
        }

        /// <summary>
        /// Subscribe to a new answer publisher
        /// </summary>
        public void SubscribeToPublisher(int publisherAnswerId, int subscriberAnswerId)
        {
            AnswerFactory.Create().SubscribeToPublisher(publisherAnswerId, subscriberAnswerId);
        }

        /// <summary>
        /// Unsubscribe from the given answer publisher
        /// </summary>
        public void UnSubscribeFromPublisher(int publisherAnswerId, int subscriberAnswerId)
        {
            AnswerFactory.Create().UnSubscribeFromPublisher(publisherAnswerId, subscriberAnswerId);
        }

        /// <summary>
        /// Update the answer in the database
        /// </summary>
        /// <param name="updatedAnswer">Answer to update, must specify the answer id</param>
        public void UpdateAnswer(AnswerData updatedAnswer, string languageCode)
        {
            AnswerFactory.Create().UpdateAnswer(updatedAnswer, languageCode);
        }

        /// <summary>
        /// Update the matrix column in the database
        /// </summary>
        /// <param name="updatedAnswer">Answer to update, must specify the answer id</param>
        public void UpdateMatrixAnswer(AnswerData updatedAnswer, string languageCode)
        {
            AnswerFactory.Create().UpdateMatrixAnswer(updatedAnswer, languageCode);
        }
    }
}

