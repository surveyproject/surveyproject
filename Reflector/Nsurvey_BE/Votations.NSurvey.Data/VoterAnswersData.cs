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
    public class VoterAnswersData : DataSet
    {
        private DataRelation relationVoterAnswersRelation;
        private VotersDataTable tableVoters;
        private VotersAnswersDataTable tableVotersAnswers;

        public VoterAnswersData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected VoterAnswersData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["Voters"] != null)
                {
                    base.Tables.Add(new VotersDataTable(dataSet.Tables["Voters"]));
                }
                if (dataSet.Tables["VotersAnswers"] != null)
                {
                    base.Tables.Add(new VotersAnswersDataTable(dataSet.Tables["VotersAnswers"]));
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
            VoterAnswersData data = (VoterAnswersData) base.Clone();
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
            base.DataSetName = "VoterAnswersData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/VoterAnswersData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableVoters = new VotersDataTable();
            base.Tables.Add(this.tableVoters);
            this.tableVotersAnswers = new VotersAnswersDataTable();
            base.Tables.Add(this.tableVotersAnswers);
            ForeignKeyConstraint constraint = new ForeignKeyConstraint("VoterAnswersRelation", new DataColumn[] { this.tableVoters.VoterIdColumn }, new DataColumn[] { this.tableVotersAnswers.VoterIdColumn });
            this.tableVotersAnswers.Constraints.Add(constraint);
            constraint.AcceptRejectRule = AcceptRejectRule.None;
            constraint.DeleteRule = Rule.Cascade;
            constraint.UpdateRule = Rule.Cascade;
            this.relationVoterAnswersRelation = new DataRelation("VoterAnswersRelation", new DataColumn[] { this.tableVoters.VoterIdColumn }, new DataColumn[] { this.tableVotersAnswers.VoterIdColumn }, false);
            base.Relations.Add(this.relationVoterAnswersRelation);
        }

        internal void InitVars()
        {
            this.tableVoters = (VotersDataTable) base.Tables["Voters"];
            if (this.tableVoters != null)
            {
                this.tableVoters.InitVars();
            }
            this.tableVotersAnswers = (VotersAnswersDataTable) base.Tables["VotersAnswers"];
            if (this.tableVotersAnswers != null)
            {
                this.tableVotersAnswers.InitVars();
            }
            this.relationVoterAnswersRelation = base.Relations["VoterAnswersRelation"];
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["Voters"] != null)
            {
                base.Tables.Add(new VotersDataTable(dataSet.Tables["Voters"]));
            }
            if (dataSet.Tables["VotersAnswers"] != null)
            {
                base.Tables.Add(new VotersAnswersDataTable(dataSet.Tables["VotersAnswers"]));
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

        private bool ShouldSerializeVoters()
        {
            return false;
        }

        private bool ShouldSerializeVotersAnswers()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public VotersDataTable Voters
        {
            get
            {
                return this.tableVoters;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public VotersAnswersDataTable VotersAnswers
        {
            get
            {
                return this.tableVotersAnswers;
            }
        }

        [DebuggerStepThrough]
        public class VotersAnswersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAnswerId;
            private DataColumn columnAnswerText;
            private DataColumn columnQuestionId;
            private DataColumn columnSectionNumber;
            private DataColumn columnTypeMode;
            private DataColumn columnVoterId;

            public event VoterAnswersData.VotersAnswersRowChangeEventHandler VotersAnswersRowChanged;

            public event VoterAnswersData.VotersAnswersRowChangeEventHandler VotersAnswersRowChanging;

            public event VoterAnswersData.VotersAnswersRowChangeEventHandler VotersAnswersRowDeleted;

            public event VoterAnswersData.VotersAnswersRowChangeEventHandler VotersAnswersRowDeleting;

            internal VotersAnswersDataTable() : base("VotersAnswers")
            {
                this.InitClass();
            }

            internal VotersAnswersDataTable(DataTable table) : base(table.TableName)
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

            public void AddVotersAnswersRow(VoterAnswersData.VotersAnswersRow row)
            {
                base.Rows.Add(row);
            }

            public VoterAnswersData.VotersAnswersRow AddVotersAnswersRow(VoterAnswersData.VotersRow parentVotersRowByVoterAnswersRelation, int AnswerId, int QuestionId, string AnswerText, int SectionNumber, int TypeMode)
            {
                VoterAnswersData.VotersAnswersRow row = (VoterAnswersData.VotersAnswersRow) base.NewRow();
                row.ItemArray = new object[] { parentVotersRowByVoterAnswersRelation[0], AnswerId, QuestionId, AnswerText, SectionNumber, TypeMode };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                VoterAnswersData.VotersAnswersDataTable table = (VoterAnswersData.VotersAnswersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new VoterAnswersData.VotersAnswersDataTable();
            }

            public VoterAnswersData.VotersAnswersRow FindByVoterIdAnswerIdSectionNumber(int VoterId, int AnswerId, int SectionNumber)
            {
                return (VoterAnswersData.VotersAnswersRow) base.Rows.Find(new object[] { VoterId, AnswerId, SectionNumber });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(VoterAnswersData.VotersAnswersRow);
            }

            private void InitClass()
            {
                this.columnVoterId = new DataColumn("VoterId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoterId);
                this.columnAnswerId = new DataColumn("AnswerId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerId);
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnAnswerText = new DataColumn("AnswerText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAnswerText);
                this.columnSectionNumber = new DataColumn("SectionNumber", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSectionNumber);
                this.columnTypeMode = new DataColumn("TypeMode", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTypeMode);
                base.Constraints.Add(new UniqueConstraint("VoterAnswersDataKey2", new DataColumn[] { this.columnVoterId, this.columnAnswerId, this.columnSectionNumber }, true));
                this.columnVoterId.AllowDBNull = false;
                this.columnAnswerId.AllowDBNull = false;
                this.columnSectionNumber.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnVoterId = base.Columns["VoterId"];
                this.columnAnswerId = base.Columns["AnswerId"];
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnAnswerText = base.Columns["AnswerText"];
                this.columnSectionNumber = base.Columns["SectionNumber"];
                this.columnTypeMode = base.Columns["TypeMode"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new VoterAnswersData.VotersAnswersRow(builder);
            }

            public VoterAnswersData.VotersAnswersRow NewVotersAnswersRow()
            {
                return (VoterAnswersData.VotersAnswersRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.VotersAnswersRowChanged != null)
                {
                    this.VotersAnswersRowChanged(this, new VoterAnswersData.VotersAnswersRowChangeEvent((VoterAnswersData.VotersAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.VotersAnswersRowChanging != null)
                {
                    this.VotersAnswersRowChanging(this, new VoterAnswersData.VotersAnswersRowChangeEvent((VoterAnswersData.VotersAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.VotersAnswersRowDeleted != null)
                {
                    this.VotersAnswersRowDeleted(this, new VoterAnswersData.VotersAnswersRowChangeEvent((VoterAnswersData.VotersAnswersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.VotersAnswersRowDeleting != null)
                {
                    this.VotersAnswersRowDeleting(this, new VoterAnswersData.VotersAnswersRowChangeEvent((VoterAnswersData.VotersAnswersRow) e.Row, e.Action));
                }
            }

            public void RemoveVotersAnswersRow(VoterAnswersData.VotersAnswersRow row)
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

            public VoterAnswersData.VotersAnswersRow this[int index]
            {
                get
                {
                    return (VoterAnswersData.VotersAnswersRow) base.Rows[index];
                }
            }

            internal DataColumn QuestionIdColumn
            {
                get
                {
                    return this.columnQuestionId;
                }
            }

            internal DataColumn SectionNumberColumn
            {
                get
                {
                    return this.columnSectionNumber;
                }
            }

            internal DataColumn TypeModeColumn
            {
                get
                {
                    return this.columnTypeMode;
                }
            }

            internal DataColumn VoterIdColumn
            {
                get
                {
                    return this.columnVoterId;
                }
            }
        }

        [DebuggerStepThrough]
        public class VotersAnswersRow : DataRow
        {
            private VoterAnswersData.VotersAnswersDataTable tableVotersAnswers;

            internal VotersAnswersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableVotersAnswers = (VoterAnswersData.VotersAnswersDataTable) base.Table;
            }

            public bool IsAnswerTextNull()
            {
                return base.IsNull(this.tableVotersAnswers.AnswerTextColumn);
            }

            public bool IsQuestionIdNull()
            {
                return base.IsNull(this.tableVotersAnswers.QuestionIdColumn);
            }

            public bool IsTypeModeNull()
            {
                return base.IsNull(this.tableVotersAnswers.TypeModeColumn);
            }

            public void SetAnswerTextNull()
            {
                base[this.tableVotersAnswers.AnswerTextColumn] = Convert.DBNull;
            }

            public void SetQuestionIdNull()
            {
                base[this.tableVotersAnswers.QuestionIdColumn] = Convert.DBNull;
            }

            public void SetTypeModeNull()
            {
                base[this.tableVotersAnswers.TypeModeColumn] = Convert.DBNull;
            }

            public int AnswerId
            {
                get
                {
                    return (int) base[this.tableVotersAnswers.AnswerIdColumn];
                }
                set
                {
                    base[this.tableVotersAnswers.AnswerIdColumn] = value;
                }
            }

            public string AnswerText
            {
                get
                {
                    if (this.IsAnswerTextNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableVotersAnswers.AnswerTextColumn];
                }
                set
                {
                    base[this.tableVotersAnswers.AnswerTextColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableVotersAnswers.QuestionIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableVotersAnswers.QuestionIdColumn] = value;
                }
            }

            public int SectionNumber
            {
                get
                {
                    return (int) base[this.tableVotersAnswers.SectionNumberColumn];
                }
                set
                {
                    base[this.tableVotersAnswers.SectionNumberColumn] = value;
                }
            }

            public int TypeMode
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableVotersAnswers.TypeModeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableVotersAnswers.TypeModeColumn] = value;
                }
            }

            public int VoterId
            {
                get
                {
                    return (int) base[this.tableVotersAnswers.VoterIdColumn];
                }
                set
                {
                    base[this.tableVotersAnswers.VoterIdColumn] = value;
                }
            }

            public Votations.NSurvey.Data.VoterAnswersData.VotersRow VotersRow
            {
                get
                {
                    return (Votations.NSurvey.Data.VoterAnswersData.VotersRow) base.GetParentRow(base.Table.ParentRelations["VoterAnswersRelation"]);
                }
                set
                {
                    base.SetParentRow(value, base.Table.ParentRelations["VoterAnswersRelation"]);
                }
            }
        }

        [DebuggerStepThrough]
        public class VotersAnswersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private VoterAnswersData.VotersAnswersRow eventRow;

            public VotersAnswersRowChangeEvent(VoterAnswersData.VotersAnswersRow row, DataRowAction action)
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

            public VoterAnswersData.VotersAnswersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void VotersAnswersRowChangeEventHandler(object sender, VoterAnswersData.VotersAnswersRowChangeEvent e);

        [DebuggerStepThrough]
        public class VotersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnContextUserName;
            private DataColumn columnEmail;
            private DataColumn columnIPSource;
            private DataColumn columnLanguageCode;
            private DataColumn columnProgressSaveDate;
            private DataColumn columnResumeAtPageNumber;
            private DataColumn columnResumeHighestPageNumber;
            private DataColumn columnResumeQuestionNumber;
            private DataColumn columnResumeUID;
            private DataColumn columnStartDate;
            private DataColumn columnSurveyId;
            private DataColumn columnUId;
            private DataColumn columnValidated;
            private DataColumn columnVoteDate;
            private DataColumn columnVoterId;

            public event VoterAnswersData.VotersRowChangeEventHandler VotersRowChanged;

            public event VoterAnswersData.VotersRowChangeEventHandler VotersRowChanging;

            public event VoterAnswersData.VotersRowChangeEventHandler VotersRowDeleted;

            public event VoterAnswersData.VotersRowChangeEventHandler VotersRowDeleting;

            internal VotersDataTable() : base("Voters")
            {
                this.InitClass();
            }

            internal VotersDataTable(DataTable table) : base(table.TableName)
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

            public void AddVotersRow(VoterAnswersData.VotersRow row)
            {
                base.Rows.Add(row);
            }

            public VoterAnswersData.VotersRow AddVotersRow(int SurveyId, DateTime VoteDate, string IPSource, bool Validated, DateTime StartDate, string UId, string Email, string ResumeUID, DateTime ProgressSaveDate, int ResumeAtPageNumber, int ResumeQuestionNumber, int ResumeHighestPageNumber, string ContextUserName, string LanguageCode)
            {
                VoterAnswersData.VotersRow row = (VoterAnswersData.VotersRow) base.NewRow();
                object[] objArray = new object[15];
                objArray[1] = SurveyId;
                objArray[2] = VoteDate;
                objArray[3] = IPSource;
                objArray[4] = Validated;
                objArray[5] = StartDate;
                objArray[6] = UId;
                objArray[7] = Email;
                objArray[8] = ResumeUID;
                objArray[9] = ProgressSaveDate;
                objArray[10] = ResumeAtPageNumber;
                objArray[11] = ResumeQuestionNumber;
                objArray[12] = ResumeHighestPageNumber;
                objArray[13] = ContextUserName;
                objArray[14] = LanguageCode;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                VoterAnswersData.VotersDataTable table = (VoterAnswersData.VotersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new VoterAnswersData.VotersDataTable();
            }

            public VoterAnswersData.VotersRow FindByVoterId(int VoterId)
            {
                return (VoterAnswersData.VotersRow) base.Rows.Find(new object[] { VoterId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(VoterAnswersData.VotersRow);
            }

            private void InitClass()
            {
                this.columnVoterId = new DataColumn("VoterId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnVoterId);
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnVoteDate = new DataColumn("VoteDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnVoteDate);
                this.columnIPSource = new DataColumn("IPSource", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnIPSource);
                this.columnValidated = new DataColumn("Validated", typeof(bool), null, MappingType.Element);
                base.Columns.Add(this.columnValidated);
                this.columnStartDate = new DataColumn("StartDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnStartDate);
                this.columnUId = new DataColumn("UId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUId);
                this.columnEmail = new DataColumn("Email", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnEmail);
                this.columnResumeUID = new DataColumn("ResumeUID", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnResumeUID);
                this.columnProgressSaveDate = new DataColumn("ProgressSaveDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnProgressSaveDate);
                this.columnResumeAtPageNumber = new DataColumn("ResumeAtPageNumber", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnResumeAtPageNumber);
                this.columnResumeQuestionNumber = new DataColumn("ResumeQuestionNumber", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnResumeQuestionNumber);
                this.columnResumeHighestPageNumber = new DataColumn("ResumeHighestPageNumber", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnResumeHighestPageNumber);
                this.columnContextUserName = new DataColumn("ContextUserName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnContextUserName);
                this.columnLanguageCode = new DataColumn("LanguageCode", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnLanguageCode);
                base.Constraints.Add(new UniqueConstraint("VoterAnswersDataKey1", new DataColumn[] { this.columnVoterId }, true));
                this.columnVoterId.AutoIncrement = true;
                this.columnVoterId.AutoIncrementSeed = -1L;
                this.columnVoterId.AutoIncrementStep = -1L;
                this.columnVoterId.AllowDBNull = false;
                this.columnVoterId.ReadOnly = true;
                this.columnVoterId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnVoterId = base.Columns["VoterId"];
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnVoteDate = base.Columns["VoteDate"];
                this.columnIPSource = base.Columns["IPSource"];
                this.columnValidated = base.Columns["Validated"];
                this.columnStartDate = base.Columns["StartDate"];
                this.columnUId = base.Columns["UId"];
                this.columnEmail = base.Columns["Email"];
                this.columnResumeUID = base.Columns["ResumeUID"];
                this.columnProgressSaveDate = base.Columns["ProgressSaveDate"];
                this.columnResumeAtPageNumber = base.Columns["ResumeAtPageNumber"];
                this.columnResumeQuestionNumber = base.Columns["ResumeQuestionNumber"];
                this.columnResumeHighestPageNumber = base.Columns["ResumeHighestPageNumber"];
                this.columnContextUserName = base.Columns["ContextUserName"];
                this.columnLanguageCode = base.Columns["LanguageCode"];
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new VoterAnswersData.VotersRow(builder);
            }

            public VoterAnswersData.VotersRow NewVotersRow()
            {
                return (VoterAnswersData.VotersRow) base.NewRow();
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.VotersRowChanged != null)
                {
                    this.VotersRowChanged(this, new VoterAnswersData.VotersRowChangeEvent((VoterAnswersData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.VotersRowChanging != null)
                {
                    this.VotersRowChanging(this, new VoterAnswersData.VotersRowChangeEvent((VoterAnswersData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.VotersRowDeleted != null)
                {
                    this.VotersRowDeleted(this, new VoterAnswersData.VotersRowChangeEvent((VoterAnswersData.VotersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.VotersRowDeleting != null)
                {
                    this.VotersRowDeleting(this, new VoterAnswersData.VotersRowChangeEvent((VoterAnswersData.VotersRow) e.Row, e.Action));
                }
            }

            public void RemoveVotersRow(VoterAnswersData.VotersRow row)
            {
                base.Rows.Remove(row);
            }

            internal DataColumn ContextUserNameColumn
            {
                get
                {
                    return this.columnContextUserName;
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

            internal DataColumn EmailColumn
            {
                get
                {
                    return this.columnEmail;
                }
            }

            internal DataColumn IPSourceColumn
            {
                get
                {
                    return this.columnIPSource;
                }
            }

            public VoterAnswersData.VotersRow this[int index]
            {
                get
                {
                    return (VoterAnswersData.VotersRow) base.Rows[index];
                }
            }

            internal DataColumn LanguageCodeColumn
            {
                get
                {
                    return this.columnLanguageCode;
                }
            }

            internal DataColumn ProgressSaveDateColumn
            {
                get
                {
                    return this.columnProgressSaveDate;
                }
            }

            internal DataColumn ResumeAtPageNumberColumn
            {
                get
                {
                    return this.columnResumeAtPageNumber;
                }
            }

            internal DataColumn ResumeHighestPageNumberColumn
            {
                get
                {
                    return this.columnResumeHighestPageNumber;
                }
            }

            internal DataColumn ResumeQuestionNumberColumn
            {
                get
                {
                    return this.columnResumeQuestionNumber;
                }
            }

            internal DataColumn ResumeUIDColumn
            {
                get
                {
                    return this.columnResumeUID;
                }
            }

            internal DataColumn StartDateColumn
            {
                get
                {
                    return this.columnStartDate;
                }
            }

            internal DataColumn SurveyIdColumn
            {
                get
                {
                    return this.columnSurveyId;
                }
            }

            internal DataColumn UIdColumn
            {
                get
                {
                    return this.columnUId;
                }
            }

            internal DataColumn ValidatedColumn
            {
                get
                {
                    return this.columnValidated;
                }
            }

            internal DataColumn VoteDateColumn
            {
                get
                {
                    return this.columnVoteDate;
                }
            }

            internal DataColumn VoterIdColumn
            {
                get
                {
                    return this.columnVoterId;
                }
            }
        }

        [DebuggerStepThrough]
        public class VotersRow : DataRow
        {
            private VoterAnswersData.VotersDataTable tableVoters;

            internal VotersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableVoters = (VoterAnswersData.VotersDataTable) base.Table;
            }

            public VoterAnswersData.VotersAnswersRow[] GetVotersAnswersRows()
            {
                return (VoterAnswersData.VotersAnswersRow[]) base.GetChildRows(base.Table.ChildRelations["VoterAnswersRelation"]);
            }

            public bool IsContextUserNameNull()
            {
                return base.IsNull(this.tableVoters.ContextUserNameColumn);
            }

            public bool IsEmailNull()
            {
                return base.IsNull(this.tableVoters.EmailColumn);
            }

            public bool IsIPSourceNull()
            {
                return base.IsNull(this.tableVoters.IPSourceColumn);
            }

            public bool IsLanguageCodeNull()
            {
                return base.IsNull(this.tableVoters.LanguageCodeColumn);
            }

            public bool IsProgressSaveDateNull()
            {
                return base.IsNull(this.tableVoters.ProgressSaveDateColumn);
            }

            public bool IsResumeAtPageNumberNull()
            {
                return base.IsNull(this.tableVoters.ResumeAtPageNumberColumn);
            }

            public bool IsResumeHighestPageNumberNull()
            {
                return base.IsNull(this.tableVoters.ResumeHighestPageNumberColumn);
            }

            public bool IsResumeQuestionNumberNull()
            {
                return base.IsNull(this.tableVoters.ResumeQuestionNumberColumn);
            }

            public bool IsResumeUIDNull()
            {
                return base.IsNull(this.tableVoters.ResumeUIDColumn);
            }

            public bool IsStartDateNull()
            {
                return base.IsNull(this.tableVoters.StartDateColumn);
            }

            public bool IsSurveyIdNull()
            {
                return base.IsNull(this.tableVoters.SurveyIdColumn);
            }

            public bool IsUIdNull()
            {
                return base.IsNull(this.tableVoters.UIdColumn);
            }

            public bool IsValidatedNull()
            {
                return base.IsNull(this.tableVoters.ValidatedColumn);
            }

            public bool IsVoteDateNull()
            {
                return base.IsNull(this.tableVoters.VoteDateColumn);
            }

            public void SetContextUserNameNull()
            {
                base[this.tableVoters.ContextUserNameColumn] = Convert.DBNull;
            }

            public void SetEmailNull()
            {
                base[this.tableVoters.EmailColumn] = Convert.DBNull;
            }

            public void SetIPSourceNull()
            {
                base[this.tableVoters.IPSourceColumn] = Convert.DBNull;
            }

            public void SetLanguageCodeNull()
            {
                base[this.tableVoters.LanguageCodeColumn] = Convert.DBNull;
            }

            public void SetProgressSaveDateNull()
            {
                base[this.tableVoters.ProgressSaveDateColumn] = Convert.DBNull;
            }

            public void SetResumeAtPageNumberNull()
            {
                base[this.tableVoters.ResumeAtPageNumberColumn] = Convert.DBNull;
            }

            public void SetResumeHighestPageNumberNull()
            {
                base[this.tableVoters.ResumeHighestPageNumberColumn] = Convert.DBNull;
            }

            public void SetResumeQuestionNumberNull()
            {
                base[this.tableVoters.ResumeQuestionNumberColumn] = Convert.DBNull;
            }

            public void SetResumeUIDNull()
            {
                base[this.tableVoters.ResumeUIDColumn] = Convert.DBNull;
            }

            public void SetStartDateNull()
            {
                base[this.tableVoters.StartDateColumn] = Convert.DBNull;
            }

            public void SetSurveyIdNull()
            {
                base[this.tableVoters.SurveyIdColumn] = Convert.DBNull;
            }

            public void SetUIdNull()
            {
                base[this.tableVoters.UIdColumn] = Convert.DBNull;
            }

            public void SetValidatedNull()
            {
                base[this.tableVoters.ValidatedColumn] = Convert.DBNull;
            }

            public void SetVoteDateNull()
            {
                base[this.tableVoters.VoteDateColumn] = Convert.DBNull;
            }

            public string ContextUserName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableVoters.ContextUserNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableVoters.ContextUserNameColumn] = value;
                }
            }

            public string Email
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableVoters.EmailColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableVoters.EmailColumn] = value;
                }
            }

            public string IPSource
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableVoters.IPSourceColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableVoters.IPSourceColumn] = value;
                }
            }

            public string LanguageCode
            {
                get
                {
                    if (this.IsLanguageCodeNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableVoters.LanguageCodeColumn];
                }
                set
                {
                    base[this.tableVoters.LanguageCodeColumn] = value;
                }
            }

            public DateTime ProgressSaveDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableVoters.ProgressSaveDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableVoters.ProgressSaveDateColumn] = value;
                }
            }

            public int ResumeAtPageNumber
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableVoters.ResumeAtPageNumberColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableVoters.ResumeAtPageNumberColumn] = value;
                }
            }

            public int ResumeHighestPageNumber
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableVoters.ResumeHighestPageNumberColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableVoters.ResumeHighestPageNumberColumn] = value;
                }
            }

            public int ResumeQuestionNumber
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableVoters.ResumeQuestionNumberColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableVoters.ResumeQuestionNumberColumn] = value;
                }
            }

            public string ResumeUID
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableVoters.ResumeUIDColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableVoters.ResumeUIDColumn] = value;
                }
            }

            public DateTime StartDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableVoters.StartDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableVoters.StartDateColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableVoters.SurveyIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableVoters.SurveyIdColumn] = value;
                }
            }

            public string UId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableVoters.UIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableVoters.UIdColumn] = value;
                }
            }

            public bool Validated
            {
                get
                {
                    bool flag;
                    try
                    {
                        flag = (bool) base[this.tableVoters.ValidatedColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return flag;
                }
                set
                {
                    base[this.tableVoters.ValidatedColumn] = value;
                }
            }

            public DateTime VoteDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableVoters.VoteDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableVoters.VoteDateColumn] = value;
                }
            }

            public int VoterId
            {
                get
                {
                    return (int) base[this.tableVoters.VoterIdColumn];
                }
                set
                {
                    base[this.tableVoters.VoterIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class VotersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private VoterAnswersData.VotersRow eventRow;

            public VotersRowChangeEvent(VoterAnswersData.VotersRow row, DataRowAction action)
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

            public VoterAnswersData.VotersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void VotersRowChangeEventHandler(object sender, VoterAnswersData.VotersRowChangeEvent e);
    }
}

