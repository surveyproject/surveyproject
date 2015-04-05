namespace Votations.NSurvey
{
    using System;

    /// <summary>
    /// Thrown when a given Filter is not found in the 
    /// database.
    /// </summary>
    [Serializable]
    public class FilterNotFoundException : ApplicationException
    {
        public FilterNotFoundException() : base("Filter not found in the database!")
        {
        }

        public FilterNotFoundException(string message) : base(message)
        {
        }
    }
}

