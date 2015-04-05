namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Interface for the regular expression DAL.
    /// </summary>
    public interface IRegularExpression
    {
        void AddRegularExpression(RegularExpressionData newRegularExpression, int userId);
        void DeleteRegularExpressionById(int RegularExpressionId);
        RegularExpressionData GetAllRegularExpressionsList();
        RegularExpressionData GetEditableRegularExpressionsListOfUser(int userId);
        RegularExpressionData GetRegularExpressionById(int RegularExpressionId);
        RegularExpressionData GetRegularExpressionsOfUser(int userId, int surveyId);
        void SetBuiltInRegularExpression(int regularExpressionId);
        void UpdateRegularExpression(RegularExpressionData updatedRegularExpression);
    }
}

