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

    [Serializable, DebuggerStepThrough, ToolboxItem(true), DesignerCategory("code")]
    public class UnAuthentifiedUserActionData : DataSet
    {
        private UnAuthentifiedUserActionsDataTable tableUnAuthentifiedUserActions;

        public UnAuthentifiedUserActionData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected UnAuthentifiedUserActionData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["UnAuthentifiedUserActions"] != null)
                {
                    base.Tables.Add(new UnAuthentifiedUserActionsDataTable(dataSet.Tables["UnAuthentifiedUserActions"]));
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
            UnAuthentifiedUserActionData data = (UnAuthentifiedUserActionData) base.Clone();
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
            base.DataSetName = "UnAuthentifiedUserActionData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/UnAuthentifiedUserActionData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableUnAuthentifiedUserActions = new UnAuthentifiedUserActionsDataTable();
            base.Tables.Add(this.tableUnAuthentifiedUserActions);
        }

        internal void InitVars()
        {
            this.tableUnAuthentifiedUserActions = (UnAuthentifiedUserActionsDataTable) base.Tables["UnAuthentifiedUserActions"];
            if (this.tableUnAuthentifiedUserActions != null)
            {
                this.tableUnAuthentifiedUserActions.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["UnAuthentifiedUserActions"] != null)
            {
                base.Tables.Add(new UnAuthentifiedUserActionsDataTable(dataSet.Tables["UnAuthentifiedUserActions"]));
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

        private bool ShouldSerializeUnAuthentifiedUserActions()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public UnAuthentifiedUserActionsDataTable UnAuthentifiedUserActions
        {
            get
            {
                return this.tableUnAuthentifiedUserActions;
            }
        }

        [DebuggerStepThrough]
        public class UnAuthentifiedUserActionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescripiton;
            private DataColumn columnUnAuthentifiedUserActionId;

            public event UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEventHandler UnAuthentifiedUserActionsRowChanged;

            public event UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEventHandler UnAuthentifiedUserActionsRowChanging;

            public event UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEventHandler UnAuthentifiedUserActionsRowDeleted;

            public event UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEventHandler UnAuthentifiedUserActionsRowDeleting;

            internal UnAuthentifiedUserActionsDataTable() : base("UnAuthentifiedUserActions")
            {
                this.InitClass();
            }

            internal UnAuthentifiedUserActionsDataTable(DataTable table) : base(table.TableName)
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

            public void AddUnAuthentifiedUserActionsRow(UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow row)
            {
                base.Rows.Add(row);
            }

            public UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow AddUnAuthentifiedUserActionsRow(int UnAuthentifiedUserActionId, string Descripiton)
            {
                UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow row = (UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow) base.NewRow();
                row.ItemArray = new object[] { UnAuthentifiedUserActionId, Descripiton };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                UnAuthentifiedUserActionData.UnAuthentifiedUserActionsDataTable table = (UnAuthentifiedUserActionData.UnAuthentifiedUserActionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new UnAuthentifiedUserActionData.UnAuthentifiedUserActionsDataTable();
            }

            public UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow FindByUnAuthentifiedUserActionId(int UnAuthentifiedUserActionId)
            {
                return (UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow) base.Rows.Find(new object[] { UnAuthentifiedUserActionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow);
            }

            private void InitClass()
            {
                this.columnUnAuthentifiedUserActionId = new DataColumn("UnAuthentifiedUserActionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnUnAuthentifiedUserActionId);
                this.columnDescripiton = new DataColumn("Descripiton", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescripiton);
                base.Constraints.Add(new UniqueConstraint("UnAuthentifiedUserActionDataKey1", new DataColumn[] { this.columnUnAuthentifiedUserActionId }, true));
                this.columnUnAuthentifiedUserActionId.AllowDBNull = false;
                this.columnUnAuthentifiedUserActionId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnUnAuthentifiedUserActionId = base.Columns["UnAuthentifiedUserActionId"];
                this.columnDescripiton = base.Columns["Descripiton"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow(builder);
            }

            public UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow NewUnAuthentifiedUserActionsRow()
            {
                return (UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.UnAuthentifiedUserActionsRowChanged != null)
                {
                    this.UnAuthentifiedUserActionsRowChanged(this, new UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEvent((UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.UnAuthentifiedUserActionsRowChanging != null)
                {
                    this.UnAuthentifiedUserActionsRowChanging(this, new UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEvent((UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.UnAuthentifiedUserActionsRowDeleted != null)
                {
                    this.UnAuthentifiedUserActionsRowDeleted(this, new UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEvent((UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.UnAuthentifiedUserActionsRowDeleting != null)
                {
                    this.UnAuthentifiedUserActionsRowDeleting(this, new UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEvent((UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow) e.Row, e.Action));
                }
            }

            public void RemoveUnAuthentifiedUserActionsRow(UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow row)
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

            internal DataColumn DescripitonColumn
            {
                get
                {
                    return this.columnDescripiton;
                }
            }

            public UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow this[int index]
            {
                get
                {
                    return (UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow) base.Rows[index];
                }
            }

            internal DataColumn UnAuthentifiedUserActionIdColumn
            {
                get
                {
                    return this.columnUnAuthentifiedUserActionId;
                }
            }
        }

        [DebuggerStepThrough]
        public class UnAuthentifiedUserActionsRow : DataRow
        {
            private UnAuthentifiedUserActionData.UnAuthentifiedUserActionsDataTable tableUnAuthentifiedUserActions;

            internal UnAuthentifiedUserActionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableUnAuthentifiedUserActions = (UnAuthentifiedUserActionData.UnAuthentifiedUserActionsDataTable) base.Table;
            }

            public bool IsDescripitonNull()
            {
                return base.IsNull(this.tableUnAuthentifiedUserActions.DescripitonColumn);
            }

            public void SetDescripitonNull()
            {
                base[this.tableUnAuthentifiedUserActions.DescripitonColumn] = Convert.DBNull;
            }

            public string Descripiton
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableUnAuthentifiedUserActions.DescripitonColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableUnAuthentifiedUserActions.DescripitonColumn] = value;
                }
            }

            public int UnAuthentifiedUserActionId
            {
                get
                {
                    return (int) base[this.tableUnAuthentifiedUserActions.UnAuthentifiedUserActionIdColumn];
                }
                set
                {
                    base[this.tableUnAuthentifiedUserActions.UnAuthentifiedUserActionIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class UnAuthentifiedUserActionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow eventRow;

            public UnAuthentifiedUserActionsRowChangeEvent(UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow row, DataRowAction action)
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

            public UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void UnAuthentifiedUserActionsRowChangeEventHandler(object sender, UnAuthentifiedUserActionData.UnAuthentifiedUserActionsRowChangeEvent e);
    }
}

