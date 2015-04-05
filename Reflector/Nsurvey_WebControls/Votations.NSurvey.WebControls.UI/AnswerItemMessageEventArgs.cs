namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Allow an answer item to create a custom information message
    /// and send it to the container
    /// </summary>
    public class AnswerItemMessageEventArgs : EventArgs
    {
        private string _message;
        private AnswerMessageType _messageType;

        public AnswerItemMessageEventArgs(string message, AnswerMessageType messageType)
        {
            this._message = message;
            this._messageType = messageType;
        }

        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }

        public AnswerMessageType MessageType
        {
            get
            {
                return this._messageType;
            }
            set
            {
                this._messageType = value;
            }
        }
    }
}

