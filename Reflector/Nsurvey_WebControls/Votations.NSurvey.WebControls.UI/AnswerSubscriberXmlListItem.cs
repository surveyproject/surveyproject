namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.Helpers;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Subscriber Xml type, this types can be used to subscribe 
    /// to another type that publish answer and use this answer 
    /// as an input file to populate the dropdownlist
    /// </summary>
    public class AnswerSubscriberXmlListItem : AnswerItem, IAnswerPublisher, IAnswerSubscriber, IMandatoryAnswer
    {
        private bool _mandatory = false;
        protected DropDownList _xmlAnswerDropDownList;

        /// <summary>
        /// Binds dropdownlist with the xml answers loaded 
        /// from the publisher xml file value
        /// </summary>
        private void BindDropDownList(NSurveyDataSource xmlAnswers)
        {
            this._xmlAnswerDropDownList.DataSource = xmlAnswers;
            this._xmlAnswerDropDownList.DataMember = "XmlAnswer";
            this._xmlAnswerDropDownList.DataValueField = "AnswerValue";
            this._xmlAnswerDropDownList.DataTextField = "AnswerDescription";
            this._xmlAnswerDropDownList.EnableViewState = false;
            this._xmlAnswerDropDownList.DataBind();
        }

        protected override void CreateChildControls()
        {
            if (this.XmlFileName != null)
            {
                this.GenerateDropDownList();
            }
        }

        /// <summary>
        /// Builds and populates the dropdown list with the current 
        /// Xml filename gathered from the answer publisher
        /// </summary>
        protected virtual void GenerateDropDownList()
        {
            this.Controls.Clear();
            this._xmlAnswerDropDownList = new DropDownList();
            NSurveyDataSource xmlAnswers = new XmlFileManager(this.XmlFileName).GetXmlAnswers(base.LanguageCode);
            if ((xmlAnswers != null) && (xmlAnswers.XmlAnswer.Count > 0))
            {
                if (((xmlAnswers.XmlDataSource.Rows.Count > 0) && (xmlAnswers.XmlDataSource[0].RunTimeAnswerLabel != null)) && (xmlAnswers.XmlDataSource[0].RunTimeAnswerLabel.Length > 0))
                {
                    this.Text = xmlAnswers.XmlDataSource[0].RunTimeAnswerLabel;
                }
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
                      //JJ  this.Controls.Add(new LiteralControl(string.Format("{0}<br />", this.Text)));
                        Label label = new Label();
                        label.Text = string.Format("{0}<br />", this.Text);
                        label.CssClass = "AnswerTextRender";
                        this.Controls.Add(label);
                    }
                }
                this.BindDropDownList(xmlAnswers);
                if ((this.DefaultText != null) && (this._xmlAnswerDropDownList.Items.FindByValue(this.DefaultText) != null))
                {
                    this._xmlAnswerDropDownList.SelectedValue = this.DefaultText;
                }
                if (this.HasSubscribers)
                {
                    this._xmlAnswerDropDownList.AutoPostBack = true;
                }
                this.Controls.Add(this._xmlAnswerDropDownList);
                PostedAnswerDataCollection answerValues = this.GetAnswerValues();
                if (answerValues != null)
                {
                    this.OnAnswerPublisherCreated(new AnswerItemEventArgs(answerValues));
                }
            }
        }

        /// <summary>
        /// Retrieves the available postback values
        /// </summary>
        /// <returns></returns>
        protected PostedAnswerDataCollection GetAnswerValues()
        {
            PostedAnswerDataCollection datas = null;
            if ((this._xmlAnswerDropDownList != null) && (this._xmlAnswerDropDownList.Items.Count > 0))
            {
                datas = new PostedAnswerDataCollection();
                if (this._xmlAnswerDropDownList.SelectedValue.Length > 0)
                {
                    datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this._xmlAnswerDropDownList.SelectedValue, AnswerTypeMode.Mandatory | AnswerTypeMode.Publisher | AnswerTypeMode.DataSource));
                }
            }
            return datas;
        }

        /// <summary>
        /// Retur	ns the selected value of the dropdown to the subscribers
        /// </summary>
        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            PostedAnswerDataCollection answerValues = this.GetAnswerValues();
            if ((this.Mandatory && (answerValues != null)) && (answerValues.Count == 0))
            {
                this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(ResourceManager.GetString("XMLAnswerSelectionRequired", base.LanguageCode), this.Text)));
            }
            this.OnAnswerPublished(new AnswerItemEventArgs(answerValues));
            return answerValues;
        }

        /// <summary>
        /// Gets the answer send by the publisher and use
        /// them as the source name of the xml file to load
        /// </summary>
        public void ProcessPublishedAnswers(object sender, AnswerItemEventArgs e)
        {
            if (((e != null) && (e.PostedAnswers != null)) && (e.PostedAnswers.Count > 0))
            {
                string str = e.PostedAnswers[0].FieldText.ToLower() + ".xml";
                if (this.XmlFileName != str)
                {
                    this.XmlFileName = str;
                    this.GenerateDropDownList();
                    base.ChildControlsCreated = true;
                    PostedAnswerDataCollection answerValues = this.GetAnswerValues();
                    this.OnAnswerPublished(new AnswerItemEventArgs(answerValues));
                }
            }
            else
            {
                this.XmlFileName = string.Empty;
                this._xmlAnswerDropDownList = null;
                this.Controls.Clear();
                this.OnAnswerPublished(new AnswerItemEventArgs(null));
            }
        }

        /// <summary>
        /// Trigger event when a publisher has been created, usually 
        /// at the end of a CreateChildControl call. This call is 
        /// mandatory to catch Answer items that have set default values 
        /// </summary>
        public void PublisherCreation(object sender, AnswerItemEventArgs e)
        {
            if (((e != null) && (e.PostedAnswers != null)) && (e.PostedAnswers.Count > 0))
            {
                string str = e.PostedAnswers[0].FieldText.ToLower() + ".xml";
                if (this.XmlFileName == null)
                {
                    this.XmlFileName = str;
                    if (base.ChildControlsCreated)
                    {
                        this.GenerateDropDownList();
                    }
                }
                else if ((this.XmlFileName != str) && !base.ChildControlsCreated)
                {
                    string str2 = (((AnswerItem) sender).DefaultText == null) ? null : (((AnswerItem) sender).DefaultText.ToLower() + ".xml");
                    if (((str2 == null) || (str2.Length == 0)) || (this.XmlFileName == str2))
                    {
                        this.XmlFileName = str;
                        this.GenerateDropDownList();
                        base.ChildControlsCreated = true;
                    }
                }
            }
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

        /// <summary>
        /// Name of the xml file to load.
        /// </summary>
        /// <remarks>
        /// As ProcessPublishedAnswers is called
        /// during postback we have to keep the name in the viewstate
        /// in order to bind the dropdownlist with the correct
        /// data to retrieve its selected value when the user
        /// does a postback.
        /// </remarks>
        public string XmlFileName
        {
            get
            {
                if (this.ViewState["SubscriberXmlFileName"] != null)
                {
                    return this.ViewState["SubscriberXmlFileName"].ToString();
                }
                return null;
            }
            set
            {
                this.ViewState["SubscriberXmlFileName"] = value;
            }
        }
    }
}

