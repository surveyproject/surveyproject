namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using NSurvey.DataAccess;
    using NSurvey.Data;
    /// <summary>
    /// Abstract class required to get 
    /// a question's section behavior
    /// </summary>
    [ToolboxItem(false)]
    public abstract class Section : Control, INamingContainer
    {
        private bool _enableClientSideValidation = false;
        private int _questionId;
        private int _sectionNumber = 0;
        private int _sectionUid = -1;

        public event ClientScriptGeneratedEventHandler ClientScriptGenerated;

        protected Section()
        {
        }

        /// <summary>
        /// Each section must generates 
        /// its own client side validation script for the answers / fields
        /// that requires it, returns null if section doesnt have any validation.
        /// </summary>
        protected abstract string GenerateClientSideValidationCode();
        /// <summary>
        /// Does the section generates any client side validation script ?
        /// </summary>
        /// <returns></returns>
        public abstract bool GeneratesClientSideScript();
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string str = this.GenerateClientSideValidationCode();
            if (str != null)
            {
                QuestionItemClientScriptEventArgs args = new QuestionItemClientScriptEventArgs();
                args.ClientScript = str;
                args.Section = this;
                this.OnClientScriptGeneration(args);
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"__ValidateQuestion" + this.UniqueID, args.ClientScript);
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

        public int QuestionId
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
        /// Section index in the section collection
        /// </summary>
        public int SectionNumber
        {
            get
            {
                return this._sectionNumber;
            }
            set
            {
                this._sectionNumber = value;
            }
        }

        /// <summary>
        /// Unique id to identify the webcontrol in the tree
        /// section number could not be used because we one 
        /// section would be removed we re-order the section 
        /// number and viewstate would be lost
        /// </summary>
        public int SectionUid
        {
            get
            {
                return this._sectionUid;
            }
            set
            {
                this._sectionUid = value;
            }
        }

        public  Unit GetCellWidth(int columnsNumber)
        {
            return new Unit( Math.Round(columnsNumber==0?0.0: 98.00/ columnsNumber,2), UnitType.Percentage);
        }
    }
}

