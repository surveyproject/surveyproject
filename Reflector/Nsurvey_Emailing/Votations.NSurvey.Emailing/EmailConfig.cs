namespace Votations.NSurvey.Emailing
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    /// <summary>
    /// Email configuration settings
    /// </summary>
    public class EmailConfig
    {
        
        /// <summary>
        /// CA1053: Static holder types should not have constructors
        /// </summary>
        private void NoInstancesNeeded() {}

        /// <summary>
        /// Authentication user name needed to send mails on
        /// the SMTP Server
        /// </summary>
        public static string NSurveySMTPServerAuthPassword
        {
            get
            {
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                return config["NSurveySMTPServerAuthPassword"];
            }
        }

        /// <summary>
        /// Authentication user name needed to send mails on
        /// the SMTP Server
        /// </summary>
        public static string NSurveySMTPServerAuthUserName
        {
            get
            {
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                return config["NSurveySMTPServerAuthUserName"];
            }
        }

        /// <summary>
        /// SMTP Server used to send emails out
        /// </summary>
        public static string SmtpServer
        {
            get
            {
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                return config["NSurveySMTPServer"];
            }
        }

        /// <summary>
        /// Port of SMTP Server used to send emails out
        /// </summary>
        public static string SmtpServerPort
        {
            get
            {
                NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                return config["NSurveySMTPServerPort"];
            }
        }

        /// <summary>
        /// Enable SSL for SMTP server
        /// </summary>
        public static string SmtpServerEnableSSL
        {
            get
            {
                NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
                if (config == null)
                {
                    config = ConfigurationManager.AppSettings;
                }
                return config["NSurveySMTPServerEnableSsl"];
            }
        }

    }
}

