namespace Votations.NSurvey
{
    using System;
    using System.Reflection;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics;
    using System.Windows.Forms;

    /// <summary>
    /// Provides an abstraction layer for all your web.config or machine.config
    /// settings
    /// </summary>
    public class GlobalConfig
    {
        public static readonly string AnswerItemName = "_ai";
        public static readonly string AnswerSectionName = "_as";
        public static readonly string AnswerSectionsName = "_ass";
        public static readonly string DeleteFileButtonName = "_dlfb";
        public static readonly string GroupName = "_grp";
        private static int majorVersion = 2;
        private static int minorVersion = 5;
        public static readonly string QuestionValidationFunction = "__ValidateQuestion";
        public static readonly string SurveyValidationFunction = "__ValidateSurvey";
        private static string version = "rtm";

        //TODO SP25
        //private static string versionSP = Application.ProductVersion;
        //private static string productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;


        /// <summary>
        /// Path of where you have copied your images
        /// that was specified in your machine.config or web.config file
        /// </summary>
        public static string ImagesPath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyImagesPath"];
                if (str == null)
                {
                    return "~/Images/";
                }
                return str;
            }
        }

        /// <summary>
        /// Path of the Xml languages files
        /// </summary>
        public static string LanguagesPath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyLanguagesPath"];
                if (str == null)
                {
                    return "/XmlData/Languages/";
                }
                return str;
            }
        }

        /// <summary>
        /// How many hours can uploaded files for sessions that has no 
        /// yet been resumed stay in the database. Leave some time
        /// because user wont get notified if its session have been deleted
        /// </summary>
        public static int SessionUploadedFileDeleteTimeOut
        {
            get
            {
                int num = 0x150;
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                string s = config["SessionUploadedFileDeleteTimeOut"];
                if (s != null)
                {
                    num = int.Parse(s);
                }
                return num;
            }
        }

        /// <summary>
        /// Are Sql based queries answer types allow
        /// </summary>
        public static bool SqlBasedAnswerTypesAllowed
        {
            get
            {
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                string str = config["SqlBasedAnswerTypesAllowed"];
                if (str == null)
                {
                    return false;
                }
                return bool.Parse(str);
            }
        }

        /// <summary>
        /// How many hours can unvalidated uploaded files stay in the database
        /// </summary>
        public static int UploadedFileDeleteTimeOut
        {
            get
            {
                int num = 0x18;
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                string s = config["UploadedFileDeleteTimeOut"];
                if (s != null)
                {
                    num = int.Parse(s);
                }
                return num;
            }
        }

        /// <summary>
        /// Current version of the tool
        /// </summary>
        public static string Version
        {
            get
            {
                return ("Survey Project v." + majorVersion.ToString() + "." + minorVersion.ToString() + " " + version);
            }
        }

        /// <summary>
        /// Path of the Xml datasources
        /// options set in a. web.config file; b. xml webdirectory
        /// </summary>
        public static string XmlDataSourcePath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyXmlDataPath"];
                if (str == null)
                {
                    return "/XmlData/";
                }
                return str;
            }
        }

        /// <summary>
        /// Path of the Xml Help files
        /// options set in a. web.config file; b. xml webdirectory
        /// </summary>
        public static string HelpfilesPath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyHelpfilesPath"];
                if (str == null)
                {
                    return "/XmlData/Helpfiles/";
                }
                return str;
            }
        }

        /// <summary>
        /// Path of the CSS Xml files
        /// options set in a. web.config file; b. xml webdirectory
        /// </summary>
        public static string CssXmlFilePath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyCssXmlPath"];
                if (str == null)
                {
                    return "/XmlData/Css/";
                }
                return str;
            }
        }


        /// <summary>
        /// Path of the Google Address Xml files
        /// options set in a. web.config file; b. xml webdirectory
        /// </summary>
        public static string AddressFilePath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyFormAddressPath"];
                if (str == null)
                {
                    return "/XmlData/Address/";
                }
                return str;
            }
        }

        /// <summary>
        /// Path of the Survey Form CSS files
        /// options set in a. web.config file
        /// </summary>
        public static string CSSFilesPath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyFormCSSFilesPath"];
                if (str == null)
                {
                    return "/Content/surveyform/";
                }
                return str;
            }
        }


        /// <summary>
        /// Path of the Survey Form CSS files
        /// options set in a. web.config file
        /// </summary>
        public static string ScriptFilesPath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyScriptFilesPath"];
                if (str == null)
                {
                    return "/Scripts/";
                }
                return str;
            }
        }

        public static string ContentFilesPath
        {
            get
            {
                string str = null;
                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                str = config["NSurveyContentFilesPath"];
                if (str == null)
                {
                    return "/Content/";
                }
                return str;
            }
        }

    }
}

