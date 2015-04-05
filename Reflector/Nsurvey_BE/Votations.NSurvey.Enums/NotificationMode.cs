namespace Votations.NSurvey.Data
{
    using System;

    /// <summary>
    /// Enumeration to determine how the user wants
    /// to be notified when a new entry is saved in
    /// the system
    /// </summary>
    public enum NotificationMode
    {
        EntryReport = 3,
        None = 1,
        OnlyAnswersReport = 4,
        Short = 2
    }
}

