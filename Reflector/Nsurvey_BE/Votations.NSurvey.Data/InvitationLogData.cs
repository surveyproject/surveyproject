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
    public class InvitationLogData : DataSet
    {
        private InvitationLogsDataTable tableInvitationLogs;

        public InvitationLogData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected InvitationLogData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["InvitationLogs"] != null)
                {
                    base.Tables.Add(new InvitationLogsDataTable(dataSet.Tables["InvitationLogs"]));
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
            InvitationLogData data = (InvitationLogData) base.Clone();
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
            base.DataSetName = "InvitationLogData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/InvitationLogData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableInvitationLogs = new InvitationLogsDataTable();
            base.Tables.Add(this.tableInvitationLogs);
        }

        internal void InitVars()
        {
            this.tableInvitationLogs = (InvitationLogsDataTable) base.Tables["InvitationLogs"];
            if (this.tableInvitationLogs != null)
            {
                this.tableInvitationLogs.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["InvitationLogs"] != null)
            {
                base.Tables.Add(new InvitationLogsDataTable(dataSet.Tables["InvitationLogs"]));
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

        private bool ShouldSerializeInvitationLogs()
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
        public InvitationLogsDataTable InvitationLogs
        {
            get
            {
                return this.tableInvitationLogs;
            }
        }

        [DebuggerStepThrough]
        public class InvitationLogsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnEmail;
            private DataColumn columnEmailId;
            private DataColumn columnErrorDate;
            private DataColumn columnExceptionMessage;
            private DataColumn columnExceptionType;
            private DataColumn columnInvitationLogId;
            private DataColumn columnSurveyId;

            public event InvitationLogData.InvitationLogsRowChangeEventHandler InvitationLogsRowChanged;

            public event InvitationLogData.InvitationLogsRowChangeEventHandler InvitationLogsRowChanging;

            public event InvitationLogData.InvitationLogsRowChangeEventHandler InvitationLogsRowDeleted;

            public event InvitationLogData.InvitationLogsRowChangeEventHandler InvitationLogsRowDeleting;

            internal InvitationLogsDataTable() : base("InvitationLogs")
            {
                this.InitClass();
            }

            internal InvitationLogsDataTable(DataTable table) : base(table.TableName)
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

            public void AddInvitationLogsRow(InvitationLogData.InvitationLogsRow row)
            {
                base.Rows.Add(row);
            }

            public InvitationLogData.InvitationLogsRow AddInvitationLogsRow(int SurveyId, int EmailId, string ExceptionMessage, string ExceptionType, DateTime ErrorDate, string Email)
            {
                InvitationLogData.InvitationLogsRow row = (InvitationLogData.InvitationLogsRow) base.NewRow();
                object[] objArray = new object[7];
                objArray[1] = SurveyId;
                objArray[2] = EmailId;
                objArray[3] = ExceptionMessage;
                objArray[4] = ExceptionType;
                objArray[5] = ErrorDate;
                objArray[6] = Email;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                InvitationLogData.InvitationLogsDataTable table = (InvitationLogData.InvitationLogsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new InvitationLogData.InvitationLogsDataTable();
            }

            public InvitationLogData.InvitationLogsRow FindByInvitationLogId(int InvitationLogId)
            {
                return (InvitationLogData.InvitationLogsRow) base.Rows.Find(new object[] { InvitationLogId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(InvitationLogData.InvitationLogsRow);
            }

            private void InitClass()
            {
                this.columnInvitationLogId = new DataColumn("InvitationLogId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnInvitationLogId);
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnEmailId = new DataColumn("EmailId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnEmailId);
                this.columnExceptionMessage = new DataColumn("ExceptionMessage", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnExceptionMessage);
                this.columnExceptionType = new DataColumn("ExceptionType", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnExceptionType);
                this.columnErrorDate = new DataColumn("ErrorDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnErrorDate);
                this.columnEmail = new DataColumn("Email", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnEmail);
                base.Constraints.Add(new UniqueConstraint("InvitationLogDataKey1", new DataColumn[] { this.columnInvitationLogId }, true));
                this.columnInvitationLogId.AutoIncrement = true;
                this.columnInvitationLogId.AllowDBNull = false;
                this.columnInvitationLogId.ReadOnly = true;
                this.columnInvitationLogId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnInvitationLogId = base.Columns["InvitationLogId"];
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnEmailId = base.Columns["EmailId"];
                this.columnExceptionMessage = base.Columns["ExceptionMessage"];
                this.columnExceptionType = base.Columns["ExceptionType"];
                this.columnErrorDate = base.Columns["ErrorDate"];
                this.columnEmail = base.Columns["Email"];
            }

            public InvitationLogData.InvitationLogsRow NewInvitationLogsRow()
            {
                return (InvitationLogData.InvitationLogsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new InvitationLogData.InvitationLogsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.InvitationLogsRowChanged != null)
                {
                    this.InvitationLogsRowChanged(this, new InvitationLogData.InvitationLogsRowChangeEvent((InvitationLogData.InvitationLogsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.InvitationLogsRowChanging != null)
                {
                    this.InvitationLogsRowChanging(this, new InvitationLogData.InvitationLogsRowChangeEvent((InvitationLogData.InvitationLogsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.InvitationLogsRowDeleted != null)
                {
                    this.InvitationLogsRowDeleted(this, new InvitationLogData.InvitationLogsRowChangeEvent((InvitationLogData.InvitationLogsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.InvitationLogsRowDeleting != null)
                {
                    this.InvitationLogsRowDeleting(this, new InvitationLogData.InvitationLogsRowChangeEvent((InvitationLogData.InvitationLogsRow) e.Row, e.Action));
                }
            }

            public void RemoveInvitationLogsRow(InvitationLogData.InvitationLogsRow row)
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

            internal DataColumn ErrorDateColumn
            {
                get
                {
                    return this.columnErrorDate;
                }
            }

            internal DataColumn ExceptionMessageColumn
            {
                get
                {
                    return this.columnExceptionMessage;
                }
            }

            internal DataColumn ExceptionTypeColumn
            {
                get
                {
                    return this.columnExceptionType;
                }
            }

            internal DataColumn InvitationLogIdColumn
            {
                get
                {
                    return this.columnInvitationLogId;
                }
            }

            public InvitationLogData.InvitationLogsRow this[int index]
            {
                get
                {
                    return (InvitationLogData.InvitationLogsRow) base.Rows[index];
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
        public class InvitationLogsRow : DataRow
        {
            private InvitationLogData.InvitationLogsDataTable tableInvitationLogs;

            internal InvitationLogsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableInvitationLogs = (InvitationLogData.InvitationLogsDataTable) base.Table;
            }

            public bool IsEmailIdNull()
            {
                return base.IsNull(this.tableInvitationLogs.EmailIdColumn);
            }

            public bool IsEmailNull()
            {
                return base.IsNull(this.tableInvitationLogs.EmailColumn);
            }

            public bool IsErrorDateNull()
            {
                return base.IsNull(this.tableInvitationLogs.ErrorDateColumn);
            }

            public bool IsExceptionMessageNull()
            {
                return base.IsNull(this.tableInvitationLogs.ExceptionMessageColumn);
            }

            public bool IsExceptionTypeNull()
            {
                return base.IsNull(this.tableInvitationLogs.ExceptionTypeColumn);
            }

            public bool IsSurveyIdNull()
            {
                return base.IsNull(this.tableInvitationLogs.SurveyIdColumn);
            }

            public void SetEmailIdNull()
            {
                base[this.tableInvitationLogs.EmailIdColumn] = Convert.DBNull;
            }

            public void SetEmailNull()
            {
                base[this.tableInvitationLogs.EmailColumn] = Convert.DBNull;
            }

            public void SetErrorDateNull()
            {
                base[this.tableInvitationLogs.ErrorDateColumn] = Convert.DBNull;
            }

            public void SetExceptionMessageNull()
            {
                base[this.tableInvitationLogs.ExceptionMessageColumn] = Convert.DBNull;
            }

            public void SetExceptionTypeNull()
            {
                base[this.tableInvitationLogs.ExceptionTypeColumn] = Convert.DBNull;
            }

            public void SetSurveyIdNull()
            {
                base[this.tableInvitationLogs.SurveyIdColumn] = Convert.DBNull;
            }

            public string Email
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableInvitationLogs.EmailColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableInvitationLogs.EmailColumn] = value;
                }
            }

            public int EmailId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableInvitationLogs.EmailIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableInvitationLogs.EmailIdColumn] = value;
                }
            }

            public DateTime ErrorDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableInvitationLogs.ErrorDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableInvitationLogs.ErrorDateColumn] = value;
                }
            }

            public string ExceptionMessage
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableInvitationLogs.ExceptionMessageColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableInvitationLogs.ExceptionMessageColumn] = value;
                }
            }

            public string ExceptionType
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableInvitationLogs.ExceptionTypeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableInvitationLogs.ExceptionTypeColumn] = value;
                }
            }

            public int InvitationLogId
            {
                get
                {
                    return (int) base[this.tableInvitationLogs.InvitationLogIdColumn];
                }
                set
                {
                    base[this.tableInvitationLogs.InvitationLogIdColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableInvitationLogs.SurveyIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableInvitationLogs.SurveyIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class InvitationLogsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private InvitationLogData.InvitationLogsRow eventRow;

            public InvitationLogsRowChangeEvent(InvitationLogData.InvitationLogsRow row, DataRowAction action)
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

            public InvitationLogData.InvitationLogsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void InvitationLogsRowChangeEventHandler(object sender, InvitationLogData.InvitationLogsRowChangeEvent e);
    }
}

