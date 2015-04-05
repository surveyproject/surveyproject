namespace Votations.NSurvey.IDAL
{
    using System;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Interface for the answer DAL.
    /// </summary>
    public interface ILibrary
    {
        void AddLibrary(LibraryData newLibrary);
        void DeleteLibrary(int libraryId);
        LibraryData GetLibraries();
        LibraryData GetLibraryById(int libraryId);
        void UpdateLibrary(LibraryData updatedLibrary);
    }
}

