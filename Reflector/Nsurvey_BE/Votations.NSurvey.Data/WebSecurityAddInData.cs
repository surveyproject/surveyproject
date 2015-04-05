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
    public class WebSecurityAddInData : DataSet
    {
        private WebSecurityAddInsDataTable tableWebSecurityAddIns;

        public WebSecurityAddInData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected WebSecurityAddInData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["WebSecurityAddIns"] != null)
                {
                    base.Tables.Add(new WebSecurityAddInsDataTable(dataSet.Tables["WebSecurityAddIns"]));
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
            WebSecurityAddInData data = (WebSecurityAddInData) base.Clone();
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
            base.DataSetName = "WebSecurityAddInData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/WebSecurityAddInData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableWebSecurityAddIns = new WebSecurityAddInsDataTable();
            base.Tables.Add(this.tableWebSecurityAddIns);
        }

        internal void InitVars()
        {
            this.tableWebSecurityAddIns = (WebSecurityAddInsDataTable) base.Tables["WebSecurityAddIns"];
            if (this.tableWebSecurityAddIns != null)
            {
                this.tableWebSecurityAddIns.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["WebSecurityAddIns"] != null)
            {
                base.Tables.Add(new WebSecurityAddInsDataTable(dataSet.Tables["WebSecurityAddIns"]));
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

        private bool ShouldSerializeWebSecurityAddIns()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public WebSecurityAddInsDataTable WebSecurityAddIns
        {
            get
            {
                return this.tableWebSecurityAddIns;
            }
        }

        [DebuggerStepThrough]
        public class WebSecurityAddInsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAddInOrder;
            private DataColumn columnBuiltIn;
            private DataColumn columnDescription;
            private DataColumn columnDisabled;
            private DataColumn columnSurveyID;
            private DataColumn columnTypeAssembly;
            private DataColumn columnTypeMode;
            private DataColumn columnTypeNameSpace;
            private DataColumn columnWebSecurityAddInId;

            public event WebSecurityAddInData.WebSecurityAddInsRowChangeEventHandler WebSecurityAddInsRowChanged;

            public event WebSecurityAddInData.WebSecurityAddInsRowChangeEventHandler WebSecurityAddInsRowChanging;

            public event WebSecurityAddInData.WebSecurityAddInsRowChangeEventHandler WebSecurityAddInsRowDeleted;

            public event WebSecurityAddInData.WebSecurityAddInsRowChangeEventHandler WebSecurityAddInsRowDeleting;

            internal WebSecurityAddInsDataTable() : base("WebSecurityAddIns")
            {
                this.InitClass();
            }

            internal WebSecurityAddInsDataTable(DataTable table) : base(table.TableName)
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

            public void AddWebSecurityAddInsRow(WebSecurityAddInData.WebSecurityAddInsRow row)
            {
                base.Rows.Add(row);
            }

            public WebSecurityAddInData.WebSecurityAddInsRow AddWebSecurityAddInsRow(string Description, bool BuiltIn, string TypeNameSpace, string TypeAssembly, int TypeMode, int SurveyID, int AddInOrder, bool Disabled)
            {
                WebSecurityAddInData.WebSecurityAddInsRow row = (WebSecurityAddInData.WebSecurityAddInsRow) base.NewRow();
                object[] objArray = new object[9];
                objArray[1] = Description;
                objArray[2] = BuiltIn;
                objArray[3] = TypeNameSpace;
                objArray[4] = TypeAssembly;
                objArray[5] = TypeMode;
                objArray[6] = SurveyID;
                objArray[7] = AddInOrder;
                objArray[8] = Disabled;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                WebSecurityAddInData.WebSecurityAddInsDataTable table = (WebSecurityAddInData.WebSecurityAddInsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new WebSecurityAddInData.WebSecurityAddInsDataTable();
            }

            public WebSecurityAddInData.WebSecurityAddInsRow FindByWebSecurityAddInId(int WebSecurityAddInId)
            {
                return (WebSecurityAddInData.WebSecurityAddInsRow) base.Rows.Find(new object[] { WebSecurityAddInId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(WebSecurityAddInData.WebSecurityAddInsRow);
            }

            private void InitClass()
            {
                this.columnWebSecurityAddInId = new DataColumn("WebSecurityAddInId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnWebSecurityAddInId);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnBuiltIn = new DataColumn("BuiltIn", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnBuiltIn);
                this.columnTypeNameSpace = new DataColumn("TypeNameSpace", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTypeNameSpace);
                this.columnTypeAssembly = new DataColumn("TypeAssembly", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTypeAssembly);
                this.columnTypeMode = new DataColumn("TypeMode", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTypeMode);
                this.columnSurveyID = new DataColumn("SurveyID", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyID);
                this.columnAddInOrder = new DataColumn("AddInOrder", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAddInOrder);
                this.columnDisabled = new DataColumn("Disabled", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnDisabled);
                base.Constraints.Add(new UniqueConstraint("WebSecurityAddInDataKey1", new DataColumn[] { this.columnWebSecurityAddInId }, true));
                this.columnWebSecurityAddInId.AutoIncrement = true;
                this.columnWebSecurityAddInId.AllowDBNull = false;
                this.columnWebSecurityAddInId.ReadOnly = true;
                this.columnWebSecurityAddInId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnWebSecurityAddInId = base.Columns["WebSecurityAddInId"];
                this.columnDescription = base.Columns["Description"];
                this.columnBuiltIn = base.Columns["BuiltIn"];
                this.columnTypeNameSpace = base.Columns["TypeNameSpace"];
                this.columnTypeAssembly = base.Columns["TypeAssembly"];
                this.columnTypeMode = base.Columns["TypeMode"];
                this.columnSurveyID = base.Columns["SurveyID"];
                this.columnAddInOrder = base.Columns["AddInOrder"];
                this.columnDisabled = base.Columns["Disabled"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new WebSecurityAddInData.WebSecurityAddInsRow(builder);
            }

            public WebSecurityAddInData.WebSecurityAddInsRow NewWebSecurityAddInsRow()
            {
                return (WebSecurityAddInData.WebSecurityAddInsRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.WebSecurityAddInsRowChanged != null)
                {
                    this.WebSecurityAddInsRowChanged(this, new WebSecurityAddInData.WebSecurityAddInsRowChangeEvent((WebSecurityAddInData.WebSecurityAddInsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.WebSecurityAddInsRowChanging != null)
                {
                    this.WebSecurityAddInsRowChanging(this, new WebSecurityAddInData.WebSecurityAddInsRowChangeEvent((WebSecurityAddInData.WebSecurityAddInsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.WebSecurityAddInsRowDeleted != null)
                {
                    this.WebSecurityAddInsRowDeleted(this, new WebSecurityAddInData.WebSecurityAddInsRowChangeEvent((WebSecurityAddInData.WebSecurityAddInsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.WebSecurityAddInsRowDeleting != null)
                {
                    this.WebSecurityAddInsRowDeleting(this, new WebSecurityAddInData.WebSecurityAddInsRowChangeEvent((WebSecurityAddInData.WebSecurityAddInsRow) e.Row, e.Action));
                }
            }

            public void RemoveWebSecurityAddInsRow(WebSecurityAddInData.WebSecurityAddInsRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AddInOrderColumn
            {
                get
                {
                    return this.columnAddInOrder;
                }
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

            internal DataColumn DisabledColumn
            {
                get
                {
                    return this.columnDisabled;
                }
            }

            public WebSecurityAddInData.WebSecurityAddInsRow this[int index]
            {
                get
                {
                    return (WebSecurityAddInData.WebSecurityAddInsRow) base.Rows[index];
                }
            }

            internal DataColumn SurveyIDColumn
            {
                get
                {
                    return this.columnSurveyID;
                }
            }

            internal DataColumn TypeAssemblyColumn
            {
                get
                {
                    return this.columnTypeAssembly;
                }
            }

            internal DataColumn TypeModeColumn
            {
                get
                {
                    return this.columnTypeMode;
                }
            }

            internal DataColumn TypeNameSpaceColumn
            {
                get
                {
                    return this.columnTypeNameSpace;
                }
            }

            internal DataColumn WebSecurityAddInIdColumn
            {
                get
                {
                    return this.columnWebSecurityAddInId;
                }
            }
        }

        [DebuggerStepThrough]
        public class WebSecurityAddInsRow : DataRow
        {
            private WebSecurityAddInData.WebSecurityAddInsDataTable tableWebSecurityAddIns;

            internal WebSecurityAddInsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableWebSecurityAddIns = (WebSecurityAddInData.WebSecurityAddInsDataTable) base.Table;
            }

            public bool IsAddInOrderNull()
            {
                return base.IsNull(this.tableWebSecurityAddIns.AddInOrderColumn);
            }

            public bool IsBuiltInNull()
            {
                return base.IsNull(this.tableWebSecurityAddIns.BuiltInColumn);
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableWebSecurityAddIns.DescriptionColumn);
            }

            public bool IsDisabledNull()
            {
                return base.IsNull(this.tableWebSecurityAddIns.DisabledColumn);
            }

            public bool IsSurveyIDNull()
            {
                return base.IsNull(this.tableWebSecurityAddIns.SurveyIDColumn);
            }

            public bool IsTypeAssemblyNull()
            {
                return base.IsNull(this.tableWebSecurityAddIns.TypeAssemblyColumn);
            }

            public bool IsTypeModeNull()
            {
                return base.IsNull(this.tableWebSecurityAddIns.TypeModeColumn);
            }

            public bool IsTypeNameSpaceNull()
            {
                return base.IsNull(this.tableWebSecurityAddIns.TypeNameSpaceColumn);
            }

            public void SetAddInOrderNull()
            {
                base[this.tableWebSecurityAddIns.AddInOrderColumn] = Convert.DBNull;
            }

            public void SetBuiltInNull()
            {
                base[this.tableWebSecurityAddIns.BuiltInColumn] = Convert.DBNull;
            }

            public void SetDescriptionNull()
            {
                base[this.tableWebSecurityAddIns.DescriptionColumn] = Convert.DBNull;
            }

            public void SetDisabledNull()
            {
                base[this.tableWebSecurityAddIns.DisabledColumn] = Convert.DBNull;
            }

            public void SetSurveyIDNull()
            {
                base[this.tableWebSecurityAddIns.SurveyIDColumn] = Convert.DBNull;
            }

            public void SetTypeAssemblyNull()
            {
                base[this.tableWebSecurityAddIns.TypeAssemblyColumn] = Convert.DBNull;
            }

            public void SetTypeModeNull()
            {
                base[this.tableWebSecurityAddIns.TypeModeColumn] = Convert.DBNull;
            }

            public void SetTypeNameSpaceNull()
            {
                base[this.tableWebSecurityAddIns.TypeNameSpaceColumn] = Convert.DBNull;
            }

            public int AddInOrder
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableWebSecurityAddIns.AddInOrderColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableWebSecurityAddIns.AddInOrderColumn] = value;
                }
            }

            public bool BuiltIn
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableWebSecurityAddIns.BuiltInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableWebSecurityAddIns.BuiltInColumn] = value;
                }
            }

            public string Description
            {
                get
                {
                    if (this.IsDescriptionNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableWebSecurityAddIns.DescriptionColumn];
                }
                set
                {
                    base[this.tableWebSecurityAddIns.DescriptionColumn] = value;
                }
            }

            public bool Disabled
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableWebSecurityAddIns.DisabledColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableWebSecurityAddIns.DisabledColumn] = value;
                }
            }

            public int SurveyID
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableWebSecurityAddIns.SurveyIDColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableWebSecurityAddIns.SurveyIDColumn] = value;
                }
            }

            public string TypeAssembly
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableWebSecurityAddIns.TypeAssemblyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableWebSecurityAddIns.TypeAssemblyColumn] = value;
                }
            }

            public int TypeMode
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableWebSecurityAddIns.TypeModeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableWebSecurityAddIns.TypeModeColumn] = value;
                }
            }

            public string TypeNameSpace
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableWebSecurityAddIns.TypeNameSpaceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableWebSecurityAddIns.TypeNameSpaceColumn] = value;
                }
            }

            public int WebSecurityAddInId
            {
                get
                {
                    return (int) base[this.tableWebSecurityAddIns.WebSecurityAddInIdColumn];
                }
                set
                {
                    base[this.tableWebSecurityAddIns.WebSecurityAddInIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class WebSecurityAddInsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private WebSecurityAddInData.WebSecurityAddInsRow eventRow;

            public WebSecurityAddInsRowChangeEvent(WebSecurityAddInData.WebSecurityAddInsRow row, DataRowAction action)
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

            public WebSecurityAddInData.WebSecurityAddInsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void WebSecurityAddInsRowChangeEventHandler(object sender, WebSecurityAddInData.WebSecurityAddInsRowChangeEvent e);
    }
}

