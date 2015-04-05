namespace Votations.NSurvey.WebControls
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provide data for the submit events on the
    /// survey box
    /// </summary>
    public class FormItemEventArgs : EventArgs
    {
        private VoterAnswersData _voterAnswers = new VoterAnswersData();

        public FormItemEventArgs(VoterAnswersData voterAnswers)
        {
            this.VoterAnswers = voterAnswers;
        }

        public VoterAnswersData VoterAnswers
        {
            get
            {
                return this._voterAnswers;
            }
            set
            {
                this._voterAnswers = value;
            }
        }
    }
}

