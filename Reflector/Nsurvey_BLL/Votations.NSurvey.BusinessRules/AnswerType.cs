namespace Votations.NSurvey.BusinessRules
{
    using System;
    using Votations.NSurvey;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Contains the business rules that are used for an answer type.
    /// </summary>
    public class AnswerType
    {
        /// <summary>
        /// Adds a new answer type in the database
        /// </summary>
        /// <param name="newAnswerType">Answer type object with information about what to add. Only Id must be ommited</param>
        public void AddAnswerType(AnswerTypeData newAnswerType, int userId)
        {
            AnswerTypeFactory.Create().AddAnswerType(newAnswerType, userId);
        }

        /// <summary>
        /// Assign the answer type to the user
        /// </summary>
        public void AssignAnswerTypeToUser(int answerTypeId, int userId)
        {
            AnswerTypeFactory.Create().AssignAnswerTypeToUser(answerTypeId, userId);
        }

        /// <summary>
        /// Remove the answer type from the database
        /// </summary>
        /// <param name="answerTypeId">Answer type to delete from the database</param>
        public void DeleteAnswerType(int answerTypeId)
        {
            IAnswerType type = AnswerTypeFactory.Create();
            if (type.IsAnswerTypeInUse(answerTypeId))
            {
                throw new AnswerTypeInUseException();
            }
            type.DeleteAnswerType(answerTypeId);
        }

        /// <summary>
        /// Makes the answer type as builtin
        /// </summary>
        public void SetBuiltInAnswerType(int answerTypeId)
        {
            AnswerTypeFactory.Create().SetBuiltInAnswerType(answerTypeId);
        }

        /// <summary>
        /// Update the answer type in the database
        /// </summary>
        /// <param name="updatedAnswerType">Answer type to update, must specify the answer type id</param>
        public void UpdateAnswerType(AnswerTypeData updatedAnswerType)
        {
            AnswerTypeFactory.Create().UpdateAnswerType(updatedAnswerType);
        }
    }
}

