namespace Votations.NSurvey.Helpers
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Pipe manager handles the question/answer token 
    /// replacement with existing answers
    /// </summary>
    public class PipeManager
    {
        private PipeData _pipeData = null;
        private int previousMatchCount = 0;

        /// <summary>
        /// Replace the pipe token with the answer text entered by 
        /// the user
        /// </summary>
        protected virtual string PipeAnswerText(int answerId, VoterAnswersData.VotersAnswersDataTable surveyAnswers)
        {
            VoterAnswersData.VotersAnswersRow[] rowArray = (VoterAnswersData.VotersAnswersRow[]) surveyAnswers.Select("AnswerId=" + answerId);
            if (rowArray.Length > 0)
            {
                return rowArray[0].AnswerText;
            }
            return string.Empty;
        }

        /// <summary>
        /// Replace the pipe token with the question answers
        /// </summary>
        protected virtual string PipeQuestionAnswers(int questionId, VoterAnswersData.VotersAnswersDataTable surveyAnswers, string languageCode)
        {
            StringBuilder builder = new StringBuilder();
            VoterAnswersData.VotersAnswersRow[] rowArray = (VoterAnswersData.VotersAnswersRow[]) surveyAnswers.Select("QuestionId=" + questionId);
            int index = 0;
            while (index < rowArray.Length)
            {
                PipeData.AnswersRow[] rowArray2 = (PipeData.AnswersRow[]) this._pipeData.Answers.Select("AnswerId=" + rowArray[index].AnswerId);
                if (rowArray2.Length > 0)
                {
                    builder.Append(rowArray2[0].AnswerText);
                }
                index++;
                if ((index + 1) == rowArray.Length)
                {
                    builder.Append(ResourceManager.GetString("PipeValuesSeparator", languageCode));
                }
                else if (index < rowArray.Length)
                {
                    builder.Append(", ");
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Parse the source text for pipe token and replace
        /// the token with the correct text depending on the
        /// given survey answers
        /// </summary>
        public string PipeValuesInText(int questionId, string sourceText, VoterAnswersData.VotersAnswersDataTable surveyAnswers, string languageCode)
        {
            Regex regex = new Regex(@"\[{2}((\S)+)\]{2}");
            if (surveyAnswers != null)
            {
                Match match = regex.Match(sourceText);
                if (match.Success)
                {
                    if (HttpContext.Current != null)
                    {
                        if (HttpContext.Current.Cache["NSurvey:PipeData"] == null)
                        {
                            HttpContext.Current.Cache.Insert("NSurvey:PipeData", new Surveys().GetSurveyPipeDataFromQuestionId(questionId), null, DateTime.Now.AddMinutes(1.0), TimeSpan.Zero);
                        }
                        this._pipeData = (PipeData) HttpContext.Current.Cache["NSurvey:PipeData"];
                    }
                    else
                    {
                        this._pipeData = new Surveys().GetSurveyPipeDataFromQuestionId(questionId);
                    }
                    while (match.Success)
                    {
                        string str = match.Groups[1].ToString();
                        if (str.Length > 0)
                        {
                            PipeData.QuestionsRow[] rowArray = (PipeData.QuestionsRow[]) this._pipeData.Questions.Select("QuestionPipeAlias='" + str + "'");
                            if (rowArray.Length > 0)
                            {
                                sourceText = sourceText.Replace("[[" + str + "]]", this.PipeQuestionAnswers(rowArray[0].QuestionId, surveyAnswers, languageCode));
                            }
                            else
                            {
                                PipeData.AnswersRow[] rowArray2 = (PipeData.AnswersRow[]) this._pipeData.Answers.Select("AnswerPipeAlias='" + str + "'");
                                if (rowArray2.Length > 0)
                                {
                                    sourceText = sourceText.Replace("[[" + str + "]]", this.PipeAnswerText(rowArray2[0].AnswerId, surveyAnswers));
                                }
                            }
                        }
                        match = match.NextMatch();
                    }
                }
                match = regex.Match(sourceText);
                if ((match.Length > 0) && (this.previousMatchCount != match.Length))
                {
                    this.previousMatchCount = match.Length;
                    return this.PipeValuesInText(questionId, sourceText, surveyAnswers, languageCode);
                }
            }
            return sourceText;
        }
    }
}

