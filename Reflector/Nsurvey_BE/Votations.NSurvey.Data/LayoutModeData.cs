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
    public class LayoutModeData : DataSet
    {
        private LayoutModesDataTable tableLayoutModes;

        public LayoutModeData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected LayoutModeData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["LayoutModes"] != null)
                {
                    base.Tables.Add(new LayoutModesDataTable(dataSet.Tables["LayoutModes"]));
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
            LayoutModeData data = (LayoutModeData) base.Clone();
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
            base.DataSetName = "LayoutModeData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/QuestionLayoutData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableLayoutModes = new LayoutModesDataTable();
            base.Tables.Add(this.tableLayoutModes);
        }

        internal void InitVars()
        {
            this.tableLayoutModes = (LayoutModesDataTable) base.Tables["LayoutModes"];
            if (this.tableLayoutModes != null)
            {
                this.tableLayoutModes.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["LayoutModes"] != null)
            {
                base.Tables.Add(new LayoutModesDataTable(dataSet.Tables["LayoutModes"]));
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

        private bool ShouldSerializeLayoutModes()
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
        public LayoutModesDataTable LayoutModes
        {
            get
            {
                return this.tableLayoutModes;
            }
        }

        [DebuggerStepThrough]
        public class LayoutModesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescription;
            private DataColumn columnLayoutModeID;

            public event LayoutModeData.LayoutModesRowChangeEventHandler LayoutModesRowChanged;

            public event LayoutModeData.LayoutModesRowChangeEventHandler LayoutModesRowChanging;

            public event LayoutModeData.LayoutModesRowChangeEventHandler LayoutModesRowDeleted;

            public event LayoutModeData.LayoutModesRowChangeEventHandler LayoutModesRowDeleting;

            internal LayoutModesDataTable() : base("LayoutModes")
            {
                this.InitClass();
            }

            internal LayoutModesDataTable(DataTable table) : base(table.TableName)
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

            public void AddLayoutModesRow(LayoutModeData.LayoutModesRow row)
            {
                base.Rows.Add(row);
            }

            public LayoutModeData.LayoutModesRow AddLayoutModesRow(int LayoutModeID, string Description)
            {
                LayoutModeData.LayoutModesRow row = (LayoutModeData.LayoutModesRow) base.NewRow();
                row.ItemArray = new object[] { LayoutModeID, Description };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                LayoutModeData.LayoutModesDataTable table = (LayoutModeData.LayoutModesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new LayoutModeData.LayoutModesDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(LayoutModeData.LayoutModesRow);
            }

            private void InitClass()
            {
                this.columnLayoutModeID = new DataColumn("LayoutModeID", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnLayoutModeID);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnLayoutModeID.AutoIncrementSeed = -1L;
                this.columnLayoutModeID.AutoIncrementStep = -1L;
                this.columnLayoutModeID.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnLayoutModeID = base.Columns["LayoutModeID"];
                this.columnDescription = base.Columns["Description"];
            }

            public LayoutModeData.LayoutModesRow NewLayoutModesRow()
            {
                return (LayoutModeData.LayoutModesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new LayoutModeData.LayoutModesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.LayoutModesRowChanged != null)
                {
                    this.LayoutModesRowChanged(this, new LayoutModeData.LayoutModesRowChangeEvent((LayoutModeData.LayoutModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.LayoutModesRowChanging != null)
                {
                    this.LayoutModesRowChanging(this, new LayoutModeData.LayoutModesRowChangeEvent((LayoutModeData.LayoutModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.LayoutModesRowDeleted != null)
                {
                    this.LayoutModesRowDeleted(this, new LayoutModeData.LayoutModesRowChangeEvent((LayoutModeData.LayoutModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.LayoutModesRowDeleting != null)
                {
                    this.LayoutModesRowDeleting(this, new LayoutModeData.LayoutModesRowChangeEvent((LayoutModeData.LayoutModesRow) e.Row, e.Action));
                }
            }

            public void RemoveLayoutModesRow(LayoutModeData.LayoutModesRow row)
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

            public LayoutModeData.LayoutModesRow this[int index]
            {
                get
                {
                    return (LayoutModeData.LayoutModesRow) base.Rows[index];
                }
            }

            internal DataColumn LayoutModeIDColumn
            {
                get
                {
                    return this.columnLayoutModeID;
                }
            }
        }

        [DebuggerStepThrough]
        public class LayoutModesRow : DataRow
        {
            private LayoutModeData.LayoutModesDataTable tableLayoutModes;

            internal LayoutModesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableLayoutModes = (LayoutModeData.LayoutModesDataTable) base.Table;
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableLayoutModes.DescriptionColumn);
            }

            public void SetDescriptionNull()
            {
                base[this.tableLayoutModes.DescriptionColumn] = Convert.DBNull;
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableLayoutModes.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableLayoutModes.DescriptionColumn] = value;
                }
            }

            public int LayoutModeID
            {
                get
                {
                    return (int) base[this.tableLayoutModes.LayoutModeIDColumn];
                }
                set
                {
                    base[this.tableLayoutModes.LayoutModeIDColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class LayoutModesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private LayoutModeData.LayoutModesRow eventRow;

            public LayoutModesRowChangeEvent(LayoutModeData.LayoutModesRow row, DataRowAction action)
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

            public LayoutModeData.LayoutModesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void LayoutModesRowChangeEventHandler(object sender, LayoutModeData.LayoutModesRowChangeEvent e);
    }
}

