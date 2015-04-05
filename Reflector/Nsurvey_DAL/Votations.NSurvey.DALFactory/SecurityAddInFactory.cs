namespace Votations.NSurvey.DALFactory
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Factory implementaion for the web security Addins DAL object
    /// </summary>
    public class SecurityAddInFactory
    {
        public static ISecurityAddIn Create()
        {
            NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            string typeName = assemblyString + ".SecurityAddIn";
            return (ISecurityAddIn) Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}

