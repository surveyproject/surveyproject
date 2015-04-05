namespace Votations.NSurvey.IDAL
{
    using System;
    using System.Data;
    using Votations.NSurvey.Data;

    public enum QuestionGroupDisplayOrder{Down = 0, Up = 1}

    public interface IQuestionGroup
    {

        QuestionGroupsData GetAll(string langCode);
        QuestionGroupsData GetByQuestionId(int questionId);
        void AddNewGroup(string name, int? parentGroupId, string langId);
        void UpdateDisplayOrder(int questionGroupId, QuestionGroupDisplayOrder order);
        void UpdateGroup(int questionGroupId, int? parentGroupId, string name, string langId);
        void DeleteGroup(int questionGroupId);
    }
}
