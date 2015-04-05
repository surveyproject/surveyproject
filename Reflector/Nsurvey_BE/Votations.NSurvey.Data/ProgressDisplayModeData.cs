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

    [Serializable, ToolboxItem(true), DesignerCategory("code"), DebuggerStepThrough]
    public class ProgressModeData : DataSet
    {
        private ProgressDisplayModesDataTable tableProgressDisplayModes;

        public ProgressModeData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected ProgressModeData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["ProgressDisplayModes"] != null)
                {
                    base.Tables.Add(new ProgressDisplayModesDataTable(dataSet.Tables["ProgressDisplayModes"]));
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
            ProgressModeData data = (ProgressModeData) base.Clone();
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
            base.DataSetName = "ProgressModeData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/ProgressModeData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableProgressDisplayModes = new ProgressDisplayModesDataTable();
            base.Tables.Add(this.tableProgressDisplayModes);
        }

        internal void InitVars()
        {
            this.tableProgressDisplayModes = (ProgressDisplayModesDataTable) base.Tables["ProgressDisplayModes"];
            if (this.tableProgressDisplayModes != null)
            {
                this.tableProgressDisplayModes.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["ProgressDisplayModes"] != null)
            {
                base.Tables.Add(new ProgressDisplayModesDataTable(dataSet.Tables["ProgressDisplayModes"]));
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

        private bool ShouldSerializeProgressDisplayModes()
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
        public ProgressDisplayModesDataTable ProgressDisplayModes
        {
            get
            {
                return this.tableProgressDisplayModes;
            }
        }

        [DebuggerStepThrough]
        public class ProgressDisplayModesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescription;
            private DataColumn columnProgressDisplayModeId;

            public event ProgressModeData.ProgressDisplayModesRowChangeEventHandler ProgressDisplayModesRowChanged;

            public event ProgressModeData.ProgressDisplayModesRowChangeEventHandler ProgressDisplayModesRowChanging;

            public event ProgressModeData.ProgressDisplayModesRowChangeEventHandler ProgressDisplayModesRowDeleted;

            public event ProgressModeData.ProgressDisplayModesRowChangeEventHandler ProgressDisplayModesRowDeleting;

            internal ProgressDisplayModesDataTable() : base("ProgressDisplayModes")
            {
                this.InitClass();
            }

            internal ProgressDisplayModesDataTable(DataTable table) : base(table.TableName)
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

            public void AddProgressDisplayModesRow(ProgressModeData.ProgressDisplayModesRow row)
            {
                base.Rows.Add(row);
            }

            public ProgressModeData.ProgressDisplayModesRow AddProgressDisplayModesRow(int ProgressDisplayModeId, string Description)
            {
                ProgressModeData.ProgressDisplayModesRow row = (ProgressModeData.ProgressDisplayModesRow) base.NewRow();
                row.ItemArray = new object[] { ProgressDisplayModeId, Description };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                ProgressModeData.ProgressDisplayModesDataTable table = (ProgressModeData.ProgressDisplayModesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new ProgressModeData.ProgressDisplayModesDataTable();
            }

            public ProgressModeData.ProgressDisplayModesRow FindByProgressDisplayModeId(int ProgressDisplayModeId)
            {
                return (ProgressModeData.ProgressDisplayModesRow) base.Rows.Find(new object[] { ProgressDisplayModeId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(ProgressModeData.ProgressDisplayModesRow);
            }

            private void InitClass()
            {
                this.columnProgressDisplayModeId = new DataColumn("ProgressDisplayModeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnProgressDisplayModeId);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                base.Constraints.Add(new UniqueConstraint("ProgressModeDataKey1", new DataColumn[] { this.columnProgressDisplayModeId }, true));
                this.columnProgressDisplayModeId.AllowDBNull = false;
                this.columnProgressDisplayModeId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnProgressDisplayModeId = base.Columns["ProgressDisplayModeId"];
                this.columnDescription = base.Columns["Description"];
            }

            public ProgressModeData.ProgressDisplayModesRow NewProgressDisplayModesRow()
            {
                return (ProgressModeData.ProgressDisplayModesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new ProgressModeData.ProgressDisplayModesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.ProgressDisplayModesRowChanged != null)
                {
                    this.ProgressDisplayModesRowChanged(this, new ProgressModeData.ProgressDisplayModesRowChangeEvent((ProgressModeData.ProgressDisplayModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.ProgressDisplayModesRowChanging != null)
                {
                    this.ProgressDisplayModesRowChanging(this, new ProgressModeData.ProgressDisplayModesRowChangeEvent((ProgressModeData.ProgressDisplayModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.ProgressDisplayModesRowDeleted != null)
                {
                    this.ProgressDisplayModesRowDeleted(this, new ProgressModeData.ProgressDisplayModesRowChangeEvent((ProgressModeData.ProgressDisplayModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.ProgressDisplayModesRowDeleting != null)
                {
                    this.ProgressDisplayModesRowDeleting(this, new ProgressModeData.ProgressDisplayModesRowChangeEvent((ProgressModeData.ProgressDisplayModesRow) e.Row, e.Action));
                }
            }

            public void RemoveProgressDisplayModesRow(ProgressModeData.ProgressDisplayModesRow row)
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

            internal DataColumn DescriptionColumn
            {
                get
                {
                    return this.columnDescription;
                }
            }

            public ProgressModeData.ProgressDisplayModesRow this[int index]
            {
                get
                {
                    return (ProgressModeData.ProgressDisplayModesRow) base.Rows[index];
                }
            }

            internal DataColumn ProgressDisplayModeIdColumn
            {
                get
                {
                    return this.columnProgressDisplayModeId;
                }
            }
        }

        [DebuggerStepThrough]
        public class ProgressDisplayModesRow : DataRow
        {
            private ProgressModeData.ProgressDisplayModesDataTable tableProgressDisplayModes;

            internal ProgressDisplayModesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableProgressDisplayModes = (ProgressModeData.ProgressDisplayModesDataTable) base.Table;
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableProgressDisplayModes.DescriptionColumn);
            }

            public void SetDescriptionNull()
            {
                base[this.tableProgressDisplayModes.DescriptionColumn] = Convert.DBNull;
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableProgressDisplayModes.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableProgressDisplayModes.DescriptionColumn] = value;
                }
            }

            public int ProgressDisplayModeId
            {
                get
                {
                    return (int) base[this.tableProgressDisplayModes.ProgressDisplayModeIdColumn];
                }
                set
                {
                    base[this.tableProgressDisplayModes.ProgressDisplayModeIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class ProgressDisplayModesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private ProgressModeData.ProgressDisplayModesRow eventRow;

            public ProgressDisplayModesRowChangeEvent(ProgressModeData.ProgressDisplayModesRow row, DataRowAction action)
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

            public ProgressModeData.ProgressDisplayModesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void ProgressDisplayModesRowChangeEventHandler(object sender, ProgressModeData.ProgressDisplayModesRowChangeEvent e);
    }
}

