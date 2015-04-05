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
    public class SecurityRightData : DataSet
    {
        private SecurityRightDataTable tableSecurityRight;

        public SecurityRightData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected SecurityRightData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["SecurityRight"] != null)
                {
                    base.Tables.Add(new SecurityRightDataTable(dataSet.Tables["SecurityRight"]));
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
            SecurityRightData data = (SecurityRightData) base.Clone();
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
            base.DataSetName = "SecurityRightData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/SecurityRightData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableSecurityRight = new SecurityRightDataTable();
            base.Tables.Add(this.tableSecurityRight);
        }

        internal void InitVars()
        {
            this.tableSecurityRight = (SecurityRightDataTable) base.Tables["SecurityRight"];
            if (this.tableSecurityRight != null)
            {
                this.tableSecurityRight.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["SecurityRight"] != null)
            {
                base.Tables.Add(new SecurityRightDataTable(dataSet.Tables["SecurityRight"]));
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

        private bool ShouldSerializeSecurityRight()
        {
            return false;
        }

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SecurityRightDataTable SecurityRight
        {
            get
            {
                return this.tableSecurityRight;
            }
        }

        [DebuggerStepThrough]
        public class SecurityRightDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescription;
            private DataColumn columnSecurityRightId;

            public event SecurityRightData.SecurityRightRowChangeEventHandler SecurityRightRowChanged;

            public event SecurityRightData.SecurityRightRowChangeEventHandler SecurityRightRowChanging;

            public event SecurityRightData.SecurityRightRowChangeEventHandler SecurityRightRowDeleted;

            public event SecurityRightData.SecurityRightRowChangeEventHandler SecurityRightRowDeleting;

            internal SecurityRightDataTable() : base("SecurityRight")
            {
                this.InitClass();
            }

            internal SecurityRightDataTable(DataTable table) : base(table.TableName)
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

            public void AddSecurityRightRow(SecurityRightData.SecurityRightRow row)
            {
                base.Rows.Add(row);
            }

            public SecurityRightData.SecurityRightRow AddSecurityRightRow(int SecurityRightId, string Description)
            {
                SecurityRightData.SecurityRightRow row = (SecurityRightData.SecurityRightRow) base.NewRow();
                row.ItemArray = new object[] { SecurityRightId, Description };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                SecurityRightData.SecurityRightDataTable table = (SecurityRightData.SecurityRightDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new SecurityRightData.SecurityRightDataTable();
            }

            public SecurityRightData.SecurityRightRow FindBySecurityRightId(int SecurityRightId)
            {
                return (SecurityRightData.SecurityRightRow) base.Rows.Find(new object[] { SecurityRightId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(SecurityRightData.SecurityRightRow);
            }

            private void InitClass()
            {
                this.columnSecurityRightId = new DataColumn("SecurityRightId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSecurityRightId);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                base.Constraints.Add(new UniqueConstraint("SecurityRightDataKey1", new DataColumn[] { this.columnSecurityRightId }, true));
                this.columnSecurityRightId.AllowDBNull = false;
                this.columnSecurityRightId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnSecurityRightId = base.Columns["SecurityRightId"];
                this.columnDescription = base.Columns["Description"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new SecurityRightData.SecurityRightRow(builder);
            }

            public SecurityRightData.SecurityRightRow NewSecurityRightRow()
            {
                return (SecurityRightData.SecurityRightRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.SecurityRightRowChanged != null)
                {
                    this.SecurityRightRowChanged(this, new SecurityRightData.SecurityRightRowChangeEvent((SecurityRightData.SecurityRightRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.SecurityRightRowChanging != null)
                {
                    this.SecurityRightRowChanging(this, new SecurityRightData.SecurityRightRowChangeEvent((SecurityRightData.SecurityRightRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.SecurityRightRowDeleted != null)
                {
                    this.SecurityRightRowDeleted(this, new SecurityRightData.SecurityRightRowChangeEvent((SecurityRightData.SecurityRightRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.SecurityRightRowDeleting != null)
                {
                    this.SecurityRightRowDeleting(this, new SecurityRightData.SecurityRightRowChangeEvent((SecurityRightData.SecurityRightRow) e.Row, e.Action));
                }
            }

            public void RemoveSecurityRightRow(SecurityRightData.SecurityRightRow row)
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

            public SecurityRightData.SecurityRightRow this[int index]
            {
                get
                {
                    return (SecurityRightData.SecurityRightRow) base.Rows[index];
                }
            }

            internal DataColumn SecurityRightIdColumn
            {
                get
                {
                    return this.columnSecurityRightId;
                }
            }
        }

        [DebuggerStepThrough]
        public class SecurityRightRow : DataRow
        {
            private SecurityRightData.SecurityRightDataTable tableSecurityRight;

            internal SecurityRightRow(DataRowBuilder rb) : base(rb)
            {
                this.tableSecurityRight = (SecurityRightData.SecurityRightDataTable) base.Table;
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableSecurityRight.DescriptionColumn);
            }

            public void SetDescriptionNull()
            {
                base[this.tableSecurityRight.DescriptionColumn] = Convert.DBNull;
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSecurityRight.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSecurityRight.DescriptionColumn] = value;
                }
            }

            public int SecurityRightId
            {
                get
                {
                    return (int) base[this.tableSecurityRight.SecurityRightIdColumn];
                }
                set
                {
                    base[this.tableSecurityRight.SecurityRightIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class SecurityRightRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private SecurityRightData.SecurityRightRow eventRow;

            public SecurityRightRowChangeEvent(SecurityRightData.SecurityRightRow row, DataRowAction action)
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

            public SecurityRightData.SecurityRightRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void SecurityRightRowChangeEventHandler(object sender, SecurityRightData.SecurityRightRowChangeEvent e);
    }
}

