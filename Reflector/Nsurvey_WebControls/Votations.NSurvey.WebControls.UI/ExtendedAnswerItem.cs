namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.ComponentModel;
    using System.Web.UI;

    /// <summary>
    /// base class that is required for a class that wants to be used 
    /// as an answer inside a questionitem class
    /// </summary>
    [ToolboxItem(false)]
    public abstract class ExtendedAnswerItem : AnswerItem
    {
        private AnswerProperties _properties = new AnswerProperties();

        protected ExtendedAnswerItem()
        {
        }

        /// <summary>
        /// Method must return a webcontrol allowing 
        /// the user to modify the custom properties 
        /// of the item
        /// </summary>
        public abstract Control GeneratePropertiesUI();
        /// <summary>
        /// Serialize and persists properties in the db
        /// </summary>
        protected virtual void PresistProperties()
        {
            this._properties.Serialize(this.AnswerId);
        }

        /// <summary>
        /// Loads and de-serialize and populate the extended 
        /// properties of the current answer item and cache
        /// them.
        /// </summary>
        public virtual void RestoreProperties()
        {
            this._properties = new AnswerProperties(this.AnswerId);
            if (this._properties == null)
            {
                this._properties = new AnswerProperties();
            }
        }

        public AnswerProperties Properties
        {
            get
            {
                return this._properties;
            }
            set
            {
                this._properties = value;
            }
        }
    }
}

