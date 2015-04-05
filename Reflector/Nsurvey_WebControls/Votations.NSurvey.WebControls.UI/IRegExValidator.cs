namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Define the properties that are required for an answeritem class 
    /// that needs to use regular expression validation.
    /// </summary>
    public interface IRegExValidator
    {
        /// <summary>
        /// Regular expression that will be used
        /// by the answer item to validate user's answer
        /// </summary>
        string RegExpression { get; set; }

        /// <summary>
        /// Message that will be show to the user
        /// if regex failed
        /// </summary>
        string RegExpressionErrorMessage { get; set; }
    }
}

