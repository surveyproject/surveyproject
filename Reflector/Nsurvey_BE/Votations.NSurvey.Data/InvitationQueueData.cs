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
    public class InvitationQueueData : DataSet
    {
        private InvitationQueuesDataTable tableInvitationQueues;

        public InvitationQueueData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected InvitationQueueData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["InvitationQueues"] != null)
                {
                    base.Tables.Add(new InvitationQueuesDataTable(dataSet.Tables["InvitationQueues"]));
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
            InvitationQueueData data = (InvitationQueueData) base.Clone();
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
            base.DataSetName = "InvitationQueueData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/InvitationQueueData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableInvitationQueues = new InvitationQueuesDataTable();
            base.Tables.Add(this.tableInvitationQueues);
        }

        internal void InitVars()
        {
            this.tableInvitationQueues = (InvitationQueuesDataTable) base.Tables["InvitationQueues"];
            if (this.tableInvitationQueues != null)
            {
                this.tableInvitationQueues.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["InvitationQueues"] != null)
            {
                base.Tables.Add(new InvitationQueuesDataTable(dataSet.Tables["InvitationQueues"]));
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

        private bool ShouldSerializeInvitationQueues()
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

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public InvitationQueuesDataTable InvitationQueues
        {
            get
            {
                return this.tableInvitationQueues;
            }
        }

        [DebuggerStepThrough]
        public class InvitationQueuesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnonymousEntry;
            private DataColumn columnEmail;
            private DataColumn columnEmailId;
            private DataColumn columnRequestDate;
            private DataColumn columnSurveyId;
            private DataColumn columnUId;

            public event InvitationQueueData.InvitationQueuesRowChangeEventHandler InvitationQueuesRowChanged;

            public event InvitationQueueData.InvitationQueuesRowChangeEventHandler InvitationQueuesRowChanging;

            public event InvitationQueueData.InvitationQueuesRowChangeEventHandler InvitationQueuesRowDeleted;

            public event InvitationQueueData.InvitationQueuesRowChangeEventHandler InvitationQueuesRowDeleting;

            internal InvitationQueuesDataTable() : base("InvitationQueues")
            {
                this.InitClass();
            }

            internal InvitationQueuesDataTable(DataTable table) : base(table.TableName)
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

            public void AddInvitationQueuesRow(InvitationQueueData.InvitationQueuesRow row)
            {
                base.Rows.Add(row);
            }

            public InvitationQueueData.InvitationQueuesRow AddInvitationQueuesRow(int EmailId, int SurveyId, string UId, DateTime RequestDate, bool AnonymousEntry, string Email)
            {
                InvitationQueueData.InvitationQueuesRow row = (InvitationQueueData.InvitationQueuesRow) base.NewRow();
                row.ItemArray = new object[] { EmailId, SurveyId, UId, RequestDate, AnonymousEntry, Email };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                InvitationQueueData.InvitationQueuesDataTable table = (InvitationQueueData.InvitationQueuesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new InvitationQueueData.InvitationQueuesDataTable();
            }

            public InvitationQueueData.InvitationQueuesRow FindByEmailIdSurveyId(int EmailId, int SurveyId)
            {
                return (InvitationQueueData.InvitationQueuesRow) base.Rows.Find(new object[] { EmailId, SurveyId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(InvitationQueueData.InvitationQueuesRow);
            }

            private void InitClass()
            {
                this.columnEmailId = new DataColumn("EmailId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnEmailId);
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnUId = new DataColumn("UId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUId);
                this.columnRequestDate = new DataColumn("RequestDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnRequestDate);
                this.columnAnonymousEntry = new DataColumn("AnonymousEntry", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnAnonymousEntry);
                this.columnEmail = new DataColumn("Email", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnEmail);
                base.Constraints.Add(new UniqueConstraint("InvitationQueueDataKey1", new DataColumn[] { this.columnEmailId, this.columnSurveyId }, true));
                this.columnEmailId.AllowDBNull = false;
                this.columnSurveyId.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnEmailId = base.Columns["EmailId"];
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnUId = base.Columns["UId"];
                this.columnRequestDate = base.Columns["RequestDate"];
                this.columnAnonymousEntry = base.Columns["AnonymousEntry"];
                this.columnEmail = base.Columns["Email"];
            }

            public InvitationQueueData.InvitationQueuesRow NewInvitationQueuesRow()
            {
                return (InvitationQueueData.InvitationQueuesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new InvitationQueueData.InvitationQueuesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.InvitationQueuesRowChanged != null)
                {
                    this.InvitationQueuesRowChanged(this, new InvitationQueueData.InvitationQueuesRowChangeEvent((InvitationQueueData.InvitationQueuesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.InvitationQueuesRowChanging != null)
                {
                    this.InvitationQueuesRowChanging(this, new InvitationQueueData.InvitationQueuesRowChangeEvent((InvitationQueueData.InvitationQueuesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.InvitationQueuesRowDeleted != null)
                {
                    this.InvitationQueuesRowDeleted(this, new InvitationQueueData.InvitationQueuesRowChangeEvent((InvitationQueueData.InvitationQueuesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.InvitationQueuesRowDeleting != null)
                {
                    this.InvitationQueuesRowDeleting(this, new InvitationQueueData.InvitationQueuesRowChangeEvent((InvitationQueueData.InvitationQueuesRow) e.Row, e.Action));
                }
            }

            public void RemoveInvitationQueuesRow(InvitationQueueData.InvitationQueuesRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AnonymousEntryColumn
            {
                get
                {
                    return this.columnAnonymousEntry;
                }
            }

            [Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            internal DataColumn EmailColumn
            {
                get
                {
                    return this.columnEmail;
                }
            }

            internal DataColumn EmailIdColumn
            {
                get
                {
                    return this.columnEmailId;
                }
            }

            public InvitationQueueData.InvitationQueuesRow this[int index]
            {
                get
                {
                    return (InvitationQueueData.InvitationQueuesRow) base.Rows[index];
                }
            }

            internal DataColumn RequestDateColumn
            {
                get
                {
                    return this.columnRequestDate;
                }
            }

            internal DataColumn SurveyIdColumn
            {
                get
                {
                    return this.columnSurveyId;
                }
            }

            internal DataColumn UIdColumn
            {
                get
                {
                    return this.columnUId;
                }
            }
        }

        [DebuggerStepThrough]
        public class InvitationQueuesRow : DataRow
        {
            private InvitationQueueData.InvitationQueuesDataTable tableInvitationQueues;

            internal InvitationQueuesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableInvitationQueues = (InvitationQueueData.InvitationQueuesDataTable) base.Table;
            }

            public bool IsAnonymousEntryNull()
            {
                return base.IsNull(this.tableInvitationQueues.AnonymousEntryColumn);
            }

            public bool IsEmailNull()
            {
                return base.IsNull(this.tableInvitationQueues.EmailColumn);
            }

            public bool IsRequestDateNull()
            {
                return base.IsNull(this.tableInvitationQueues.RequestDateColumn);
            }

            public bool IsUIdNull()
            {
                return base.IsNull(this.tableInvitationQueues.UIdColumn);
            }

            public void SetAnonymousEntryNull()
            {
                base[this.tableInvitationQueues.AnonymousEntryColumn] = Convert.DBNull;
            }

            public void SetEmailNull()
            {
                base[this.tableInvitationQueues.EmailColumn] = Convert.DBNull;
            }

            public void SetRequestDateNull()
            {
                base[this.tableInvitationQueues.RequestDateColumn] = Convert.DBNull;
            }

            public void SetUIdNull()
            {
                base[this.tableInvitationQueues.UIdColumn] = Convert.DBNull;
            }

            public bool AnonymousEntry
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableInvitationQueues.AnonymousEntryColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableInvitationQueues.AnonymousEntryColumn] = value;
                }
            }

            public string Email
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableInvitationQueues.EmailColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableInvitationQueues.EmailColumn] = value;
                }
            }

            public int EmailId
            {
                get
                {
                    return (int) base[this.tableInvitationQueues.EmailIdColumn];
                }
                set
                {
                    base[this.tableInvitationQueues.EmailIdColumn] = value;
                }
            }

            public DateTime RequestDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableInvitationQueues.RequestDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableInvitationQueues.RequestDateColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    return (int) base[this.tableInvitationQueues.SurveyIdColumn];
                }
                set
                {
                    base[this.tableInvitationQueues.SurveyIdColumn] = value;
                }
            }

            public string UId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableInvitationQueues.UIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableInvitationQueues.UIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class InvitationQueuesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private InvitationQueueData.InvitationQueuesRow eventRow;

            public InvitationQueuesRowChangeEvent(InvitationQueueData.InvitationQueuesRow row, DataRowAction action)
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

            public InvitationQueueData.InvitationQueuesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void InvitationQueuesRowChangeEventHandler(object sender, InvitationQueueData.InvitationQueuesRowChangeEvent e);
    }
}

