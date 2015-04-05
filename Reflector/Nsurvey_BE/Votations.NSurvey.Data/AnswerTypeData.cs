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
    public class AnswerTypeData : DataSet
    {
        private AnswerTypesDataTable tableAnswerTypes;

        public AnswerTypeData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected AnswerTypeData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["AnswerTypes"] != null)
                {
                    base.Tables.Add(new AnswerTypesDataTable(dataSet.Tables["AnswerTypes"]));
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
            AnswerTypeData data = (AnswerTypeData) base.Clone();
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
            base.DataSetName = "AnswerTypeData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/AnswerTypeData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableAnswerTypes = new AnswerTypesDataTable();
            base.Tables.Add(this.tableAnswerTypes);
        }

        internal void InitVars()
        {
            this.tableAnswerTypes = (AnswerTypesDataTable) base.Tables["AnswerTypes"];
            if (this.tableAnswerTypes != null)
            {
                this.tableAnswerTypes.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["AnswerTypes"] != null)
            {
                base.Tables.Add(new AnswerTypesDataTable(dataSet.Tables["AnswerTypes"]));
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

        private bool ShouldSerializeAnswerTypes()
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
        public AnswerTypesDataTable AnswerTypes
        {
            get
            {
                return this.tableAnswerTypes;
            }
        }

        [DebuggerStepThrough]
        public class AnswerTypesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerTypeId;
            private DataColumn columnBuiltIn;
            private DataColumn columnDataSource;
            private DataColumn columnDescription;
            private DataColumn columnFieldHeight;
            private DataColumn columnFieldLength;
            private DataColumn columnFieldWidth;
            private DataColumn columnJavascriptCode;
            private DataColumn columnJavascriptErrorMessage;
            private DataColumn columnJavascriptFunctionName;
            private DataColumn columnPublicFieldResults;
            private DataColumn columnTypeAssembly;
            private DataColumn columnTypeMode;
            private DataColumn columnTypeNameSpace;
            private DataColumn columnXmlDataSource;

            public event AnswerTypeData.AnswerTypesRowChangeEventHandler AnswerTypesRowChanged;

            public event AnswerTypeData.AnswerTypesRowChangeEventHandler AnswerTypesRowChanging;

            public event AnswerTypeData.AnswerTypesRowChangeEventHandler AnswerTypesRowDeleted;

            public event AnswerTypeData.AnswerTypesRowChangeEventHandler AnswerTypesRowDeleting;

            internal AnswerTypesDataTable() : base("AnswerTypes")
            {
                this.InitClass();
            }

            internal AnswerTypesDataTable(DataTable table) : base(table.TableName)
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

            public void AddAnswerTypesRow(AnswerTypeData.AnswerTypesRow row)
            {
                base.Rows.Add(row);
            }

            public AnswerTypeData.AnswerTypesRow AddAnswerTypesRow(string Description, int FieldWidth, int FieldHeight, int FieldLength, int TypeMode, string XmlDataSource, bool PublicFieldResults, string JavascriptFunctionName, string JavascriptCode, string JavascriptErrorMessage, string TypeAssembly, string TypeNameSpace, bool BuiltIn, string DataSource)
            {
                AnswerTypeData.AnswerTypesRow row = (AnswerTypeData.AnswerTypesRow) base.NewRow();
                object[] objArray = new object[15];
                objArray[1] = Description;
                objArray[2] = FieldWidth;
                objArray[3] = FieldHeight;
                objArray[4] = FieldLength;
                objArray[5] = TypeMode;
                objArray[6] = XmlDataSource;
                objArray[7] = PublicFieldResults;
                objArray[8] = JavascriptFunctionName;
                objArray[9] = JavascriptCode;
                objArray[10] = JavascriptErrorMessage;
                objArray[11] = TypeAssembly;
                objArray[12] = TypeNameSpace;
                objArray[13] = BuiltIn;
                objArray[14] = DataSource;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                AnswerTypeData.AnswerTypesDataTable table = (AnswerTypeData.AnswerTypesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new AnswerTypeData.AnswerTypesDataTable();
            }

            public AnswerTypeData.AnswerTypesRow FindByAnswerTypeId(int AnswerTypeId)
            {
                return (AnswerTypeData.AnswerTypesRow) base.Rows.Find(new object[] { AnswerTypeId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(AnswerTypeData.AnswerTypesRow);
            }

            private void InitClass()
            {
                this.columnAnswerTypeId = new DataColumn("AnswerTypeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerTypeId);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnFieldWidth = new DataColumn("FieldWidth", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldWidth);
                this.columnFieldHeight = new DataColumn("FieldHeight", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldHeight);
                this.columnFieldLength = new DataColumn("FieldLength", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldLength);
                this.columnTypeMode = new DataColumn("TypeMode", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTypeMode);
                this.columnXmlDataSource = new DataColumn("XmlDataSource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnXmlDataSource);
                this.columnPublicFieldResults = new DataColumn("PublicFieldResults", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnPublicFieldResults);
                this.columnJavascriptFunctionName = new DataColumn("JavascriptFunctionName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptFunctionName);
                this.columnJavascriptCode = new DataColumn("JavascriptCode", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptCode);
                this.columnJavascriptErrorMessage = new DataColumn("JavascriptErrorMessage", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptErrorMessage);
                this.columnTypeAssembly = new DataColumn("TypeAssembly", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTypeAssembly);
                this.columnTypeNameSpace = new DataColumn("TypeNameSpace", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTypeNameSpace);
                this.columnBuiltIn = new DataColumn("BuiltIn", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnBuiltIn);
                this.columnDataSource = new DataColumn("DataSource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDataSource);
                base.Constraints.Add(new UniqueConstraint("AnswerTypeDataKey1", new DataColumn[] { this.columnAnswerTypeId }, true));
                this.columnAnswerTypeId.AutoIncrement = true;
                this.columnAnswerTypeId.AllowDBNull = false;
                this.columnAnswerTypeId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnAnswerTypeId = base.Columns["AnswerTypeId"];
                this.columnDescription = base.Columns["Description"];
                this.columnFieldWidth = base.Columns["FieldWidth"];
                this.columnFieldHeight = base.Columns["FieldHeight"];
                this.columnFieldLength = base.Columns["FieldLength"];
                this.columnTypeMode = base.Columns["TypeMode"];
                this.columnXmlDataSource = base.Columns["XmlDataSource"];
                this.columnPublicFieldResults = base.Columns["PublicFieldResults"];
                this.columnJavascriptFunctionName = base.Columns["JavascriptFunctionName"];
                this.columnJavascriptCode = base.Columns["JavascriptCode"];
                this.columnJavascriptErrorMessage = base.Columns["JavascriptErrorMessage"];
                this.columnTypeAssembly = base.Columns["TypeAssembly"];
                this.columnTypeNameSpace = base.Columns["TypeNameSpace"];
                this.columnBuiltIn = base.Columns["BuiltIn"];
                this.columnDataSource = base.Columns["DataSource"];
            }

            public AnswerTypeData.AnswerTypesRow NewAnswerTypesRow()
            {
                return (AnswerTypeData.AnswerTypesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new AnswerTypeData.AnswerTypesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswerTypesRowChanged != null)
                {
                    this.AnswerTypesRowChanged(this, new AnswerTypeData.AnswerTypesRowChangeEvent((AnswerTypeData.AnswerTypesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswerTypesRowChanging != null)
                {
                    this.AnswerTypesRowChanging(this, new AnswerTypeData.AnswerTypesRowChangeEvent((AnswerTypeData.AnswerTypesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswerTypesRowDeleted != null)
                {
                    this.AnswerTypesRowDeleted(this, new AnswerTypeData.AnswerTypesRowChangeEvent((AnswerTypeData.AnswerTypesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswerTypesRowDeleting != null)
                {
                    this.AnswerTypesRowDeleting(this, new AnswerTypeData.AnswerTypesRowChangeEvent((AnswerTypeData.AnswerTypesRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswerTypesRow(AnswerTypeData.AnswerTypesRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AnswerTypeIdColumn
            {
                get
                {
                    return this.columnAnswerTypeId;
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

            internal DataColumn DataSourceColumn
            {
                get
                {
                    return this.columnDataSource;
                }
            }

            internal DataColumn DescriptionColumn
            {
                get
                {
                    return this.columnDescription;
                }
            }

            internal DataColumn FieldHeightColumn
            {
                get
                {
                    return this.columnFieldHeight;
                }
            }

            internal DataColumn FieldLengthColumn
            {
                get
                {
                    return this.columnFieldLength;
                }
            }

            internal DataColumn FieldWidthColumn
            {
                get
                {
                    return this.columnFieldWidth;
                }
            }

            public AnswerTypeData.AnswerTypesRow this[int index]
            {
                get
                {
                    return (AnswerTypeData.AnswerTypesRow) base.Rows[index];
                }
            }

            internal DataColumn JavascriptCodeColumn
            {
                get
                {
                    return this.columnJavascriptCode;
                }
            }

            internal DataColumn JavascriptErrorMessageColumn
            {
                get
                {
                    return this.columnJavascriptErrorMessage;
                }
            }

            internal DataColumn JavascriptFunctionNameColumn
            {
                get
                {
                    return this.columnJavascriptFunctionName;
                }
            }

            internal DataColumn PublicFieldResultsColumn
            {
                get
                {
                    return this.columnPublicFieldResults;
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

            internal DataColumn XmlDataSourceColumn
            {
                get
                {
                    return this.columnXmlDataSource;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswerTypesRow : DataRow
        {
            private AnswerTypeData.AnswerTypesDataTable tableAnswerTypes;

            internal AnswerTypesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswerTypes = (AnswerTypeData.AnswerTypesDataTable) base.Table;
            }

            public bool IsBuiltInNull()
            {
                return base.IsNull(this.tableAnswerTypes.BuiltInColumn);
            }

            public bool IsDataSourceNull()
            {
                return base.IsNull(this.tableAnswerTypes.DataSourceColumn);
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableAnswerTypes.DescriptionColumn);
            }

            public bool IsFieldHeightNull()
            {
                return base.IsNull(this.tableAnswerTypes.FieldHeightColumn);
            }

            public bool IsFieldLengthNull()
            {
                return base.IsNull(this.tableAnswerTypes.FieldLengthColumn);
            }

            public bool IsFieldWidthNull()
            {
                return base.IsNull(this.tableAnswerTypes.FieldWidthColumn);
            }

            public bool IsJavascriptCodeNull()
            {
                return base.IsNull(this.tableAnswerTypes.JavascriptCodeColumn);
            }

            public bool IsJavascriptErrorMessageNull()
            {
                return base.IsNull(this.tableAnswerTypes.JavascriptErrorMessageColumn);
            }

            public bool IsJavascriptFunctionNameNull()
            {
                return base.IsNull(this.tableAnswerTypes.JavascriptFunctionNameColumn);
            }

            public bool IsPublicFieldResultsNull()
            {
                return base.IsNull(this.tableAnswerTypes.PublicFieldResultsColumn);
            }

            public bool IsTypeAssemblyNull()
            {
                return base.IsNull(this.tableAnswerTypes.TypeAssemblyColumn);
            }

            public bool IsTypeModeNull()
            {
                return base.IsNull(this.tableAnswerTypes.TypeModeColumn);
            }

            public bool IsTypeNameSpaceNull()
            {
                return base.IsNull(this.tableAnswerTypes.TypeNameSpaceColumn);
            }

            public bool IsXmlDataSourceNull()
            {
                return base.IsNull(this.tableAnswerTypes.XmlDataSourceColumn);
            }

            public void SetBuiltInNull()
            {
                base[this.tableAnswerTypes.BuiltInColumn] = Convert.DBNull;
            }

            public void SetDataSourceNull()
            {
                base[this.tableAnswerTypes.DataSourceColumn] = Convert.DBNull;
            }

            public void SetDescriptionNull()
            {
                base[this.tableAnswerTypes.DescriptionColumn] = Convert.DBNull;
            }

            public void SetFieldHeightNull()
            {
                base[this.tableAnswerTypes.FieldHeightColumn] = Convert.DBNull;
            }

            public void SetFieldLengthNull()
            {
                base[this.tableAnswerTypes.FieldLengthColumn] = Convert.DBNull;
            }

            public void SetFieldWidthNull()
            {
                base[this.tableAnswerTypes.FieldWidthColumn] = Convert.DBNull;
            }

            public void SetJavascriptCodeNull()
            {
                base[this.tableAnswerTypes.JavascriptCodeColumn] = Convert.DBNull;
            }

            public void SetJavascriptErrorMessageNull()
            {
                base[this.tableAnswerTypes.JavascriptErrorMessageColumn] = Convert.DBNull;
            }

            public void SetJavascriptFunctionNameNull()
            {
                base[this.tableAnswerTypes.JavascriptFunctionNameColumn] = Convert.DBNull;
            }

            public void SetPublicFieldResultsNull()
            {
                base[this.tableAnswerTypes.PublicFieldResultsColumn] = Convert.DBNull;
            }

            public void SetTypeAssemblyNull()
            {
                base[this.tableAnswerTypes.TypeAssemblyColumn] = Convert.DBNull;
            }

            public void SetTypeModeNull()
            {
                base[this.tableAnswerTypes.TypeModeColumn] = Convert.DBNull;
            }

            public void SetTypeNameSpaceNull()
            {
                base[this.tableAnswerTypes.TypeNameSpaceColumn] = Convert.DBNull;
            }

            public void SetXmlDataSourceNull()
            {
                base[this.tableAnswerTypes.XmlDataSourceColumn] = Convert.DBNull;
            }

            public int AnswerTypeId
            {
                get
                {
                    return (int) base[this.tableAnswerTypes.AnswerTypeIdColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.AnswerTypeIdColumn] = value;
                }
            }

            public bool BuiltIn
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswerTypes.BuiltInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswerTypes.BuiltInColumn] = value;
                }
            }

            public string DataSource
            {
                get
                {
                    if (this.IsDataSourceNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswerTypes.DataSourceColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.DataSourceColumn] = value;
                }
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswerTypes.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswerTypes.DescriptionColumn] = value;
                }
            }

            public int FieldHeight
            {
                get
                {
                    if (this.IsFieldHeightNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableAnswerTypes.FieldHeightColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.FieldHeightColumn] = value;
                }
            }

            public int FieldLength
            {
                get
                {
                    if (this.IsFieldLengthNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableAnswerTypes.FieldLengthColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.FieldLengthColumn] = value;
                }
            }

            public int FieldWidth
            {
                get
                {
                    if (this.IsFieldWidthNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableAnswerTypes.FieldWidthColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.FieldWidthColumn] = value;
                }
            }

            public string JavascriptCode
            {
                get
                {
                    if (this.IsJavascriptCodeNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswerTypes.JavascriptCodeColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.JavascriptCodeColumn] = value;
                }
            }

            public string JavascriptErrorMessage
            {
                get
                {
                    if (this.IsJavascriptErrorMessageNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswerTypes.JavascriptErrorMessageColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.JavascriptErrorMessageColumn] = value;
                }
            }

            public string JavascriptFunctionName
            {
                get
                {
                    if (this.IsJavascriptFunctionNameNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswerTypes.JavascriptFunctionNameColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.JavascriptFunctionNameColumn] = value;
                }
            }

            public bool PublicFieldResults
            {
                get
                {
                    if (this.IsPublicFieldResultsNull())
                    {
                        return false;
                    }
                    return (bool) base[this.tableAnswerTypes.PublicFieldResultsColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.PublicFieldResultsColumn] = value;
                }
            }

            public string TypeAssembly
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswerTypes.TypeAssemblyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswerTypes.TypeAssemblyColumn] = value;
                }
            }

            public int TypeMode
            {
                get
                {
                    if (this.IsTypeModeNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableAnswerTypes.TypeModeColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.TypeModeColumn] = value;
                }
            }

            public string TypeNameSpace
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswerTypes.TypeNameSpaceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswerTypes.TypeNameSpaceColumn] = value;
                }
            }

            public string XmlDataSource
            {
                get
                {
                    if (this.IsXmlDataSourceNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswerTypes.XmlDataSourceColumn];
                }
                set
                {
                    base[this.tableAnswerTypes.XmlDataSourceColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswerTypesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private AnswerTypeData.AnswerTypesRow eventRow;

            public AnswerTypesRowChangeEvent(AnswerTypeData.AnswerTypesRow row, DataRowAction action)
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

            public AnswerTypeData.AnswerTypesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswerTypesRowChangeEventHandler(object sender, AnswerTypeData.AnswerTypesRowChangeEvent e);
    }
}

