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
    public class CSVExportData : DataSet
    {
        private ExportAnswersDataTable tableExportAnswers;
        private VoterAnswersDataTable tableVoterAnswers;
        private VotersDataTable tableVoters;

        public CSVExportData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected CSVExportData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["ExportAnswers"] != null)
                {
                    base.Tables.Add(new ExportAnswersDataTable(dataSet.Tables["ExportAnswers"]));
                }
                if (dataSet.Tables["Voters"] != null)
                {
                    base.Tables.Add(new VotersDataTable(dataSet.Tables["Voters"]));
                }
                if (dataSet.Tables["VoterAnswers"] != null)
                {
                    base.Tables.Add(new VoterAnswersDataTable(dataSet.Tables["VoterAnswers"]));
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
            CSVExportData data = (CSVExportData) base.Clone();
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
            base.DataSetName = "CSVExportData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/CSVExportData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableExportAnswers = new ExportAnswersDataTable();
            base.Tables.Add(this.tableExportAnswers);
            this.tableVoters = new VotersDataTable();
            base.Tables.Add(this.tableVoters);
            this.tableVoterAnswers = new VoterAnswersDataTable();
            base.Tables.Add(this.tableVoterAnswers);
        }

        internal void InitVars()
        {
            this.tableExportAnswers = (ExportAnswersDataTable) base.Tables["ExportAnswers"];
            if (this.tableExportAnswers != null)
            {
                this.tableExportAnswers.InitVars();
            }
            this.tableVoters = (VotersDataTable) base.Tables["Voters"];
            if (this.tableVoters != null)
            {
                this.tableVoters.InitVars();
            }
            this.tableVoterAnswers = (VoterAnswersDataTable) base.Tables["VoterAnswers"];
            if (this.tableVoterAnswers != null)
            {
                this.tableVoterAnswers.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["ExportAnswers"] != null)
            {
                base.Tables.Add(new ExportAnswersDataTable(dataSet.Tables["ExportAnswers"]));
            }
            if (dataSet.Tables["Voters"] != null)
            {
                base.Tables.Add(new VotersDataTable(dataSet.Tables["Voters"]));
            }
            if (dataSet.Tables["VoterAnswers"] != null)
            {
                base.Tables.Add(new VoterAnswersDataTable(dataSet.Tables["VoterAnswers"]));
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

        private bool ShouldSerializeExportAnswers()
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

        private bool ShouldSerializeVoterAnswers()
        {
            return false;
        }

        private bool ShouldSerializeVoters()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public ExportAnswersDataTable ExportAnswers
        {
            get
            {
                return this.tableExportAnswers;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public VoterAnswersDataTable VoterAnswers
        {
            get
            {
                return this.tableVoterAnswers;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public VotersDataTable Voters
        {
            get
            {
                return this.tableVoters;
            }
        }

        [DebuggerStepThrough]
        public class ExportAnswersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnColumnHeader;

            public event CSVExportData.ExportAnswersRowChangeEventHandler ExportAnswersRowChanged;

            public event CSVExportData.ExportAnswersRowChangeEventHandler ExportAnswersRowChanging;

            public event CSVExportData.ExportAnswersRowChangeEventHandler ExportAnswersRowDeleted;

            public event CSVExportData.ExportAnswersRowChangeEventHandler ExportAnswersRowDeleting;

            internal ExportAnswersDataTable() : base("ExportAnswers")
            {
                this.InitClass();
            }

            internal ExportAnswersDataTable(DataTable table) : base(table.TableName)
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

            public CSVExportData.ExportAnswersRow AddExportAnswersRow(string ColumnHeader)
            {
                CSVExportData.ExportAnswersRow row = (CSVExportData.ExportAnswersRow) base.NewRow();
                object[] objArray = new object[2];
                objArray[1] = ColumnHeader;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public void AddExportAnswersRow(CSVExportData.ExportAnswersRow row)
            {
                base.Rows.Add(row);
            }

            public override DataTable Clone()
            {
                CSVExportData.ExportAnswersDataTable table = (CSVExportData.ExportAnswersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new CSVExportData.ExportAnswersDataTable();
            }

            public CSVExportData.ExportAnswersRow FindByAnswerId(int AnswerId)
            {
                return (CSVExportData.ExportAnswersRow) base.Rows.Find(new object[] { AnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(CSVExportData.ExportAnswersRow);
            }

            private void InitClass()
            {
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnColumnHeader = new DataColumn("ColumnHeader", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnColumnHeader);
                base.Constraints.Add(new UniqueConstraint("CSVExportDataKey1", new DataColumn[] { this.columnAnswerId }, true));
                this.columnAnswerId.AutoIncrement = true;
                this.columnAnswerId.AllowDBNull = false;
                this.columnAnswerId.ReadOnly = true;
                this.columnAnswerId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnColumnHeader = base.Columns["ColumnHeader"];
            }

            public CSVExportData.ExportAnswersRow NewExportAnswersRow()
            {
                return (CSVExportData.ExportAnswersRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new CSVExportData.ExportAnswersRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.ExportAnswersRowChanged != null)
                {
                    this.ExportAnswersRowChanged(this, new CSVExportData.ExportAnswersRowChangeEvent((CSVExportData.ExportAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.ExportAnswersRowChanging != null)
                {
                    this.ExportAnswersRowChanging(this, new CSVExportData.ExportAnswersRowChangeEvent((CSVExportData.ExportAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.ExportAnswersRowDeleted != null)
                {
                    this.ExportAnswersRowDeleted(this, new CSVExportData.ExportAnswersRowChangeEvent((CSVExportData.ExportAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.ExportAnswersRowDeleting != null)
                {
                    this.ExportAnswersRowDeleting(this, new CSVExportData.ExportAnswersRowChangeEvent((CSVExportData.ExportAnswersRow) e.Row, e.Action));
                }
            }

            public void RemoveExportAnswersRow(CSVExportData.ExportAnswersRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AnswerIdColumn
            {
                get
                {
                    return this.columnAnswerId;
                }
            }

            internal DataColumn ColumnHeaderColumn
            {
                get
                {
                    return this.columnColumnHeader;
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

            public CSVExportData.ExportAnswersRow this[int index]
            {
                get
                {
                    return (CSVExportData.ExportAnswersRow) base.Rows[index];
                }
            }
        }

        [DebuggerStepThrough]
        public class ExportAnswersRow : DataRow
        {
            private CSVExportData.ExportAnswersDataTable tableExportAnswers;

            internal ExportAnswersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableExportAnswers = (CSVExportData.ExportAnswersDataTable) base.Table;
            }

            public bool IsColumnHeaderNull()
            {
                return base.IsNull(this.tableExportAnswers.ColumnHeaderColumn);
            }

            public void SetColumnHeaderNull()
            {
                base[this.tableExportAnswers.ColumnHeaderColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    return (int) base[this.tableExportAnswers.AnswerIdColumn];
                }
                set
                {
                    base[this.tableExportAnswers.AnswerIdColumn] = value;
                }
            }

            public string ColumnHeader
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableExportAnswers.ColumnHeaderColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableExportAnswers.ColumnHeaderColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class ExportAnswersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private CSVExportData.ExportAnswersRow eventRow;

            public ExportAnswersRowChangeEvent(CSVExportData.ExportAnswersRow row, DataRowAction action)
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

            public CSVExportData.ExportAnswersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void ExportAnswersRowChangeEventHandler(object sender, CSVExportData.ExportAnswersRowChangeEvent e);

        [DebuggerStepThrough]
        public class VoterAnswersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerID;
            private DataColumn columnAnswerText;
            private DataColumn columnSectionNumber;
            private DataColumn columnVoterID;

            public event CSVExportData.VoterAnswersRowChangeEventHandler VoterAnswersRowChanged;

            public event CSVExportData.VoterAnswersRowChangeEventHandler VoterAnswersRowChanging;

            public event CSVExportData.VoterAnswersRowChangeEventHandler VoterAnswersRowDeleted;

            public event CSVExportData.VoterAnswersRowChangeEventHandler VoterAnswersRowDeleting;

            internal VoterAnswersDataTable() : base("VoterAnswers")
            {
                this.InitClass();
            }

            internal VoterAnswersDataTable(DataTable table) : base(table.TableName)
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

            public void AddVoterAnswersRow(CSVExportData.VoterAnswersRow row)
            {
                base.Rows.Add(row);
            }

            public CSVExportData.VoterAnswersRow AddVoterAnswersRow(int VoterID, int AnswerID, string AnswerText, int SectionNumber)
            {
                CSVExportData.VoterAnswersRow row = (CSVExportData.VoterAnswersRow) base.NewRow();
                row.ItemArray = new object[] { VoterID, AnswerID, AnswerText, SectionNumber };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                CSVExportData.VoterAnswersDataTable table = (CSVExportData.VoterAnswersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new CSVExportData.VoterAnswersDataTable();
            }

            public CSVExportData.VoterAnswersRow FindByVoterIDAnswerIDSectionNumber(int VoterID, int AnswerID, int SectionNumber)
            {
                return (CSVExportData.VoterAnswersRow) base.Rows.Find(new object[] { VoterID, AnswerID, SectionNumber });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(CSVExportData.VoterAnswersRow);
            }

            private void InitClass()
            {
                this.columnVoterID = new DataColumn("VoterID", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoterID);
                this.columnAnswerID = new DataColumn("AnswerID", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerID);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnSectionNumber = new DataColumn("SectionNumber", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSectionNumber);
                base.Constraints.Add(new UniqueConstraint("CSVExportDataKey2", new DataColumn[] { this.columnVoterID, this.columnAnswerID, this.columnSectionNumber }, true));
                this.columnVoterID.AllowDBNull = false;
                this.columnAnswerID.AllowDBNull = false;
                this.columnSectionNumber.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnVoterID = base.Columns["VoterID"];
                this.columnAnswerID = base.Columns["AnswerID"];
                this.columnAnswerText = base.Columns["AnswerText"];
                this.columnSectionNumber = base.Columns["SectionNumber"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new CSVExportData.VoterAnswersRow(builder);
            }

            public CSVExportData.VoterAnswersRow NewVoterAnswersRow()
            {
                return (CSVExportData.VoterAnswersRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.VoterAnswersRowChanged != null)
                {
                    this.VoterAnswersRowChanged(this, new CSVExportData.VoterAnswersRowChangeEvent((CSVExportData.VoterAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.VoterAnswersRowChanging != null)
                {
                    this.VoterAnswersRowChanging(this, new CSVExportData.VoterAnswersRowChangeEvent((CSVExportData.VoterAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.VoterAnswersRowDeleted != null)
                {
                    this.VoterAnswersRowDeleted(this, new CSVExportData.VoterAnswersRowChangeEvent((CSVExportData.VoterAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.VoterAnswersRowDeleting != null)
                {
                    this.VoterAnswersRowDeleting(this, new CSVExportData.VoterAnswersRowChangeEvent((CSVExportData.VoterAnswersRow) e.Row, e.Action));
                }
            }

            public void RemoveVoterAnswersRow(CSVExportData.VoterAnswersRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AnswerIDColumn
            {
                get
                {
                    return this.columnAnswerID;
                }
            }

            internal DataColumn AnswerTextColumn
            {
                get
                {
                    return this.columnAnswerText;
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

            public CSVExportData.VoterAnswersRow this[int index]
            {
                get
                {
                    return (CSVExportData.VoterAnswersRow) base.Rows[index];
                }
            }

            internal DataColumn SectionNumberColumn
            {
                get
                {
                    return this.columnSectionNumber;
                }
            }

            internal DataColumn VoterIDColumn
            {
                get
                {
                    return this.columnVoterID;
                }
            }
        }

        [DebuggerStepThrough]
        public class VoterAnswersRow : DataRow
        {
            private CSVExportData.VoterAnswersDataTable tableVoterAnswers;

            internal VoterAnswersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableVoterAnswers = (CSVExportData.VoterAnswersDataTable) base.Table;
            }

            public bool IsAnswerTextNull()
            {
                return base.IsNull(this.tableVoterAnswers.AnswerTextColumn);
            }

            public void SetAnswerTextNull()
            {
                base[this.tableVoterAnswers.AnswerTextColumn] = Convert.DBNull;
            }

            public int AnswerID
            {
                get
                {
                    return (int) base[this.tableVoterAnswers.AnswerIDColumn];
                }
                set
                {
                    base[this.tableVoterAnswers.AnswerIDColumn] = value;
                }
            }

            public string AnswerText
            {
                get
                {
                    if (this.IsAnswerTextNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableVoterAnswers.AnswerTextColumn];
                }
                set
                {
                    base[this.tableVoterAnswers.AnswerTextColumn] = value;
                }
            }

            public int SectionNumber
            {
                get
                {
                    return (int) base[this.tableVoterAnswers.SectionNumberColumn];
                }
                set
                {
                    base[this.tableVoterAnswers.SectionNumberColumn] = value;
                }
            }

            public int VoterID
            {
                get
                {
                    return (int) base[this.tableVoterAnswers.VoterIDColumn];
                }
                set
                {
                    base[this.tableVoterAnswers.VoterIDColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class VoterAnswersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private CSVExportData.VoterAnswersRow eventRow;

            public VoterAnswersRowChangeEvent(CSVExportData.VoterAnswersRow row, DataRowAction action)
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

            public CSVExportData.VoterAnswersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void VoterAnswersRowChangeEventHandler(object sender, CSVExportData.VoterAnswersRowChangeEvent e);

        [DebuggerStepThrough]
        public class VotersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnContextUserName;
            private DataColumn columnEmail;
            private DataColumn columnIPSource;
            private DataColumn columnScore;
            private DataColumn columnStartDate;
            private DataColumn columnUserName;
            private DataColumn columnVoteDate;
            private DataColumn columnVoterID;

            public event CSVExportData.VotersRowChangeEventHandler VotersRowChanged;

            public event CSVExportData.VotersRowChangeEventHandler VotersRowChanging;

            public event CSVExportData.VotersRowChangeEventHandler VotersRowDeleted;

            public event CSVExportData.VotersRowChangeEventHandler VotersRowDeleting;

            internal VotersDataTable() : base("Voters")
            {
                this.InitClass();
            }

            internal VotersDataTable(DataTable table) : base(table.TableName)
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

            public void AddVotersRow(CSVExportData.VotersRow row)
            {
                base.Rows.Add(row);
            }

            public CSVExportData.VotersRow AddVotersRow(string ContextUserName, DateTime VoteDate, DateTime StartDate, string IPSource, int Score, string Email, string UserName)
            {
                CSVExportData.VotersRow row = (CSVExportData.VotersRow) base.NewRow();
                object[] objArray = new object[8];
                objArray[1] = ContextUserName;
                objArray[2] = VoteDate;
                objArray[3] = StartDate;
                objArray[4] = IPSource;
                objArray[5] = Score;
                objArray[6] = Email;
                objArray[7] = UserName;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                CSVExportData.VotersDataTable table = (CSVExportData.VotersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new CSVExportData.VotersDataTable();
            }

            public CSVExportData.VotersRow FindByVoterID(int VoterID)
            {
                return (CSVExportData.VotersRow) base.Rows.Find(new object[] { VoterID });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(CSVExportData.VotersRow);
            }

            private void InitClass()
            {
                this.columnVoterID = new DataColumn("VoterID", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoterID);
                this.columnContextUserName = new DataColumn("ContextUserName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnContextUserName);
                this.columnVoteDate = new DataColumn("VoteDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnVoteDate);
                this.columnStartDate = new DataColumn("StartDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnStartDate);
                this.columnIPSource = new DataColumn("IPSource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnIPSource);
                this.columnScore = new DataColumn("Score", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnScore);
                this.columnEmail = new DataColumn("Email", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnEmail);
                this.columnUserName = new DataColumn("UserName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUserName);
                base.Constraints.Add(new UniqueConstraint("CSVExportDataKey3", new DataColumn[] { this.columnVoterID }, true));
                this.columnVoterID.AutoIncrement = true;
                this.columnVoterID.AllowDBNull = false;
                this.columnVoterID.ReadOnly = true;
                this.columnVoterID.Unique = true;
            }

            internal void InitVars()
            {
                this.columnVoterID = base.Columns["VoterID"];
                this.columnContextUserName = base.Columns["ContextUserName"];
                this.columnVoteDate = base.Columns["VoteDate"];
                this.columnStartDate = base.Columns["StartDate"];
                this.columnIPSource = base.Columns["IPSource"];
                this.columnScore = base.Columns["Score"];
                this.columnEmail = base.Columns["Email"];
                this.columnUserName = base.Columns["UserName"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new CSVExportData.VotersRow(builder);
            }

            public CSVExportData.VotersRow NewVotersRow()
            {
                return (CSVExportData.VotersRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.VotersRowChanged != null)
                {
                    this.VotersRowChanged(this, new CSVExportData.VotersRowChangeEvent((CSVExportData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.VotersRowChanging != null)
                {
                    this.VotersRowChanging(this, new CSVExportData.VotersRowChangeEvent((CSVExportData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.VotersRowDeleted != null)
                {
                    this.VotersRowDeleted(this, new CSVExportData.VotersRowChangeEvent((CSVExportData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.VotersRowDeleting != null)
                {
                    this.VotersRowDeleting(this, new CSVExportData.VotersRowChangeEvent((CSVExportData.VotersRow) e.Row, e.Action));
                }
            }

            public void RemoveVotersRow(CSVExportData.VotersRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn ContextUserNameColumn
            {
                get
                {
                    return this.columnContextUserName;
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

            internal DataColumn IPSourceColumn
            {
                get
                {
                    return this.columnIPSource;
                }
            }

            public CSVExportData.VotersRow this[int index]
            {
                get
                {
                    return (CSVExportData.VotersRow) base.Rows[index];
                }
            }

            internal DataColumn ScoreColumn
            {
                get
                {
                    return this.columnScore;
                }
            }

            internal DataColumn StartDateColumn
            {
                get
                {
                    return this.columnStartDate;
                }
            }

            internal DataColumn UserNameColumn
            {
                get
                {
                    return this.columnUserName;
                }
            }

            internal DataColumn VoteDateColumn
            {
                get
                {
                    return this.columnVoteDate;
                }
            }

            internal DataColumn VoterIDColumn
            {
                get
                {
                    return this.columnVoterID;
                }
            }
        }

        [DebuggerStepThrough]
        public class VotersRow : DataRow
        {
            private CSVExportData.VotersDataTable tableVoters;

            internal VotersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableVoters = (CSVExportData.VotersDataTable) base.Table;
            }

            public bool IsContextUserNameNull()
            {
                return base.IsNull(this.tableVoters.ContextUserNameColumn);
            }

            public bool IsEmailNull()
            {
                return base.IsNull(this.tableVoters.EmailColumn);
            }

            public bool IsIPSourceNull()
            {
                return base.IsNull(this.tableVoters.IPSourceColumn);
            }

            public bool IsScoreNull()
            {
                return base.IsNull(this.tableVoters.ScoreColumn);
            }

            public bool IsStartDateNull()
            {
                return base.IsNull(this.tableVoters.StartDateColumn);
            }

            public bool IsUserNameNull()
            {
                return base.IsNull(this.tableVoters.UserNameColumn);
            }

            public bool IsVoteDateNull()
            {
                return base.IsNull(this.tableVoters.VoteDateColumn);
            }

            public void SetContextUserNameNull()
            {
                base[this.tableVoters.ContextUserNameColumn] = Convert.DBNull;
            }

            public void SetEmailNull()
            {
                base[this.tableVoters.EmailColumn] = Convert.DBNull;
            }

            public void SetIPSourceNull()
            {
                base[this.tableVoters.IPSourceColumn] = Convert.DBNull;
            }

            public void SetScoreNull()
            {
                base[this.tableVoters.ScoreColumn] = Convert.DBNull;
            }

            public void SetStartDateNull()
            {
                base[this.tableVoters.StartDateColumn] = Convert.DBNull;
            }

            public void SetUserNameNull()
            {
                base[this.tableVoters.UserNameColumn] = Convert.DBNull;
            }

            public void SetVoteDateNull()
            {
                base[this.tableVoters.VoteDateColumn] = Convert.DBNull;
            }

            public string ContextUserName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableVoters.ContextUserNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableVoters.ContextUserNameColumn] = value;
                }
            }

            public string Email
            {
                get
                {
                    if (this.IsEmailNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableVoters.EmailColumn];
                }
                set
                {
                    base[this.tableVoters.EmailColumn] = value;
                }
            }

            public string IPSource
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableVoters.IPSourceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableVoters.IPSourceColumn] = value;
                }
            }

            public int Score
            {
                get
                {
                    if (this.IsScoreNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableVoters.ScoreColumn];
                }
                set
                {
                    base[this.tableVoters.ScoreColumn] = value;
                }
            }

            public DateTime StartDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableVoters.StartDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableVoters.StartDateColumn] = value;
                }
            }

            public string UserName
            {
                get
                {
                    if (this.IsUserNameNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableVoters.UserNameColumn];
                }
                set
                {
                    base[this.tableVoters.UserNameColumn] = value;
                }
            }

            public DateTime VoteDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableVoters.VoteDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableVoters.VoteDateColumn] = value;
                }
            }

            public int VoterID
            {
                get
                {
                    return (int) base[this.tableVoters.VoterIDColumn];
                }
                set
                {
                    base[this.tableVoters.VoterIDColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class VotersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private CSVExportData.VotersRow eventRow;

            public VotersRowChangeEvent(CSVExportData.VotersRow row, DataRowAction action)
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

            public CSVExportData.VotersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void VotersRowChangeEventHandler(object sender, CSVExportData.VotersRowChangeEvent e);
    }
}

