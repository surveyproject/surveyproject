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
    public class QuestionSelectionModeData : DataSet
    {
        private QuestionSelectionModesDataTable tableQuestionSelectionModes;

        public QuestionSelectionModeData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected QuestionSelectionModeData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["QuestionSelectionModes"] != null)
                {
                    base.Tables.Add(new QuestionSelectionModesDataTable(dataSet.Tables["QuestionSelectionModes"]));
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
            QuestionSelectionModeData data = (QuestionSelectionModeData) base.Clone();
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
            base.DataSetName = "QuestionSelectionModeData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/QuestionSelectionModeData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableQuestionSelectionModes = new QuestionSelectionModesDataTable();
            base.Tables.Add(this.tableQuestionSelectionModes);
        }

        internal void InitVars()
        {
            this.tableQuestionSelectionModes = (QuestionSelectionModesDataTable) base.Tables["QuestionSelectionModes"];
            if (this.tableQuestionSelectionModes != null)
            {
                this.tableQuestionSelectionModes.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["QuestionSelectionModes"] != null)
            {
                base.Tables.Add(new QuestionSelectionModesDataTable(dataSet.Tables["QuestionSelectionModes"]));
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

        private bool ShouldSerializeQuestionSelectionModes()
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
        public QuestionSelectionModesDataTable QuestionSelectionModes
        {
            get
            {
                return this.tableQuestionSelectionModes;
            }
        }

        [DebuggerStepThrough]
        public class QuestionSelectionModesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescription;
            private DataColumn columnQuestionSelectionModeId;
            private DataColumn columnTypeMode;

            public event QuestionSelectionModeData.QuestionSelectionModesRowChangeEventHandler QuestionSelectionModesRowChanged;

            public event QuestionSelectionModeData.QuestionSelectionModesRowChangeEventHandler QuestionSelectionModesRowChanging;

            public event QuestionSelectionModeData.QuestionSelectionModesRowChangeEventHandler QuestionSelectionModesRowDeleted;

            public event QuestionSelectionModeData.QuestionSelectionModesRowChangeEventHandler QuestionSelectionModesRowDeleting;

            internal QuestionSelectionModesDataTable() : base("QuestionSelectionModes")
            {
                this.InitClass();
            }

            internal QuestionSelectionModesDataTable(DataTable table) : base(table.TableName)
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

            public void AddQuestionSelectionModesRow(QuestionSelectionModeData.QuestionSelectionModesRow row)
            {
                base.Rows.Add(row);
            }

            public QuestionSelectionModeData.QuestionSelectionModesRow AddQuestionSelectionModesRow(int QuestionSelectionModeId, string Description, int TypeMode)
            {
                QuestionSelectionModeData.QuestionSelectionModesRow row = (QuestionSelectionModeData.QuestionSelectionModesRow) base.NewRow();
                row.ItemArray = new object[] { QuestionSelectionModeId, Description, TypeMode };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                QuestionSelectionModeData.QuestionSelectionModesDataTable table = (QuestionSelectionModeData.QuestionSelectionModesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new QuestionSelectionModeData.QuestionSelectionModesDataTable();
            }

            public QuestionSelectionModeData.QuestionSelectionModesRow FindByQuestionSelectionModeId(int QuestionSelectionModeId)
            {
                return (QuestionSelectionModeData.QuestionSelectionModesRow) base.Rows.Find(new object[] { QuestionSelectionModeId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(QuestionSelectionModeData.QuestionSelectionModesRow);
            }

            private void InitClass()
            {
                this.columnQuestionSelectionModeId = new DataColumn("QuestionSelectionModeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionSelectionModeId);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnTypeMode = new DataColumn("TypeMode", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTypeMode);
                base.Constraints.Add(new UniqueConstraint("QuestionSelectionModeDataKey1", new DataColumn[] { this.columnQuestionSelectionModeId }, true));
                this.columnQuestionSelectionModeId.AutoIncrementSeed = -1L;
                this.columnQuestionSelectionModeId.AutoIncrementStep = -1L;
                this.columnQuestionSelectionModeId.AllowDBNull = false;
                this.columnQuestionSelectionModeId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnQuestionSelectionModeId = base.Columns["QuestionSelectionModeId"];
                this.columnDescription = base.Columns["Description"];
                this.columnTypeMode = base.Columns["TypeMode"];
            }

            public QuestionSelectionModeData.QuestionSelectionModesRow NewQuestionSelectionModesRow()
            {
                return (QuestionSelectionModeData.QuestionSelectionModesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new QuestionSelectionModeData.QuestionSelectionModesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.QuestionSelectionModesRowChanged != null)
                {
                    this.QuestionSelectionModesRowChanged(this, new QuestionSelectionModeData.QuestionSelectionModesRowChangeEvent((QuestionSelectionModeData.QuestionSelectionModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.QuestionSelectionModesRowChanging != null)
                {
                    this.QuestionSelectionModesRowChanging(this, new QuestionSelectionModeData.QuestionSelectionModesRowChangeEvent((QuestionSelectionModeData.QuestionSelectionModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.QuestionSelectionModesRowDeleted != null)
                {
                    this.QuestionSelectionModesRowDeleted(this, new QuestionSelectionModeData.QuestionSelectionModesRowChangeEvent((QuestionSelectionModeData.QuestionSelectionModesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.QuestionSelectionModesRowDeleting != null)
                {
                    this.QuestionSelectionModesRowDeleting(this, new QuestionSelectionModeData.QuestionSelectionModesRowChangeEvent((QuestionSelectionModeData.QuestionSelectionModesRow) e.Row, e.Action));
                }
            }

            public void RemoveQuestionSelectionModesRow(QuestionSelectionModeData.QuestionSelectionModesRow row)
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

            public QuestionSelectionModeData.QuestionSelectionModesRow this[int index]
            {
                get
                {
                    return (QuestionSelectionModeData.QuestionSelectionModesRow) base.Rows[index];
                }
            }

            internal DataColumn QuestionSelectionModeIdColumn
            {
                get
                {
                    return this.columnQuestionSelectionModeId;
                }
            }

            internal DataColumn TypeModeColumn
            {
                get
                {
                    return this.columnTypeMode;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionSelectionModesRow : DataRow
        {
            private QuestionSelectionModeData.QuestionSelectionModesDataTable tableQuestionSelectionModes;

            internal QuestionSelectionModesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableQuestionSelectionModes = (QuestionSelectionModeData.QuestionSelectionModesDataTable) base.Table;
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableQuestionSelectionModes.DescriptionColumn);
            }

            public bool IsTypeModeNull()
            {
                return base.IsNull(this.tableQuestionSelectionModes.TypeModeColumn);
            }

            public void SetDescriptionNull()
            {
                base[this.tableQuestionSelectionModes.DescriptionColumn] = Convert.DBNull;
            }

            public void SetTypeModeNull()
            {
                base[this.tableQuestionSelectionModes.TypeModeColumn] = Convert.DBNull;
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableQuestionSelectionModes.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableQuestionSelectionModes.DescriptionColumn] = value;
                }
            }

            public int QuestionSelectionModeId
            {
                get
                {
                    return (int) base[this.tableQuestionSelectionModes.QuestionSelectionModeIdColumn];
                }
                set
                {
                    base[this.tableQuestionSelectionModes.QuestionSelectionModeIdColumn] = value;
                }
            }

            public int TypeMode
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestionSelectionModes.TypeModeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestionSelectionModes.TypeModeColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionSelectionModesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private QuestionSelectionModeData.QuestionSelectionModesRow eventRow;

            public QuestionSelectionModesRowChangeEvent(QuestionSelectionModeData.QuestionSelectionModesRow row, DataRowAction action)
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

            public QuestionSelectionModeData.QuestionSelectionModesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void QuestionSelectionModesRowChangeEventHandler(object sender, QuestionSelectionModeData.QuestionSelectionModesRowChangeEvent e);
    }
}

