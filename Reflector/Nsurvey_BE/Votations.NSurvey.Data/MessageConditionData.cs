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

    [Serializable, DebuggerStepThrough, ToolboxItem(true), DesignerCategory("code")]
    public class MessageConditionData : DataSet
    {
        private MessageConditionsDataTable tableMessageConditions;

        public MessageConditionData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected MessageConditionData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["MessageConditions"] != null)
                {
                    base.Tables.Add(new MessageConditionsDataTable(dataSet.Tables["MessageConditions"]));
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
            MessageConditionData data = (MessageConditionData) base.Clone();
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
            base.DataSetName = "MessageConditionData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/MessageConditionData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableMessageConditions = new MessageConditionsDataTable();
            base.Tables.Add(this.tableMessageConditions);
        }

        internal void InitVars()
        {
            this.tableMessageConditions = (MessageConditionsDataTable) base.Tables["MessageConditions"];
            if (this.tableMessageConditions != null)
            {
                this.tableMessageConditions.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["MessageConditions"] != null)
            {
                base.Tables.Add(new MessageConditionsDataTable(dataSet.Tables["MessageConditions"]));
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

        private bool ShouldSerializeMessageConditions()
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
        public MessageConditionsDataTable MessageConditions
        {
            get
            {
                return this.tableMessageConditions;
            }
        }

        [DebuggerStepThrough]
        public class MessageConditionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnConditionalOperator;
            private DataColumn columnExpressionOperator;
            private DataColumn columnMessageConditionalOperator;
            private DataColumn columnMessageConditionId;
            private DataColumn columnQuestionId;
            private DataColumn columnScore;
            private DataColumn columnScoreMax;
            private DataColumn columnSurveyId;
            private DataColumn columnTextFilter;
            private DataColumn columnThankYouMessage;

            public event MessageConditionData.MessageConditionsRowChangeEventHandler MessageConditionsRowChanged;

            public event MessageConditionData.MessageConditionsRowChangeEventHandler MessageConditionsRowChanging;

            public event MessageConditionData.MessageConditionsRowChangeEventHandler MessageConditionsRowDeleted;

            public event MessageConditionData.MessageConditionsRowChangeEventHandler MessageConditionsRowDeleting;

            internal MessageConditionsDataTable() : base("MessageConditions")
            {
                this.InitClass();
            }

            internal MessageConditionsDataTable(DataTable table) : base(table.TableName)
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

            public void AddMessageConditionsRow(MessageConditionData.MessageConditionsRow row)
            {
                base.Rows.Add(row);
            }

            public MessageConditionData.MessageConditionsRow AddMessageConditionsRow(int MessageConditionalOperator, int SurveyId, int QuestionId, int ConditionalOperator, int AnswerId, string TextFilter, string ThankYouMessage, int Score, int ScoreMax, int ExpressionOperator)
            {
                MessageConditionData.MessageConditionsRow row = (MessageConditionData.MessageConditionsRow) base.NewRow();
                object[] objArray = new object[11];
                objArray[1] = MessageConditionalOperator;
                objArray[2] = SurveyId;
                objArray[3] = QuestionId;
                objArray[4] = ConditionalOperator;
                objArray[5] = AnswerId;
                objArray[6] = TextFilter;
                objArray[7] = ThankYouMessage;
                objArray[8] = Score;
                objArray[9] = ScoreMax;
                objArray[10] = ExpressionOperator;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                MessageConditionData.MessageConditionsDataTable table = (MessageConditionData.MessageConditionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new MessageConditionData.MessageConditionsDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(MessageConditionData.MessageConditionsRow);
            }

            private void InitClass()
            {
                this.columnMessageConditionId = new DataColumn("MessageConditionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnMessageConditionId);
                this.columnMessageConditionalOperator = new DataColumn("MessageConditionalOperator", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnMessageConditionalOperator);
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnConditionalOperator = new DataColumn("ConditionalOperator", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnConditionalOperator);
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnTextFilter = new DataColumn("TextFilter", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTextFilter);
                this.columnThankYouMessage = new DataColumn("ThankYouMessage", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnThankYouMessage);
                this.columnScore = new DataColumn("Score", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnScore);
                this.columnScoreMax = new DataColumn("ScoreMax", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnScoreMax);
                this.columnExpressionOperator = new DataColumn("ExpressionOperator", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnExpressionOperator);
                this.columnMessageConditionId.AutoIncrement = true;
                this.columnMessageConditionId.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnMessageConditionId = base.Columns["MessageConditionId"];
                this.columnMessageConditionalOperator = base.Columns["MessageConditionalOperator"];
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnConditionalOperator = base.Columns["ConditionalOperator"];
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnTextFilter = base.Columns["TextFilter"];
                this.columnThankYouMessage = base.Columns["ThankYouMessage"];
                this.columnScore = base.Columns["Score"];
                this.columnScoreMax = base.Columns["ScoreMax"];
                this.columnExpressionOperator = base.Columns["ExpressionOperator"];
            }

            public MessageConditionData.MessageConditionsRow NewMessageConditionsRow()
            {
                return (MessageConditionData.MessageConditionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new MessageConditionData.MessageConditionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.MessageConditionsRowChanged != null)
                {
                    this.MessageConditionsRowChanged(this, new MessageConditionData.MessageConditionsRowChangeEvent((MessageConditionData.MessageConditionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.MessageConditionsRowChanging != null)
                {
                    this.MessageConditionsRowChanging(this, new MessageConditionData.MessageConditionsRowChangeEvent((MessageConditionData.MessageConditionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.MessageConditionsRowDeleted != null)
                {
                    this.MessageConditionsRowDeleted(this, new MessageConditionData.MessageConditionsRowChangeEvent((MessageConditionData.MessageConditionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.MessageConditionsRowDeleting != null)
                {
                    this.MessageConditionsRowDeleting(this, new MessageConditionData.MessageConditionsRowChangeEvent((MessageConditionData.MessageConditionsRow) e.Row, e.Action));
                }
            }

            public void RemoveMessageConditionsRow(MessageConditionData.MessageConditionsRow row)
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

            public MessageConditionData.MessageConditionsRow this[int index]
            {
                get
                {
                    return (MessageConditionData.MessageConditionsRow) base.Rows[index];
                }
            }

            internal DataColumn MessageConditionalOperatorColumn
            {
                get
                {
                    return this.columnMessageConditionalOperator;
                }
            }

            internal DataColumn MessageConditionIdColumn
            {
                get
                {
                    return this.columnMessageConditionId;
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

            internal DataColumn SurveyIdColumn
            {
                get
                {
                    return this.columnSurveyId;
                }
            }

            internal DataColumn TextFilterColumn
            {
                get
                {
                    return this.columnTextFilter;
                }
            }

            internal DataColumn ThankYouMessageColumn
            {
                get
                {
                    return this.columnThankYouMessage;
                }
            }
        }

        [DebuggerStepThrough]
        public class MessageConditionsRow : DataRow
        {
            private MessageConditionData.MessageConditionsDataTable tableMessageConditions;

            internal MessageConditionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableMessageConditions = (MessageConditionData.MessageConditionsDataTable) base.Table;
            }

            public bool IsAnswerIdNull()
            {
                return base.IsNull(this.tableMessageConditions.AnswerIdColumn);
            }

            public bool IsConditionalOperatorNull()
            {
                return base.IsNull(this.tableMessageConditions.ConditionalOperatorColumn);
            }

            public bool IsExpressionOperatorNull()
            {
                return base.IsNull(this.tableMessageConditions.ExpressionOperatorColumn);
            }

            public bool IsMessageConditionalOperatorNull()
            {
                return base.IsNull(this.tableMessageConditions.MessageConditionalOperatorColumn);
            }

            public bool IsQuestionIdNull()
            {
                return base.IsNull(this.tableMessageConditions.QuestionIdColumn);
            }

            public bool IsScoreMaxNull()
            {
                return base.IsNull(this.tableMessageConditions.ScoreMaxColumn);
            }

            public bool IsScoreNull()
            {
                return base.IsNull(this.tableMessageConditions.ScoreColumn);
            }

            public bool IsSurveyIdNull()
            {
                return base.IsNull(this.tableMessageConditions.SurveyIdColumn);
            }

            public bool IsTextFilterNull()
            {
                return base.IsNull(this.tableMessageConditions.TextFilterColumn);
            }

            public bool IsThankYouMessageNull()
            {
                return base.IsNull(this.tableMessageConditions.ThankYouMessageColumn);
            }

            public void SetAnswerIdNull()
            {
                base[this.tableMessageConditions.AnswerIdColumn] = Convert.DBNull;
            }

            public void SetConditionalOperatorNull()
            {
                base[this.tableMessageConditions.ConditionalOperatorColumn] = Convert.DBNull;
            }

            public void SetExpressionOperatorNull()
            {
                base[this.tableMessageConditions.ExpressionOperatorColumn] = Convert.DBNull;
            }

            public void SetMessageConditionalOperatorNull()
            {
                base[this.tableMessageConditions.MessageConditionalOperatorColumn] = Convert.DBNull;
            }

            public void SetQuestionIdNull()
            {
                base[this.tableMessageConditions.QuestionIdColumn] = Convert.DBNull;
            }

            public void SetScoreMaxNull()
            {
                base[this.tableMessageConditions.ScoreMaxColumn] = Convert.DBNull;
            }

            public void SetScoreNull()
            {
                base[this.tableMessageConditions.ScoreColumn] = Convert.DBNull;
            }

            public void SetSurveyIdNull()
            {
                base[this.tableMessageConditions.SurveyIdColumn] = Convert.DBNull;
            }

            public void SetTextFilterNull()
            {
                base[this.tableMessageConditions.TextFilterColumn] = Convert.DBNull;
            }

            public void SetThankYouMessageNull()
            {
                base[this.tableMessageConditions.ThankYouMessageColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableMessageConditions.AnswerIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableMessageConditions.AnswerIdColumn] = value;
                }
            }

            public int ConditionalOperator
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableMessageConditions.ConditionalOperatorColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableMessageConditions.ConditionalOperatorColumn] = value;
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
                    return (int) base[this.tableMessageConditions.ExpressionOperatorColumn];
                }
                set
                {
                    base[this.tableMessageConditions.ExpressionOperatorColumn] = value;
                }
            }

            public int MessageConditionalOperator
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableMessageConditions.MessageConditionalOperatorColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableMessageConditions.MessageConditionalOperatorColumn] = value;
                }
            }

            public int MessageConditionId
            {
                get
                {
                    return (int) base[this.tableMessageConditions.MessageConditionIdColumn];
                }
                set
                {
                    base[this.tableMessageConditions.MessageConditionIdColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableMessageConditions.QuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableMessageConditions.QuestionIdColumn] = value;
                }
            }

            public int Score
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableMessageConditions.ScoreColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableMessageConditions.ScoreColumn] = value;
                }
            }

            public int ScoreMax
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableMessageConditions.ScoreMaxColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableMessageConditions.ScoreMaxColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableMessageConditions.SurveyIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableMessageConditions.SurveyIdColumn] = value;
                }
            }

            public string TextFilter
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableMessageConditions.TextFilterColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableMessageConditions.TextFilterColumn] = value;
                }
            }

            public string ThankYouMessage
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableMessageConditions.ThankYouMessageColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableMessageConditions.ThankYouMessageColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class MessageConditionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private MessageConditionData.MessageConditionsRow eventRow;

            public MessageConditionsRowChangeEvent(MessageConditionData.MessageConditionsRow row, DataRowAction action)
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

            public MessageConditionData.MessageConditionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void MessageConditionsRowChangeEventHandler(object sender, MessageConditionData.MessageConditionsRowChangeEvent e);
    }
}

