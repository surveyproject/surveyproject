namespace Votations.NSurvey.WebControls
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Provide data for the question next / previous page navigation events on the
    /// survey box
    /// </summary>
    public class FormNavigationEventArgs : EventArgs
    {
        private FormNavigationMode _navigationMode;
        private int _sourcePageIndex;
        private int _sourceQuestionNumber;
        private int _targetPageIndex;
        private int _targetQuestionNumber;
        private VoterAnswersData _voterAnswers;

        public FormNavigationEventArgs(VoterAnswersData voterAnswers, int sourcePageIndex, int targetPage, int sourceQuestionNumber, int targetQuestionNumber, FormNavigationMode navigationMode)
        {
            this._voterAnswers = voterAnswers;
            this._sourcePageIndex = sourcePageIndex;
            this._targetPageIndex = targetPage;
            this._sourceQuestionNumber = sourceQuestionNumber;
            this._targetQuestionNumber = targetQuestionNumber;
            this._navigationMode = navigationMode;
        }

        public FormNavigationMode NavigationMode
        {
            get
            {
                return this._navigationMode;
            }
            set
            {
                this._navigationMode = value;
            }
        }

        public int SourcePageIndex
        {
            get
            {
                return this._sourcePageIndex;
            }
            set
            {
                this._sourcePageIndex = value;
            }
        }

        public int SourceQuestionNumber
        {
            get
            {
                return this._sourceQuestionNumber;
            }
            set
            {
                this._sourceQuestionNumber = value;
            }
        }

        public int TargetPageIndex
        {
            get
            {
                return this._targetPageIndex;
            }
            set
            {
                this._targetPageIndex = value;
            }
        }

        public int TargetQuestionNumber
        {
            get
            {
                return this._targetQuestionNumber;
            }
            set
            {
                this._targetQuestionNumber = value;
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

