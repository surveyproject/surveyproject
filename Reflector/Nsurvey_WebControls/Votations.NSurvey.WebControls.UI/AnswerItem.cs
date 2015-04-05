namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;

    /// <summary>
    /// base class that is required for a class that wants to be used 
    /// as an answer inside a questionitem class
    /// </summary>
    [Serializable, ToolboxItem(false)]
    public abstract class AnswerItem : WebControl, INamingContainer, IPostBackDataHandler
    {
        private int _answerId = -1;
        private Style _answerStyle;
        private string _defaultText = null;
        private string _imageUrl = null;
        private string _languageCode;
        private QuestionItem _question;
        private int _questionId = -1;
        private ControlRenderMode _renderMode = ControlRenderMode.Standard;
        private Votations.NSurvey.WebControls.UI.Section _sectionContainer;
        private bool _showAnswerText = true;
        private string _text = null;

        public event AnswerPublisherEventHandler AnswerPublished;

        public event AnswerPublisherEventHandler AnswerPublisherCreated;

        protected AnswerItem()
        {
        }

        /// <summary>
        /// Returns the selected, posted data of the answer must be 
        /// implemented by all classes in the hierarchy 
        /// </summary>
        /// <returns>The selected, posted answers of the answer</returns>
        protected abstract PostedAnswerDataCollection GetPostedAnswerData();

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            this.EnsureChildControls();
            PostedAnswerDataCollection postedAnswerData = this.GetPostedAnswerData();
            if (postedAnswerData != null)
            {
                AnswerItemEventArgs e = new AnswerItemEventArgs(postedAnswerData);
                this.OnAnswerPost(e);
            }
            return false;
        }

        /// <summary>
        /// Event to trigger if any messages must be send
        /// to the container
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAnswerMessage(AnswerItemMessageEventArgs e)
        {
            base.RaiseBubbleEvent(this, e);
        }

        /// <summary>
        /// Event to trigger if answers have been posted
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAnswerPost(AnswerItemEventArgs e)
        {
            base.RaiseBubbleEvent(this, e);
        }

        /// <summary>
        /// Post an event to notify subscribers that answers has been published
        /// </summary>
        protected virtual void OnAnswerPublished(AnswerItemEventArgs e)
        {
            if (this.AnswerPublished != null)
            {
                this.AnswerPublished(this, e);
            }
        }

        /// <summary>
        /// Post an event to notify subscribers that the publisher 
        /// has been created with its child controls tree and properties 
        /// </summary>
        protected virtual void OnAnswerPublisherCreated(AnswerItemEventArgs e)
        {
            if (this.AnswerPublisherCreated != null)
            {
                this.AnswerPublisherCreated(this, e);
            }
        }

        /// <summary>
        /// Event to trigger if any server side validation errors have
        /// occured
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnInvalidAnswer(AnswerItemInvalidEventArgs e)
        {
            base.RaiseBubbleEvent(this, e);
        }

        /// <summary>
        /// Register the control to receive a callback from the page
        /// on the postback event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.Page.RegisterRequiresPostBack(this);
        }

        public void RaisePostDataChangedEvent()
        {
        }

        public virtual int AnswerId
        {
            get
            {
                return this._answerId;
            }
            set
            {
                this._answerId = value;
            }
        }

        public Style AnswerStyle
        {
            get
            {
                if (this._answerStyle != null)
                {
                    return this._answerStyle;
                }
                return (this._answerStyle = new Style());
            }
            set
            {
                this._answerStyle = value;
            }
        }

        /// <summary>
        /// Can be used to restore a previous state 
        /// of the item
        /// </summary>
        public virtual string DefaultText
        {
            get
            {
                return this._defaultText;
            }
            set
            {
                this._defaultText = value;
            }
        }

        public virtual bool HasSubscribers
        {
            get
            {
                return (this.AnswerPublished != null);
            }
        }

        public virtual string ImageUrl
        {
            get
            {
                return this._imageUrl;
            }
            set
            {
                this._imageUrl = value;
            }
        }

        /// <summary>
        /// Language in which 
        /// the messages will be 
        /// rendered 
        /// </summary>
        /// <remarks>
        /// if no language is specified
        /// the default thread culture is taken
        /// </remarks>
        public string LanguageCode
        {
            get
            {
                return this._languageCode;
            }
            set
            {
                this._languageCode = value;
            }
        }

        /// <summary>
        /// Gets a reference to the QuestionItem instance that contains 
        /// the answer item
        /// </summary>
        public virtual QuestionItem Question
        {
            get
            {
                return this._question;
            }
            set
            {
                this._question = value;
            }
        }

        public virtual int QuestionId
        {
            get
            {
                return this._questionId;
            }
            set
            {
                this._questionId = value;
            }
        }

        /// <summary>
        /// Defines in which mode the control is going 
        /// to be rendered eg : if its for edition or if its 
        /// to let the user answer the question
        /// </summary>
        public virtual ControlRenderMode RenderMode
        {
            get
            {
                return this._renderMode;
            }
            set
            {
                this._renderMode = value;
            }
        }

        /// <summary>
        /// Current section that contains
        /// the answer
        /// </summary>
        public Votations.NSurvey.WebControls.UI.Section SectionContainer
        {
            get
            {
                return this._sectionContainer;
            }
            set
            {
                this._sectionContainer = value;
            }
        }

        public virtual bool ShowAnswerText
        {
            get
            {
                return this._showAnswerText;
            }
            set
            {
                this._showAnswerText = value;
            }
        }

        public virtual string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }
    }
}

