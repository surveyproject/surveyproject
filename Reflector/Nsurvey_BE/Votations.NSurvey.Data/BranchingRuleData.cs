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
    public class BranchingRuleData : DataSet
    {
        private BranchingRulesDataTable tableBranchingRules;

        public BranchingRuleData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected BranchingRuleData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["BranchingRules"] != null)
                {
                    base.Tables.Add(new BranchingRulesDataTable(dataSet.Tables["BranchingRules"]));
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
            BranchingRuleData data = (BranchingRuleData) base.Clone();
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
            base.DataSetName = "BranchingRuleData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/BranchingRuleData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableBranchingRules = new BranchingRulesDataTable();
            base.Tables.Add(this.tableBranchingRules);
        }

        internal void InitVars()
        {
            this.tableBranchingRules = (BranchingRulesDataTable) base.Tables["BranchingRules"];
            if (this.tableBranchingRules != null)
            {
                this.tableBranchingRules.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["BranchingRules"] != null)
            {
                base.Tables.Add(new BranchingRulesDataTable(dataSet.Tables["BranchingRules"]));
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

        private bool ShouldSerializeBranchingRules()
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
        public BranchingRulesDataTable BranchingRules
        {
            get
            {
                return this.tableBranchingRules;
            }
        }

        [DebuggerStepThrough]
        public class BranchingRulesDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnAnswerText;
            private DataColumn columnBranchingRuleId;
            private DataColumn columnConditionalOperator;
            private DataColumn columnExpressionOperator;
            private DataColumn columnPageNumber;
            private DataColumn columnQuestionId;
            private DataColumn columnQuestionText;
            private DataColumn columnScore;
            private DataColumn columnScoreMax;
            private DataColumn columnTargetPageNumber;
            private DataColumn columnTextFilter;

            public event BranchingRuleData.BranchingRulesRowChangeEventHandler BranchingRulesRowChanged;

            public event BranchingRuleData.BranchingRulesRowChangeEventHandler BranchingRulesRowChanging;

            public event BranchingRuleData.BranchingRulesRowChangeEventHandler BranchingRulesRowDeleted;

            public event BranchingRuleData.BranchingRulesRowChangeEventHandler BranchingRulesRowDeleting;

            internal BranchingRulesDataTable() : base("BranchingRules")
            {
                this.InitClass();
            }

            internal BranchingRulesDataTable(DataTable table) : base(table.TableName)
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

            public void AddBranchingRulesRow(BranchingRuleData.BranchingRulesRow row)
            {
                base.Rows.Add(row);
            }

            public BranchingRuleData.BranchingRulesRow AddBranchingRulesRow(int PageNumber, int TargetPageNumber, int AnswerId, int QuestionId, string TextFilter, int ConditionalOperator, string QuestionText, string AnswerText, int Score, int ScoreMax, int ExpressionOperator)
            {
                BranchingRuleData.BranchingRulesRow row = (BranchingRuleData.BranchingRulesRow) base.NewRow();
                object[] objArray = new object[12];
                objArray[1] = PageNumber;
                objArray[2] = TargetPageNumber;
                objArray[3] = AnswerId;
                objArray[4] = QuestionId;
                objArray[5] = TextFilter;
                objArray[6] = ConditionalOperator;
                objArray[7] = QuestionText;
                objArray[8] = AnswerText;
                objArray[9] = Score;
                objArray[10] = ScoreMax;
                objArray[11] = ExpressionOperator;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                BranchingRuleData.BranchingRulesDataTable table = (BranchingRuleData.BranchingRulesDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new BranchingRuleData.BranchingRulesDataTable();
            }

            public BranchingRuleData.BranchingRulesRow FindByBranchingRuleId(int BranchingRuleId)
            {
                return (BranchingRuleData.BranchingRulesRow) base.Rows.Find(new object[] { BranchingRuleId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(BranchingRuleData.BranchingRulesRow);
            }

            private void InitClass()
            {
                this.columnBranchingRuleId = new DataColumn("BranchingRuleId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnBranchingRuleId);
                this.columnPageNumber = new DataColumn("PageNumber", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnPageNumber);
                this.columnTargetPageNumber = new DataColumn("TargetPageNumber", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTargetPageNumber);
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnTextFilter = new DataColumn("TextFilter", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTextFilter);
                this.columnConditionalOperator = new DataColumn("ConditionalOperator", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnConditionalOperator);
                this.columnQuestionText = new DataColumn("QuestionText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionText);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnScore = new DataColumn("Score", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnScore);
                this.columnScoreMax = new DataColumn("ScoreMax", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnScoreMax);
                this.columnExpressionOperator = new DataColumn("ExpressionOperator", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnExpressionOperator);
                base.Constraints.Add(new UniqueConstraint("BranchingRuleDataKey1", new DataColumn[] { this.columnBranchingRuleId }, true));
                this.columnBranchingRuleId.AutoIncrement = true;
                this.columnBranchingRuleId.AllowDBNull = false;
                this.columnBranchingRuleId.ReadOnly = true;
                this.columnBranchingRuleId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnBranchingRuleId = base.Columns["BranchingRuleId"];
                this.columnPageNumber = base.Columns["PageNumber"];
                this.columnTargetPageNumber = base.Columns["TargetPageNumber"];
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnTextFilter = base.Columns["TextFilter"];
                this.columnConditionalOperator = base.Columns["ConditionalOperator"];
                this.columnQuestionText = base.Columns["QuestionText"];
                this.columnAnswerText = base.Columns["AnswerText"];
                this.columnScore = base.Columns["Score"];
                this.columnScoreMax = base.Columns["ScoreMax"];
                this.columnExpressionOperator = base.Columns["ExpressionOperator"];
            }

            public BranchingRuleData.BranchingRulesRow NewBranchingRulesRow()
            {
                return (BranchingRuleData.BranchingRulesRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new BranchingRuleData.BranchingRulesRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.BranchingRulesRowChanged != null)
                {
                    this.BranchingRulesRowChanged(this, new BranchingRuleData.BranchingRulesRowChangeEvent((BranchingRuleData.BranchingRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.BranchingRulesRowChanging != null)
                {
                    this.BranchingRulesRowChanging(this, new BranchingRuleData.BranchingRulesRowChangeEvent((BranchingRuleData.BranchingRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.BranchingRulesRowDeleted != null)
                {
                    this.BranchingRulesRowDeleted(this, new BranchingRuleData.BranchingRulesRowChangeEvent((BranchingRuleData.BranchingRulesRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.BranchingRulesRowDeleting != null)
                {
                    this.BranchingRulesRowDeleting(this, new BranchingRuleData.BranchingRulesRowChangeEvent((BranchingRuleData.BranchingRulesRow) e.Row, e.Action));
                }
            }

            public void RemoveBranchingRulesRow(BranchingRuleData.BranchingRulesRow row)
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

            internal DataColumn BranchingRuleIdColumn
            {
                get
                {
                    return this.columnBranchingRuleId;
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

            public BranchingRuleData.BranchingRulesRow this[int index]
            {
                get
                {
                    return (BranchingRuleData.BranchingRulesRow) base.Rows[index];
                }
            }

            internal DataColumn PageNumberColumn
            {
                get
                {
                    return this.columnPageNumber;
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

            internal DataColumn TargetPageNumberColumn
            {
                get
                {
                    return this.columnTargetPageNumber;
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
        public class BranchingRulesRow : DataRow
        {
            private BranchingRuleData.BranchingRulesDataTable tableBranchingRules;

            internal BranchingRulesRow(DataRowBuilder rb) : base(rb)
            {
                this.tableBranchingRules = (BranchingRuleData.BranchingRulesDataTable) base.Table;
            }

            public bool IsAnswerIdNull()
            {
                return base.IsNull(this.tableBranchingRules.AnswerIdColumn);
            }

            public bool IsAnswerTextNull()
            {
                return base.IsNull(this.tableBranchingRules.AnswerTextColumn);
            }

            public bool IsConditionalOperatorNull()
            {
                return base.IsNull(this.tableBranchingRules.ConditionalOperatorColumn);
            }

            public bool IsExpressionOperatorNull()
            {
                return base.IsNull(this.tableBranchingRules.ExpressionOperatorColumn);
            }

            public bool IsPageNumberNull()
            {
                return base.IsNull(this.tableBranchingRules.PageNumberColumn);
            }

            public bool IsQuestionIdNull()
            {
                return base.IsNull(this.tableBranchingRules.QuestionIdColumn);
            }

            public bool IsQuestionTextNull()
            {
                return base.IsNull(this.tableBranchingRules.QuestionTextColumn);
            }

            public bool IsScoreMaxNull()
            {
                return base.IsNull(this.tableBranchingRules.ScoreMaxColumn);
            }

            public bool IsScoreNull()
            {
                return base.IsNull(this.tableBranchingRules.ScoreColumn);
            }

            public bool IsTargetPageNumberNull()
            {
                return base.IsNull(this.tableBranchingRules.TargetPageNumberColumn);
            }

            public bool IsTextFilterNull()
            {
                return base.IsNull(this.tableBranchingRules.TextFilterColumn);
            }

            public void SetAnswerIdNull()
            {
                base[this.tableBranchingRules.AnswerIdColumn] = Convert.DBNull;
            }

            public void SetAnswerTextNull()
            {
                base[this.tableBranchingRules.AnswerTextColumn] = Convert.DBNull;
            }

            public void SetConditionalOperatorNull()
            {
                base[this.tableBranchingRules.ConditionalOperatorColumn] = Convert.DBNull;
            }

            public void SetExpressionOperatorNull()
            {
                base[this.tableBranchingRules.ExpressionOperatorColumn] = Convert.DBNull;
            }

            public void SetPageNumberNull()
            {
                base[this.tableBranchingRules.PageNumberColumn] = Convert.DBNull;
            }

            public void SetQuestionIdNull()
            {
                base[this.tableBranchingRules.QuestionIdColumn] = Convert.DBNull;
            }

            public void SetQuestionTextNull()
            {
                base[this.tableBranchingRules.QuestionTextColumn] = Convert.DBNull;
            }

            public void SetScoreMaxNull()
            {
                base[this.tableBranchingRules.ScoreMaxColumn] = Convert.DBNull;
            }

            public void SetScoreNull()
            {
                base[this.tableBranchingRules.ScoreColumn] = Convert.DBNull;
            }

            public void SetTargetPageNumberNull()
            {
                base[this.tableBranchingRules.TargetPageNumberColumn] = Convert.DBNull;
            }

            public void SetTextFilterNull()
            {
                base[this.tableBranchingRules.TextFilterColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableBranchingRules.AnswerIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableBranchingRules.AnswerIdColumn] = value;
                }
            }

            public string AnswerText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableBranchingRules.AnswerTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableBranchingRules.AnswerTextColumn] = value;
                }
            }

            public int BranchingRuleId
            {
                get
                {
                    return (int) base[this.tableBranchingRules.BranchingRuleIdColumn];
                }
                set
                {
                    base[this.tableBranchingRules.BranchingRuleIdColumn] = value;
                }
            }

            public int ConditionalOperator
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableBranchingRules.ConditionalOperatorColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableBranchingRules.ConditionalOperatorColumn] = value;
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
                    return (int) base[this.tableBranchingRules.ExpressionOperatorColumn];
                }
                set
                {
                    base[this.tableBranchingRules.ExpressionOperatorColumn] = value;
                }
            }

            public int PageNumber
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableBranchingRules.PageNumberColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableBranchingRules.PageNumberColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableBranchingRules.QuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableBranchingRules.QuestionIdColumn] = value;
                }
            }

            public string QuestionText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableBranchingRules.QuestionTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableBranchingRules.QuestionTextColumn] = value;
                }
            }

            public int Score
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableBranchingRules.ScoreColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableBranchingRules.ScoreColumn] = value;
                }
            }

            public int ScoreMax
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableBranchingRules.ScoreMaxColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableBranchingRules.ScoreMaxColumn] = value;
                }
            }

            public int TargetPageNumber
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableBranchingRules.TargetPageNumberColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableBranchingRules.TargetPageNumberColumn] = value;
                }
            }

            public string TextFilter
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableBranchingRules.TextFilterColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableBranchingRules.TextFilterColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class BranchingRulesRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private BranchingRuleData.BranchingRulesRow eventRow;

            public BranchingRulesRowChangeEvent(BranchingRuleData.BranchingRulesRow row, DataRowAction action)
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

            public BranchingRuleData.BranchingRulesRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void BranchingRulesRowChangeEventHandler(object sender, BranchingRuleData.BranchingRulesRowChangeEvent e);
    }
}

