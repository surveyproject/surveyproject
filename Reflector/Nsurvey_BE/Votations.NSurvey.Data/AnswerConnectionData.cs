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

    [Serializable, ToolboxItem(true), DebuggerStepThrough, DesignerCategory("code")]
    public class AnswerConnectionData : DataSet
    {
        private AnswerConnectionsDataTable tableAnswerConnections;

        public AnswerConnectionData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected AnswerConnectionData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["AnswerConnections"] != null)
                {
                    base.Tables.Add(new AnswerConnectionsDataTable(dataSet.Tables["AnswerConnections"]));
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
            AnswerConnectionData data = (AnswerConnectionData) base.Clone();
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
            base.DataSetName = "AnswerConnectionData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/AnswerSubscriberData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableAnswerConnections = new AnswerConnectionsDataTable();
            base.Tables.Add(this.tableAnswerConnections);
        }

        internal void InitVars()
        {
            this.tableAnswerConnections = (AnswerConnectionsDataTable) base.Tables["AnswerConnections"];
            if (this.tableAnswerConnections != null)
            {
                this.tableAnswerConnections.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["AnswerConnections"] != null)
            {
                base.Tables.Add(new AnswerConnectionsDataTable(dataSet.Tables["AnswerConnections"]));
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

        private bool ShouldSerializeAnswerConnections()
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
        public AnswerConnectionsDataTable AnswerConnections
        {
            get
            {
                return this.tableAnswerConnections;
            }
        }

        [DebuggerStepThrough]
        public class AnswerConnectionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnPublisherAnswerId;
            private DataColumn columnSubscriberAnswerId;

            public event AnswerConnectionData.AnswerConnectionsRowChangeEventHandler AnswerConnectionsRowChanged;

            public event AnswerConnectionData.AnswerConnectionsRowChangeEventHandler AnswerConnectionsRowChanging;

            public event AnswerConnectionData.AnswerConnectionsRowChangeEventHandler AnswerConnectionsRowDeleted;

            public event AnswerConnectionData.AnswerConnectionsRowChangeEventHandler AnswerConnectionsRowDeleting;

            internal AnswerConnectionsDataTable() : base("AnswerConnections")
            {
                this.InitClass();
            }

            internal AnswerConnectionsDataTable(DataTable table) : base(table.TableName)
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

            public void AddAnswerConnectionsRow(AnswerConnectionData.AnswerConnectionsRow row)
            {
                base.Rows.Add(row);
            }

            public AnswerConnectionData.AnswerConnectionsRow AddAnswerConnectionsRow(int PublisherAnswerId, int SubscriberAnswerId)
            {
                AnswerConnectionData.AnswerConnectionsRow row = (AnswerConnectionData.AnswerConnectionsRow) base.NewRow();
                row.ItemArray = new object[] { PublisherAnswerId, SubscriberAnswerId };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                AnswerConnectionData.AnswerConnectionsDataTable table = (AnswerConnectionData.AnswerConnectionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new AnswerConnectionData.AnswerConnectionsDataTable();
            }

            public AnswerConnectionData.AnswerConnectionsRow FindByPublisherAnswerIdSubscriberAnswerId(int PublisherAnswerId, int SubscriberAnswerId)
            {
                return (AnswerConnectionData.AnswerConnectionsRow) base.Rows.Find(new object[] { PublisherAnswerId, SubscriberAnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(AnswerConnectionData.AnswerConnectionsRow);
            }

            private void InitClass()
            {
                this.columnPublisherAnswerId = new DataColumn("PublisherAnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnPublisherAnswerId);
                this.columnSubscriberAnswerId = new DataColumn("SubscriberAnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSubscriberAnswerId);
                base.Constraints.Add(new UniqueConstraint("AnswerSubscriberDataKey1", new DataColumn[] { this.columnPublisherAnswerId, this.columnSubscriberAnswerId }, true));
                this.columnPublisherAnswerId.AllowDBNull = false;
                this.columnSubscriberAnswerId.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnPublisherAnswerId = base.Columns["PublisherAnswerId"];
                this.columnSubscriberAnswerId = base.Columns["SubscriberAnswerId"];
            }

            public AnswerConnectionData.AnswerConnectionsRow NewAnswerConnectionsRow()
            {
                return (AnswerConnectionData.AnswerConnectionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new AnswerConnectionData.AnswerConnectionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswerConnectionsRowChanged != null)
                {
                    this.AnswerConnectionsRowChanged(this, new AnswerConnectionData.AnswerConnectionsRowChangeEvent((AnswerConnectionData.AnswerConnectionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswerConnectionsRowChanging != null)
                {
                    this.AnswerConnectionsRowChanging(this, new AnswerConnectionData.AnswerConnectionsRowChangeEvent((AnswerConnectionData.AnswerConnectionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswerConnectionsRowDeleted != null)
                {
                    this.AnswerConnectionsRowDeleted(this, new AnswerConnectionData.AnswerConnectionsRowChangeEvent((AnswerConnectionData.AnswerConnectionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswerConnectionsRowDeleting != null)
                {
                    this.AnswerConnectionsRowDeleting(this, new AnswerConnectionData.AnswerConnectionsRowChangeEvent((AnswerConnectionData.AnswerConnectionsRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswerConnectionsRow(AnswerConnectionData.AnswerConnectionsRow row)
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

            public AnswerConnectionData.AnswerConnectionsRow this[int index]
            {
                get
                {
                    return (AnswerConnectionData.AnswerConnectionsRow) base.Rows[index];
                }
            }

            internal DataColumn PublisherAnswerIdColumn
            {
                get
                {
                    return this.columnPublisherAnswerId;
                }
            }

            internal DataColumn SubscriberAnswerIdColumn
            {
                get
                {
                    return this.columnSubscriberAnswerId;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswerConnectionsRow : DataRow
        {
            private AnswerConnectionData.AnswerConnectionsDataTable tableAnswerConnections;

            internal AnswerConnectionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswerConnections = (AnswerConnectionData.AnswerConnectionsDataTable) base.Table;
            }

            public int PublisherAnswerId
            {
                get
                {
                    return (int) base[this.tableAnswerConnections.PublisherAnswerIdColumn];
                }
                set
                {
                    base[this.tableAnswerConnections.PublisherAnswerIdColumn] = value;
                }
            }

            public int SubscriberAnswerId
            {
                get
                {
                    return (int) base[this.tableAnswerConnections.SubscriberAnswerIdColumn];
                }
                set
                {
                    base[this.tableAnswerConnections.SubscriberAnswerIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswerConnectionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private AnswerConnectionData.AnswerConnectionsRow eventRow;

            public AnswerConnectionsRowChangeEvent(AnswerConnectionData.AnswerConnectionsRow row, DataRowAction action)
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

            public AnswerConnectionData.AnswerConnectionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswerConnectionsRowChangeEventHandler(object sender, AnswerConnectionData.AnswerConnectionsRowChangeEvent e);
    }
}

