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
    public class NotificationModeData : DataSet
    {
        private NotificationModesDataTable tableNotificationModes;

        public NotificationModeData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected NotificationModeData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["NotificationModes"] != null)
                {
                    base.Tables.Add(new NotificationModesDataTable(dataSet.Tables["NotificationModes"]));
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
            NotificationModeData data = (NotificationModeData) base.Clone();
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
            base.DataSetName = "NotificationModeData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/NotificationModeData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableNotificationModes = new NotificationModesDataTable();
            base.Tables.Add(this.tableNotificationModes);
        }

        internal void InitVars()
        {
            this.tableNotificationModes = (NotificationModesDataTable) base.Tables["NotificationModes"];
            if (this.tableNotificationModes != null)
            {
                this.tableNotificationModes.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["NotificationModes"] != null)
            {
                base.Tables.Add(new NotificationModesDataTable(dataSet.Tables["NotificationModes"]));
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

        private bool ShouldSerializeNotificationModes()
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
        public NotificationModesDataTable NotificationModes
        {
            get
            {
                return this.tableNotificationModes;
            }
        }

        [DebuggerStepThrough]
        public class NotificationModesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescription;
            private DataColumn columnNotificationModeID;

            public event NotificationModeData.NotificationModesRowChangeEventHandler NotificationModesRowChanged;

            public event NotificationModeData.NotificationModesRowChangeEventHandler NotificationModesRowChanging;

            public event NotificationModeData.NotificationModesRowChangeEventHandler NotificationModesRowDeleted;

            public event NotificationModeData.NotificationModesRowChangeEventHandler NotificationModesRowDeleting;

            internal NotificationModesDataTable() : base("NotificationModes")
            {
                this.InitClass();
            }

            internal NotificationModesDataTable(DataTable table) : base(table.TableName)
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

            public void AddNotificationModesRow(NotificationModeData.NotificationModesRow row)
            {
                base.Rows.Add(row);
            }

            public NotificationModeData.NotificationModesRow AddNotificationModesRow(int NotificationModeID, string Description)
            {
                NotificationModeData.NotificationModesRow row = (NotificationModeData.NotificationModesRow) base.NewRow();
                row.ItemArray = new object[] { NotificationModeID, Description };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NotificationModeData.NotificationModesDataTable table = (NotificationModeData.NotificationModesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NotificationModeData.NotificationModesDataTable();
            }

            public NotificationModeData.NotificationModesRow FindByNotificationModeID(int NotificationModeID)
            {
                return (NotificationModeData.NotificationModesRow) base.Rows.Find(new object[] { NotificationModeID });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NotificationModeData.NotificationModesRow);
            }

            private void InitClass()
            {
                this.columnNotificationModeID = new DataColumn("NotificationModeID", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnNotificationModeID);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                base.Constraints.Add(new UniqueConstraint("NotificationModeDataKey1", new DataColumn[] { this.columnNotificationModeID }, true));
                this.columnNotificationModeID.AllowDBNull = false;
                this.columnNotificationModeID.Unique = true;
            }

            internal void InitVars()
            {
                this.columnNotificationModeID = base.Columns["NotificationModeID"];
                this.columnDescription = base.Columns["Description"];
            }

            public NotificationModeData.NotificationModesRow NewNotificationModesRow()
            {
                return (NotificationModeData.NotificationModesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NotificationModeData.NotificationModesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.NotificationModesRowChanged != null)
                {
                    this.NotificationModesRowChanged(this, new NotificationModeData.NotificationModesRowChangeEvent((NotificationModeData.NotificationModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.NotificationModesRowChanging != null)
                {
                    this.NotificationModesRowChanging(this, new NotificationModeData.NotificationModesRowChangeEvent((NotificationModeData.NotificationModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.NotificationModesRowDeleted != null)
                {
                    this.NotificationModesRowDeleted(this, new NotificationModeData.NotificationModesRowChangeEvent((NotificationModeData.NotificationModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.NotificationModesRowDeleting != null)
                {
                    this.NotificationModesRowDeleting(this, new NotificationModeData.NotificationModesRowChangeEvent((NotificationModeData.NotificationModesRow) e.Row, e.Action));
                }
            }

            public void RemoveNotificationModesRow(NotificationModeData.NotificationModesRow row)
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

            public NotificationModeData.NotificationModesRow this[int index]
            {
                get
                {
                    return (NotificationModeData.NotificationModesRow) base.Rows[index];
                }
            }

            internal DataColumn NotificationModeIDColumn
            {
                get
                {
                    return this.columnNotificationModeID;
                }
            }
        }

        [DebuggerStepThrough]
        public class NotificationModesRow : DataRow
        {
            private NotificationModeData.NotificationModesDataTable tableNotificationModes;

            internal NotificationModesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableNotificationModes = (NotificationModeData.NotificationModesDataTable) base.Table;
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableNotificationModes.DescriptionColumn);
            }

            public void SetDescriptionNull()
            {
                base[this.tableNotificationModes.DescriptionColumn] = Convert.DBNull;
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableNotificationModes.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableNotificationModes.DescriptionColumn] = value;
                }
            }

            public int NotificationModeID
            {
                get
                {
                    return (int) base[this.tableNotificationModes.NotificationModeIDColumn];
                }
                set
                {
                    base[this.tableNotificationModes.NotificationModeIDColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class NotificationModesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NotificationModeData.NotificationModesRow eventRow;

            public NotificationModesRowChangeEvent(NotificationModeData.NotificationModesRow row, DataRowAction action)
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

            public NotificationModeData.NotificationModesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void NotificationModesRowChangeEventHandler(object sender, NotificationModeData.NotificationModesRowChangeEvent e);
    }
}

