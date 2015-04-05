namespace Votations.NSurvey.WebControls.UI
{
    using System;

    public class AnswerItemInvalidEventArgs : EventArgs
    {
        private string _errorMessage;

        public AnswerItemInvalidEventArgs(string errorMessage)
        {
            this._errorMessage = errorMessage;
        }

        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
            set
            {
                this._errorMessage = value;
            }
        }
    }
}

