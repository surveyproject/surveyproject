namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provides the method to access the RegularExpression's data.
    /// </summary>
    public class RegularExpressions
    {
        /// <summary>
        /// Retrieves all regular expressions from the database
        /// </summary>
        public RegularExpressionData GetAllRegularExpressionsList()
        {
            return RegularExpressionFactory.Create().GetAllRegularExpressionsList();
        }

        /// <summary>
        /// Retrieves all regular expressions from the database that can be edited 
        /// by the user
        /// </summary>
        public RegularExpressionData GetEditableRegularExpressionsListOfUser(int userId)
        {
            return RegularExpressionFactory.Create().GetEditableRegularExpressionsListOfUser(userId);
        }

        /// <summary>
        /// Retrieves regular expression details from the database
        /// </summary>
        public RegularExpressionData GetRegularExpressionById(int regularExpressionId)
        {
            return RegularExpressionFactory.Create().GetRegularExpressionById(regularExpressionId);
        }

        /// <summary>
        /// Retrieves all regular expressions from the database assigned to a user
        /// </summary>
        public RegularExpressionData GetRegularExpressionsOfUser(int userId, int surveyId)
        {
            return RegularExpressionFactory.Create().GetRegularExpressionsOfUser(userId, surveyId);
        }
    }
}

