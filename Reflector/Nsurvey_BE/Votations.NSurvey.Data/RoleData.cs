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

    [Serializable, DesignerCategory("code"), DebuggerStepThrough, ToolboxItem(true)]
    public class RoleData : DataSet
    {
        private RolesDataTable tableRoles;
        private SecurityRightsDataTable tableSecurityRights;

        public RoleData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected RoleData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["Roles"] != null)
                {
                    base.Tables.Add(new RolesDataTable(dataSet.Tables["Roles"]));
                }
                if (dataSet.Tables["SecurityRights"] != null)
                {
                    base.Tables.Add(new SecurityRightsDataTable(dataSet.Tables["SecurityRights"]));
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
            RoleData data = (RoleData) base.Clone();
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
            base.DataSetName = "RoleData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/RoleData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableRoles = new RolesDataTable();
            base.Tables.Add(this.tableRoles);
            this.tableSecurityRights = new SecurityRightsDataTable();
            base.Tables.Add(this.tableSecurityRights);
        }

        internal void InitVars()
        {
            this.tableRoles = (RolesDataTable) base.Tables["Roles"];
            if (this.tableRoles != null)
            {
                this.tableRoles.InitVars();
            }
            this.tableSecurityRights = (SecurityRightsDataTable) base.Tables["SecurityRights"];
            if (this.tableSecurityRights != null)
            {
                this.tableSecurityRights.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["Roles"] != null)
            {
                base.Tables.Add(new RolesDataTable(dataSet.Tables["Roles"]));
            }
            if (dataSet.Tables["SecurityRights"] != null)
            {
                base.Tables.Add(new SecurityRightsDataTable(dataSet.Tables["SecurityRights"]));
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

        private bool ShouldSerializeRoles()
        {
            return false;
        }

        private bool ShouldSerializeSecurityRights()
        {
            return false;
        }

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RolesDataTable Roles
        {
            get
            {
                return this.tableRoles;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SecurityRightsDataTable SecurityRights
        {
            get
            {
                return this.tableSecurityRights;
            }
        }

        [DebuggerStepThrough]
        public class RolesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnRoleId;
            private DataColumn columnRoleName;

            public event RoleData.RolesRowChangeEventHandler RolesRowChanged;

            public event RoleData.RolesRowChangeEventHandler RolesRowChanging;

            public event RoleData.RolesRowChangeEventHandler RolesRowDeleted;

            public event RoleData.RolesRowChangeEventHandler RolesRowDeleting;

            internal RolesDataTable() : base("Roles")
            {
                this.InitClass();
            }

            internal RolesDataTable(DataTable table) : base(table.TableName)
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

            public RoleData.RolesRow AddRolesRow(string RoleName)
            {
                RoleData.RolesRow row = (RoleData.RolesRow) base.NewRow();
                object[] objArray = new object[2];
                objArray[1] = RoleName;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public void AddRolesRow(RoleData.RolesRow row)
            {
                base.Rows.Add(row);
            }

            public override DataTable Clone()
            {
                RoleData.RolesDataTable table = (RoleData.RolesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new RoleData.RolesDataTable();
            }

            public RoleData.RolesRow FindByRoleId(int RoleId)
            {
                return (RoleData.RolesRow) base.Rows.Find(new object[] { RoleId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(RoleData.RolesRow);
            }

            private void InitClass()
            {
                this.columnRoleId = new DataColumn("RoleId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnRoleId);
                this.columnRoleName = new DataColumn("RoleName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnRoleName);
                base.Constraints.Add(new UniqueConstraint("RoleDataKey1", new DataColumn[] { this.columnRoleId }, true));
                this.columnRoleId.AutoIncrement = true;
                this.columnRoleId.AllowDBNull = false;
                this.columnRoleId.ReadOnly = true;
                this.columnRoleId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnRoleId = base.Columns["RoleId"];
                this.columnRoleName = base.Columns["RoleName"];
            }

            public RoleData.RolesRow NewRolesRow()
            {
                return (RoleData.RolesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new RoleData.RolesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.RolesRowChanged != null)
                {
                    this.RolesRowChanged(this, new RoleData.RolesRowChangeEvent((RoleData.RolesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.RolesRowChanging != null)
                {
                    this.RolesRowChanging(this, new RoleData.RolesRowChangeEvent((RoleData.RolesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.RolesRowDeleted != null)
                {
                    this.RolesRowDeleted(this, new RoleData.RolesRowChangeEvent((RoleData.RolesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.RolesRowDeleting != null)
                {
                    this.RolesRowDeleting(this, new RoleData.RolesRowChangeEvent((RoleData.RolesRow) e.Row, e.Action));
                }
            }

            public void RemoveRolesRow(RoleData.RolesRow row)
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

            public RoleData.RolesRow this[int index]
            {
                get
                {
                    return (RoleData.RolesRow) base.Rows[index];
                }
            }

            internal DataColumn RoleIdColumn
            {
                get
                {
                    return this.columnRoleId;
                }
            }

            internal DataColumn RoleNameColumn
            {
                get
                {
                    return this.columnRoleName;
                }
            }
        }

        [DebuggerStepThrough]
        public class RolesRow : DataRow
        {
            private RoleData.RolesDataTable tableRoles;

            internal RolesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableRoles = (RoleData.RolesDataTable) base.Table;
            }

            public bool IsRoleNameNull()
            {
                return base.IsNull(this.tableRoles.RoleNameColumn);
            }

            public void SetRoleNameNull()
            {
                base[this.tableRoles.RoleNameColumn] = Convert.DBNull;
            }

            public int RoleId
            {
                get
                {
                    return (int) base[this.tableRoles.RoleIdColumn];
                }
                set
                {
                    base[this.tableRoles.RoleIdColumn] = value;
                }
            }

            public string RoleName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableRoles.RoleNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableRoles.RoleNameColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class RolesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private RoleData.RolesRow eventRow;

            public RolesRowChangeEvent(RoleData.RolesRow row, DataRowAction action)
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

            public RoleData.RolesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void RolesRowChangeEventHandler(object sender, RoleData.RolesRowChangeEvent e);

        [DebuggerStepThrough]
        public class SecurityRightsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnSecurityRightId;

            public event RoleData.SecurityRightsRowChangeEventHandler SecurityRightsRowChanged;

            public event RoleData.SecurityRightsRowChangeEventHandler SecurityRightsRowChanging;

            public event RoleData.SecurityRightsRowChangeEventHandler SecurityRightsRowDeleted;

            public event RoleData.SecurityRightsRowChangeEventHandler SecurityRightsRowDeleting;

            internal SecurityRightsDataTable() : base("SecurityRights")
            {
                this.InitClass();
            }

            internal SecurityRightsDataTable(DataTable table) : base(table.TableName)
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

            public RoleData.SecurityRightsRow AddSecurityRightsRow(int SecurityRightId)
            {
                RoleData.SecurityRightsRow row = (RoleData.SecurityRightsRow) base.NewRow();
                row.ItemArray = new object[] { SecurityRightId };
                base.Rows.Add(row);
                return row;
            }

            public void AddSecurityRightsRow(RoleData.SecurityRightsRow row)
            {
                base.Rows.Add(row);
            }

            public override DataTable Clone()
            {
                RoleData.SecurityRightsDataTable table = (RoleData.SecurityRightsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new RoleData.SecurityRightsDataTable();
            }

            public RoleData.SecurityRightsRow FindBySecurityRightId(int SecurityRightId)
            {
                return (RoleData.SecurityRightsRow) base.Rows.Find(new object[] { SecurityRightId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(RoleData.SecurityRightsRow);
            }

            private void InitClass()
            {
                this.columnSecurityRightId = new DataColumn("SecurityRightId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSecurityRightId);
                base.Constraints.Add(new UniqueConstraint("RoleDataKey2", new DataColumn[] { this.columnSecurityRightId }, true));
                this.columnSecurityRightId.AllowDBNull = false;
                this.columnSecurityRightId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnSecurityRightId = base.Columns["SecurityRightId"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new RoleData.SecurityRightsRow(builder);
            }

            public RoleData.SecurityRightsRow NewSecurityRightsRow()
            {
                return (RoleData.SecurityRightsRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.SecurityRightsRowChanged != null)
                {
                    this.SecurityRightsRowChanged(this, new RoleData.SecurityRightsRowChangeEvent((RoleData.SecurityRightsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.SecurityRightsRowChanging != null)
                {
                    this.SecurityRightsRowChanging(this, new RoleData.SecurityRightsRowChangeEvent((RoleData.SecurityRightsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.SecurityRightsRowDeleted != null)
                {
                    this.SecurityRightsRowDeleted(this, new RoleData.SecurityRightsRowChangeEvent((RoleData.SecurityRightsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.SecurityRightsRowDeleting != null)
                {
                    this.SecurityRightsRowDeleting(this, new RoleData.SecurityRightsRowChangeEvent((RoleData.SecurityRightsRow) e.Row, e.Action));
                }
            }

            public void RemoveSecurityRightsRow(RoleData.SecurityRightsRow row)
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

            public RoleData.SecurityRightsRow this[int index]
            {
                get
                {
                    return (RoleData.SecurityRightsRow) base.Rows[index];
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
        public class SecurityRightsRow : DataRow
        {
            private RoleData.SecurityRightsDataTable tableSecurityRights;

            internal SecurityRightsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableSecurityRights = (RoleData.SecurityRightsDataTable) base.Table;
            }

            public int SecurityRightId
            {
                get
                {
                    return (int) base[this.tableSecurityRights.SecurityRightIdColumn];
                }
                set
                {
                    base[this.tableSecurityRights.SecurityRightIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class SecurityRightsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private RoleData.SecurityRightsRow eventRow;

            public SecurityRightsRowChangeEvent(RoleData.SecurityRightsRow row, DataRowAction action)
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

            public RoleData.SecurityRightsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void SecurityRightsRowChangeEventHandler(object sender, RoleData.SecurityRightsRowChangeEvent e);
    }
}

