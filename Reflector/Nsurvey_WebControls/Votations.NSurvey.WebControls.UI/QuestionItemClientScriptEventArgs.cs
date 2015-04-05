namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Provide data for the client script generation event
    /// </summary>
    public class QuestionItemClientScriptEventArgs : EventArgs
    {
        private string _clientScript;
        private Votations.NSurvey.WebControls.UI.Section _section;

        /// <summary>
        /// Generated script
        /// </summary>
        public string ClientScript
        {
            get
            {
                return this._clientScript;
            }
            set
            {
                this._clientScript = value;
            }
        }

        /// <summary>
        /// Which section is holding the answer item
        /// that has generated the script
        /// </summary>
        public Votations.NSurvey.WebControls.UI.Section Section
        {
            get
            {
                return this._section;
            }
            set
            {
                this._section = value;
            }
        }
    }
}

