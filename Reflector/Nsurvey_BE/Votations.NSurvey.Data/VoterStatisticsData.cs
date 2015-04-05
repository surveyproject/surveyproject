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
    public class VoterStatisticsData : DataSet
    {
        private VoterStatsDataTable tableVoterStats;

        public VoterStatisticsData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected VoterStatisticsData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["VoterStats"] != null)
                {
                    base.Tables.Add(new VoterStatsDataTable(dataSet.Tables["VoterStats"]));
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
            VoterStatisticsData data = (VoterStatisticsData) base.Clone();
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
            base.DataSetName = "VoterStatisticsData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/VoterStatisticsData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableVoterStats = new VoterStatsDataTable();
            base.Tables.Add(this.tableVoterStats);
        }

        internal void InitVars()
        {
            this.tableVoterStats = (VoterStatsDataTable) base.Tables["VoterStats"];
            if (this.tableVoterStats != null)
            {
                this.tableVoterStats.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["VoterStats"] != null)
            {
                base.Tables.Add(new VoterStatsDataTable(dataSet.Tables["VoterStats"]));
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

        private bool ShouldSerializeVoterStats()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public VoterStatsDataTable VoterStats
        {
            get
            {
                return this.tableVoterStats;
            }
        }

        [DebuggerStepThrough]
        public class VoterStatsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnVoterCount;
            private DataColumn columnVotesDate;

            public event VoterStatisticsData.VoterStatsRowChangeEventHandler VoterStatsRowChanged;

            public event VoterStatisticsData.VoterStatsRowChangeEventHandler VoterStatsRowChanging;

            public event VoterStatisticsData.VoterStatsRowChangeEventHandler VoterStatsRowDeleted;

            public event VoterStatisticsData.VoterStatsRowChangeEventHandler VoterStatsRowDeleting;

            internal VoterStatsDataTable() : base("VoterStats")
            {
                this.InitClass();
            }

            internal VoterStatsDataTable(DataTable table) : base(table.TableName)
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

            public void AddVoterStatsRow(VoterStatisticsData.VoterStatsRow row)
            {
                base.Rows.Add(row);
            }

            public VoterStatisticsData.VoterStatsRow AddVoterStatsRow(DateTime VotesDate, int VoterCount)
            {
                VoterStatisticsData.VoterStatsRow row = (VoterStatisticsData.VoterStatsRow) base.NewRow();
                row.ItemArray = new object[] { VotesDate, VoterCount };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                VoterStatisticsData.VoterStatsDataTable table = (VoterStatisticsData.VoterStatsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new VoterStatisticsData.VoterStatsDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(VoterStatisticsData.VoterStatsRow);
            }

            private void InitClass()
            {
                this.columnVotesDate = new DataColumn("VotesDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnVotesDate);
                this.columnVoterCount = new DataColumn("VoterCount", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoterCount);
            }

            internal void InitVars()
            {
                this.columnVotesDate = base.Columns["VotesDate"];
                this.columnVoterCount = base.Columns["VoterCount"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new VoterStatisticsData.VoterStatsRow(builder);
            }

            public VoterStatisticsData.VoterStatsRow NewVoterStatsRow()
            {
                return (VoterStatisticsData.VoterStatsRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.VoterStatsRowChanged != null)
                {
                    this.VoterStatsRowChanged(this, new VoterStatisticsData.VoterStatsRowChangeEvent((VoterStatisticsData.VoterStatsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.VoterStatsRowChanging != null)
                {
                    this.VoterStatsRowChanging(this, new VoterStatisticsData.VoterStatsRowChangeEvent((VoterStatisticsData.VoterStatsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.VoterStatsRowDeleted != null)
                {
                    this.VoterStatsRowDeleted(this, new VoterStatisticsData.VoterStatsRowChangeEvent((VoterStatisticsData.VoterStatsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.VoterStatsRowDeleting != null)
                {
                    this.VoterStatsRowDeleting(this, new VoterStatisticsData.VoterStatsRowChangeEvent((VoterStatisticsData.VoterStatsRow) e.Row, e.Action));
                }
            }

            public void RemoveVoterStatsRow(VoterStatisticsData.VoterStatsRow row)
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

            public VoterStatisticsData.VoterStatsRow this[int index]
            {
                get
                {
                    return (VoterStatisticsData.VoterStatsRow) base.Rows[index];
                }
            }

            internal DataColumn VoterCountColumn
            {
                get
                {
                    return this.columnVoterCount;
                }
            }

            internal DataColumn VotesDateColumn
            {
                get
                {
                    return this.columnVotesDate;
                }
            }
        }

        [DebuggerStepThrough]
        public class VoterStatsRow : DataRow
        {
            private VoterStatisticsData.VoterStatsDataTable tableVoterStats;

            internal VoterStatsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableVoterStats = (VoterStatisticsData.VoterStatsDataTable) base.Table;
            }

            public bool IsVoterCountNull()
            {
                return base.IsNull(this.tableVoterStats.VoterCountColumn);
            }

            public bool IsVotesDateNull()
            {
                return base.IsNull(this.tableVoterStats.VotesDateColumn);
            }

            public void SetVoterCountNull()
            {
                base[this.tableVoterStats.VoterCountColumn] = Convert.DBNull;
            }

            public void SetVotesDateNull()
            {
                base[this.tableVoterStats.VotesDateColumn] = Convert.DBNull;
            }

            public int VoterCount
            {
                get
                {
                    if (this.IsVoterCountNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableVoterStats.VoterCountColumn];
                }
                set
                {
                    base[this.tableVoterStats.VoterCountColumn] = value;
                }
            }

            public DateTime VotesDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableVoterStats.VotesDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableVoterStats.VotesDateColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class VoterStatsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private VoterStatisticsData.VoterStatsRow eventRow;

            public VoterStatsRowChangeEvent(VoterStatisticsData.VoterStatsRow row, DataRowAction action)
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

            public VoterStatisticsData.VoterStatsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void VoterStatsRowChangeEventHandler(object sender, VoterStatisticsData.VoterStatsRowChangeEvent e);
    }
}

