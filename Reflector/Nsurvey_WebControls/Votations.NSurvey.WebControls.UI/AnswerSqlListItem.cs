namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Xml based dropdown list answer type that
    /// feed a dropdownlist with the xml loaded by 
    /// the parent class 
    /// </summary>
    public class AnswerSqlListItem : AnswerSqlItem, IAnswerPublisher, IMandatoryAnswer
    {
        private bool _mandatory = false;
        protected DropDownList _sqlAnswerDropDownList;

        /// <summary>
        /// Implemented from the abstract method of the parent class
        /// Is called to generate the "layout" and to place the child 
        /// controls (eg:dropdownlist) in the tree
        /// </summary>
        /// <param name="sqlResults">a dataset containing the results to process</param>
        protected override void GenerateSqlControl(DataSet sqlResults)
        {
            if (this.ShowAnswerText)
            {
                if ((this.ImageUrl != null) && (this.ImageUrl.Length != 0))
                {
                    Image child = new Image();
                    child.ImageUrl = this.ImageUrl;
                    child.ImageAlign = ImageAlign.Middle;
                    child.ToolTip = this.Text;
                    this.Controls.Add(child);
                    this.Controls.Add(new LiteralControl("<br />"));
                }
                else if ((this.Text != null) && (this.Text.Length != 0))
                {
                   //JJ this.Controls.Add(new LiteralControl(string.Format("{0}<br />", this.Text)));
                    Label label = new Label();
                    label.Text = string.Format("{0}<br />", this.Text);
                    label.CssClass = "AnswerTextRender";
                    this.Controls.Add(label);
                }
            }
            this._sqlAnswerDropDownList = new DropDownList();
            if (((sqlResults.Tables.Count > 0) && (sqlResults.Tables[0].Rows.Count > 0)) && (sqlResults.Tables[0].Columns.Count > 0))
            {
                string tableName = sqlResults.Tables[0].TableName;
                string columnName = sqlResults.Tables[0].Columns[0].ColumnName;
                string str3 = null;
                if (sqlResults.Tables[0].Columns.Count > 1)
                {
                    str3 = sqlResults.Tables[0].Columns[1].ColumnName;
                }
                this._sqlAnswerDropDownList.DataSource = sqlResults;
                this._sqlAnswerDropDownList.DataMember = tableName;
                this._sqlAnswerDropDownList.DataValueField = columnName;
                if (str3 != null)
                {
                    this._sqlAnswerDropDownList.DataTextField = str3;
                }
                else
                {
                    this._sqlAnswerDropDownList.DataTextField = columnName;
                }
                this._sqlAnswerDropDownList.EnableViewState = false;
                this._sqlAnswerDropDownList.DataBind();
                if ((this.DefaultText != null) && (this._sqlAnswerDropDownList.Items.FindByValue(this.DefaultText) != null))
                {
                    this._sqlAnswerDropDownList.SelectedValue = this.DefaultText;
                }
                if (this.HasSubscribers)
                {
                    this._sqlAnswerDropDownList.AutoPostBack = true;
                }
            }
            this._sqlAnswerDropDownList.Items.Insert(0, new ListItem(ResourceManager.GetString("ListSelection", base.LanguageCode), ""));
            this.Controls.Add(this._sqlAnswerDropDownList);
            this.OnAnswerPublisherCreated(new AnswerItemEventArgs(this.GetUserAnswers()));
        }

        /// <summary>
        /// Returns the selected value of the dropdown to the subscribers
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            if (this.Mandatory && (this._sqlAnswerDropDownList.SelectedValue.Length == 0))
            {
                this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(ResourceManager.GetString("SqlAnswerSelectionRequired", base.LanguageCode), this.Text)));
            }
            PostedAnswerDataCollection userAnswers = this.GetUserAnswers();
            this.OnAnswerPublished(new AnswerItemEventArgs(userAnswers));
            return userAnswers;
        }

        /// <summary>
        /// Returns the answeritem user's answers
        /// </summary>
        protected virtual PostedAnswerDataCollection GetUserAnswers()
        {
            PostedAnswerDataCollection datas = new PostedAnswerDataCollection();
            if (this._sqlAnswerDropDownList.SelectedValue.Length == 0)
            {
                return null;
            }
            datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._sqlAnswerDropDownList.SelectedValue, AnswerTypeMode.Mandatory | AnswerTypeMode.Publisher | AnswerTypeMode.DataSource));
            return datas;
        }

        public virtual bool Mandatory
        {
            get
            {
                return this._mandatory;
            }
            set
            {
                this._mandatory = value;
            }
        }
    }
}

