namespace Votations.NSurvey
{
    using System;

    /// <summary>
    /// Thrown when a given question is not found in the 
    /// database.
    /// </summary>
    [Serializable]
    public class QuestionNotFoundException : ApplicationException
    {
        public QuestionNotFoundException() : base("Question not found in the database!")
        {
        }

        public QuestionNotFoundException(string message) : base(message)
        {
        }
    }
}

