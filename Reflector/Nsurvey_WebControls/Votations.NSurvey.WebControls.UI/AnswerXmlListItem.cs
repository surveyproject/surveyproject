namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Xml based dropdown list answer type that
    /// feed a dropdownlist with the xml loaded by 
    /// the parent class 
    /// </summary>
    public class AnswerXmlListItem : AnswerXmlItem, IAnswerPublisher, IMandatoryAnswer
    {
        private bool _mandatory = false;
        protected DropDownList _xmlAnswerDropDownList;

        /// <summary>
        /// Implemented from the abstract method of the parent class
        /// Is called to generate the "layout" and to place the child 
        /// controls (eg:dropdownlist) in the tree
        /// </summary>
        /// <param name="xmlAnswers">the xml items loaded from the xml datasource file</param>
        protected override void GenerateXmlControl(NSurveyDataSource xmlAnswers)
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
                    label.Text = string.Format("{0}", this.Text);
                    label.CssClass = "AnswerTextRender";
                    this.Controls.Add(label);

                    if (this.Mandatory)
                    {
                        this.Controls.Add(new LiteralControl("<div style='color:red; display:inline-flex;'>&nbsp; * &nbsp;</div>"));
                    }
                    this.Controls.Add(new LiteralControl("<br />"));
                }
            }
            this._xmlAnswerDropDownList = new DropDownList();
            if (xmlAnswers != null)
            {
                this._xmlAnswerDropDownList.DataSource = xmlAnswers;
                this._xmlAnswerDropDownList.DataMember = "XmlAnswer";
                this._xmlAnswerDropDownList.DataValueField = "AnswerValue";
                this._xmlAnswerDropDownList.DataTextField = "AnswerDescription";
                this._xmlAnswerDropDownList.EnableViewState = false;
                this._xmlAnswerDropDownList.DataBind();
                if ((this.DefaultText != null) && (this._xmlAnswerDropDownList.Items.FindByValue(this.DefaultText) != null))
                {
                    this._xmlAnswerDropDownList.SelectedValue = this.DefaultText;
                }
                if (this.HasSubscribers)
                {
                    this._xmlAnswerDropDownList.AutoPostBack = true;
                }
            }
            this.Controls.Add(this._xmlAnswerDropDownList);
            this.OnAnswerPublisherCreated(new AnswerItemEventArgs(this.GetUserAnswers()));
        }

        /// <summary>
        /// Returns the selected value of the dropdown to the subscribers
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            if (this.Mandatory && (this._xmlAnswerDropDownList.SelectedValue.Length == 0))
            {
                this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(ResourceManager.GetString("XMLAnswerSelectionRequired", base.LanguageCode), this.Text)));
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
            PostedAnswerDataCollection datas = null;
            if (this._xmlAnswerDropDownList.SelectedValue.Length != 0)
            {
                datas = new PostedAnswerDataCollection();
                datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._xmlAnswerDropDownList.SelectedValue, AnswerTypeMode.Mandatory | AnswerTypeMode.Publisher | AnswerTypeMode.DataSource));
            }
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

