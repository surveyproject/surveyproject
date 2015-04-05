namespace Votations.NSurvey.UserProvider
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;

    /// <summary>
    /// Factory implementation for the Userprovider object
    /// </summary>
    public class UserFactory
    {
        /// <summary>
        /// Creates a new instance of the Userprovider class as 
        /// specified in the .config file
        /// </summary>
        public static IUserProvider Create()
        {
            NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string typeName = config["UserProviderClass"];
            string assemblyString = config["UserProviderAssembly"];
            return (IUserProvider) Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}

