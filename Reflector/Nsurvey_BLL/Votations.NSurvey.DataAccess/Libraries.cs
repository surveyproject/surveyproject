namespace Votations.NSurvey.DataAccess
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the methods to access answer types database data.
    /// </summary>
    public class Libraries
    {
        /// <summary>
        /// Returns all available libraries
        /// </summary>
        public LibraryData GetLibraries()
        {
            return LibraryFactory.Create().GetLibraries();
        }

        /// <summary>
        /// Return a library object that reflects the database library
        /// </summary>
        /// <param name="answerTypeId">Id of the library you need</param>
        /// <returns>An Library data object with the current database values</returns>
        public LibraryData GetLibraryById(int libraryId)
        {
            return LibraryFactory.Create().GetLibraryById(libraryId);
        }
    }
}

