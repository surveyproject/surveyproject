namespace Votations.NSurvey.WebControls.UI
{
    using System;

    public class MatrixChildQuestion : QuestionItem
    {
        private AnswerItemCollection _answers = new AnswerItemCollection();

        /// <summary>
        /// Answer items collection that will be used
        /// as child controls
        /// </summary>
        public AnswerItemCollection Answers
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

