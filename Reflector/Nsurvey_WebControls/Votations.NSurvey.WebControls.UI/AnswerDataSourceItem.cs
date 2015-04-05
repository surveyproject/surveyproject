namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Base class for the answer types based on an 
    /// external data source
    /// </summary>
    public abstract class AnswerDataSourceItem : AnswerItem
    {
        private string _dataSource;

        protected AnswerDataSourceItem()
        {
        }

        /// <summary>
        /// Data source info to feed the answers
        /// </summary>
        public virtual string DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;
            }
        }
    }
}

