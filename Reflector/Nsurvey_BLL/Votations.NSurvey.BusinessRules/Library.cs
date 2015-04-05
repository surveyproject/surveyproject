namespace Votations.NSurvey.BusinessRules
{
    using System;
    using Votations.NSurvey.DALFactory;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Contains the business rules that are used for an answer type.
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Adds a new library in the database
        /// </summary>
        /// <param name="newAnswerType">Library object with information about what to add. Only Id must be ommited</param>
        public void AddLibrary(LibraryData newLibrary)
        {
            LibraryFactory.Create().AddLibrary(newLibrary);
        }

        /// <summary>
        /// Remove the library from the database
        /// </summary>
        /// <param name="libaryid">library to delete from the database</param>
        public void DeleteLibrary(int libraryId)
        {
            LibraryFactory.Create().DeleteLibrary(libraryId);
        }

        /// <summary>
        /// Update the library in the database
        /// </summary>
        public void UpdateLibrary(LibraryData updatedLibrary)
        {
            LibraryFactory.Create().UpdateLibrary(updatedLibrary);
        }
    }
}

