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
    public class RegularExpressionData : DataSet
    {
        private RegularExpressionsDataTable tableRegularExpressions;

        public RegularExpressionData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected RegularExpressionData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["RegularExpressions"] != null)
                {
                    base.Tables.Add(new RegularExpressionsDataTable(dataSet.Tables["RegularExpressions"]));
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
            RegularExpressionData data = (RegularExpressionData) base.Clone();
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
            base.DataSetName = "RegularExpressionData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/RegularExpressionData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableRegularExpressions = new RegularExpressionsDataTable();
            base.Tables.Add(this.tableRegularExpressions);
        }

        internal void InitVars()
        {
            this.tableRegularExpressions = (RegularExpressionsDataTable) base.Tables["RegularExpressions"];
            if (this.tableRegularExpressions != null)
            {
                this.tableRegularExpressions.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["RegularExpressions"] != null)
            {
                base.Tables.Add(new RegularExpressionsDataTable(dataSet.Tables["RegularExpressions"]));
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

        private bool ShouldSerializeRegularExpressions()
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
        public RegularExpressionsDataTable RegularExpressions
        {
            get
            {
                return this.tableRegularExpressions;
            }
        }

        [DebuggerStepThrough]
        public class RegularExpressionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnBuiltIn;
            private DataColumn columnDescription;
            private DataColumn columnRegExMessage;
            private DataColumn columnRegExpression;
            private DataColumn columnRegularExpressionId;

            public event RegularExpressionData.RegularExpressionsRowChangeEventHandler RegularExpressionsRowChanged;

            public event RegularExpressionData.RegularExpressionsRowChangeEventHandler RegularExpressionsRowChanging;

            public event RegularExpressionData.RegularExpressionsRowChangeEventHandler RegularExpressionsRowDeleted;

            public event RegularExpressionData.RegularExpressionsRowChangeEventHandler RegularExpressionsRowDeleting;

            internal RegularExpressionsDataTable() : base("RegularExpressions")
            {
                this.InitClass();
            }

            internal RegularExpressionsDataTable(DataTable table) : base(table.TableName)
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

            public void AddRegularExpressionsRow(RegularExpressionData.RegularExpressionsRow row)
            {
                base.Rows.Add(row);
            }

            public RegularExpressionData.RegularExpressionsRow AddRegularExpressionsRow(string RegExpression, string RegExMessage, bool BuiltIn, string Description)
            {
                RegularExpressionData.RegularExpressionsRow row = (RegularExpressionData.RegularExpressionsRow) base.NewRow();
                object[] objArray = new object[5];
                objArray[1] = RegExpression;
                objArray[2] = RegExMessage;
                objArray[3] = BuiltIn;
                objArray[4] = Description;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                RegularExpressionData.RegularExpressionsDataTable table = (RegularExpressionData.RegularExpressionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new RegularExpressionData.RegularExpressionsDataTable();
            }

            public RegularExpressionData.RegularExpressionsRow FindByRegularExpressionId(int RegularExpressionId)
            {
                return (RegularExpressionData.RegularExpressionsRow) base.Rows.Find(new object[] { RegularExpressionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(RegularExpressionData.RegularExpressionsRow);
            }

            private void InitClass()
            {
                this.columnRegularExpressionId = new DataColumn("RegularExpressionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnRegularExpressionId);
                this.columnRegExpression = new DataColumn("RegExpression", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnRegExpression);
                this.columnRegExMessage = new DataColumn("RegExMessage", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnRegExMessage);
                this.columnBuiltIn = new DataColumn("BuiltIn", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnBuiltIn);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                base.Constraints.Add(new UniqueConstraint("RegularExpressionDataKey1", new DataColumn[] { this.columnRegularExpressionId }, true));
                this.columnRegularExpressionId.AutoIncrement = true;
                this.columnRegularExpressionId.AllowDBNull = false;
                this.columnRegularExpressionId.ReadOnly = true;
                this.columnRegularExpressionId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnRegularExpressionId = base.Columns["RegularExpressionId"];
                this.columnRegExpression = base.Columns["RegExpression"];
                this.columnRegExMessage = base.Columns["RegExMessage"];
                this.columnBuiltIn = base.Columns["BuiltIn"];
                this.columnDescription = base.Columns["Description"];
            }

            public RegularExpressionData.RegularExpressionsRow NewRegularExpressionsRow()
            {
                return (RegularExpressionData.RegularExpressionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new RegularExpressionData.RegularExpressionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.RegularExpressionsRowChanged != null)
                {
                    this.RegularExpressionsRowChanged(this, new RegularExpressionData.RegularExpressionsRowChangeEvent((RegularExpressionData.RegularExpressionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.RegularExpressionsRowChanging != null)
                {
                    this.RegularExpressionsRowChanging(this, new RegularExpressionData.RegularExpressionsRowChangeEvent((RegularExpressionData.RegularExpressionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.RegularExpressionsRowDeleted != null)
                {
                    this.RegularExpressionsRowDeleted(this, new RegularExpressionData.RegularExpressionsRowChangeEvent((RegularExpressionData.RegularExpressionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.RegularExpressionsRowDeleting != null)
                {
                    this.RegularExpressionsRowDeleting(this, new RegularExpressionData.RegularExpressionsRowChangeEvent((RegularExpressionData.RegularExpressionsRow) e.Row, e.Action));
                }
            }

            public void RemoveRegularExpressionsRow(RegularExpressionData.RegularExpressionsRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn BuiltInColumn
            {
                get
                {
                    return this.columnBuiltIn;
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

            internal DataColumn DescriptionColumn
            {
                get
                {
                    return this.columnDescription;
                }
            }

            public RegularExpressionData.RegularExpressionsRow this[int index]
            {
                get
                {
                    return (RegularExpressionData.RegularExpressionsRow) base.Rows[index];
                }
            }

            internal DataColumn RegExMessageColumn
            {
                get
                {
                    return this.columnRegExMessage;
                }
            }

            internal DataColumn RegExpressionColumn
            {
                get
                {
                    return this.columnRegExpression;
                }
            }

            internal DataColumn RegularExpressionIdColumn
            {
                get
                {
                    return this.columnRegularExpressionId;
                }
            }
        }

        [DebuggerStepThrough]
        public class RegularExpressionsRow : DataRow
        {
            private RegularExpressionData.RegularExpressionsDataTable tableRegularExpressions;

            internal RegularExpressionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableRegularExpressions = (RegularExpressionData.RegularExpressionsDataTable) base.Table;
            }

            public bool IsBuiltInNull()
            {
                return base.IsNull(this.tableRegularExpressions.BuiltInColumn);
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableRegularExpressions.DescriptionColumn);
            }

            public bool IsRegExMessageNull()
            {
                return base.IsNull(this.tableRegularExpressions.RegExMessageColumn);
            }

            public bool IsRegExpressionNull()
            {
                return base.IsNull(this.tableRegularExpressions.RegExpressionColumn);
            }

            public void SetBuiltInNull()
            {
                base[this.tableRegularExpressions.BuiltInColumn] = Convert.DBNull;
            }

            public void SetDescriptionNull()
            {
                base[this.tableRegularExpressions.DescriptionColumn] = Convert.DBNull;
            }

            public void SetRegExMessageNull()
            {
                base[this.tableRegularExpressions.RegExMessageColumn] = Convert.DBNull;
            }

            public void SetRegExpressionNull()
            {
                base[this.tableRegularExpressions.RegExpressionColumn] = Convert.DBNull;
            }

            public bool BuiltIn
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableRegularExpressions.BuiltInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableRegularExpressions.BuiltInColumn] = value;
                }
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableRegularExpressions.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableRegularExpressions.DescriptionColumn] = value;
                }
            }

            public string RegExMessage
            {
                get
                {
                    if (this.IsRegExMessageNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableRegularExpressions.RegExMessageColumn];
                }
                set
                {
                    base[this.tableRegularExpressions.RegExMessageColumn] = value;
                }
            }

            public string RegExpression
            {
                get
                {
                    if (this.IsRegExpressionNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableRegularExpressions.RegExpressionColumn];
                }
                set
                {
                    base[this.tableRegularExpressions.RegExpressionColumn] = value;
                }
            }

            public int RegularExpressionId
            {
                get
                {
                    return (int) base[this.tableRegularExpressions.RegularExpressionIdColumn];
                }
                set
                {
                    base[this.tableRegularExpressions.RegularExpressionIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class RegularExpressionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private RegularExpressionData.RegularExpressionsRow eventRow;

            public RegularExpressionsRowChangeEvent(RegularExpressionData.RegularExpressionsRow row, DataRowAction action)
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

            public RegularExpressionData.RegularExpressionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void RegularExpressionsRowChangeEventHandler(object sender, RegularExpressionData.RegularExpressionsRowChangeEvent e);
    }
}

