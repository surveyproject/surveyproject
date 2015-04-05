namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Interface for the answer DAL.
    /// </summary>
    public interface IAnswerType
    {
        void AddAnswerType(AnswerTypeData newAnswerType, int userId);
        void AssignAnswerTypeToUser(int answerTypeId, int userId);
        void DeleteAnswerType(int answerTypeId);
        AnswerTypeData GetAnswerTypeById(int answerTypeId);
        AnswerTypeData GetAnswerTypes();
        AnswerTypeData GetAnswerTypesList();
        AnswerTypeData GetAssignedAnswerTypesList(int userId, int surveyId);
        AnswerTypeData GetEditableAnswerTypesList();
        AnswerTypeData GetEditableAssignedAnswerTypesList(int userId);
        bool IsAnswerTypeInUse(int answerTypeId);
        void SetBuiltInAnswerType(int answerTypeId);
        void UpdateAnswerType(AnswerTypeData updatedAnswerType);
    }
}

