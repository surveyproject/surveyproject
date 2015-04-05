namespace Votations.NSurvey.DataAccess
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the methods to access answers database data.
    /// </summary>
    public class Answers
    {
        /// <summary>
        /// Return an answer object that reflects the database answer
        /// </summary>
        /// <param name="answeriD">Id of the answer you need</param>
        /// <returns>An answer object with the current database values</returns>
        public AnswerData GetAnswerById(int answerId, string languageCode)
        {
            return AnswerFactory.Create().GetAnswerById(answerId, languageCode);
        }

        /// <summary>
        /// Get the details of the file
        /// </summary>
        public FileData GetAnswerFile(int fileId, string groupGuid)
        {
            return AnswerFactory.Create().GetAnswerFile(fileId, groupGuid);
        }

        /// <summary>
        /// Get the count of files in a group
        /// </summary>
        public int GetAnswerFileCount(string groupGuid)
        {
            return AnswerFactory.Create().GetAnswerFileCount(groupGuid);
        }

        /// <summary>
        /// Get the binarie data of the file
        /// </summary>
        public byte[] GetAnswerFileData(int fileId, string groupGuid)
        {
            return AnswerFactory.Create().GetAnswerFileData(fileId, groupGuid);
        }

        /// <summary>
        /// Get all answers for a given question
        /// </summary>
        /// <param name="questionId">question which is owning the answers</param>
        /// <param name="languageId">Language in which to return the answers, -1 act as the default language</param>
        /// <returns></returns>
        public AnswerData GetAnswers(int questionId, string languageCode)
        {
            return AnswerFactory.Create().GetAnswers(questionId, languageCode);
        }

        /// <summary>
        /// Get a list of answers for the given question
        /// </summary>
        /// <param name="questionId">question which is owning the answers</param>
        /// <returns></returns>
        public AnswerData GetAnswersList(int questionId)
        {
            AnswerData answersList = AnswerFactory.Create().GetAnswersList(questionId);
            foreach (AnswerData.AnswersRow row in answersList.Answers)
            {
                row.AnswerText = Regex.Replace(row.AnswerText, "<[^>]*>", " ");
            }
            return answersList;
        }

        /// <summary>
        /// Return the total score of the given answers
        /// </summary>
        /// <param name="answersIdValues">CSV of answer ids to get the total score from</param>
        /// <returns></returns>
        public int GetAnswersScoreTotal(string answersIdValues)
        {
            return AnswerFactory.Create().GetAnswersScoreTotal(answersIdValues);
        }

        /// <summary>
        /// returns the type mode of the answer
        /// </summary>
        public int GetAnswerTypeMode(int answerId)
        {
            return AnswerFactory.Create().GetAnswerTypeMode(answerId);
        }

        /// <summary>
        /// Get a list of all files associated with this guidto
        /// </summary>
        public FileData GetGuidFiles(string groupGuid)
        {
            return AnswerFactory.Create().GetGuidFiles(groupGuid);
        }

        /// <summary>
        /// Get a list of answers that can be subscribed to
        /// </summary>
        public AnswerData GetPublishersList(int answerId)
        {
            return AnswerFactory.Create().GetPublishersList(answerId);
        }

        /// <summary>
        /// Get a list of answers that are acting as a publisher
        /// in the question
        /// </summary>
        public AnswerData GetQuestionPublishersList(int questionId)
        {
            return AnswerFactory.Create().GetPublishersList(questionId);
        }

        /// <summary>
        /// Get all answers and randomize them
        /// </summary>
        /// <param name="questionId">question which is owning the answers</param>
        /// <param name="randomSeed"></param>
        /// <returns></returns>
        public AnswerData GetRandomAnswers(int questionId, int randomSeed, string languageCode)
        {
            Random random = new Random(randomSeed);
            AnswerData answers = AnswerFactory.Create().GetAnswers(questionId, languageCode);
            answers.EnforceConstraints = false;
            for (int i = 0; i < answers.Answers.Rows.Count; i++)
            {
                int index = random.Next(0, answers.Answers.Rows.Count - 1);
                answers.Answers.Rows.Add(answers.Answers.Rows[index].ItemArray);
                answers.Answers.Rows.RemoveAt(index);
            }
            answers.EnforceConstraints = true;
            return answers;
        }

        /// <summary>
        /// Get all answers for a given question that can be selected 
        /// </summary>
        /// <param name="questionId">question which is owning the answers</param>
        /// <returns></returns>
        public AnswerData GetSelectableAnswers(int questionId)
        {
            return AnswerFactory.Create().GetSelectableAnswers(questionId);
        }

        /// <summary>
        /// Get a list of answers to which the answer has subscribed to
        /// </summary>
        public AnswerData GetSubscriptionList(int answerId)
        {
            return AnswerFactory.Create().GetSubscriptionList(answerId);
        }

        /// <summary>
        /// Returns all the files that have not yet been validated
        /// </summary>
        public FileData GetUnValidatedFileAnswers(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            return AnswerFactory.Create().GetUnValidatedFileAnswers(surveyId, pageNumber, pageSize, out totalRecords);
        }

        /// <summary>
        /// Returns all the files that have already been validated
        /// </summary>
        public FileData GetValidatedFileAnswers(int surveyId, int pageNumber, int pageSize, out int totalRecords)
        {
            return AnswerFactory.Create().GetValidatedFileAnswers(surveyId, pageNumber, pageSize, out totalRecords);
        }

        /// <summary>
        /// Restores properties in the database
        /// </summary>
        public byte[] RestoreAnswerProperties(int answerId)
        {
            return AnswerFactory.Create().RestoreAnswerProperties(answerId);
        }
    }
}

