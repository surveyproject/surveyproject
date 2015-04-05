namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// This radio button is much like the built in <see cref="T:System.Web.UI.WebControls.RadioButton" /> control, but unlike it, has a <see cref="P:Votations.NSurvey.WebControls.UI.GlobalRadioButton.GroupName" /> that can be global, ignoring naming containers.
    /// </summary>
    /// <remarks>
    /// Be <b>very careful</b> when using this control, 
    /// as it is quite easy to break other controls on the page 
    /// because of the disrespect for naming containers in the "name" attribute of the rendered html input.
    /// </remarks>
    [DefaultProperty("Text"), ToolboxItem(false), DefaultEvent("CheckedChanged"), Description("A RadioButton with a global GroupName")]
    public class GlobalRadioButton : WebControl, IPostBackDataHandler
    {
        private bool _checkChanged;
        private static readonly object EventCheckedChanged = new object();

        /// <summary>
        /// The event that occurs when the checked property has changed.
        /// </summary>
        public event EventHandler CheckedChanged
        {
            add
            {
                base.Events.AddHandler(EventCheckedChanged, value);
            }
            remove
            {
                base.Events.AddHandler(EventCheckedChanged, value);
            }
        }

        /// <summary>
        /// Creates a new instance of the GlobalRadioButton class.
        /// </summary>
        public GlobalRadioButton()
            : base(HtmlTextWriterTag.Input)
        {
            this._checkChanged = false;
        }

        private object GetHiddenProperty(object target, Type targetType, string propertyName)
        {
            PropertyInfo property = targetType.GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                return property.GetValue(target, null);
            }
            return null;
        }

        public bool IsChecked()
        {
            string str = this.Context.Request[this.UniqueGroupName];
            if ((str != null) && (str == this.ValueAttribute))
            {
                if (!this.Checked)
                {
                    this.Checked = true;
                    this._checkChanged = true;
                }
            }
            else if (this.Checked)
            {
                this.Checked = false;
            }
            return this.Checked;
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
        }

        /// <summary>
        /// Raises the <see cref="E:Votations.NSurvey.WebControls.UI.GlobalRadioButton.CheckedChanged" /> event.
        /// </summary>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventCheckedChanged];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Overrides the OnPreRender method.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            if ((this.Page != null) && this.Enabled)
            {
                this.Page.RegisterRequiresPostBack(this);
                if (this.AutoPostBack)
                {
                    this.Page.ClientScript.GetPostBackEventReference(this, "");
                }
            }
            if (this.GroupName.Length == 0)
            {
                this.GroupName = this.UniqueID;
            }
            base.OnPreRender(e);
        }

        /// <summary>
        /// Overrides the Render method.
        /// </summary>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Page != null)
            {
                this.Page.VerifyRenderingInServerForm(this);
            }
            bool flag = false;
            if (base.ControlStyleCreated && !((bool)this.GetHiddenProperty(base.ControlStyle, typeof(Style), "IsEmpty")))
            {
                base.ControlStyle.AddAttributesToRender(writer, this);
                flag = true;
            }
            if (!this.Enabled)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                flag = true;
            }
            if (this.ToolTip.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
                flag = true;
            }
            if (base.Attributes.Count != 0)
            {
                string str = base.Attributes["value"];
                if (str != null)
                {
                    base.Attributes.Remove("value");
                }
                if (base.Attributes.Count != 0)
                {
                    base.Attributes.AddAttributes(writer);
                    flag = true;
                }
                if (str != null)
                {
                    base.Attributes["value"] = str;
                }
            }
            if (flag)
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
            }
            if (this.Text.Length != 0)
            {
                if (this.TextAlign == System.Web.UI.WebControls.TextAlign.Left)
                {
                    //TODO JJ Multiline answers separated by <br/>
                    writer.Write("<div style='float:left;margin-left:1px;margin-top:0px;'> ");
                    this.RenderLabel(writer, this.Text, this.ClientID);
                    writer.Write("</div>");
                    writer.Write("<div style='width:86%;float:left;margin-left:1px;margin-top:0px;'> ");
                    this.RenderInputTag(writer, this.ClientID);
                    writer.Write("</div>");
                }
                else
                {
                    //TODO JJ Multiline answers separated by <br/>
                    writer.Write("<div style='float:left;margin-left:1px;margin-top:0px;'> ");
                    this.RenderInputTag(writer, this.ClientID);
                    writer.Write("</div>");
                    writer.Write(
                        string.Format("<div   style='width:86%;float:left;margin-left:0px;margin-top:-5px;'> {0}",this.Text));
                   // this.RenderLabel(writer, this.Text, this.ClientID);
                    writer.Write("</div>");
                }
            }
            else
            {
                this.RenderInputTag(writer, this.ClientID);
            }
            if (flag)
            {
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// Renders the input tag portion of the control.
        /// </summary>
        protected virtual void RenderInputTag(HtmlTextWriter writer, string clientID)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueGroupName);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, this.ValueAttribute);
            if (this.Checked)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
            }
            if (this.AutoPostBack)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onchange, this.Page.ClientScript.GetPostBackEventReference(this, ""));
                writer.AddAttribute("language", "javascript");
            }
            if (this.AccessKey.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
            }
            if (this.TabIndex != 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString(NumberFormatInfo.InvariantInfo));
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        /// <summary>
        /// Renders the label portion of the control.
        /// </summary>
        protected virtual void RenderLabel(HtmlTextWriter writer, string text, string clientID)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.For, clientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write(text);
            writer.RenderEndTag();
        }

        bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            return this._checkChanged;
        }

        void IPostBackDataHandler.RaisePostDataChangedEvent()
        {
            this.OnCheckedChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the state automatically posts back to the server when clicked.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Gets or sets a value indicating whether the state automatically posts back to the server when clicked.")]
        public virtual bool AutoPostBack
        {
            get
            {
                object obj2 = this.ViewState["AutoPostBack"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["AutoPostBack"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        [Description("Gets or sets a value indicating whether the control is checked."), DefaultValue(false), Category("Appearance")]
        public virtual bool Checked
        {
            get
            {
                object obj2 = this.ViewState["Checked"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["Checked"] = value;
            }
        }

        /// <summary>
        /// Gets or sets if the GroupName will span across naming containers on the page.
        /// </summary>
        /// <remarks>
        /// Set this property to true to enable the "global" functionality of the control.
        /// Set this property to false to make it behave like a normal RadioButton.
        /// </remarks>
        [Description("Gets or sets if the GroupName will span across naming containers on the page."), Category("Behavior"), DefaultValue(true)]
        public virtual bool GlobalGroup
        {
            get
            {
                object obj2 = this.ViewState["GlobalGroup"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["GlobalGroup"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the group that the radio button belongs to.
        /// </summary>
        [DefaultValue(""), Description("Gets or sets the name of the group that the radio button belongs to."), Category("Behavior")]
        public virtual string GroupName
        {
            get
            {
                object obj2 = this.ViewState["GroupName"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["GroupName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the text label associated with the control.
        /// </summary>
        [Category("Appearance"), Description("Gets or sets the text label associated with the control."), DefaultValue("")]
        public virtual string Text
        {
            get
            {
                object obj2 = this.ViewState["Text"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the alignment of the text label associated with the control.
        /// </summary>
        [DefaultValue(2), Description("Gets or sets the alignment of the text label associated with the control."), Category("Appearance")]
        public virtual System.Web.UI.WebControls.TextAlign TextAlign
        {
            get
            {
                object obj2 = this.ViewState["TextAlign"];
                if (obj2 != null)
                {
                    return (System.Web.UI.WebControls.TextAlign)obj2;
                }
                return System.Web.UI.WebControls.TextAlign.Right;
            }
            set
            {
                if (!Enum.IsDefined(typeof(System.Web.UI.WebControls.TextAlign), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["TextAlign"] = value;
            }
        }

        /// <summary>
        /// Gets the group name for the control as it will exist in the name attribute of the html.
        /// </summary>
        public virtual string UniqueGroupName
        {
            get
            {
                string groupName = this.GroupName;
                if (!this.GlobalGroup)
                {
                    string uniqueID = this.UniqueID;
                    int num = uniqueID.LastIndexOf(':');
                    if (num >= 0)
                    {
                        groupName = uniqueID.Substring(0, num + 1) + groupName;
                    }
                }
                return groupName;
            }
        }

        /// <summary>
        /// Gets the content to be put in the html value attribute.
        /// </summary>
        protected virtual string ValueAttribute
        {
            get
            {
                string uniqueID = base.Attributes["value"];
                if (uniqueID == null)
                {
                    uniqueID = this.UniqueID;
                }
                return uniqueID;
            }
        }
    }
}

