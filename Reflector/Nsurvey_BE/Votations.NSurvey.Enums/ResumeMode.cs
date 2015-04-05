namespace Votations.NSurvey.Data
{
    using System;

    /// <summary>
    /// Enumeration to determine how we the user wants
    /// to be notified when a new entry is saved in
    /// the system
    /// </summary>
    public enum ResumeMode
    {
        Automatic = 2,
        Manual = 3,
        NotAllowed = 1
    }
}

