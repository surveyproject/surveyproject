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
    public class MatrixChildQuestionData : DataSet
    {
        private DataRelation relationChildQuestionAnswers;
        private AnswersDataTable tableAnswers;
        private ChildQuestionsDataTable tableChildQuestions;

        public MatrixChildQuestionData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected MatrixChildQuestionData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["ChildQuestions"] != null)
                {
                    base.Tables.Add(new ChildQuestionsDataTable(dataSet.Tables["ChildQuestions"]));
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
            MatrixChildQuestionData data = (MatrixChildQuestionData) base.Clone();
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
            base.DataSetName = "MatrixChildQuestionData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/MatrixChildQuestionData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableChildQuestions = new ChildQuestionsDataTable();
            base.Tables.Add(this.tableChildQuestions);
            this.tableAnswers = new AnswersDataTable();
            base.Tables.Add(this.tableAnswers);
            ForeignKeyConstraint constraint = new ForeignKeyConstraint("ChildQuestionAnswers", new DataColumn[] { this.tableChildQuestions.QuestionIdColumn }, new DataColumn[] { this.tableAnswers.QuestionIdColumn });
            this.tableAnswers.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            this.relationChildQuestionAnswers = new DataRelation("ChildQuestionAnswers", new DataColumn[] { this.tableChildQuestions.QuestionIdColumn }, new DataColumn[] { this.tableAnswers.QuestionIdColumn }, false);
            base.Relations.Add(this.relationChildQuestionAnswers);
        }

        internal void InitVars()
        {
            this.tableChildQuestions = (ChildQuestionsDataTable) base.Tables["ChildQuestions"];
            if (this.tableChildQuestions != null)
            {
                this.tableChildQuestions.InitVars();
            }
            this.tableAnswers = (AnswersDataTable) base.Tables["Answers"];
            if (this.tableAnswers != null)
            {
                this.tableAnswers.InitVars();
            }
            this.relationChildQuestionAnswers = base.Relations["ChildQuestionAnswers"];
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["ChildQuestions"] != null)
            {
                base.Tables.Add(new ChildQuestionsDataTable(dataSet.Tables["ChildQuestions"]));
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

        private bool ShouldSerializeChildQuestions()
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public ChildQuestionsDataTable ChildQuestions
        {
            get
            {
                return this.tableChildQuestions;
            }
        }

        [DebuggerStepThrough]
        public class AnswersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnAnswerText;
            private DataColumn columnDataSource;
            private DataColumn columnDefaultText;
            private DataColumn columnFieldHeight;
            private DataColumn columnFieldLength;
            private DataColumn columnFieldWidth;
            private DataColumn columnImageUrl;
            private DataColumn columnMandatory;
            private DataColumn columnQuestionId;
            private DataColumn columnSelected;
            private DataColumn columnTypeMode;
            private DataColumn columnUserSelected;
            private DataColumn columnUserText;
            private DataColumn columnVoteCount;
            private DataColumn columnXmlDatasource;

            public event MatrixChildQuestionData.AnswersRowChangeEventHandler AnswersRowChanged;

            public event MatrixChildQuestionData.AnswersRowChangeEventHandler AnswersRowChanging;

            public event MatrixChildQuestionData.AnswersRowChangeEventHandler AnswersRowDeleted;

            public event MatrixChildQuestionData.AnswersRowChangeEventHandler AnswersRowDeleting;

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

            public void AddAnswersRow(MatrixChildQuestionData.AnswersRow row)
            {
                base.Rows.Add(row);
            }

            public MatrixChildQuestionData.AnswersRow AddAnswersRow(MatrixChildQuestionData.ChildQuestionsRow parentChildQuestionsRowByChildQuestionAnswers, string AnswerText, string ImageUrl, int TypeMode, int FieldWidth, int FieldLength, int FieldHeight, int VoteCount, string XmlDatasource, bool Selected, string DefaultText, bool UserSelected, string UserText, string DataSource, bool Mandatory)
            {
                MatrixChildQuestionData.AnswersRow row = (MatrixChildQuestionData.AnswersRow) base.NewRow();
                object[] objArray = new object[0x10];
                objArray[1] = parentChildQuestionsRowByChildQuestionAnswers[0];
                objArray[2] = AnswerText;
                objArray[3] = ImageUrl;
                objArray[4] = TypeMode;
                objArray[5] = FieldWidth;
                objArray[6] = FieldLength;
                objArray[7] = FieldHeight;
                objArray[8] = VoteCount;
                objArray[9] = XmlDatasource;
                objArray[10] = Selected;
                objArray[11] = DefaultText;
                objArray[12] = UserSelected;
                objArray[13] = UserText;
                objArray[14] = DataSource;
                objArray[15] = Mandatory;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                MatrixChildQuestionData.AnswersDataTable table = (MatrixChildQuestionData.AnswersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new MatrixChildQuestionData.AnswersDataTable();
            }

            public MatrixChildQuestionData.AnswersRow FindByAnswerId(int AnswerId)
            {
                return (MatrixChildQuestionData.AnswersRow) base.Rows.Find(new object[] { AnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(MatrixChildQuestionData.AnswersRow);
            }

            private void InitClass()
            {
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnImageUrl = new DataColumn("ImageUrl", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnImageUrl);
                this.columnTypeMode = new DataColumn("TypeMode", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTypeMode);
                this.columnFieldWidth = new DataColumn("FieldWidth", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldWidth);
                this.columnFieldLength = new DataColumn("FieldLength", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldLength);
                this.columnFieldHeight = new DataColumn("FieldHeight", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFieldHeight);
                this.columnVoteCount = new DataColumn("VoteCount", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoteCount);
                this.columnXmlDatasource = new DataColumn("XmlDatasource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnXmlDatasource);
                this.columnSelected = new DataColumn("Selected", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnSelected);
                this.columnDefaultText = new DataColumn("DefaultText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDefaultText);
                this.columnUserSelected = new DataColumn("UserSelected", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnUserSelected);
                this.columnUserText = new DataColumn("UserText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUserText);
                this.columnDataSource = new DataColumn("DataSource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDataSource);
                this.columnMandatory = new DataColumn("Mandatory", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnMandatory);
                base.Constraints.Add(new UniqueConstraint("MatrixChildQuestionsKey2", new DataColumn[] { this.columnAnswerId }, true));
                this.columnAnswerId.AutoIncrement = true;
                this.columnAnswerId.AutoIncrementSeed = -1L;
                this.columnAnswerId.AutoIncrementStep = -1L;
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
                this.columnImageUrl = base.Columns["ImageUrl"];
                this.columnTypeMode = base.Columns["TypeMode"];
                this.columnFieldWidth = base.Columns["FieldWidth"];
                this.columnFieldLength = base.Columns["FieldLength"];
                this.columnFieldHeight = base.Columns["FieldHeight"];
                this.columnVoteCount = base.Columns["VoteCount"];
                this.columnXmlDatasource = base.Columns["XmlDatasource"];
                this.columnSelected = base.Columns["Selected"];
                this.columnDefaultText = base.Columns["DefaultText"];
                this.columnUserSelected = base.Columns["UserSelected"];
                this.columnUserText = base.Columns["UserText"];
                this.columnDataSource = base.Columns["DataSource"];
                this.columnMandatory = base.Columns["Mandatory"];
            }

            public MatrixChildQuestionData.AnswersRow NewAnswersRow()
            {
                return (MatrixChildQuestionData.AnswersRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new MatrixChildQuestionData.AnswersRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswersRowChanged != null)
                {
                    this.AnswersRowChanged(this, new MatrixChildQuestionData.AnswersRowChangeEvent((MatrixChildQuestionData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswersRowChanging != null)
                {
                    this.AnswersRowChanging(this, new MatrixChildQuestionData.AnswersRowChangeEvent((MatrixChildQuestionData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswersRowDeleted != null)
                {
                    this.AnswersRowDeleted(this, new MatrixChildQuestionData.AnswersRowChangeEvent((MatrixChildQuestionData.AnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswersRowDeleting != null)
                {
                    this.AnswersRowDeleting(this, new MatrixChildQuestionData.AnswersRowChangeEvent((MatrixChildQuestionData.AnswersRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswersRow(MatrixChildQuestionData.AnswersRow row)
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

            internal DataColumn DataSourceColumn
            {
                get
                {
                    return this.columnDataSource;
                }
            }

            internal DataColumn DefaultTextColumn
            {
                get
                {
                    return this.columnDefaultText;
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

            internal DataColumn ImageUrlColumn
            {
                get
                {
                    return this.columnImageUrl;
                }
            }

            public MatrixChildQuestionData.AnswersRow this[int index]
            {
                get
                {
                    return (MatrixChildQuestionData.AnswersRow) base.Rows[index];
                }
            }

            internal DataColumn MandatoryColumn
            {
                get
                {
                    return this.columnMandatory;
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn SelectedColumn
            {
                get
                {
                    return this.columnSelected;
                }
            }

            internal DataColumn TypeModeColumn
            {
                get
                {
                    return this.columnTypeMode;
                }
            }

            internal DataColumn UserSelectedColumn
            {
                get
                {
                    return this.columnUserSelected;
                }
            }

            internal DataColumn UserTextColumn
            {
                get
                {
                    return this.columnUserText;
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
            private MatrixChildQuestionData.AnswersDataTable tableAnswers;

            internal AnswersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswers = (MatrixChildQuestionData.AnswersDataTable) base.Table;
            }

            public bool IsAnswerTextNull()
            {
                return base.IsNull(this.tableAnswers.AnswerTextColumn);
            }

            public bool IsDataSourceNull()
            {
                return base.IsNull(this.tableAnswers.DataSourceColumn);
            }

            public bool IsDefaultTextNull()
            {
                return base.IsNull(this.tableAnswers.DefaultTextColumn);
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

            public bool IsImageUrlNull()
            {
                return base.IsNull(this.tableAnswers.ImageUrlColumn);
            }

            public bool IsMandatoryNull()
            {
                return base.IsNull(this.tableAnswers.MandatoryColumn);
            }

            public bool IsSelectedNull()
            {
                return base.IsNull(this.tableAnswers.SelectedColumn);
            }

            public bool IsTypeModeNull()
            {
                return base.IsNull(this.tableAnswers.TypeModeColumn);
            }

            public bool IsUserSelectedNull()
            {
                return base.IsNull(this.tableAnswers.UserSelectedColumn);
            }

            public bool IsUserTextNull()
            {
                return base.IsNull(this.tableAnswers.UserTextColumn);
            }

            public bool IsVoteCountNull()
            {
                return base.IsNull(this.tableAnswers.VoteCountColumn);
            }

            public bool IsXmlDatasourceNull()
            {
                return base.IsNull(this.tableAnswers.XmlDatasourceColumn);
            }

            public void SetAnswerTextNull()
            {
                base[this.tableAnswers.AnswerTextColumn] = Convert.DBNull;
            }

            public void SetDataSourceNull()
            {
                base[this.tableAnswers.DataSourceColumn] = Convert.DBNull;
            }

            public void SetDefaultTextNull()
            {
                base[this.tableAnswers.DefaultTextColumn] = Convert.DBNull;
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

            public void SetImageUrlNull()
            {
                base[this.tableAnswers.ImageUrlColumn] = Convert.DBNull;
            }

            public void SetMandatoryNull()
            {
                base[this.tableAnswers.MandatoryColumn] = Convert.DBNull;
            }

            public void SetSelectedNull()
            {
                base[this.tableAnswers.SelectedColumn] = Convert.DBNull;
            }

            public void SetTypeModeNull()
            {
                base[this.tableAnswers.TypeModeColumn] = Convert.DBNull;
            }

            public void SetUserSelectedNull()
            {
                base[this.tableAnswers.UserSelectedColumn] = Convert.DBNull;
            }

            public void SetUserTextNull()
            {
                base[this.tableAnswers.UserTextColumn] = Convert.DBNull;
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

            public Votations.NSurvey.Data.MatrixChildQuestionData.ChildQuestionsRow ChildQuestionsRow
            {
                get
                {
                    return (Votations.NSurvey.Data.MatrixChildQuestionData.ChildQuestionsRow) base.GetParentRow(base.Table.ParentRelations["ChildQuestionAnswers"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["ChildQuestionAnswers"]);
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

            public string DefaultText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswers.DefaultTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswers.DefaultTextColumn] = value;
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

            public string ImageUrl
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswers.ImageUrlColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswers.ImageUrlColumn] = value;
                }
            }

            public bool Mandatory
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswers.MandatoryColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswers.MandatoryColumn] = value;
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

            public bool Selected
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswers.SelectedColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswers.SelectedColumn] = value;
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

            public bool UserSelected
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswers.UserSelectedColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswers.UserSelectedColumn] = value;
                }
            }

            public string UserText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswers.UserTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswers.UserTextColumn] = value;
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
            private MatrixChildQuestionData.AnswersRow eventRow;

            public AnswersRowChangeEvent(MatrixChildQuestionData.AnswersRow row, DataRowAction action)
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

            public MatrixChildQuestionData.AnswersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswersRowChangeEventHandler(object sender, MatrixChildQuestionData.AnswersRowChangeEvent e);

        [DebuggerStepThrough]
        public class ChildQuestionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDisplayOrder;
            private DataColumn columnParentQuestionId;
            private DataColumn columnQuestionId;
            private DataColumn columnQuestionText;
            private DataColumn columnSelectionModeId;

            public event MatrixChildQuestionData.ChildQuestionsRowChangeEventHandler ChildQuestionsRowChanged;

            public event MatrixChildQuestionData.ChildQuestionsRowChangeEventHandler ChildQuestionsRowChanging;

            public event MatrixChildQuestionData.ChildQuestionsRowChangeEventHandler ChildQuestionsRowDeleted;

            public event MatrixChildQuestionData.ChildQuestionsRowChangeEventHandler ChildQuestionsRowDeleting;

            internal ChildQuestionsDataTable() : base("ChildQuestions")
            {
                this.InitClass();
            }

            internal ChildQuestionsDataTable(DataTable table) : base(table.TableName)
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

            public void AddChildQuestionsRow(MatrixChildQuestionData.ChildQuestionsRow row)
            {
                base.Rows.Add(row);
            }

            public MatrixChildQuestionData.ChildQuestionsRow AddChildQuestionsRow(int ParentQuestionId, int SelectionModeId, string QuestionText, int DisplayOrder)
            {
                MatrixChildQuestionData.ChildQuestionsRow row = (MatrixChildQuestionData.ChildQuestionsRow) base.NewRow();
                object[] objArray = new object[5];
                objArray[1] = ParentQuestionId;
                objArray[2] = SelectionModeId;
                objArray[3] = QuestionText;
                objArray[4] = DisplayOrder;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                MatrixChildQuestionData.ChildQuestionsDataTable table = (MatrixChildQuestionData.ChildQuestionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new MatrixChildQuestionData.ChildQuestionsDataTable();
            }

            public MatrixChildQuestionData.ChildQuestionsRow FindByQuestionId(int QuestionId)
            {
                return (MatrixChildQuestionData.ChildQuestionsRow) base.Rows.Find(new object[] { QuestionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(MatrixChildQuestionData.ChildQuestionsRow);
            }

            private void InitClass()
            {
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnParentQuestionId = new DataColumn("ParentQuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnParentQuestionId);
                this.columnSelectionModeId = new DataColumn("SelectionModeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSelectionModeId);
                this.columnQuestionText = new DataColumn("QuestionText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionText);
                this.columnDisplayOrder = new DataColumn("DisplayOrder", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnDisplayOrder);
                base.Constraints.Add(new UniqueConstraint("MatrixChildQuestionsKey1", new DataColumn[] { this.columnQuestionId }, true));
                this.columnQuestionId.AutoIncrement = true;
                this.columnQuestionId.AutoIncrementSeed = -1L;
                this.columnQuestionId.AutoIncrementStep = -1L;
                this.columnQuestionId.AllowDBNull = false;
                this.columnQuestionId.ReadOnly = true;
                this.columnQuestionId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnParentQuestionId = base.Columns["ParentQuestionId"];
                this.columnSelectionModeId = base.Columns["SelectionModeId"];
                this.columnQuestionText = base.Columns["QuestionText"];
                this.columnDisplayOrder = base.Columns["DisplayOrder"];
            }

            public MatrixChildQuestionData.ChildQuestionsRow NewChildQuestionsRow()
            {
                return (MatrixChildQuestionData.ChildQuestionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new MatrixChildQuestionData.ChildQuestionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.ChildQuestionsRowChanged != null)
                {
                    this.ChildQuestionsRowChanged(this, new MatrixChildQuestionData.ChildQuestionsRowChangeEvent((MatrixChildQuestionData.ChildQuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.ChildQuestionsRowChanging != null)
                {
                    this.ChildQuestionsRowChanging(this, new MatrixChildQuestionData.ChildQuestionsRowChangeEvent((MatrixChildQuestionData.ChildQuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.ChildQuestionsRowDeleted != null)
                {
                    this.ChildQuestionsRowDeleted(this, new MatrixChildQuestionData.ChildQuestionsRowChangeEvent((MatrixChildQuestionData.ChildQuestionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.ChildQuestionsRowDeleting != null)
                {
                    this.ChildQuestionsRowDeleting(this, new MatrixChildQuestionData.ChildQuestionsRowChangeEvent((MatrixChildQuestionData.ChildQuestionsRow) e.Row, e.Action));
                }
            }

            public void RemoveChildQuestionsRow(MatrixChildQuestionData.ChildQuestionsRow row)
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

            public MatrixChildQuestionData.ChildQuestionsRow this[int index]
            {
                get
                {
                    return (MatrixChildQuestionData.ChildQuestionsRow) base.Rows[index];
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

            internal DataColumn QuestionTextColumn
            {
                get
                {
                    return this.columnQuestionText;
                }
            }

            internal DataColumn SelectionModeIdColumn
            {
                get
                {
                    return this.columnSelectionModeId;
                }
            }
        }

        [DebuggerStepThrough]
        public class ChildQuestionsRow : DataRow
        {
            private MatrixChildQuestionData.ChildQuestionsDataTable tableChildQuestions;

            internal ChildQuestionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableChildQuestions = (MatrixChildQuestionData.ChildQuestionsDataTable) base.Table;
            }

            public MatrixChildQuestionData.AnswersRow[] GetAnswersRows()
            {
                return (MatrixChildQuestionData.AnswersRow[]) base.GetChildRows(base.Table.ChildRelations["ChildQuestionAnswers"]);
            }

            public bool IsDisplayOrderNull()
            {
                return base.IsNull(this.tableChildQuestions.DisplayOrderColumn);
            }

            public bool IsParentQuestionIdNull()
            {
                return base.IsNull(this.tableChildQuestions.ParentQuestionIdColumn);
            }

            public bool IsQuestionTextNull()
            {
                return base.IsNull(this.tableChildQuestions.QuestionTextColumn);
            }

            public bool IsSelectionModeIdNull()
            {
                return base.IsNull(this.tableChildQuestions.SelectionModeIdColumn);
            }

            public void SetDisplayOrderNull()
            {
                base[this.tableChildQuestions.DisplayOrderColumn] = Convert.DBNull;
            }

            public void SetParentQuestionIdNull()
            {
                base[this.tableChildQuestions.ParentQuestionIdColumn] = Convert.DBNull;
            }

            public void SetQuestionTextNull()
            {
                base[this.tableChildQuestions.QuestionTextColumn] = Convert.DBNull;
            }

            public void SetSelectionModeIdNull()
            {
                base[this.tableChildQuestions.SelectionModeIdColumn] = Convert.DBNull;
            }

            public int DisplayOrder
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableChildQuestions.DisplayOrderColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableChildQuestions.DisplayOrderColumn] = value;
                }
            }

            public int ParentQuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableChildQuestions.ParentQuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableChildQuestions.ParentQuestionIdColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    return (int) base[this.tableChildQuestions.QuestionIdColumn];
                }
                set
                {
                    base[this.tableChildQuestions.QuestionIdColumn] = value;
                }
            }

            public string QuestionText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableChildQuestions.QuestionTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableChildQuestions.QuestionTextColumn] = value;
                }
            }

            public int SelectionModeId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableChildQuestions.SelectionModeIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableChildQuestions.SelectionModeIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class ChildQuestionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private MatrixChildQuestionData.ChildQuestionsRow eventRow;

            public ChildQuestionsRowChangeEvent(MatrixChildQuestionData.ChildQuestionsRow row, DataRowAction action)
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

            public MatrixChildQuestionData.ChildQuestionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void ChildQuestionsRowChangeEventHandler(object sender, MatrixChildQuestionData.ChildQuestionsRowChangeEvent e);
    }
}

