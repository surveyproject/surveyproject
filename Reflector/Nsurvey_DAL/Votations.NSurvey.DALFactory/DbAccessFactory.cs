namespace Votations.NSurvey.DALFactory
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Factory implementation for the SQL helper DAL object
    /// </summary>
    public class DbAccessFactory
    {
        public static IDbAccess Create()
        {
            NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            string typeName = assemblyString + ".DbAccess";
            return (IDbAccess) Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}

