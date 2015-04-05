namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the methods to access surveys database data
    /// </summary>
    public class Surveys
    {
        /// <summary>
        /// Check if the survey allows multiple submission
        /// for the same username 
        /// </summary>
        public bool AspSecurityAllowsMultipleSubmissions(int surveyId)
        {
            return SurveyFactory.Create().AspSecurityAllowsMultipleSubmissions(surveyId);
        }

        /// <summary>
        /// Return a survey object that reflects the current activated poll. 
        /// IE: The survey that shows up when the surveybox has a surveyid value of 
        /// 0
        /// </summary>
        /// <returns>A surveydata dataset with the current database values</returns>
        public SurveyData GetActivatedSurvey()
        {
            return SurveyFactory.Create().GetActivatedSurvey();
        }

        /// <summary>
        /// Retrieves all surveys details from the database
        /// </summary>
        /// <returns>A SurveyData dataset</returns>
        public SurveyData GetAllSurveys()
        {
            return SurveyFactory.Create().GetAllSurveys();
        }

        /// <summary>
        /// Get a survey list with only the survey id and title
        /// </summary>
        /// <returns></returns>
        public SurveyData GetAllSurveysList()
        {
            return SurveyFactory.Create().GetAllSurveysList();
        }

        /// <summary>
        /// Get a survey list with only the survey id and title filtered by title
        /// </summary>
        /// <returns></returns>
        public SurveyData GetAllSurveysByTitle(string title, int? folderID,int userID)
        {
            return SurveyFactory.Create().GetAllSurveysByTitle(title, folderID,userID);
        }

        /// <summary>
        /// Retrieves all archived surveys from the database
        /// </summary>
        /// <returns>A SurveyData dataset of all archived surveys</returns>
        public SurveyData GetArchivedSurveys()
        {
            return SurveyFactory.Create().GetArchivedSurveys();
        }
        /// <summary>
        /// Retrieves all archived surveys from the database
        /// </summary>
        /// <returns>A SurveyData dataset of all archived surveys</returns>
        public int GetSurveyIdFromGuid(Guid guid)
        {
            return SurveyFactory.Create().GetSurveyIdFromGuid(guid);
        }
        public int GetSurveyIdFromFriendlyName(string name)
        {
            return SurveyFactory.Create().GetSurveyIdFromFriendlyName(name);
        }

        /// <summary>
        /// Get a survey list assigned to the user
        /// </summary>
        /// <returns></returns>
        public SurveyData GetAssignedSurveysList(int userId)
        {
            return SurveyFactory.Create().GetAssignedSurveysList(userId);
        }

        /// <summary>
        /// Get the number of minutes after which the security cookie 
        /// expires
        /// </summary>
        public int GetCookieExpiration(int surveyId)
        {
            return SurveyFactory.Create().GetCookieExpiration(surveyId);
        }

        /// <summary>
        /// Retrieves the first survey ID available in the DB
        /// </summary>
        public int GetFirstSurveyId()
        {
            return SurveyFactory.Create().GetFirstSurveyId();
        }

        /// <summary>
        /// Retrieves the first survey ID available for the user in the DB
        /// </summary>
        public int GetFirstSurveyId(int userId)
        {
            return SurveyFactory.Create().GetFirstSurveyId(userId);
        }

        /// <summary>
        /// Returns all the survey, questions, answers
        /// for a survey
        /// </summary>
        public NSurveyForm GetFormForExport(int surveyId)
        {
            NSurveyForm formForExport = SurveyFactory.Create().GetFormForExport(surveyId);
            if (formForExport.Survey.Rows.Count > 0)
            {
                formForExport.Survey[0].SurveyID = 1;
                int num = 1;
                foreach (NSurveyForm.QuestionRow row in formForExport.Survey[0].GetQuestionRows())
                {
                    row.QuestionId = num;
                    num++;
                }
            }
            return formForExport;
        }

        /// <summary>
        /// Get the number of minutes after which the security IP 
        /// expires
        /// </summary>
        public int GetIPExpiration(int surveyId)
        {
            return SurveyFactory.Create().GetIPExpiration(surveyId);
        }

        /// <summary>
        /// Returns the total of pages in the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public int GetPagesNumber(int surveyId)
        {
            return SurveyFactory.Create().GetPagesNumber(surveyId);
        }

        /// <summary>
        /// Retrieves the quota of the survey
        /// </summary>
        public SurveyEntryQuotaData GetQuotaSettings(int surveyId)
        {
            return SurveyFactory.Create().GetQuotaSettings(surveyId);
        }

        /// <summary>
        /// returns the branching rules for the given survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public BranchingRuleData GetSurveyBranchingRules(int surveyId)
        {
            return SurveyFactory.Create().GetSurveyBranchingRules(surveyId);
        }

        /// <summary>
        /// Return a survey object that reflects the database poll
        /// </summary>
        /// <param name="surveyId">Id of the survey you need</param>
        /// <returns>A SurveyData dataset object with the current database values</returns>
        public SurveyData GetSurveyById(int surveyId, string languageCode)
        {
            SurveyData surveyById = SurveyFactory.Create().GetSurveyById(surveyId, languageCode);
            if (surveyById.Surveys.Rows.Count == 0)
            {
                throw new SurveyNotFoundException();
            }
            return surveyById;
        }

        /// <summary>
        /// returns the thank you message conditions for the given survey
        /// </summary>
        public MessageConditionData GetSurveyMessageCondition(int messageConditionId)
        {
            return SurveyFactory.Create().GetSurveyMessageCondition(messageConditionId);
        }

        /// <summary>
        /// returns the thank you message conditions for the given survey
        /// </summary>
        public MessageConditionData GetSurveyMessageConditions(int surveyId)
        {
            return SurveyFactory.Create().GetSurveyMessageConditions(surveyId);
        }

        /// <summary>
        /// Returns a list of available email notification mode
        /// </summary>
        public NotificationModeData GetSurveyNotificationModes()
        {
            return SurveyFactory.Create().GetSurveyNotificationModes();
        }

        /// <summary>
        /// returns the branching rules for the given page of the survey
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public BranchingRuleData GetSurveyPageBranchingRules(int surveyId, int pageNumber)
        {
            return SurveyFactory.Create().GetSurveyPageBranchingRules(surveyId, pageNumber);
        }

        /// <summary>
        /// returns the branching rules for the given page with all
        /// their details
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public BranchingRuleData GetSurveyPageBranchingRulesDetails(int surveyId, int pageNumber)
        {
            return SurveyFactory.Create().GetSurveyPageBranchingRulesDetails(surveyId, pageNumber);
        }

        /// <summary>
        /// Retrieves the options that were setup for the page
        /// </summary>
        public PageOptionData GetSurveyPageOptions(int surveyId, int pageNumber)
        {
            return SurveyFactory.Create().GetSurveyPageOptions(surveyId, pageNumber);
        }

        /// <summary>
        /// Get the access password of the survey
        /// </summary>
        public string GetSurveyPassword(int surveyId)
        {
            return SurveyFactory.Create().GetSurveyPassword(surveyId);
        }

        /// <summary>
        /// Returns all the data need to handle 
        /// and process question / answer piping
        /// </summary>
        public PipeData GetSurveyPipeDataFromQuestionId(int questionId)
        {
            return SurveyFactory.Create().GetSurveyPipeDataFromQuestionId(questionId);
        }

        /// <summary>
        /// Returns a list of available progress modes
        /// </summary>
        public ProgressModeData GetSurveyProgressModes()
        {
            return SurveyFactory.Create().GetSurveyProgressModes();
        }

        /// <summary>
        /// Returns a list of available resume modes
        /// </summary>
        public ResumeModeData GetSurveyResumeModes()
        {
            return SurveyFactory.Create().GetSurveyResumeModes();
        }

        /// <summary>
        /// Get the actions that is set for the given survey
        /// </summary>
        public int GetSurveyUnAuthentifiedUserAction(int surveyId)
        {
            int surveyUnAuthentifiedUserAction = SurveyFactory.Create().GetSurveyUnAuthentifiedUserAction(surveyId);
            if (surveyUnAuthentifiedUserAction == -1)
            {
                return 1;
            }
            return surveyUnAuthentifiedUserAction;
        }

        /// <summary>
        /// Get a survey list not assigned to the user
        /// </summary>
        /// <returns></returns>
        public SurveyData GetUnAssignedSurveysList(int userId)
        {
            return SurveyFactory.Create().GetUnAssignedSurveysList(userId);
        }

        /// <summary>
        /// Returns true if the page has any branching rule
        /// associated with it
        /// </summary>
        public bool HasPageBranching(int surveyId, int pageNumber)
        {
            return SurveyFactory.Create().HasPageBranching(surveyId, pageNumber);
        }

        /// <summary>
        /// Check if the survey only accept entries from
        /// users with email codes
        /// </summary>
        public bool IsSurveyOnlyInvited(int surveyId)
        {
            return SurveyFactory.Create().IsSurveyEmailInviteOnly(surveyId);
        }

        /// <summary>
        /// Check if the survey sAves Token User Data
        /// 
        /// </summary>
        public bool IsSurveySaveTokenUserData(int surveyId)
        {
            return SurveyFactory.Create().IsSurveySaveTokenUserData(surveyId);
        }
        /// <summary>
        /// Check if the given password is valid for the survey
        /// </summary>
        /// <param name="surveyId">Id of the protected survey</param>
        /// <param name="password">the password to check for</param>
        public bool IsSurveyPasswordValid(int surveyId, string password)
        {
            return SurveyFactory.Create().IsSurveyPasswordValid(surveyId, password);
        }

        /// <summary>
        /// Check if survey has been set to be scored
        /// </summary>
        public bool IsSurveyScored(int surveyId)
        {
            return SurveyFactory.Create().IsSurveyScored(surveyId);
        }

        /// <summary>
        /// Check if the survey allows multiple submission
        /// for the same nsurvey username 
        /// </summary>
        public bool NSurveyAllowsMultipleSubmissions(int surveyId)
        {
            return SurveyFactory.Create().NSurveyAllowsMultipleSubmissions(surveyId);
        }

        /// <summary>
        /// Returns the total of surveys stored in the database
        /// </summary>
        /// <returns>Total of surveys</returns>
        public int TotalOfSurveys()
        {
            return SurveyFactory.Create().TotalOfSurveys();
        }

        public void SetFolderId(int? folderId, int surveyId)
        {
            SurveyFactory.Create().SetFolderId(folderId, surveyId);
        }

        public void SetFriendlyName(int surveyId, string friendlyName)
        {
            SurveyFactory.Create().SetFriendlyName(surveyId, friendlyName);
        }
    }
}

