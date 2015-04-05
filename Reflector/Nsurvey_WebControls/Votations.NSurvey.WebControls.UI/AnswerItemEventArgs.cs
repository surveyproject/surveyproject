namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using Votations.NSurvey.Data;

    public class AnswerItemEventArgs : EventArgs
    {
        private PostedAnswerDataCollection _postedAnswers;

        public AnswerItemEventArgs(PostedAnswerDataCollection postedAnswers)
        {
            this.PostedAnswers = postedAnswers;
        }

        public PostedAnswerDataCollection PostedAnswers
        {
            get
            {
                return this._postedAnswers;
            }
            set
            {
                this._postedAnswers = value;
            }
        }
    }
}

