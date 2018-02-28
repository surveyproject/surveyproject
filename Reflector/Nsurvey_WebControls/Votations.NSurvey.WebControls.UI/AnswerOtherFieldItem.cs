namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Selection - Other answertype textbox
    /// </summary>
    public class AnswerOtherFieldItem : AnswerSelectionItem, IFieldItem, IClientScriptValidator, IMandatoryAnswer, IRegExValidator
    {
        private bool _enableValidation = false;
        private int _fieldHeight = 1;
        private int _fieldLength = 1;
        private TextBox _fieldTextBox = new TextBox();
        private int _fieldWidth = 1;
        private bool _generatedSelection = false;
        private string _javascriptCode = null;
        private string _javascriptErrorMessage = null;
        private string _javascriptFunctionName = null;
        private bool _mandatory = false;
        private string _regExpression = null;
        private string _regExpressionErrorMessage = null;

        protected override void CreateChildControls()
        {
            this._generatedSelection = base.GenerateSelectionControl(this.Controls);

           // this.Controls.Add(new LiteralControl("<br />&nbsp;"));

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

            if (this.Mandatory)
            {
                //this.Controls.Add(new LiteralControl("<div style='color:red; display:inline-flex;'>&nbsp; * &nbsp;</div>"));
                Label mandatoryLabel = new Label();
                mandatoryLabel.ToolTip = ResourceManager.GetString("MandatoryAnswerTitle");
                mandatoryLabel.CssClass = CssXmlManager.GetString("AnswerMandatorySign");
                this.Controls.Add(mandatoryLabel);
            }
        }

        /// <summary>
        /// Returns the control ID and 
        /// ensures the current control has children,
        /// then get the field unique ID in the control tree.
        /// </summary>
        public string GetControlIdToValidate()
        {
            this.EnsureChildControls();
            return this._fieldTextBox.UniqueID;
        }

        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            this.EnsureChildControls();
            PostedAnswerDataCollection datas = new PostedAnswerDataCollection();
            if ((base.SelectionMode == AnswerSelectionMode.Radio) && ((SelectionRadioButton) base.SelectionControl).IsChecked())
            {
                this.ServerValidation();
                datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._fieldTextBox.Text, AnswerTypeMode.RegExValidator | AnswerTypeMode.Mandatory | AnswerTypeMode.Other));
                return datas;
            }
            if ((base.SelectionMode == AnswerSelectionMode.CheckBox) && (this.Context.Request[((SelectionCheckBox) base.SelectionControl).UniqueID] != null))
            {
                this.ServerValidation();
                datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._fieldTextBox.Text, AnswerTypeMode.RegExValidator | AnswerTypeMode.Mandatory | AnswerTypeMode.Other));
                return datas;
            }
            return null;
        }

        /// <summary>
        /// Generates the validation javascript
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (((this.EnableValidation && (this.JavascriptCode != null)) && ((this.JavascriptCode.Length != 0) && (this.JavascriptFunctionName != null))) && (this.JavascriptFunctionName.Length != 0))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.JavascriptFunctionName, string.Format("<script type=\"text/javascript\"><!--{0}{1}//--></script>", Environment.NewLine, this.JavascriptCode));
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
                str = (ResourceManager.GetString(this.RegExpressionErrorMessage, base.LanguageCode) == null) ? this.RegExpressionErrorMessage : ResourceManager.GetString(this.RegExpressionErrorMessage, base.LanguageCode);
            }
            else
            {
                str = string.Format(ResourceManager.GetString("RegExValidationFailedMessage", base.LanguageCode), this.Text);
            }
            this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(str, Text)));
            return false;
        }

        public bool EnableValidation
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

        public int FieldHeight
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

        public int FieldLength
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

        public int FieldWidth
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

        public string JavascriptCode
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

        public string JavascriptErrorMessage
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

        public string JavascriptFunctionName
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

