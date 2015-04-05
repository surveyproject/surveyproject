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
    public class VoterData : DataSet
    {
        private VotersDataTable tableVoters;

        public VoterData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected VoterData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["Voters"] != null)
                {
                    base.Tables.Add(new VotersDataTable(dataSet.Tables["Voters"]));
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
            VoterData data = (VoterData) base.Clone();
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
            base.DataSetName = "VoterData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/VoterData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableVoters = new VotersDataTable();
            base.Tables.Add(this.tableVoters);
        }

        internal void InitVars()
        {
            this.tableVoters = (VotersDataTable) base.Tables["Voters"];
            if (this.tableVoters != null)
            {
                this.tableVoters.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["Voters"] != null)
            {
                base.Tables.Add(new VotersDataTable(dataSet.Tables["Voters"]));
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

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        private bool ShouldSerializeVoters()
        {
            return false;
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
        public class VotersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnEmail;
            private DataColumn columnSurveyId;
            private DataColumn columnVoteDate;
            private DataColumn columnVoterId;

            public event VoterData.VotersRowChangeEventHandler VotersRowChanged;

            public event VoterData.VotersRowChangeEventHandler VotersRowChanging;

            public event VoterData.VotersRowChangeEventHandler VotersRowDeleted;

            public event VoterData.VotersRowChangeEventHandler VotersRowDeleting;

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

            public void AddVotersRow(VoterData.VotersRow row)
            {
                base.Rows.Add(row);
            }

            public VoterData.VotersRow AddVotersRow(int SurveyId, DateTime VoteDate, string Email)
            {
                VoterData.VotersRow row = (VoterData.VotersRow) base.NewRow();
                object[] objArray = new object[4];
                objArray[1] = SurveyId;
                objArray[2] = VoteDate;
                objArray[3] = Email;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                VoterData.VotersDataTable table = (VoterData.VotersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new VoterData.VotersDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(VoterData.VotersRow);
            }

            private void InitClass()
            {
                this.columnVoterId = new DataColumn("VoterId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoterId);
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnVoteDate = new DataColumn("VoteDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnVoteDate);
                this.columnEmail = new DataColumn("Email", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnEmail);
                this.columnVoterId.AutoIncrement = true;
                this.columnVoterId.AutoIncrementSeed = -1L;
                this.columnVoterId.AutoIncrementStep = -1L;
                this.columnVoterId.AllowDBNull = false;
                this.columnVoterId.ReadOnly = true;
            }

            internal void InitVars()
            {
                this.columnVoterId = base.Columns["VoterId"];
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnVoteDate = base.Columns["VoteDate"];
                this.columnEmail = base.Columns["Email"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new VoterData.VotersRow(builder);
            }

            public VoterData.VotersRow NewVotersRow()
            {
                return (VoterData.VotersRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.VotersRowChanged != null)
                {
                    this.VotersRowChanged(this, new VoterData.VotersRowChangeEvent((VoterData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.VotersRowChanging != null)
                {
                    this.VotersRowChanging(this, new VoterData.VotersRowChangeEvent((VoterData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.VotersRowDeleted != null)
                {
                    this.VotersRowDeleted(this, new VoterData.VotersRowChangeEvent((VoterData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.VotersRowDeleting != null)
                {
                    this.VotersRowDeleting(this, new VoterData.VotersRowChangeEvent((VoterData.VotersRow) e.Row, e.Action));
                }
            }

            public void RemoveVotersRow(VoterData.VotersRow row)
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

            public VoterData.VotersRow this[int index]
            {
                get
                {
                    return (VoterData.VotersRow) base.Rows[index];
                }
            }

            internal DataColumn SurveyIdColumn
            {
                get
                {
                    return this.columnSurveyId;
                }
            }

            internal DataColumn VoteDateColumn
            {
                get
                {
                    return this.columnVoteDate;
                }
            }

            internal DataColumn VoterIdColumn
            {
                get
                {
                    return this.columnVoterId;
                }
            }
        }

        [DebuggerStepThrough]
        public class VotersRow : DataRow
        {
            private VoterData.VotersDataTable tableVoters;

            internal VotersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableVoters = (VoterData.VotersDataTable) base.Table;
            }

            public bool IsEmailNull()
            {
                return base.IsNull(this.tableVoters.EmailColumn);
            }

            public bool IsSurveyIdNull()
            {
                return base.IsNull(this.tableVoters.SurveyIdColumn);
            }

            public bool IsVoteDateNull()
            {
                return base.IsNull(this.tableVoters.VoteDateColumn);
            }

            public void SetEmailNull()
            {
                base[this.tableVoters.EmailColumn] = Convert.DBNull;
            }

            public void SetSurveyIdNull()
            {
                base[this.tableVoters.SurveyIdColumn] = Convert.DBNull;
            }

            public void SetVoteDateNull()
            {
                base[this.tableVoters.VoteDateColumn] = Convert.DBNull;
            }

            public string Email
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableVoters.EmailColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableVoters.EmailColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableVoters.SurveyIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableVoters.SurveyIdColumn] = value;
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

            public int VoterId
            {
                get
                {
                    return (int) base[this.tableVoters.VoterIdColumn];
                }
                set
                {
                    base[this.tableVoters.VoterIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class VotersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private VoterData.VotersRow eventRow;

            public VotersRowChangeEvent(VoterData.VotersRow row, DataRowAction action)
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

            public VoterData.VotersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void VotersRowChangeEventHandler(object sender, VoterData.VotersRowChangeEvent e);
    }
}

