using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Votations.NSurvey.WebAdmin.Code;


namespace Votations.NSurvey.WebAdmin
{
    public partial class HttpErrorPage : System.Web.UI.Page
    {

        protected HttpException exc = null;


        protected void Page_Load(object sender, EventArgs e)
        {

            // Create safe error messages.
            string generalErrorMsg =    "A problem has occurred on this web site. Please try again. " +
                                        "If this error continues, please contact support.";
            string httpErrorMsg =       "An HTTP error occurred. Page Not found. Please try again.";
            string unhandledErrorMsg = "The error was unhandled by application code.";

            // Display safe error message.
            FriendlyErrorMsg.Text = generalErrorMsg;


            // Determine where error was handled.
            string errorHandler = Request.QueryString["handler"];
            if (errorHandler == null)
            {
                errorHandler = "Http Error Page";
            }

            // Get the last error from the server.
            Exception ex = Server.GetLastError();

            // Get the error number passed as a querystring value.
            string errorMsg = Request.QueryString["msg"];
            if (errorMsg == "404")
            {
                exc = new HttpException(404, httpErrorMsg, ex);
                FriendlyErrorMsg.Text = exc.Message;
            }

            // If the exception no longer exists, create a generic exception.
            if (exc == null)
            {
                exc = new HttpException(unhandledErrorMsg);
            }

            // Show error details to only you (developer). LOCAL ACCESS ONLY.
            if (Request.IsLocal)
            {
                // Detailed Error Message.
                ErrorDetailedMsg.Text = exc.Message;

                // Show where the error was handled.
                ErrorHandler.Text = errorHandler;

                // Show local access details.
                DetailedErrorPanel.Visible = true;

                if (exc.InnerException != null)
                {
                    InnerMessage.Text = exc.GetType().ToString() + "<br/>" +
                        exc.InnerException.Message;
                    InnerTrace.Text = exc.InnerException.StackTrace;
                }
                else
                {
                    InnerMessage.Text = exc.GetType().ToString();
                    if (exc.StackTrace != null)
                    {
                        InnerTrace.Text = exc.StackTrace.ToString().TrimStart();
                    }
                }
            }

            // Log the exception.
            ExceptionUtility.LogException(exc, errorHandler);
            ExceptionUtility.NotifySystemOps(exc);

            // Clear the error from the server.
            Server.ClearError();


        }
    }
}