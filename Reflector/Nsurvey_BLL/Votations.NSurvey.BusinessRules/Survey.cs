namespace Votations.NSurvey.BusinessRules
{
    using Microsoft.VisualBasic;
    using System;
    using System.Data;
    using System.Text;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Contains the business rules that are used for a survey
    /// </summary>
    public class Survey
    {
        /// <summary>
        /// Add a new branching rule to the survey
        /// </summary>
        /// <param name="newBranchingRule"></param>
        public void AddBranchingRule(BranchingRuleData newBranchingRule)
        {
            SurveyFactory.Create().AddBranchingRule(newBranchingRule);
        }

        /// <summary>
        /// Add a new condition to show a given thank you message
        /// </summary>
        /// <param name="newBranchingRule"></param>
        public void AddMessageCondition(MessageConditionData newMessageCondition)
        {
            SurveyFactory.Create().AddMessageCondition(newMessageCondition);
        }

        /// <summary>
        /// Adds a new survey to the database
        /// </summary>
        /// <param name="newSurvey">Survey object with information about what to add. Only Id must be ommited</param>
        public void AddSurvey(SurveyData newSurvey)
        {
            if (newSurvey.Surveys.Rows.Count > 0)
            {
                SurveyFactory.Create().AddSurvey(newSurvey);
            }
        }

        /// <summary>
        /// Assigns a new user to a survey
        /// </summary>
        public void AssignUserToSurvey(int surveyId, int userId)
        {
            SurveyFactory.Create().AssignUserToSurvey(surveyId, userId);
        }

        /// <summary>
        /// Check if the user has this survey assigned
        /// </summary>
        public bool CheckSurveyUser(int surveyId, int userId)
        {
            return SurveyFactory.Create().CheckSurveyUser(surveyId, userId);
        }

        /// <summary>
        /// Clone the given survey and returns its clone
        /// </summary>
        /// <param name="surveyId">Id of the survey you want to clone</param>
        /// <returns>A SurveyData dataset object with the current database values of the clone</returns>
        public SurveyData CloneSurvey(int surveyId)
        {
            return SurveyFactory.Create().CloneSurvey(surveyId);
        }

        /// <summary>
        /// Deletes the branching rules 
        /// </summary>
        public void DeleteBranchingRuleById(int branchingRuleId)
        {
            SurveyFactory.Create().DeleteBranchingRuleById(branchingRuleId);
        }

        /// <summary>
        /// Deletes the message condition
        /// </summary>
        public void DeleteMessageConditionById(int messageConditionId)
        {
            SurveyFactory.Create().DeleteMessageConditionById(messageConditionId);
        }

        /// <summary>
        /// deletes a page break
        /// </summary>
        /// <param name="surveyId">ID of the survey to which we add the page break</param>
        /// <param name="pageNumber">Page number delete</param>
        public void DeletePageBreak(int surveyId, int pageNumber)
        {
            SurveyFactory.Create().DeletePageBreak(surveyId, pageNumber);
        }

        /// <summary>
        /// Deletes survey's quotas
        /// </summary>
        public void DeleteQuotaSettings(int surveyId)
        {
            SurveyFactory.Create().DeleteQuotaSettings(surveyId);
        }

        /// <summary>
        /// Remove the survey and all its question / answers from the database
        /// </summary>
        /// <param name="surveyId">Survey to delete from the database</param>
        public void DeleteSurveyById(int surveyId)
        {
            SurveyFactory.Create().DeleteSurveyById(surveyId);
        }

        public string EscapeFilterString(string filterString)
        {
            filterString = filterString.Replace("[", "[[]");
            filterString = filterString.Replace("]", "[]]");
            filterString = filterString.Replace("*", "[*]");
            filterString = filterString.Replace("%", "[%]");
            filterString = filterString.Replace(" ", "%");
            return filterString;
        }

        /// <summary>
        /// Get next page depending depending on the
        /// branching rules
        /// </summary>
        public int GetNextPage(int surveyId, int pageNumber, VoterAnswersData voterAnswers, bool evaluateScores)
        {
            BranchingRuleData surveyPageBranchingRules = new Surveys().GetSurveyPageBranchingRules(surveyId, pageNumber);
            if (surveyPageBranchingRules.BranchingRules.Rows.Count <= 0)
            {
                pageNumber++;
                return pageNumber;
            }
            int num = pageNumber + 1;
            DataRow[] rowArray = null;
            foreach (BranchingRuleData.BranchingRulesRow row in surveyPageBranchingRules.BranchingRules.Rows)
            {
                int num2 = evaluateScores ? new Question().GetQuestionAnswersScore(row.QuestionId, voterAnswers) : 0;
                if ((evaluateScores && (row.ConditionalOperator == 3)) && (num2 == row.Score))
                {
                    return row.TargetPageNumber;
                }
                if ((evaluateScores && (row.ConditionalOperator == 4)) && (num2 < row.Score))
                {
                    return row.TargetPageNumber;
                }
                if ((evaluateScores && (row.ConditionalOperator == 5)) && (num2 > row.Score))
                {
                    return row.TargetPageNumber;
                }
                if ((evaluateScores && (row.ConditionalOperator == 6)) && ((num2 >= row.Score) && (num2 <= row.ScoreMax)))
                {
                    return row.TargetPageNumber;
                }
                if (!row.IsAnswerIdNull() && !row.IsTextFilterNull())
                {
                    string expression = this.EscapeFilterString(row.TextFilter);
                    if ((!row.IsExpressionOperatorNull() && (row.ExpressionOperator != 2)) && Information.IsDate(expression))
                    {
                        expression = "#" + expression + "#";
                    }
                    else if ((row.ExpressionOperator != 2) && !Information.IsNumeric(expression))
                    {
                        expression = "'" + expression + "'";
                    }
                    try
                    {
                        switch (row.ExpressionOperator)
                        {
                            case 1:
                                rowArray = voterAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText = ", expression }));
                                goto Label_0319;

                            case 3:
                                rowArray = voterAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText > ", expression }));
                                goto Label_0319;

                            case 4:
                                rowArray = voterAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText < ", expression }));
                                goto Label_0319;
                        }
                        rowArray = voterAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText like '%", expression, "%'" }));
                    }
                    catch (EvaluateException)
                    {
                    }
                }
                else if (!row.IsAnswerIdNull())
                {
                    rowArray = voterAnswers.VotersAnswers.Select("AnswerId=" + row.AnswerId);
                }
                else if (row.IsAnswerIdNull())
                {
                    rowArray = voterAnswers.VotersAnswers.Select("QuestionId=" + row.QuestionId);
                }
                else
                {
                    rowArray = null;
                }
            Label_0319:
                if ((((rowArray != null) && (row.ConditionalOperator == 1)) && (rowArray.Length > 0)) || ((row.ConditionalOperator == 2) && (rowArray.Length == 0)))
                {
                    return row.TargetPageNumber;
                }
            }
            return num;
        }

        /// <summary>
        /// Returns the previous page index of the page specified 
        /// as a parameter and based the voter's answers
        /// </summary>
        public int GetPreviousPage(int surveyId, int pageNumber, VoterAnswersData voterAnswers, bool evaluateScores)
        {
            int num = 1;
            int num2 = 1;
            while (num2 < pageNumber)
            {
                num2 = new Survey().GetNextPage(surveyId, num, voterAnswers, evaluateScores);
                if (num2 >= pageNumber)
                {
                    return num;
                }
                num = num2;
            }
            return num;
        }

        /// <summary>
        /// Calculate the total score of the current answers set
        /// </summary>
        private int GetScoreTotal(VoterAnswersData votersAnswers)
        {
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (VoterAnswersData.VotersAnswersRow row in votersAnswers.VotersAnswers.Rows)
            {
                builder.Append(row.AnswerId);
                if ((num + 1) < votersAnswers.VotersAnswers.Count)
                {
                    builder.Append(',');
                }
                num++;
            }
            return new Answers().GetAnswersScoreTotal(builder.ToString());
        }

        /// <summary>
        /// Get conditional thanks message based
        /// on user's answers
        /// </summary>
        public string GetThanksMessage(int surveyId, VoterAnswersData surveyAnswers, bool evaluateScores)
        {
            MessageConditionData surveyMessageConditions = new Surveys().GetSurveyMessageConditions(surveyId);
            if ((surveyMessageConditions.MessageConditions.Rows.Count > 0) && (surveyAnswers.Voters.Count > 0))
            {
                DataRow[] rowArray = null;
                string thankYouMessage = null;
                int scoreTotal = evaluateScores ? this.GetScoreTotal(surveyAnswers) : 0;
                foreach (MessageConditionData.MessageConditionsRow row in surveyMessageConditions.MessageConditions.Rows)
                {
                    if ((evaluateScores && (row.MessageConditionalOperator == 3)) && (scoreTotal < row.Score))
                    {
                        thankYouMessage = row.ThankYouMessage;
                    }
                    if ((evaluateScores && (row.MessageConditionalOperator == 2)) && (scoreTotal == row.Score))
                    {
                        thankYouMessage = row.ThankYouMessage;
                    }
                    if ((evaluateScores && (row.MessageConditionalOperator == 4)) && (scoreTotal > row.Score))
                    {
                        thankYouMessage = row.ThankYouMessage;
                    }
                    if ((evaluateScores && (row.MessageConditionalOperator == 5)) && ((scoreTotal >= row.Score) && (scoreTotal <= row.ScoreMax)))
                    {
                        thankYouMessage = row.ThankYouMessage;
                    }
                    if (row.MessageConditionalOperator != 1)
                    {
                        continue;
                    }
                    int num2 = evaluateScores ? new Question().GetQuestionAnswersScore(row.QuestionId, surveyAnswers) : 0;
                    if ((evaluateScores && (row.ConditionalOperator == 3)) && (num2 == row.Score))
                    {
                        thankYouMessage = row.ThankYouMessage;
                    }
                    if ((evaluateScores && (row.ConditionalOperator == 4)) && (num2 < row.Score))
                    {
                        thankYouMessage = row.ThankYouMessage;
                    }
                    if ((evaluateScores && (row.ConditionalOperator == 5)) && (num2 > row.Score))
                    {
                        thankYouMessage = row.ThankYouMessage;
                    }
                    if ((evaluateScores && (row.ConditionalOperator == 6)) && ((num2 >= row.Score) && (num2 <= row.ScoreMax)))
                    {
                        thankYouMessage = row.ThankYouMessage;
                    }
                    if (!row.IsAnswerIdNull() && !row.IsTextFilterNull())
                    {
                        string expression = this.EscapeFilterString(row.TextFilter);
                        if ((!row.IsExpressionOperatorNull() && (row.ExpressionOperator != 2)) && Information.IsDate(expression))
                        {
                            expression = "#" + expression + "#";
                        }
                        else if ((row.ExpressionOperator != 2) && !Information.IsNumeric(expression))
                        {
                            expression = "'" + expression + "'";
                        }
                        try
                        {
                            switch (row.ExpressionOperator)
                            {
                                case 1:
                                    rowArray = surveyAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText = ", expression }));
                                    goto Label_03F9;

                                case 3:
                                    rowArray = surveyAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText > ", expression }));
                                    goto Label_03F9;

                                case 4:
                                    rowArray = surveyAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText < ", expression }));
                                    goto Label_03F9;
                            }
                            rowArray = surveyAnswers.VotersAnswers.Select(string.Concat(new object[] { "AnswerId = ", row.AnswerId, " AND AnswerText like '%", expression, "%'" }));
                        }
                        catch (EvaluateException)
                        {
                        }
                    }
                    else if (!row.IsAnswerIdNull())
                    {
                        rowArray = surveyAnswers.VotersAnswers.Select("AnswerId=" + row.AnswerId);
                    }
                    else if (row.IsAnswerIdNull())
                    {
                        rowArray = surveyAnswers.VotersAnswers.Select("QuestionId=" + row.QuestionId);
                    }
                    else
                    {
                        rowArray = null;
                    }
                Label_03F9:
                    if ((((rowArray != null) && (row.ConditionalOperator == 1)) && (rowArray.Length > 0)) || ((row.ConditionalOperator == 2) && (rowArray.Length == 0)))
                    {
                        thankYouMessage = row.ThankYouMessage;
                        break;
                    }
                }
                if (thankYouMessage != null)
                {
                    return ParseThankYouMessage(thankYouMessage, scoreTotal);
                }
            }
            return null;
        }

        /// <summary>
        /// Import the given surveys into the DB
        /// </summary>
        public void ImportSurveys(NSurveyForm importSurveys, int userId,int folderId)
        {
            ISurvey survey = SurveyFactory.Create();
            this.TurnOverPrimaryKeys(importSurveys);
            survey.ImportSurveys(importSurveys, userId,folderId);
        }

        /// <summary>
        /// Increase the current entries number calculated
        /// against the max. quota
        /// </summary>
        public void IncreaseQuotaEntries(int surveyId)
        {
            SurveyFactory.Create().IncreaseQuotaEntries(surveyId);
        }

        /// <summary>
        /// Increment the number of time the survey's result has been viewed
        /// </summary>
        /// <param name="surveyId">Id of the survey to increment result view</param>
        /// <param name="incrementNumber">number of views to add</param>
        public void IncrementResultsViews(int surveyId, int incrementNumber)
        {
            SurveyFactory.Create().IncrementResultsViews(surveyId, incrementNumber);
        }

        /// <summary>
        /// Increment the number of time a survey has been viewed
        /// </summary>
        /// <param name="surveyId">Id of the survey to increment views</param>
        /// <param name="incrementNumber">number of views to add</param>
        public void IncrementSurveyViews(int surveyId, int incrementNumber)
        {
            SurveyFactory.Create().IncrementSurveyViews(surveyId, incrementNumber);
        }

        /// <summary>
        /// Adds a new survey page break
        /// </summary>
        /// <param name="surveyId">ID of the survey to which we add the page break</param>
        /// <param name="displayOrder">Position of the page break</param>
        public void InsertPageBreak(int surveyId, int displayOrder)
        {
            SurveyFactory.Create().InsertPageBreak(surveyId, displayOrder);
        }

        /// <summary>
        /// Adds a new survey line break, which is currently a static question
        /// with a html hr tag in it.
        /// </summary>
        /// <param name="surveyId">ID of the survey to which we add the line break</param>
        /// <param name="displayOrder">Position of the line break</param>
        public void InsertSurveyLineBreak(int surveyId, int displayOrder, int pageNumber)
        {
            new Question().AddStaticInformationText(surveyId, displayOrder, pageNumber, "<hr class=\"surveyLineBreak\" />", "");
        }

        /// <summary>
        /// Move a page break Down
        /// </summary>
        /// <param name="surveyId">ID of the survey to which we add the page break</param>
        /// <param name="PageNumber">Page break to move down</param>
        public void MovePageBreakDown(int surveyId, int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            SurveyFactory.Create().MovePageBreakDown(surveyId, pageNumber);
        }

        /// <summary>
        /// Move a page break up
        /// </summary>
        /// <param name="surveyId">ID of the survey to which we add the page break</param>
        /// <param name="PageNumber">Page break to move up</param>
        public void MovePageBreakUp(int surveyId, int pageNumber)
        {
            SurveyFactory.Create().MovePageBreakUp(surveyId, pageNumber);
        }

        /// <summary>
        /// Parse the given string and replace any
        /// available templates by their runtime values
        /// </summary>
        private static string ParseThankYouMessage(string thankYouMessage, int scoreTotal)
        {
            return thankYouMessage.Replace("::score::", scoreTotal.ToString());
        }

        /// <summary>
        /// Resets the current entries number calculated
        /// against the max. quota
        /// </summary>
        public void ResetQuotaEntries(int surveyId)
        {
            SurveyFactory.Create().ResetQuotaEntries(surveyId);
        }

        /// <summary>
        /// Sets all answers and vote count to 0
        /// </summary>
        /// <param name="surveyId">Survey ID</param>
        public void ResetVotes(int surveyId)
        {
            SurveyFactory.Create().ResetVotes(surveyId);
        }

        /// <summary>
        /// Invert main table keys signs to avoid key collisions on insertion
        /// of new row with existing keys in the dataset that have not 
        /// yet been updated with their "live" DB key.
        /// Changes will casade to child tables
        /// </summary>
        private void TurnOverPrimaryKeys(NSurveyForm importSurveys)
        {
            foreach (NSurveyForm.AnswerTypeRow row in importSurveys.AnswerType)
            {
                row.AnswerTypeID = (short)-row.AnswerTypeID;
            }
            foreach (NSurveyForm.RegularExpressionRow row2 in importSurveys.RegularExpression)
            {
                row2.RegularExpressionId = -row2.RegularExpressionId;
            }
            foreach (NSurveyForm.QuestionRow row3 in importSurveys.Question)
            {
                row3.QuestionId = -row3.QuestionId;
            }
            foreach (NSurveyForm.AnswerRow row4 in importSurveys.Answer)
            {
                row4.AnswerId = -row4.AnswerId;
            }
        }

        /// <summary>
        /// UnAssigns a user from a survey
        /// </summary>
        public void UnAssignUserFromSurvey(int surveyId, int userId)
        {
            SurveyFactory.Create().UnAssignUserFromSurvey(surveyId, userId);
        }

        /// <summary>
        /// Update the survey access password
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="accessPassword"></param>
        public void UpdateAccessPassword(int surveyId, string accessPassword)
        {
            SurveyFactory.Create().UpdateAccessPassword(surveyId, accessPassword);
        }

        /// <summary>
        /// Updates asp.net settings
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="allowMultipleSubmissions"></param>
        public void UpdateAspSecuritySettings(int surveyId, bool allowMultipleSubmissions)
        {
            SurveyFactory.Create().UpdateAspSecuritySettings(surveyId, allowMultipleSubmissions);
        }

        /// <summary>
        /// Update the cookie expiration time
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="cookieExpires"></param>
        public void UpdateCookieExpiration(int surveyId, int cookieExpires)
        {
            SurveyFactory.Create().UpdateCookieExpiration(surveyId, cookieExpires);
        }

        /// <summary>
        /// Update the ip expiration time
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="ipExpires"></param>
        public void UpdateIPExpiration(int surveyId, int ipExpires)
        {
            SurveyFactory.Create().UpdateIPExpiration(surveyId, ipExpires);
        }

        /// <summary>
        /// Updates a message condition
        /// </summary>
        public void UpdateMessageCondition(MessageConditionData updatedMessageCondition)
        {
            SurveyFactory.Create().UpdateMessageCondition(updatedMessageCondition);
        }

        /// <summary>
        /// Update the nsurvey security settings
        /// </summary>
        public void UpdateNSurveySecuritySettings(int surveyId, bool allowMultipleNSurveySubmissions)
        {
            SurveyFactory.Create().UpdateNSurveySecuritySettings(surveyId, allowMultipleNSurveySubmissions);
        }

        /// <summary>
        /// Update the survey only invited status
        /// </summary>
        public void UpdateOnlyInvited(int surveyId, bool onlyInvited)
        {
            SurveyFactory.Create().UpdateOnlyInvited(surveyId, onlyInvited);
        }

        /// <summary>
        /// Update the survey Save Token User  Data flag
        /// </summary>
        public void UpdateSaveTokenUserDate(int surveyId, bool saveData)
        {
            SurveyFactory.Create().UpdateSaveTokenUserData(surveyId, saveData);
        }

        /// <summary>
        /// Updates or creates if it doesnt exists 
        /// quota settings for the survey
        /// </summary>
        public void UpdateQuotaSettings(SurveyEntryQuotaData surveyQuota)
        {
            SurveyFactory.Create().UpdateQuotaSettings(surveyQuota);
        }

        /// <summary>
        /// Update the database with the given survey
        /// </summary>
        /// <param name="updatedSurvey">survey to update, must contain the surveyid</param>
        public void UpdateSurvey(SurveyData updatedSurvey, string languageCode)
        {
            SurveyFactory.Create().UpdateSurvey(updatedSurvey, languageCode);
        }

        /// <summary>
        /// Update the options that were setup for the page
        /// </summary>
        public void UpdateSurveyPageOptions(PageOptionData updatedPageOptions)
        {
            SurveyFactory.Create().UpdateSurveyPageOptions(updatedPageOptions);
        }

        /// <summary>
        /// Updates the survey to a new unAuthentified user action
        /// </summary>
        /// <returns></returns>
        public void UpdateUnAuthentifiedUserActions(int surveyId, int UnAuthentifiedUserActionId)
        {
            SurveyFactory.Create().UpdateUnAuthentifiedUserActions(surveyId, UnAuthentifiedUserActionId);
        }
    }
}

