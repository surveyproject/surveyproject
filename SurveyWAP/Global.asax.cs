using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Votations.NSurvey.WebAdmin.Code;


namespace SurveyWAP
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                 return;                

                Server.Transfer("HttpErrorPage.aspx");
            }

            // For other kinds of errors give the user some information
            // but stay on the default page

            Response.Write("<div style='font-family:Tahoma; font-size:small; width:1020px; background-color: #e2e2e2; padding:35px; -webkit-border-radius: 7px; -moz-border-radius: 7px; border-radius: 7px;'> <div class='topCell' style='left:-4px;  top:-3px; position: relative; padding:0px 13px 2px 0px; border: 0px;  border-top-style: none; border-left-style: none; border-bottom-style: none; border-right-style: none; border-color: #ffffff;'> <a href='" + 
                HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/default.aspx' title='Survey&#8482; Project Homepage' target='_self'> <img src='" + 
                HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/Images/logotest.jpg' alt='logo' border='0' /> </a> </div>  <br /><br /> ");

            Response.Write("<h2>Survey™ Project Global Page Error</h2>\n");
            Response.Write("An application error has been generated. More details on the error can be found in the logfiles directory of the Survey™ Project application. An automated warning message has been sent to the default SMTP server account.\n");
            Response.Write( "<p>" + exc.Message + "\n" + exc.InnerException.Message +  "</p>\n");
            Response.Write("Please return to the <a href='" + HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/default.aspx'>Default Page</a>\n");

            Response.Write("<br /><br /> <hr style='color:#e2e2e2;'/><br /><br />&copy; Fryslan Webservices&trade; 2012 </div> ");

            // Log the exception and notify system operators
            ExceptionUtility.LogException(exc, "DefaultPage");
            ExceptionUtility.NotifySystemOps(exc);

            // Clear the error from the server
            Server.ClearError();


        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

            // Session Variable to demo the working of the default answer option @@sessionvariablename@@
            HttpContext.Current.Session["SpDemoSessionVariable"] = "This Session Variable was set in Global.asax.cs";

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
