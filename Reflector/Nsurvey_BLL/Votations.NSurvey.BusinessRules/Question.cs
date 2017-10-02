namespace Votations.NSurvey.BusinessRules
{
    using Microsoft.VisualBasic;
    using System;
    using System.Data;
    using System.Text;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Contains the business rules that are used for a question.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Adds a new child question to the parent question specified by the parent questio id in the database
        /// </summary>
        /// <param name="newChildQuestion">Question object with information about what to add. Only Id must be ommited</param>
        public void AddChildQuestion(MatrixChildQuestionData newChildQuestion)
        {
            QuestionFactory.Create().AddChildQuestion(newChildQuestion);
        }

        /// <summary>
        /// Adds a matrix question to the library
        /// </summary>
        public QuestionData AddDefaultMatrixQuestion(int libraryId, string questionText,string questionIDText)
        {
            QuestionData defaultQuestion = this.GetDefaultQuestion();
            QuestionData.QuestionsRow row = defaultQuestion.Questions[0];
            row.LibraryId = libraryId;
            row.QuestionText = (questionText.Length > 0xf3c) ? questionText.Substring(0, 0xf3c) : questionText;
            row.DisplayOrder = 1;
            row.PageNumber = 1;
            row.SelectionModeId = 4;
            row.QuestionIdText = questionIDText;
            this.AddQuestion(defaultQuestion);
            return defaultQuestion;
        }

        /// <summary>
        /// Insert a matrix parent question with hardcoded default values
        /// </summary>
        /// <param name="surveyId">ID of the survey to which belongs the question</param>
        /// <param name="displayOrder">Order in which the question will be displayed</param>
        /// <param name="PageNumber">Page number to which the questions belongs</param>
        /// <param name="questionText">The main question's text</param>
        /// <returns>The full matrix parent question</returns>
        public QuestionData AddDefaultMatrixQuestion(int surveyId, int displayOrder, int pageNumber, string questionText,string questionIDText)
        {
            QuestionData defaultQuestion = this.GetDefaultQuestion();
            QuestionData.QuestionsRow row = defaultQuestion.Questions[0];
            row.SurveyId = surveyId;
            row.QuestionText = (questionText.Length > 0xf3c) ? questionText.Substring(0, 0xf3c) : questionText;
            row.DisplayOrder = displayOrder;
            row.QuestionIdText = questionIDText;
            row.PageNumber = pageNumber;
            row.SelectionModeId = 4;
            this.AddQuestion(defaultQuestion);
            return defaultQuestion;
        }

        /// <summary>
        /// Insert a single question to the library
        /// </summary>
        public QuestionData AddDefaultSingleQuestion(int libraryId, string questionText,string questionIdText)
        {
            QuestionData defaultQuestion = this.GetDefaultQuestion();
            QuestionData.QuestionsRow row = defaultQuestion.Questions[0];
            row.LibraryId = libraryId;

            //orig code: limited textlength 3900 - nvarchar(4000)
            //row.QuestionText = (questionText.Length > 0xf3c) ? questionText.Substring(0, 0xf3c) : questionText;

            //SP25: unlimited text lenght nvarchar(max)
            row.QuestionText = questionText;

            row.DisplayOrder = 1;
            row.PageNumber = 1;
            row.SelectionModeId = 1;
            row.LayoutModeId = 1;
            row.QuestionIdText = questionIdText;
            this.AddQuestion(defaultQuestion);
            return defaultQuestion;
        }

        /// <summary>
        /// Insert a single question with hardcoded default values
        /// </summary>
        /// <param name="surveyId">ID of the survey to which belongs the question</param>
        /// <param name="displayOrder">Order in which the question will be displayed</param>
        /// <param name="pageNumber">Page number to which the questions belongs</param>
        /// <param name="questionText">The question's text</param>
        /// <returns>The full static question</returns>
        public QuestionData AddDefaultSingleQuestion(int surveyId, int displayOrder, int pageNumber, string questionText, string questionIdText)
        {
            QuestionData defaultQuestion = this.GetDefaultQuestion();
            QuestionData.QuestionsRow row = defaultQuestion.Questions[0];
            row.SurveyId = surveyId;

            //orig code: limited textlength 3900 - nvarchar(4000)
            //row.QuestionText = (questionText.Length > 0xf3c) ? questionText.Substring(0, 0xf3c) : questionText;

            //SP25: unlimited text lenght nvarchar(max)
            row.QuestionText = questionText;

            row.DisplayOrder = displayOrder;
            row.PageNumber = pageNumber;
            row.SelectionModeId = 1;
            row.LayoutModeId = 1;
            row.QuestionIdText = questionIdText;
            this.AddQuestion(defaultQuestion);
            return defaultQuestion;
        }

        /// <summary>
        /// Adds a new question to the poll specified by the poll id in the database
        /// </summary>
        /// <param name="newQuestion">Question object with information about what to add. Only Id must be ommited</param>
        public void AddQuestion(QuestionData newQuestion)
        {
            QuestionFactory.Create().AddQuestion(newQuestion);
        }

        /// <summary>
        /// Adds a new answer to be shown in the section grid
        /// </summary>
        public void AddQuestionSectionGridAnswers(int questionId, int answerId)
        {
            QuestionFactory.Create().AddQuestionSectionGridAnswers(questionId, answerId);
        }

        /// <summary>
        /// Add a new skip logic rule
        /// </summary>
        public void AddSkipLogicRule(SkipLogicRuleData newSkipLogicRule)
        {
            QuestionFactory.Create().AddSkipLogicRule(newSkipLogicRule);
        }

        /// <summary>
        /// Insert a new static question to the library
        /// </summary>
        public QuestionData AddStaticInformationText(int libraryId, string informationText,string questionIDText)
        {
            QuestionData defaultQuestion = this.GetDefaultQuestion();
            QuestionData.QuestionsRow row = defaultQuestion.Questions[0];
            row.LibraryId = libraryId;

            //row.QuestionText = (informationText.Length > 0xf3c) ? informationText.Substring(0, 0xf3c) : informationText;

            row.QuestionText = informationText;

            row.DisplayOrder = 1;
            row.PageNumber = 1;
            row.SelectionModeId = 5;
            row.QuestionIdText = questionIDText;
            this.AddQuestion(defaultQuestion);
            return defaultQuestion;
        }

        /// <summary>
        /// Insert a new static question 
        /// </summary>
        /// <param name="surveyId">ID of the survey to which belongs the question</param>
        /// <param name="displayOrder">Order in which the question will be displayed</param>
        /// <param name="informationText">Static text</param>
        /// <returns>The full static question</returns>
        public QuestionData AddStaticInformationText(int surveyId, int displayOrder, int pageNumber, string informationText, string questionIdText)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            QuestionData defaultQuestion = this.GetDefaultQuestion();
            QuestionData.QuestionsRow row = defaultQuestion.Questions[0];
            row.SurveyId = surveyId;

            row.QuestionText = informationText;

            row.DisplayOrder = displayOrder;
            row.PageNumber = pageNumber;
            row.SelectionModeId = 5;
            row.QuestionIdText = questionIdText;
            this.AddQuestion(defaultQuestion);
            return defaultQuestion;
        }

        /// <summary>
        /// Check if the user has this question assigned
        /// </summary>
        public bool CheckQuestionUser(int questionId, int userId)
        {
            return QuestionFactory.Create().CheckQuestionUser(questionId, userId);
        }

        /// <summary>
        /// Clones a question and its answers 
        /// and returns the cloned question object 
        /// </summary>
        /// <param name="questionId">Id of the question you want to clone</param>
        /// <returns>A questiondata object with the cloned question</returns>
        public QuestionData CloneQuestionById(int questionId)
        {
            return QuestionFactory.Create().CloneQuestionById(questionId);
        }

        /// <summary>
        /// Copy a question to the given target survey
        /// </summary>
        public void CopyQuestionById(int questionId, int targetSurveyId, int targetDisplayOrder, int targetPageNumber)
        {
            QuestionFactory.Create().CopyQuestionById(questionId, targetSurveyId, targetDisplayOrder, targetPageNumber);
            //return QuestionFactory.Create().CopyQuestionById(questionId, targetSurveyId, targetDisplayOrder, targetPageNumber);
        }

        /// <summary>
        /// Copy a question to the library
        /// </summary>
        public void CopyQuestionToLibrary(int questionId, int libraryId)
        {
            //return QuestionFactory.Create().CopyQuestionToLibrary(questionId, libraryId);
            QuestionFactory.Create().CopyQuestionToLibrary(questionId, libraryId);
        }

        /// <summary>
        /// Remove all the matrix answers from the database
        /// </summary>
        /// <param name="parentQuestionId">Matrix question id</param>
        public void DeleteMatrixAnswers(int parentQuestionId)
        {
            QuestionFactory.Create().DeleteMatrixAnswers(parentQuestionId);
        }

        /// <summary>
        /// Remove the question and all its answers from the database
        /// </summary>
        /// <param name="questionId">Question to delete from the database</param>
        public void DeleteQuestionById(int questionId)
        {
            QuestionFactory.Create().DeleteQuestionById(questionId);
        }

        /// <summary>
        /// Delete an answer to be shown in the section grid
        /// </summary>
        public void DeleteQuestionSectionGridAnswers(int questionId, int answerId)
        {
            QuestionFactory.Create().DeleteQuestionSectionGridAnswers(questionId, answerId);
        }

        /// <summary>
        /// Deletes the question's sections option
        /// </summary>
        public void DeleteQuestionSectionOptions(int questionId)
        {
            QuestionFactory.Create().DeleteQuestionSectionOptions(questionId);
        }

        /// <summary>
        /// Deletes the skip logic rule
        /// </summary>
        public void DeleteSkipLogicRuleById(int skipLogicRuleId)
        {
            QuestionFactory.Create().DeleteSkipLogicRuleById(skipLogicRuleId);
        }

        /// <summary>
        /// Creates a new question entity with all
        /// required default values 
        /// </summary>
        /// <returns></returns>
        private QuestionData GetDefaultQuestion()
        {
            QuestionData data = new QuestionData();
            QuestionData.QuestionsRow row = data.Questions.NewQuestionsRow();
            row.ColumnsNumber = 0;
            row.MinSelectionRequired = 0;
            row.MaxSelectionAllowed = 0;
            row.RandomizeAnswers = false;
            row.RatingEnabled = false;
            row.QuestionPipeAlias = null;
            data.Questions.AddQuestionsRow(row);
            return data;
        }

        public int GetQuestionAnswersScore(int questionId, VoterAnswersData votersAnswers)
        {
            StringBuilder builder = new StringBuilder();
            VoterAnswersData.VotersAnswersRow[] rowArray = (VoterAnswersData.VotersAnswersRow[]) votersAnswers.VotersAnswers.Select("QuestionId=" + questionId);
            int num = 0;
            foreach (VoterAnswersData.VotersAnswersRow row in rowArray)
            {
                builder.Append(row.AnswerId);
                if ((num + 1) < rowArray.Length)
                {
                    builder.Append(',');
                }
                num++;
            }
            return new Answers().GetAnswersScoreTotal(builder.ToString());
        }

        /// <summary>
        /// Import the given questions into the DB
        /// </summary>
        public void ImportQuestions(NSurveyQuestion importQuestions, int userId)
        {
            IQuestion question = QuestionFactory.Create();
            this.TurnOverPrimaryKeys(importQuestions);
            question.ImportQuestions(importQuestions, userId);
        }

        /// <summary>
        /// Move the give question's display position down
        /// </summary>
        /// <param name="questionId">ID of the question to change the position</param>
        public void MoveQuestionPositionDown(int questionId)
        {
            QuestionFactory.Create().MoveQuestionPositionDown(questionId);
        }

        /// <summary>
        /// Move the give question's display position up
        /// </summary>
        /// <param name="questionId">ID of the question to change the position</param>
        public void MoveQuestionPositionUp(int questionId)
        {
            QuestionFactory.Create().MoveQuestionPositionUp(questionId);
        }

        /// <summary>
        /// Check if we need to hide / skip the question depending on the
        /// skip logic rules rules
        /// </summary>
        public bool SkipQuestion(int questionId, VoterAnswersData voterAnswers, bool evaluateScores)
        {
            SkipLogicRuleData questionSkipLogicRules = new Questions().GetQuestionSkipLogicRules(questionId);
            if (questionSkipLogicRules.SkipLogicRules.Rows.Count <= 0)
            {
                return false;
            }
            DataRow[] rowArray = null;
            foreach (SkipLogicRuleData.SkipLogicRulesRow row in questionSkipLogicRules.SkipLogicRules.Rows)
            {
                int num = evaluateScores ? this.GetQuestionAnswersScore(row.QuestionId, voterAnswers) : 0;
                if ((evaluateScores && (row.ConditionalOperator == 3)) && (num == row.Score))
                {
                    return true;
                }
                if ((evaluateScores && (row.ConditionalOperator == 4)) && (num < row.Score))
                {
                    return true;
                }
                if ((evaluateScores && (row.ConditionalOperator == 5)) && (num > row.Score))
                {
                    return true;
                }
                if ((evaluateScores && (row.ConditionalOperator == 6)) && ((num >= row.Score) && (num <= row.ScoreMax)))
                {
                    return true;
                }
                if (!row.IsAnswerIdNull() && !row.IsTextFilterNull())
                {
                    string expression = new Survey().EscapeFilterString(row.TextFilter);
                    if ((!row.IsExpressionOperatorNull() && (row.ExpressionOperator != 2)) && Information.IsDate(expression))
                    {
                        expression = "#" + expression + "#";
                    }
                    else if ((row.ExpressionOperator != 2) && !Information.IsNumeric(expression))
                    {
                        expression = "'" + expression + "'";
                    }
                    try
                    {
                        switch (row.ExpressionOperator)
                        {
                            case 1:
                                rowArray = voterAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText = ", expression }));
                                goto Label_02FD;

                            case 3:
                                rowArray = voterAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText > ", expression }));
                                goto Label_02FD;

                            case 4:
                                rowArray = voterAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText < ", expression }));
                                goto Label_02FD;
                        }
                        rowArray = voterAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText like '%", expression, "%'" }));
                    }
                    catch (EvaluateException)
                    {
                    }
                }
                else if (!row.IsAnswerIdNull())
                {
                    rowArray = voterAnswers.VotersAnswers.Select("AnswerId=" + row.AnswerId);
                }
                else if (row.IsAnswerIdNull())
                {
                    rowArray = voterAnswers.VotersAnswers.Select("QuestionId=" + row.QuestionId);
                }
                else
                {
                    rowArray = null;
                }
            Label_02FD:
                if ((((rowArray != null) && (row.ConditionalOperator == 1)) && (rowArray.Length > 0)) || ((row.ConditionalOperator == 2) && (rowArray.Length == 0)))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Invert main table keys signs to avoid key collisions on insertion
        /// of new row with existing keys in the dataset that have not 
        /// yet been updated with their "live" DB key.
        /// Changes will casade to child tables
        /// </summary>
        private void TurnOverPrimaryKeys(NSurveyQuestion importQuestions)
        {
            foreach (NSurveyQuestion.AnswerTypeRow row in importQuestions.AnswerType)
            {
                row.AnswerTypeID = -row.AnswerTypeID;
            }
            foreach (NSurveyQuestion.RegularExpressionRow row2 in importQuestions.RegularExpression)
            {
                row2.RegularExpressionId = -row2.RegularExpressionId;
            }
            foreach (NSurveyQuestion.QuestionRow row3 in importQuestions.Question)
            {
                row3.QuestionId = -row3.QuestionId;
            }
            foreach (NSurveyQuestion.AnswerRow row4 in importQuestions.Answer)
            {
                row4.AnswerId = -row4.AnswerId;
            }
        }

        /// <summary>
        /// Update the child question in the database
        /// </summary>
        /// <param name="updatedChildQuestion">question to update, must contain the question id</param>
        public void UpdateChildQuestion(MatrixChildQuestionData updatedChildQuestion, string languageCode)
        {
            QuestionFactory.Create().UpdateChildQuestion(updatedChildQuestion, languageCode);
        }

        /// <summary>
        /// Update the question in the database
        /// </summary>
        /// <param name="updatedQuestion">question to update, must contain the question id</param>
        public void UpdateQuestion(QuestionData updatedQuestion, string languageCode)
        {
            QuestionFactory.Create().UpdateQuestion(updatedQuestion, languageCode);
        }

        /// <summary>
        /// updates a section options, creates it if it doesnt exists
        /// </summary>
        public void UpdateQuestionSectionOptions(QuestionSectionOptionData sectionOptions, string languageCode)
        {
            QuestionFactory.Create().UpdateQuestionSectionOptions(sectionOptions, languageCode);
        }
    }
}

