namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Web;
    using System.Web.Caching;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.DataAccess;

    public class AnswerProperties : IEnumerable
    {
        private Hashtable _properties;
        private Cache _webCache;

        /// <summary>
        /// Creates an empty property collection
        /// </summary>
        public AnswerProperties()
        {
            this._webCache = HttpContext.Current.Cache;
            this._properties = new Hashtable();
            this._webCache = HttpContext.Current.Cache;
        }

        /// <summary>
        /// Restore collection for the given answer id
        /// </summary>
        /// <param name="answerId"></param>
        public AnswerProperties(int answerId)
        {
            this._webCache = HttpContext.Current.Cache;
            object obj2 = null;
            if ((this._webCache != null) && (this._webCache["nsurvey:answ" + answerId + ":properties"] == null))
            {
                obj2 = this.Deserialize(answerId);
                if (obj2 != null)
                {
                    this._webCache.Insert("nsurvey:answ" + answerId + ":properties", obj2, null, DateTime.Now.AddHours(1.0), TimeSpan.Zero);
                }
            }
            else if ((this._webCache != null) && (this._webCache["nsurvey:answ" + answerId + ":properties"] != null))
            {
                obj2 = this._webCache["nsurvey:answ" + answerId + ":properties"];
            }
            else
            {
                obj2 = this.Deserialize(answerId);
            }
            this._properties = (obj2 != null) ? ((Hashtable) obj2) : new Hashtable();
        }

        public void Add(string propertyName, object propertyValue)
        {
            if (propertyName == null)
            {
                throw new ArgumentException("Property name can not be null");
            }
            if (this._properties.ContainsKey(propertyName))
            {
                this._properties[propertyName] = propertyValue;
            }
            else
            {
                this._properties.Add(propertyName, propertyValue);
            }
        }

        public bool ContainsProperty(string propertyName)
        {
            return this._properties.ContainsKey(propertyName);
        }

        /// <summary>
        /// Deserialize the properties from the datastore and 
        /// populate the local properties
        /// </summary>
        /// <param name="answerId">answer owner of the properties</param>
        protected virtual object Deserialize(int answerId)
        {
            byte[] buffer = new Answers().RestoreAnswerProperties(answerId);
            object obj2 = null;
            if (buffer != null)
            {
                MemoryStream serializationStream = null;
                try
                {
                    serializationStream = new MemoryStream(buffer);
                    obj2 = new BinaryFormatter().Deserialize(serializationStream);
                }
                catch (SerializationException)
                {
                    obj2 = new Hashtable();
                }
                finally
                {
                    if (serializationStream != null)
                    {
                        serializationStream.Close();
                    }
                }
            }
            return obj2;
        }

        public IEnumerator GetEnumerator()
        {
            return this._properties.Values.GetEnumerator();
        }

        public void Remove(string propertyName)
        {
            this._properties.Remove(propertyName);
        }

        /// <summary>
        /// Serialize the properties in the datastore
        /// </summary>
        /// <param name="answerId">answer owner of the properties</param>
        public virtual void Serialize(int answerId)
        {
            if (this._properties != null)
            {
                MemoryStream serializationStream = new MemoryStream();
                try
                {
                    new BinaryFormatter().Serialize(serializationStream, this._properties);
                    new Answer().StoreAnswerProperties(answerId, serializationStream.ToArray());
                    if ((this._webCache != null) && (this._webCache["nsurvey:answ" + answerId + ":properties"] != null))
                    {
                        this._webCache.Remove("nsurvey:answ" + answerId + ":properties");
                    }
                }
                finally
                {
                    serializationStream.Close();
                }
            }
        }

        public object this[string PropertyName]
        {
            get
            {
                return this._properties[PropertyName];
            }
            set
            {
                this.Add(PropertyName, value);
            }
        }
    }
}

