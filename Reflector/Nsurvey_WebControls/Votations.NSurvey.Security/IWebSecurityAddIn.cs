namespace Votations.NSurvey.Security
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Security AddIns that are made available to the user 
    /// for survey access security purposes must implement
    /// this interface
    /// </summary>
    public interface IWebSecurityAddIn
    {
        event UserAuthenticatedEventHandler UserAuthenticated;

        /// <summary>
        /// Can return keys/values of the custom 
        /// stored data during the ProcessVoterData. 
        /// At this time these data are retrieved to
        /// be shown in individual reports and for results export
        /// </summary>
        NameValueCollection GetAddInVoterData(int voterId);
        /// <summary>
        /// Must create and return the control 
        /// that will show the administration interface
        /// If none is available returns null
        /// </summary>
        Control GetAdministrationInterface(Style controlStyle);
        /// <summary>
        /// In case the user has not been authenticated you can 
        /// create and return a control 
        /// that will show the logon interface.
        /// If none is available returns null
        /// </summary>
        Control GetLoginInterface(Style controlStyle);
        /// <summary>
        /// Method called once an addin has been added to a survey
        /// Can be used to set default values, settings for the addin
        /// </summary>
        void InitOnSurveyAddition();
        /// <summary>
        /// Check if the current user has already
        /// the correct credentials
        /// </summary>
        bool IsAuthenticated();
        /// <summary>
        /// Method to handle voter data once it has been stored in the database
        /// </summary>
        /// <param name="voter">Voter information as saved in the database and its answers</param>
        void ProcessVoterData(VoterAnswersData voter);
        /// <summary>
        /// Method called once an addin has been removed from a survey
        /// Can be used to remove useless settings for the addin
        /// </summary>
        void UnInitOnSurveyRemoval();

        int AddInDbId { get; set; }

        string Description { get; set; }

        bool Disabled { get; set; }

        /// <summary>
        /// Contains the current user language
        /// choice in a multi-language survey
        /// </summary>
        string LanguageCode { get; set; }

        int Order { get; set; }

        int SurveyId { get; set; }

        /// <summary>
        /// Must be set to a valid Viewstate, 
        /// allows Addins to store data between postbacks
        /// until the addin unload is called again
        /// </summary>
        StateBag ViewState { get; set; }
    }
}

