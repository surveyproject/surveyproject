namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.WebControlsFactories;

    /// <summary>
    /// Abstract class required to get 
    /// a single question's answer section behavior
    /// </summary>
    public abstract class AnswerSection : Section
    {
        private AnswerConnectionsCollection _answerConnections = new AnswerConnectionsCollection();
        private AnswerItemCollection _answers = new AnswerItemCollection();
        private Style _answerStyle;
        private int _columnsNumber = 1;
        private string _languageCode;
        private QuestionLayoutMode _questionLayoutMode;

        protected AnswerSection()
        {
        }

        protected override void CreateChildControls()
        {
            SubscriberItemFactory.ActivateAnswerConnections(this._answerConnections, this._answers);
            this.Controls.Add(this.GenerateSection());
        }

        /// <summary>
        /// Parse the answer collection and generates 
        /// the question client side validation script for the answers / fields
        /// of sections that require it.
        /// </summary>
        protected override string GenerateClientSideValidationCode()
        {
            if (base.EnableClientSideValidation)
            {
                bool flag = false;
                StringBuilder builder = new StringBuilder();
                builder.Append(string.Format("<script type=\"text/javascript\" language=\"javascript\"><!--{0}function {1}{2}(){{/*alert('start validation');*/", Environment.NewLine, GlobalConfig.QuestionValidationFunction, this.UniqueID.Replace(":", "_")));
                foreach (AnswerItem item in this.Answers)
                {
                    if (item is IClientScriptValidator)
                    {
                        IClientScriptValidator validator = (IClientScriptValidator) item;
                        if ((validator.EnableValidation && (validator.JavascriptFunctionName != null)) && (validator.JavascriptFunctionName.Length != 0))
                        {
                            builder.Append(string.Format("if ((document.getElementsByName('{1}').item(0) != null  && !{0}(document.getElementsByName('{1}').item(0))) || (document.getElementById('{1}') != null  && !{0}(document.getElementById('{1}'))) ){{alert('{2} : {3}');return false;}}", new object[] { validator.JavascriptFunctionName, validator.GetControlIdToValidate(), item.Text, validator.JavascriptErrorMessage }));
                            flag = true;
                        }
                    }
                }
                builder.Append("return true;}//--></script>");
                if (((this.Page != null) && flag) && (builder != null))
                {
                    return builder.ToString();
                }
            }
            return null;
        }

        /// <summary>
        /// Does the section generates any client side validation script ?
        /// </summary>
        /// <returns></returns>
        public override bool GeneratesClientSideScript()
        {
            if (base.EnableClientSideValidation)
            {
                foreach (AnswerItem item in this.Answers)
                {
                    if (item is IClientScriptValidator)
                    {
                        IClientScriptValidator validator = (IClientScriptValidator) item;
                        if ((validator.EnableValidation && (validator.JavascriptFunctionName != null)) && (validator.JavascriptFunctionName.Length != 0))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Must returns a control (table, placeholder ...) that
        /// represents the section to be insered in the question
        /// </summary>
        protected abstract Control GenerateSection();

        /// <summary>
        /// List of answer publisher that have to be linked
        /// to their subscribers
        /// </summary>
        public AnswerConnectionsCollection AnswerConnections
        {
            get
            {
                return this._answerConnections;
            }
            set
            {
                this._answerConnections = value;
            }
        }

        /// <summary>
        /// Answer items collection that will be used
        /// in this section
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

        /// <summary>
        /// Style used for the answers
        /// </summary>
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
        /// How many columns of answers will be rendered 
        /// </summary>
        public int ColumnsNumber
        {
            get
            {
                return this._columnsNumber;
            }
            set
            {
                this._columnsNumber = value;
            }
        }

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
        /// How will the answers of the control be rendered 
        /// </summary>
        public QuestionLayoutMode LayoutMode
        {
            get
            {
                return this._questionLayoutMode;
            }
            set
            {
                this._questionLayoutMode = value;
            }
        }
    }
}

