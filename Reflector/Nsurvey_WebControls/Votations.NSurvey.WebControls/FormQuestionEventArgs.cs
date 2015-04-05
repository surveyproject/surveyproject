namespace Votations.NSurvey.WebControls
{
    using System;
    using Votations.NSurvey.WebControls.UI;

    /// <summary>
    /// Provide data for the question creation and bound events on the
    /// survey box
    /// </summary>
    public class FormQuestionEventArgs : EventArgs
    {
        private QuestionItem _question;

        public FormQuestionEventArgs(QuestionItem question)
        {
            this.Question = question;
        }

        public QuestionItem Question
        {
            get
            {
                return this._question;
            }
            set
            {
                this._question = value;
            }
        }
    }
}

