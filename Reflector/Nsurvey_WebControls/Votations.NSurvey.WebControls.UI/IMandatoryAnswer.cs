namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Define the properties that are required for a class 
    /// that wants to be notified if a user has requested
    /// that an answer has to be posted
    /// </summary>
    public interface IMandatoryAnswer
    {
        bool Mandatory { get; set; }
    }
}

