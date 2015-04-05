namespace Votations.NSurvey
{
    using System;

    /// <summary>
    /// Thrown when a given answer is not found in the 
    /// database.
    /// </summary>
    [Serializable]
    public class AnswerNotFoundException : ApplicationException
    {
        public AnswerNotFoundException() : base("Answer not found in the database!")
        {
        }

        public AnswerNotFoundException(string message) : base(message)
        {
        }
    }
}

