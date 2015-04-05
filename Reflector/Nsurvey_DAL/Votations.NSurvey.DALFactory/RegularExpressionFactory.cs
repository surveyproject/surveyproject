namespace Votations.NSurvey.DALFactory
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Factory implementaion for the regular expression DAL object
    /// </summary>
    public class RegularExpressionFactory
    {
        public static IRegularExpression Create()
        {
            NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            string typeName = assemblyString + ".RegularExpression";
            return (IRegularExpression) Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}

