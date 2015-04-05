using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Votations.NSurvey.Data;
using Votations.NSurvey.IDAL;
using Votations.NSurvey.DALFactory;

namespace Votations.NSurvey.DataAccess
{
    public class QuestionGroups
    {
        public QuestionGroupsData GetAll(string langId)
        {
            QuestionGroupsData data = QuestionGroupFactory.Create().GetAll(langId);
            return data;
        }

        public QuestionGroupsData GetByQuestionId(int questionId)
        {
            QuestionGroupsData data = QuestionGroupFactory.Create().GetByQuestionId(questionId);
            return data;
        }

        public void AddNewGroup(string name, int? parentGroupId, string langId)
        {
            QuestionGroupFactory.Create().AddNewGroup(name, parentGroupId, langId);
            
        }

        public void UpdateGroup(int questionGroupID, int? parentGroupId, string name, string langId)
        {
            QuestionGroupFactory.Create().UpdateGroup(questionGroupID, parentGroupId, name, langId);            
        }

        public void UpdateDisplayOrder(int questionGroupId, QuestionGroupDisplayOrder order)
        {
            QuestionGroupFactory.Create().UpdateDisplayOrder(questionGroupId, order);            
        }

        public void DeleteGroup(int questionGroupId)
        {
            QuestionGroupFactory.Create().DeleteGroup(questionGroupId);            
        }
    }
}
