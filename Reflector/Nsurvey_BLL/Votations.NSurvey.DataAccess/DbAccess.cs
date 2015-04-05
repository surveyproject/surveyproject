namespace Votations.NSurvey.DataAccess
{
    using System;
    using System.Data;
    using Votations.NSurvey.DALFactory;

    /// <summary>
    /// Provides an abstraction layer to access
    /// common DB read / write features
    /// </summary>
    public class DbAccess
    {
        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        public DataSet ExecuteDataset(string commandText)
        {
            return DbAccessFactory.Create().ExecuteDataset(commandText);
        }
    }
}

