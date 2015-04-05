namespace Votations.NSurvey.DALFactory
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;
    using Votations.NSurvey.IDAL;

    /// <summary>
    /// Factory implementation for the answer DAL object
    /// </summary>
    public class AnswerTypeFactory
    {
        public static IAnswerType Create()
        {
            NameValueCollection config = (NameValueCollection) ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            string typeName = assemblyString + ".AnswerType";
            return (IAnswerType) Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}

