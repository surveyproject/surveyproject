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
            // https://msdn.microsoft.com/en-us/library/24395wz3.aspx
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                //NOTE: code redirect per httpcode
                //HttpException ex = (HttpException)Server.GetLastError();
                //if(ex.GetHttpCode() == 404)
                //{
                //    Server.Transfer("~/Errors/ErrorPage.aspx?msg=404&amp;handler=Application_Error%20-HttpError-%20Global.asax", true);
                //}

                Server.Transfer("~/Errors/HttpErrorPage.aspx?handler=Application_Error%20-HttpError-%20Global.asax", true);
            }
            
            //Invalid or illegal Login attempt:
            if (exc is HttpRequestValidationException)
            {
                Server.Transfer("~/Errors/ErrorPage.aspx?handler=Application_Error%20HttpRequestValidationException%20-%20Global.asax", true);

                Response.Clear();
                Response.StatusCode = 200;
                Response.End();
            }
            

            // Handle Invalid Operation exceptions
            if (exc.GetBaseException() is InvalidOperationException)
            {

                Server.Transfer("~/Errors/ErrorPage.aspx?handler=Application_Error%20InvalidOperationException%20-%20Global.asax", true);

            }

            // Handle AoR exceptions
            if (exc.GetBaseException() is ArgumentOutOfRangeException)
            {
                Server.Transfer("~/Errors/ErrorPage.aspx?handler=Application_Error%20AoRException%20-%20Global.asax", true);
            }

            // For all other kinds of errors
            Server.Transfer("~/Errors/ErrorPage.aspx?handler=Application_Error%20Generic%20-%20Global.asax", true);

            // Determine where error was handled.
            string errorHandler = Request.QueryString["handler"];

            if (errorHandler == null)
            {
                errorHandler = "Application_Error UNKNOWN Global.asax";
            }

            // Log the exception and notify system operators
            ExceptionUtility.LogException(exc, errorHandler);
            ExceptionUtility.NotifySystemOps(exc);

           // Clear the error from the server
            Server.ClearError();
            

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

            // Session Variable to demo the working of the default answer option @@sessionvariablename@@
            HttpContext.Current.Session["SpDemoSessionVariable"] = "This Session Variable was set in Global.asax.cs for demonstration purposes only.";

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
