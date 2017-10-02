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

        protected HttpException ex = null;


        protected void Page_Load(object sender, EventArgs e)
        {

                ex = (HttpException)Server.GetLastError();
                int httpCode = ex.GetHttpCode();

                // Filter for Error Codes and set text
                if (httpCode >= 400 && httpCode < 500)
                    ex = new HttpException
                        (httpCode, "A HTTP Error occured in the 4xx HTTP code range: Client Error.", ex);
                else if (httpCode > 499)
                    ex = new HttpException
                        (ex.ErrorCode, "A HTTP Error occured in the 5xx HTTP code range: Internal Server Error.", ex);
                else
                    ex = new HttpException
                        (httpCode, "A HTTP Error occured with an unexpected HTTP code.", ex);


            // Determine where error was handled.
            string errorHandler = Request.QueryString["handler"];
            if (errorHandler == null)
            {
                errorHandler = "Error Page";
            }

            // Show where the error was handled.
                ErrorHandler.Text = errorHandler;

            // Fill the page fields
                exMessage.Text = ex.Message;
                exTrace.Text = ex.StackTrace;

                // Show Inner Exception fields for local access
                if (ex.InnerException != null)
                {
                    innerTrace.Text = ex.InnerException.StackTrace;
                    InnerErrorPanel.Visible = Request.IsLocal;
                    innerMessage.Text = string.Format("HTTP [{0}] CODE: {1}", httpCode, ex.InnerException.Message);
                }
                // Show Trace for local access
                exTrace.Visible = Request.IsLocal;

            // Log the exception and notify system operators
            ExceptionUtility.LogException(ex, errorHandler + " / HttpCode: " + httpCode);
            ExceptionUtility.NotifySystemOps(ex);

            // Clear the error from the server
            Server.ClearError();

        }
    }
}