namespace Votations.NSurvey.SQLServerDAL
{
   // using Microsoft.ApplicationBlocks.Data;
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    using System;
    using System.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Sql Server DAL implementation.
    /// </summary>
    public class DbAccess : IDbAccess
    {
        public DataSet ExecuteDataset(string commandText)
        {
            return DbConnection.db.ExecuteDataSet(CommandType.Text, commandText);
        }
    }
}

