namespace Votations.NSurvey
{
    using System;

    /// <summary>
    /// Thrown when a given answer type is already in use by an
    /// answer.
    /// </summary>
    [Serializable]
    public class AnswerTypeInUseException : ApplicationException
    {
        public AnswerTypeInUseException() : base("Answer is already in use by an answer!")
        {
        }

        public AnswerTypeInUseException(string message) : base(message)
        {
        }
    }
}

