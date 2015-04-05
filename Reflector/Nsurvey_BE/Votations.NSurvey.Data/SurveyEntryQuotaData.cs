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

    [Serializable, DebuggerStepThrough, DesignerCategory("code"), ToolboxItem(true)]
    public class SurveyEntryQuotaData : DataSet
    {
        private SurveyEntryQuotasDataTable tableSurveyEntryQuotas;

        public SurveyEntryQuotaData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected SurveyEntryQuotaData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["SurveyEntryQuotas"] != null)
                {
                    base.Tables.Add(new SurveyEntryQuotasDataTable(dataSet.Tables["SurveyEntryQuotas"]));
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
            SurveyEntryQuotaData data = (SurveyEntryQuotaData) base.Clone();
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
            base.DataSetName = "SurveyEntryQuotaData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/SurveyEntryQuotaData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableSurveyEntryQuotas = new SurveyEntryQuotasDataTable();
            base.Tables.Add(this.tableSurveyEntryQuotas);
        }

        internal void InitVars()
        {
            this.tableSurveyEntryQuotas = (SurveyEntryQuotasDataTable) base.Tables["SurveyEntryQuotas"];
            if (this.tableSurveyEntryQuotas != null)
            {
                this.tableSurveyEntryQuotas.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["SurveyEntryQuotas"] != null)
            {
                base.Tables.Add(new SurveyEntryQuotasDataTable(dataSet.Tables["SurveyEntryQuotas"]));
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

        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        private bool ShouldSerializeSurveyEntryQuotas()
        {
            return false;
        }

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SurveyEntryQuotasDataTable SurveyEntryQuotas
        {
            get
            {
                return this.tableSurveyEntryQuotas;
            }
        }

        [DebuggerStepThrough]
        public class SurveyEntryQuotasDataTable : DataTable, IEnumerable
        {
            private DataColumn columnEntryCount;
            private DataColumn columnMaxEntries;
            private DataColumn columnMaxReachedMessage;
            private DataColumn columnSurveyId;

            public event SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEventHandler SurveyEntryQuotasRowChanged;

            public event SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEventHandler SurveyEntryQuotasRowChanging;

            public event SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEventHandler SurveyEntryQuotasRowDeleted;

            public event SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEventHandler SurveyEntryQuotasRowDeleting;

            internal SurveyEntryQuotasDataTable() : base("SurveyEntryQuotas")
            {
                this.InitClass();
            }

            internal SurveyEntryQuotasDataTable(DataTable table) : base(table.TableName)
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

            public void AddSurveyEntryQuotasRow(SurveyEntryQuotaData.SurveyEntryQuotasRow row)
            {
                base.Rows.Add(row);
            }

            public SurveyEntryQuotaData.SurveyEntryQuotasRow AddSurveyEntryQuotasRow(int SurveyId, int MaxEntries, int EntryCount, string MaxReachedMessage)
            {
                SurveyEntryQuotaData.SurveyEntryQuotasRow row = (SurveyEntryQuotaData.SurveyEntryQuotasRow) base.NewRow();
                row.ItemArray = new object[] { SurveyId, MaxEntries, EntryCount, MaxReachedMessage };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                SurveyEntryQuotaData.SurveyEntryQuotasDataTable table = (SurveyEntryQuotaData.SurveyEntryQuotasDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new SurveyEntryQuotaData.SurveyEntryQuotasDataTable();
            }

            public SurveyEntryQuotaData.SurveyEntryQuotasRow FindBySurveyId(int SurveyId)
            {
                return (SurveyEntryQuotaData.SurveyEntryQuotasRow) base.Rows.Find(new object[] { SurveyId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(SurveyEntryQuotaData.SurveyEntryQuotasRow);
            }

            private void InitClass()
            {
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnMaxEntries = new DataColumn("MaxEntries", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnMaxEntries);
                this.columnEntryCount = new DataColumn("EntryCount", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnEntryCount);
                this.columnMaxReachedMessage = new DataColumn("MaxReachedMessage", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnMaxReachedMessage);
                base.Constraints.Add(new UniqueConstraint("SurveyEntryQuotaDataKey1", new DataColumn[] { this.columnSurveyId }, true));
                this.columnSurveyId.AllowDBNull = false;
                this.columnSurveyId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnMaxEntries = base.Columns["MaxEntries"];
                this.columnEntryCount = base.Columns["EntryCount"];
                this.columnMaxReachedMessage = base.Columns["MaxReachedMessage"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new SurveyEntryQuotaData.SurveyEntryQuotasRow(builder);
            }

            public SurveyEntryQuotaData.SurveyEntryQuotasRow NewSurveyEntryQuotasRow()
            {
                return (SurveyEntryQuotaData.SurveyEntryQuotasRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.SurveyEntryQuotasRowChanged != null)
                {
                    this.SurveyEntryQuotasRowChanged(this, new SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEvent((SurveyEntryQuotaData.SurveyEntryQuotasRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.SurveyEntryQuotasRowChanging != null)
                {
                    this.SurveyEntryQuotasRowChanging(this, new SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEvent((SurveyEntryQuotaData.SurveyEntryQuotasRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.SurveyEntryQuotasRowDeleted != null)
                {
                    this.SurveyEntryQuotasRowDeleted(this, new SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEvent((SurveyEntryQuotaData.SurveyEntryQuotasRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.SurveyEntryQuotasRowDeleting != null)
                {
                    this.SurveyEntryQuotasRowDeleting(this, new SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEvent((SurveyEntryQuotaData.SurveyEntryQuotasRow) e.Row, e.Action));
                }
            }

            public void RemoveSurveyEntryQuotasRow(SurveyEntryQuotaData.SurveyEntryQuotasRow row)
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

            internal DataColumn EntryCountColumn
            {
                get
                {
                    return this.columnEntryCount;
                }
            }

            public SurveyEntryQuotaData.SurveyEntryQuotasRow this[int index]
            {
                get
                {
                    return (SurveyEntryQuotaData.SurveyEntryQuotasRow) base.Rows[index];
                }
            }

            internal DataColumn MaxEntriesColumn
            {
                get
                {
                    return this.columnMaxEntries;
                }
            }

            internal DataColumn MaxReachedMessageColumn
            {
                get
                {
                    return this.columnMaxReachedMessage;
                }
            }

            internal DataColumn SurveyIdColumn
            {
                get
                {
                    return this.columnSurveyId;
                }
            }
        }

        [DebuggerStepThrough]
        public class SurveyEntryQuotasRow : DataRow
        {
            private SurveyEntryQuotaData.SurveyEntryQuotasDataTable tableSurveyEntryQuotas;

            internal SurveyEntryQuotasRow(DataRowBuilder rb) : base(rb)
            {
                this.tableSurveyEntryQuotas = (SurveyEntryQuotaData.SurveyEntryQuotasDataTable) base.Table;
            }

            public bool IsEntryCountNull()
            {
                return base.IsNull(this.tableSurveyEntryQuotas.EntryCountColumn);
            }

            public bool IsMaxEntriesNull()
            {
                return base.IsNull(this.tableSurveyEntryQuotas.MaxEntriesColumn);
            }

            public bool IsMaxReachedMessageNull()
            {
                return base.IsNull(this.tableSurveyEntryQuotas.MaxReachedMessageColumn);
            }

            public void SetEntryCountNull()
            {
                base[this.tableSurveyEntryQuotas.EntryCountColumn] = Convert.DBNull;
            }

            public void SetMaxEntriesNull()
            {
                base[this.tableSurveyEntryQuotas.MaxEntriesColumn] = Convert.DBNull;
            }

            public void SetMaxReachedMessageNull()
            {
                base[this.tableSurveyEntryQuotas.MaxReachedMessageColumn] = Convert.DBNull;
            }

            public int EntryCount
            {
                get
                {
                    if (this.IsEntryCountNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableSurveyEntryQuotas.EntryCountColumn];
                }
                set
                {
                    base[this.tableSurveyEntryQuotas.EntryCountColumn] = value;
                }
            }

            public int MaxEntries
            {
                get
                {
                    if (this.IsMaxEntriesNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableSurveyEntryQuotas.MaxEntriesColumn];
                }
                set
                {
                    base[this.tableSurveyEntryQuotas.MaxEntriesColumn] = value;
                }
            }

            public string MaxReachedMessage
            {
                get
                {
                    if (this.IsMaxReachedMessageNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableSurveyEntryQuotas.MaxReachedMessageColumn];
                }
                set
                {
                    base[this.tableSurveyEntryQuotas.MaxReachedMessageColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    return (int) base[this.tableSurveyEntryQuotas.SurveyIdColumn];
                }
                set
                {
                    base[this.tableSurveyEntryQuotas.SurveyIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class SurveyEntryQuotasRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private SurveyEntryQuotaData.SurveyEntryQuotasRow eventRow;

            public SurveyEntryQuotasRowChangeEvent(SurveyEntryQuotaData.SurveyEntryQuotasRow row, DataRowAction action)
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

            public SurveyEntryQuotaData.SurveyEntryQuotasRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void SurveyEntryQuotasRowChangeEventHandler(object sender, SurveyEntryQuotaData.SurveyEntryQuotasRowChangeEvent e);
    }
}

