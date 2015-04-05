namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;

    /// <summary>
    /// Makes to link answer publishers to answer subscribers
    /// </summary>
    [ToolboxItem(false)]
    public class AnswerConnection : UserControl, INamingContainer
    {
        private int _publisherId = -1;
        private int _subscriberId = -1;

        public int PublisherId
        {
            get
            {
                return this._publisherId;
            }
            set
            {
                this._publisherId = value;
            }
        }

        public int SubscriberId
        {
            get
            {
                return this._subscriberId;
            }
            set
            {
                this._subscriberId = value;
            }
        }
    }
}

