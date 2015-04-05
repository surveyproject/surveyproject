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
    public class ResumeModeData : DataSet
    {
        private ResumeModesDataTable tableResumeModes;

        public ResumeModeData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected ResumeModeData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["ResumeModes"] != null)
                {
                    base.Tables.Add(new ResumeModesDataTable(dataSet.Tables["ResumeModes"]));
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
            ResumeModeData data = (ResumeModeData) base.Clone();
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
            base.DataSetName = "ResumeModeData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/ResumeModeData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableResumeModes = new ResumeModesDataTable();
            base.Tables.Add(this.tableResumeModes);
        }

        internal void InitVars()
        {
            this.tableResumeModes = (ResumeModesDataTable) base.Tables["ResumeModes"];
            if (this.tableResumeModes != null)
            {
                this.tableResumeModes.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["ResumeModes"] != null)
            {
                base.Tables.Add(new ResumeModesDataTable(dataSet.Tables["ResumeModes"]));
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

        private bool ShouldSerializeResumeModes()
        {
            return false;
        }

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public ResumeModesDataTable ResumeModes
        {
            get
            {
                return this.tableResumeModes;
            }
        }

        [DebuggerStepThrough]
        public class ResumeModesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescription;
            private DataColumn columnResumeModeID;

            public event ResumeModeData.ResumeModesRowChangeEventHandler ResumeModesRowChanged;

            public event ResumeModeData.ResumeModesRowChangeEventHandler ResumeModesRowChanging;

            public event ResumeModeData.ResumeModesRowChangeEventHandler ResumeModesRowDeleted;

            public event ResumeModeData.ResumeModesRowChangeEventHandler ResumeModesRowDeleting;

            internal ResumeModesDataTable() : base("ResumeModes")
            {
                this.InitClass();
            }

            internal ResumeModesDataTable(DataTable table) : base(table.TableName)
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

            public void AddResumeModesRow(ResumeModeData.ResumeModesRow row)
            {
                base.Rows.Add(row);
            }

            public ResumeModeData.ResumeModesRow AddResumeModesRow(byte ResumeModeID, string Description)
            {
                ResumeModeData.ResumeModesRow row = (ResumeModeData.ResumeModesRow) base.NewRow();
                row.ItemArray = new object[] { ResumeModeID, Description };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                ResumeModeData.ResumeModesDataTable table = (ResumeModeData.ResumeModesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new ResumeModeData.ResumeModesDataTable();
            }

            public ResumeModeData.ResumeModesRow FindByResumeModeID(byte ResumeModeID)
            {
                return (ResumeModeData.ResumeModesRow) base.Rows.Find(new object[] { ResumeModeID });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(ResumeModeData.ResumeModesRow);
            }

            private void InitClass()
            {
                this.columnResumeModeID = new DataColumn("ResumeModeID", typeof(byte), null, MappingType.Element);
                base.Columns.Add(this.columnResumeModeID);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                base.Constraints.Add(new UniqueConstraint("ResumeModeDataKey1", new DataColumn[] { this.columnResumeModeID }, true));
                this.columnResumeModeID.AllowDBNull = false;
                this.columnResumeModeID.Unique = true;
            }

            internal void InitVars()
            {
                this.columnResumeModeID = base.Columns["ResumeModeID"];
                this.columnDescription = base.Columns["Description"];
            }

            public ResumeModeData.ResumeModesRow NewResumeModesRow()
            {
                return (ResumeModeData.ResumeModesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new ResumeModeData.ResumeModesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.ResumeModesRowChanged != null)
                {
                    this.ResumeModesRowChanged(this, new ResumeModeData.ResumeModesRowChangeEvent((ResumeModeData.ResumeModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.ResumeModesRowChanging != null)
                {
                    this.ResumeModesRowChanging(this, new ResumeModeData.ResumeModesRowChangeEvent((ResumeModeData.ResumeModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.ResumeModesRowDeleted != null)
                {
                    this.ResumeModesRowDeleted(this, new ResumeModeData.ResumeModesRowChangeEvent((ResumeModeData.ResumeModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.ResumeModesRowDeleting != null)
                {
                    this.ResumeModesRowDeleting(this, new ResumeModeData.ResumeModesRowChangeEvent((ResumeModeData.ResumeModesRow) e.Row, e.Action));
                }
            }

            public void RemoveResumeModesRow(ResumeModeData.ResumeModesRow row)
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

            public ResumeModeData.ResumeModesRow this[int index]
            {
                get
                {
                    return (ResumeModeData.ResumeModesRow) base.Rows[index];
                }
            }

            internal DataColumn ResumeModeIDColumn
            {
                get
                {
                    return this.columnResumeModeID;
                }
            }
        }

        [DebuggerStepThrough]
        public class ResumeModesRow : DataRow
        {
            private ResumeModeData.ResumeModesDataTable tableResumeModes;

            internal ResumeModesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableResumeModes = (ResumeModeData.ResumeModesDataTable) base.Table;
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableResumeModes.DescriptionColumn);
            }

            public void SetDescriptionNull()
            {
                base[this.tableResumeModes.DescriptionColumn] = Convert.DBNull;
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableResumeModes.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableResumeModes.DescriptionColumn] = value;
                }
            }

            public byte ResumeModeID
            {
                get
                {
                    return (byte) base[this.tableResumeModes.ResumeModeIDColumn];
                }
                set
                {
                    base[this.tableResumeModes.ResumeModeIDColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class ResumeModesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private ResumeModeData.ResumeModesRow eventRow;

            public ResumeModesRowChangeEvent(ResumeModeData.ResumeModesRow row, DataRowAction action)
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

            public ResumeModeData.ResumeModesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void ResumeModesRowChangeEventHandler(object sender, ResumeModeData.ResumeModesRowChangeEvent e);
    }
}

