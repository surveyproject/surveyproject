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
    public class NSurveyDataSource : DataSet
    {
        private DataRelation relationXmlAnswers_XmlAnswer;
        private DataRelation relationXmlDataSource_XmlAnswers;
        private XmlAnswerDataTable tableXmlAnswer;
        private XmlAnswersDataTable tableXmlAnswers;
        private XmlDataSourceDataTable tableXmlDataSource;

        public NSurveyDataSource()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected NSurveyDataSource(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["XmlDataSource"] != null)
                {
                    base.Tables.Add(new XmlDataSourceDataTable(dataSet.Tables["XmlDataSource"]));
                }
                if (dataSet.Tables["XmlAnswers"] != null)
                {
                    base.Tables.Add(new XmlAnswersDataTable(dataSet.Tables["XmlAnswers"]));
                }
                if (dataSet.Tables["XmlAnswer"] != null)
                {
                    base.Tables.Add(new XmlAnswerDataTable(dataSet.Tables["XmlAnswer"]));
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
            NSurveyDataSource source = (NSurveyDataSource) base.Clone();
            source.InitVars();
            return source;
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
            base.DataSetName = "NSurveyDataSource";
            base.Prefix = "";
            base.Namespace = "http://www.nsurvey.org/NSurveyDataSource.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableXmlDataSource = new XmlDataSourceDataTable();
            base.Tables.Add(this.tableXmlDataSource);
            this.tableXmlAnswers = new XmlAnswersDataTable();
            base.Tables.Add(this.tableXmlAnswers);
            this.tableXmlAnswer = new XmlAnswerDataTable();
            base.Tables.Add(this.tableXmlAnswer);
            ForeignKeyConstraint constraint = new ForeignKeyConstraint("XmlDataSource_XmlAnswers", new DataColumn[] { this.tableXmlDataSource.XmlDataSource_IdColumn }, new DataColumn[] { this.tableXmlAnswers.XmlDataSource_IdColumn });
            this.tableXmlAnswers.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            constraint = new ForeignKeyConstraint("XmlAnswers_XmlAnswer", new DataColumn[] { this.tableXmlAnswers.XmlAnswers_IdColumn }, new DataColumn[] { this.tableXmlAnswer.XmlAnswers_IdColumn });
            this.tableXmlAnswer.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            this.relationXmlAnswers_XmlAnswer = new DataRelation("XmlAnswers_XmlAnswer", new DataColumn[] { this.tableXmlAnswers.XmlAnswers_IdColumn }, new DataColumn[] { this.tableXmlAnswer.XmlAnswers_IdColumn }, false);
            this.relationXmlAnswers_XmlAnswer.Nested = true;
            base.Relations.Add(this.relationXmlAnswers_XmlAnswer);
            this.relationXmlDataSource_XmlAnswers = new DataRelation("XmlDataSource_XmlAnswers", new DataColumn[] { this.tableXmlDataSource.XmlDataSource_IdColumn }, new DataColumn[] { this.tableXmlAnswers.XmlDataSource_IdColumn }, false);
            this.relationXmlDataSource_XmlAnswers.Nested = true;
            base.Relations.Add(this.relationXmlDataSource_XmlAnswers);
        }

        internal void InitVars()
        {
            this.tableXmlDataSource = (XmlDataSourceDataTable) base.Tables["XmlDataSource"];
            if (this.tableXmlDataSource != null)
            {
                this.tableXmlDataSource.InitVars();
            }
            this.tableXmlAnswers = (XmlAnswersDataTable) base.Tables["XmlAnswers"];
            if (this.tableXmlAnswers != null)
            {
                this.tableXmlAnswers.InitVars();
            }
            this.tableXmlAnswer = (XmlAnswerDataTable) base.Tables["XmlAnswer"];
            if (this.tableXmlAnswer != null)
            {
                this.tableXmlAnswer.InitVars();
            }
            this.relationXmlAnswers_XmlAnswer = base.Relations["XmlAnswers_XmlAnswer"];
            this.relationXmlDataSource_XmlAnswers = base.Relations["XmlDataSource_XmlAnswers"];
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["XmlDataSource"] != null)
            {
                base.Tables.Add(new XmlDataSourceDataTable(dataSet.Tables["XmlDataSource"]));
            }
            if (dataSet.Tables["XmlAnswers"] != null)
            {
                base.Tables.Add(new XmlAnswersDataTable(dataSet.Tables["XmlAnswers"]));
            }
            if (dataSet.Tables["XmlAnswer"] != null)
            {
                base.Tables.Add(new XmlAnswerDataTable(dataSet.Tables["XmlAnswer"]));
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

        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        private bool ShouldSerializeXmlAnswer()
        {
            return false;
        }

        private bool ShouldSerializeXmlAnswers()
        {
            return false;
        }

        private bool ShouldSerializeXmlDataSource()
        {
            return false;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public XmlAnswerDataTable XmlAnswer
        {
            get
            {
                return this.tableXmlAnswer;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public XmlAnswersDataTable XmlAnswers
        {
            get
            {
                return this.tableXmlAnswers;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public XmlDataSourceDataTable XmlDataSource
        {
            get
            {
                return this.tableXmlDataSource;
            }
        }

        [DebuggerStepThrough]
        public class XmlAnswerDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerDescription;
            private DataColumn columnAnswerValue;
            private DataColumn columnXmlAnswers_Id;

            public event NSurveyDataSource.XmlAnswerRowChangeEventHandler XmlAnswerRowChanged;

            public event NSurveyDataSource.XmlAnswerRowChangeEventHandler XmlAnswerRowChanging;

            public event NSurveyDataSource.XmlAnswerRowChangeEventHandler XmlAnswerRowDeleted;

            public event NSurveyDataSource.XmlAnswerRowChangeEventHandler XmlAnswerRowDeleting;

            internal XmlAnswerDataTable() : base("XmlAnswer")
            {
                this.InitClass();
            }

            internal XmlAnswerDataTable(DataTable table) : base(table.TableName)
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

            public void AddXmlAnswerRow(NSurveyDataSource.XmlAnswerRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyDataSource.XmlAnswerRow AddXmlAnswerRow(string AnswerValue, string AnswerDescription, NSurveyDataSource.XmlAnswersRow parentXmlAnswersRowByXmlAnswers_XmlAnswer)
            {
                NSurveyDataSource.XmlAnswerRow row = (NSurveyDataSource.XmlAnswerRow) base.NewRow();
                row.ItemArray = new object[] { AnswerValue, AnswerDescription, parentXmlAnswersRowByXmlAnswers_XmlAnswer[0] };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyDataSource.XmlAnswerDataTable table = (NSurveyDataSource.XmlAnswerDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyDataSource.XmlAnswerDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyDataSource.XmlAnswerRow);
            }

            private void InitClass()
            {
                this.columnAnswerValue = new DataColumn("AnswerValue", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerValue);
                this.columnAnswerDescription = new DataColumn("AnswerDescription", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerDescription);
                this.columnXmlAnswers_Id = new DataColumn("XmlAnswers_Id", typeof(int), null, MappingType.Hidden);
                base.Columns.Add(this.columnXmlAnswers_Id);
            }

            internal void InitVars()
            {
                this.columnAnswerValue = base.Columns["AnswerValue"];
                this.columnAnswerDescription = base.Columns["AnswerDescription"];
                this.columnXmlAnswers_Id = base.Columns["XmlAnswers_Id"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyDataSource.XmlAnswerRow(builder);
            }

            public NSurveyDataSource.XmlAnswerRow NewXmlAnswerRow()
            {
                return (NSurveyDataSource.XmlAnswerRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.XmlAnswerRowChanged != null)
                {
                    this.XmlAnswerRowChanged(this, new NSurveyDataSource.XmlAnswerRowChangeEvent((NSurveyDataSource.XmlAnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.XmlAnswerRowChanging != null)
                {
                    this.XmlAnswerRowChanging(this, new NSurveyDataSource.XmlAnswerRowChangeEvent((NSurveyDataSource.XmlAnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.XmlAnswerRowDeleted != null)
                {
                    this.XmlAnswerRowDeleted(this, new NSurveyDataSource.XmlAnswerRowChangeEvent((NSurveyDataSource.XmlAnswerRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.XmlAnswerRowDeleting != null)
                {
                    this.XmlAnswerRowDeleting(this, new NSurveyDataSource.XmlAnswerRowChangeEvent((NSurveyDataSource.XmlAnswerRow) e.Row, e.Action));
                }
            }

            public void RemoveXmlAnswerRow(NSurveyDataSource.XmlAnswerRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn AnswerDescriptionColumn
            {
                get
                {
                    return this.columnAnswerDescription;
                }
            }

            internal DataColumn AnswerValueColumn
            {
                get
                {
                    return this.columnAnswerValue;
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

            public NSurveyDataSource.XmlAnswerRow this[int index]
            {
                get
                {
                    return (NSurveyDataSource.XmlAnswerRow) base.Rows[index];
                }
            }

            internal DataColumn XmlAnswers_IdColumn
            {
                get
                {
                    return this.columnXmlAnswers_Id;
                }
            }
        }

        [DebuggerStepThrough]
        public class XmlAnswerRow : DataRow
        {
            private NSurveyDataSource.XmlAnswerDataTable tableXmlAnswer;

            internal XmlAnswerRow(DataRowBuilder rb) : base(rb)
            {
                this.tableXmlAnswer = (NSurveyDataSource.XmlAnswerDataTable) base.Table;
            }

            public bool IsAnswerDescriptionNull()
            {
                return base.IsNull(this.tableXmlAnswer.AnswerDescriptionColumn);
            }

            public bool IsAnswerValueNull()
            {
                return base.IsNull(this.tableXmlAnswer.AnswerValueColumn);
            }

            public void SetAnswerDescriptionNull()
            {
                base[this.tableXmlAnswer.AnswerDescriptionColumn] = Convert.DBNull;
            }

            public void SetAnswerValueNull()
            {
                base[this.tableXmlAnswer.AnswerValueColumn] = Convert.DBNull;
            }

            public string AnswerDescription
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableXmlAnswer.AnswerDescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableXmlAnswer.AnswerDescriptionColumn] = value;
                }
            }

            public string AnswerValue
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableXmlAnswer.AnswerValueColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableXmlAnswer.AnswerValueColumn] = value;
                }
            }

            public Votations.NSurvey.Data.NSurveyDataSource.XmlAnswersRow XmlAnswersRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyDataSource.XmlAnswersRow) base.GetParentRow(base.Table.ParentRelations["XmlAnswers_XmlAnswer"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["XmlAnswers_XmlAnswer"]);
                }
            }
        }

        [DebuggerStepThrough]
        public class XmlAnswerRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyDataSource.XmlAnswerRow eventRow;

            public XmlAnswerRowChangeEvent(NSurveyDataSource.XmlAnswerRow row, DataRowAction action)
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

            public NSurveyDataSource.XmlAnswerRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void XmlAnswerRowChangeEventHandler(object sender, NSurveyDataSource.XmlAnswerRowChangeEvent e);

        [DebuggerStepThrough]
        public class XmlAnswersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnXmlAnswers_Id;
            private DataColumn columnXmlDataSource_Id;

            public event NSurveyDataSource.XmlAnswersRowChangeEventHandler XmlAnswersRowChanged;

            public event NSurveyDataSource.XmlAnswersRowChangeEventHandler XmlAnswersRowChanging;

            public event NSurveyDataSource.XmlAnswersRowChangeEventHandler XmlAnswersRowDeleted;

            public event NSurveyDataSource.XmlAnswersRowChangeEventHandler XmlAnswersRowDeleting;

            internal XmlAnswersDataTable() : base("XmlAnswers")
            {
                this.InitClass();
            }

            internal XmlAnswersDataTable(DataTable table) : base(table.TableName)
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

            public void AddXmlAnswersRow(NSurveyDataSource.XmlAnswersRow row)
            {
                base.Rows.Add(row);
            }

            public NSurveyDataSource.XmlAnswersRow AddXmlAnswersRow(NSurveyDataSource.XmlDataSourceRow parentXmlDataSourceRowByXmlDataSource_XmlAnswers)
            {
                NSurveyDataSource.XmlAnswersRow row = (NSurveyDataSource.XmlAnswersRow) base.NewRow();
                object[] objArray = new object[2];
                objArray[1] = parentXmlDataSourceRowByXmlDataSource_XmlAnswers[1];
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                NSurveyDataSource.XmlAnswersDataTable table = (NSurveyDataSource.XmlAnswersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyDataSource.XmlAnswersDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyDataSource.XmlAnswersRow);
            }

            private void InitClass()
            {
                this.columnXmlAnswers_Id = new DataColumn("XmlAnswers_Id", typeof(int), null, MappingType.Hidden);
                base.Columns.Add(this.columnXmlAnswers_Id);
                this.columnXmlDataSource_Id = new DataColumn("XmlDataSource_Id", typeof(int), null, MappingType.Hidden);
                base.Columns.Add(this.columnXmlDataSource_Id);
                base.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnXmlAnswers_Id }, true));
                this.columnXmlAnswers_Id.AutoIncrement = true;
                this.columnXmlAnswers_Id.AllowDBNull = false;
                this.columnXmlAnswers_Id.Unique = true;
            }

            internal void InitVars()
            {
                this.columnXmlAnswers_Id = base.Columns["XmlAnswers_Id"];
                this.columnXmlDataSource_Id = base.Columns["XmlDataSource_Id"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyDataSource.XmlAnswersRow(builder);
            }

            public NSurveyDataSource.XmlAnswersRow NewXmlAnswersRow()
            {
                return (NSurveyDataSource.XmlAnswersRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.XmlAnswersRowChanged != null)
                {
                    this.XmlAnswersRowChanged(this, new NSurveyDataSource.XmlAnswersRowChangeEvent((NSurveyDataSource.XmlAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.XmlAnswersRowChanging != null)
                {
                    this.XmlAnswersRowChanging(this, new NSurveyDataSource.XmlAnswersRowChangeEvent((NSurveyDataSource.XmlAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.XmlAnswersRowDeleted != null)
                {
                    this.XmlAnswersRowDeleted(this, new NSurveyDataSource.XmlAnswersRowChangeEvent((NSurveyDataSource.XmlAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.XmlAnswersRowDeleting != null)
                {
                    this.XmlAnswersRowDeleting(this, new NSurveyDataSource.XmlAnswersRowChangeEvent((NSurveyDataSource.XmlAnswersRow) e.Row, e.Action));
                }
            }

            public void RemoveXmlAnswersRow(NSurveyDataSource.XmlAnswersRow row)
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

            public NSurveyDataSource.XmlAnswersRow this[int index]
            {
                get
                {
                    return (NSurveyDataSource.XmlAnswersRow) base.Rows[index];
                }
            }

            internal DataColumn XmlAnswers_IdColumn
            {
                get
                {
                    return this.columnXmlAnswers_Id;
                }
            }

            internal DataColumn XmlDataSource_IdColumn
            {
                get
                {
                    return this.columnXmlDataSource_Id;
                }
            }
        }

        [DebuggerStepThrough]
        public class XmlAnswersRow : DataRow
        {
            private NSurveyDataSource.XmlAnswersDataTable tableXmlAnswers;

            internal XmlAnswersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableXmlAnswers = (NSurveyDataSource.XmlAnswersDataTable) base.Table;
            }

            public NSurveyDataSource.XmlAnswerRow[] GetXmlAnswerRows()
            {
                return (NSurveyDataSource.XmlAnswerRow[]) base.GetChildRows(base.Table.ChildRelations["XmlAnswers_XmlAnswer"]);
            }

            public Votations.NSurvey.Data.NSurveyDataSource.XmlDataSourceRow XmlDataSourceRow
            {
                get
                {
                    return (Votations.NSurvey.Data.NSurveyDataSource.XmlDataSourceRow) base.GetParentRow(base.Table.ParentRelations["XmlDataSource_XmlAnswers"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["XmlDataSource_XmlAnswers"]);
                }
            }
        }

        [DebuggerStepThrough]
        public class XmlAnswersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyDataSource.XmlAnswersRow eventRow;

            public XmlAnswersRowChangeEvent(NSurveyDataSource.XmlAnswersRow row, DataRowAction action)
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

            public NSurveyDataSource.XmlAnswersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void XmlAnswersRowChangeEventHandler(object sender, NSurveyDataSource.XmlAnswersRowChangeEvent e);

        [DebuggerStepThrough]
        public class XmlDataSourceDataTable : DataTable, IEnumerable
        {
            private DataColumn columnRunTimeAnswerLabel;
            private DataColumn columnXmlDataSource_Id;

            public event NSurveyDataSource.XmlDataSourceRowChangeEventHandler XmlDataSourceRowChanged;

            public event NSurveyDataSource.XmlDataSourceRowChangeEventHandler XmlDataSourceRowChanging;

            public event NSurveyDataSource.XmlDataSourceRowChangeEventHandler XmlDataSourceRowDeleted;

            public event NSurveyDataSource.XmlDataSourceRowChangeEventHandler XmlDataSourceRowDeleting;

            internal XmlDataSourceDataTable() : base("XmlDataSource")
            {
                this.InitClass();
            }

            internal XmlDataSourceDataTable(DataTable table) : base(table.TableName)
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

            public NSurveyDataSource.XmlDataSourceRow AddXmlDataSourceRow(string RunTimeAnswerLabel)
            {
                NSurveyDataSource.XmlDataSourceRow row = (NSurveyDataSource.XmlDataSourceRow) base.NewRow();
                object[] objArray = new object[2];
                objArray[0] = RunTimeAnswerLabel;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public void AddXmlDataSourceRow(NSurveyDataSource.XmlDataSourceRow row)
            {
                base.Rows.Add(row);
            }

            public override DataTable Clone()
            {
                NSurveyDataSource.XmlDataSourceDataTable table = (NSurveyDataSource.XmlDataSourceDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new NSurveyDataSource.XmlDataSourceDataTable();
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(NSurveyDataSource.XmlDataSourceRow);
            }

            private void InitClass()
            {
                this.columnRunTimeAnswerLabel = new DataColumn("RunTimeAnswerLabel", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnRunTimeAnswerLabel);
                this.columnXmlDataSource_Id = new DataColumn("XmlDataSource_Id", typeof(int), null, MappingType.Hidden);
                base.Columns.Add(this.columnXmlDataSource_Id);
                base.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnXmlDataSource_Id }, true));
                this.columnXmlDataSource_Id.AutoIncrement = true;
                this.columnXmlDataSource_Id.AllowDBNull = false;
                this.columnXmlDataSource_Id.Unique = true;
            }

            internal void InitVars()
            {
                this.columnRunTimeAnswerLabel = base.Columns["RunTimeAnswerLabel"];
                this.columnXmlDataSource_Id = base.Columns["XmlDataSource_Id"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new NSurveyDataSource.XmlDataSourceRow(builder);
            }

            public NSurveyDataSource.XmlDataSourceRow NewXmlDataSourceRow()
            {
                return (NSurveyDataSource.XmlDataSourceRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.XmlDataSourceRowChanged != null)
                {
                    this.XmlDataSourceRowChanged(this, new NSurveyDataSource.XmlDataSourceRowChangeEvent((NSurveyDataSource.XmlDataSourceRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.XmlDataSourceRowChanging != null)
                {
                    this.XmlDataSourceRowChanging(this, new NSurveyDataSource.XmlDataSourceRowChangeEvent((NSurveyDataSource.XmlDataSourceRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.XmlDataSourceRowDeleted != null)
                {
                    this.XmlDataSourceRowDeleted(this, new NSurveyDataSource.XmlDataSourceRowChangeEvent((NSurveyDataSource.XmlDataSourceRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.XmlDataSourceRowDeleting != null)
                {
                    this.XmlDataSourceRowDeleting(this, new NSurveyDataSource.XmlDataSourceRowChangeEvent((NSurveyDataSource.XmlDataSourceRow) e.Row, e.Action));
                }
            }

            public void RemoveXmlDataSourceRow(NSurveyDataSource.XmlDataSourceRow row)
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

            public NSurveyDataSource.XmlDataSourceRow this[int index]
            {
                get
                {
                    return (NSurveyDataSource.XmlDataSourceRow) base.Rows[index];
                }
            }

            internal DataColumn RunTimeAnswerLabelColumn
            {
                get
                {
                    return this.columnRunTimeAnswerLabel;
                }
            }

            internal DataColumn XmlDataSource_IdColumn
            {
                get
                {
                    return this.columnXmlDataSource_Id;
                }
            }
        }

        [DebuggerStepThrough]
        public class XmlDataSourceRow : DataRow
        {
            private NSurveyDataSource.XmlDataSourceDataTable tableXmlDataSource;

            internal XmlDataSourceRow(DataRowBuilder rb) : base(rb)
            {
                this.tableXmlDataSource = (NSurveyDataSource.XmlDataSourceDataTable) base.Table;
            }

            public NSurveyDataSource.XmlAnswersRow[] GetXmlAnswersRows()
            {
                return (NSurveyDataSource.XmlAnswersRow[]) base.GetChildRows(base.Table.ChildRelations["XmlDataSource_XmlAnswers"]);
            }

            public bool IsRunTimeAnswerLabelNull()
            {
                return base.IsNull(this.tableXmlDataSource.RunTimeAnswerLabelColumn);
            }

            public void SetRunTimeAnswerLabelNull()
            {
                base[this.tableXmlDataSource.RunTimeAnswerLabelColumn] = Convert.DBNull;
            }

            public string RunTimeAnswerLabel
            {
                get
                {
                    if (this.IsRunTimeAnswerLabelNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableXmlDataSource.RunTimeAnswerLabelColumn];
                }
                set
                {
                    base[this.tableXmlDataSource.RunTimeAnswerLabelColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class XmlDataSourceRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private NSurveyDataSource.XmlDataSourceRow eventRow;

            public XmlDataSourceRowChangeEvent(NSurveyDataSource.XmlDataSourceRow row, DataRowAction action)
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

            public NSurveyDataSource.XmlDataSourceRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void XmlDataSourceRowChangeEventHandler(object sender, NSurveyDataSource.XmlDataSourceRowChangeEvent e);
    }
}

