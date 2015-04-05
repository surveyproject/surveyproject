namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using Votations.NSurvey.Data;

    public class SectionAnswersItemEventArgs : EventArgs
    {
        private GridAnswerDataCollection _sectionAnswers;
        private int _sectionNumber;

        public SectionAnswersItemEventArgs(int sectionNumber, GridAnswerDataCollection sectionAnswers)
        {
            this.SectionNumber = sectionNumber;
            this.SectionAnswers = sectionAnswers;
        }

        /// <summary>
        /// Posted answers related to this section
        /// </summary>
        public GridAnswerDataCollection SectionAnswers
        {
            get
            {
                return this._sectionAnswers;
            }
            set
            {
                this._sectionAnswers = value;
            }
        }

        /// <summary>
        /// The section number to which this event is related
        /// </summary>
        public int SectionNumber
        {
            get
            {
                return this._sectionNumber;
            }
            set
            {
                this._sectionNumber = value;
            }
        }
    }
}

