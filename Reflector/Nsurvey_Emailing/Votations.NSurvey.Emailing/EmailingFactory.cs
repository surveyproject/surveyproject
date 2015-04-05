namespace Votations.NSurvey.Emailing
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;

    /// <summary>
    /// Factory implementation for the emailing object
    /// </summary>
    public class EmailingFactory
    {
        /// <summary>
        /// Creates a new instance of the emailing class as 
        /// specified in the .config file
        /// </summary>
        public static IEmailing Create()
        {
            NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string typeName = config["EmailingProviderClass"];
            string assemblyString = config["EmailingProviderAssembly"];
            return (IEmailing) Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}

