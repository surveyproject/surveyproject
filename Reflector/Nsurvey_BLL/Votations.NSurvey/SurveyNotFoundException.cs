namespace Votations.NSurvey
{
    using System;

    /// <summary>
    /// Thrown when a given survey is not found in the 
    /// database.
    /// </summary>
    [Serializable]
    public class SurveyNotFoundException : ApplicationException
    {
        public SurveyNotFoundException() : base("Survey not found in the database!")
        {
        }

        public SurveyNotFoundException(string message) : base(message)
        {
        }
    }
}

