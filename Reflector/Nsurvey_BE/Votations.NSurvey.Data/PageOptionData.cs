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
    public class PageOptionData : DataSet
    {
        private PageOptionsDataTable tablePageOptions;

        public PageOptionData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected PageOptionData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["PageOptions"] != null)
                {
                    base.Tables.Add(new PageOptionsDataTable(dataSet.Tables["PageOptions"]));
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
            PageOptionData data = (PageOptionData) base.Clone();
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
            base.DataSetName = "PageOptionData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/PageOptionData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tablePageOptions = new PageOptionsDataTable();
            base.Tables.Add(this.tablePageOptions);
        }

        internal void InitVars()
        {
            this.tablePageOptions = (PageOptionsDataTable) base.Tables["PageOptions"];
            if (this.tablePageOptions != null)
            {
                this.tablePageOptions.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["PageOptions"] != null)
            {
                base.Tables.Add(new PageOptionsDataTable(dataSet.Tables["PageOptions"]));
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

        private bool ShouldSerializePageOptions()
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
        public PageOptionsDataTable PageOptions
        {
            get
            {
                return this.tablePageOptions;
            }
        }

        [DebuggerStepThrough]
        public class PageOptionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnEnableSubmitButton;
            private DataColumn columnPageNumber;
            private DataColumn columnRandomizeQuestions;
            private DataColumn columnSurveyId;

            public event PageOptionData.PageOptionsRowChangeEventHandler PageOptionsRowChanged;

            public event PageOptionData.PageOptionsRowChangeEventHandler PageOptionsRowChanging;

            public event PageOptionData.PageOptionsRowChangeEventHandler PageOptionsRowDeleted;

            public event PageOptionData.PageOptionsRowChangeEventHandler PageOptionsRowDeleting;

            internal PageOptionsDataTable() : base("PageOptions")
            {
                this.InitClass();
            }

            internal PageOptionsDataTable(DataTable table) : base(table.TableName)
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

            public void AddPageOptionsRow(PageOptionData.PageOptionsRow row)
            {
                base.Rows.Add(row);
            }

            public PageOptionData.PageOptionsRow AddPageOptionsRow(int SurveyId, int PageNumber, bool RandomizeQuestions, bool EnableSubmitButton)
            {
                PageOptionData.PageOptionsRow row = (PageOptionData.PageOptionsRow) base.NewRow();
                row.ItemArray = new object[] { SurveyId, PageNumber, RandomizeQuestions, EnableSubmitButton };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                PageOptionData.PageOptionsDataTable table = (PageOptionData.PageOptionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new PageOptionData.PageOptionsDataTable();
            }

            public PageOptionData.PageOptionsRow FindBySurveyIdPageNumber(int SurveyId, int PageNumber)
            {
                return (PageOptionData.PageOptionsRow) base.Rows.Find(new object[] { SurveyId, PageNumber });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(PageOptionData.PageOptionsRow);
            }

            private void InitClass()
            {
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnPageNumber = new DataColumn("PageNumber", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnPageNumber);
                this.columnRandomizeQuestions = new DataColumn("RandomizeQuestions", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnRandomizeQuestions);
                this.columnEnableSubmitButton = new DataColumn("EnableSubmitButton", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnEnableSubmitButton);
                base.Constraints.Add(new UniqueConstraint("PageOptionDataKey1", new DataColumn[] { this.columnSurveyId, this.columnPageNumber }, true));
                this.columnSurveyId.AllowDBNull = false;
                this.columnPageNumber.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnPageNumber = base.Columns["PageNumber"];
                this.columnRandomizeQuestions = base.Columns["RandomizeQuestions"];
                this.columnEnableSubmitButton = base.Columns["EnableSubmitButton"];
            }

            public PageOptionData.PageOptionsRow NewPageOptionsRow()
            {
                return (PageOptionData.PageOptionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new PageOptionData.PageOptionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.PageOptionsRowChanged != null)
                {
                    this.PageOptionsRowChanged(this, new PageOptionData.PageOptionsRowChangeEvent((PageOptionData.PageOptionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.PageOptionsRowChanging != null)
                {
                    this.PageOptionsRowChanging(this, new PageOptionData.PageOptionsRowChangeEvent((PageOptionData.PageOptionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.PageOptionsRowDeleted != null)
                {
                    this.PageOptionsRowDeleted(this, new PageOptionData.PageOptionsRowChangeEvent((PageOptionData.PageOptionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.PageOptionsRowDeleting != null)
                {
                    this.PageOptionsRowDeleting(this, new PageOptionData.PageOptionsRowChangeEvent((PageOptionData.PageOptionsRow) e.Row, e.Action));
                }
            }

            public void RemovePageOptionsRow(PageOptionData.PageOptionsRow row)
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

            internal DataColumn EnableSubmitButtonColumn
            {
                get
                {
                    return this.columnEnableSubmitButton;
                }
            }

            public PageOptionData.PageOptionsRow this[int index]
            {
                get
                {
                    return (PageOptionData.PageOptionsRow) base.Rows[index];
                }
            }

            internal DataColumn PageNumberColumn
            {
                get
                {
                    return this.columnPageNumber;
                }
            }

            internal DataColumn RandomizeQuestionsColumn
            {
                get
                {
                    return this.columnRandomizeQuestions;
                }
            }

            internal DataColumn SurveyIdColumn
            {
                get
                {
                    return this.columnSurveyId;
                }
            }
        }

        [DebuggerStepThrough]
        public class PageOptionsRow : DataRow
        {
            private PageOptionData.PageOptionsDataTable tablePageOptions;

            internal PageOptionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tablePageOptions = (PageOptionData.PageOptionsDataTable) base.Table;
            }

            public bool IsEnableSubmitButtonNull()
            {
                return base.IsNull(this.tablePageOptions.EnableSubmitButtonColumn);
            }

            public bool IsRandomizeQuestionsNull()
            {
                return base.IsNull(this.tablePageOptions.RandomizeQuestionsColumn);
            }

            public void SetEnableSubmitButtonNull()
            {
                base[this.tablePageOptions.EnableSubmitButtonColumn] = Convert.DBNull;
            }

            public void SetRandomizeQuestionsNull()
            {
                base[this.tablePageOptions.RandomizeQuestionsColumn] = Convert.DBNull;
            }

            public bool EnableSubmitButton
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tablePageOptions.EnableSubmitButtonColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tablePageOptions.EnableSubmitButtonColumn] = value;
                }
            }

            public int PageNumber
            {
                get
                {
                    return (int) base[this.tablePageOptions.PageNumberColumn];
                }
                set
                {
                    base[this.tablePageOptions.PageNumberColumn] = value;
                }
            }

            public bool RandomizeQuestions
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tablePageOptions.RandomizeQuestionsColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tablePageOptions.RandomizeQuestionsColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    return (int) base[this.tablePageOptions.SurveyIdColumn];
                }
                set
                {
                    base[this.tablePageOptions.SurveyIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class PageOptionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private PageOptionData.PageOptionsRow eventRow;

            public PageOptionsRowChangeEvent(PageOptionData.PageOptionsRow row, DataRowAction action)
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

            public PageOptionData.PageOptionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void PageOptionsRowChangeEventHandler(object sender, PageOptionData.PageOptionsRowChangeEvent e);
    }
}

