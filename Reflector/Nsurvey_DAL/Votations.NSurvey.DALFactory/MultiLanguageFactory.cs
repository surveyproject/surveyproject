namespace Votations.NSurvey.DALFactory
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Factory implementaion for the mutli language DAL object
    /// </summary>
    public class MultiLanguageFactory
    {
        public static IMultiLanguage Create()
        {
            NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            string typeName = assemblyString + ".MultiLanguage";
            return (IMultiLanguage) Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}

