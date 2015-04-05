namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Define the properties that are required for a class 
    /// that wants to act as a field inside a question.
    /// </summary>
    public interface IFieldItem
    {
        int FieldHeight { get; set; }

        int FieldLength { get; set; }

        int FieldWidth { get; set; }
    }
}

