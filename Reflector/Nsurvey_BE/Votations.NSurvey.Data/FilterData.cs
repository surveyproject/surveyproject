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
    public class FilterData : DataSet
    {
        private FiltersDataTable tableFilters;

        public FilterData()
        {
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
        }

        protected FilterData(SerializationInfo info, StreamingContext context)
        {
            string s = (string) info.GetValue("XmlSchema", typeof(string));
            if (s != null)
            {
                DataSet dataSet = new DataSet();
                dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                if (dataSet.Tables["Filters"] != null)
                {
                    base.Tables.Add(new FiltersDataTable(dataSet.Tables["Filters"]));
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
            FilterData data = (FilterData) base.Clone();
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
            base.DataSetName = "FilterData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/FilterData.xsd";
            base.Locale = new CultureInfo("en-US");
            base.CaseSensitive = false;
            base.EnforceConstraints = true;
            this.tableFilters = new FiltersDataTable();
            base.Tables.Add(this.tableFilters);
        }

        internal void InitVars()
        {
            this.tableFilters = (FiltersDataTable) base.Tables["Filters"];
            if (this.tableFilters != null)
            {
                this.tableFilters.InitVars();
            }
        }

        protected override void ReadXmlSerializable(XmlReader reader)
        {
            this.Reset();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(reader);
            if (dataSet.Tables["Filters"] != null)
            {
                base.Tables.Add(new FiltersDataTable(dataSet.Tables["Filters"]));
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

        private bool ShouldSerializeFilters()
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
        public FiltersDataTable Filters
        {
            get
            {
                return this.tableFilters;
            }
        }

        [DebuggerStepThrough]
        public class FiltersDataTable : DataTable, IEnumerable
        {
            private DataColumn columnDescription;
            private DataColumn columnFilterId;
            private DataColumn columnLogicalOperatorTypeID;
            private DataColumn columnSurveyId;
            private DataColumn columnParentFilterId;

            public event FilterData.FiltersRowChangeEventHandler FiltersRowChanged;

            public event FilterData.FiltersRowChangeEventHandler FiltersRowChanging;

            public event FilterData.FiltersRowChangeEventHandler FiltersRowDeleted;

            public event FilterData.FiltersRowChangeEventHandler FiltersRowDeleting;

            internal FiltersDataTable() : base("Filters")
            {
                this.InitClass();
            }

            internal FiltersDataTable(DataTable table) : base(table.TableName)
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

            public void AddFiltersRow(FilterData.FiltersRow row)
            {
                base.Rows.Add(row);
            }

            public FilterData.FiltersRow AddFiltersRow(int SurveyId, string Description, short LogicalOperatorTypeID, int ParentFilterId)
            {
                FilterData.FiltersRow row = (FilterData.FiltersRow) base.NewRow();
                object[] objArray = new object[5];
                objArray[1] = SurveyId;
                objArray[2] = Description;
                objArray[3] = LogicalOperatorTypeID;
                objArray[4] = ParentFilterId;
                row.ItemArray = objArray;
                base.Rows.Add(row);
                return row;
            }

            public override DataTable Clone()
            {
                FilterData.FiltersDataTable table = (FilterData.FiltersDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            protected override DataTable CreateInstance()
            {
                return new FilterData.FiltersDataTable();
            }

            public FilterData.FiltersRow FindByFilterId(int FilterId)
            {
                return (FilterData.FiltersRow) base.Rows.Find(new object[] { FilterId });
            }

            public IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            protected override System.Type GetRowType()
            {
                return typeof(FilterData.FiltersRow);
            }

            private void InitClass()
            {
                this.columnFilterId = new DataColumn("FilterId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnFilterId);
                this.columnSurveyId = new DataColumn("SurveyId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSurveyId);
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
                this.columnLogicalOperatorTypeID = new DataColumn("LogicalOperatorTypeID", typeof(short), null, MappingType.Element);
                base.Columns.Add(this.columnLogicalOperatorTypeID);
                this.columnParentFilterId = new DataColumn("ParentFilterId", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnParentFilterId);
                base.Constraints.Add(new UniqueConstraint("FilterDataKey1", new DataColumn[] { this.columnFilterId }, true));
                this.columnFilterId.AutoIncrement = true;
                this.columnFilterId.AllowDBNull = false;
                this.columnFilterId.ReadOnly = true;
                this.columnFilterId.Unique = true;
            }

            internal void InitVars()
            {
                this.columnFilterId = base.Columns["FilterId"];
                this.columnSurveyId = base.Columns["SurveyId"];
                this.columnDescription = base.Columns["Description"];
                this.columnLogicalOperatorTypeID = base.Columns["LogicalOperatorTypeID"];
                this.columnParentFilterId = base.Columns["ParentFilterId"];
            }

            public FilterData.FiltersRow NewFiltersRow()
            {
                return (FilterData.FiltersRow) base.NewRow();
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new FilterData.FiltersRow(builder);
            }

            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.FiltersRowChanged != null)
                {
                    this.FiltersRowChanged(this, new FilterData.FiltersRowChangeEvent((FilterData.FiltersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.FiltersRowChanging != null)
                {
                    this.FiltersRowChanging(this, new FilterData.FiltersRowChangeEvent((FilterData.FiltersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.FiltersRowDeleted != null)
                {
                    this.FiltersRowDeleted(this, new FilterData.FiltersRowChangeEvent((FilterData.FiltersRow) e.Row, e.Action));
                }
            }

            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.FiltersRowDeleting != null)
                {
                    this.FiltersRowDeleting(this, new FilterData.FiltersRowChangeEvent((FilterData.FiltersRow) e.Row, e.Action));
                }
            }

            public void RemoveFiltersRow(FilterData.FiltersRow row)
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

            internal DataColumn DescriptionColumn
            {
                get
                {
                    return this.columnDescription;
                }
            }

            internal DataColumn FilterIdColumn
            {
                get
                {
                    return this.columnFilterId;
                }
            }

            public FilterData.FiltersRow this[int index]
            {
                get
                {
                    return (FilterData.FiltersRow) base.Rows[index];
                }
            }

            internal DataColumn LogicalOperatorTypeIDColumn
            {
                get
                {
                    return this.columnLogicalOperatorTypeID;
                }
            }

            internal DataColumn SurveyIdColumn
            {
                get
                {
                    return this.columnSurveyId;
                }
            }
            internal DataColumn ParentFilterIdColumn {
                get 
                {
                    return this.columnParentFilterId;
                }
            }
        }

        [DebuggerStepThrough]
        public class FiltersRow : DataRow
        {
            private FilterData.FiltersDataTable tableFilters;

            internal FiltersRow(DataRowBuilder rb) : base(rb)
            {
                this.tableFilters = (FilterData.FiltersDataTable) base.Table;
            }

            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableFilters.DescriptionColumn);
            }

            public bool IsLogicalOperatorTypeIDNull()
            {
                return base.IsNull(this.tableFilters.LogicalOperatorTypeIDColumn);
            }

            public bool IsSurveyIdNull()
            {
                return base.IsNull(this.tableFilters.SurveyIdColumn);
            }

            public void SetDescriptionNull()
            {
                base[this.tableFilters.DescriptionColumn] = Convert.DBNull;
            }

            public void SetLogicalOperatorTypeIDNull()
            {
                base[this.tableFilters.LogicalOperatorTypeIDColumn] = Convert.DBNull;
            }

            public void SetSurveyIdNull()
            {
                base[this.tableFilters.SurveyIdColumn] = Convert.DBNull;
            }

            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableFilters.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableFilters.DescriptionColumn] = value;
                }
            }

            public int FilterId
            {
                get
                {
                    return (int) base[this.tableFilters.FilterIdColumn];
                }
                set
                {
                    base[this.tableFilters.FilterIdColumn] = value;
                }
            }

            public int ParentFilterId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int)base[this.tableFilters.ParentFilterIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        num = 0;
                    }
                    return num;
                }
                set
                {
                    base[this.tableFilters.ParentFilterIdColumn] = value;
                }
            }

            public short LogicalOperatorTypeID
            {
                get
                {
                    short num;
                    try
                    {
                        num = (short) base[this.tableFilters.LogicalOperatorTypeIDColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableFilters.LogicalOperatorTypeIDColumn] = value;
                }
            }

            public int SurveyId
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableFilters.SurveyIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableFilters.SurveyIdColumn] = value;
                }
            }
        }

        [DebuggerStepThrough]
        public class FiltersRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private FilterData.FiltersRow eventRow;

            public FiltersRowChangeEvent(FilterData.FiltersRow row, DataRowAction action)
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

            public FilterData.FiltersRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void FiltersRowChangeEventHandler(object sender, FilterData.FiltersRowChangeEvent e);
    }
}

