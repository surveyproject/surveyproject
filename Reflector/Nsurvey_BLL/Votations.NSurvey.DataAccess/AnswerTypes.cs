namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the methods to access answer types database data.
    /// </summary>
    public class AnswerTypes
    {
        /// <summary>
        /// Return an answer type object that reflects the database answer type
        /// </summary>
        /// <param name="answerTypeId">Id of the answer type you need</param>
        /// <returns>An answer type object with the current database values</returns>
        public AnswerTypeData GetAnswerTypeById(int answerTypeId)
        {
            return AnswerTypeFactory.Create().GetAnswerTypeById(answerTypeId);
        }

        /// <summary>
        /// Returns all the answer type available
        /// </summary>
        public AnswerTypeData GetAnswerTypes()
        {
            return AnswerTypeFactory.Create().GetAnswerTypes();
        }

        /// <summary>
        /// Returns a list of answer types list
        /// </summary>
        public AnswerTypeData GetAnswerTypesList()
        {
            return AnswerTypeFactory.Create().GetAnswerTypesList();
        }

        /// <summary>
        /// Returns a list of answer types available to the user
        /// </summary>
        public AnswerTypeData GetAssignedAnswerTypesList(int userId, int surveyId)
        {
            return AnswerTypeFactory.Create().GetAssignedAnswerTypesList(userId, surveyId);
        }

        /// <summary>
        /// Returns a list of answer types that can be edited from the
        /// admin interface
        /// </summary>
        public AnswerTypeData GetEditableAnswerTypesList()
        {
            return AnswerTypeFactory.Create().GetEditableAnswerTypesList();
        }

        /// <summary>
        /// Returns a list of answer types available to the user and that can 
        /// be edited from the admin interface
        /// </summary>
        public AnswerTypeData GetEditableAssignedAnswerTypesList(int userId)
        {
            return AnswerTypeFactory.Create().GetEditableAssignedAnswerTypesList(userId);
        }
    }
}

