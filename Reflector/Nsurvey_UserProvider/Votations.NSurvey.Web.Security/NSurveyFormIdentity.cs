namespace Votations.NSurvey.Web.Security
{
    using System;

    /// <summary>
    /// NSurveyIdentity abstraction of a nsurvey web administration's user
    /// </summary>
    public class NSurveyFormIdentity : INSurveyIdentity
    {
        private string _email;
        private string _firstName;
        private bool _hasAllSurveyAccess = false;
        private bool _isAdmin = false;
        private bool _isAuthenticated = false;
        private string _lastName;
        private string _name;
        private int _userId;
        private string _surveyHeaderText;
        private string _surveyFooterText;
        private string _surveyCss;
        private string _surveyImg;

        public NSurveyFormIdentity(string name, int userId, string firstName, string lastName, 
            string email, bool isAdmin, bool hasAllSurveyAccess, bool isAuthenticated
        /*    string surveyHeaderText, string surveyFooterText, string surveyCss, string surveyImg*/
            )
        {
            this._name = name;
            this._userId = userId;
            this._firstName = firstName;
            this._lastName = lastName;
            this._email = email;
            this._isAdmin = isAdmin;
            this._hasAllSurveyAccess = hasAllSurveyAccess;
            
            this._isAuthenticated = isAuthenticated;
            /*
            this._surveyHeaderText= surveyHeaderText;
            this._surveyFooterText=surveyFooterText;
            this._surveyCss=surveyCss;
            this._surveyImg=surveyImg;
             */
        }

        public string AuthenticationType
        {
            get
            {
                return "NSurveyFormAuthentication";
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
        }

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
        }

        public bool HasAllSurveyAccess
        {
            get
            {
                return this._hasAllSurveyAccess;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this._isAdmin;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return this._isAuthenticated;
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
        }

        /// <summary>
        /// Login name 
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        /// <summary>
        /// Id of the user in the database
        /// </summary>
        public int UserId
        {
            get
            {
                return this._userId;
            }
        }
        /* JJ Moved Survey Details into SurveyLayout table */

        public string SurveyHeaderText{get{return _surveyHeaderText;}}
        public string SurveyFooterText{get{return _surveyFooterText;}}
        public string SurveyCss{get{return _surveyCss;}}
        public string SurveyImg{get{return _surveyImg;}}
    }
}

