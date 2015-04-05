namespace Votations.NSurvey.Helpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.Caching;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Xml file manager handles the loading, caching and localisation of the 
    /// given xml file name
    /// </summary>
    public class XmlFileManager
    {
        private HttpContext _context = HttpContext.Current;
        private string _fileName;

        public XmlFileManager(string FileName)
        {
            this._fileName = FileName;
        }

        /// <summary>
        /// Returns the localized Xml file
        /// </summary>
        /// <returns></returns>
        protected virtual NSurveyDataSource GetLocalizedXml(string filePath)
        {
            if (this._context.Cache[this.FileName + CultureInfo.CurrentUICulture.TwoLetterISOLanguageName] == null)
            {
                if (!File.Exists(filePath))
                {
                    return null;
                }
                NSurveyDataSource source = new NSurveyDataSource();
                source.ReadXml(filePath);
                CacheDependency dependencies = new CacheDependency(filePath);
                this._context.Cache.Insert(this.FileName + CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, source, dependencies);
            }
            return (NSurveyDataSource) this._context.Cache[this.FileName + CultureInfo.CurrentUICulture.TwoLetterISOLanguageName];
        }

        /// <summary>
        /// Returns the default language neutral Xml file
        /// </summary>
        /// <returns></returns>
        protected virtual NSurveyDataSource GetNeutralXml(string filePath)
        {
            if (this._context.Cache[this.FileName] == null)
            {
                if (!File.Exists(filePath))
                {
                    return null;
                }
                NSurveyDataSource source = new NSurveyDataSource();
                source.ReadXml(filePath);
                CacheDependency dependencies = new CacheDependency(filePath);
                this._context.Cache.Insert(this.FileName, source, dependencies);
            }
            return (NSurveyDataSource) this._context.Cache[this.FileName];
        }

        /// <summary>
        /// Loads and return the Xml information either
        /// from cache or from the file specified by
        /// the Xml filename
        /// </summary>
        public virtual NSurveyDataSource GetXmlAnswers(string languageCode)
        {
            if ((this.FileName == null) || (this.FileName.Length <= 0))
            {
                return new NSurveyDataSource();
            }
            NSurveyDataSource localizedXml = null;
            string filePath = this._context.Server.MapPath(GlobalConfig.XmlDataSourcePath + this.FileName);
            string str2 = null;
            if ((languageCode != null) && (languageCode.Length > 0))
            {
                string[] strArray = languageCode.Split(new char[] { '-' });
                if (strArray.Length > 1)
                {
                    str2 = strArray[0];
                    localizedXml = this.GetLocalizedXml(filePath.Replace(".xml", "." + str2 + ".xml"));
                }
            }
            if (str2 == null)
            {
                string str3 = filePath.Replace(".xml", "." + CultureInfo.CurrentUICulture.TwoLetterISOLanguageName + ".xml");
                if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName != CultureInfo.InstalledUICulture.TwoLetterISOLanguageName)
                {
                    localizedXml = this.GetLocalizedXml(str3);
                }
            }
            if (localizedXml == null)
            {
                return this.GetNeutralXml(filePath);
            }
            return localizedXml;
        }

        public string FileName
        {
            get
            {
                return this._fileName;
            }
            set
            {
                this._fileName = value;
            }
        }
    }
}

