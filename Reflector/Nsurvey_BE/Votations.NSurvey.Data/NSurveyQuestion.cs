namespace Votations.NSurvey.BE.Votations.NSurvey.Data
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
    public partial class NSurveyQuestion : DataSet
    {
        
    
        private DataRelation relationAnswerAnswerProperty;
        private DataRelation relationAnswerQuestionSectionGridAnswer;
        private DataRelation relationAnswerTypeAnswer;
        private DataRelation relationPublisherAnswerConnection;
        private DataRelation relationQuestionAnswer;
        private DataRelation relationQuestionAnswerConnection;
        private DataRelation relationQuestionChildQuestion;
        private DataRelation relationQuestionQuestionSectionOption;
        private DataRelation relationQuestionSectionOptionQuestionSectionGridAnswer;
        private DataRelation relationRegularExpressionAnswer;
        private DataRelation relationSubscriberAnswerConnection;
        private AnswerDataTable tableAnswer;
        private AnswerConnectionDataTable tableAnswerConnection;
        private AnswerPropertyDataTable tableAnswerProperty;
        private AnswerTypeDataTable tableAnswerType;
        private ChildQuestionDataTable tableChildQuestion;
        private QuestionDataTable tableQuestion;
        private QuestionSectionGridAnswerDataTable tableQuestionSectionGridAnswer;
        private QuestionSectionOptionDataTable tableQuestionSectionOption;
        private RegularExpressionDataTable tableRegularExpression;

        public NSurveyQuestion()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected NSurveyQuestion(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["AnswerType"] != null)
                {
                    base.Tables.Add(new AnswerTypeDataTable(dataSet.Tables["AnswerType"]));
                }
                if (dataSet.Tables["Question"] != null)
                {
                    base.Tables.Add(new QuestionDataTable(dataSet.Tables["Question"]));
                }
                if (dataSet.Tables["Answer"] != null)
                {
                    base.Tables.Add(new AnswerDataTable(dataSet.Tables["Answer"]));
                }
                if (dataSet.Tables["AnswerProperty"] != null)
                {
                    base.Tables.Add(new AnswerPropertyDataTable(dataSet.Tables["AnswerProperty"]));
                }
                if (dataSet.Tables["ChildQuestion"] != null)
                {
                    base.Tables.Add(new ChildQuestionDataTable(dataSet.Tables["ChildQuestion"]));
                }
                if (dataSet.Tables["AnswerConnection"] != null)
                {
                    base.Tables.Add(new AnswerConnectionDataTable(dataSet.Tables["AnswerConnection"]));
                }
                if (dataSet.Tables["QuestionSectionOption"] != null)
                {
                    base.Tables.Add(new QuestionSectionOptionDataTable(dataSet.Tables["QuestionSectionOption"]));
                }
                if (dataSet.Tables["QuestionSectionGridAnswer"] != null)
                {
                    base.Tables.Add(new QuestionSectionGridAnswerDataTable(dataSet.Tables["QuestionSectionGridAnswer"]));
                }
                if (dataSet.Tables["RegularExpression"] != null)
                {
                    base.Tables.Add(new RegularExpressionDataTable(dataSet.Tables["RegularExpression"]));
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
            NSurveyQuestion question = (NSurveyQuestion) base.Clone();
            question.InitVars();
            return question;
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
            base.DataSetName = "NSurveyQuestion";
            base.Prefix = "";
            base.Namespace = "http://www.nsurvey.org/nsurveyquestion.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableAnswerType = new AnswerTypeDataTable();
            base.Tables.Add(this.tableAnswerType);
            this.tableQuestion = new QuestionDataTable();
            base.Tables.Add(this.tableQuestion);
            this.tableAnswer = new AnswerDataTable();
            base.Tables.Add(this.tableAnswer);
            this.tableAnswerProperty = new AnswerPropertyDataTable();
            base.Tables.Add(this.tableAnswerProperty);
            this.tableChildQuestion = new ChildQuestionDataTable();
            base.Tables.Add(this.tableChildQuestion);
            this.tableAnswerConnection = new AnswerConnectionDataTable();
            base.Tables.Add(this.tableAnswerConnection);
            this.tableQuestionSectionOption = new QuestionSectionOptionDataTable();
            base.Tables.Add(this.tableQuestionSectionOption);
            this.tableQuestionSectionGridAnswer = new QuestionSectionGridAnswerDataTable();
            base.Tables.Add(this.tableQuestionSectionGridAnswer);
            this.tableRegularExpression = new RegularExpressionDataTable();
            base.Tables.Add(this.tableRegularExpression);
            ForeignKeyConstraint constraint = new ForeignKeyConstraint("QuestionAnswer", new DataColumn[] { this.tableQuestion.QuestionIdColumn }, new DataColumn[] { this.tableAnswer.QuestionIdColumn });
            this.tableAnswer.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("AnswerTypeAnswer", new DataColumn[] { this.tableAnswerType.AnswerTypeIDColumn }, new DataColumn[] { this.tableAnswer.AnswerTypeIdColumn });
            this.tableAnswer.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("RegularExpressionAnswer", new DataColumn[] { this.tableRegularExpression.RegularExpressionIdColumn }, new DataColumn[] { this.tableAnswer.RegularExpressionIdColumn });
            this.tableAnswer.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("AnswerAnswerProperty", new DataColumn[] { this.tableAnswer.AnswerIdColumn }, new DataColumn[] { this.tableAnswerProperty.AnswerIdColumn });
            this.tableAnswerProperty.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("QuestionChildQuestion", new DataColumn[] { this.tableQuestion.QuestionIdColumn }, new DataColumn[] { this.tableChildQuestion.ParentQuestionIdColumn });
            this.tableChildQuestion.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("QuestionAnswerConnection", new DataColumn[] { this.tableQuestion.QuestionIdColumn }, new DataColumn[] { this.tableAnswerConnection.QuestionIdColumn });
            this.tableAnswerConnection.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("PublisherAnswerConnection", new DataColumn[] { this.tableAnswer.AnswerIdColumn }, new DataColumn[] { this.tableAnswerConnection.PublisherAnswerIdColumn });
            this.tableAnswerConnection.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("SubscriberAnswerConnection", new DataColumn[] { this.tableAnswer.AnswerIdColumn }, new DataColumn[] { this.tableAnswerConnection.SubscriberAnswerIdColumn });
            this.tableAnswerConnection.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("QuestionQuestionSectionOption", new DataColumn[] { this.tableQuestion.QuestionIdColumn }, new DataColumn[] { this.tableQuestionSectionOption.QuestionIdColumn });
            this.tableQuestionSectionOption.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("QuestionSectionOptionQuestionSectionGridAnswer", new DataColumn[] { this.tableQuestionSectionOption.QuestionIdColumn }, new DataColumn[] { this.tableQuestionSectionGridAnswer.QuestionIdColumn });
            this.tableQuestionSectionGridAnswer.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("AnswerQuestionSectionGridAnswer", new DataColumn[] { this.tableAnswer.AnswerIdColumn }, new DataColumn[] { this.tableQuestionSectionGridAnswer.AnswerIdColumn });
            this.tableQuestionSectionGridAnswer.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            this.relationAnswerAnswerProperty = new DataRelation("AnswerAnswerProperty", new DataColumn[] { this.tableAnswer.AnswerIdColumn }, new DataColumn[] { this.tableAnswerProperty.AnswerIdColumn }, false);
            this.relationAnswerAnswerProperty.Nested = true;
            base.Relations.Add(this.relationAnswerAnswerProperty);
            this.relationQuestionSectionOptionQuestionSectionGridAnswer = new DataRelation("QuestionSectionOptionQuestionSectionGridAnswer", new DataColumn[] { this.tableQuestionSectionOption.QuestionIdColumn }, new DataColumn[] { this.tableQuestionSectionGridAnswer.QuestionIdColumn }, false);
            this.relationQuestionSectionOptionQuestionSectionGridAnswer.Nested = true;
            base.Relations.Add(this.relationQuestionSectionOptionQuestionSectionGridAnswer);
            this.relationQuestionAnswer = new DataRelation("QuestionAnswer", new DataColumn[] { this.tableQuestion.QuestionIdColumn }, new DataColumn[] { this.tableAnswer.QuestionIdColumn }, false);
            this.relationQuestionAnswer.Nested = true;
            base.Relations.Add(this.relationQuestionAnswer);
            this.relationQuestionChildQuestion = new DataRelation("QuestionChildQuestion", new DataColumn[] { this.tableQuestion.QuestionIdColumn }, new DataColumn[] { this.tableChildQuestion.ParentQuestionIdColumn }, false);
            this.relationQuestionChildQuestion.Nested = true;
            base.Relations.Add(this.relationQuestionChildQuestion);
            this.relationQuestionAnswerConnection = new DataRelation("QuestionAnswerConnection", new DataColumn[] { this.tableQuestion.QuestionIdColumn }, new DataColumn[] { this.tableAnswerConnection.QuestionIdColumn }, false);
            this.relationQuestionAnswerConnection.Nested = true;
            base.Relations.Add(this.relationQuestionAnswerConnection);
            this.relationQuestionQuestionSectionOption = new DataRelation("QuestionQuestionSectionOption", new DataColumn[] { this.tableQuestion.QuestionIdColumn }, new DataColumn[] { this.tableQuestionSectionOption.QuestionIdColumn }, false);
            this.relationQuestionQuestionSectionOption.Nested = true;
            base.Relations.Add(this.relationQuestionQuestionSectionOption);
            this.relationAnswerTypeAnswer = new DataRelation("AnswerTypeAnswer", new DataColumn[] { this.tableAnswerType.AnswerTypeIDColumn }, new DataColumn[] { this.tableAnswer.AnswerTypeIdColumn }, false);
            base.Relations.Add(this.relationAnswerTypeAnswer);
            this.relationPublisherAnswerConnection = new DataRelation("PublisherAnswerConnection", new DataColumn[] { this.tableAnswer.AnswerIdColumn }, new DataColumn[] { this.tableAnswerConnection.PublisherAnswerIdColumn }, false);
            base.Relations.Add(this.relationPublisherAnswerConnection);
            this.relationSubscriberAnswerConnection = new DataRelation("SubscriberAnswerConnection", new DataColumn[] { this.tableAnswer.AnswerIdColumn }, new DataColumn[] { this.tableAnswerConnection.SubscriberAnswerIdColumn }, false);
            base.Relations.Add(this.relationSubscriberAnswerConnection);
            this.relationRegularExpressionAnswer = new DataRelation("RegularExpressionAnswer", new DataColumn[] { this.tableRegularExpression.RegularExpressionIdColumn }, new DataColumn[] { this.tableAnswer.RegularExpressionIdColumn }, false);
            base.Relations.Add(this.relationRegularExpressionAnswer);
            this.relationAnswerQuestionSectionGridAnswer = new DataRelation("AnswerQuestionSectionGridAnswer", new DataColumn[] { this.tableAnswer.AnswerIdColumn }, new DataColumn[] { this.tableQuestionSectionGridAnswer.AnswerIdColumn }, false);
            base.Relations.Add(this.relationAnswerQuestionSectionGridAnswer);
        }

        internal void InitVars()
        {
            this.tableAnswerType = (AnswerTypeDataTable) base.Tables["AnswerType"];
            if (this.tableAnswerType != null)
            {
                this.tableAnswerType.InitVars();
            }
            this.tableQuestion = (QuestionDataTable) base.Tables["Question"];
            if (this.tableQuestion != null)
            {
                this.tableQuestion.InitVars();
            }
            this.tableAnswer = (AnswerDataTable) base.Tables["Answer"];
            if (this.tableAnswer != null)
            {
                this.tableAnswer.InitVars();
            }
            this.tableAnswerProperty = (AnswerPropertyDataTable) base.Tables["AnswerProperty"];
            if (this.tableAnswerProperty != null)
            {
                this.tableAnswerProperty.InitVars();
            }
            this.tableChildQuestion = (ChildQuestionDataTable) base.Tables["ChildQuestion"];
            if (this.tableChildQuestion != null)
            {
                this.tableChildQuestion.InitVars();
            }
            this.tableAnswerConnection = (AnswerConnectionDataTable) base.Tables["AnswerConnection"];
            if (this.tableAnswerConnection != null)
            {
                this.tableAnswerConnection.InitVars();
            }
            this.tableQuestionSectionOption = (QuestionSectionOptionDataTable) base.Tables["QuestionSectionOption"];
            if (this.tableQuestionSectionOption != null)
            {
                this.tableQuestionSectionOption.InitVars();
            }
            this.tableQuestionSectionGridAnswer = (QuestionSectionGridAnswerDataTable) base.Tables["QuestionSectionGridAnswer"];
            if (this.tableQuestionSectionGridAnswer != null)
            {
                this.tableQuestionSectionGridAnswer.InitVars();
            }
            this.tableRegularExpression = (RegularExpressionDataTable) base.Tables["RegularExpression"];
            if (this.tableRegularExpression != null)
            {
                this.tableRegularExpression.InitVars();
            }
            this.relationAnswerAnswerProperty = base.Relations["AnswerAnswerProperty"];
            this.relationQuestionSectionOptionQuestionSectionGridAnswer = base.Relations["QuestionSectionOptionQuestionSectionGridAnswer"];
            this.relationQuestionAnswer = base.Relations["QuestionAnswer"];
            this.relationQuestionChildQuestion = base.Relations["QuestionChildQuestion"];
            this.relationQuestionAnswerConnection = base.Relations["QuestionAnswerConnection"];
            this.relationQuestionQuestionSectionOption = base.Relations["QuestionQuestionSectionOption"];
            this.relationAnswerTypeAnswer = base.Relations["AnswerTypeAnswer"];
            this.relationPublisherAnswerConnection = base.Relations["PublisherAnswerConnection"];
            this.relationSubscriberAnswerConnection = base.Relations["SubscriberAnswerConnection"];
            this.relationRegularExpressionAnswer = base.Relations["RegularExpressionAnswer"];
            this.relationAnswerQuestionSectionGridAnswer = base.Relations["AnswerQuestionSectionGridAnswer"];
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["AnswerType"] != null)
            {
                base.Tables.Add(new AnswerTypeDataTable(dataSet.Tables["AnswerType"]));
            }
            if (dataSet.Tables["Question"] != null)
            {
                base.Tables.Add(new QuestionDataTable(dataSet.Tables["Question"]));
            }
            if (dataSet.Tables["Answer"] != null)
            {
                base.Tables.Add(new AnswerDataTable(dataSet.Tables["Answer"]));
            }
            if (dataSet.Tables["AnswerProperty"] != null)
            {
                base.Tables.Add(new AnswerPropertyDataTable(dataSet.Tables["AnswerProperty"]));
            }
            if (dataSet.Tables["ChildQuestion"] != null)
            {
                base.Tables.Add(new ChildQuestionDataTable(dataSet.Tables["ChildQuestion"]));
            }
            if (dataSet.Tables["AnswerConnection"] != null)
            {
                base.Tables.Add(new AnswerConnectionDataTable(dataSet.Tables["AnswerConnection"]));
            }
            if (dataSet.Tables["QuestionSectionOption"] != null)
            {
                base.Tables.Add(new QuestionSectionOptionDataTable(dataSet.Tables["QuestionSectionOption"]));
            }
            if (dataSet.Tables["QuestionSectionGridAnswer"] != null)
            {
                base.Tables.Add(new QuestionSectionGridAnswerDataTable(dataSet.Tables["QuestionSectionGridAnswer"]));
            }
            if (dataSet.Tables["RegularExpression"] != null)
            {
                base.Tables.Add(new RegularExpressionDataTable(dataSet.Tables["RegularExpression"]));
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

        private bool ShouldSerializeAnswer()
        {
            return false;
        }

        private bool ShouldSerializeAnswerConnection()
        {
            return false;
        }

        private bool ShouldSerializeAnswerProperty()
        {
            return false;
        }

        private bool ShouldSerializeAnswerType()
        {
            return false;
        }

        private bool ShouldSerializeChildQuestion()
        {
            return false;
        }

        private bool ShouldSerializeQuestion()
        {
            return false;
        }

        private bool ShouldSerializeQuestionSectionGridAnswer()
        {
            return false;
        }

        private bool ShouldSerializeQuestionSectionOption()
        {
            return false;
        }

        private bool ShouldSerializeRegularExpression()
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
        public AnswerDataTable Answer
        {
            get
            {
                return this.tableAnswer;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public AnswerConnectionDataTable AnswerConnection
        {
            get
            {
                return this.tableAnswerConnection;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AnswerPropertyDataTable AnswerProperty
        {
            get
            {
                return this.tableAnswerProperty;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public AnswerTypeDataTable AnswerType
        {
            get
            {
                return this.tableAnswerType;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChildQuestionDataTable ChildQuestion
        {
            get
            {
                return this.tableChildQuestion;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public QuestionDataTable Question
        {
            get
            {
                return this.tableQuestion;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public QuestionSectionGridAnswerDataTable QuestionSectionGridAnswer
        {
            get
            {
                return this.tableQuestionSectionGridAnswer;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public QuestionSectionOptionDataTable QuestionSectionOption
        {
            get
            {
                return this.tableQuestionSectionOption;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RegularExpressionDataTable RegularExpression
        {
            get
            {
                return this.tableRegularExpression;
            }
        }

        [DebuggerStepThrough]
        public partial class AnswerConnectionDataTable : DataTable, IEnumerable
        {
            private DataColumn columnPublisherAnswerId;
            private DataColumn columnQuestionId;
            private DataColumn columnSubscriberAnswerId;

            public event NSurveyQuestion.AnswerConnectionRowChangeEventHandler AnswerConnectionRowChanged;

            public event NSurveyQuestion.AnswerConnectionRowChangeEventHandler AnswerConnectionRowChanging;

            public event NSurveyQuestion.AnswerConnectionRowChangeEventHandler AnswerConnectionRowDeleted;

            public event NSurveyQuestion.AnswerConnectionRowChangeEventHandler AnswerConnectionRowDeleting;

            internal AnswerConnectionDataTable() : base("AnswerConnection")
            {
                this.InitClass();
            }

            internal AnswerConnectionDataTable(DataTable table) : base(table.TableName)
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

            public void AddAnswerConnectionRow(NSurveyQuestion.AnswerConnectionRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.AnswerConnectionRow AddAnswerConnectionRow(NSurveyQuestion.AnswerRow parentAnswerRowByPublisherAnswerConnection, NSurveyQuestion.AnswerRow parentAnswerRowBySubscriberAnswerConnection, NSurveyQuestion.QuestionRow parentQuestionRowByQuestionAnswerConnection)
            {
                NSurveyQuestion.AnswerConnectionRow row = (NSurveyQuestion.AnswerConnectionRow) base.NewRow();
                row.ItemArray = new object[] { parentAnswerRowByPublisherAnswerConnection[6], parentAnswerRowBySubscriberAnswerConnection[6], parentQuestionRowByQuestionAnswerConnection[0] };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.AnswerConnectionDataTable table = (NSurveyQuestion.AnswerConnectionDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.AnswerConnectionDataTable();
            }

            public NSurveyQuestion.AnswerConnectionRow FindByPublisherAnswerIdSubscriberAnswerId(int PublisherAnswerId, int SubscriberAnswerId)
            {
                return (NSurveyQuestion.AnswerConnectionRow) base.Rows.Find(new object[] { PublisherAnswerId, SubscriberAnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.AnswerConnectionRow);
            }

            private void InitClass()
            {
                this.columnPublisherAnswerId = new DataColumn("PublisherAnswerId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnPublisherAnswerId);
                this.columnSubscriberAnswerId = new DataColumn("SubscriberAnswerId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnSubscriberAnswerId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnQuestionId);
                base.Constraints.Add(new UniqueConstraint("NSurveyQuestionKey2", new DataColumn[] { this.columnPublisherAnswerId, this.columnSubscriberAnswerId }, true));
                this.columnPublisherAnswerId.AllowDBNull = false;
                this.columnPublisherAnswerId.Namespace = "";
                this.columnSubscriberAnswerId.AllowDBNull = false;
                this.columnSubscriberAnswerId.Namespace = "";
                this.columnQuestionId.Namespace = "";
            }

            internal void InitVars()
            {
                this.columnPublisherAnswerId = base.Columns["PublisherAnswerId"];
                this.columnSubscriberAnswerId = base.Columns["SubscriberAnswerId"];
                this.columnQuestionId = base.Columns["QuestionId"];
            }

            public NSurveyQuestion.AnswerConnectionRow NewAnswerConnectionRow()
            {
                return (NSurveyQuestion.AnswerConnectionRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.AnswerConnectionRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswerConnectionRowChanged != null)
                {
                    this.AnswerConnectionRowChanged(this, new NSurveyQuestion.AnswerConnectionRowChangeEvent((NSurveyQuestion.AnswerConnectionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswerConnectionRowChanging != null)
                {
                    this.AnswerConnectionRowChanging(this, new NSurveyQuestion.AnswerConnectionRowChangeEvent((NSurveyQuestion.AnswerConnectionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswerConnectionRowDeleted != null)
                {
                    this.AnswerConnectionRowDeleted(this, new NSurveyQuestion.AnswerConnectionRowChangeEvent((NSurveyQuestion.AnswerConnectionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswerConnectionRowDeleting != null)
                {
                    this.AnswerConnectionRowDeleting(this, new NSurveyQuestion.AnswerConnectionRowChangeEvent((NSurveyQuestion.AnswerConnectionRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswerConnectionRow(NSurveyQuestion.AnswerConnectionRow row)
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

            public NSurveyQuestion.AnswerConnectionRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.AnswerConnectionRow) base.Rows[index];
                }
            }

            internal DataColumn PublisherAnswerIdColumn
            {
                get
                {
                    return this.columnPublisherAnswerId;
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn SubscriberAnswerIdColumn
            {
                get
                {
                    return this.columnSubscriberAnswerId;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class AnswerConnectionRow : DataRow
        {
            private NSurveyQuestion.AnswerConnectionDataTable tableAnswerConnection;

            internal AnswerConnectionRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswerConnection = (NSurveyQuestion.AnswerConnectionDataTable) base.Table;
            }

            public bool IsQuestionIdNull()
            {
                return base.IsNull(this.tableAnswerConnection.QuestionIdColumn);
            }

            public void SetQuestionIdNull()
            {
                base[this.tableAnswerConnection.QuestionIdColumn] = Convert.DBNull;
            }

            public NSurveyQuestion.AnswerRow AnswerRowByPublisherAnswerConnection
            {
                get
                {
                    return (NSurveyQuestion.AnswerRow) base.GetParentRow(base.Table.ParentRelations["PublisherAnswerConnection"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["PublisherAnswerConnection"]);
                }
            }

            public NSurveyQuestion.AnswerRow AnswerRowBySubscriberAnswerConnection
            {
                get
                {
                    return (NSurveyQuestion.AnswerRow) base.GetParentRow(base.Table.ParentRelations["SubscriberAnswerConnection"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["SubscriberAnswerConnection"]);
                }
            }

            public int PublisherAnswerId
            {
                get
                {
                    return (int) base[this.tableAnswerConnection.PublisherAnswerIdColumn];
                }
                set
                {
                    base[this.tableAnswerConnection.PublisherAnswerIdColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswerConnection.QuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswerConnection.QuestionIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.QuestionRow QuestionRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.QuestionRow) base.GetParentRow(base.Table.ParentRelations["QuestionAnswerConnection"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["QuestionAnswerConnection"]);
                }
            }

            public int SubscriberAnswerId
            {
                get
                {
                    return (int) base[this.tableAnswerConnection.SubscriberAnswerIdColumn];
                }
                set
                {
                    base[this.tableAnswerConnection.SubscriberAnswerIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class AnswerConnectionRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.AnswerConnectionRow eventRow;

            public AnswerConnectionRowChangeEvent(NSurveyQuestion.AnswerConnectionRow row, DataRowAction action)
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

            public NSurveyQuestion.AnswerConnectionRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswerConnectionRowChangeEventHandler(object sender, NSurveyQuestion.AnswerConnectionRowChangeEvent e);

        [DebuggerStepThrough]
        public partial class AnswerDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnAnswerPipeAlias;
            private DataColumn columnAnswerText;
            private DataColumn columnAnswerTypeId;
            private DataColumn columnDefaultText;
            private DataColumn columnDisplayOrder;
            private DataColumn columnImageURL;
            private DataColumn columnMandatory;
            private DataColumn columnQuestionId;
            private DataColumn columnRatePart;
            private DataColumn columnRegularExpressionId;
            private DataColumn columnScorePoint;
            private DataColumn columnSelected;

            public event NSurveyQuestion.AnswerRowChangeEventHandler AnswerRowChanged;

            public event NSurveyQuestion.AnswerRowChangeEventHandler AnswerRowChanging;

            public event NSurveyQuestion.AnswerRowChangeEventHandler AnswerRowDeleted;

            public event NSurveyQuestion.AnswerRowChangeEventHandler AnswerRowDeleting;

            internal AnswerDataTable() : base("Answer")
            {
                this.InitClass();
            }

            internal AnswerDataTable(DataTable table) : base(table.TableName)
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

            public void AddAnswerRow(NSurveyQuestion.AnswerRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.AnswerRow AddAnswerRow(NSurveyQuestion.QuestionRow parentQuestionRowByQuestionAnswer, NSurveyQuestion.AnswerTypeRow parentAnswerTypeRowByAnswerTypeAnswer, int DisplayOrder, int ScorePoint, bool RatePart, bool Selected, int AnswerId, NSurveyQuestion.RegularExpressionRow parentRegularExpressionRowByRegularExpressionAnswer, bool Mandatory, string AnswerText, string ImageURL, string DefaultText, string AnswerPipeAlias)
            {
                NSurveyQuestion.AnswerRow row = (NSurveyQuestion.AnswerRow) base.NewRow();
                row.ItemArray = new object[] { parentQuestionRowByQuestionAnswer[0], parentAnswerTypeRowByAnswerTypeAnswer[0], DisplayOrder, ScorePoint, RatePart, Selected, AnswerId, parentRegularExpressionRowByRegularExpressionAnswer[0], Mandatory, AnswerText, ImageURL, DefaultText, AnswerPipeAlias };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.AnswerDataTable table = (NSurveyQuestion.AnswerDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.AnswerDataTable();
            }

            public NSurveyQuestion.AnswerRow FindByAnswerId(int AnswerId)
            {
                return (NSurveyQuestion.AnswerRow) base.Rows.Find(new object[] { AnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.AnswerRow);
            }

            private void InitClass()
            {
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnQuestionId);
                this.columnAnswerTypeId = new DataColumn("AnswerTypeId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnAnswerTypeId);
                this.columnDisplayOrder = new DataColumn("DisplayOrder", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnDisplayOrder);
                this.columnScorePoint = new DataColumn("ScorePoint", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnScorePoint);
                this.columnRatePart = new DataColumn("RatePart", typeof(bool), null, MappingType.Attribute);
                base.Columns.Add(this.columnRatePart);
                this.columnSelected = new DataColumn("Selected", typeof(bool), null, MappingType.Attribute);
                base.Columns.Add(this.columnSelected);
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnAnswerId);
                this.columnRegularExpressionId = new DataColumn("RegularExpressionId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnRegularExpressionId);
                this.columnMandatory = new DataColumn("Mandatory", typeof(bool), null, MappingType.Attribute);
                base.Columns.Add(this.columnMandatory);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnImageURL = new DataColumn("ImageURL", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnImageURL);
                this.columnDefaultText = new DataColumn("DefaultText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDefaultText);
                this.columnAnswerPipeAlias = new DataColumn("AnswerPipeAlias", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerPipeAlias);
                base.Constraints.Add(new UniqueConstraint("NSurveyQuestionKey5", new DataColumn[] { this.columnAnswerId }, true));
                this.columnQuestionId.Namespace = "";
                this.columnAnswerTypeId.Namespace = "";
                this.columnDisplayOrder.Namespace = "";
                this.columnScorePoint.Namespace = "";
                this.columnRatePart.Namespace = "";
                this.columnSelected.Namespace = "";
                this.columnAnswerId.AllowDBNull = false;
                this.columnAnswerId.Unique = true;
                this.columnAnswerId.Namespace = "";
                this.columnRegularExpressionId.Namespace = "";
                this.columnMandatory.Namespace = "";
            }

            internal void InitVars()
            {
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnAnswerTypeId = base.Columns["AnswerTypeId"];
                this.columnDisplayOrder = base.Columns["DisplayOrder"];
                this.columnScorePoint = base.Columns["ScorePoint"];
                this.columnRatePart = base.Columns["RatePart"];
                this.columnSelected = base.Columns["Selected"];
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnRegularExpressionId = base.Columns["RegularExpressionId"];
                this.columnMandatory = base.Columns["Mandatory"];
                this.columnAnswerText = base.Columns["AnswerText"];
                this.columnImageURL = base.Columns["ImageURL"];
                this.columnDefaultText = base.Columns["DefaultText"];
                this.columnAnswerPipeAlias = base.Columns["AnswerPipeAlias"];
            }

            public NSurveyQuestion.AnswerRow NewAnswerRow()
            {
                return (NSurveyQuestion.AnswerRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.AnswerRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswerRowChanged != null)
                {
                    this.AnswerRowChanged(this, new NSurveyQuestion.AnswerRowChangeEvent((NSurveyQuestion.AnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswerRowChanging != null)
                {
                    this.AnswerRowChanging(this, new NSurveyQuestion.AnswerRowChangeEvent((NSurveyQuestion.AnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswerRowDeleted != null)
                {
                    this.AnswerRowDeleted(this, new NSurveyQuestion.AnswerRowChangeEvent((NSurveyQuestion.AnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswerRowDeleting != null)
                {
                    this.AnswerRowDeleting(this, new NSurveyQuestion.AnswerRowChangeEvent((NSurveyQuestion.AnswerRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswerRow(NSurveyQuestion.AnswerRow row)
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

            internal DataColumn DefaultTextColumn
            {
                get
                {
                    return this.columnDefaultText;
                }
            }

            internal DataColumn DisplayOrderColumn
            {
                get
                {
                    return this.columnDisplayOrder;
                }
            }

            internal DataColumn ImageURLColumn
            {
                get
                {
                    return this.columnImageURL;
                }
            }

            public NSurveyQuestion.AnswerRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.AnswerRow) base.Rows[index];
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

            internal DataColumn RatePartColumn
            {
                get
                {
                    return this.columnRatePart;
                }
            }

            internal DataColumn RegularExpressionIdColumn
            {
                get
                {
                    return this.columnRegularExpressionId;
                }
            }

            internal DataColumn ScorePointColumn
            {
                get
                {
                    return this.columnScorePoint;
                }
            }

            internal DataColumn SelectedColumn
            {
                get
                {
                    return this.columnSelected;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class AnswerPropertyDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnProperties;

            public event NSurveyQuestion.AnswerPropertyRowChangeEventHandler AnswerPropertyRowChanged;

            public event NSurveyQuestion.AnswerPropertyRowChangeEventHandler AnswerPropertyRowChanging;

            public event NSurveyQuestion.AnswerPropertyRowChangeEventHandler AnswerPropertyRowDeleted;

            public event NSurveyQuestion.AnswerPropertyRowChangeEventHandler AnswerPropertyRowDeleting;

            internal AnswerPropertyDataTable() : base("AnswerProperty")
            {
                this.InitClass();
            }

            internal AnswerPropertyDataTable(DataTable table) : base(table.TableName)
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

            public void AddAnswerPropertyRow(NSurveyQuestion.AnswerPropertyRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.AnswerPropertyRow AddAnswerPropertyRow(NSurveyQuestion.AnswerRow parentAnswerRowByAnswerAnswerProperty, byte[] Properties)
            {
                NSurveyQuestion.AnswerPropertyRow row = (NSurveyQuestion.AnswerPropertyRow) base.NewRow();
                row.ItemArray = new object[] { parentAnswerRowByAnswerAnswerProperty[6], Properties };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.AnswerPropertyDataTable table = (NSurveyQuestion.AnswerPropertyDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.AnswerPropertyDataTable();
            }

            public NSurveyQuestion.AnswerPropertyRow FindByAnswerId(int AnswerId)
            {
                return (NSurveyQuestion.AnswerPropertyRow) base.Rows.Find(new object[] { AnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.AnswerPropertyRow);
            }

            private void InitClass()
            {
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnAnswerId);
                this.columnProperties = new DataColumn("Properties", typeof(byte[]), null, MappingType.Element);
                base.Columns.Add(this.columnProperties);
                base.Constraints.Add(new UniqueConstraint("NSurveyQuestionKey7", new DataColumn[] { this.columnAnswerId }, true));
                this.columnAnswerId.AllowDBNull = false;
                this.columnAnswerId.Unique = true;
                this.columnAnswerId.Namespace = "";
            }

            internal void InitVars()
            {
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnProperties = base.Columns["Properties"];
            }

            public NSurveyQuestion.AnswerPropertyRow NewAnswerPropertyRow()
            {
                return (NSurveyQuestion.AnswerPropertyRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.AnswerPropertyRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswerPropertyRowChanged != null)
                {
                    this.AnswerPropertyRowChanged(this, new NSurveyQuestion.AnswerPropertyRowChangeEvent((NSurveyQuestion.AnswerPropertyRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswerPropertyRowChanging != null)
                {
                    this.AnswerPropertyRowChanging(this, new NSurveyQuestion.AnswerPropertyRowChangeEvent((NSurveyQuestion.AnswerPropertyRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswerPropertyRowDeleted != null)
                {
                    this.AnswerPropertyRowDeleted(this, new NSurveyQuestion.AnswerPropertyRowChangeEvent((NSurveyQuestion.AnswerPropertyRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswerPropertyRowDeleting != null)
                {
                    this.AnswerPropertyRowDeleting(this, new NSurveyQuestion.AnswerPropertyRowChangeEvent((NSurveyQuestion.AnswerPropertyRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswerPropertyRow(NSurveyQuestion.AnswerPropertyRow row)
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

            [Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            public NSurveyQuestion.AnswerPropertyRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.AnswerPropertyRow) base.Rows[index];
                }
            }

            internal DataColumn PropertiesColumn
            {
                get
                {
                    return this.columnProperties;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class AnswerPropertyRow : DataRow
        {
            private NSurveyQuestion.AnswerPropertyDataTable tableAnswerProperty;

            internal AnswerPropertyRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswerProperty = (NSurveyQuestion.AnswerPropertyDataTable) base.Table;
            }

            public bool IsPropertiesNull()
            {
                return base.IsNull(this.tableAnswerProperty.PropertiesColumn);
            }

            public void SetPropertiesNull()
            {
                base[this.tableAnswerProperty.PropertiesColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    return (int) base[this.tableAnswerProperty.AnswerIdColumn];
                }
                set
                {
                    base[this.tableAnswerProperty.AnswerIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.AnswerRow AnswerRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.AnswerRow) base.GetParentRow(base.Table.ParentRelations["AnswerAnswerProperty"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["AnswerAnswerProperty"]);
                }
            }

            public byte[] Properties
            {
                get
                {
                    byte[] buffer;
                    try
                    {
                        buffer = (byte[]) base[this.tableAnswerProperty.PropertiesColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return buffer;
                }
                set
                {
                    base[this.tableAnswerProperty.PropertiesColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class AnswerPropertyRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.AnswerPropertyRow eventRow;

            public AnswerPropertyRowChangeEvent(NSurveyQuestion.AnswerPropertyRow row, DataRowAction action)
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

            public NSurveyQuestion.AnswerPropertyRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswerPropertyRowChangeEventHandler(object sender, NSurveyQuestion.AnswerPropertyRowChangeEvent e);

        [DebuggerStepThrough]
        public partial class AnswerRow : DataRow
        {
            private NSurveyQuestion.AnswerDataTable tableAnswer;

            internal AnswerRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswer = (NSurveyQuestion.AnswerDataTable) base.Table;
            }

            public NSurveyQuestion.AnswerConnectionRow[] GetAnswerConnectionRowsByPublisherAnswerConnection()
            {
                return (NSurveyQuestion.AnswerConnectionRow[]) base.GetChildRows(base.Table.ChildRelations["PublisherAnswerConnection"]);
            }

            public NSurveyQuestion.AnswerConnectionRow[] GetAnswerConnectionRowsBySubscriberAnswerConnection()
            {
                return (NSurveyQuestion.AnswerConnectionRow[]) base.GetChildRows(base.Table.ChildRelations["SubscriberAnswerConnection"]);
            }

            public NSurveyQuestion.AnswerPropertyRow[] GetAnswerPropertyRows()
            {
                return (NSurveyQuestion.AnswerPropertyRow[]) base.GetChildRows(base.Table.ChildRelations["AnswerAnswerProperty"]);
            }

            public NSurveyQuestion.QuestionSectionGridAnswerRow[] GetQuestionSectionGridAnswerRows()
            {
                return (NSurveyQuestion.QuestionSectionGridAnswerRow[]) base.GetChildRows(base.Table.ChildRelations["AnswerQuestionSectionGridAnswer"]);
            }

            public bool IsAnswerPipeAliasNull()
            {
                return base.IsNull(this.tableAnswer.AnswerPipeAliasColumn);
            }

            public bool IsAnswerTextNull()
            {
                return base.IsNull(this.tableAnswer.AnswerTextColumn);
            }

            public bool IsAnswerTypeIdNull()
            {
                return base.IsNull(this.tableAnswer.AnswerTypeIdColumn);
            }

            public bool IsDefaultTextNull()
            {
                return base.IsNull(this.tableAnswer.DefaultTextColumn);
            }

            public bool IsDisplayOrderNull()
            {
                return base.IsNull(this.tableAnswer.DisplayOrderColumn);
            }

            public bool IsImageURLNull()
            {
                return base.IsNull(this.tableAnswer.ImageURLColumn);
            }

            public bool IsMandatoryNull()
            {
                return base.IsNull(this.tableAnswer.MandatoryColumn);
            }

            public bool IsQuestionIdNull()
            {
                return base.IsNull(this.tableAnswer.QuestionIdColumn);
            }

            public bool IsRatePartNull()
            {
                return base.IsNull(this.tableAnswer.RatePartColumn);
            }

            public bool IsRegularExpressionIdNull()
            {
                return base.IsNull(this.tableAnswer.RegularExpressionIdColumn);
            }

            public bool IsScorePointNull()
            {
                return base.IsNull(this.tableAnswer.ScorePointColumn);
            }

            public bool IsSelectedNull()
            {
                return base.IsNull(this.tableAnswer.SelectedColumn);
            }

            public void SetAnswerPipeAliasNull()
            {
                base[this.tableAnswer.AnswerPipeAliasColumn] = Convert.DBNull;
            }

            public void SetAnswerTextNull()
            {
                base[this.tableAnswer.AnswerTextColumn] = Convert.DBNull;
            }

            public void SetAnswerTypeIdNull()
            {
                base[this.tableAnswer.AnswerTypeIdColumn] = Convert.DBNull;
            }

            public void SetDefaultTextNull()
            {
                base[this.tableAnswer.DefaultTextColumn] = Convert.DBNull;
            }

            public void SetDisplayOrderNull()
            {
                base[this.tableAnswer.DisplayOrderColumn] = Convert.DBNull;
            }

            public void SetImageURLNull()
            {
                base[this.tableAnswer.ImageURLColumn] = Convert.DBNull;
            }

            public void SetMandatoryNull()
            {
                base[this.tableAnswer.MandatoryColumn] = Convert.DBNull;
            }

            public void SetQuestionIdNull()
            {
                base[this.tableAnswer.QuestionIdColumn] = Convert.DBNull;
            }

            public void SetRatePartNull()
            {
                base[this.tableAnswer.RatePartColumn] = Convert.DBNull;
            }

            public void SetRegularExpressionIdNull()
            {
                base[this.tableAnswer.RegularExpressionIdColumn] = Convert.DBNull;
            }

            public void SetScorePointNull()
            {
                base[this.tableAnswer.ScorePointColumn] = Convert.DBNull;
            }

            public void SetSelectedNull()
            {
                base[this.tableAnswer.SelectedColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    return (int) base[this.tableAnswer.AnswerIdColumn];
                }
                set
                {
                    base[this.tableAnswer.AnswerIdColumn] = value;
                }
            }

            public string AnswerPipeAlias
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswer.AnswerPipeAliasColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswer.AnswerPipeAliasColumn] = value;
                }
            }

            public string AnswerText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswer.AnswerTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswer.AnswerTextColumn] = value;
                }
            }

            public int AnswerTypeId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswer.AnswerTypeIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswer.AnswerTypeIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.AnswerTypeRow AnswerTypeRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.AnswerTypeRow) base.GetParentRow(base.Table.ParentRelations["AnswerTypeAnswer"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["AnswerTypeAnswer"]);
                }
            }

            public string DefaultText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswer.DefaultTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswer.DefaultTextColumn] = value;
                }
            }

            public int DisplayOrder
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswer.DisplayOrderColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswer.DisplayOrderColumn] = value;
                }
            }

            public string ImageURL
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswer.ImageURLColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswer.ImageURLColumn] = value;
                }
            }

            public bool Mandatory
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswer.MandatoryColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswer.MandatoryColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswer.QuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswer.QuestionIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.QuestionRow QuestionRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.QuestionRow) base.GetParentRow(base.Table.ParentRelations["QuestionAnswer"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["QuestionAnswer"]);
                }
            }

            public bool RatePart
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswer.RatePartColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswer.RatePartColumn] = value;
                }
            }

            public int RegularExpressionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswer.RegularExpressionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswer.RegularExpressionIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.RegularExpressionRow RegularExpressionRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.RegularExpressionRow) base.GetParentRow(base.Table.ParentRelations["RegularExpressionAnswer"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["RegularExpressionAnswer"]);
                }
            }

            public int ScorePoint
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswer.ScorePointColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswer.ScorePointColumn] = value;
                }
            }

            public bool Selected
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswer.SelectedColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswer.SelectedColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class AnswerRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.AnswerRow eventRow;

            public AnswerRowChangeEvent(NSurveyQuestion.AnswerRow row, DataRowAction action)
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

            public NSurveyQuestion.AnswerRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswerRowChangeEventHandler(object sender, NSurveyQuestion.AnswerRowChangeEvent e);

        [DebuggerStepThrough]
        public partial class AnswerTypeDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerTypeID;
            private DataColumn columnBuiltIn;
            private DataColumn columnDataSource;
            private DataColumn columnDescription;
            private DataColumn columnFieldHeight;
            private DataColumn columnFieldLength;
            private DataColumn columnFieldWidth;
            private DataColumn columnJavascriptCode;
            private DataColumn columnJavascriptErrorMessage;
            private DataColumn columnJavascriptFunctionName;
            private DataColumn columnPublicFieldResults;
            private DataColumn columnTypeAssembly;
            private DataColumn columnTypeMode;
            private DataColumn columnTypeNameSpace;
            private DataColumn columnXMLDataSource;

            public event NSurveyQuestion.AnswerTypeRowChangeEventHandler AnswerTypeRowChanged;

            public event NSurveyQuestion.AnswerTypeRowChangeEventHandler AnswerTypeRowChanging;

            public event NSurveyQuestion.AnswerTypeRowChangeEventHandler AnswerTypeRowDeleted;

            public event NSurveyQuestion.AnswerTypeRowChangeEventHandler AnswerTypeRowDeleting;

            internal AnswerTypeDataTable() : base("AnswerType")
            {
                this.InitClass();
            }

            internal AnswerTypeDataTable(DataTable table) : base(table.TableName)
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

            public void AddAnswerTypeRow(NSurveyQuestion.AnswerTypeRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.AnswerTypeRow AddAnswerTypeRow(int FieldWidth, int FieldHeight, int FieldLength, int TypeMode, bool PublicFieldResults, bool BuiltIn, string Description, string XMLDataSource, string JavascriptFunctionName, string JavascriptCode, string JavascriptErrorMessage, string TypeNameSpace, string TypeAssembly, string DataSource)
            {
                NSurveyQuestion.AnswerTypeRow row = (NSurveyQuestion.AnswerTypeRow) base.NewRow();
                object[] objArray = new object[15];
                objArray[1] = FieldWidth;
                objArray[2] = FieldHeight;
                objArray[3] = FieldLength;
                objArray[4] = TypeMode;
                objArray[5] = PublicFieldResults;
                objArray[6] = BuiltIn;
                objArray[7] = Description;
                objArray[8] = XMLDataSource;
                objArray[9] = JavascriptFunctionName;
                objArray[10] = JavascriptCode;
                objArray[11] = JavascriptErrorMessage;
                objArray[12] = TypeNameSpace;
                objArray[13] = TypeAssembly;
                objArray[14] = DataSource;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.AnswerTypeDataTable table = (NSurveyQuestion.AnswerTypeDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.AnswerTypeDataTable();
            }

            public NSurveyQuestion.AnswerTypeRow FindByAnswerTypeID(int AnswerTypeID)
            {
                return (NSurveyQuestion.AnswerTypeRow) base.Rows.Find(new object[] { AnswerTypeID });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.AnswerTypeRow);
            }

            private void InitClass()
            {
                this.columnAnswerTypeID = new DataColumn("AnswerTypeID", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnAnswerTypeID);
                this.columnFieldWidth = new DataColumn("FieldWidth", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnFieldWidth);
                this.columnFieldHeight = new DataColumn("FieldHeight", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnFieldHeight);
                this.columnFieldLength = new DataColumn("FieldLength", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnFieldLength);
                this.columnTypeMode = new DataColumn("TypeMode", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnTypeMode);
                this.columnPublicFieldResults = new DataColumn("PublicFieldResults", typeof(bool), null, MappingType.Attribute);
                base.Columns.Add(this.columnPublicFieldResults);
                this.columnBuiltIn = new DataColumn("BuiltIn", typeof(bool), null, MappingType.Attribute);
                base.Columns.Add(this.columnBuiltIn);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnXMLDataSource = new DataColumn("XMLDataSource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnXMLDataSource);
                this.columnJavascriptFunctionName = new DataColumn("JavascriptFunctionName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptFunctionName);
                this.columnJavascriptCode = new DataColumn("JavascriptCode", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptCode);
                this.columnJavascriptErrorMessage = new DataColumn("JavascriptErrorMessage", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnJavascriptErrorMessage);
                this.columnTypeNameSpace = new DataColumn("TypeNameSpace", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTypeNameSpace);
                this.columnTypeAssembly = new DataColumn("TypeAssembly", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTypeAssembly);
                this.columnDataSource = new DataColumn("DataSource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDataSource);
                base.Constraints.Add(new UniqueConstraint("NSurveyQuestionKey3", new DataColumn[] { this.columnAnswerTypeID }, true));
                this.columnAnswerTypeID.AutoIncrement = true;
                this.columnAnswerTypeID.AllowDBNull = false;
                this.columnAnswerTypeID.Unique = true;
                this.columnAnswerTypeID.Namespace = "";
                this.columnFieldWidth.Namespace = "";
                this.columnFieldHeight.Namespace = "";
                this.columnFieldLength.Namespace = "";
                this.columnTypeMode.Namespace = "";
                this.columnPublicFieldResults.Namespace = "";
                this.columnBuiltIn.Namespace = "";
                this.columnDescription.AllowDBNull = false;
                this.columnTypeNameSpace.AllowDBNull = false;
                this.columnTypeAssembly.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnAnswerTypeID = base.Columns["AnswerTypeID"];
                this.columnFieldWidth = base.Columns["FieldWidth"];
                this.columnFieldHeight = base.Columns["FieldHeight"];
                this.columnFieldLength = base.Columns["FieldLength"];
                this.columnTypeMode = base.Columns["TypeMode"];
                this.columnPublicFieldResults = base.Columns["PublicFieldResults"];
                this.columnBuiltIn = base.Columns["BuiltIn"];
                this.columnDescription = base.Columns["Description"];
                this.columnXMLDataSource = base.Columns["XMLDataSource"];
                this.columnJavascriptFunctionName = base.Columns["JavascriptFunctionName"];
                this.columnJavascriptCode = base.Columns["JavascriptCode"];
                this.columnJavascriptErrorMessage = base.Columns["JavascriptErrorMessage"];
                this.columnTypeNameSpace = base.Columns["TypeNameSpace"];
                this.columnTypeAssembly = base.Columns["TypeAssembly"];
                this.columnDataSource = base.Columns["DataSource"];
            }

            public NSurveyQuestion.AnswerTypeRow NewAnswerTypeRow()
            {
                return (NSurveyQuestion.AnswerTypeRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.AnswerTypeRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.AnswerTypeRowChanged != null)
                {
                    this.AnswerTypeRowChanged(this, new NSurveyQuestion.AnswerTypeRowChangeEvent((NSurveyQuestion.AnswerTypeRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.AnswerTypeRowChanging != null)
                {
                    this.AnswerTypeRowChanging(this, new NSurveyQuestion.AnswerTypeRowChangeEvent((NSurveyQuestion.AnswerTypeRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.AnswerTypeRowDeleted != null)
                {
                    this.AnswerTypeRowDeleted(this, new NSurveyQuestion.AnswerTypeRowChangeEvent((NSurveyQuestion.AnswerTypeRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.AnswerTypeRowDeleting != null)
                {
                    this.AnswerTypeRowDeleting(this, new NSurveyQuestion.AnswerTypeRowChangeEvent((NSurveyQuestion.AnswerTypeRow) e.Row, e.Action));
                }
            }

            public void RemoveAnswerTypeRow(NSurveyQuestion.AnswerTypeRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AnswerTypeIDColumn
            {
                get
                {
                    return this.columnAnswerTypeID;
                }
            }

            internal DataColumn BuiltInColumn
            {
                get
                {
                    return this.columnBuiltIn;
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

            internal DataColumn DescriptionColumn
            {
                get
                {
                    return this.columnDescription;
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

            public NSurveyQuestion.AnswerTypeRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.AnswerTypeRow) base.Rows[index];
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

            internal DataColumn PublicFieldResultsColumn
            {
                get
                {
                    return this.columnPublicFieldResults;
                }
            }

            internal DataColumn TypeAssemblyColumn
            {
                get
                {
                    return this.columnTypeAssembly;
                }
            }

            internal DataColumn TypeModeColumn
            {
                get
                {
                    return this.columnTypeMode;
                }
            }

            internal DataColumn TypeNameSpaceColumn
            {
                get
                {
                    return this.columnTypeNameSpace;
                }
            }

            internal DataColumn XMLDataSourceColumn
            {
                get
                {
                    return this.columnXMLDataSource;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class AnswerTypeRow : DataRow
        {
            private NSurveyQuestion.AnswerTypeDataTable tableAnswerType;

            internal AnswerTypeRow(DataRowBuilder rb) : base(rb)
            {
                this.tableAnswerType = (NSurveyQuestion.AnswerTypeDataTable) base.Table;
            }

            public NSurveyQuestion.AnswerRow[] GetAnswerRows()
            {
                return (NSurveyQuestion.AnswerRow[]) base.GetChildRows(base.Table.ChildRelations["AnswerTypeAnswer"]);
            }

            public bool IsBuiltInNull()
            {
                return base.IsNull(this.tableAnswerType.BuiltInColumn);
            }

            public bool IsDataSourceNull()
            {
                return base.IsNull(this.tableAnswerType.DataSourceColumn);
            }

            public bool IsFieldHeightNull()
            {
                return base.IsNull(this.tableAnswerType.FieldHeightColumn);
            }

            public bool IsFieldLengthNull()
            {
                return base.IsNull(this.tableAnswerType.FieldLengthColumn);
            }

            public bool IsFieldWidthNull()
            {
                return base.IsNull(this.tableAnswerType.FieldWidthColumn);
            }

            public bool IsJavascriptCodeNull()
            {
                return base.IsNull(this.tableAnswerType.JavascriptCodeColumn);
            }

            public bool IsJavascriptErrorMessageNull()
            {
                return base.IsNull(this.tableAnswerType.JavascriptErrorMessageColumn);
            }

            public bool IsJavascriptFunctionNameNull()
            {
                return base.IsNull(this.tableAnswerType.JavascriptFunctionNameColumn);
            }

            public bool IsPublicFieldResultsNull()
            {
                return base.IsNull(this.tableAnswerType.PublicFieldResultsColumn);
            }

            public bool IsTypeModeNull()
            {
                return base.IsNull(this.tableAnswerType.TypeModeColumn);
            }

            public bool IsXMLDataSourceNull()
            {
                return base.IsNull(this.tableAnswerType.XMLDataSourceColumn);
            }

            public void SetBuiltInNull()
            {
                base[this.tableAnswerType.BuiltInColumn] = Convert.DBNull;
            }

            public void SetDataSourceNull()
            {
                base[this.tableAnswerType.DataSourceColumn] = Convert.DBNull;
            }

            public void SetFieldHeightNull()
            {
                base[this.tableAnswerType.FieldHeightColumn] = Convert.DBNull;
            }

            public void SetFieldLengthNull()
            {
                base[this.tableAnswerType.FieldLengthColumn] = Convert.DBNull;
            }

            public void SetFieldWidthNull()
            {
                base[this.tableAnswerType.FieldWidthColumn] = Convert.DBNull;
            }

            public void SetJavascriptCodeNull()
            {
                base[this.tableAnswerType.JavascriptCodeColumn] = Convert.DBNull;
            }

            public void SetJavascriptErrorMessageNull()
            {
                base[this.tableAnswerType.JavascriptErrorMessageColumn] = Convert.DBNull;
            }

            public void SetJavascriptFunctionNameNull()
            {
                base[this.tableAnswerType.JavascriptFunctionNameColumn] = Convert.DBNull;
            }

            public void SetPublicFieldResultsNull()
            {
                base[this.tableAnswerType.PublicFieldResultsColumn] = Convert.DBNull;
            }

            public void SetTypeModeNull()
            {
                base[this.tableAnswerType.TypeModeColumn] = Convert.DBNull;
            }

            public void SetXMLDataSourceNull()
            {
                base[this.tableAnswerType.XMLDataSourceColumn] = Convert.DBNull;
            }

            public int AnswerTypeID
            {
                get
                {
                    return (int) base[this.tableAnswerType.AnswerTypeIDColumn];
                }
                set
                {
                    base[this.tableAnswerType.AnswerTypeIDColumn] = value;
                }
            }

            public bool BuiltIn
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswerType.BuiltInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswerType.BuiltInColumn] = value;
                }
            }

            public string DataSource
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswerType.DataSourceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswerType.DataSourceColumn] = value;
                }
            }

            public string Description
            {
                get
                {
                    return (string) base[this.tableAnswerType.DescriptionColumn];
                }
                set
                {
                    base[this.tableAnswerType.DescriptionColumn] = value;
                }
            }

            public int FieldHeight
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswerType.FieldHeightColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswerType.FieldHeightColumn] = value;
                }
            }

            public int FieldLength
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswerType.FieldLengthColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswerType.FieldLengthColumn] = value;
                }
            }

            public int FieldWidth
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswerType.FieldWidthColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswerType.FieldWidthColumn] = value;
                }
            }

            public string JavascriptCode
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswerType.JavascriptCodeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswerType.JavascriptCodeColumn] = value;
                }
            }

            public string JavascriptErrorMessage
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswerType.JavascriptErrorMessageColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswerType.JavascriptErrorMessageColumn] = value;
                }
            }

            public string JavascriptFunctionName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswerType.JavascriptFunctionNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswerType.JavascriptFunctionNameColumn] = value;
                }
            }

            public bool PublicFieldResults
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableAnswerType.PublicFieldResultsColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableAnswerType.PublicFieldResultsColumn] = value;
                }
            }

            public string TypeAssembly
            {
                get
                {
                    return (string) base[this.tableAnswerType.TypeAssemblyColumn];
                }
                set
                {
                    base[this.tableAnswerType.TypeAssemblyColumn] = value;
                }
            }

            public int TypeMode
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableAnswerType.TypeModeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableAnswerType.TypeModeColumn] = value;
                }
            }

            public string TypeNameSpace
            {
                get
                {
                    return (string) base[this.tableAnswerType.TypeNameSpaceColumn];
                }
                set
                {
                    base[this.tableAnswerType.TypeNameSpaceColumn] = value;
                }
            }

            public string XMLDataSource
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableAnswerType.XMLDataSourceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableAnswerType.XMLDataSourceColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class AnswerTypeRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.AnswerTypeRow eventRow;

            public AnswerTypeRowChangeEvent(NSurveyQuestion.AnswerTypeRow row, DataRowAction action)
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

            public NSurveyQuestion.AnswerTypeRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void AnswerTypeRowChangeEventHandler(object sender, NSurveyQuestion.AnswerTypeRowChangeEvent e);

        [DebuggerStepThrough]
        public partial class ChildQuestionDataTable : DataTable, IEnumerable
        {
            private DataColumn columnParentQuestionId;
            private DataColumn columnQuestionText;

            public event NSurveyQuestion.ChildQuestionRowChangeEventHandler ChildQuestionRowChanged;

            public event NSurveyQuestion.ChildQuestionRowChangeEventHandler ChildQuestionRowChanging;

            public event NSurveyQuestion.ChildQuestionRowChangeEventHandler ChildQuestionRowDeleted;

            public event NSurveyQuestion.ChildQuestionRowChangeEventHandler ChildQuestionRowDeleting;

            internal ChildQuestionDataTable() : base("ChildQuestion")
            {
                this.InitClass();
            }

            internal ChildQuestionDataTable(DataTable table) : base(table.TableName)
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

            public void AddChildQuestionRow(NSurveyQuestion.ChildQuestionRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.ChildQuestionRow AddChildQuestionRow(NSurveyQuestion.QuestionRow parentQuestionRowByQuestionChildQuestion, string QuestionText)
            {
                NSurveyQuestion.ChildQuestionRow row = (NSurveyQuestion.ChildQuestionRow) base.NewRow();
                row.ItemArray = new object[] { parentQuestionRowByQuestionChildQuestion[0], QuestionText };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.ChildQuestionDataTable table = (NSurveyQuestion.ChildQuestionDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.ChildQuestionDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.ChildQuestionRow);
            }

            private void InitClass()
            {
                this.columnParentQuestionId = new DataColumn("ParentQuestionId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnParentQuestionId);
                this.columnQuestionText = new DataColumn("QuestionText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionText);
                this.columnParentQuestionId.Namespace = "";
            }

            internal void InitVars()
            {
                this.columnParentQuestionId = base.Columns["ParentQuestionId"];
                this.columnQuestionText = base.Columns["QuestionText"];
            }

            public NSurveyQuestion.ChildQuestionRow NewChildQuestionRow()
            {
                return (NSurveyQuestion.ChildQuestionRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.ChildQuestionRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.ChildQuestionRowChanged != null)
                {
                    this.ChildQuestionRowChanged(this, new NSurveyQuestion.ChildQuestionRowChangeEvent((NSurveyQuestion.ChildQuestionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.ChildQuestionRowChanging != null)
                {
                    this.ChildQuestionRowChanging(this, new NSurveyQuestion.ChildQuestionRowChangeEvent((NSurveyQuestion.ChildQuestionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.ChildQuestionRowDeleted != null)
                {
                    this.ChildQuestionRowDeleted(this, new NSurveyQuestion.ChildQuestionRowChangeEvent((NSurveyQuestion.ChildQuestionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.ChildQuestionRowDeleting != null)
                {
                    this.ChildQuestionRowDeleting(this, new NSurveyQuestion.ChildQuestionRowChangeEvent((NSurveyQuestion.ChildQuestionRow) e.Row, e.Action));
                }
            }

            public void RemoveChildQuestionRow(NSurveyQuestion.ChildQuestionRow row)
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

            public NSurveyQuestion.ChildQuestionRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.ChildQuestionRow) base.Rows[index];
                }
            }

            internal DataColumn ParentQuestionIdColumn
            {
                get
                {
                    return this.columnParentQuestionId;
                }
            }

            internal DataColumn QuestionTextColumn
            {
                get
                {
                    return this.columnQuestionText;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class ChildQuestionRow : DataRow
        {
            private NSurveyQuestion.ChildQuestionDataTable tableChildQuestion;

            internal ChildQuestionRow(DataRowBuilder rb) : base(rb)
            {
                this.tableChildQuestion = (NSurveyQuestion.ChildQuestionDataTable) base.Table;
            }

            public bool IsParentQuestionIdNull()
            {
                return base.IsNull(this.tableChildQuestion.ParentQuestionIdColumn);
            }

            public bool IsQuestionTextNull()
            {
                return base.IsNull(this.tableChildQuestion.QuestionTextColumn);
            }

            public void SetParentQuestionIdNull()
            {
                base[this.tableChildQuestion.ParentQuestionIdColumn] = Convert.DBNull;
            }

            public void SetQuestionTextNull()
            {
                base[this.tableChildQuestion.QuestionTextColumn] = Convert.DBNull;
            }

            public int ParentQuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableChildQuestion.ParentQuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableChildQuestion.ParentQuestionIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.QuestionRow QuestionRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.QuestionRow) base.GetParentRow(base.Table.ParentRelations["QuestionChildQuestion"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["QuestionChildQuestion"]);
                }
            }

            public string QuestionText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableChildQuestion.QuestionTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableChildQuestion.QuestionTextColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class ChildQuestionRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.ChildQuestionRow eventRow;

            public ChildQuestionRowChangeEvent(NSurveyQuestion.ChildQuestionRow row, DataRowAction action)
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

            public NSurveyQuestion.ChildQuestionRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void ChildQuestionRowChangeEventHandler(object sender, NSurveyQuestion.ChildQuestionRowChangeEvent e);

        [DebuggerStepThrough]
        public partial class QuestionDataTable : DataTable, IEnumerable
        {
            private DataColumn columnColumnsNumber;
            private DataColumn columnDisplayOrder;
            private DataColumn columnLayoutModeId;
            private DataColumn columnLibraryId;
            private DataColumn columnMaxSelectionAllowed;
            private DataColumn columnMinSelectionRequired;
            private DataColumn columnPageNumber;
            private DataColumn columnQuestionId;
            private DataColumn columnQuestionPipeAlias;
            private DataColumn columnQuestionText;
            private DataColumn columnRandomizeAnswers;
            private DataColumn columnRatingEnabled;
            private DataColumn columnSelectionModeId;
            private DataColumn columnSurveyId;

            public event NSurveyQuestion.QuestionRowChangeEventHandler QuestionRowChanged;

            public event NSurveyQuestion.QuestionRowChangeEventHandler QuestionRowChanging;

            public event NSurveyQuestion.QuestionRowChangeEventHandler QuestionRowDeleted;

            public event NSurveyQuestion.QuestionRowChangeEventHandler QuestionRowDeleting;

            internal QuestionDataTable() : base("Question")
            {
                this.InitClass();
            }

            internal QuestionDataTable(DataTable table) : base(table.TableName)
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

            public void AddQuestionRow(NSurveyQuestion.QuestionRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.QuestionRow AddQuestionRow(byte LayoutModeId, byte SelectionModeId, int ColumnsNumber, int MinSelectionRequired, int MaxSelectionAllowed, bool RatingEnabled, bool RandomizeAnswers, int SurveyId, int PageNumber, int LibraryId, int DisplayOrder, string QuestionText, string QuestionPipeAlias)
            {
                NSurveyQuestion.QuestionRow row = (NSurveyQuestion.QuestionRow) base.NewRow();
                object[] objArray = new object[14];
                objArray[1] = LayoutModeId;
                objArray[2] = SelectionModeId;
                objArray[3] = ColumnsNumber;
                objArray[4] = MinSelectionRequired;
                objArray[5] = MaxSelectionAllowed;
                objArray[6] = RatingEnabled;
                objArray[7] = RandomizeAnswers;
                objArray[8] = SurveyId;
                objArray[9] = PageNumber;
                objArray[10] = LibraryId;
                objArray[11] = DisplayOrder;
                objArray[12] = QuestionText;
                objArray[13] = QuestionPipeAlias;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.QuestionDataTable table = (NSurveyQuestion.QuestionDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.QuestionDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.QuestionRow);
            }

            private void InitClass()
            {
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnQuestionId);
                this.columnLayoutModeId = new DataColumn("LayoutModeId", typeof(byte), null, MappingType.Attribute);
                base.Columns.Add(this.columnLayoutModeId);
                this.columnSelectionModeId = new DataColumn("SelectionModeId", typeof(byte), null, MappingType.Attribute);
                base.Columns.Add(this.columnSelectionModeId);
                this.columnColumnsNumber = new DataColumn("ColumnsNumber", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnColumnsNumber);
                this.columnMinSelectionRequired = new DataColumn("MinSelectionRequired", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnMinSelectionRequired);
                this.columnMaxSelectionAllowed = new DataColumn("MaxSelectionAllowed", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnMaxSelectionAllowed);
                this.columnRatingEnabled = new DataColumn("RatingEnabled", typeof(bool), null, MappingType.Attribute);
                base.Columns.Add(this.columnRatingEnabled);
                this.columnRandomizeAnswers = new DataColumn("RandomizeAnswers", typeof(bool), null, MappingType.Attribute);
                base.Columns.Add(this.columnRandomizeAnswers);
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnSurveyId);
                this.columnPageNumber = new DataColumn("PageNumber", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnPageNumber);
                this.columnLibraryId = new DataColumn("LibraryId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnLibraryId);
                this.columnDisplayOrder = new DataColumn("DisplayOrder", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnDisplayOrder);
                this.columnQuestionText = new DataColumn("QuestionText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionText);
                this.columnQuestionPipeAlias = new DataColumn("QuestionPipeAlias", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionPipeAlias);
                base.Constraints.Add(new UniqueConstraint("NSurveyQuestionKey4", new DataColumn[] { this.columnQuestionId }, false));
                this.columnQuestionId.AutoIncrement = true;
                this.columnQuestionId.AllowDBNull = false;
                this.columnQuestionId.Unique = true;
                this.columnQuestionId.Namespace = "";
                this.columnLayoutModeId.Namespace = "";
                this.columnSelectionModeId.Namespace = "";
                this.columnColumnsNumber.Namespace = "";
                this.columnMinSelectionRequired.Namespace = "";
                this.columnMaxSelectionAllowed.Namespace = "";
                this.columnRatingEnabled.Namespace = "";
                this.columnRandomizeAnswers.Namespace = "";
                this.columnSurveyId.Namespace = "";
                this.columnPageNumber.Namespace = "";
                this.columnLibraryId.Namespace = "";
                this.columnDisplayOrder.Namespace = "";
            }

            internal void InitVars()
            {
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnLayoutModeId = base.Columns["LayoutModeId"];
                this.columnSelectionModeId = base.Columns["SelectionModeId"];
                this.columnColumnsNumber = base.Columns["ColumnsNumber"];
                this.columnMinSelectionRequired = base.Columns["MinSelectionRequired"];
                this.columnMaxSelectionAllowed = base.Columns["MaxSelectionAllowed"];
                this.columnRatingEnabled = base.Columns["RatingEnabled"];
                this.columnRandomizeAnswers = base.Columns["RandomizeAnswers"];
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnPageNumber = base.Columns["PageNumber"];
                this.columnLibraryId = base.Columns["LibraryId"];
                this.columnDisplayOrder = base.Columns["DisplayOrder"];
                this.columnQuestionText = base.Columns["QuestionText"];
                this.columnQuestionPipeAlias = base.Columns["QuestionPipeAlias"];
            }

            public NSurveyQuestion.QuestionRow NewQuestionRow()
            {
                return (NSurveyQuestion.QuestionRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.QuestionRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.QuestionRowChanged != null)
                {
                    this.QuestionRowChanged(this, new NSurveyQuestion.QuestionRowChangeEvent((NSurveyQuestion.QuestionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.QuestionRowChanging != null)
                {
                    this.QuestionRowChanging(this, new NSurveyQuestion.QuestionRowChangeEvent((NSurveyQuestion.QuestionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.QuestionRowDeleted != null)
                {
                    this.QuestionRowDeleted(this, new NSurveyQuestion.QuestionRowChangeEvent((NSurveyQuestion.QuestionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.QuestionRowDeleting != null)
                {
                    this.QuestionRowDeleting(this, new NSurveyQuestion.QuestionRowChangeEvent((NSurveyQuestion.QuestionRow) e.Row, e.Action));
                }
            }

            public void RemoveQuestionRow(NSurveyQuestion.QuestionRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn ColumnsNumberColumn
            {
                get
                {
                    return this.columnColumnsNumber;
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

            internal DataColumn DisplayOrderColumn
            {
                get
                {
                    return this.columnDisplayOrder;
                }
            }

            public NSurveyQuestion.QuestionRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.QuestionRow) base.Rows[index];
                }
            }

            internal DataColumn LayoutModeIdColumn
            {
                get
                {
                    return this.columnLayoutModeId;
                }
            }

            internal DataColumn LibraryIdColumn
            {
                get
                {
                    return this.columnLibraryId;
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
        public partial class QuestionRow : DataRow
        {
            private NSurveyQuestion.QuestionDataTable tableQuestion;

            internal QuestionRow(DataRowBuilder rb) : base(rb)
            {
                this.tableQuestion = (NSurveyQuestion.QuestionDataTable) base.Table;
            }

            public NSurveyQuestion.AnswerConnectionRow[] GetAnswerConnectionRows()
            {
                return (NSurveyQuestion.AnswerConnectionRow[]) base.GetChildRows(base.Table.ChildRelations["QuestionAnswerConnection"]);
            }

            public NSurveyQuestion.AnswerRow[] GetAnswerRows()
            {
                return (NSurveyQuestion.AnswerRow[]) base.GetChildRows(base.Table.ChildRelations["QuestionAnswer"]);
            }

            public NSurveyQuestion.ChildQuestionRow[] GetChildQuestionRows()
            {
                return (NSurveyQuestion.ChildQuestionRow[]) base.GetChildRows(base.Table.ChildRelations["QuestionChildQuestion"]);
            }

            public NSurveyQuestion.QuestionSectionOptionRow[] GetQuestionSectionOptionRows()
            {
                return (NSurveyQuestion.QuestionSectionOptionRow[]) base.GetChildRows(base.Table.ChildRelations["QuestionQuestionSectionOption"]);
            }

            public bool IsColumnsNumberNull()
            {
                return base.IsNull(this.tableQuestion.ColumnsNumberColumn);
            }

            public bool IsDisplayOrderNull()
            {
                return base.IsNull(this.tableQuestion.DisplayOrderColumn);
            }

            public bool IsLayoutModeIdNull()
            {
                return base.IsNull(this.tableQuestion.LayoutModeIdColumn);
            }

            public bool IsLibraryIdNull()
            {
                return base.IsNull(this.tableQuestion.LibraryIdColumn);
            }

            public bool IsMaxSelectionAllowedNull()
            {
                return base.IsNull(this.tableQuestion.MaxSelectionAllowedColumn);
            }

            public bool IsMinSelectionRequiredNull()
            {
                return base.IsNull(this.tableQuestion.MinSelectionRequiredColumn);
            }

            public bool IsPageNumberNull()
            {
                return base.IsNull(this.tableQuestion.PageNumberColumn);
            }

            public bool IsQuestionPipeAliasNull()
            {
                return base.IsNull(this.tableQuestion.QuestionPipeAliasColumn);
            }

            public bool IsQuestionTextNull()
            {
                return base.IsNull(this.tableQuestion.QuestionTextColumn);
            }

            public bool IsRandomizeAnswersNull()
            {
                return base.IsNull(this.tableQuestion.RandomizeAnswersColumn);
            }

            public bool IsRatingEnabledNull()
            {
                return base.IsNull(this.tableQuestion.RatingEnabledColumn);
            }

            public bool IsSelectionModeIdNull()
            {
                return base.IsNull(this.tableQuestion.SelectionModeIdColumn);
            }

            public bool IsSurveyIdNull()
            {
                return base.IsNull(this.tableQuestion.SurveyIdColumn);
            }

            public void SetColumnsNumberNull()
            {
                base[this.tableQuestion.ColumnsNumberColumn] = Convert.DBNull;
            }

            public void SetDisplayOrderNull()
            {
                base[this.tableQuestion.DisplayOrderColumn] = Convert.DBNull;
            }

            public void SetLayoutModeIdNull()
            {
                base[this.tableQuestion.LayoutModeIdColumn] = Convert.DBNull;
            }

            public void SetLibraryIdNull()
            {
                base[this.tableQuestion.LibraryIdColumn] = Convert.DBNull;
            }

            public void SetMaxSelectionAllowedNull()
            {
                base[this.tableQuestion.MaxSelectionAllowedColumn] = Convert.DBNull;
            }

            public void SetMinSelectionRequiredNull()
            {
                base[this.tableQuestion.MinSelectionRequiredColumn] = Convert.DBNull;
            }

            public void SetPageNumberNull()
            {
                base[this.tableQuestion.PageNumberColumn] = Convert.DBNull;
            }

            public void SetQuestionPipeAliasNull()
            {
                base[this.tableQuestion.QuestionPipeAliasColumn] = Convert.DBNull;
            }

            public void SetQuestionTextNull()
            {
                base[this.tableQuestion.QuestionTextColumn] = Convert.DBNull;
            }

            public void SetRandomizeAnswersNull()
            {
                base[this.tableQuestion.RandomizeAnswersColumn] = Convert.DBNull;
            }

            public void SetRatingEnabledNull()
            {
                base[this.tableQuestion.RatingEnabledColumn] = Convert.DBNull;
            }

            public void SetSelectionModeIdNull()
            {
                base[this.tableQuestion.SelectionModeIdColumn] = Convert.DBNull;
            }

            public void SetSurveyIdNull()
            {
                base[this.tableQuestion.SurveyIdColumn] = Convert.DBNull;
            }

            public int ColumnsNumber
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestion.ColumnsNumberColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.ColumnsNumberColumn] = value;
                }
            }

            public int DisplayOrder
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestion.DisplayOrderColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.DisplayOrderColumn] = value;
                }
            }

            public byte LayoutModeId
            {
                get
                {
                    byte num;
                    try
                    {
                        num = (byte) base[this.tableQuestion.LayoutModeIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.LayoutModeIdColumn] = value;
                }
            }

            public int LibraryId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestion.LibraryIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.LibraryIdColumn] = value;
                }
            }

            public int MaxSelectionAllowed
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestion.MaxSelectionAllowedColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.MaxSelectionAllowedColumn] = value;
                }
            }

            public int MinSelectionRequired
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestion.MinSelectionRequiredColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.MinSelectionRequiredColumn] = value;
                }
            }

            public int PageNumber
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestion.PageNumberColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.PageNumberColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    return (int) base[this.tableQuestion.QuestionIdColumn];
                }
                set
                {
                    base[this.tableQuestion.QuestionIdColumn] = value;
                }
            }

            public string QuestionPipeAlias
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableQuestion.QuestionPipeAliasColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableQuestion.QuestionPipeAliasColumn] = value;
                }
            }

            public string QuestionText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableQuestion.QuestionTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableQuestion.QuestionTextColumn] = value;
                }
            }

            public bool RandomizeAnswers
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableQuestion.RandomizeAnswersColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableQuestion.RandomizeAnswersColumn] = value;
                }
            }

            public bool RatingEnabled
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableQuestion.RatingEnabledColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableQuestion.RatingEnabledColumn] = value;
                }
            }

            public byte SelectionModeId
            {
                get
                {
                    byte num;
                    try
                    {
                        num = (byte) base[this.tableQuestion.SelectionModeIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.SelectionModeIdColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestion.SurveyIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestion.SurveyIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.QuestionRow eventRow;

            public QuestionRowChangeEvent(NSurveyQuestion.QuestionRow row, DataRowAction action)
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

            public NSurveyQuestion.QuestionRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void QuestionRowChangeEventHandler(object sender, NSurveyQuestion.QuestionRowChangeEvent e);

        [DebuggerStepThrough]
        public partial class QuestionSectionGridAnswerDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnQuestionId;

            public event NSurveyQuestion.QuestionSectionGridAnswerRowChangeEventHandler QuestionSectionGridAnswerRowChanged;

            public event NSurveyQuestion.QuestionSectionGridAnswerRowChangeEventHandler QuestionSectionGridAnswerRowChanging;

            public event NSurveyQuestion.QuestionSectionGridAnswerRowChangeEventHandler QuestionSectionGridAnswerRowDeleted;

            public event NSurveyQuestion.QuestionSectionGridAnswerRowChangeEventHandler QuestionSectionGridAnswerRowDeleting;

            internal QuestionSectionGridAnswerDataTable() : base("QuestionSectionGridAnswer")
            {
                this.InitClass();
            }

            internal QuestionSectionGridAnswerDataTable(DataTable table) : base(table.TableName)
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

            public void AddQuestionSectionGridAnswerRow(NSurveyQuestion.QuestionSectionGridAnswerRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.QuestionSectionGridAnswerRow AddQuestionSectionGridAnswerRow(NSurveyQuestion.QuestionSectionOptionRow parentQuestionSectionOptionRowByQuestionSectionOptionQuestionSectionGridAnswer, NSurveyQuestion.AnswerRow parentAnswerRowByAnswerQuestionSectionGridAnswer)
            {
                NSurveyQuestion.QuestionSectionGridAnswerRow row = (NSurveyQuestion.QuestionSectionGridAnswerRow) base.NewRow();
                row.ItemArray = new object[] { parentQuestionSectionOptionRowByQuestionSectionOptionQuestionSectionGridAnswer[0], parentAnswerRowByAnswerQuestionSectionGridAnswer[6] };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.QuestionSectionGridAnswerDataTable table = (NSurveyQuestion.QuestionSectionGridAnswerDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.QuestionSectionGridAnswerDataTable();
            }

            public NSurveyQuestion.QuestionSectionGridAnswerRow FindByQuestionIdAnswerId(int QuestionId, int AnswerId)
            {
                return (NSurveyQuestion.QuestionSectionGridAnswerRow) base.Rows.Find(new object[] { QuestionId, AnswerId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.QuestionSectionGridAnswerRow);
            }

            private void InitClass()
            {
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnQuestionId);
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnAnswerId);
                base.Constraints.Add(new UniqueConstraint("NSurveyQuestionKey9", new DataColumn[] { this.columnQuestionId, this.columnAnswerId }, true));
                this.columnQuestionId.AllowDBNull = false;
                this.columnQuestionId.Namespace = "";
                this.columnAnswerId.AllowDBNull = false;
                this.columnAnswerId.Namespace = "";
            }

            internal void InitVars()
            {
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnAnswerId = base.Columns["AnswerId"];
            }

            public NSurveyQuestion.QuestionSectionGridAnswerRow NewQuestionSectionGridAnswerRow()
            {
                return (NSurveyQuestion.QuestionSectionGridAnswerRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.QuestionSectionGridAnswerRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.QuestionSectionGridAnswerRowChanged != null)
                {
                    this.QuestionSectionGridAnswerRowChanged(this, new NSurveyQuestion.QuestionSectionGridAnswerRowChangeEvent((NSurveyQuestion.QuestionSectionGridAnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.QuestionSectionGridAnswerRowChanging != null)
                {
                    this.QuestionSectionGridAnswerRowChanging(this, new NSurveyQuestion.QuestionSectionGridAnswerRowChangeEvent((NSurveyQuestion.QuestionSectionGridAnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.QuestionSectionGridAnswerRowDeleted != null)
                {
                    this.QuestionSectionGridAnswerRowDeleted(this, new NSurveyQuestion.QuestionSectionGridAnswerRowChangeEvent((NSurveyQuestion.QuestionSectionGridAnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.QuestionSectionGridAnswerRowDeleting != null)
                {
                    this.QuestionSectionGridAnswerRowDeleting(this, new NSurveyQuestion.QuestionSectionGridAnswerRowChangeEvent((NSurveyQuestion.QuestionSectionGridAnswerRow) e.Row, e.Action));
                }
            }

            public void RemoveQuestionSectionGridAnswerRow(NSurveyQuestion.QuestionSectionGridAnswerRow row)
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

            [Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            public NSurveyQuestion.QuestionSectionGridAnswerRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.QuestionSectionGridAnswerRow) base.Rows[index];
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
        public partial class QuestionSectionGridAnswerRow : DataRow
        {
            private NSurveyQuestion.QuestionSectionGridAnswerDataTable tableQuestionSectionGridAnswer;

            internal QuestionSectionGridAnswerRow(DataRowBuilder rb) : base(rb)
            {
                this.tableQuestionSectionGridAnswer = (NSurveyQuestion.QuestionSectionGridAnswerDataTable) base.Table;
            }

            public int AnswerId
            {
                get
                {
                    return (int) base[this.tableQuestionSectionGridAnswer.AnswerIdColumn];
                }
                set
                {
                    base[this.tableQuestionSectionGridAnswer.AnswerIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.AnswerRow AnswerRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.AnswerRow) base.GetParentRow(base.Table.ParentRelations["AnswerQuestionSectionGridAnswer"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["AnswerQuestionSectionGridAnswer"]);
                }
            }

            public int QuestionId
            {
                get
                {
                    return (int) base[this.tableQuestionSectionGridAnswer.QuestionIdColumn];
                }
                set
                {
                    base[this.tableQuestionSectionGridAnswer.QuestionIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.QuestionSectionOptionRow QuestionSectionOptionRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.QuestionSectionOptionRow) base.GetParentRow(base.Table.ParentRelations["QuestionSectionOptionQuestionSectionGridAnswer"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["QuestionSectionOptionQuestionSectionGridAnswer"]);
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionSectionGridAnswerRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.QuestionSectionGridAnswerRow eventRow;

            public QuestionSectionGridAnswerRowChangeEvent(NSurveyQuestion.QuestionSectionGridAnswerRow row, DataRowAction action)
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

            public NSurveyQuestion.QuestionSectionGridAnswerRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void QuestionSectionGridAnswerRowChangeEventHandler(object sender, NSurveyQuestion.QuestionSectionGridAnswerRowChangeEvent e);

        [DebuggerStepThrough]
        public partial class QuestionSectionOptionDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAddSectionLinkText;
            private DataColumn columnDeleteSectionLinkText;
            private DataColumn columnEditSectionLinkText;
            private DataColumn columnMaxSections;
            private DataColumn columnQuestionId;
            private DataColumn columnRepeatableSectionModeId;
            private DataColumn columnUpdateSectionLinkText;

            public event NSurveyQuestion.QuestionSectionOptionRowChangeEventHandler QuestionSectionOptionRowChanged;

            public event NSurveyQuestion.QuestionSectionOptionRowChangeEventHandler QuestionSectionOptionRowChanging;

            public event NSurveyQuestion.QuestionSectionOptionRowChangeEventHandler QuestionSectionOptionRowDeleted;

            public event NSurveyQuestion.QuestionSectionOptionRowChangeEventHandler QuestionSectionOptionRowDeleting;

            internal QuestionSectionOptionDataTable() : base("QuestionSectionOption")
            {
                this.InitClass();
            }

            internal QuestionSectionOptionDataTable(DataTable table) : base(table.TableName)
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

            public void AddQuestionSectionOptionRow(NSurveyQuestion.QuestionSectionOptionRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.QuestionSectionOptionRow AddQuestionSectionOptionRow(NSurveyQuestion.QuestionRow parentQuestionRowByQuestionQuestionSectionOption, int MaxSections, int RepeatableSectionModeId, string AddSectionLinkText, string DeleteSectionLinkText, string EditSectionLinkText, string UpdateSectionLinkText)
            {
                NSurveyQuestion.QuestionSectionOptionRow row = (NSurveyQuestion.QuestionSectionOptionRow) base.NewRow();
                row.ItemArray = new object[] { parentQuestionRowByQuestionQuestionSectionOption[0], MaxSections, RepeatableSectionModeId, AddSectionLinkText, DeleteSectionLinkText, EditSectionLinkText, UpdateSectionLinkText };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.QuestionSectionOptionDataTable table = (NSurveyQuestion.QuestionSectionOptionDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.QuestionSectionOptionDataTable();
            }

            public NSurveyQuestion.QuestionSectionOptionRow FindByQuestionId(int QuestionId)
            {
                return (NSurveyQuestion.QuestionSectionOptionRow) base.Rows.Find(new object[] { QuestionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.QuestionSectionOptionRow);
            }

            private void InitClass()
            {
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnQuestionId);
                this.columnMaxSections = new DataColumn("MaxSections", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnMaxSections);
                this.columnRepeatableSectionModeId = new DataColumn("RepeatableSectionModeId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnRepeatableSectionModeId);
                this.columnAddSectionLinkText = new DataColumn("AddSectionLinkText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAddSectionLinkText);
                this.columnDeleteSectionLinkText = new DataColumn("DeleteSectionLinkText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDeleteSectionLinkText);
                this.columnEditSectionLinkText = new DataColumn("EditSectionLinkText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnEditSectionLinkText);
                this.columnUpdateSectionLinkText = new DataColumn("UpdateSectionLinkText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUpdateSectionLinkText);
                base.Constraints.Add(new UniqueConstraint("NSurveyQuestionKey8", new DataColumn[] { this.columnQuestionId }, true));
                this.columnQuestionId.AllowDBNull = false;
                this.columnQuestionId.Unique = true;
                this.columnQuestionId.Namespace = "";
                this.columnMaxSections.Namespace = "";
                this.columnRepeatableSectionModeId.Namespace = "";
            }

            internal void InitVars()
            {
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnMaxSections = base.Columns["MaxSections"];
                this.columnRepeatableSectionModeId = base.Columns["RepeatableSectionModeId"];
                this.columnAddSectionLinkText = base.Columns["AddSectionLinkText"];
                this.columnDeleteSectionLinkText = base.Columns["DeleteSectionLinkText"];
                this.columnEditSectionLinkText = base.Columns["EditSectionLinkText"];
                this.columnUpdateSectionLinkText = base.Columns["UpdateSectionLinkText"];
            }

            public NSurveyQuestion.QuestionSectionOptionRow NewQuestionSectionOptionRow()
            {
                return (NSurveyQuestion.QuestionSectionOptionRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.QuestionSectionOptionRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.QuestionSectionOptionRowChanged != null)
                {
                    this.QuestionSectionOptionRowChanged(this, new NSurveyQuestion.QuestionSectionOptionRowChangeEvent((NSurveyQuestion.QuestionSectionOptionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.QuestionSectionOptionRowChanging != null)
                {
                    this.QuestionSectionOptionRowChanging(this, new NSurveyQuestion.QuestionSectionOptionRowChangeEvent((NSurveyQuestion.QuestionSectionOptionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.QuestionSectionOptionRowDeleted != null)
                {
                    this.QuestionSectionOptionRowDeleted(this, new NSurveyQuestion.QuestionSectionOptionRowChangeEvent((NSurveyQuestion.QuestionSectionOptionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.QuestionSectionOptionRowDeleting != null)
                {
                    this.QuestionSectionOptionRowDeleting(this, new NSurveyQuestion.QuestionSectionOptionRowChangeEvent((NSurveyQuestion.QuestionSectionOptionRow) e.Row, e.Action));
                }
            }

            public void RemoveQuestionSectionOptionRow(NSurveyQuestion.QuestionSectionOptionRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AddSectionLinkTextColumn
            {
                get
                {
                    return this.columnAddSectionLinkText;
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

            internal DataColumn DeleteSectionLinkTextColumn
            {
                get
                {
                    return this.columnDeleteSectionLinkText;
                }
            }

            internal DataColumn EditSectionLinkTextColumn
            {
                get
                {
                    return this.columnEditSectionLinkText;
                }
            }

            public NSurveyQuestion.QuestionSectionOptionRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.QuestionSectionOptionRow) base.Rows[index];
                }
            }

            internal DataColumn MaxSectionsColumn
            {
                get
                {
                    return this.columnMaxSections;
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn RepeatableSectionModeIdColumn
            {
                get
                {
                    return this.columnRepeatableSectionModeId;
                }
            }

            internal DataColumn UpdateSectionLinkTextColumn
            {
                get
                {
                    return this.columnUpdateSectionLinkText;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class QuestionSectionOptionRow : DataRow
        {
            private NSurveyQuestion.QuestionSectionOptionDataTable tableQuestionSectionOption;

            internal QuestionSectionOptionRow(DataRowBuilder rb) : base(rb)
            {
                this.tableQuestionSectionOption = (NSurveyQuestion.QuestionSectionOptionDataTable) base.Table;
            }

            public NSurveyQuestion.QuestionSectionGridAnswerRow[] GetQuestionSectionGridAnswerRows()
            {
                return (NSurveyQuestion.QuestionSectionGridAnswerRow[]) base.GetChildRows(base.Table.ChildRelations["QuestionSectionOptionQuestionSectionGridAnswer"]);
            }

            public bool IsAddSectionLinkTextNull()
            {
                return base.IsNull(this.tableQuestionSectionOption.AddSectionLinkTextColumn);
            }

            public bool IsDeleteSectionLinkTextNull()
            {
                return base.IsNull(this.tableQuestionSectionOption.DeleteSectionLinkTextColumn);
            }

            public bool IsEditSectionLinkTextNull()
            {
                return base.IsNull(this.tableQuestionSectionOption.EditSectionLinkTextColumn);
            }

            public bool IsMaxSectionsNull()
            {
                return base.IsNull(this.tableQuestionSectionOption.MaxSectionsColumn);
            }

            public bool IsRepeatableSectionModeIdNull()
            {
                return base.IsNull(this.tableQuestionSectionOption.RepeatableSectionModeIdColumn);
            }

            public bool IsUpdateSectionLinkTextNull()
            {
                return base.IsNull(this.tableQuestionSectionOption.UpdateSectionLinkTextColumn);
            }

            public void SetAddSectionLinkTextNull()
            {
                base[this.tableQuestionSectionOption.AddSectionLinkTextColumn] = Convert.DBNull;
            }

            public void SetDeleteSectionLinkTextNull()
            {
                base[this.tableQuestionSectionOption.DeleteSectionLinkTextColumn] = Convert.DBNull;
            }

            public void SetEditSectionLinkTextNull()
            {
                base[this.tableQuestionSectionOption.EditSectionLinkTextColumn] = Convert.DBNull;
            }

            public void SetMaxSectionsNull()
            {
                base[this.tableQuestionSectionOption.MaxSectionsColumn] = Convert.DBNull;
            }

            public void SetRepeatableSectionModeIdNull()
            {
                base[this.tableQuestionSectionOption.RepeatableSectionModeIdColumn] = Convert.DBNull;
            }

            public void SetUpdateSectionLinkTextNull()
            {
                base[this.tableQuestionSectionOption.UpdateSectionLinkTextColumn] = Convert.DBNull;
            }

            public string AddSectionLinkText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableQuestionSectionOption.AddSectionLinkTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableQuestionSectionOption.AddSectionLinkTextColumn] = value;
                }
            }

            public string DeleteSectionLinkText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableQuestionSectionOption.DeleteSectionLinkTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableQuestionSectionOption.DeleteSectionLinkTextColumn] = value;
                }
            }

            public string EditSectionLinkText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableQuestionSectionOption.EditSectionLinkTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableQuestionSectionOption.EditSectionLinkTextColumn] = value;
                }
            }

            public int MaxSections
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestionSectionOption.MaxSectionsColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestionSectionOption.MaxSectionsColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    return (int) base[this.tableQuestionSectionOption.QuestionIdColumn];
                }
                set
                {
                    base[this.tableQuestionSectionOption.QuestionIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyQuestion.QuestionRow QuestionRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyQuestion.QuestionRow) base.GetParentRow(base.Table.ParentRelations["QuestionQuestionSectionOption"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["QuestionQuestionSectionOption"]);
                }
            }

            public int RepeatableSectionModeId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableQuestionSectionOption.RepeatableSectionModeIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableQuestionSectionOption.RepeatableSectionModeIdColumn] = value;
                }
            }

            public string UpdateSectionLinkText
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableQuestionSectionOption.UpdateSectionLinkTextColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableQuestionSectionOption.UpdateSectionLinkTextColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionSectionOptionRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.QuestionSectionOptionRow eventRow;

            public QuestionSectionOptionRowChangeEvent(NSurveyQuestion.QuestionSectionOptionRow row, DataRowAction action)
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

            public NSurveyQuestion.QuestionSectionOptionRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void QuestionSectionOptionRowChangeEventHandler(object sender, NSurveyQuestion.QuestionSectionOptionRowChangeEvent e);

        [DebuggerStepThrough]
        public partial class RegularExpressionDataTable : DataTable, IEnumerable
        {
            private DataColumn columnBuiltIn;
            private DataColumn columnDescription;
            private DataColumn columnRegExMessage;
            private DataColumn columnRegExpression;
            private DataColumn columnRegularExpressionId;

            public event NSurveyQuestion.RegularExpressionRowChangeEventHandler RegularExpressionRowChanged;

            public event NSurveyQuestion.RegularExpressionRowChangeEventHandler RegularExpressionRowChanging;

            public event NSurveyQuestion.RegularExpressionRowChangeEventHandler RegularExpressionRowDeleted;

            public event NSurveyQuestion.RegularExpressionRowChangeEventHandler RegularExpressionRowDeleting;

            internal RegularExpressionDataTable() : base("RegularExpression")
            {
                this.InitClass();
            }

            internal RegularExpressionDataTable(DataTable table) : base(table.TableName)
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

            public void AddRegularExpressionRow(NSurveyQuestion.RegularExpressionRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyQuestion.RegularExpressionRow AddRegularExpressionRow(bool BuiltIn, string Description, string RegExpression, string RegExMessage)
            {
                NSurveyQuestion.RegularExpressionRow row = (NSurveyQuestion.RegularExpressionRow) base.NewRow();
                object[] objArray = new object[5];
                objArray[1] = BuiltIn;
                objArray[2] = Description;
                objArray[3] = RegExpression;
                objArray[4] = RegExMessage;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyQuestion.RegularExpressionDataTable table = (NSurveyQuestion.RegularExpressionDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyQuestion.RegularExpressionDataTable();
            }

            public NSurveyQuestion.RegularExpressionRow FindByRegularExpressionId(int RegularExpressionId)
            {
                return (NSurveyQuestion.RegularExpressionRow) base.Rows.Find(new object[] { RegularExpressionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyQuestion.RegularExpressionRow);
            }

            private void InitClass()
            {
                this.columnRegularExpressionId = new DataColumn("RegularExpressionId", typeof(int), null, MappingType.Attribute);
                base.Columns.Add(this.columnRegularExpressionId);
                this.columnBuiltIn = new DataColumn("BuiltIn", typeof(bool), null, MappingType.Attribute);
                base.Columns.Add(this.columnBuiltIn);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnRegExpression = new DataColumn("RegExpression", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnRegExpression);
                this.columnRegExMessage = new DataColumn("RegExMessage", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnRegExMessage);
                base.Constraints.Add(new UniqueConstraint("NSurveyQuestionKey6", new DataColumn[] { this.columnRegularExpressionId }, true));
                this.columnRegularExpressionId.AutoIncrement = true;
                this.columnRegularExpressionId.AllowDBNull = false;
                this.columnRegularExpressionId.Unique = true;
                this.columnRegularExpressionId.Namespace = "";
                this.columnBuiltIn.Namespace = "";
            }

            internal void InitVars()
            {
                this.columnRegularExpressionId = base.Columns["RegularExpressionId"];
                this.columnBuiltIn = base.Columns["BuiltIn"];
                this.columnDescription = base.Columns["Description"];
                this.columnRegExpression = base.Columns["RegExpression"];
                this.columnRegExMessage = base.Columns["RegExMessage"];
            }

            public NSurveyQuestion.RegularExpressionRow NewRegularExpressionRow()
            {
                return (NSurveyQuestion.RegularExpressionRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyQuestion.RegularExpressionRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.RegularExpressionRowChanged != null)
                {
                    this.RegularExpressionRowChanged(this, new NSurveyQuestion.RegularExpressionRowChangeEvent((NSurveyQuestion.RegularExpressionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.RegularExpressionRowChanging != null)
                {
                    this.RegularExpressionRowChanging(this, new NSurveyQuestion.RegularExpressionRowChangeEvent((NSurveyQuestion.RegularExpressionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.RegularExpressionRowDeleted != null)
                {
                    this.RegularExpressionRowDeleted(this, new NSurveyQuestion.RegularExpressionRowChangeEvent((NSurveyQuestion.RegularExpressionRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.RegularExpressionRowDeleting != null)
                {
                    this.RegularExpressionRowDeleting(this, new NSurveyQuestion.RegularExpressionRowChangeEvent((NSurveyQuestion.RegularExpressionRow) e.Row, e.Action));
                }
            }

            public void RemoveRegularExpressionRow(NSurveyQuestion.RegularExpressionRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn BuiltInColumn
            {
                get
                {
                    return this.columnBuiltIn;
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

            internal DataColumn DescriptionColumn
            {
                get
                {
                    return this.columnDescription;
                }
            }

            public NSurveyQuestion.RegularExpressionRow this[int index]
            {
                get
                {
                    return (NSurveyQuestion.RegularExpressionRow) base.Rows[index];
                }
            }

            internal DataColumn RegExMessageColumn
            {
                get
                {
                    return this.columnRegExMessage;
                }
            }

            internal DataColumn RegExpressionColumn
            {
                get
                {
                    return this.columnRegExpression;
                }
            }

            internal DataColumn RegularExpressionIdColumn
            {
                get
                {
                    return this.columnRegularExpressionId;
                }
            }
        }

        [DebuggerStepThrough]
        public partial class RegularExpressionRow : DataRow
        {
            private NSurveyQuestion.RegularExpressionDataTable tableRegularExpression;

            internal RegularExpressionRow(DataRowBuilder rb) : base(rb)
            {
                this.tableRegularExpression = (NSurveyQuestion.RegularExpressionDataTable) base.Table;
            }

            public NSurveyQuestion.AnswerRow[] GetAnswerRows()
            {
                return (NSurveyQuestion.AnswerRow[]) base.GetChildRows(base.Table.ChildRelations["RegularExpressionAnswer"]);
            }

            public bool IsBuiltInNull()
            {
                return base.IsNull(this.tableRegularExpression.BuiltInColumn);
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableRegularExpression.DescriptionColumn);
            }

            public bool IsRegExMessageNull()
            {
                return base.IsNull(this.tableRegularExpression.RegExMessageColumn);
            }

            public bool IsRegExpressionNull()
            {
                return base.IsNull(this.tableRegularExpression.RegExpressionColumn);
            }

            public void SetBuiltInNull()
            {
                base[this.tableRegularExpression.BuiltInColumn] = Convert.DBNull;
            }

            public void SetDescriptionNull()
            {
                base[this.tableRegularExpression.DescriptionColumn] = Convert.DBNull;
            }

            public void SetRegExMessageNull()
            {
                base[this.tableRegularExpression.RegExMessageColumn] = Convert.DBNull;
            }

            public void SetRegExpressionNull()
            {
                base[this.tableRegularExpression.RegExpressionColumn] = Convert.DBNull;
            }

            public bool BuiltIn
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableRegularExpression.BuiltInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableRegularExpression.BuiltInColumn] = value;
                }
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableRegularExpression.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableRegularExpression.DescriptionColumn] = value;
                }
            }

            public string RegExMessage
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableRegularExpression.RegExMessageColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableRegularExpression.RegExMessageColumn] = value;
                }
            }

            public string RegExpression
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableRegularExpression.RegExpressionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableRegularExpression.RegExpressionColumn] = value;
                }
            }

            public int RegularExpressionId
            {
                get
                {
                    return (int) base[this.tableRegularExpression.RegularExpressionIdColumn];
                }
                set
                {
                    base[this.tableRegularExpression.RegularExpressionIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class RegularExpressionRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyQuestion.RegularExpressionRow eventRow;

            public RegularExpressionRowChangeEvent(NSurveyQuestion.RegularExpressionRow row, DataRowAction action)
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

            public NSurveyQuestion.RegularExpressionRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void RegularExpressionRowChangeEventHandler(object sender, NSurveyQuestion.RegularExpressionRowChangeEvent e);
    }
}

