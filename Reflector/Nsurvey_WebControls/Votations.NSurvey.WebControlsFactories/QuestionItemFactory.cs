namespace Votations.NSurvey.WebControlsFactories
{
    using System;
    using System.Reflection;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Helpers;
    using Votations.NSurvey.WebControls.UI;

    /// <summary>
    /// Create a web control compatible instance 
    /// of the given question.
    /// </summary>
    public class QuestionItemFactory
    {
        private QuestionItemFactory()
        {
        }

        private static string HelpTextInSmallFont(string text)
        {
            return string.Format("<div class=\"HelpTextSmallFont\">{0}</div>", text);
        }
        public static QuestionItem Create(QuestionData.QuestionsRow question, string languageCode, string parentControlUniqueID, 
            int questionRandomSeed, VoterAnswersData.VotersAnswersDataTable voterAnswersState, 
            bool enableAnswerDefaults,bool isDesignMode=false)
        {
            QuestionItem item;
            if ((question.TypeAssembly.Length == 0) || (question.TypeAssembly == null))
            {
                throw new ApplicationException("Could not create an instance because the question data has no assembly type specified");
            }
            try
            {
                item = (QuestionItem) Assembly.Load(question.TypeAssembly).CreateInstance(question.TypeNameSpace);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException("Concrete type " + question.TypeNameSpace + " is not a child of the questionitem abstract class");
            }
            catch (NullReferenceException)
            {
                throw new InvalidCastException("specfied type " + question.TypeNameSpace + " could not be found in the specifed assembly " + question.TypeAssembly);
            }
            item.ID = "Question" + question.QuestionId.ToString();
            item.QuestionId = question.QuestionId;
            item.Text = new PipeManager().PipeValuesInText(question.QuestionId, question.QuestionText+
               ( (question.ShowHelpText && !isDesignMode )?(
                question.IsHelpTextNull()?string.Empty: HelpTextInSmallFont(question.HelpText)):string.Empty)//JJSurveyBoxChange
                , voterAnswersState, languageCode) ;
            item.LanguageCode = languageCode;
            item.QuestionId_Text = question.QuestionIdText;
            item.HelpText = question.HelpText;
            SectionQuestion question2 = item as SectionQuestion;
            if ((question2 != null) && (question.RepeatableSectionModeId != 0))
            {
                question2.RepeatMode = (RepeatableSectionMode) question.RepeatableSectionModeId;
                question2.MaxSections = question.MaxSections;
                question2.GridAnswers = new Questions().GetQuestionSectionGridAnswers(question.QuestionId);
                if ((question.AddSectionLinkText != null) && (question.AddSectionLinkText.Length > 0))
                {
                    question2.AddSectionLinkText = question.AddSectionLinkText;
                }
                if ((question.DeleteSectionLinkText != null) && (question.DeleteSectionLinkText.Length > 0))
                {
                    question2.DeleteSectionLinkText = question.DeleteSectionLinkText;
                }
                if ((question.EditSectionLinkText != null) && (question.EditSectionLinkText.Length > 0))
                {
                    question2.EditSectionLinkText = question.EditSectionLinkText;
                }
                if ((question.UpdateSectionLinkText != null) && (question.UpdateSectionLinkText.Length > 0))
                {
                    question2.UpdateSectionLinkText = question.UpdateSectionLinkText;
                }
            }
            SingleQuestion question3 = item as SingleQuestion;
            if (question3 != null)
            {
              
                question3.VoterAnswersState = voterAnswersState;
                question3.EnableAnswersDefault = enableAnswerDefaults;
                question3.MinSelectionRequired = question.MinSelectionRequired;
                question3.MaxSelectionAllowed = question.MaxSelectionAllowed;
                question3.ColumnsNumber = question.ColumnsNumber;
                question3.LayoutMode = question.IsLayoutModeIdNull() ? QuestionLayoutMode.Vertical : ((QuestionLayoutMode) question.LayoutModeId);
                if (question.RandomizeAnswers)
                {
                    question3.DataSource = new Answers().GetRandomAnswers(question.QuestionId, questionRandomSeed, languageCode);
                    return question3;
                }
                question3.DataSource = new Answers().GetAnswers(question.QuestionId, languageCode);
                return question3;
            }
            MatrixQuestion question4 = item as MatrixQuestion;
            if (question4 != null)
            {
                question4.VoterAnswersState = voterAnswersState;
                question4.EnableAnswersDefault = enableAnswerDefaults;
                question4.MinSelectionRequired = question.MinSelectionRequired;
                question4.MaxSelectionAllowed = question.MaxSelectionAllowed;
                question4.DataSource = new Questions().GetMatrixChildQuestions(question.QuestionId, languageCode);
                return question4;
            }
            return item;
        }

        /// <summary>
        /// Converts a stronlgy typed MatrixChildQuestionData dataset
        /// to a webcontrol collection
        /// </summary>
        /// <param name="childQuestions">The questions / answers data</param>
        /// <param name="parentControlUniqueID">
        /// Unique ID required to identify global groups 
        /// like radiobutton groups
        /// </param>
        /// <returns>a web control collection of MatrixChildQuestion</returns>
        public static MatrixChildCollection CreateQuestionChildCollection(MatrixChildQuestionData childQuestions, Section sectionContainer, string languageCode, string parentControlUniqueID, AnswerSelectionMode selectionMode, Style answerStyle, ControlRenderMode renderMode, VoterAnswersData.VotersAnswersDataTable voterAnswersState, bool enableAnswersDefault)
        {
            MatrixChildCollection childs = new MatrixChildCollection();
            
            foreach (MatrixChildQuestionData.ChildQuestionsRow row in childQuestions.ChildQuestions.Rows)
            {
                MatrixChildQuestion question = new MatrixChildQuestion();
                question.QuestionId = row.QuestionId;
                question.Text = new PipeManager().PipeValuesInText(row.QuestionId, row.QuestionText, voterAnswersState, languageCode);
                AnswerData answers = new AnswerData();

//                answers.Merge(row.GetAnswersRows());

				MatrixChildQuestionData.AnswersRow[] ar = row.GetAnswersRows();
					foreach (MatrixChildQuestionData.AnswersRow r in ar) {
								r.Table.Namespace = answers.Namespace; }
					answers.Merge(ar);
				
                question.Answers = AnswerItemFactory.CreateAnswerItemCollection(answers, question, sectionContainer, selectionMode, answerStyle, renderMode, languageCode, parentControlUniqueID, false, voterAnswersState, enableAnswersDefault);
                childs.Add(question);
            }
            return childs;
        }
    }
}

