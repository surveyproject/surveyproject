namespace Votations.NSurvey.WebControls
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provide data for the session (resume load / save) events on the
    /// survey box
    /// </summary>
    public class FormSessionEventArgs : EventArgs
    {
        private string _resumeUId;
        private VoterAnswersData _voterAnswers = new VoterAnswersData();

        public FormSessionEventArgs(VoterAnswersData voterAnswers, string resumeUId)
        {
            this.VoterAnswers = voterAnswers;
            this._resumeUId = resumeUId;
        }

        public string ResumeUId
        {
            get
            {
                return this._resumeUId;
            }
            set
            {
                this._resumeUId = value;
            }
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

