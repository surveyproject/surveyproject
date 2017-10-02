using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.WebAdmin.Code;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.Errors
{
    public partial class DefaultRedirectErrorPage : System.Web.UI.Page
    {
        protected HttpException ex = null;        

        protected void Page_Load(object sender, EventArgs e)
        {
            // Log the exception and notify system operators
            //ex = new HttpException("Web.config Redirected Error");

            Exception exc = Server.GetLastError();

            ExceptionUtility.LogException(exc, "Caught in DefaultRedirectErrorPage");
            ExceptionUtility.NotifySystemOps(exc);

            // Clear the error from the server.
            Server.ClearError();

        }
    }
}