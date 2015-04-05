namespace Votations.NSurvey.Resources
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.Caching;
    using System.Web.UI.WebControls;
    using System.Xml;
    using Votations.NSurvey;

    /// <summary>
    /// Helpfiles manager to access the survey xml Helpfiles.
    /// </summary>
    public class HelpfilesManager
    {
        /// <summary>
        /// returns the resource value
        /// </summary>
        /// <param name="name"></param>
        public static string GetString(string name)
        {
            string str = null;
            Hashtable hashtable = LoadResources("en-US");
            if (CultureInfo.CurrentUICulture.Name != "en-US")
            {
                Hashtable hashtable2 = LoadResources(CultureInfo.CurrentUICulture.Name);
                if ((hashtable2 != null) && hashtable2.ContainsKey(name))
                {
                    str = hashtable2[name].ToString();
                }
                if (str == null)
                {
                    hashtable2 = LoadResources(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
                    if ((hashtable2 != null) && hashtable2.ContainsKey(name))
                    {
                        str = hashtable2[name].ToString();
                    }
                }
            }
            if (((str == null) && (hashtable != null)) && (hashtable[name] != null))
            {
                str = hashtable[name].ToString();
            }
            return str;
        }

        /// <summary>
        /// returns the resource value for the given
        /// language code
        /// </summary>
        public static string GetString(string name, string languageCode)
        {
            string str = null;
            if ((languageCode == null) || (languageCode.Length == 0))
            {
                return GetString(name);
            }
            Hashtable hashtable = LoadResources(languageCode);
            if ((hashtable != null) && hashtable.ContainsKey(name))
            {
                str = hashtable[name].ToString();
            }
            if (str == null)
            {
                string[] strArray = languageCode.Split(new char[] { '-' });
                if (strArray.Length > 1)
                {
                    hashtable = LoadResources(strArray[0]);
                    if ((hashtable != null) && hashtable.ContainsKey(name))
                    {
                        str = hashtable[name].ToString();
                    }
                }
                if (str == null)
                {
                    str = GetString(name);
                }
            }
            return str;
        }

        /// <summary>
        /// Loads the resource from cache or from the 
        /// xml files
        /// </summary>
        public static Hashtable LoadResources(string language)
        {
            Hashtable hashtable = null;
            string path = HttpContext.Current.Server.MapPath(GlobalConfig.HelpfilesPath + language + ".xml");
            string key = "NSurvey:Help:" + language;
            if (HttpContext.Current.Cache[key] != null)
            {
                return (Hashtable) HttpContext.Current.Cache[key];
            }
            if (File.Exists(path))
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                hashtable = new Hashtable();
                foreach (XmlNode node in document["root"])
                {
                    if ((node.NodeType != XmlNodeType.Comment) && (node.ChildNodes.Count > 0))
                    {
                        if (hashtable[node.Attributes["name"].Value] == null)
                        {
                            hashtable.Add(node.Attributes["name"].Value, node.ChildNodes[0].InnerText);
                        }
                        else
                        {
                            hashtable[node.Attributes["name"].Value] = node.ChildNodes[0].InnerText;
                        }
                    }
                }
                HttpContext.Current.Cache.Insert(key, hashtable, new CacheDependency(path), DateTime.MaxValue, TimeSpan.Zero);
            }
            return hashtable;
        }

        /// <summary>
        /// Look up the given item texts in the resource file 
        /// to translate them
        /// </summary>
        /// <param name="dropDown"></param>
        public static void TranslateListControl(ListControl unTranslatedListControl)
        {
            foreach (ListItem item in unTranslatedListControl.Items)
            {
                string str = GetString(item.Text);
                item.Text = (str == null) ? item.Text : str;
            }
        }
    }
}

