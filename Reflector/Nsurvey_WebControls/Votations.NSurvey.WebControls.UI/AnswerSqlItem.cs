namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Data;
    using Votations.NSurvey.DataAccess;

    /// <summary>
    /// Base class for the Sql based answer types
    /// </summary>
    public abstract class AnswerSqlItem : AnswerDataSourceItem
    {
        protected AnswerSqlItem()
        {
        }

        protected override void CreateChildControls()
        {
            this.GenerateSqlControl(this.GetSqlRequestDataSet());
        }

        protected abstract void GenerateSqlControl(DataSet sqlResults);
        /// <summary>
        /// executes the statement and fill a dataset with
        /// the result of the statement
        /// </summary>
        public virtual DataSet GetSqlRequestDataSet()
        {
            if (this.DataSource == null)
            {
                return new DataSet();
            }
            try
            {
                return new DbAccess().ExecuteDataset(this.DataSource);
            }
            catch (SystemException)
            {
                return new DataSet();
            }
        }
    }
}

