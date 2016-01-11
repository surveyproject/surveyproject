using System;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using System.Web;
using System.Web.Caching;
using System.Configuration;
using Apprenda.SaaSGrid;

namespace Votations.NSurvey.SQLServerDAL
{
    public class DbConnection
    {

        /// <summary>
        /// Create Database and connect: see web.config file dataConfiguration defaultDatabase=
        /// </summary>     

        public static SpSqlDatabase db 
        {
            get { return new SpSqlDatabase(NewDbConnectionString); }
        }
          
        
        public static string NewDbConnectionString
        {

            get
            {
                try
                {
                    return TenantContext.Current.ConnectionString;
                }
                catch 
                {
                    if (HttpContext.Current == null)
                    {

                        // return System.Configuration.ConfigurationManager.ConnectionStrings[""].ConnectionString;     

                        DatabaseSettings dbSettings = (DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");
                        return ConfigurationManager.ConnectionStrings[dbSettings.DefaultDatabase].ConnectionString;

                    }

                    Cache cache = HttpContext.Current.Cache;

                    if (cache["NSurvey.NewDbConnectionString"] == null)
                    {

                        DatabaseSettings dbSettings = (DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");
                        String values2 = System.Configuration.ConfigurationManager.ConnectionStrings[dbSettings.DefaultDatabase].ConnectionString;

                        cache.Insert("NSurvey.NewDbConnectionString", values2);
                    }

                    return cache["NSurvey.NewDbConnectionString"].ToString();
                }
            }

        }
    }
}
