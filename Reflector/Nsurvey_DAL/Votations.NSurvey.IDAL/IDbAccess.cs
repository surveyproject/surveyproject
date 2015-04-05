namespace Votations.NSurvey.IDAL
{
    using System;
    using System.Data;

    /// <summary>
    /// SQL helper interface for the voter DAL.
    /// </summary>
    public interface IDbAccess
    {
        DataSet ExecuteDataset(string commandText);
    }
}

