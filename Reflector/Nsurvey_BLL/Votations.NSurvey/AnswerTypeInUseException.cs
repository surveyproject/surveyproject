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
        public AnswerTypeInUseException() : base("AnswerType is in use on a survey answer!")
        {
        }

        public AnswerTypeInUseException(string message) : base(message)
        {
        }
    }
}

