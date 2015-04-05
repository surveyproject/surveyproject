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
    public class FilterRuleData : DataSet
    {
        private FilterRulesDataTable tableFilterRules;

        public FilterRuleData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected FilterRuleData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["FilterRules"] != null)
                {
                    base.Tables.Add(new FilterRulesDataTable(dataSet.Tables["FilterRules"]));
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
            FilterRuleData data = (FilterRuleData) base.Clone();
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
            base.DataSetName = "FilterRuleData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/FilterRuleData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableFilterRules = new FilterRulesDataTable();
            base.Tables.Add(this.tableFilterRules);
        }

        internal void InitVars()
        {
            this.tableFilterRules = (FilterRulesDataTable) base.Tables["FilterRules"];
            if (this.tableFilterRules != null)
            {
                this.tableFilterRules.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["FilterRules"] != null)
            {
                base.Tables.Add(new FilterRulesDataTable(dataSet.Tables["FilterRules"]));
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

        private bool ShouldSerializeFilterRules()
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
        public FilterRulesDataTable FilterRules
        {
            get
            {
                return this.tableFilterRules;
            }
        }

        [DebuggerStepThrough]
        public class FilterRulesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnAnswerText;
            private DataColumn columnFilterId;
            private DataColumn columnFilterRuleId;
            private DataColumn columnQuestionId;
            private DataColumn columnQuestionText;
            private DataColumn columnTextFilter;

            public event FilterRuleData.FilterRulesRowChangeEventHandler FilterRulesRowChanged;

            public event FilterRuleData.FilterRulesRowChangeEventHandler FilterRulesRowChanging;

            public event FilterRuleData.FilterRulesRowChangeEventHandler FilterRulesRowDeleted;

            public event FilterRuleData.FilterRulesRowChangeEventHandler FilterRulesRowDeleting;

            internal FilterRulesDataTable() : base("FilterRules")
            {
                this.InitClass();
            }

            internal FilterRulesDataTable(DataTable table) : base(table.TableName)
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

            public void AddFilterRulesRow(FilterRuleData.FilterRulesRow row)
            {
                base.Rows.Add(row);
            }

            public FilterRuleData.FilterRulesRow AddFilterRulesRow(int FilterId, int QuestionId, int AnswerId, string TextFilter, string AnswerText, string QuestionText)
            {
                FilterRuleData.FilterRulesRow row = (FilterRuleData.FilterRulesRow) base.NewRow();
                object[] objArray = new object[7];
                objArray[1] = FilterId;
                objArray[2] = QuestionId;
                objArray[3] = AnswerId;
                objArray[4] = TextFilter;
                objArray[5] = AnswerText;
                objArray[6] = QuestionText;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                FilterRuleData.FilterRulesDataTable table = (FilterRuleData.FilterRulesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new FilterRuleData.FilterRulesDataTable();
            }

            public FilterRuleData.FilterRulesRow FindByFilterRuleId(int FilterRuleId)
            {
                return (FilterRuleData.FilterRulesRow) base.Rows.Find(new object[] { FilterRuleId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(FilterRuleData.FilterRulesRow);
            }

            private void InitClass()
            {
                this.columnFilterRuleId = new DataColumn("FilterRuleId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFilterRuleId);
                this.columnFilterId = new DataColumn("FilterId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFilterId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnTextFilter = new DataColumn("TextFilter", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTextFilter);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnQuestionText = new DataColumn("QuestionText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionText);
                base.Constraints.Add(new UniqueConstraint("FilterRuleDataKey1", new DataColumn[] { this.columnFilterRuleId }, true));
                this.columnFilterRuleId.AutoIncrement = true;
                this.columnFilterRuleId.AllowDBNull = false;
                this.columnFilterRuleId.ReadOnly = true;
                this.columnFilterRuleId.Unique = true;
                this.columnFilterId.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnFilterRuleId = base.Columns["FilterRuleId"];
                this.columnFilterId = base.Columns["FilterId"];
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnTextFilter = base.Columns["TextFilter"];
                this.columnAnswerText = base.Columns["AnswerText"];
                this.columnQuestionText = base.Columns["QuestionText"];
            }

            public FilterRuleData.FilterRulesRow NewFilterRulesRow()
            {
                return (FilterRuleData.FilterRulesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new FilterRuleData.FilterRulesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.FilterRulesRowChanged != null)
                {
                    this.FilterRulesRowChanged(this, new FilterRuleData.FilterRulesRowChangeEvent((FilterRuleData.FilterRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.FilterRulesRowChanging != null)
                {
                    this.FilterRulesRowChanging(this, new FilterRuleData.FilterRulesRowChangeEvent((FilterRuleData.FilterRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.FilterRulesRowDeleted != null)
                {
                    this.FilterRulesRowDeleted(this, new FilterRuleData.FilterRulesRowChangeEvent((FilterRuleData.FilterRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.FilterRulesRowDeleting != null)
                {
                    this.FilterRulesRowDeleting(this, new FilterRuleData.FilterRulesRowChangeEvent((FilterRuleData.FilterRulesRow) e.Row, e.Action));
                }
            }

            public void RemoveFilterRulesRow(FilterRuleData.FilterRulesRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AnswerIdColumn
            {
                get
                {
                    return this.columnAnswerId;
                }
            }

            internal DataColumn AnswerTextColumn
            {
                get
                {
                    return this.columnAnswerText;
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

            internal DataColumn FilterIdColumn
            {
                get
                {
                    return this.columnFilterId;
                }
            }

            internal DataColumn FilterRuleIdColumn
            {
                get
                {
                    return this.columnFilterRuleId;
                }
            }

            public FilterRuleData.FilterRulesRow this[int index]
            {
                get
                {
                    return (FilterRuleData.FilterRulesRow) base.Rows[index];
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn QuestionTextColumn
            {
                get
                {
                    return this.columnQuestionText;
                }
            }

            internal DataColumn TextFilterColumn
            {
                get
                {
                    return this.columnTextFilter;
                }
            }
        }

        [DebuggerStepThrough]
        public class FilterRulesRow : DataRow
        {
            private FilterRuleData.FilterRulesDataTable tableFilterRules;

            internal FilterRulesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableFilterRules = (FilterRuleData.FilterRulesDataTable) base.Table;
            }

            public bool IsAnswerIdNull()
            {
                return base.IsNull(this.tableFilterRules.AnswerIdColumn);
            }

            public bool IsAnswerTextNull()
            {
                return base.IsNull(this.tableFilterRules.AnswerTextColumn);
            }

            public bool IsQuestionIdNull()
            {
                return base.IsNull(this.tableFilterRules.QuestionIdColumn);
            }

            public bool IsQuestionTextNull()
            {
                return base.IsNull(this.tableFilterRules.QuestionTextColumn);
            }

            public bool IsTextFilterNull()
            {
                return base.IsNull(this.tableFilterRules.TextFilterColumn);
            }

            public void SetAnswerIdNull()
            {
                base[this.tableFilterRules.AnswerIdColumn] = Convert.DBNull;
            }

            public void SetAnswerTextNull()
            {
                base[this.tableFilterRules.AnswerTextColumn] = Convert.DBNull;
            }

            public void SetQuestionIdNull()
            {
                base[this.tableFilterRules.QuestionIdColumn] = Convert.DBNull;
            }

            public void SetQuestionTextNull()
            {
                base[this.tableFilterRules.QuestionTextColumn] = Convert.DBNull;
            }

            public void SetTextFilterNull()
            {
                base[this.tableFilterRules.TextFilterColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableFilterRules.AnswerIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableFilterRules.AnswerIdColumn] = value;
                }
            }

            public string AnswerText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableFilterRules.AnswerTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableFilterRules.AnswerTextColumn] = value;
                }
            }

            public int FilterId
            {
                get
                {
                    return (int) base[this.tableFilterRules.FilterIdColumn];
                }
                set
                {
                    base[this.tableFilterRules.FilterIdColumn] = value;
                }
            }

            public int FilterRuleId
            {
                get
                {
                    return (int) base[this.tableFilterRules.FilterRuleIdColumn];
                }
                set
                {
                    base[this.tableFilterRules.FilterRuleIdColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableFilterRules.QuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableFilterRules.QuestionIdColumn] = value;
                }
            }

            public string QuestionText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableFilterRules.QuestionTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableFilterRules.QuestionTextColumn] = value;
                }
            }

            public string TextFilter
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableFilterRules.TextFilterColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableFilterRules.TextFilterColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class FilterRulesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private FilterRuleData.FilterRulesRow eventRow;

            public FilterRulesRowChangeEvent(FilterRuleData.FilterRulesRow row, DataRowAction action)
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

            public FilterRuleData.FilterRulesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void FilterRulesRowChangeEventHandler(object sender, FilterRuleData.FilterRulesRowChangeEvent e);
    }
}

