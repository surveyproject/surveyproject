namespace Votations.NSurvey.DALFactory
{
    using System;
    using System.Collections.Generic;
    using Votations.NSurvey.IDAL;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Reflection;

    public class QuestionGroupFactory
    {
        public static IQuestionGroup Create()
        {
            NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            string typeName = assemblyString + ".QuestionGroups";
            return (IQuestionGroup)Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}
