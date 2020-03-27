namespace Votations.NSurvey.BusinessRules
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Contains the business rules that are used for a regular expression
    /// </summary>
    public class RegularExpression
    {
        /// <summary>
        /// Adds a new regular expression to the database
        /// </summary>
        public void AddRegularExpression(RegularExpressionData newRegularExpression, int userId)
        {
            RegularExpressionFactory.Create().AddRegularExpression(newRegularExpression, userId);
        }

        /// <summary>
        /// Remove the regular expression 
        /// </summary>
        public void DeleteRegularExpressionById(int regularExpressionId)
        {
            IRegularExpression type = RegularExpressionFactory.Create();
            if (type.IsRegExInUse(regularExpressionId)) 
            {
                // if true returned by DAL:
                throw new RegExInUseException();
            }
            else
            {
                type.DeleteRegularExpressionById(regularExpressionId);
            }

            //RegularExpressionFactory.Create().DeleteRegularExpressionById(regularExpressionId);
        }


        /// <summary>
        /// Makes the regular expression as builtin
        /// </summary>
        public void SetBuiltInRegularExpression(int regularExpressionId)
        {
            RegularExpressionFactory.Create().SetBuiltInRegularExpression(regularExpressionId);
        }

        /// <summary>
        /// Updates regular expressions data
        /// </summary>
        public void UpdateRegularExpression(RegularExpressionData updatedRegularExpression)
        {
            RegularExpressionFactory.Create().UpdateRegularExpression(updatedRegularExpression);
        }
    }
}

