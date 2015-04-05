namespace Votations.NSurvey.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Schema;

    [Serializable, DesignerCategory("code"), DebuggerStepThrough, ToolboxItem(true)]
    public class MultiLanguageData : DataSet
    {
        private MultiLanguagesDataTable tableMultiLanguages;

        public MultiLanguageData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected MultiLanguageData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["MultiLanguages"] != null)
                {
                    base.Tables.Add(new MultiLanguagesDataTable(dataSet.Tables["MultiLanguages"]));
                }
                base.DataSetName = dataSet.DataSetName;
                base.Prefix = dataSet.Prefix;
                base.Namespace = dataSet.Namespace;
                base.Locale = dataSet.Locale;
                base.CaseSensitive = dataSet.CaseSensitive;
                base.EnforceConstraints = dataSet.EnforceConstraints;
                base.Merge(dataSet, false, MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                this.InitClass();
            }
            base.GetSerializationData(info, context);
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        public override DataSet Clone()
        {
            MultiLanguageData data = (MultiLanguageData) base.Clone();
            data.InitVars();
            return data;
        }

        protected override XmlSchema GetSchemaSerializable()
        {
            MemoryStream w = new MemoryStream();
            base.WriteXmlSchema(new XmlTextWriter(w, null));
            w.Position = 0L;
            return XmlSchema.Read(new XmlTextReader(w), null);
        }

        private void InitClass()
        {
            base.DataSetName = "MultiLanguageData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/MultiLanguagesData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableMultiLanguages = new MultiLanguagesDataTable();
            base.Tables.Add(this.tableMultiLanguages);
        }

        internal void InitVars()
        {
            this.tableMultiLanguages = (MultiLanguagesDataTable) base.Tables["MultiLanguages"];
            if (this.tableMultiLanguages != null)
            {
                this.tableMultiLanguages.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["MultiLanguages"] != null)
            {
                base.Tables.Add(new MultiLanguagesDataTable(dataSet.Tables["MultiLanguages"]));
            }
            base.DataSetName = dataSet.DataSetName;
            base.Prefix = dataSet.Prefix;
            base.Namespace = dataSet.Namespace;
            base.Locale = dataSet.Locale;
            base.CaseSensitive = dataSet.CaseSensitive;
            base.EnforceConstraints = dataSet.EnforceConstraints;
            base.Merge(dataSet, false, MissingSchemaAction.Add);
            this.InitVars();
        }

        private void SchemaChanged(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Remove)
            {
                this.InitVars();
            }
        }

        private bool ShouldSerializeMultiLanguages()
        {
            return false;
        }

        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public MultiLanguagesDataTable MultiLanguages
        {
            get
            {
                return this.tableMultiLanguages;
            }
        }

        [DebuggerStepThrough]
        public class MultiLanguagesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDefaultLanguage;
            private DataColumn columnLanguageCode;
            private DataColumn columnLanguageDescription;

            public event MultiLanguageData.MultiLanguagesRowChangeEventHandler MultiLanguagesRowChanged;

            public event MultiLanguageData.MultiLanguagesRowChangeEventHandler MultiLanguagesRowChanging;

            public event MultiLanguageData.MultiLanguagesRowChangeEventHandler MultiLanguagesRowDeleted;

            public event MultiLanguageData.MultiLanguagesRowChangeEventHandler MultiLanguagesRowDeleting;

            internal MultiLanguagesDataTable() : base("MultiLanguages")
            {
                this.InitClass();
            }

            internal MultiLanguagesDataTable(DataTable table) : base(table.TableName)
            {
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
                base.DisplayExpression = table.DisplayExpression;
            }

            public void AddMultiLanguagesRow(MultiLanguageData.MultiLanguagesRow row)
            {
                base.Rows.Add(row);
            }

            public MultiLanguageData.MultiLanguagesRow AddMultiLanguagesRow(string LanguageCode, string LanguageDescription, bool DefaultLanguage)
            {
                MultiLanguageData.MultiLanguagesRow row = (MultiLanguageData.MultiLanguagesRow) base.NewRow();
                row.ItemArray = new object[] { LanguageCode, LanguageDescription, DefaultLanguage };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                MultiLanguageData.MultiLanguagesDataTable table = (MultiLanguageData.MultiLanguagesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new MultiLanguageData.MultiLanguagesDataTable();
            }

            public MultiLanguageData.MultiLanguagesRow FindByLanguageCode(string LanguageCode)
            {
                return (MultiLanguageData.MultiLanguagesRow) base.Rows.Find(new object[] { LanguageCode });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(MultiLanguageData.MultiLanguagesRow);
            }

            private void InitClass()
            {
                this.columnLanguageCode = new DataColumn("LanguageCode", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnLanguageCode);
                this.columnLanguageDescription = new DataColumn("LanguageDescription", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnLanguageDescription);
                this.columnDefaultLanguage = new DataColumn("DefaultLanguage", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnDefaultLanguage);
                base.Constraints.Add(new UniqueConstraint("MultiLanguageDataKey1", new DataColumn[] { this.columnLanguageCode }, true));
                this.columnLanguageCode.AllowDBNull = false;
                this.columnLanguageCode.Unique = true;
            }

            internal void InitVars()
            {
                this.columnLanguageCode = base.Columns["LanguageCode"];
                this.columnLanguageDescription = base.Columns["LanguageDescription"];
                this.columnDefaultLanguage = base.Columns["DefaultLanguage"];
            }

            public MultiLanguageData.MultiLanguagesRow NewMultiLanguagesRow()
            {
                return (MultiLanguageData.MultiLanguagesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new MultiLanguageData.MultiLanguagesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.MultiLanguagesRowChanged != null)
                {
                    this.MultiLanguagesRowChanged(this, new MultiLanguageData.MultiLanguagesRowChangeEvent((MultiLanguageData.MultiLanguagesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.MultiLanguagesRowChanging != null)
                {
                    this.MultiLanguagesRowChanging(this, new MultiLanguageData.MultiLanguagesRowChangeEvent((MultiLanguageData.MultiLanguagesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.MultiLanguagesRowDeleted != null)
                {
                    this.MultiLanguagesRowDeleted(this, new MultiLanguageData.MultiLanguagesRowChangeEvent((MultiLanguageData.MultiLanguagesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.MultiLanguagesRowDeleting != null)
                {
                    this.MultiLanguagesRowDeleting(this, new MultiLanguageData.MultiLanguagesRowChangeEvent((MultiLanguageData.MultiLanguagesRow) e.Row, e.Action));
                }
            }

            public void RemoveMultiLanguagesRow(MultiLanguageData.MultiLanguagesRow row)
            {
                base.Rows.Remove(row);
            }

            [Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            internal DataColumn DefaultLanguageColumn
            {
                get
                {
                    return this.columnDefaultLanguage;
                }
            }

            public MultiLanguageData.MultiLanguagesRow this[int index]
            {
                get
                {
                    return (MultiLanguageData.MultiLanguagesRow) base.Rows[index];
                }
            }

            internal DataColumn LanguageCodeColumn
            {
                get
                {
                    return this.columnLanguageCode;
                }
            }

            internal DataColumn LanguageDescriptionColumn
            {
                get
                {
                    return this.columnLanguageDescription;
                }
            }
        }

        [DebuggerStepThrough]
        public class MultiLanguagesRow : DataRow
        {
            private MultiLanguageData.MultiLanguagesDataTable tableMultiLanguages;

            internal MultiLanguagesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableMultiLanguages = (MultiLanguageData.MultiLanguagesDataTable) base.Table;
            }

            public bool IsDefaultLanguageNull()
            {
                return base.IsNull(this.tableMultiLanguages.DefaultLanguageColumn);
            }

            public bool IsLanguageDescriptionNull()
            {
                return base.IsNull(this.tableMultiLanguages.LanguageDescriptionColumn);
            }

            public void SetDefaultLanguageNull()
            {
                base[this.tableMultiLanguages.DefaultLanguageColumn] = Convert.DBNull;
            }

            public void SetLanguageDescriptionNull()
            {
                base[this.tableMultiLanguages.LanguageDescriptionColumn] = Convert.DBNull;
            }

            public bool DefaultLanguage
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableMultiLanguages.DefaultLanguageColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableMultiLanguages.DefaultLanguageColumn] = value;
                }
            }

            public string LanguageCode
            {
                get
                {
                    return (string) base[this.tableMultiLanguages.LanguageCodeColumn];
                }
                set
                {
                    base[this.tableMultiLanguages.LanguageCodeColumn] = value;
                }
            }

            public string LanguageDescription
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableMultiLanguages.LanguageDescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableMultiLanguages.LanguageDescriptionColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class MultiLanguagesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private MultiLanguageData.MultiLanguagesRow eventRow;

            public MultiLanguagesRowChangeEvent(MultiLanguageData.MultiLanguagesRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            public MultiLanguageData.MultiLanguagesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void MultiLanguagesRowChangeEventHandler(object sender, MultiLanguageData.MultiLanguagesRowChangeEvent e);
    }
}

