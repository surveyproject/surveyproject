namespace Votations.NSurvey.DataAccess
{
    using System;
    using System.Data;
    using System.Text.RegularExpressions;
    using Votations.NSurvey;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the methods to access questions database data
    /// </summary>
    public class Questions
    {
        /// <summary>
        /// Returns a question list that can be answered 
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableQuestionList(int surveyId)
        {
            QuestionData answerableQuestionList = QuestionFactory.Create().GetAnswerableQuestionList(surveyId);
            this.ParseHTMLTagsFromQuestionText(answerableQuestionList, 80);
            return answerableQuestionList;
        }

        /// <summary>
        /// Returns a question list of the given page that can be answered 
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableQuestionList(int surveyId, int pageNumber)
        {
            QuestionData answerableQuestionList = QuestionFactory.Create().GetAnswerableQuestionList(surveyId, pageNumber);
            this.ParseHTMLTagsFromQuestionText(answerableQuestionList, 80);
            return answerableQuestionList;
        }

        /// <summary>
        /// Returns a question list of the given page range that can be answered 
        /// </summary>
        public QuestionData GetAnswerableQuestionListInPageRange(int surveyId, int startPageNumber, int endPageNumber)
        {
            QuestionData questions = QuestionFactory.Create().GetAnswerableQuestionListInPageRange(surveyId, startPageNumber, endPageNumber);
            this.ParseHTMLTagsFromQuestionText(questions, 80);
            return questions;
        }

        /// <summary>
        /// Returns a question list that can be answered without their child questions
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableQuestionListWithoutChilds(int surveyId)
        {
            QuestionData answerableQuestionListWithoutChilds = QuestionFactory.Create().GetAnswerableQuestionListWithoutChilds(surveyId);
            this.ParseHTMLTagsFromQuestionText(answerableQuestionListWithoutChilds, 80);
            return answerableQuestionListWithoutChilds;
        }

        /// <summary>
        /// Returns the questions that can be answered without their childs
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableQuestionWithoutChilds(int surveyId)
        {
            return QuestionFactory.Create().GetAnswerableQuestionWithoutChilds(surveyId);
        }

        /// <summary>
        /// Returns a question list that can be answered and that don't have any questions
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetAnswerableSingleQuestionListWithoutChilds(int surveyId)
        {
            QuestionData answerableSingleQuestionListWithoutChilds = QuestionFactory.Create().GetAnswerableSingleQuestionListWithoutChilds(surveyId);
            this.ParseHTMLTagsFromQuestionText(answerableSingleQuestionListWithoutChilds, 80);
            return answerableSingleQuestionListWithoutChilds;
        }

        /// <summary>
        /// returns a results set with the compare question's answers number of voter 
        /// that have also answered the base question answer
        /// </summary>
        public DataSet GetCrossTabResults(int compareQuestionId, int baseAnswerId)
        {
            return QuestionFactory.Create().GetCrossTabResults(compareQuestionId, baseAnswerId);
        }

        /// <summary>
        /// Returns a question list for the given library 
        /// that can be answered and that don't have any child questions
        /// </summary>
        /// <returns>A question object collection</returns>
        public QuestionData GetLibraryAnswerableSingleQuestionListWithoutChilds(int libraryId)
        {
            QuestionData libraryAnswerableSingleQuestionListWithoutChilds = QuestionFactory.Create().GetLibraryAnswerableSingleQuestionListWithoutChilds(libraryId);
            this.ParseHTMLTagsFromQuestionText(libraryAnswerableSingleQuestionListWithoutChilds, 80);
            return libraryAnswerableSingleQuestionListWithoutChilds;
        }

        /// <summary>
        /// Returns all question listed in the library
        /// </summary>
        public QuestionData GetLibraryQuestionList(int libraryId)
        {
            QuestionData libraryQuestionList = QuestionFactory.Create().GetLibraryQuestionList(libraryId);
            this.ParseHTMLTagsFromQuestionText(libraryQuestionList, 80);
            return libraryQuestionList;
        }

        /// <summary>
        /// Returns all question listed in the library without their child questions
        /// </summary>
        public QuestionData GetLibraryQuestionListWithoutChilds(int libraryId)
        {
            QuestionData libraryQuestionListWithoutChilds = QuestionFactory.Create().GetLibraryQuestionListWithoutChilds(libraryId);
            this.ParseHTMLTagsFromQuestionText(libraryQuestionListWithoutChilds, 80);
            return libraryQuestionListWithoutChilds;
        }

        /// <summary>
        /// Returns all question listed in the library
        /// </summary>
        public QuestionData GetLibraryQuestions(int libraryId,string languageCode)
        {
            return QuestionFactory.Create().GetLibraryQuestions(libraryId,languageCode);
        }

        /// <summary>
        /// Returns all child question from the given question
        /// </summary>
        /// <param name="parentQuestionId">Parent question id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public MatrixChildQuestionData GetMatrixChildQuestions(int parentQuestionId, string languageCode)
        {
            return QuestionFactory.Create().GetMatrixChildQuestions(parentQuestionId, languageCode);
        }

        /// <summary>
        /// Returns the question until next page break is encountered
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the paged questions</param>
        /// <param name="pageNumber">Page to retrieve</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetPagedQuestions(int surveyId, int pageNumber, string languageCode)
        {
            return QuestionFactory.Create().GetPagedQuestions(surveyId, pageNumber, languageCode);
        }

        /// <summary>
        /// Returns all question's answer subscribers
        /// </summary>
        public AnswerConnectionData GetQuestionAnswerConnections(int questionId)
        {
            return QuestionFactory.Create().GetQuestionAnswerConnections(questionId);
        }

        /// <summary>
        /// Return a question object that reflects the database question
        /// </summary>
        /// <param name="questionId">Id of the question you need</param>
        /// <returns>A questiondata object with the current database values</returns>
        public QuestionData GetQuestionById(int questionId, string languageCode)
        {
            QuestionData questionById = QuestionFactory.Create().GetQuestionById(questionId, languageCode);
            if (questionById.Questions.Rows.Count == 0)
            {
                throw new QuestionNotFoundException();
            }
            return questionById;
        }

        /// <summary>
        /// Returns all question details, answers and answer types
        /// </summary>
        public NSurveyQuestion GetQuestionForExport(int questionId)
        {
            NSurveyQuestion questionForExport = QuestionFactory.Create().GetQuestionForExport(questionId);
            int num = 1;
            foreach (NSurveyQuestion.QuestionRow row in questionForExport.Question)
            {
                row.QuestionId = num;
                num++;
            }
            return questionForExport;
        }

        /// <summary>
        /// Returns all question including their childs
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions and childs</param>
        /// <returns>A question object collection with all their childs</returns>
        public QuestionData GetQuestionHierarchy(int surveyId)
        {
            return QuestionFactory.Create().GetQuestionHierarchy(surveyId);
        }

        /// <summary>
        /// Return the layout modes available
        /// </summary>
        /// <returns>A questionlayoutmodedata object with the current layout values</returns>
        public LayoutModeData GetQuestionLayoutModes()
        {
            return QuestionFactory.Create().GetQuestionLayoutModes();
        }

        /// <summary>
        /// Returns a question list of all available questions
        /// in the given page range
        /// </summary>
        public QuestionData GetQuestionListForPageRange(int surveyId, int startPageNumber, int endPageNumber)
        {
            return QuestionFactory.Create().GetQuestionListForPageRange(surveyId, startPageNumber, endPageNumber);
        }

        /// <summary>
        /// Returns a question list with only text, questionid and display order field 
        /// from the given survey that have at leat one selectable answer type
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetQuestionListWithSelectableAnswers(int surveyId)
        {
            QuestionData questionListWithSelectableAnswers = QuestionFactory.Create().GetQuestionListWithSelectableAnswers(surveyId);
            this.ParseHTMLTagsFromQuestionText(questionListWithSelectableAnswers, 80);
            return questionListWithSelectableAnswers;
        }

        /// <summary>
        /// Returns a question list of the given page with only text, questionid and display order field 
        /// from the given survey that have at leat one selectable answer type
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetQuestionListWithSelectableAnswers(int surveyId, int pageNumber)
        {
            QuestionData questionListWithSelectableAnswers = QuestionFactory.Create().GetQuestionListWithSelectableAnswers(surveyId, pageNumber);
            this.ParseHTMLTagsFromQuestionText(questionListWithSelectableAnswers, 80);
            return questionListWithSelectableAnswers;
        }

        /// <summary>
        /// Returns the question and its answers results
        /// </summary>
        public QuestionResultsData GetQuestionResults(int questionId, int filterId, string sortOrder, string languageCode, DateTime startDate, DateTime endDate)
        {
            QuestionResultsData data = QuestionFactory.Create().GetQuestionResults(questionId, filterId, sortOrder, languageCode, startDate, endDate);
            if (data.Questions.Rows.Count == 0)
            {
                throw new QuestionNotFoundException();
            }
            foreach (QuestionResultsData.QuestionsRow row in data.Questions)
            {
                if (row.QuestionText != null)
                {
                    row.QuestionText = Regex.Replace(row.QuestionText, "<[^>]*>", " ");
                    if (row.QuestionText.Length > 40)
                    {
                        row.QuestionText = row.QuestionText.Substring(0, 40) + "...";
                    }
                }
                if (row.ParentQuestionText != null)
                {
                    row.ParentQuestionText = Regex.Replace(row.ParentQuestionText, "<[^>]*>", " ");
                    if (row.ParentQuestionText.Length > 40)
                    {
                        row.ParentQuestionText = row.ParentQuestionText.Substring(0, 40) + "...";
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// Returns all question from the given survey with title
        /// cleaned up from any html tag
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetQuestions(int surveyId, bool removeHtmlFromTitle)
        {
            QuestionData questions = QuestionFactory.Create().GetQuestions(surveyId, null);
            if (removeHtmlFromTitle)
            {
                foreach (QuestionData.QuestionsRow row in questions.Questions)
                {
                    row.QuestionText = Regex.Replace(row.QuestionText, "<[^>]*>", " ");
                }
            }
            return questions;
        }

        /// <summary>
        /// Returns all question from the given survey
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetQuestions(int surveyId, string languageCode)
        {
            return QuestionFactory.Create().GetQuestions(surveyId, languageCode);
        }

        /// <summary>
        /// Returns all question and all their answers from the given survey
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the questions and answers</param>
        /// <returns>A question object collection with all answers</returns>
        public QuestionsAnswersData GetQuestionsAnswers(int surveyId)
        {
            return QuestionFactory.Create().GetQuestionsAnswers(surveyId);
        }

        /// <summary>
        /// Get all answerids to show in the grid
        /// </summary>
        public int[] GetQuestionSectionGridAnswers(int questionId)
        {
            return QuestionFactory.Create().GetQuestionSectionGridAnswers(questionId);
        }

        /// <summary>
        /// Return the options available for the question's section
        /// </summary>
        public QuestionSectionOptionData GetQuestionSectionOptions(int questionId, string languageCode)
        {
            return QuestionFactory.Create().GetQuestionSectionOptions(questionId, languageCode);
        }

        /// <summary>
        /// Retrieves the skip logic rules for this question
        /// </summary>
        public SkipLogicRuleData GetQuestionSkipLogicRules(int questionId)
        {
            return QuestionFactory.Create().GetQuestionSkipLogicRules(questionId);
        }

        /// <summary>
        /// Get the questions until next page break is encountered and 
        /// return them in a random them
        /// </summary>
        /// <param name="surveyId">Survey id from which you want to retrieve the paged questions</param>
        /// <param name="pageNumber">Page to retrieve</param>
        /// <returns>A question object collection</returns>
        public QuestionData GetRandomPagedQuestions(int surveyId, int pageNumber, int randomSeed, string languageCode)
        {
            Random random = new Random(randomSeed);
            QuestionData data = this.GetPagedQuestions(surveyId, pageNumber, languageCode);
            data.EnforceConstraints = false;
            for (int i = 0; i < data.Questions.Rows.Count; i++)
            {
                int index = random.Next(0, data.Questions.Rows.Count - 1);
                data.Questions.Rows.Add(data.Questions.Rows[index].ItemArray);
                data.Questions.Rows.RemoveAt(index);
            }
            data.EnforceConstraints = true;
            return data;
        }

        /// <summary>
        /// Return the selection mode available for a singe question type
        /// </summary>
        /// <returns>A questionselectionmode object with the current selections options</returns>
        public QuestionSelectionModeData GetSelectableQuestionSelectionModes()
        {
            return QuestionFactory.Create().GetSelectableQuestionSelectionModes();
        }

        /// <summary>
        /// returns a results set with the total of compare question's answers number of voter 
        /// that have answered or not answered the base question answers
        /// </summary>
        public DataSet GetTotalCrossTabResults(int compareQuestionId, int baseQuestionId)
        {
            return QuestionFactory.Create().GetTotalCrossTabResults(compareQuestionId, baseQuestionId);
        }

        /// <summary>
        /// returns a results set with the compare question's answers number of voter 
        /// that have not answered the base question answers
        /// </summary>
        public DataSet GetUnansweredCrossTabResults(int compareQuestionId, int baseQuestionId)
        {
            return QuestionFactory.Create().GetUnansweredCrossTabResults(compareQuestionId, baseQuestionId);
        }

        /// <summary>
        /// Parse HTML tags from the question text
        /// </summary>
        public string ParseHTMLTagsFromQuestionText(string questionText, int maxTextLength)
        {
            string str = Regex.Replace(questionText, "<[^>]*>", " ");
            if (str.Length > maxTextLength)
            {
                str = str.Substring(0, maxTextLength) + "...";
            }
            return str;
        }

        /// <summary>
        /// Parse HTML tags from the question collection
        /// </summary>
        public void ParseHTMLTagsFromQuestionText(QuestionData questions, int maxTextLength)
        {
            foreach (QuestionData.QuestionsRow row in questions.Questions)
            {
                if (row.QuestionText != null)
                {
                    row.QuestionText = Regex.Replace(row.QuestionText, "<[^>]*>", " ");
                    if (row.QuestionText.Length > maxTextLength)
                    {
                        row.QuestionText = row.QuestionText.Substring(0, maxTextLength) + "...";
                    }
                }
            }
        }

        public void MoveQuestionUp(int questionId)
        {
            QuestionFactory.Create().MoveQuestionUp(questionId);
        }

        public void MoveQuestionDown(int questionId)
        {
            QuestionFactory.Create().MoveQuestionDown(questionId);
        }
    }
}

