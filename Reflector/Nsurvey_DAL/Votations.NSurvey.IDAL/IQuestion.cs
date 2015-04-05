namespace Votations.NSurvey.IDAL
{
    using System;
    using System.Data;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Interface for the question DAL.
    /// </summary>
    public interface IQuestion
    {
        void AddChildQuestion(MatrixChildQuestionData newChildQuestion);
        void AddQuestion(QuestionData newQuestion);
        void AddQuestionSectionGridAnswers(int questionId, int answerId);
        void AddSkipLogicRule(SkipLogicRuleData newSkipLogicRule);
        bool CheckQuestionUser(int questionId, int userId);
        QuestionData CloneQuestionById(int questionId);
        void CopyQuestionById(int questionId, int targetSurveyId, int targetDisplayOrder, int targetPageNumber);
        void CopyQuestionToLibrary(int questionId, int libraryId);
        void DeleteMatrixAnswers(int parentQuestionId);
        void DeleteQuestionById(int id);
        void DeleteQuestionSectionGridAnswers(int questionId, int answerId);
        void DeleteQuestionSectionOptions(int questionId);
        void DeleteSkipLogicRuleById(int skipLogicRuleId);
        QuestionData GetAnswerableQuestionList(int surveyId);
        QuestionData GetAnswerableQuestionList(int surveyId, int pageNumber);
        QuestionData GetAnswerableQuestionListInPageRange(int surveyId, int startPageNumber, int endPageNumber);
        QuestionData GetAnswerableQuestionListWithoutChilds(int surveyId);
        QuestionData GetAnswerableQuestionWithoutChilds(int surveyId);
        QuestionData GetAnswerableSingleQuestionListWithoutChilds(int surveyId);
        DataSet GetCrossTabResults(int compareQuestionId, int baseAnswerId);
        QuestionData GetLibraryAnswerableSingleQuestionListWithoutChilds(int libraryId);
        QuestionData GetLibraryQuestionList(int libraryId);
        QuestionData GetLibraryQuestionListWithoutChilds(int libraryId);
        QuestionData GetLibraryQuestions(int libraryId,string languageCode);
        MatrixChildQuestionData GetMatrixChildQuestions(int parentQuestionId, string languageCode);
        QuestionData GetPagedQuestions(int surveyId, int pageNumber, string languageCode);
        AnswerConnectionData GetQuestionAnswerConnections(int questionId);
        QuestionData GetQuestionById(int id, string languageCode);
        NSurveyQuestion GetQuestionForExport(int questionId);
        QuestionData GetQuestionHierarchy(int surveyId);
        LayoutModeData GetQuestionLayoutModes();
        QuestionData GetQuestionListForPageRange(int surveyId, int startPageNumber, int endPageNumber);
        QuestionData GetQuestionListWithSelectableAnswers(int surveyId);
        QuestionData GetQuestionListWithSelectableAnswers(int surveyId, int pageNumber);
        QuestionResultsData GetQuestionResults(int questionId, int filterId, string sortOrder, string languageCode, DateTime startDate, DateTime endDate);
        QuestionData GetQuestions(int surveyId, string languageCode);
        QuestionsAnswersData GetQuestionsAnswers(int surveyId);
        int[] GetQuestionSectionGridAnswers(int questionId);
        QuestionSectionOptionData GetQuestionSectionOptions(int questionId, string languageCode);
        SkipLogicRuleData GetQuestionSkipLogicRules(int questionId);
        QuestionSelectionModeData GetSelectableQuestionSelectionModes();
        DataSet GetTotalCrossTabResults(int compareQuestionId, int baseQuestionId);
        DataSet GetUnansweredCrossTabResults(int compareQuestionId, int baseQuestionId);
        void ImportQuestions(NSurveyQuestion importQuestions, int userId);
        void MoveQuestionPositionDown(int id);
        void MoveQuestionPositionUp(int id);
        void UpdateChildQuestion(MatrixChildQuestionData updatedChildQuestion, string languageCode);
        void UpdateQuestion(QuestionData updatedQuestion, string languageCode);
        void UpdateQuestionSectionOptions(QuestionSectionOptionData sectionOptions, string languageCode);
        void MoveQuestionUp(int questionId);
        void MoveQuestionDown(int questionId);
    }
}

