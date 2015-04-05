namespace Votations.NSurvey
{
    using System;

    /// <summary>
    /// Thrown when a given voter is not found in the 
    /// database.
    /// </summary>
    [Serializable]
    public class UserAuthenticationFailedException : ApplicationException
    {
        public UserAuthenticationFailedException() : base("User Survey authentication failed")
        {
        }

        public UserAuthenticationFailedException(string message) : base(message)
        {
        }
    }
}

