namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Define the properties that are required for an answeritem class 
    /// that generates client side validation code.
    /// </summary>
    public interface IClientScriptValidator
    {
        /// <summary>
        /// Returns the field ID and 
        /// ensures the current control has children,
        /// then get the field unique ID in the control tree.
        /// Used to by parent controls to render the correct javascript
        /// </summary>
        string GetControlIdToValidate();

        bool EnableValidation { get; set; }

        string JavascriptCode { get; set; }

        string JavascriptErrorMessage { get; set; }

        string JavascriptFunctionName { get; set; }
    }
}

