namespace Votations.NSurvey
{
    using System;

    /// <summary>
    /// Thrown when a given voter is not found in the 
    /// database.
    /// </summary>
    [Serializable]
    public class VoterNotFoundException : ApplicationException
    {
        public VoterNotFoundException() : base("Voter not found in the database!")
        {
        }

        public VoterNotFoundException(string message) : base(message)
        {
        }
    }
}

