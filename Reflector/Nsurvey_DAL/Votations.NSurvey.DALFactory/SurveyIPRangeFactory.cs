namespace Votations.NSurvey.DALFactory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Votations.NSurvey.IDAL;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;

    public class SurveyIPRangeFactory
    {
        public static ISurveyIPRange Create()
        {
            NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            string typeName = assemblyString + ".SurveyIPRange";
            return (ISurveyIPRange)Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}
