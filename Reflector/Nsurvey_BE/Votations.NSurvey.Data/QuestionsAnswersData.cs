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
    public class QuestionsAnswersData : DataSet
    {
        private DataRelation relationQuestionsAnswers;
        private AnswersDataTable tableAnswers;
        private QuestionsDataTable tableQuestions;

        public QuestionsAnswersData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected QuestionsAnswersData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["Answers"] != null)
                {
                    base.Tables.Add(new AnswersDataTable(dataSet.Tables["Answers"]));
                }
                if (dataSet.Tables["Questions"] != null)
                {
                    base.Tables.Add(new QuestionsDataTable(dataSet.Tables["Questions"]));
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
            QuestionsAnswersData data = (QuestionsAnswersData) base.Clone();
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
            base.DataSetName = "QuestionsAnswersData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/AnswerData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableAnswers = new AnswersDataTable();
            base.Tables.Add(this.tableAnswers);
            this.tableQuestions = new QuestionsDataTable();
            base.Tables.Add(this.tableQuestions);
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
            this.tableAnswers = (AnswersDataTable) base.Tables["Answers"];
            if (this.tableAnswers != null)
            {
                this.tableAnswers.InitVars();
            }
            this.tableQuestions = (QuestionsDataTable) base.Tables["Questions"];
            if (this.tableQuestions != null)
            {
                this.tableQuestions.InitVars();
            }
            this.relationQuestionsAnswers = base.Relations["QuestionsAnswers"];
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["Answers"] != null)
            {
                base.Tables.Add(new AnswersDataTable(dataSet.Tables["Answers"]));
            }
            if (dataSet.Tables["Questions"] != null)
            {
                base.Tables.Add(new QuestionsDataTable(dataSet.Tables["Questions"]));
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
            private DataColumn columnAnswerPipeAlias;
            private DataColumn columnAnswerText;
            private DataColumn columnAnswerTypeId;
            private DataColumn columnDataSource;
            private DataColumn columnFieldHeight;
            private DataColumn columnFieldLength;
            private DataColumn columnFieldWidth;
            private DataColumn columnImageURL;
            private DataColumn columnJavascriptCode;
            private DataColumn columnJavascriptErrorMessage;
            private DataColumn columnJavascriptFunctionName;
            private DataColumn columnQuestionId;
            private DataColumn columnTypeMode;
            private DataColumn columnVoteCount;
            private DataColumn columnXmlDatasource;

            public event QuestionsAnswersData.AnswersRowChangeEventHandler AnswersRowChanged;

            public event QuestionsAnswersData.AnswersRowChangeEventHandler AnswersRowChanging;

            public event QuestionsAnswersData.AnswersRowChangeEventHandler AnswersRowDeleted;

            public event QuestionsAnswersData.AnswersRowChangeEventHandler AnswersRowDeleting;

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

            public void AddAnswersRow(QuestionsAnswersData.AnswersRow row)
            {
                base.Rows.Add(row);
            }

            public QuestionsAnswersData.AnswersRow AddAnswersRow(int AnswerTypeId, int FieldHeight, int FieldLength, int FieldWidth, int VoteCount, string AnswerText, string ImageURL, int TypeMode, string XmlDatasource, string JavascriptFunctionName, string JavascriptErrorMessage, string JavascriptCode, string AnswerPipeAlias, string DataSource)
            {
                QuestionsAnswersData.AnswersRow row = (QuestionsAnswersData.AnswersRow) base.NewRow();
                object[] objArray = new object[0x10];
                objArray[1] = AnswerTypeId;
                objArray[3] = FieldHeight;
                objArray[4] = FieldLength;
                objArray[5] = FieldWidth;
                objArray[6] = VoteCount;
                objArray[7] = AnswerText;
                objArray[8] = ImageURL;
                objArray[9] = TypeMode;
                objArray[10] = XmlDatasource;
                objArray[11] = JavascriptFunctionName;
                objArray[12] = JavascriptErrorMessage;
                objArray[13] = JavascriptCode;
                objArray[14] = AnswerPipeAlias;
                objArray[15] = DataSource;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                QuestionsAnswersData.AnswersDataTable table = (QuestionsAnswersData.AnswersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new QuestionsAnswersData.AnswersDataTable();
            }

            public QuestionsAnswersData.AnswersRow FindByAnswerId(int AnswerId)
            {
                return (QuestionsAnswersData.AnswersRow) base.Rows.Find(new object[] { AnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(QuestionsAnswersData.AnswersRow);
            }

            private void InitClass()
            {
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnAnswerTypeId = new DataColumn("AnswerTypeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerTypeId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnFieldHeight = new DataColumn("FieldHeight", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldHeight);
                this.columnFieldLength = new DataColumn("FieldLength", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldLength);
                this.columnFieldWidth = new DataColumn("FieldWidth", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldWidth);
                this.columnVoteCount = new DataColumn("VoteCount", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoteCount);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnImageURL = new DataColumn("ImageURL", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnImageURL);
                this.columnTypeMode = new DataColumn("TypeMode", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTypeMode);
                this.columnXmlDatasource = new DataColumn("XmlDatasource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnXmlDatasource);
                this.columnJavascriptFunctionName = new DataColumn("JavascriptFunctionName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptFunctionName);
                this.columnJavascriptErrorMessage = new DataColumn("JavascriptErrorMessage", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptErrorMessage);
                this.columnJavascriptCode = new DataColumn("JavascriptCode", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptCode);
                this.columnAnswerPipeAlias = new DataColumn("AnswerPipeAlias", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerPipeAlias);
                this.columnDataSource = new DataColumn("DataSource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDataSource);
                base.Constraints.Add(new UniqueConstraint("AnswerDataKey1", new DataColumn[] { this.columnAnswerId }, true));
                this.columnAnswerId.AutoIncrement = true;
                this.columnAnswerId.AutoIncrementSeed = -1L;
                this.columnAnswerId.AutoIncrementStep = -1L;
                this.columnAnswerId.AllowDBNull = false;
                this.columnAnswerId.ReadOnly = true;
                this.columnAnswerId.Unique = true;
                this.columnQuestionId.AutoIncrement = true;
                this.columnQuestionId.AllowDBNull = false;
                this.columnQuestionId.ReadOnly = true;
                this.columnAnswerText.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnAnswerTypeId = base.Columns["AnswerTypeId"];
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnFieldHeight = base.Columns["FieldHeight"];
                this.columnFieldLength = base.Columns["FieldLength"];
                this.columnFieldWidth = base.Columns["FieldWidth"];
                this.columnVoteCount = base.Columns["VoteCount"];
                this.columnAnswerText = base.Columns["AnswerText"];
                this.columnImageURL = base.Columns["ImageURL"];
                this.columnTypeMode = base.Columns["TypeMode"];
                this.columnXmlDatasource = base.Columns["XmlDatasource"];
                this.columnJavascriptFunctionName = base.Columns["JavascriptFunctionName"];
                this.columnJavascriptErrorMessage = base.Columns["JavascriptErrorMessage"];
                this.columnJavascriptCode = base.Columns["JavascriptCode"];
                this.columnAnswerPipeAlias = base.Columns["AnswerPipeAlias"];
                this.columnDataSource = base.Columns["DataSource"];
            }

            public QuestionsAnswersData.AnswersRow NewAnswersRow()
            {
                return (QuestionsAnswersData.AnswersRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new QuestionsAnswersData.AnswersRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswersRowChanged != null)
                {
                    this.AnswersRowChanged(this, new QuestionsAnswersData.AnswersRowChangeEvent((QuestionsAnswersData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswersRowChanging != null)
                {
                    this.AnswersRowChanging(this, new QuestionsAnswersData.AnswersRowChangeEvent((QuestionsAnswersData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswersRowDeleted != null)
                {
                    this.AnswersRowDeleted(this, new QuestionsAnswersData.AnswersRowChangeEvent((QuestionsAnswersData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswersRowDeleting != null)
                {
                    this.AnswersRowDeleting(this, new QuestionsAnswersData.AnswersRowChangeEvent((QuestionsAnswersData.AnswersRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswersRow(QuestionsAnswersData.AnswersRow row)
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

            internal DataColumn DataSourceColumn
            {
                get
                {
                    return this.columnDataSource;
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

            internal DataColumn ImageURLColumn
            {
                get
                {
                    return this.columnImageURL;
                }
            }

            public QuestionsAnswersData.AnswersRow this[int index]
            {
                get
                {
                    return (QuestionsAnswersData.AnswersRow) base.Rows[index];
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

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn TypeModeColumn
            {
                get
                {
                    return this.columnTypeMode;
                }
            }

            internal DataColumn VoteCountColumn
            {
                get
                {
                    return this.columnVoteCount;
                }
            }

            internal DataColumn XmlDatasourceColumn
            {
                get
                {
                    return this.columnXmlDatasource;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswersRow : DataRow
        {
            private QuestionsAnswersData.AnswersDataTable tableAnswers;

            internal AnswersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswers = (QuestionsAnswersData.AnswersDataTable) base.Table;
            }

            public bool IsAnswerPipeAliasNull()
            {
                return base.IsNull(this.tableAnswers.AnswerPipeAliasColumn);
            }

            public bool IsAnswerTypeIdNull()
            {
                return base.IsNull(this.tableAnswers.AnswerTypeIdColumn);
            }

            public bool IsDataSourceNull()
            {
                return base.IsNull(this.tableAnswers.DataSourceColumn);
            }

            public bool IsFieldHeightNull()
            {
                return base.IsNull(this.tableAnswers.FieldHeightColumn);
            }

            public bool IsFieldLengthNull()
            {
                return base.IsNull(this.tableAnswers.FieldLengthColumn);
            }

            public bool IsFieldWidthNull()
            {
                return base.IsNull(this.tableAnswers.FieldWidthColumn);
            }

            public bool IsImageURLNull()
            {
                return base.IsNull(this.tableAnswers.ImageURLColumn);
            }

            public bool IsJavascriptCodeNull()
            {
                return base.IsNull(this.tableAnswers.JavascriptCodeColumn);
            }

            public bool IsJavascriptErrorMessageNull()
            {
                return base.IsNull(this.tableAnswers.JavascriptErrorMessageColumn);
            }

            public bool IsJavascriptFunctionNameNull()
            {
                return base.IsNull(this.tableAnswers.JavascriptFunctionNameColumn);
            }

            public bool IsTypeModeNull()
            {
                return base.IsNull(this.tableAnswers.TypeModeColumn);
            }

            public bool IsVoteCountNull()
            {
                return base.IsNull(this.tableAnswers.VoteCountColumn);
            }

            public bool IsXmlDatasourceNull()
            {
                return base.IsNull(this.tableAnswers.XmlDatasourceColumn);
            }

            public void SetAnswerPipeAliasNull()
            {
                base[this.tableAnswers.AnswerPipeAliasColumn] = Convert.DBNull;
            }

            public void SetAnswerTypeIdNull()
            {
                base[this.tableAnswers.AnswerTypeIdColumn] = Convert.DBNull;
            }

            public void SetDataSourceNull()
            {
                base[this.tableAnswers.DataSourceColumn] = Convert.DBNull;
            }

            public void SetFieldHeightNull()
            {
                base[this.tableAnswers.FieldHeightColumn] = Convert.DBNull;
            }

            public void SetFieldLengthNull()
            {
                base[this.tableAnswers.FieldLengthColumn] = Convert.DBNull;
            }

            public void SetFieldWidthNull()
            {
                base[this.tableAnswers.FieldWidthColumn] = Convert.DBNull;
            }

            public void SetImageURLNull()
            {
                base[this.tableAnswers.ImageURLColumn] = Convert.DBNull;
            }

            public void SetJavascriptCodeNull()
            {
                base[this.tableAnswers.JavascriptCodeColumn] = Convert.DBNull;
            }

            public void SetJavascriptErrorMessageNull()
            {
                base[this.tableAnswers.JavascriptErrorMessageColumn] = Convert.DBNull;
            }

            public void SetJavascriptFunctionNameNull()
            {
                base[this.tableAnswers.JavascriptFunctionNameColumn] = Convert.DBNull;
            }

            public void SetTypeModeNull()
            {
                base[this.tableAnswers.TypeModeColumn] = Convert.DBNull;
            }

            public void SetVoteCountNull()
            {
                base[this.tableAnswers.VoteCountColumn] = Convert.DBNull;
            }

            public void SetXmlDatasourceNull()
            {
                base[this.tableAnswers.XmlDatasourceColumn] = Convert.DBNull;
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
                    if (this.IsAnswerPipeAliasNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswers.AnswerPipeAliasColumn];
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
                    return (string) base[this.tableAnswers.AnswerTextColumn];
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

            public string DataSource
            {
                get
                {
                    if (this.IsDataSourceNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswers.DataSourceColumn];
                }
                set
                {
                    base[this.tableAnswers.DataSourceColumn] = value;
                }
            }

            public int FieldHeight
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswers.FieldHeightColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswers.FieldHeightColumn] = value;
                }
            }

            public int FieldLength
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswers.FieldLengthColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswers.FieldLengthColumn] = value;
                }
            }

            public int FieldWidth
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswers.FieldWidthColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswers.FieldWidthColumn] = value;
                }
            }

            public string ImageURL
            {
                get
                {
                    if (this.IsImageURLNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswers.ImageURLColumn];
                }
                set
                {
                    base[this.tableAnswers.ImageURLColumn] = value;
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
                    return (string) base[this.tableAnswers.JavascriptCodeColumn];
                }
                set
                {
                    base[this.tableAnswers.JavascriptCodeColumn] = value;
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
                    return (string) base[this.tableAnswers.JavascriptErrorMessageColumn];
                }
                set
                {
                    base[this.tableAnswers.JavascriptErrorMessageColumn] = value;
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
                    return (string) base[this.tableAnswers.JavascriptFunctionNameColumn];
                }
                set
                {
                    base[this.tableAnswers.JavascriptFunctionNameColumn] = value;
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

            public Votations.NSurvey.Data.QuestionsAnswersData.QuestionsRow QuestionsRow
            {
                get
                {
                    return (Votations.NSurvey.Data.QuestionsAnswersData.QuestionsRow) base.GetParentRow(base.Table.ParentRelations["QuestionsAnswers"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["QuestionsAnswers"]);
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

            public int VoteCount
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswers.VoteCountColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswers.VoteCountColumn] = value;
                }
            }

            public string XmlDatasource
            {
                get
                {
                    if (this.IsXmlDatasourceNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableAnswers.XmlDatasourceColumn];
                }
                set
                {
                    base[this.tableAnswers.XmlDatasourceColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private QuestionsAnswersData.AnswersRow eventRow;

            public AnswersRowChangeEvent(QuestionsAnswersData.AnswersRow row, DataRowAction action)
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

            public QuestionsAnswersData.AnswersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswersRowChangeEventHandler(object sender, QuestionsAnswersData.AnswersRowChangeEvent e);

        [DebuggerStepThrough]
        public class QuestionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDisplayOrder;
            private DataColumn columnImagesEnabled;
            private DataColumn columnLayoutModeId;
            private DataColumn columnMaxSelectionAllowed;
            private DataColumn columnMinSelectionRequired;
            private DataColumn columnParentQuestionId;
            private DataColumn columnQuestionId;
            private DataColumn columnQuestionPipeAlias;
            private DataColumn columnQuestionText;
            private DataColumn columnRandomizeAnswers;
            private DataColumn columnSelectionModeId;
            private DataColumn columnSurveyId;
            private DataColumn columnTypeAssembly;
            private DataColumn columnTypeNameSpace;

            public event QuestionsAnswersData.QuestionsRowChangeEventHandler QuestionsRowChanged;

            public event QuestionsAnswersData.QuestionsRowChangeEventHandler QuestionsRowChanging;

            public event QuestionsAnswersData.QuestionsRowChangeEventHandler QuestionsRowDeleted;

            public event QuestionsAnswersData.QuestionsRowChangeEventHandler QuestionsRowDeleting;

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

            public void AddQuestionsRow(QuestionsAnswersData.QuestionsRow row)
            {
                base.Rows.Add(row);
            }

            public QuestionsAnswersData.QuestionsRow AddQuestionsRow(int ParentQuestionId, int SurveyId, int SelectionModeId, int LayoutModeId, int DisplayOrder, int MinSelectionRequired, int MaxSelectionAllowed, string QuestionText, bool ImagesEnabled, bool RandomizeAnswers, string TypeNameSpace, string TypeAssembly, string QuestionPipeAlias)
            {
                QuestionsAnswersData.QuestionsRow row = (QuestionsAnswersData.QuestionsRow) base.NewRow();
                object[] objArray = new object[14];
                objArray[1] = ParentQuestionId;
                objArray[2] = SurveyId;
                objArray[3] = SelectionModeId;
                objArray[4] = LayoutModeId;
                objArray[5] = DisplayOrder;
                objArray[6] = MinSelectionRequired;
                objArray[7] = MaxSelectionAllowed;
                objArray[8] = QuestionText;
                objArray[9] = ImagesEnabled;
                objArray[10] = RandomizeAnswers;
                objArray[11] = TypeNameSpace;
                objArray[12] = TypeAssembly;
                objArray[13] = QuestionPipeAlias;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                QuestionsAnswersData.QuestionsDataTable table = (QuestionsAnswersData.QuestionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new QuestionsAnswersData.QuestionsDataTable();
            }

            public QuestionsAnswersData.QuestionsRow FindByQuestionId(int QuestionId)
            {
                return (QuestionsAnswersData.QuestionsRow) base.Rows.Find(new object[] { QuestionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(QuestionsAnswersData.QuestionsRow);
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
                this.columnLayoutModeId = new DataColumn("LayoutModeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnLayoutModeId);
                this.columnDisplayOrder = new DataColumn("DisplayOrder", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnDisplayOrder);
                this.columnMinSelectionRequired = new DataColumn("MinSelectionRequired", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnMinSelectionRequired);
                this.columnMaxSelectionAllowed = new DataColumn("MaxSelectionAllowed", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnMaxSelectionAllowed);
                this.columnQuestionText = new DataColumn("QuestionText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionText);
                this.columnImagesEnabled = new DataColumn("ImagesEnabled", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnImagesEnabled);
                this.columnRandomizeAnswers = new DataColumn("RandomizeAnswers", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnRandomizeAnswers);
                this.columnTypeNameSpace = new DataColumn("TypeNameSpace", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTypeNameSpace);
                this.columnTypeAssembly = new DataColumn("TypeAssembly", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTypeAssembly);
                this.columnQuestionPipeAlias = new DataColumn("QuestionPipeAlias", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionPipeAlias);
                base.Constraints.Add(new UniqueConstraint("QuestionsAnswersDataKey1", new DataColumn[] { this.columnQuestionId }, true));
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
                this.columnLayoutModeId = base.Columns["LayoutModeId"];
                this.columnDisplayOrder = base.Columns["DisplayOrder"];
                this.columnMinSelectionRequired = base.Columns["MinSelectionRequired"];
                this.columnMaxSelectionAllowed = base.Columns["MaxSelectionAllowed"];
                this.columnQuestionText = base.Columns["QuestionText"];
                this.columnImagesEnabled = base.Columns["ImagesEnabled"];
                this.columnRandomizeAnswers = base.Columns["RandomizeAnswers"];
                this.columnTypeNameSpace = base.Columns["TypeNameSpace"];
                this.columnTypeAssembly = base.Columns["TypeAssembly"];
                this.columnQuestionPipeAlias = base.Columns["QuestionPipeAlias"];
            }

            public QuestionsAnswersData.QuestionsRow NewQuestionsRow()
            {
                return (QuestionsAnswersData.QuestionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new QuestionsAnswersData.QuestionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.QuestionsRowChanged != null)
                {
                    this.QuestionsRowChanged(this, new QuestionsAnswersData.QuestionsRowChangeEvent((QuestionsAnswersData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.QuestionsRowChanging != null)
                {
                    this.QuestionsRowChanging(this, new QuestionsAnswersData.QuestionsRowChangeEvent((QuestionsAnswersData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.QuestionsRowDeleted != null)
                {
                    this.QuestionsRowDeleted(this, new QuestionsAnswersData.QuestionsRowChangeEvent((QuestionsAnswersData.QuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.QuestionsRowDeleting != null)
                {
                    this.QuestionsRowDeleting(this, new QuestionsAnswersData.QuestionsRowChangeEvent((QuestionsAnswersData.QuestionsRow) e.Row, e.Action));
                }
            }

            public void RemoveQuestionsRow(QuestionsAnswersData.QuestionsRow row)
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

            internal DataColumn ImagesEnabledColumn
            {
                get
                {
                    return this.columnImagesEnabled;
                }
            }

            public QuestionsAnswersData.QuestionsRow this[int index]
            {
                get
                {
                    return (QuestionsAnswersData.QuestionsRow) base.Rows[index];
                }
            }

            internal DataColumn LayoutModeIdColumn
            {
                get
                {
                    return this.columnLayoutModeId;
                }
            }

            internal DataColumn MaxSelectionAllowedColumn
            {
                get
                {
                    return this.columnMaxSelectionAllowed;
                }
            }

            internal DataColumn MinSelectionRequiredColumn
            {
                get
                {
                    return this.columnMinSelectionRequired;
                }
            }

            internal DataColumn ParentQuestionIdColumn
            {
                get
                {
                    return this.columnParentQuestionId;
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

            internal DataColumn QuestionTextColumn
            {
                get
                {
                    return this.columnQuestionText;
                }
            }

            internal DataColumn RandomizeAnswersColumn
            {
                get
                {
                    return this.columnRandomizeAnswers;
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

            internal DataColumn TypeAssemblyColumn
            {
                get
                {
                    return this.columnTypeAssembly;
                }
            }

            internal DataColumn TypeNameSpaceColumn
            {
                get
                {
                    return this.columnTypeNameSpace;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionsRow : DataRow
        {
            private QuestionsAnswersData.QuestionsDataTable tableQuestions;

            internal QuestionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableQuestions = (QuestionsAnswersData.QuestionsDataTable) base.Table;
            }

            public QuestionsAnswersData.AnswersRow[] GetAnswersRows()
            {
                return (QuestionsAnswersData.AnswersRow[]) base.GetChildRows(base.Table.ChildRelations["QuestionsAnswers"]);
            }

            public bool IsDisplayOrderNull()
            {
                return base.IsNull(this.tableQuestions.DisplayOrderColumn);
            }

            public bool IsImagesEnabledNull()
            {
                return base.IsNull(this.tableQuestions.ImagesEnabledColumn);
            }

            public bool IsLayoutModeIdNull()
            {
                return base.IsNull(this.tableQuestions.LayoutModeIdColumn);
            }

            public bool IsMaxSelectionAllowedNull()
            {
                return base.IsNull(this.tableQuestions.MaxSelectionAllowedColumn);
            }

            public bool IsMinSelectionRequiredNull()
            {
                return base.IsNull(this.tableQuestions.MinSelectionRequiredColumn);
            }

            public bool IsParentQuestionIdNull()
            {
                return base.IsNull(this.tableQuestions.ParentQuestionIdColumn);
            }

            public bool IsQuestionPipeAliasNull()
            {
                return base.IsNull(this.tableQuestions.QuestionPipeAliasColumn);
            }

            public bool IsQuestionTextNull()
            {
                return base.IsNull(this.tableQuestions.QuestionTextColumn);
            }

            public bool IsRandomizeAnswersNull()
            {
                return base.IsNull(this.tableQuestions.RandomizeAnswersColumn);
            }

            public bool IsSelectionModeIdNull()
            {
                return base.IsNull(this.tableQuestions.SelectionModeIdColumn);
            }

            public bool IsSurveyIdNull()
            {
                return base.IsNull(this.tableQuestions.SurveyIdColumn);
            }

            public bool IsTypeAssemblyNull()
            {
                return base.IsNull(this.tableQuestions.TypeAssemblyColumn);
            }

            public bool IsTypeNameSpaceNull()
            {
                return base.IsNull(this.tableQuestions.TypeNameSpaceColumn);
            }

            public void SetDisplayOrderNull()
            {
                base[this.tableQuestions.DisplayOrderColumn] = Convert.DBNull;
            }

            public void SetImagesEnabledNull()
            {
                base[this.tableQuestions.ImagesEnabledColumn] = Convert.DBNull;
            }

            public void SetLayoutModeIdNull()
            {
                base[this.tableQuestions.LayoutModeIdColumn] = Convert.DBNull;
            }

            public void SetMaxSelectionAllowedNull()
            {
                base[this.tableQuestions.MaxSelectionAllowedColumn] = Convert.DBNull;
            }

            public void SetMinSelectionRequiredNull()
            {
                base[this.tableQuestions.MinSelectionRequiredColumn] = Convert.DBNull;
            }

            public void SetParentQuestionIdNull()
            {
                base[this.tableQuestions.ParentQuestionIdColumn] = Convert.DBNull;
            }

            public void SetQuestionPipeAliasNull()
            {
                base[this.tableQuestions.QuestionPipeAliasColumn] = Convert.DBNull;
            }

            public void SetQuestionTextNull()
            {
                base[this.tableQuestions.QuestionTextColumn] = Convert.DBNull;
            }

            public void SetRandomizeAnswersNull()
            {
                base[this.tableQuestions.RandomizeAnswersColumn] = Convert.DBNull;
            }

            public void SetSelectionModeIdNull()
            {
                base[this.tableQuestions.SelectionModeIdColumn] = Convert.DBNull;
            }

            public void SetSurveyIdNull()
            {
                base[this.tableQuestions.SurveyIdColumn] = Convert.DBNull;
            }

            public void SetTypeAssemblyNull()
            {
                base[this.tableQuestions.TypeAssemblyColumn] = Convert.DBNull;
            }

            public void SetTypeNameSpaceNull()
            {
                base[this.tableQuestions.TypeNameSpaceColumn] = Convert.DBNull;
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

            public bool ImagesEnabled
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableQuestions.ImagesEnabledColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableQuestions.ImagesEnabledColumn] = value;
                }
            }

            public int LayoutModeId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestions.LayoutModeIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestions.LayoutModeIdColumn] = value;
                }
            }

            public int MaxSelectionAllowed
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestions.MaxSelectionAllowedColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestions.MaxSelectionAllowedColumn] = value;
                }
            }

            public int MinSelectionRequired
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestions.MinSelectionRequiredColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestions.MinSelectionRequiredColumn] = value;
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
                    if (this.IsQuestionPipeAliasNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestions.QuestionPipeAliasColumn];
                }
                set
                {
                    base[this.tableQuestions.QuestionPipeAliasColumn] = value;
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

            public bool RandomizeAnswers
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableQuestions.RandomizeAnswersColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableQuestions.RandomizeAnswersColumn] = value;
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

            public string TypeAssembly
            {
                get
                {
                    if (this.IsTypeAssemblyNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestions.TypeAssemblyColumn];
                }
                set
                {
                    base[this.tableQuestions.TypeAssemblyColumn] = value;
                }
            }

            public string TypeNameSpace
            {
                get
                {
                    if (this.IsTypeNameSpaceNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestions.TypeNameSpaceColumn];
                }
                set
                {
                    base[this.tableQuestions.TypeNameSpaceColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private QuestionsAnswersData.QuestionsRow eventRow;

            public QuestionsRowChangeEvent(QuestionsAnswersData.QuestionsRow row, DataRowAction action)
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

            public QuestionsAnswersData.QuestionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void QuestionsRowChangeEventHandler(object sender, QuestionsAnswersData.QuestionsRowChangeEvent e);
    }
}

