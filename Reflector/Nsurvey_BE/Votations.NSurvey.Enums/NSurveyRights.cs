namespace Votations.NSurvey.Data
{
    using System;

    /// <summary>
    /// Enumeration (list of related and fixed numbers or Constants) to determine the different rights available in 
    /// for the nsurvey administration interface
    /// </summary>
    public enum NSurveyRights
    {
        AccessAnswerTypeEditor = 13,
        AccessASPNetCode = 0x16,
        AccessCrossTab = 0x13,
        AccessExport = 20,
        AccessFieldEntries = 0x10,
        AccessFileManager = 0x20,
        AccessFormBuilder = 12,
        AccessLibrary = 0x18,
        AccessMailing = 0x15,
        AccessMultiLanguages = 0x22,
        AccessPrivacySettings = 7,
        AccessRegExEditor = 0x1f,
        AccessReports = 14,
        AccessSecuritySettings = 8,
        AccessStats = 9,
        AccessSurveySettings = 3,
        AccessUserManager = 0x17,
        AllowXmlImport = 0x1d,
        ApplySurveySettings = 5,
        CloneSurvey = 6,
        CopyQuestionFromAllSurvey = 0x1a,
        CopyQuestionFromLibrary = 0x19,
        CreateResultsFilter = 15,
        CreateSurvey = 1,
        DeleteSurvey = 2,
        DeleteUnvalidateEntries = 11,
        DeleteVoterEntries = 0x12,
        EditVoterEntries = 0x11,
        ExportFiles = 0x21,
        ExportSurveyXml = 4,
        AccessHelpFiles = 0x23,
        ManageLibrary = 0x1b,
        ResetVotes = 10,
        SqlAnswerTypesEdition = 0x1c,
        TakeSurvey = 30,
        AccessQuestionGroupRight=36,
        DataImportRight=37,
        TokenSecurityRight=38,
        SurveyLayoutRight=39,
        AccessSurveyList=40
    }
}

