namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provide data for the overflow and answer missing events
    /// </summary>
    public class QuestionItemAnswersEventArgs : EventArgs
    {
        private PostedAnswerDataCollection _answers = new PostedAnswerDataCollection();

        public PostedAnswerDataCollection Answers
        {
            get
            {
                return this._answers;
            }
            set
            {
                this._answers = value;
            }
        }
    }
}

