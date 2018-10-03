namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Base class to represent a field behavior with external java 
    /// script validation code
    /// </summary>
    public class AnswerFieldItem : AnswerItem, IFieldItem, IClientScriptValidator, IAnswerPublisher, IMandatoryAnswer, IRegExValidator
    {
        private bool _enableValidation = false;
        private int _fieldHeight = 1;
        private int _fieldLength = 1;
        protected TextBox _fieldTextBox = new TextBox();
        private int _fieldWidth = 1;
        private string _javascriptCode = null;
        private string _javascriptErrorMessage = null;
        private string _javascriptFunctionName = null;
        private bool _mandatory = false;
        private string _regExpression = null;
        private string _regExpressionErrorMessage = null;

        /// <summary>
        /// Create the "layout" and adds the textbox control to the 
        /// control tree
        /// </summary>
        protected override void CreateChildControls()
        {
            if (this.ShowAnswerText)
            {
                if ((this.ImageUrl != null) && (this.ImageUrl.Length != 0))
                {
                    Image child = new Image();
                    child.ImageUrl = this.ImageUrl;
                    child.ImageAlign = ImageAlign.Middle;
                    child.ToolTip = this.Text;
                    this.Controls.Add(child);
                }
                else
                {

                    Label label= new Label();
                    label.CssClass = CssXmlManager.GetString("AnswerTextRender");
                    label.Text = this.Text;
                    this.Controls.Add(label);
                    
                    if (this.Mandatory)
                    {
                        Label mandatoryLabel = new Label();
                        mandatoryLabel.ToolTip = ResourceManager.GetString("MandatoryAnswerTitle");
                        mandatoryLabel.CssClass = CssXmlManager.GetString("AnswerMandatorySign");
                        this.Controls.Add(mandatoryLabel);
                        }
                }

                this.Controls.Add(new LiteralControl("<br />"));
            }


            if (this.FieldHeight > 1)
            {
                this._fieldTextBox.TextMode = TextBoxMode.MultiLine;
                this._fieldTextBox.Wrap = true;
                this._fieldTextBox.Columns = this.FieldWidth;
                this._fieldTextBox.Rows = this.FieldHeight;
            }
            else
            {
                this._fieldTextBox.MaxLength = this.FieldLength;
                this._fieldTextBox.Columns = this.FieldWidth;
            }

            this._fieldTextBox.Text = this.DefaultText;
            this.Controls.Add(this._fieldTextBox);

            this.OnAnswerPublisherCreated(new AnswerItemEventArgs(this.GetUserAnswers()));
        }

        /// <summary>
        /// Returns the field ID and 
        /// ensures the current control has children,
        /// then get the field unique ID in the control tree.
        /// Used to by parent controls to render the correct javascript
        /// </summary>
        public virtual string GetControlIdToValidate()
        {
            this.EnsureChildControls();
            return this._fieldTextBox.UniqueID;
        }

        /// <summary>
        /// Returns the textbox's text to the subscribers once
        /// there is a postback
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            this.ServerValidation();
            PostedAnswerDataCollection userAnswers = this.GetUserAnswers();
            this.OnAnswerPublished(new AnswerItemEventArgs(userAnswers));
            return userAnswers;
        }

        /// <summary>
        /// Returns the answeritem user's answers
        /// </summary>
        protected virtual PostedAnswerDataCollection GetUserAnswers()
        {
            PostedAnswerDataCollection datas = null;
            if (this._fieldTextBox.Text.Length != 0)
            {
                datas = new PostedAnswerDataCollection();
                datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._fieldTextBox.Text, AnswerTypeMode.RegExValidator | AnswerTypeMode.Mandatory | AnswerTypeMode.Publisher | AnswerTypeMode.Field));
            }
            return datas;
        }

        /// <summary>
        /// If a javascript function was provided adds it to the 
        /// current page
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            if (((this.EnableValidation && (this.JavascriptCode != null)) && ((this.JavascriptCode.Length != 0) && (this.JavascriptFunctionName != null))) && ((this.JavascriptFunctionName.Length != 0) && !this.Page.ClientScript.IsClientScriptBlockRegistered(this.JavascriptFunctionName)))
            {
                //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.JavascriptFunctionName, string.Format("<script>{0}{1}</script>", Environment.NewLine, this.JavascriptCode));
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), this.JavascriptFunctionName, string.Format("<script>{0}{1}</script>", Environment.NewLine, this.JavascriptCode));

            }
            base.OnPreRender(e);
        }

        /// <summary>
        /// Validate user answers
        /// </summary>
        protected virtual bool ServerValidation()
        {
            string str;
            bool flag = true;
            if ((this._fieldTextBox.Text.Length == 0) && this.Mandatory)
            {
                this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(ResourceManager.GetString("FieldRequiredMessage", base.LanguageCode), this.Text)));
                return false;
            }
            if (((this._fieldTextBox.Text.Length <= 0) || (this.RegExpression == null)) || ((this.RegExpression.Length <= 0) || Regex.IsMatch(this._fieldTextBox.Text, this.RegExpression)))
            {
                return flag;
            }
            if ((this.RegExpressionErrorMessage != null) && (this.RegExpressionErrorMessage.Length > 0))
            {
                //str = (ResourceManager.GetString(this.RegExpressionErrorMessage, base.LanguageCode) == null) ? this.RegExpressionErrorMessage : ResourceManager.GetString(this.RegExpressionErrorMessage, base.LanguageCode);
                str = (ResourceManager.GetString(this.RegExpressionErrorMessage, base.LanguageCode) == null) ? this.RegExpressionErrorMessage : string.Format(ResourceManager.GetString(this.RegExpressionErrorMessage, base.LanguageCode), this._fieldTextBox.Text);

            }
            else
            {
                str = string.Format(ResourceManager.GetString("RegExValidationFailedMessage", base.LanguageCode), this._fieldTextBox.Text);
            }
            this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(str, this._fieldTextBox.Text)));
            return false;
        }

        public virtual bool EnableValidation
        {
            get
            {
                return this._enableValidation;
            }
            set
            {
                this._enableValidation = value;
            }
        }

        public virtual int FieldHeight
        {
            get
            {
                return this._fieldHeight;
            }
            set
            {
                this._fieldHeight = value;
            }
        }

        public virtual int FieldLength
        {
            get
            {
                return this._fieldLength;
            }
            set
            {
                this._fieldLength = value;
            }
        }

        public virtual int FieldWidth
        {
            get
            {
                return this._fieldWidth;
            }
            set
            {
                this._fieldWidth = value;
            }
        }

        public virtual string JavascriptCode
        {
            get
            {
                return this._javascriptCode;
            }
            set
            {
                this._javascriptCode = value;
            }
        }

        public virtual string JavascriptErrorMessage
        {
            get
            {
                return this._javascriptErrorMessage;
            }
            set
            {
                this._javascriptErrorMessage = value;
            }
        }

        public virtual string JavascriptFunctionName
        {
            get
            {
                return this._javascriptFunctionName;
            }
            set
            {
                this._javascriptFunctionName = value;
            }
        }

        public virtual bool Mandatory
        {
            get
            {
                return this._mandatory;
            }
            set
            {
                this._mandatory = value;
            }
        }

        public virtual string RegExpression
        {
            get
            {
                return this._regExpression;
            }
            set
            {
                this._regExpression = value;
            }
        }

        public virtual string RegExpressionErrorMessage
        {
            get
            {
                return this._regExpressionErrorMessage;
            }
            set
            {
                this._regExpressionErrorMessage = value;
            }
        }
    }
}

