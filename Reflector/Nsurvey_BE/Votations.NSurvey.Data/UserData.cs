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
    public class UserData : DataSet
    {
        private UsersDataTable tableUsers;

        public UserData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected UserData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["Users"] != null)
                {
                    base.Tables.Add(new UsersDataTable(dataSet.Tables["Users"]));
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
            UserData data = (UserData) base.Clone();
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
            base.DataSetName = "UserData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/UserData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableUsers = new UsersDataTable();
            base.Tables.Add(this.tableUsers);
        }

        internal void InitVars()
        {
            this.tableUsers = (UsersDataTable) base.Tables["Users"];
            if (this.tableUsers != null)
            {
                this.tableUsers.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["Users"] != null)
            {
                base.Tables.Add(new UsersDataTable(dataSet.Tables["Users"]));
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

        private bool ShouldSerializeUsers()
        {
            return false;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public UsersDataTable Users
        {
            get
            {
                return this.tableUsers;
            }
        }

        [DebuggerStepThrough]
        public class UsersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnUserId;
            private DataColumn columnUserName;

            public event UserData.UsersRowChangeEventHandler UsersRowChanged;

            public event UserData.UsersRowChangeEventHandler UsersRowChanging;

            public event UserData.UsersRowChangeEventHandler UsersRowDeleted;

            public event UserData.UsersRowChangeEventHandler UsersRowDeleting;

            internal UsersDataTable() : base("Users")
            {
                this.InitClass();
            }

            internal UsersDataTable(DataTable table) : base(table.TableName)
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

            public UserData.UsersRow AddUsersRow(string UserName)
            {
                UserData.UsersRow row = (UserData.UsersRow) base.NewRow();
                object[] objArray = new object[2];
                objArray[1] = UserName;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public void AddUsersRow(UserData.UsersRow row)
            {
                base.Rows.Add(row);
            }

            public override DataTable Clone()
            {
                UserData.UsersDataTable table = (UserData.UsersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new UserData.UsersDataTable();
            }

            public UserData.UsersRow FindByUserId(int UserId)
            {
                return (UserData.UsersRow) base.Rows.Find(new object[] { UserId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(UserData.UsersRow);
            }

            private void InitClass()
            {
                this.columnUserId = new DataColumn("UserId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnUserId);
                this.columnUserName = new DataColumn("UserName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUserName);
                base.Constraints.Add(new UniqueConstraint("UserDataKey1", new DataColumn[] { this.columnUserId }, true));
                this.columnUserId.AutoIncrement = true;
                this.columnUserId.AllowDBNull = false;
                this.columnUserId.ReadOnly = true;
                this.columnUserId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnUserId = base.Columns["UserId"];
                this.columnUserName = base.Columns["UserName"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new UserData.UsersRow(builder);
            }

            public UserData.UsersRow NewUsersRow()
            {
                return (UserData.UsersRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.UsersRowChanged != null)
                {
                    this.UsersRowChanged(this, new UserData.UsersRowChangeEvent((UserData.UsersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.UsersRowChanging != null)
                {
                    this.UsersRowChanging(this, new UserData.UsersRowChangeEvent((UserData.UsersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.UsersRowDeleted != null)
                {
                    this.UsersRowDeleted(this, new UserData.UsersRowChangeEvent((UserData.UsersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.UsersRowDeleting != null)
                {
                    this.UsersRowDeleting(this, new UserData.UsersRowChangeEvent((UserData.UsersRow) e.Row, e.Action));
                }
            }

            public void RemoveUsersRow(UserData.UsersRow row)
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

            public UserData.UsersRow this[int index]
            {
                get
                {
                    return (UserData.UsersRow) base.Rows[index];
                }
            }

            internal DataColumn UserIdColumn
            {
                get
                {
                    return this.columnUserId;
                }
            }

            internal DataColumn UserNameColumn
            {
                get
                {
                    return this.columnUserName;
                }
            }
        }

        [DebuggerStepThrough]
        public class UsersRow : DataRow
        {
            private UserData.UsersDataTable tableUsers;

            internal UsersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableUsers = (UserData.UsersDataTable) base.Table;
            }

            public bool IsUserNameNull()
            {
                return base.IsNull(this.tableUsers.UserNameColumn);
            }

            public void SetUserNameNull()
            {
                base[this.tableUsers.UserNameColumn] = Convert.DBNull;
            }

            public int UserId
            {
                get
                {
                    return (int) base[this.tableUsers.UserIdColumn];
                }
                set
                {
                    base[this.tableUsers.UserIdColumn] = value;
                }
            }

            public string UserName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableUsers.UserNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableUsers.UserNameColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class UsersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private UserData.UsersRow eventRow;

            public UsersRowChangeEvent(UserData.UsersRow row, DataRowAction action)
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

            public UserData.UsersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void UsersRowChangeEventHandler(object sender, UserData.UsersRowChangeEvent e);
    }
}

