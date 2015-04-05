namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI.WebControls;

    /// <summary>
    /// A hidden field, mainly used to get / store default text
    /// values that were setup using the query string, 
    /// session or server variable values
    /// </summary>
    public class AnswerFieldPasswordItem : AnswerFieldItem
    {
        /// <summary>
        /// Create the "layout" and adds the textbox control to the 
        /// control tree
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            base._fieldTextBox.TextMode = TextBoxMode.Password;
            base._fieldTextBox.Attributes.Add("value", this.DefaultText);
        }
    }
}

