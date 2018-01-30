using System;
using System.Web;
using Votations.NSurvey.WebAdmin.Code;

namespace SurveyWAP
{
    /// <summary>
    /// Namespace referrering to entire SurveyWAP webapplication project. Used only by the global.asax code to run application level events.
    /// Global Class: code for responding to application-level events raised by ASP.NET or by HttpModules
    /// More info: https://msdn.microsoft.com/en-us/library/1xaas8a2(v=vs.71).aspx
    /// </summary>
    /// <remarks>Includes Errorhandling references; demo servervariable; removing info from httpheader</remarks>
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            //Load Sqlserver Spacial Data Types DLL's - see the ssrs reports codebehind
            //SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        /// <summary>
        /// Errorhandling instructions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Used to remove the (web) Server details from the Http header - for security reasons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;
            if (app != null && app.Context != null)
            {
                app.Context.Response.Headers.Remove("Server");
            }
        }

        /// <summary>
        /// Used for LDAP (AD) authentication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void Application_AuthenticateRequest(Object sender, EventArgs e)
        //{
        //    String cookieName = FormsAuthentication.FormsCookieName;
        //    HttpCookie authCookie = Context.Request.Cookies[cookieName];

        //    if (null == authCookie)
        //    {//There is no authentication cookie.
        //        return;
        //    }

        //    FormsAuthenticationTicket authTicket = null;

        //    try
        //    {
        //        authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //    }
        //    catch (Exception exc)
        //    {
        //        ExceptionUtility.LogException(exc, "Failed AD Authentication");
        //        return;
        //    }

        //    if (null == authTicket)
        //    {//Cookie failed to decrypt.
        //        return;
        //    }

        //    //When the ticket was created, the UserData property was assigned a
        //    //pipe-delimited string of group names.
        //    String[] groups = authTicket.UserData.Split(new char[] { '|' });

        //    //Create an Identity.
        //    GenericIdentity id = new GenericIdentity(authTicket.Name, "LdapAuthentication");

        //    //This principal flows throughout the request.
        //    GenericPrincipal principal = new GenericPrincipal(id, groups);

        //    Context.User = principal;

        //}


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
