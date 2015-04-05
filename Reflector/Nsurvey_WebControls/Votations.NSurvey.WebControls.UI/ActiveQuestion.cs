namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Base active question class for all question controls that needs handling 
    /// of the posted child answeritems field values 
    /// </summary>
    public abstract class ActiveQuestion : QuestionItem, IPostBackDataHandler
    {
        private ArrayList _answerMessages = new ArrayList();
        private Style _confirmationMessageStyle;
        private bool _displayValidationErrorMessages = true;
        private bool _enableAnswersDefault = true;
        private bool _enableClientSideValidation = false;
        private bool _enableServerSideValidation = false;
        private bool _hasInvalidAnswers = false;
        private AnswerItemCollection _invalidAnswerItems = new AnswerItemCollection();
        private ArrayList _invalidErrorMessages = new ArrayList();
        private bool _isSelectionOverflow = false;
        private bool _isSelectionRequired = false;
        private string _maxAnswerSelectionMessage;
        private int _maxSelectionAllowed = 1;
        private string _minAnswerSelectionMessage;
        private int _minSelectionRequired = 0;
        private QuestionItemAnswersEventArgs _questionEventArgs = new QuestionItemAnswersEventArgs();
        private string _validationMark = "*";
        private Style _validationMarkStyle;
        private Style _validationMessageStyle;
        private VoterAnswersData.VotersAnswersDataTable _voterAnswersState;

        /// <summary>
        /// Events posted by the question
        /// </summary>
        public event AnswerPostedEventHandler AnswerPosted;

        public event ClientScriptGeneratedEventHandler ClientScriptGenerated;

        public event InvalidAnswersEventHandler InvalidAnswers;

        public event SelectionOverflowEventHandler SelectionOverflow;

        public event SelectionRequiredEventHandler SelectionRequired;

        protected ActiveQuestion()
        {
        }

        /// <summary>
        /// Check if any answer has sent a message to display
        /// </summary>
        private void DisplayAnswerMessages()
        {
            if (this._answerMessages.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                for (int i = 0; i < this._answerMessages.Count; i++)
                {
                    AnswerItemMessageEventArgs args = (AnswerItemMessageEventArgs) this._answerMessages[i];
                    if (args.MessageType == AnswerMessageType.Information)
                    {
                        builder2.Append(args.Message);
                        if ((i + 1) < this._answerMessages.Count)
                        {
                            builder2.Append("<br />");
                        }
                    }
                    else
                    {
                        builder.Append(args.Message);
                        if ((i + 1) < this._answerMessages.Count)
                        {
                            builder.Append("<br />");
                        }
                    }
                }
                if (builder.Length > 0)
                {
                    base.QuestionTable.Rows[0].ControlStyle.CopyFrom(this.ValidationMessageStyle);
                    if (base.QuestionTable.Rows[0].Cells[0].Controls.Count > 0)
                    {
                        base.QuestionTable.Rows[0].Cells[0].Controls.Add(new LiteralControl("<br />" + builder.ToString()));
                    }
                    else
                    {
                        base.QuestionTable.Rows[0].Cells[0].Controls.Add(new LiteralControl(builder.ToString()));
                    }
                }
                if (builder2.Length > 0)
                {
                    base.QuestionTable.Rows[1].ControlStyle.CopyFrom(this.ConfirmationMessageStyle);
                    if (base.QuestionTable.Rows[1].Cells[0].Controls.Count > 0)
                    {
                        base.QuestionTable.Rows[1].Cells[0].Controls.Add(new LiteralControl("<br />" + builder2.ToString()));
                    }
                    else
                    {
                        base.QuestionTable.Rows[1].Cells[0].Controls.Add(new LiteralControl(builder2.ToString()));
                    }
                }
            }
        }

        /// <summary>
        /// Check if everything was validated 
        /// and if not, add the errors to the layout
        /// </summary>
        private void DisplayErrorMessages()
        {
            if ((this.IsSelectionOverflow || this.IsSelectionRequired) || (this._invalidErrorMessages.Count > 0))
            {
                base.QuestionTable.Rows[0].ControlStyle.CopyFrom(this.ValidationMessageStyle);
                StringBuilder builder = new StringBuilder();
                if (this.IsSelectionOverflow)
                {
                    builder.Append(string.Format(this.MaxAnswerSelectionMessage, this.MaxSelectionAllowed));
                }
                if (this.IsSelectionRequired)
                {
                    if (this.IsSelectionOverflow)
                    {
                        builder.Append("<br />");
                    }
                    builder.Append(string.Format(this.MinAnswerSelectionMessage, this.MinSelectionRequired));
                }
                if (this._invalidErrorMessages.Count > 0)
                {
                    if (this.IsSelectionRequired)
                    {
                        builder.Append("<br />");
                    }
                    for (int i = 0; i < this._invalidErrorMessages.Count; i++)
                    {
                        builder.Append(this._invalidErrorMessages[i].ToString());
                        if ((i + 1) < this._invalidErrorMessages.Count)
                        {
                            builder.Append("<br />");
                        }
                    }
                }
                base.QuestionTable.Rows[0].Cells[0].Controls.Add(new LiteralControl(builder.ToString()));
            }
        }

        /// <summary>
        /// Check if selections has reached the max quota
        /// </summary>
        protected abstract bool MaxSelectionsReached(PostedAnswerDataCollection answers);
        /// <summary>
        /// Check if enough selections have been made
        /// </summary>
        protected abstract bool MinSelectionsRequired(PostedAnswerDataCollection answers);
        /// <summary>
        /// Post an event when all answers have been
        /// collected and grouped.
        /// </summary>
        /// <param name="e">The question's answers</param>
        protected virtual void OnAnswersPost(QuestionItemAnswersEventArgs e)
        {
            if (this.AnswerPosted != null)
            {
                this.AnswerPosted(this, e);
            }
        }

        /// <summary>
        /// Takes care of the children controls (answer items) events
        /// </summary>
        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            if (e is AnswerItemEventArgs)
            {
                this._questionEventArgs.Answers.AddRange(((AnswerItemEventArgs) e).PostedAnswers);
                return true;
            }
            if (e is AnswerItemInvalidEventArgs)
            {
                this._invalidErrorMessages.Add(((AnswerItemInvalidEventArgs) e).ErrorMessage);
                this._invalidAnswerItems.Add((AnswerItem) source);
                this._hasInvalidAnswers = true;
                return true;
            }
            if (e is AnswerItemMessageEventArgs)
            {
                this._answerMessages.Add((AnswerItemMessageEventArgs) e);
            }
            return false;
        }

        /// <summary>
        /// Post an event when a client script has been generated
        /// </summary>
        /// <param name="e">Question's invalid answers args</param>
        protected virtual void OnClientScriptGeneration(QuestionItemClientScriptEventArgs e)
        {
            if (this.ClientScriptGenerated != null)
            {
                this.ClientScriptGenerated(this, e);
            }
        }

        /// <summary>
        /// Post an event when there are invalid server side checked answers
        /// </summary>
        /// <param name="e">Question's invalid answers args</param>
        protected virtual void OnInvalidAnswers(QuestionItemInvalidAnswersEventArgs e)
        {
            if (this.InvalidAnswers != null)
            {
                this.InvalidAnswers(this, e);
            }
        }

        /// <summary>
        /// Register the control for postback
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            if ((this.Page != null) && this.Enabled)
            {
                this.Page.RegisterRequiresPostBack(this);
            }
            if (this.EnableServerSideValidation)
            {
                this.DisplayErrorMessages();
            }
            this.DisplayAnswerMessages();
            base.OnPreRender(e);
        }

        /// <summary>
        /// Post an event when there are too many selections
        /// </summary>
        /// <param name="e">Question's answers</param>
        protected virtual void OnSelectionOverflow(QuestionItemAnswersEventArgs e)
        {
            if (this.SelectionOverflow != null)
            {
                this.SelectionOverflow(this, e);
            }
        }

        /// <summary>
        /// Post an event when there is a missing
        /// answer
        /// </summary>
        /// <param name="e">Question's answer if any was answered</param>
        protected virtual void OnSelectionRequired(QuestionItemAnswersEventArgs e)
        {
            if (this.SelectionRequired != null)
            {
                this.SelectionRequired(this, e);
            }
        }

        /// <summary>
        /// Last chance to change any answer that was posted
        /// by child answeritems before they get posted to 
        /// subscribers
        /// </summary>
        protected abstract void PostedAnswersHandler(PostedAnswerDataCollection answers);
        /// <summary>
        /// Handle all the postback data and store the select answers and field 
        /// of the question
        /// </summary>
        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return true;
        }

        /// <summary>
        /// Raise the AnswerPosted event
        /// </summary>
        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            this._isSelectionRequired = this.MinSelectionsRequired(this._questionEventArgs.Answers);
            this._isSelectionOverflow = this.MaxSelectionsReached(this._questionEventArgs.Answers);
            if (this.EnableServerSideValidation)
            {
                if (this.IsSelectionRequired)
                {
                    this.OnSelectionRequired(this._questionEventArgs);
                }
                else if (this.IsSelectionOverflow)
                {
                    this.OnSelectionOverflow(this._questionEventArgs);
                }
                else if (this.HasInvalidAnswers)
                {
                    QuestionItemInvalidAnswersEventArgs e = new QuestionItemInvalidAnswersEventArgs();
                    e.InvalidItems = this._invalidAnswerItems;
                    this.OnInvalidAnswers(e);
                }
                else if (this._questionEventArgs.Answers.Count > 0)
                {
                    this.PostedAnswersHandler(this._questionEventArgs.Answers);
                    this.OnAnswersPost(this._questionEventArgs);
                }
            }
            else
            {
                this.PostedAnswersHandler(this._questionEventArgs.Answers);
                this.OnAnswersPost(this._questionEventArgs);
            }
        }

        /// <summary>
        /// Style of the confirmation messages of the survey and questions
        /// </summary>
        [DefaultValue((string) null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), Category("Styles")]
        public Style ConfirmationMessageStyle
        {
            get
            {
                if (this._confirmationMessageStyle != null)
                {
                    return this._confirmationMessageStyle;
                }
                return new Style();
            }
            set
            {
                this._confirmationMessageStyle = value;
            }
        }

        /// <summary>
        /// Do we add the validation error messages
        /// to the question layout
        /// </summary>
        public bool DisplayValidationErrorMessages
        {
            get
            {
                return this._displayValidationErrorMessages;
            }
            set
            {
                this._displayValidationErrorMessages = value;
            }
        }

        /// <summary>
        /// Enable question's answers default values
        /// </summary>
        public bool EnableAnswersDefault
        {
            get
            {
                return this._enableAnswersDefault;
            }
            set
            {
                this._enableAnswersDefault = value;
            }
        }

        /// <summary>
        /// Enable client side validation of answers which needs it
        /// </summary>
        public bool EnableClientSideValidation
        {
            get
            {
                return this._enableClientSideValidation;
            }
            set
            {
                this._enableClientSideValidation = value;
            }
        }

        /// <summary>
        /// Enable server side validation of answers which needs it
        /// </summary>
        public bool EnableServerSideValidation
        {
            get
            {
                return this._enableServerSideValidation;
            }
            set
            {
                this._enableServerSideValidation = value;
            }
        }

        /// <summary>
        /// Has the question any invalid answer
        /// </summary>
        public bool HasInvalidAnswers
        {
            get
            {
                return this._hasInvalidAnswers;
            }
        }

        /// <summary>
        /// Has the question too much answers ?
        /// </summary>
        public bool IsSelectionOverflow
        {
            get
            {
                return this._isSelectionOverflow;
            }
        }

        /// <summary>
        /// Has the question been answered ?
        /// </summary>
        public bool IsSelectionRequired
        {
            get
            {
                return this._isSelectionRequired;
            }
        }

        /// <summary>
        /// Message show when too much selections were
        /// selected in the question
        /// </summary>
        public string MaxAnswerSelectionMessage
        {
            get
            {
                if (this._maxAnswerSelectionMessage != null)
                {
                    return this._maxAnswerSelectionMessage;
                }
                return ResourceManager.GetString("MaxAnswerSelection", base.LanguageCode);
            }
            set
            {
                this._maxAnswerSelectionMessage = value;
            }
        }

        /// <summary>
        /// Max. of selection required in the question
        /// (Fields are not checked)
        /// </summary>
        public int MaxSelectionAllowed
        {
            get
            {
                return this._maxSelectionAllowed;
            }
            set
            {
                this._maxSelectionAllowed = value;
            }
        }

        /// <summary>
        /// Message show when not enough selections were
        /// selected in the question
        /// </summary>
        public string MinAnswerSelectionMessage
        {
            get
            {
                if (this._minAnswerSelectionMessage != null)
                {
                    return this._minAnswerSelectionMessage;
                }
                return ResourceManager.GetString("MinAnswerSelection", base.LanguageCode);
            }
            set
            {
                this._minAnswerSelectionMessage = value;
            }
        }

        /// <summary>
        /// Min. of selection required in the question
        /// (Fields are not checked)
        /// </summary>
        public int MinSelectionRequired
        {
            get
            {
                return this._minSelectionRequired;
            }
            set
            {
                this._minSelectionRequired = value;
            }
        }

        /// <summary>
        /// Validation mark shown near the question
        /// that requires answers
        /// </summary>
        public string ValidationMark
        {
            get
            {
                return this._validationMark;
            }
            set
            {
                this._validationMark = value;
            }
        }

        /// <summary>
        /// Style used for the question's mark
        /// </summary>
        [Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DefaultValue((string) null)]
        public Style ValidationMarkStyle
        {
            get
            {
                if (this._validationMarkStyle != null)
                {
                    return this._validationMarkStyle;
                }
                return (this._validationMarkStyle = new Style());
            }
            set
            {
                this._validationMarkStyle = value;
            }
        }

        /// <summary>
        /// Style used for the question's error messages
        /// </summary>
        [Category("Styles"), PersistenceMode(PersistenceMode.InnerProperty), DefaultValue((string) null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style ValidationMessageStyle
        {
            get
            {
                if (this._validationMessageStyle != null)
                {
                    return this._validationMessageStyle;
                }
                return (this._validationMessageStyle = new Style());
            }
            set
            {
                this._validationMessageStyle = value;
            }
        }

        /// <summary>
        /// Global answers that a voter has answered 
        /// to the survey, used for piping in the answers 
        /// purposes, its optional and can be null
        /// </summary>
        public VoterAnswersData.VotersAnswersDataTable VoterAnswersState
        {
            get
            {
                return this._voterAnswersState;
            }
            set
            {
                this._voterAnswersState = value;
            }
        }
    }
}

