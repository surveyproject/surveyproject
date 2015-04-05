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

    [Serializable, DesignerCategory("code"), ToolboxItem(true), DebuggerStepThrough]
    public class SkipLogicRuleData : DataSet
    {
        private SkipLogicRulesDataTable tableSkipLogicRules;

        public SkipLogicRuleData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected SkipLogicRuleData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["SkipLogicRules"] != null)
                {
                    base.Tables.Add(new SkipLogicRulesDataTable(dataSet.Tables["SkipLogicRules"]));
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
            SkipLogicRuleData data = (SkipLogicRuleData) base.Clone();
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
            base.DataSetName = "SkipLogicRuleData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/SkipLogicRuleData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableSkipLogicRules = new SkipLogicRulesDataTable();
            base.Tables.Add(this.tableSkipLogicRules);
        }

        internal void InitVars()
        {
            this.tableSkipLogicRules = (SkipLogicRulesDataTable) base.Tables["SkipLogicRules"];
            if (this.tableSkipLogicRules != null)
            {
                this.tableSkipLogicRules.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["SkipLogicRules"] != null)
            {
                base.Tables.Add(new SkipLogicRulesDataTable(dataSet.Tables["SkipLogicRules"]));
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

        private bool ShouldSerializeSkipLogicRules()
        {
            return false;
        }

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SkipLogicRulesDataTable SkipLogicRules
        {
            get
            {
                return this.tableSkipLogicRules;
            }
        }

        [DebuggerStepThrough]
        public class SkipLogicRulesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnConditionalOperator;
            private DataColumn columnExpressionOperator;
            private DataColumn columnQuestionId;
            private DataColumn columnScore;
            private DataColumn columnScoreMax;
            private DataColumn columnSkipLogicRuleId;
            private DataColumn columnSkipQuestionId;
            private DataColumn columnTextFilter;

            public event SkipLogicRuleData.SkipLogicRulesRowChangeEventHandler SkipLogicRulesRowChanged;

            public event SkipLogicRuleData.SkipLogicRulesRowChangeEventHandler SkipLogicRulesRowChanging;

            public event SkipLogicRuleData.SkipLogicRulesRowChangeEventHandler SkipLogicRulesRowDeleted;

            public event SkipLogicRuleData.SkipLogicRulesRowChangeEventHandler SkipLogicRulesRowDeleting;

            internal SkipLogicRulesDataTable() : base("SkipLogicRules")
            {
                this.InitClass();
            }

            internal SkipLogicRulesDataTable(DataTable table) : base(table.TableName)
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

            public void AddSkipLogicRulesRow(SkipLogicRuleData.SkipLogicRulesRow row)
            {
                base.Rows.Add(row);
            }

            public SkipLogicRuleData.SkipLogicRulesRow AddSkipLogicRulesRow(int SkipQuestionId, int ConditionalOperator, int AnswerId, int QuestionId, string TextFilter, int Score, int ScoreMax, int ExpressionOperator)
            {
                SkipLogicRuleData.SkipLogicRulesRow row = (SkipLogicRuleData.SkipLogicRulesRow) base.NewRow();
                object[] objArray = new object[9];
                objArray[1] = SkipQuestionId;
                objArray[2] = ConditionalOperator;
                objArray[3] = AnswerId;
                objArray[4] = QuestionId;
                objArray[5] = TextFilter;
                objArray[6] = Score;
                objArray[7] = ScoreMax;
                objArray[8] = ExpressionOperator;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                SkipLogicRuleData.SkipLogicRulesDataTable table = (SkipLogicRuleData.SkipLogicRulesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new SkipLogicRuleData.SkipLogicRulesDataTable();
            }

            public SkipLogicRuleData.SkipLogicRulesRow FindBySkipLogicRuleId(int SkipLogicRuleId)
            {
                return (SkipLogicRuleData.SkipLogicRulesRow) base.Rows.Find(new object[] { SkipLogicRuleId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(SkipLogicRuleData.SkipLogicRulesRow);
            }

            private void InitClass()
            {
                this.columnSkipLogicRuleId = new DataColumn("SkipLogicRuleId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSkipLogicRuleId);
                this.columnSkipQuestionId = new DataColumn("SkipQuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSkipQuestionId);
                this.columnConditionalOperator = new DataColumn("ConditionalOperator", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnConditionalOperator);
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnTextFilter = new DataColumn("TextFilter", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTextFilter);
                this.columnScore = new DataColumn("Score", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnScore);
                this.columnScoreMax = new DataColumn("ScoreMax", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnScoreMax);
                this.columnExpressionOperator = new DataColumn("ExpressionOperator", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnExpressionOperator);
                base.Constraints.Add(new UniqueConstraint("SkipLogicRuleDataKey1", new DataColumn[] { this.columnSkipLogicRuleId }, true));
                this.columnSkipLogicRuleId.AutoIncrement = true;
                this.columnSkipLogicRuleId.AllowDBNull = false;
                this.columnSkipLogicRuleId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnSkipLogicRuleId = base.Columns["SkipLogicRuleId"];
                this.columnSkipQuestionId = base.Columns["SkipQuestionId"];
                this.columnConditionalOperator = base.Columns["ConditionalOperator"];
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnTextFilter = base.Columns["TextFilter"];
                this.columnScore = base.Columns["Score"];
                this.columnScoreMax = base.Columns["ScoreMax"];
                this.columnExpressionOperator = base.Columns["ExpressionOperator"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new SkipLogicRuleData.SkipLogicRulesRow(builder);
            }

            public SkipLogicRuleData.SkipLogicRulesRow NewSkipLogicRulesRow()
            {
                return (SkipLogicRuleData.SkipLogicRulesRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.SkipLogicRulesRowChanged != null)
                {
                    this.SkipLogicRulesRowChanged(this, new SkipLogicRuleData.SkipLogicRulesRowChangeEvent((SkipLogicRuleData.SkipLogicRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.SkipLogicRulesRowChanging != null)
                {
                    this.SkipLogicRulesRowChanging(this, new SkipLogicRuleData.SkipLogicRulesRowChangeEvent((SkipLogicRuleData.SkipLogicRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.SkipLogicRulesRowDeleted != null)
                {
                    this.SkipLogicRulesRowDeleted(this, new SkipLogicRuleData.SkipLogicRulesRowChangeEvent((SkipLogicRuleData.SkipLogicRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.SkipLogicRulesRowDeleting != null)
                {
                    this.SkipLogicRulesRowDeleting(this, new SkipLogicRuleData.SkipLogicRulesRowChangeEvent((SkipLogicRuleData.SkipLogicRulesRow) e.Row, e.Action));
                }
            }

            public void RemoveSkipLogicRulesRow(SkipLogicRuleData.SkipLogicRulesRow row)
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

            internal DataColumn ConditionalOperatorColumn
            {
                get
                {
                    return this.columnConditionalOperator;
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

            internal DataColumn ExpressionOperatorColumn
            {
                get
                {
                    return this.columnExpressionOperator;
                }
            }

            public SkipLogicRuleData.SkipLogicRulesRow this[int index]
            {
                get
                {
                    return (SkipLogicRuleData.SkipLogicRulesRow) base.Rows[index];
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn ScoreColumn
            {
                get
                {
                    return this.columnScore;
                }
            }

            internal DataColumn ScoreMaxColumn
            {
                get
                {
                    return this.columnScoreMax;
                }
            }

            internal DataColumn SkipLogicRuleIdColumn
            {
                get
                {
                    return this.columnSkipLogicRuleId;
                }
            }

            internal DataColumn SkipQuestionIdColumn
            {
                get
                {
                    return this.columnSkipQuestionId;
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
        public class SkipLogicRulesRow : DataRow
        {
            private SkipLogicRuleData.SkipLogicRulesDataTable tableSkipLogicRules;

            internal SkipLogicRulesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableSkipLogicRules = (SkipLogicRuleData.SkipLogicRulesDataTable) base.Table;
            }

            public bool IsAnswerIdNull()
            {
                return base.IsNull(this.tableSkipLogicRules.AnswerIdColumn);
            }

            public bool IsConditionalOperatorNull()
            {
                return base.IsNull(this.tableSkipLogicRules.ConditionalOperatorColumn);
            }

            public bool IsExpressionOperatorNull()
            {
                return base.IsNull(this.tableSkipLogicRules.ExpressionOperatorColumn);
            }

            public bool IsQuestionIdNull()
            {
                return base.IsNull(this.tableSkipLogicRules.QuestionIdColumn);
            }

            public bool IsScoreMaxNull()
            {
                return base.IsNull(this.tableSkipLogicRules.ScoreMaxColumn);
            }

            public bool IsScoreNull()
            {
                return base.IsNull(this.tableSkipLogicRules.ScoreColumn);
            }

            public bool IsSkipQuestionIdNull()
            {
                return base.IsNull(this.tableSkipLogicRules.SkipQuestionIdColumn);
            }

            public bool IsTextFilterNull()
            {
                return base.IsNull(this.tableSkipLogicRules.TextFilterColumn);
            }

            public void SetAnswerIdNull()
            {
                base[this.tableSkipLogicRules.AnswerIdColumn] = Convert.DBNull;
            }

            public void SetConditionalOperatorNull()
            {
                base[this.tableSkipLogicRules.ConditionalOperatorColumn] = Convert.DBNull;
            }

            public void SetExpressionOperatorNull()
            {
                base[this.tableSkipLogicRules.ExpressionOperatorColumn] = Convert.DBNull;
            }

            public void SetQuestionIdNull()
            {
                base[this.tableSkipLogicRules.QuestionIdColumn] = Convert.DBNull;
            }

            public void SetScoreMaxNull()
            {
                base[this.tableSkipLogicRules.ScoreMaxColumn] = Convert.DBNull;
            }

            public void SetScoreNull()
            {
                base[this.tableSkipLogicRules.ScoreColumn] = Convert.DBNull;
            }

            public void SetSkipQuestionIdNull()
            {
                base[this.tableSkipLogicRules.SkipQuestionIdColumn] = Convert.DBNull;
            }

            public void SetTextFilterNull()
            {
                base[this.tableSkipLogicRules.TextFilterColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableSkipLogicRules.AnswerIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableSkipLogicRules.AnswerIdColumn] = value;
                }
            }

            public int ConditionalOperator
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableSkipLogicRules.ConditionalOperatorColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableSkipLogicRules.ConditionalOperatorColumn] = value;
                }
            }

            public int ExpressionOperator
            {
                get
                {
                    if (this.IsExpressionOperatorNull())
                    {
                        return 2;
                    }
                    return (int) base[this.tableSkipLogicRules.ExpressionOperatorColumn];
                }
                set
                {
                    base[this.tableSkipLogicRules.ExpressionOperatorColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableSkipLogicRules.QuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableSkipLogicRules.QuestionIdColumn] = value;
                }
            }

            public int Score
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableSkipLogicRules.ScoreColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableSkipLogicRules.ScoreColumn] = value;
                }
            }

            public int ScoreMax
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableSkipLogicRules.ScoreMaxColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableSkipLogicRules.ScoreMaxColumn] = value;
                }
            }

            public int SkipLogicRuleId
            {
                get
                {
                    return (int) base[this.tableSkipLogicRules.SkipLogicRuleIdColumn];
                }
                set
                {
                    base[this.tableSkipLogicRules.SkipLogicRuleIdColumn] = value;
                }
            }

            public int SkipQuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableSkipLogicRules.SkipQuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableSkipLogicRules.SkipQuestionIdColumn] = value;
                }
            }

            public string TextFilter
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSkipLogicRules.TextFilterColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSkipLogicRules.TextFilterColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class SkipLogicRulesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private SkipLogicRuleData.SkipLogicRulesRow eventRow;

            public SkipLogicRulesRowChangeEvent(SkipLogicRuleData.SkipLogicRulesRow row, DataRowAction action)
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

            public SkipLogicRuleData.SkipLogicRulesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void SkipLogicRulesRowChangeEventHandler(object sender, SkipLogicRuleData.SkipLogicRulesRowChangeEvent e);
    }
}

