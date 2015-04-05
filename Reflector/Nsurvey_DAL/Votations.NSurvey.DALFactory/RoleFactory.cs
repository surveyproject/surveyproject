namespace Votations.NSurvey.DALFactory
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Factory implementaion for the role DAL object
    /// </summary>
    public class RoleFactory
    {
        public static IRole Create()
        {
            NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            string typeName = assemblyString + ".Role";
            return (IRole) Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}

