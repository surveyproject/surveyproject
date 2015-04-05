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

    [Serializable, DesignerCategory("code"), DebuggerStepThrough, ToolboxItem(true)]
    public class QuestionSectionOptionData : DataSet
    {
        private QuestionSectionOptionsDataTable tableQuestionSectionOptions;

        public QuestionSectionOptionData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected QuestionSectionOptionData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["QuestionSectionOptions"] != null)
                {
                    base.Tables.Add(new QuestionSectionOptionsDataTable(dataSet.Tables["QuestionSectionOptions"]));
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
            QuestionSectionOptionData data = (QuestionSectionOptionData) base.Clone();
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
            base.DataSetName = "QuestionSectionOptionData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/QuestionSectionOptionData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableQuestionSectionOptions = new QuestionSectionOptionsDataTable();
            base.Tables.Add(this.tableQuestionSectionOptions);
        }

        internal void InitVars()
        {
            this.tableQuestionSectionOptions = (QuestionSectionOptionsDataTable) base.Tables["QuestionSectionOptions"];
            if (this.tableQuestionSectionOptions != null)
            {
                this.tableQuestionSectionOptions.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["QuestionSectionOptions"] != null)
            {
                base.Tables.Add(new QuestionSectionOptionsDataTable(dataSet.Tables["QuestionSectionOptions"]));
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

        private bool ShouldSerializeQuestionSectionOptions()
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
        public QuestionSectionOptionsDataTable QuestionSectionOptions
        {
            get
            {
                return this.tableQuestionSectionOptions;
            }
        }

        [DebuggerStepThrough]
        public class QuestionSectionOptionsDataTable : DataTable, IEnumerable
        {
            private DataColumn columnAddSectionLinkText;
            private DataColumn columnDeleteSectionLinkText;
            private DataColumn columnEditSectionLinkText;
            private DataColumn columnMaxSections;
            private DataColumn columnQuestionId;
            private DataColumn columnRepeatableSectionModeId;
            private DataColumn columnUpdateSectionLinkText;

            public event QuestionSectionOptionData.QuestionSectionOptionsRowChangeEventHandler QuestionSectionOptionsRowChanged;

            public event QuestionSectionOptionData.QuestionSectionOptionsRowChangeEventHandler QuestionSectionOptionsRowChanging;

            public event QuestionSectionOptionData.QuestionSectionOptionsRowChangeEventHandler QuestionSectionOptionsRowDeleted;

            public event QuestionSectionOptionData.QuestionSectionOptionsRowChangeEventHandler QuestionSectionOptionsRowDeleting;

            internal QuestionSectionOptionsDataTable() : base("QuestionSectionOptions")
            {
                this.InitClass();
            }

            internal QuestionSectionOptionsDataTable(DataTable table) : base(table.TableName)
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

            public void AddQuestionSectionOptionsRow(QuestionSectionOptionData.QuestionSectionOptionsRow row)
            {
                base.Rows.Add(row);
            }

            public QuestionSectionOptionData.QuestionSectionOptionsRow AddQuestionSectionOptionsRow(int QuestionId, int RepeatableSectionModeId, string AddSectionLinkText, string DeleteSectionLinkText, int MaxSections, string EditSectionLinkText, string UpdateSectionLinkText)
            {
                QuestionSectionOptionData.QuestionSectionOptionsRow row = (QuestionSectionOptionData.QuestionSectionOptionsRow) base.NewRow();
                row.ItemArray = new object[] { QuestionId, RepeatableSectionModeId, AddSectionLinkText, DeleteSectionLinkText, MaxSections, EditSectionLinkText, UpdateSectionLinkText };
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                QuestionSectionOptionData.QuestionSectionOptionsDataTable table = (QuestionSectionOptionData.QuestionSectionOptionsDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new QuestionSectionOptionData.QuestionSectionOptionsDataTable();
            }

            public QuestionSectionOptionData.QuestionSectionOptionsRow FindByQuestionId(int QuestionId)
            {
                return (QuestionSectionOptionData.QuestionSectionOptionsRow) base.Rows.Find(new object[] { QuestionId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(QuestionSectionOptionData.QuestionSectionOptionsRow);
            }

            private void InitClass()
            {
                this.columnQuestionId = new DataColumn("QuestionId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnQuestionId);
                this.columnRepeatableSectionModeId = new DataColumn("RepeatableSectionModeId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnRepeatableSectionModeId);
                this.columnAddSectionLinkText = new DataColumn("AddSectionLinkText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnAddSectionLinkText);
                this.columnDeleteSectionLinkText = new DataColumn("DeleteSectionLinkText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDeleteSectionLinkText);
                this.columnMaxSections = new DataColumn("MaxSections", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnMaxSections);
                this.columnEditSectionLinkText = new DataColumn("EditSectionLinkText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnEditSectionLinkText);
                this.columnUpdateSectionLinkText = new DataColumn("UpdateSectionLinkText", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUpdateSectionLinkText);
                base.Constraints.Add(new UniqueConstraint("QuestionSectionOptionDataKey1", new DataColumn[] { this.columnQuestionId }, true));
                this.columnQuestionId.AllowDBNull = false;
                this.columnQuestionId.Unique = true;
                this.columnRepeatableSectionModeId.AllowDBNull = false;
            }

            internal void InitVars()
            {
                this.columnQuestionId = base.Columns["QuestionId"];
                this.columnRepeatableSectionModeId = base.Columns["RepeatableSectionModeId"];
                this.columnAddSectionLinkText = base.Columns["AddSectionLinkText"];
                this.columnDeleteSectionLinkText = base.Columns["DeleteSectionLinkText"];
                this.columnMaxSections = base.Columns["MaxSections"];
                this.columnEditSectionLinkText = base.Columns["EditSectionLinkText"];
                this.columnUpdateSectionLinkText = base.Columns["UpdateSectionLinkText"];
            }

            public QuestionSectionOptionData.QuestionSectionOptionsRow NewQuestionSectionOptionsRow()
            {
                return (QuestionSectionOptionData.QuestionSectionOptionsRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new QuestionSectionOptionData.QuestionSectionOptionsRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.QuestionSectionOptionsRowChanged != null)
                {
                    this.QuestionSectionOptionsRowChanged(this, new QuestionSectionOptionData.QuestionSectionOptionsRowChangeEvent((QuestionSectionOptionData.QuestionSectionOptionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.QuestionSectionOptionsRowChanging != null)
                {
                    this.QuestionSectionOptionsRowChanging(this, new QuestionSectionOptionData.QuestionSectionOptionsRowChangeEvent((QuestionSectionOptionData.QuestionSectionOptionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.QuestionSectionOptionsRowDeleted != null)
                {
                    this.QuestionSectionOptionsRowDeleted(this, new QuestionSectionOptionData.QuestionSectionOptionsRowChangeEvent((QuestionSectionOptionData.QuestionSectionOptionsRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.QuestionSectionOptionsRowDeleting != null)
                {
                    this.QuestionSectionOptionsRowDeleting(this, new QuestionSectionOptionData.QuestionSectionOptionsRowChangeEvent((QuestionSectionOptionData.QuestionSectionOptionsRow) e.Row, e.Action));
                }
            }

            public void RemoveQuestionSectionOptionsRow(QuestionSectionOptionData.QuestionSectionOptionsRow row)
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

            public QuestionSectionOptionData.QuestionSectionOptionsRow this[int index]
            {
                get
                {
                    return (QuestionSectionOptionData.QuestionSectionOptionsRow) base.Rows[index];
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
        public class QuestionSectionOptionsRow : DataRow
        {
            private QuestionSectionOptionData.QuestionSectionOptionsDataTable tableQuestionSectionOptions;

            internal QuestionSectionOptionsRow(DataRowBuilder rb) : base(rb)
            {
                this.tableQuestionSectionOptions = (QuestionSectionOptionData.QuestionSectionOptionsDataTable) base.Table;
            }

            public bool IsAddSectionLinkTextNull()
            {
                return base.IsNull(this.tableQuestionSectionOptions.AddSectionLinkTextColumn);
            }

            public bool IsDeleteSectionLinkTextNull()
            {
                return base.IsNull(this.tableQuestionSectionOptions.DeleteSectionLinkTextColumn);
            }

            public bool IsEditSectionLinkTextNull()
            {
                return base.IsNull(this.tableQuestionSectionOptions.EditSectionLinkTextColumn);
            }

            public bool IsMaxSectionsNull()
            {
                return base.IsNull(this.tableQuestionSectionOptions.MaxSectionsColumn);
            }

            public bool IsUpdateSectionLinkTextNull()
            {
                return base.IsNull(this.tableQuestionSectionOptions.UpdateSectionLinkTextColumn);
            }

            public void SetAddSectionLinkTextNull()
            {
                base[this.tableQuestionSectionOptions.AddSectionLinkTextColumn] = Convert.DBNull;
            }

            public void SetDeleteSectionLinkTextNull()
            {
                base[this.tableQuestionSectionOptions.DeleteSectionLinkTextColumn] = Convert.DBNull;
            }

            public void SetEditSectionLinkTextNull()
            {
                base[this.tableQuestionSectionOptions.EditSectionLinkTextColumn] = Convert.DBNull;
            }

            public void SetMaxSectionsNull()
            {
                base[this.tableQuestionSectionOptions.MaxSectionsColumn] = Convert.DBNull;
            }

            public void SetUpdateSectionLinkTextNull()
            {
                base[this.tableQuestionSectionOptions.UpdateSectionLinkTextColumn] = Convert.DBNull;
            }

            public string AddSectionLinkText
            {
                get
                {
                    if (this.IsAddSectionLinkTextNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestionSectionOptions.AddSectionLinkTextColumn];
                }
                set
                {
                    base[this.tableQuestionSectionOptions.AddSectionLinkTextColumn] = value;
                }
            }

            public string DeleteSectionLinkText
            {
                get
                {
                    if (this.IsDeleteSectionLinkTextNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestionSectionOptions.DeleteSectionLinkTextColumn];
                }
                set
                {
                    base[this.tableQuestionSectionOptions.DeleteSectionLinkTextColumn] = value;
                }
            }

            public string EditSectionLinkText
            {
                get
                {
                    if (this.IsEditSectionLinkTextNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestionSectionOptions.EditSectionLinkTextColumn];
                }
                set
                {
                    base[this.tableQuestionSectionOptions.EditSectionLinkTextColumn] = value;
                }
            }

            public int MaxSections
            {
                get
                {
                    if (this.IsMaxSectionsNull())
                    {
                        return 0;
                    }
                    return (int) base[this.tableQuestionSectionOptions.MaxSectionsColumn];
                }
                set
                {
                    base[this.tableQuestionSectionOptions.MaxSectionsColumn] = value;
                }
            }

            public int QuestionId
            {
                get
                {
                    return (int) base[this.tableQuestionSectionOptions.QuestionIdColumn];
                }
                set
                {
                    base[this.tableQuestionSectionOptions.QuestionIdColumn] = value;
                }
            }

            public int RepeatableSectionModeId
            {
                get
                {
                    return (int) base[this.tableQuestionSectionOptions.RepeatableSectionModeIdColumn];
                }
                set
                {
                    base[this.tableQuestionSectionOptions.RepeatableSectionModeIdColumn] = value;
                }
            }

            public string UpdateSectionLinkText
            {
                get
                {
                    if (this.IsUpdateSectionLinkTextNull())
                    {
                        return null;
                    }
                    return (string) base[this.tableQuestionSectionOptions.UpdateSectionLinkTextColumn];
                }
                set
                {
                    base[this.tableQuestionSectionOptions.UpdateSectionLinkTextColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class QuestionSectionOptionsRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private QuestionSectionOptionData.QuestionSectionOptionsRow eventRow;

            public QuestionSectionOptionsRowChangeEvent(QuestionSectionOptionData.QuestionSectionOptionsRow row, DataRowAction action)
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

            public QuestionSectionOptionData.QuestionSectionOptionsRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void QuestionSectionOptionsRowChangeEventHandler(object sender, QuestionSectionOptionData.QuestionSectionOptionsRowChangeEvent e);
    }
}

