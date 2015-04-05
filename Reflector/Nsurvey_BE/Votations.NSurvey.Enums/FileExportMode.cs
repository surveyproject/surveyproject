namespace Votations.NSurvey.Data
{
    using System;

    /// <summary>
    /// Enumeration to determine what mode is used 
    /// to export files from the db to the file system
    /// </summary>
    public enum FileExportMode
    {
        GuidFileGroup = 3,
        NoFileGroup = 1,
        VoterFileGroup = 2
    }
}

