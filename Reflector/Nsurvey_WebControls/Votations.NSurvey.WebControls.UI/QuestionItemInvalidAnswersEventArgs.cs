namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Provide data for the invalid answer events
    /// </summary>
    public class QuestionItemInvalidAnswersEventArgs : EventArgs
    {
        private AnswerItemCollection _invalidAnswers = new AnswerItemCollection();

        public AnswerItemCollection InvalidItems
        {
            get
            {
                return this._invalidAnswers;
            }
            set
            {
                this._invalidAnswers = value;
            }
        }
    }
}

