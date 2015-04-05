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

    [Serializable, DebuggerStepThrough, DesignerCategory("code"), ToolboxItem(true)]
    public class PipeData : DataSet
    {
        private AnswersDataTable tableAnswers;
        private QuestionsDataTable tableQuestions;

        public PipeData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected PipeData(SerializationInfo info, StreamingContext context)
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
            PipeData data = (PipeData) base.Clone();
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
            base.DataSetName = "PipeData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/PipeData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableQuestions = new QuestionsDataTable();
            base.Tables.Add(this.tableQuestions);
            this.tableAnswers = new AnswersDataTable();
            base.Tables.Add(this.tableAnswers);
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
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
            private DataColumn columnAnswerPipeAlias;
            private DataColumn columnAnswerText;
            private DataColumn columnQuestionId;

            public event PipeData.AnswersRowChangeEventHandler AnswersRowChanged;

            public event PipeData.AnswersRowChangeEventHandler AnswersRowChanging;

            public event PipeData.AnswersRowChangeEventHandler AnswersRowDeleted;

            public event PipeData.AnswersRowChangeEventHandler AnswersRowDeleting;

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

            public void AddAnswersRow(PipeData.AnswersRow row)
            {
                base.Rows.Add(row);
            }

            public PipeData.AnswersRow AddAnswersRow(int QuestionId, string AnswerText, string AnswerPipeAlias)
            {
                PipeData.AnswersRow row = (PipeData.AnswersRow) base.NewRow();
                object[] objArray = new object[4];
                objArray[1] = QuestionId;
                objArray[2] = AnswerText;
                objArray[3] = AnswerPipeAlias;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                PipeData.AnswersDataTable table = (PipeData.AnswersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new PipeData.AnswersDataTable();
            }

            public PipeData.AnswersRow FindByAnswerId(int AnswerId)
            {
                return (PipeData.AnswersRow) base.Rows.Find(new object[] { AnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(PipeData.AnswersRow);
            }

            private void InitClass()
            {
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnAnswerPipeAlias = new DataColumn("AnswerPipeAlias", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerPipeAlias);
                base.Constraints.Add(new UniqueConstraint("PipeDataKey2", new DataColumn[] { this.columnAnswerId }, true));
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
                this.columnAnswerText = base.Columns["AnswerText"];
                this.columnAnswerPipeAlias = base.Columns["AnswerPipeAlias"];
            }

            public PipeData.AnswersRow NewAnswersRow()
            {
                return (PipeData.AnswersRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new PipeData.AnswersRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswersRowChanged != null)
                {
                    this.AnswersRowChanged(this, new PipeData.AnswersRowChangeEvent((PipeData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswersRowChanging != null)
                {
                    this.AnswersRowChanging(this, new PipeData.AnswersRowChangeEvent((PipeData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswersRowDeleted != null)
                {
                    this.AnswersRowDeleted(this, new PipeData.AnswersRowChangeEvent((PipeData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswersRowDeleting != null)
                {
                    this.AnswersRowDeleting(this, new PipeData.AnswersRowChangeEvent((PipeData.AnswersRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswersRow(PipeData.AnswersRow row)
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

            internal DataColumn AnswerPipeAliasColumn
            {
                get
                {
                    return this.columnAnswerPipeAlias;
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

            public PipeData.AnswersRow this[int index]
            {
                get
                {
                    return (PipeData.AnswersRow) base.Rows[index];
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswersRow : DataRow
        {
            private PipeData.AnswersDataTable tableAnswers;

            internal AnswersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswers = (PipeData.AnswersDataTable) base.Table;
            }

            public bool IsAnswerPipeAliasNull()
            {
                return base.IsNull(this.tableAnswers.AnswerPipeAliasColumn);
            }

            public bool IsAnswerTextNull()
            {
                return base.IsNull(this.tableAnswers.AnswerTextColumn);
            }

            public void SetAnswerPipeAliasNull()
            {
                base[this.tableAnswers.AnswerPipeAliasColumn] = Convert.DBNull;
            }

            public void SetAnswerTextNull()
            {
                base[this.tableAnswers.AnswerTextColumn] = Convert.DBNull;
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

            public string AnswerPipeAlias
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswers.AnswerPipeAliasColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswers.AnswerPipeAliasColumn] = value;
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
        }

        [DebuggerStepThrough]
        public class AnswersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private PipeData.AnswersRow eventRow;

            public AnswersRowChangeEvent(PipeData.AnswersRow row, DataRowAction action)
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

            public PipeData.AnswersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswersRowChangeEventHandler(object sender, PipeData.AnswersRowChangeEvent e);

        [DebuggerStepThrough]
        public class QuestionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnQuestionId;
            private DataColumn columnQuestionPipeAlias;

            public event PipeData.QuestionsRowChangeEventHandler QuestionsRowChanged;

            public event PipeData.QuestionsRowChangeEventHandler QuestionsRowChanging;

            public event PipeData.QuestionsRowChangeEventHandler QuestionsRowDeleted;

            public event PipeData.QuestionsRowChangeEventHandler QuestionsRowDeleting;

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

            public PipeData.QuestionsRow AddQuestionsRow(string QuestionPipeAlias)
            {
                PipeData.QuestionsRow row = (PipeData.QuestionsRow) base.NewRow();
                object[] objArray = new object[2];
                objArray[1] = QuestionPipeAlias;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public void AddQuestionsRow(PipeData.QuestionsRow row)
            {
                base.Rows.Add(row);
            }

            public override DataTable Clone()
            {
                PipeData.QuestionsDataTable table = (PipeData.QuestionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new PipeData.QuestionsDataTable();
            }

            public PipeData.QuestionsRow FindByQuestionId(int QuestionId)
            {
                return (PipeData.QuestionsRow) base.Rows.Find(new object[] { QuestionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(PipeData.QuestionsRow);
            }

            private void InitClass()
            {
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnQuestionPipeAlias = new DataColumn("QuestionPipeAlias", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionPipeAlias);
                base.Constraints.Add(new UniqueConstraint("PipeDataKey1", new DataColumn[] { this.columnQuestionId }, true));
                this.columnQuestionId.AutoIncrement = true;
                this.columnQuestionId.AllowDBNull = false;
                this.columnQuestionId.ReadOnly = true;
                this.columnQuestionId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnQuestionPipeAlias = base.Columns["QuestionPipeAlias"];
            }

            public PipeData.QuestionsRow NewQuestionsRow()
            {
                return (PipeData.QuestionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new PipeData.QuestionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.QuestionsRowChanged != null)
                {
                    this.QuestionsRowChanged(this, new PipeData.QuestionsRowChangeEvent((PipeData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.QuestionsRowChanging != null)
                {
                    this.QuestionsRowChanging(this, new PipeData.QuestionsRowChangeEvent((PipeData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.QuestionsRowDeleted != null)
                {
                    this.QuestionsRowDeleted(this, new PipeData.QuestionsRowChangeEvent((PipeData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.QuestionsRowDeleting != null)
                {
                    this.QuestionsRowDeleting(this, new PipeData.QuestionsRowChangeEvent((PipeData.QuestionsRow) e.Row, e.Action));
                }
            }

            public void RemoveQuestionsRow(PipeData.QuestionsRow row)
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

            public PipeData.QuestionsRow this[int index]
            {
                get
                {
                    return (PipeData.QuestionsRow) base.Rows[index];
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn QuestionPipeAliasColumn
            {
                get
                {
                    return this.columnQuestionPipeAlias;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionsRow : DataRow
        {
            private PipeData.QuestionsDataTable tableQuestions;

            internal QuestionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableQuestions = (PipeData.QuestionsDataTable) base.Table;
            }

            public bool IsQuestionPipeAliasNull()
            {
                return base.IsNull(this.tableQuestions.QuestionPipeAliasColumn);
            }

            public void SetQuestionPipeAliasNull()
            {
                base[this.tableQuestions.QuestionPipeAliasColumn] = Convert.DBNull;
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

            public string QuestionPipeAlias
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableQuestions.QuestionPipeAliasColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableQuestions.QuestionPipeAliasColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private PipeData.QuestionsRow eventRow;

            public QuestionsRowChangeEvent(PipeData.QuestionsRow row, DataRowAction action)
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

            public PipeData.QuestionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void QuestionsRowChangeEventHandler(object sender, PipeData.QuestionsRowChangeEvent e);
    }
}

