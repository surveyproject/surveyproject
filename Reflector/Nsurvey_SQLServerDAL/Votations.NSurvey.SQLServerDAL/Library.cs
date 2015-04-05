namespace Votations.NSurvey.SQLServerDAL
{
    using Microsoft.Practices.EnterpriseLibrary.Common;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

    //using Microsoft.ApplicationBlocks.Data;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// SQL Server DAL implementation.
    /// </summary>
    public class Library : ILibrary
    {
        /// <summary>
        /// Adds a new library in the database
        /// </summary>
        /// <param name="newAnswerType">Library object with information about what to add. Only Id must be ommited</param>
        public void AddLibrary(LibraryData newLibrary)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spLibraryAddNew", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@LibraryName", SqlDbType.VarChar, 0xff, "LibraryName"));
            insertCommand.Parameters.Add(new SqlParameter("@LibraryId", SqlDbType.Int, 4, "LibraryId"));
            insertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.NText, 10000, "Description"));
            insertCommand.Parameters.Add(new SqlParameter("@DefaultLanguageCode", SqlDbType.NVarChar, 50, "DefaultLanguageCode")); 
            insertCommand.Parameters["@LibraryId"].Direction = ParameterDirection.Output;
            DbConnection.db.UpdateDataSet(newLibrary, "Libraries", insertCommand, new SqlCommand(), new SqlCommand(), UpdateBehavior.Transactional);
        }

        /// <summary>
        /// Remove the library from the database
        /// </summary>
        /// <param name="libaryid">library to delete from the database</param>
        public void DeleteLibrary(int libraryId)
        {
            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@LibraryID", libraryId).SqlValue);
            }

            DbConnection.db.ExecuteNonQuery("vts_spLibraryDelete", commandParameters.ToArray());
        }

        /// <summary>
        /// Returns all available libraries
        /// </summary>
        public LibraryData GetLibraries()
        {
            LibraryData dataSet = new LibraryData();


            DbConnection.db.LoadDataSet("vts_spLibraryGetAll", dataSet, new string[] { "Libraries" });
            return dataSet;
        }

        /// <summary>
        /// Return a library object that reflects the database library
        /// </summary>
        /// <param name="answerTypeId">Id of the library you need</param>
        /// <returns>An Library data object with the current database values</returns>
        public LibraryData GetLibraryById(int libraryId)
        {
            LibraryData dataSet = new LibraryData();

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@LibraryID", libraryId).SqlValue);
            }

            DbConnection.db.LoadDataSet("vts_spLibraryGetDetails", dataSet, new string[] { "Libraries" }, commandParameters.ToArray());
            return dataSet;
        }

        /// <summary>
        /// Update the library in the database
        /// </summary>
        public void UpdateLibrary(LibraryData updatedLibrary)
        {
            SqlConnection connection = new SqlConnection(DbConnection.NewDbConnectionString);
            SqlCommand insertCommand = new SqlCommand("vts_spLibraryUpdate", connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.Add(new SqlParameter("@LibraryID", SqlDbType.Int, 4, "LibraryID"));
            insertCommand.Parameters.Add(new SqlParameter("@LibraryName", SqlDbType.NVarChar, 255, "LibraryName"));
            insertCommand.Parameters.Add(new SqlParameter("@Description", SqlDbType.NText, 10000, "Description"));
            insertCommand.Parameters.Add(new SqlParameter("@DefaultLanguageCode", SqlDbType.NVarChar, 50, "DefaultLanguageCode"));
            DbConnection.db.UpdateDataSet(updatedLibrary, "Libraries", insertCommand, new SqlCommand(), insertCommand, UpdateBehavior.Transactional);
        }
    }
}

