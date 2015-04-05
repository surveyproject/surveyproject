namespace Votations.NSurvey
{
    using System;

    /// <summary>
    /// Thrown when a given survey already exists in the 
    /// database.
    /// </summary>
    [Serializable]
    public class SurveyExistsFoundException : ApplicationException
    {
        public SurveyExistsFoundException() : base("A Survey with this title already exists in the folder!")
        {
        }

        public SurveyExistsFoundException(string message) : base(message)
        {
        }
    }
}

