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
    public class QuestionResultsData : DataSet
    {
        private DataRelation relationQuestionsAnswers;
        private AnswersDataTable tableAnswers;
        private QuestionsDataTable tableQuestions;

        public QuestionResultsData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected QuestionResultsData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["Questions"] != null)
                {
                    base.Tables.Add(new QuestionsDataTable(dataSet.Tables["Questions"]));
                }
                if (dataSet.Tables["Answers"] != null)
                {
                    base.Tables.Add(new AnswersDataTable(dataSet.Tables["Answers"]));
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
            QuestionResultsData data = (QuestionResultsData) base.Clone();
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
            base.DataSetName = "QuestionResultsData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/QuestionResultsData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableQuestions = new QuestionsDataTable();
            base.Tables.Add(this.tableQuestions);
            this.tableAnswers = new AnswersDataTable();
            base.Tables.Add(this.tableAnswers);
            ForeignKeyConstraint constraint = new ForeignKeyConstraint("QuestionsAnswers", new DataColumn[] { this.tableQuestions.QuestionIdColumn }, new DataColumn[] { this.tableAnswers.QuestionIdColumn });
            this.tableAnswers.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            this.relationQuestionsAnswers = new DataRelation("QuestionsAnswers", new DataColumn[] { this.tableQuestions.QuestionIdColumn }, new DataColumn[] { this.tableAnswers.QuestionIdColumn }, false);
            base.Relations.Add(this.relationQuestionsAnswers);
        }

        internal void InitVars()
        {
            this.tableQuestions = (QuestionsDataTable) base.Tables["Questions"];
            if (this.tableQuestions != null)
            {
                this.tableQuestions.InitVars();
            }
            this.tableAnswers = (AnswersDataTable) base.Tables["Answers"];
            if (this.tableAnswers != null)
            {
                this.tableAnswers.InitVars();
            }
            this.relationQuestionsAnswers = base.Relations["QuestionsAnswers"];
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["Questions"] != null)
            {
                base.Tables.Add(new QuestionsDataTable(dataSet.Tables["Questions"]));
            }
            if (dataSet.Tables["Answers"] != null)
            {
                base.Tables.Add(new AnswersDataTable(dataSet.Tables["Answers"]));
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

        private bool ShouldSerializeAnswers()
        {
            return false;
        }

        private bool ShouldSerializeQuestions()
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
        public AnswersDataTable Answers
        {
            get
            {
                return this.tableAnswers;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public QuestionsDataTable Questions
        {
            get
            {
                return this.tableQuestions;
            }
        }

        [DebuggerStepThrough]
        public class AnswersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnAnswerText;
            private DataColumn columnAnswerTypeId;
            private DataColumn columnQuestionId;
            private DataColumn columnRatePart;
            private DataColumn columnTypeMode;
            private DataColumn columnVoterCount;

            public event QuestionResultsData.AnswersRowChangeEventHandler AnswersRowChanged;

            public event QuestionResultsData.AnswersRowChangeEventHandler AnswersRowChanging;

            public event QuestionResultsData.AnswersRowChangeEventHandler AnswersRowDeleted;

            public event QuestionResultsData.AnswersRowChangeEventHandler AnswersRowDeleting;

            internal AnswersDataTable() : base("Answers")
            {
                this.InitClass();
            }

            internal AnswersDataTable(DataTable table) : base(table.TableName)
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

            public void AddAnswersRow(QuestionResultsData.AnswersRow row)
            {
                base.Rows.Add(row);
            }

            public QuestionResultsData.AnswersRow AddAnswersRow(QuestionResultsData.QuestionsRow parentQuestionsRowByQuestionsAnswers, int AnswerTypeId, int VoterCount, int TypeMode, string AnswerText, bool RatePart)
            {
                QuestionResultsData.AnswersRow row = (QuestionResultsData.AnswersRow) base.NewRow();
                object[] objArray = new object[7];
                objArray[1] = parentQuestionsRowByQuestionsAnswers[0];
                objArray[2] = AnswerTypeId;
                objArray[3] = VoterCount;
                objArray[4] = TypeMode;
                objArray[5] = AnswerText;
                objArray[6] = RatePart;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                QuestionResultsData.AnswersDataTable table = (QuestionResultsData.AnswersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new QuestionResultsData.AnswersDataTable();
            }

            public QuestionResultsData.AnswersRow FindByAnswerId(int AnswerId)
            {
                return (QuestionResultsData.AnswersRow) base.Rows.Find(new object[] { AnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(QuestionResultsData.AnswersRow);
            }

            private void InitClass()
            {
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnAnswerTypeId = new DataColumn("AnswerTypeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerTypeId);
                this.columnVoterCount = new DataColumn("VoterCount", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoterCount);
                this.columnTypeMode = new DataColumn("TypeMode", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTypeMode);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnRatePart = new DataColumn("RatePart", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnRatePart);
                base.Constraints.Add(new UniqueConstraint("QuestionResultsDataKey2", new DataColumn[] { this.columnAnswerId }, true));
                this.columnAnswerId.AutoIncrement = true;
                this.columnAnswerId.AllowDBNull = false;
                this.columnAnswerId.ReadOnly = true;
                this.columnAnswerId.Unique = true;
                this.columnQuestionId.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnAnswerTypeId = base.Columns["AnswerTypeId"];
                this.columnVoterCount = base.Columns["VoterCount"];
                this.columnTypeMode = base.Columns["TypeMode"];
                this.columnAnswerText = base.Columns["AnswerText"];
                this.columnRatePart = base.Columns["RatePart"];
            }

            public QuestionResultsData.AnswersRow NewAnswersRow()
            {
                return (QuestionResultsData.AnswersRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new QuestionResultsData.AnswersRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswersRowChanged != null)
                {
                    this.AnswersRowChanged(this, new QuestionResultsData.AnswersRowChangeEvent((QuestionResultsData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswersRowChanging != null)
                {
                    this.AnswersRowChanging(this, new QuestionResultsData.AnswersRowChangeEvent((QuestionResultsData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswersRowDeleted != null)
                {
                    this.AnswersRowDeleted(this, new QuestionResultsData.AnswersRowChangeEvent((QuestionResultsData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswersRowDeleting != null)
                {
                    this.AnswersRowDeleting(this, new QuestionResultsData.AnswersRowChangeEvent((QuestionResultsData.AnswersRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswersRow(QuestionResultsData.AnswersRow row)
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

            internal DataColumn AnswerTypeIdColumn
            {
                get
                {
                    return this.columnAnswerTypeId;
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

            public QuestionResultsData.AnswersRow this[int index]
            {
                get
                {
                    return (QuestionResultsData.AnswersRow) base.Rows[index];
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn RatePartColumn
            {
                get
                {
                    return this.columnRatePart;
                }
            }

            internal DataColumn TypeModeColumn
            {
                get
                {
                    return this.columnTypeMode;
                }
            }

            internal DataColumn VoterCountColumn
            {
                get
                {
                    return this.columnVoterCount;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswersRow : DataRow
        {
            private QuestionResultsData.AnswersDataTable tableAnswers;

            internal AnswersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswers = (QuestionResultsData.AnswersDataTable) base.Table;
            }

            public bool IsAnswerTextNull()
            {
                return base.IsNull(this.tableAnswers.AnswerTextColumn);
            }

            public bool IsAnswerTypeIdNull()
            {
                return base.IsNull(this.tableAnswers.AnswerTypeIdColumn);
            }

            public bool IsRatePartNull()
            {
                return base.IsNull(this.tableAnswers.RatePartColumn);
            }

            public bool IsTypeModeNull()
            {
                return base.IsNull(this.tableAnswers.TypeModeColumn);
            }

            public bool IsVoterCountNull()
            {
                return base.IsNull(this.tableAnswers.VoterCountColumn);
            }

            public void SetAnswerTextNull()
            {
                base[this.tableAnswers.AnswerTextColumn] = Convert.DBNull;
            }

            public void SetAnswerTypeIdNull()
            {
                base[this.tableAnswers.AnswerTypeIdColumn] = Convert.DBNull;
            }

            public void SetRatePartNull()
            {
                base[this.tableAnswers.RatePartColumn] = Convert.DBNull;
            }

            public void SetTypeModeNull()
            {
                base[this.tableAnswers.TypeModeColumn] = Convert.DBNull;
            }

            public void SetVoterCountNull()
            {
                base[this.tableAnswers.VoterCountColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    return (int) base[this.tableAnswers.AnswerIdColumn];
                }
                set
                {
                    base[this.tableAnswers.AnswerIdColumn] = value;
                }
            }

            public string AnswerText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswers.AnswerTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswers.AnswerTextColumn] = value;
                }
            }

            public int AnswerTypeId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswers.AnswerTypeIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswers.AnswerTypeIdColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    return (int) base[this.tableAnswers.QuestionIdColumn];
                }
                set
                {
                    base[this.tableAnswers.QuestionIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.QuestionResultsData.QuestionsRow QuestionsRow
            {
                get
                {
                    return (Votations.NSurvey.Data.QuestionResultsData.QuestionsRow) base.GetParentRow(base.Table.ParentRelations["QuestionsAnswers"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["QuestionsAnswers"]);
                }
            }

            public bool RatePart
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswers.RatePartColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswers.RatePartColumn] = value;
                }
            }

            public int TypeMode
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswers.TypeModeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswers.TypeModeColumn] = value;
                }
            }

            public int VoterCount
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswers.VoterCountColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswers.VoterCountColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private QuestionResultsData.AnswersRow eventRow;

            public AnswersRowChangeEvent(QuestionResultsData.AnswersRow row, DataRowAction action)
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

            public QuestionResultsData.AnswersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswersRowChangeEventHandler(object sender, QuestionResultsData.AnswersRowChangeEvent e);

        [DebuggerStepThrough]
        public class QuestionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDisplayOrder;
            private DataColumn columnParentQuestionId;
            private DataColumn columnParentQuestionText;
            private DataColumn columnQuestionId;
            private DataColumn columnQuestionText;
            private DataColumn columnRatingEnabled;
            private DataColumn columnSelectionModeId;
            private DataColumn columnSurveyId;

            public event QuestionResultsData.QuestionsRowChangeEventHandler QuestionsRowChanged;

            public event QuestionResultsData.QuestionsRowChangeEventHandler QuestionsRowChanging;

            public event QuestionResultsData.QuestionsRowChangeEventHandler QuestionsRowDeleted;

            public event QuestionResultsData.QuestionsRowChangeEventHandler QuestionsRowDeleting;

            internal QuestionsDataTable() : base("Questions")
            {
                this.InitClass();
            }

            internal QuestionsDataTable(DataTable table) : base(table.TableName)
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

            public void AddQuestionsRow(QuestionResultsData.QuestionsRow row)
            {
                base.Rows.Add(row);
            }

            public QuestionResultsData.QuestionsRow AddQuestionsRow(int ParentQuestionId, int SurveyId, int SelectionModeId, int DisplayOrder, string QuestionText, bool RatingEnabled, string ParentQuestionText)
            {
                QuestionResultsData.QuestionsRow row = (QuestionResultsData.QuestionsRow) base.NewRow();
                object[] objArray = new object[8];
                objArray[1] = ParentQuestionId;
                objArray[2] = SurveyId;
                objArray[3] = SelectionModeId;
                objArray[4] = DisplayOrder;
                objArray[5] = QuestionText;
                objArray[6] = RatingEnabled;
                objArray[7] = ParentQuestionText;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                QuestionResultsData.QuestionsDataTable table = (QuestionResultsData.QuestionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new QuestionResultsData.QuestionsDataTable();
            }

            public QuestionResultsData.QuestionsRow FindByQuestionId(int QuestionId)
            {
                return (QuestionResultsData.QuestionsRow) base.Rows.Find(new object[] { QuestionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(QuestionResultsData.QuestionsRow);
            }

            private void InitClass()
            {
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnParentQuestionId = new DataColumn("ParentQuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnParentQuestionId);
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnSelectionModeId = new DataColumn("SelectionModeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSelectionModeId);
                this.columnDisplayOrder = new DataColumn("DisplayOrder", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnDisplayOrder);
                this.columnQuestionText = new DataColumn("QuestionText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionText);
                this.columnRatingEnabled = new DataColumn("RatingEnabled", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnRatingEnabled);
                this.columnParentQuestionText = new DataColumn("ParentQuestionText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnParentQuestionText);
                base.Constraints.Add(new UniqueConstraint("QuestionResultsDataKey1", new DataColumn[] { this.columnQuestionId }, true));
                this.columnQuestionId.AutoIncrement = true;
                this.columnQuestionId.AllowDBNull = false;
                this.columnQuestionId.ReadOnly = true;
                this.columnQuestionId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnParentQuestionId = base.Columns["ParentQuestionId"];
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnSelectionModeId = base.Columns["SelectionModeId"];
                this.columnDisplayOrder = base.Columns["DisplayOrder"];
                this.columnQuestionText = base.Columns["QuestionText"];
                this.columnRatingEnabled = base.Columns["RatingEnabled"];
                this.columnParentQuestionText = base.Columns["ParentQuestionText"];
            }

            public QuestionResultsData.QuestionsRow NewQuestionsRow()
            {
                return (QuestionResultsData.QuestionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new QuestionResultsData.QuestionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.QuestionsRowChanged != null)
                {
                    this.QuestionsRowChanged(this, new QuestionResultsData.QuestionsRowChangeEvent((QuestionResultsData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.QuestionsRowChanging != null)
                {
                    this.QuestionsRowChanging(this, new QuestionResultsData.QuestionsRowChangeEvent((QuestionResultsData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.QuestionsRowDeleted != null)
                {
                    this.QuestionsRowDeleted(this, new QuestionResultsData.QuestionsRowChangeEvent((QuestionResultsData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.QuestionsRowDeleting != null)
                {
                    this.QuestionsRowDeleting(this, new QuestionResultsData.QuestionsRowChangeEvent((QuestionResultsData.QuestionsRow) e.Row, e.Action));
                }
            }

            public void RemoveQuestionsRow(QuestionResultsData.QuestionsRow row)
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

            internal DataColumn DisplayOrderColumn
            {
                get
                {
                    return this.columnDisplayOrder;
                }
            }

            public QuestionResultsData.QuestionsRow this[int index]
            {
                get
                {
                    return (QuestionResultsData.QuestionsRow) base.Rows[index];
                }
            }

            internal DataColumn ParentQuestionIdColumn
            {
                get
                {
                    return this.columnParentQuestionId;
                }
            }

            internal DataColumn ParentQuestionTextColumn
            {
                get
                {
                    return this.columnParentQuestionText;
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

            internal DataColumn RatingEnabledColumn
            {
                get
                {
                    return this.columnRatingEnabled;
                }
            }

            internal DataColumn SelectionModeIdColumn
            {
                get
                {
                    return this.columnSelectionModeId;
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
        public class QuestionsRow : DataRow
        {
            private QuestionResultsData.QuestionsDataTable tableQuestions;

            internal QuestionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableQuestions = (QuestionResultsData.QuestionsDataTable) base.Table;
            }

            public QuestionResultsData.AnswersRow[] GetAnswersRows()
            {
                return (QuestionResultsData.AnswersRow[]) base.GetChildRows(base.Table.ChildRelations["QuestionsAnswers"]);
            }

            public bool IsDisplayOrderNull()
            {
                return base.IsNull(this.tableQuestions.DisplayOrderColumn);
            }

            public bool IsParentQuestionIdNull()
            {
                return base.IsNull(this.tableQuestions.ParentQuestionIdColumn);
            }

            public bool IsParentQuestionTextNull()
            {
                return base.IsNull(this.tableQuestions.ParentQuestionTextColumn);
            }

            public bool IsQuestionTextNull()
            {
                return base.IsNull(this.tableQuestions.QuestionTextColumn);
            }

            public bool IsRatingEnabledNull()
            {
                return base.IsNull(this.tableQuestions.RatingEnabledColumn);
            }

            public bool IsSelectionModeIdNull()
            {
                return base.IsNull(this.tableQuestions.SelectionModeIdColumn);
            }

            public bool IsSurveyIdNull()
            {
                return base.IsNull(this.tableQuestions.SurveyIdColumn);
            }

            public void SetDisplayOrderNull()
            {
                base[this.tableQuestions.DisplayOrderColumn] = Convert.DBNull;
            }

            public void SetParentQuestionIdNull()
            {
                base[this.tableQuestions.ParentQuestionIdColumn] = Convert.DBNull;
            }

            public void SetParentQuestionTextNull()
            {
                base[this.tableQuestions.ParentQuestionTextColumn] = Convert.DBNull;
            }

            public void SetQuestionTextNull()
            {
                base[this.tableQuestions.QuestionTextColumn] = Convert.DBNull;
            }

            public void SetRatingEnabledNull()
            {
                base[this.tableQuestions.RatingEnabledColumn] = Convert.DBNull;
            }

            public void SetSelectionModeIdNull()
            {
                base[this.tableQuestions.SelectionModeIdColumn] = Convert.DBNull;
            }

            public void SetSurveyIdNull()
            {
                base[this.tableQuestions.SurveyIdColumn] = Convert.DBNull;
            }

            public int DisplayOrder
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestions.DisplayOrderColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestions.DisplayOrderColumn] = value;
                }
            }

            public int ParentQuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestions.ParentQuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestions.ParentQuestionIdColumn] = value;
                }
            }

            public string ParentQuestionText
            {
                get
                {
                    if (this.IsParentQuestionTextNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestions.ParentQuestionTextColumn];
                }
                set
                {
                    base[this.tableQuestions.ParentQuestionTextColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    return (int) base[this.tableQuestions.QuestionIdColumn];
                }
                set
                {
                    base[this.tableQuestions.QuestionIdColumn] = value;
                }
            }

            public string QuestionText
            {
                get
                {
                    if (this.IsQuestionTextNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestions.QuestionTextColumn];
                }
                set
                {
                    base[this.tableQuestions.QuestionTextColumn] = value;
                }
            }

            public bool RatingEnabled
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableQuestions.RatingEnabledColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableQuestions.RatingEnabledColumn] = value;
                }
            }

            public int SelectionModeId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestions.SelectionModeIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestions.SelectionModeIdColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestions.SurveyIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestions.SurveyIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private QuestionResultsData.QuestionsRow eventRow;

            public QuestionsRowChangeEvent(QuestionResultsData.QuestionsRow row, DataRowAction action)
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

            public QuestionResultsData.QuestionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void QuestionsRowChangeEventHandler(object sender, QuestionResultsData.QuestionsRowChangeEvent e);
    }
}

