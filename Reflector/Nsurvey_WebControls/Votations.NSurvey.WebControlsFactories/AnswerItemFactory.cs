namespace Votations.NSurvey.WebControlsFactories
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Helpers;
    using Votations.NSurvey.Resources;
    using Votations.NSurvey.WebControls.UI;

    /// <summary>
    /// Creates a new answer item instance from the db answer data
    /// </summary>
    public class AnswerItemFactory
    {
        private AnswerItemFactory()
        {
        }

        /// <summary>
        /// Subsribe the answer items which require it to the correct
        /// answer publisher controls
        /// </summary>
        public static void BindSubscribersToPublishers(AnswerItemCollection answers)
        {
            foreach (AnswerItem item in answers)
            {
                IAnswerSubscriber subscriber = item as IAnswerSubscriber;
                if (subscriber != null)
                {
                    foreach (AnswerData.AnswersRow row in new Answers().GetSubscriptionList(item.AnswerId).Answers.Rows)
                    {
                        AnswerItem answerItem = GetAnswerItem(answers, row.AnswerId);
                        if (answerItem != null)
                        {
                            answerItem.AnswerPublished += new AnswerPublisherEventHandler(subscriber.ProcessPublishedAnswers);
                            answerItem.AnswerPublisherCreated += new AnswerPublisherEventHandler(subscriber.PublisherCreation);
                        }
                    }
                    continue;
                }
            }
        }

        /// <summary>
        /// Creates a new answer item instance from the db answer data
        /// </summary>
        /// <param name="answer">The data source to create the instance from</param>
        /// <param name="question">The question container</param>
        /// <param name="defaultSelectionMode">The default selection mode for the 
        /// item (Radio, checkbox etc...)</param>
        /// <param name="parentControlUniqueID">
        /// Unique ID required to identify global selection groups 
        /// like radiobutton groups
        /// </param>
        /// <param name="showAnswerText">
        /// Assigns the text to the answeritem control ?
        /// </param>
        /// <param name="enableDefaults">
        /// Does the answer set the user default values of fields to the answer web controls
        /// </param>
        public static AnswerItem Create(AnswerData.AnswersRow answer, QuestionItem question, Section section, AnswerSelectionMode defaultSelectionMode, Style answerStyle, ControlRenderMode renderMode, string languageCode, string parentControlUniqueID, bool showAnswerText, VoterAnswersData.VotersAnswersDataTable voterAnswersState, bool enableDefaults)
        {
            AnswerItem item = null;
            try
            {
                if (answer.TypeAssembly != null)
                {
                    item = (AnswerItem) Assembly.Load(answer.TypeAssembly).CreateInstance(answer.TypeNameSpace);
                    item.AnswerId = answer.AnswerId;
                    item.ImageUrl = answer.IsImageURLNull() ? null : answer.ImageURL;
                    item.ID = (section == null) ? (GlobalConfig.AnswerItemName + answer.AnswerId) : string.Concat(new object[] { GlobalConfig.AnswerItemName, answer.AnswerId, GlobalConfig.AnswerSectionName, section.SectionUid });
                    item.AnswerStyle = answerStyle;
                    item.Question = question;
                    item.QuestionId = answer.QuestionId;
                    item.RenderMode = renderMode;
                    item.Text = new PipeManager().PipeValuesInText(answer.QuestionId, answer.AnswerText, voterAnswersState, languageCode);
                    item.ShowAnswerText = showAnswerText;
                    item.SectionContainer = section;
                    item.LanguageCode = languageCode;
                    
                   
                }
            }
            catch (FileNotFoundException)
            {
                item = new AnswerTextItem();
                item.AnswerId = -1;
                item.Text = string.Format(ResourceManager.GetString("AnswerTypeAssemblyNotFoundMessage"), answer.TypeAssembly);
                return item;
            }
            catch (NullReferenceException)
            {
                item = new AnswerTextItem();
                item.AnswerId = -1;
                item.Text = string.Format(ResourceManager.GetString("AnswerTypeNotFoundMessage"), answer.TypeNameSpace, answer.TypeAssembly);
                return item;
            }
            catch (InvalidCastException)
            {
                item = new AnswerTextItem();
                item.AnswerId = -1;
                item.Text = string.Format(ResourceManager.GetString("AnswerTypeInvalidMessage"), answer.TypeNameSpace, answer.TypeAssembly);
                return item;
            }
            if (item is IClientScriptValidator)
            {
                IClientScriptValidator validator = (IClientScriptValidator) item;
                validator.JavascriptCode = answer.JavascriptCode;
                validator.JavascriptFunctionName = answer.JavascriptFunctionName;
                if (answer.JavascriptErrorMessage != null)
                {
                    validator.JavascriptErrorMessage = (ResourceManager.GetString(answer.JavascriptErrorMessage, languageCode) == null) ? answer.JavascriptErrorMessage : ResourceManager.GetString(answer.JavascriptErrorMessage, languageCode);
                }
                else
                {
                    validator.JavascriptErrorMessage = null;
                }
                validator.EnableValidation = true;
            }
            if (item is IMandatoryAnswer)
            {
                ((IMandatoryAnswer) item).Mandatory = answer.Mandatory;
            }
            if ((item is IRegExValidator) && (answer.RegExpression != null))
            {
                ((IRegExValidator) item).RegExpression = answer.RegExpression;
                ((IRegExValidator) item).RegExpressionErrorMessage = answer.RegExMessage;
            }
            AnswerSelectionItem item2 = item as AnswerSelectionItem;
            if (item2 != null)
            {
                item2.UniqueGroupId = parentControlUniqueID;
                item2.AnswerId = answer.AnswerId;
                item2.TypeMode = (AnswerTypeMode) answer.TypeMode;
                item2.SelectionMode = defaultSelectionMode;
                bool flag = (section == null) ? IsUserSelected(answer.AnswerId, 0, voterAnswersState) : IsUserSelected(answer.AnswerId, section.SectionNumber, voterAnswersState);
                if (flag)
                {
                    item2.Selected = flag;
                }
                else
                {
                    item2.Selected = enableDefaults ? answer.Selected : false;
                }
            }
            if (item is IFieldItem)
            {
                IFieldItem item3 = (IFieldItem) item;
                item3.FieldHeight = answer.FieldHeight;
                item3.FieldWidth = answer.FieldWidth;
                item3.FieldLength = answer.FieldLength;
            }
            else if (item is AnswerDataSourceItem)
            {
                AnswerDataSourceItem item4 = (AnswerDataSourceItem) item;
                item4.QuestionId = answer.QuestionId;
                item4.AnswerId = answer.AnswerId;
                if (item4 is AnswerXmlItem)
                {
                    item4.DataSource = answer.XmlDatasource;
                }
                else if (answer.DataSource != null)
                {
                    item4.DataSource = ParseDefaultAnswerText(answer.QuestionId, answer.DataSource, voterAnswersState, languageCode);
                }
                item4.ImageUrl = answer.IsImageURLNull() ? null : answer.ImageURL;
            }
            string str = (section == null) ? GetUserText(answer.AnswerId, 0, voterAnswersState) : GetUserText(answer.AnswerId, section.SectionNumber, voterAnswersState);
            if (str != null)
            {
                item.DefaultText = str;
            }
            else if ((enableDefaults && !answer.IsDefaultTextNull()) && (answer.DefaultText.Length > 0))
            {
                item.DefaultText = ParseDefaultAnswerText(answer.QuestionId, answer.DefaultText, voterAnswersState, languageCode);
            }
            if (item is ExtendedAnswerItem)
            {
                ((ExtendedAnswerItem) item).RestoreProperties();
            }
            return item;
        }

        /// <summary>
        /// Parse an AnswerDataCollection, converts the data and
        /// returns an AnswerItemCollection filled with the correct 
        /// child controls
        /// </summary>
        /// <param name="answers">A collection of answerdata entities</param>
        /// <param name="defaultSelectionMode">The default selection mode for the 
        /// item (Radio, checkbox etc...)</param>
        /// <param name="parentcontrolUniqueID">
        /// Unique ID required to identify global groups 
        /// like radiobutton groups
        /// </param>
        /// <param name="showAnswerText">
        /// Assigns the text to the answeritem control ?
        /// </param>
        /// <param name="voterAnswersState">
        /// Current state of answers, used for piping. If not available
        /// leave it to null 
        /// </param>
        /// <param name="enableDefaults">
        /// Does the answer set the user default values of fields to the answer web controls
        /// </param>
        /// <returns>A collection of answeritem web controls</returns>
        public static AnswerItemCollection CreateAnswerItemCollection(AnswerData answers, QuestionItem question, Section section, AnswerSelectionMode defaultSelectionMode, Style answerStyle, ControlRenderMode renderMode, string languageCode, string parentControlUniqueID, bool showAnswerText, VoterAnswersData.VotersAnswersDataTable voterAnswersState, bool enableDefaults)
        {
            AnswerItemCollection items = new AnswerItemCollection();
            foreach (AnswerData.AnswersRow row in answers.Answers.Rows)
            {
                items.Add(Create(row, question, section, defaultSelectionMode, answerStyle, renderMode, languageCode, parentControlUniqueID, showAnswerText, voterAnswersState, enableDefaults));
            }
            return items;
        }

        /// <summary>
        /// Retrieves the given answer item from the control collection
        /// </summary>
        private static AnswerItem GetAnswerItem(AnswerItemCollection answers, int answerId)
        {
            foreach (AnswerItem item in answers)
            {
                if (item.AnswerId == answerId)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns, if any, the text entered by the user
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="voterAnswersState"></param>
        /// <returns></returns>
        private static string GetUserText(int answerId, int sectionNumber, VoterAnswersData.VotersAnswersDataTable voterAnswersState)
        {
            string answerText = null;
            if ((voterAnswersState != null) && (voterAnswersState.Rows.Count > 0))
            {
                VoterAnswersData.VotersAnswersRow[] rowArray = (VoterAnswersData.VotersAnswersRow[]) voterAnswersState.Select(string.Concat(new object[] { "AnswerId = ", answerId, " AND SectionNumber=", sectionNumber }));
                if (rowArray.Length > 0)
                {
                    answerText = rowArray[0].AnswerText;
                }
            }
            return answerText;
        }

        /// <summary>
        /// Returns if answer has been selected by the user
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="voterAnswersState"></param>
        /// <returns></returns>
        private static bool IsUserSelected(int answerId, int sectionNumber, VoterAnswersData.VotersAnswersDataTable voterAnswersState)
        {
            bool flag = false;
            if ((voterAnswersState != null) && (voterAnswersState.Rows.Count > 0))
            {
                VoterAnswersData.VotersAnswersRow[] rowArray = (VoterAnswersData.VotersAnswersRow[]) voterAnswersState.Select(string.Concat(new object[] { "AnswerId = ", answerId, " AND SectionNumber=", sectionNumber }));
                if (rowArray.Length > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// Parse the given string and replace any
        /// available templates by their runtime values
        /// </summary>
        private static string ParseDefaultAnswerText(int questionId, string currentDefaultText, VoterAnswersData.VotersAnswersDataTable voterAnswersState, string languageCode)
        {
            currentDefaultText = new PipeManager().PipeValuesInText(questionId, currentDefaultText, voterAnswersState, languageCode);
            string str = new Regex(@"#{2}((\S)+)#{2}").Match(currentDefaultText).Groups[1].ToString();
            if ((str.Length > 0) && (HttpContext.Current != null))
            {
                currentDefaultText = (HttpContext.Current.Request[str] == null) ? string.Empty : HttpContext.Current.Request[str];
                return currentDefaultText;
            }
            string str2 = new Regex(@"%{2}((\S)+)%{2}").Match(currentDefaultText).Groups[1].ToString();
            if ((str2.Length > 0) && (HttpContext.Current != null))
            {
                currentDefaultText = (HttpContext.Current.Request.ServerVariables[str2] == null) ? string.Empty : HttpContext.Current.Request.ServerVariables[str2];
                return currentDefaultText;
            }
            string str3 = new Regex(@"@{2}((\S)+)@{2}").Match(currentDefaultText).Groups[1].ToString();
            if ((str3.Length > 0) && (HttpContext.Current != null))
            {
                currentDefaultText = (HttpContext.Current.Session[str3] == null) ? string.Empty : HttpContext.Current.Session[str3].ToString();
                return currentDefaultText;
            }
            string str4 = new Regex(@"&{2}((\S)+)&{2}").Match(currentDefaultText).Groups[1].ToString();
            if ((str4.Length > 0) && (HttpContext.Current != null))
            {
                currentDefaultText = (HttpContext.Current.Request.Cookies[str4] == null) ? string.Empty : HttpContext.Current.Request.Cookies[str4].Value.ToString();
                return currentDefaultText;
            }
            return currentDefaultText;
        }
    }
}

