using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using Votations.NSurvey.IDAL;

namespace Votations.NSurvey.DALFactory
{
    public class ReportFactory
    {
        public static IReport Create()
        {
            NameValueCollection config = (NameValueCollection)ConfigurationManager.GetSection("nSurveySettings");
            if (config == null)
            {
                config = ConfigurationManager.AppSettings;
            }
            string assemblyString = config["WebDAL"];
            //string typeName = assemblyString + ".Report";
            // typename: must match namespace; after namechange of assembly, namespace no longer similar to 
            string typeName = "Votations.NSurvey.SQLServerDAL.Report";

            return (IReport)Assembly.Load(assemblyString).CreateInstance(typeName);
        }
    }
}
