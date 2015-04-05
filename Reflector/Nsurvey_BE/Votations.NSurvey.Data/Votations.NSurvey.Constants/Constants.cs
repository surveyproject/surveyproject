using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Votations.NSurvey.Constants
{
    public class Constants
    {
        public const string EntityLibrary = "Library";
        public const string EntitySurvey = "Survey";
        public const string VoterExportHeaderSplitChar = "|";
        public const string PreviewPage = "CampaignStart.aspx";
        public const string ScrollQuestionQstr = "ScrollQuestionId";
        public const string QRYSTRGuid = "surveyid";
        public static string GetFilePathCSS(int surveyId)
        {

         string dir=
      System.Web.HttpContext.Current.Server.MapPath(UserSettingsConstants.CssStoragePath) + "\\" + surveyId.ToString() + "\\";
         if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
         return dir;
        }
    }
}
