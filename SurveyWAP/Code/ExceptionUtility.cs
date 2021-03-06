using System;
using System.Collections.Generic;
using Votations.NSurvey.Emailing;
using Votations.NSurvey.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;

namespace Votations.NSurvey.WebAdmin.Code
{
    public sealed class ExceptionUtility
    {
        // All methods are static, so this can be private
        private ExceptionUtility()
        { }

        // Log an Exception
        public static void LogException(Exception exc, string source)
        {

                // Include enterprise logic for logging exceptions
                // Get the absolute path to the log file
                string logFile = "~/App_Data/ErrorLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            // Open the log file for append and write the log
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.Write("Inner Exception Type: ");
                sw.WriteLine(exc.InnerException.GetType().ToString());
                sw.Write("Inner Exception: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("Inner Source: ");
                sw.WriteLine(exc.InnerException.Source);
                if (exc.InnerException.StackTrace != null)
                {
                    sw.WriteLine("Inner Stack Trace: ");
                    sw.WriteLine(exc.InnerException.StackTrace);
                }
            }
            sw.Write("Exception Type: ");
            sw.WriteLine(exc.GetType().ToString());
            sw.WriteLine("Exception Message: " + exc.Message);
            sw.WriteLine("Source/ ErrorHandler: " + source);
            sw.WriteLine("Stack Trace: ");
            if (exc.StackTrace != null)
            {
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }

            if (exc.GetType() == typeof(HttpException))
            {
                var httpException = exc as HttpException;
                int statusCode = httpException != null ? httpException.GetHttpCode() : (int)HttpStatusCode.InternalServerError;
                sw.WriteLine("StatusCode: " + statusCode);
            }            

            sw.Close();
        }


        // Log an Invalid Request on the Default.aspx Loginform
        public static void LogInvalidRequest(string request, string source)
        {
            // Include enterprise logic for logging exceptions
            // Get the absolute path to the log file
            string logFile = "~/App_Data/ErrorLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            // Open the log file for append and write the log
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);

            sw.Write("Exception Type: ");
            sw.WriteLine("System.Web.HttpRequestValidationException");
            sw.WriteLine("Exception: A potentially dangerous Request.Form value was detected from the client");
            sw.WriteLine("Source: " + source);
            sw.WriteLine("Stack Trace: dna");
            sw.WriteLine("No StackTrace");
            sw.WriteLine();

            sw.Close();
        }


        // Notify System Operators about an exception
        public static void NotifySystemOps(Exception exc)
        {
        //    // Include code for notifying IT system operators

            IEmailing emailing = EmailingFactory.Create();
            EmailingMessage mail = new EmailingMessage();
            mail.FromEmail = EmailConfig.NSurveySMTPServerAuthUserName;

            string iEstacktrace = "no Internal Stacktrace";
            string eStacktrace = "no Stacktrace";

            if (exc.InnerException != null)
            {

                if (exc.InnerException.StackTrace != null)
                    iEstacktrace = exc.InnerException.StackTrace;

                if (exc.StackTrace != null)
                    eStacktrace = exc.StackTrace;

                mail.Body = "Survey Project Warning Message: Global Page Error details."
                + "<br /><br />Inner Exception Type: " + exc.InnerException.GetType().ToString()
                + "<br /><br />Inner Exception: " + exc.InnerException.Message
                + "<br /><br />Inner Source: " + exc.InnerException.Source
                + "<br /><br />Inner Stack Trace: " + iEstacktrace
                + "<br /><br />Exception Type: " + exc.GetType().ToString()
                + "<br /><br />Exception: " + exc.Message
                + "<br /><br />Stack Trace: " + eStacktrace;
            }
            else

               if (exc.StackTrace != null)
                eStacktrace = exc.StackTrace;

            mail.Body = "Survey Project Warning Message: Global Page Error details."
                + "<br /><br />Exception Type: " + exc.GetType().ToString()
                + "<br /><br />Exception: " + exc.Message
                + "<br /><br />Source: " + exc.Source
                + "<br /><br />Stack Trace: " + eStacktrace;

            mail.Subject = "Survey Project Warning Message: Global Page Error";
            mail.ToEmail = EmailConfig.NSurveySMTPServerAuthUserName;
            emailing.SendEmail(mail);

        }

    }
}