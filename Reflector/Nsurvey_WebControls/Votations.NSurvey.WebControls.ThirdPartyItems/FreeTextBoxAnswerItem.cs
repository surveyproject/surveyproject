using CKEditor;
//using FreeTextBoxControls;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.Data;
using Votations.NSurvey.Resources;
//using Votations.NSurvey.WebControls.ThirdPartyItems;
using Votations.NSurvey.WebControls.UI;


namespace Votations.NSurvey.WebControls.ThirdPartyItems
{
    public class FreeTextBoxAnswerItem : ExtendedAnswerItem, IFieldItem, IClientScriptValidator, IAnswerPublisher, IMandatoryAnswer, IRegExValidator
    {
        private Table _adminTable;
        private DropDownList _breakModeDropDownList;
        private bool _enableValidation = false;
        private int _fieldHeight = 1;
        private int _fieldLength = 1;
        private int _fieldWidth = 1;

       //protected FreeTextBox _freeTextBox = new FreeTextBox();
        protected CKEditor.NET.CKEditorControl _freeTextBox = new CKEditor.NET.CKEditorControl();

        private string _javascriptCode = null;
        private string _javascriptErrorMessage = null;
        private string _javascriptFunctionName = null;
        private TextBox _languageTextBox;
        private bool _mandatory = false;
        private string _regExpression = null;
        private string _regExpressionErrorMessage = null;

       // private TextBox _toolBarLayoutTextBox;
        private DropDownList _toolBarStylesDropDownList;
        private DropDownList _toolBarLayoutDropDownList;

        private TableRow BuildOptionRow(string label, Control optionControl, string comment, string controlComment)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            TableCell cell2 = new TableCell();
            cell.Wrap = false;
            if (label != null)
            {
                Label child = new Label();
                child.ControlStyle.Font.Bold = true;
                child.Text = label;
                child.CssClass = "AnswerTextRender";//JJ
                cell.Controls.Add(child);
                cell.VerticalAlign = VerticalAlign.Top;
                if (comment != null)
                {
                    cell.Controls.Add(new LiteralControl("<br />" + comment));
                }
                row.Cells.Add(cell);
            }
            else
            {
                cell2.ColumnSpan = 2;
            }
            cell2.Controls.Add(optionControl);
            if (controlComment != null)
            {
                cell2.Controls.Add(new LiteralControl("<br />" + controlComment));
            }
            row.Cells.Add(cell2);
            return row;
        }

        protected override void CreateChildControls()
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
                }
                else
                {
                    /*
                    Literal literal = new Literal();
                    literal.Text = this.Text;
                    this.Controls.Add(literal);
                     */
                    Label label = new Label();
                    label.Text = this.Text;
                    label.CssClass = "AnswerTextRender";
                    this.Controls.Add(label);

                    if (this.Mandatory)
                    {
                        this.Controls.Add(new LiteralControl("<div style='color:red; display:inline-flex;'>&nbsp; * &nbsp;</div>"));
                    }

                }
                this.Controls.Add(new LiteralControl("<br />"));
            }
            this._freeTextBox.ID = "FreeTextBox";
            this._freeTextBox.BasePath = "~/Scripts/ckeditor";
            this._freeTextBox.Width = this.FieldWidth;
            this._freeTextBox.Height = this.FieldHeight;

            //this._freeTextBox.ToolbarStyleConfiguration = this.ToolBarStyle;
            //this._freeTextBox.ToolbarLayout = this.ToolBarLayout;

            this._freeTextBox.config.skin = Convert.ToString(this.ToolBarStyle);
            this._freeTextBox.config.toolbar = this.ToolBarLayout;

            this._freeTextBox.config.enterMode = (CKEditor.NET.EnterMode) this.EditBreakMode;
            //this._freeTextBox.config.skin = "moonocolor";

            this._freeTextBox.Language = this.Language;
            this._freeTextBox.Text = this.DefaultText;
            this.Controls.Add(this._freeTextBox);
            this.OnAnswerPublisherCreated(new AnswerItemEventArgs(this.GetUserAnswers()));
        }

        public override Control GeneratePropertiesUI()
        {
            this._adminTable = new Table();
// moved to css file
// this._adminTable.CssClass = "smallText";
            this._adminTable.Width = Unit.Percentage(100) ;
            new TableRow();
            this._adminTable.Rows.Add(this.BuildOptionRow(null, new LiteralControl(ResourceManager.GetString("FreeTextBoxWarningLabel")), null, null));

            this._toolBarStylesDropDownList = new DropDownList();
            this._toolBarStylesDropDownList.ID = "FreeTextBoxStyle";
            foreach (CkEditorControls.CkeToolbarStyleConfiguration configuration in Enum.GetValues(typeof(CkEditorControls.CkeToolbarStyleConfiguration)))
            {
                this._toolBarStylesDropDownList.Items.Add(new ListItem(configuration.ToString(), ((int)configuration).ToString()));
            }
            this._toolBarStylesDropDownList.SelectedValue = ((int)this.ToolBarStyle).ToString();

            this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("TextBoxStyleLabel"), this._toolBarStylesDropDownList, null, null));


            //this._breakModeDropDownList = new DropDownList();
            //this._breakModeDropDownList.ID = "FreeTextBoxBreakMode";
            //foreach (BreakMode mode in Enum.GetValues(typeof(BreakMode)))
            //{
            //    this._breakModeDropDownList.Items.Add(new ListItem(mode.ToString(), ((int)mode).ToString()));
            //}
            //this._breakModeDropDownList.SelectedValue = ((int)this.EditBreakMode).ToString();
            //this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("BreakModeLabel"), this._breakModeDropDownList, null, null));
            
            this._breakModeDropDownList = new DropDownList();
            this._breakModeDropDownList.ID = "FreeTextBoxBreakMode";
            foreach (CkEditorControls.CkeBreakModeConfiguration mode in Enum.GetValues(typeof(CkEditorControls.CkeBreakModeConfiguration)))
            {
                this._breakModeDropDownList.Items.Add(new ListItem(mode.ToString(), ((int)mode).ToString()));
            }
            this._breakModeDropDownList.SelectedValue = ((int)this.EditBreakMode).ToString();
            this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("BreakModeLabel"), this._breakModeDropDownList, null, null));


            this._languageTextBox = new TextBox();
            this._languageTextBox.ID = "LanguageTextBox";
            this._languageTextBox.Columns = 5;
            this._languageTextBox.Text = this.Language;
            this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("FreeTextBoxLanguageLabel"), this._languageTextBox, null, null));

            //new ckeditor toolbar layout:
            this._toolBarLayoutDropDownList = new DropDownList();
            this._toolBarLayoutDropDownList.ID = "ToolBarLayoutTextBox";

            foreach (CkEditorControls.CkeToolbarLayoutConfiguration configuration in Enum.GetValues(typeof(CkEditorControls.CkeToolbarLayoutConfiguration)))
            {
                this._toolBarLayoutDropDownList.Items.Add(new ListItem(configuration.ToString()));
            }
            this._toolBarLayoutDropDownList.SelectedValue = this.ToolBarLayout;
            this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("ToolBarLayoutLabel"), this._toolBarLayoutDropDownList, null, null));
            

            //this._toolBarLayoutTextBox = new TextBox();
            //this._toolBarLayoutTextBox.ID = "ToolBarLayoutTexBox";
            //this._toolBarLayoutTextBox.Columns = 40;
            //this._toolBarLayoutTextBox.Text = this.ToolBarLayout;

            //this._adminTable.Rows.Add(this.BuildOptionRow(ResourceManager.GetString("ToolBarLayoutLabel"), this._toolBarLayoutTextBox, null, ResourceManager.GetString("ToolBarLayoutExampleLabel")));


            Button optionControl = new Button();
            optionControl.ID = "ApplyChangeButton";
            optionControl.Text = ResourceManager.GetString("ApplyChangesButton");
            optionControl.Click += new EventHandler(this.OnClick);
            this._adminTable.Rows.Add(this.BuildOptionRow(null, optionControl, null, null));

            return this._adminTable;
        }

        public virtual string GetControlIdToValidate()
        {
            this.EnsureChildControls();
            return this._freeTextBox.ClientID;
        }

        protected override PostedAnswerDataCollection GetPostedAnswerData()
        {
            this.ServerValidation();
            PostedAnswerDataCollection userAnswers = this.GetUserAnswers();
            this.OnAnswerPublished(new AnswerItemEventArgs(userAnswers));
            return userAnswers;
        }

        protected virtual PostedAnswerDataCollection GetUserAnswers()
        {
            PostedAnswerDataCollection datas = null;
            //if ((this.Context.Request[this._freeTextBox.ClientID] != null) && (this.Context.Request[this._freeTextBox.ClientID].Length != 0))
            if ((this.Context.Request[this._freeTextBox.UniqueID] != null) && (this.Context.Request[this._freeTextBox.UniqueID].Length != 0))
            {
                datas = new PostedAnswerDataCollection();
                //datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this.Context.Request[this._freeTextBox.ClientID], AnswerTypeMode.RegExValidator | AnswerTypeMode.Mandatory | AnswerTypeMode.Publisher | AnswerTypeMode.Field));
                datas.Add(new PostedAnswerData(this, this.AnswerId, base.SectionContainer.SectionNumber, this.Context.Request[this._freeTextBox.UniqueID], AnswerTypeMode.RegExValidator | AnswerTypeMode.Mandatory | AnswerTypeMode.Publisher | AnswerTypeMode.Field));
            }
            return datas;
        }

        protected virtual void OnClick(object sender, EventArgs e)
        {
            this.ToolBarStyle = (CkEditorControls.CkeToolbarStyleConfiguration)int.Parse(this._toolBarStylesDropDownList.SelectedValue);
            //this.EditBreakMode = (BreakMode)int.Parse(this._breakModeDropDownList.SelectedValue);
            this.EditBreakMode = (CkEditorControls.CkeBreakModeConfiguration)int.Parse(this._breakModeDropDownList.SelectedValue);
            this.Language = this._languageTextBox.Text;
            // this.ToolBarLayout = this._toolBarLayoutTextBox.Text;
//            this.ToolBarLayout = Convert.ToString((CkEditorControls.CkeToolbarLayoutConfiguration)int.Parse(this._toolBarLayoutDropDownList.SelectedValue));
            this.ToolBarLayout = this._toolBarLayoutDropDownList.SelectedValue;
            this.PresistProperties();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (((this.EnableValidation && (this.JavascriptCode != null)) && ((this.JavascriptCode.Length != 0) && (this.JavascriptFunctionName != null))) && ((this.JavascriptFunctionName.Length != 0) && !this.Page.ClientScript.IsClientScriptBlockRegistered(this.JavascriptFunctionName)))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.JavascriptFunctionName, string.Format("<script type=\"text/javascript\" language=\"javascript\"><!--{0}{1}//--></script>", Environment.NewLine, this.JavascriptCode));
            }
            base.OnPreRender(e);
        }

        protected virtual bool ServerValidation()
        {
            string str2;
            bool flag = true;
            //string input = this.Context.Request[this._freeTextBox.ClientID];
            string input = this.Context.Request[this._freeTextBox.UniqueID];
            if (input == null)
            {
                return flag;
            }
            if ((input.Length == 0) && this.Mandatory)
            {
                this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(ResourceManager.GetString("FieldRequiredMessage"), this.Text)));
                return false;
            }
            if (((input.Length <= 0) || (this.RegExpression == null)) || ((this.RegExpression.Length <= 0) || Regex.IsMatch(input, this.RegExpression)))
            {
                return flag;
            }
            if ((this.RegExpressionErrorMessage != null) && (this.RegExpressionErrorMessage.Length > 0))
            {
                str2 = (ResourceManager.GetString(this.RegExpressionErrorMessage) == null) ? this.RegExpressionErrorMessage : ResourceManager.GetString(this.RegExpressionErrorMessage);
            }
            else
            {
                str2 = string.Format(ResourceManager.GetString("RegExValidationFailedMessage"), this.Text);
            }
            this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(str2, this.Text)));
            return false;
        }


        public virtual bool EnableValidation
        {
            get
            {
                return this._enableValidation;
            }
            set
            {
                this._enableValidation = value;
            }
        }

        public virtual int FieldHeight
        {
            get
            {
                return this._fieldHeight;
            }
            set
            {
                this._fieldHeight = value;
            }
        }

        public virtual int FieldLength
        {
            get
            {
                return this._fieldLength;
            }
            set
            {
                this._fieldLength = value;
            }
        }

        public virtual int FieldWidth
        {
            get
            {
                return this._fieldWidth;
            }
            set
            {
                this._fieldWidth = value;
            }
        }

        public virtual string JavascriptCode
        {
            get
            {
                return this._javascriptCode;
            }
            set
            {
                this._javascriptCode = value;
            }
        }

        public virtual string JavascriptErrorMessage
        {
            get
            {
                return this._javascriptErrorMessage;
            }
            set
            {
                this._javascriptErrorMessage = value;
            }
        }

        public virtual string JavascriptFunctionName
        {
            get
            {
                return this._javascriptFunctionName;
            }
            set
            {
                this._javascriptFunctionName = value;
            }
        }

        public string Language
        {
            get
            {
                if (!base.Properties.ContainsProperty("Language"))
                {
                    return "en-US";
                }
                return base.Properties["Language"].ToString();
            }
            set
            {
                base.Properties["Language"] = value;
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

        public virtual string RegExpression
        {
            get
            {
                return this._regExpression;
            }
            set
            {
                this._regExpression = value;
            }
        }

        public virtual string RegExpressionErrorMessage
        {
            get
            {
                return this._regExpressionErrorMessage;
            }
            set
            {
                this._regExpressionErrorMessage = value;
            }
        }

        /// <summary>
        /// CKeditor toolbarlayout options: full/ basic
        /// </summary>
        public string ToolBarLayout
        {
            get
            {
                if (base.Properties.ContainsProperty("ToolBarLayout") && (base.Properties["ToolBarLayout"].ToString().Length > 0))
                {
                    return base.Properties["ToolBarLayout"].ToString();
                }
                return Convert.ToString(CkEditorControls.CkeToolbarLayoutConfiguration.Full);
            }
            set
            {
                base.Properties["ToolBarLayout"] = value;
            }
        }

        public CkEditorControls.CkeToolbarStyleConfiguration ToolBarStyle
        {
            get
            {
                if (!base.Properties.ContainsProperty("ToolbarStyleConfiguration"))
                {
                    return CkEditorControls.CkeToolbarStyleConfiguration.MoonoColor;
                }
                return (CkEditorControls.CkeToolbarStyleConfiguration)base.Properties["ToolbarStyleConfiguration"];
            }
            set
            {
                base.Properties["ToolbarStyleConfiguration"] = value;
            }
        }


        public CkEditorControls.CkeBreakModeConfiguration EditBreakMode
        {
            get
            {
                if (!base.Properties.ContainsProperty("EditBreakMode"))
                {
                    return CkEditorControls.CkeBreakModeConfiguration.BR;
                }
                return (CkEditorControls.CkeBreakModeConfiguration)base.Properties["EditBreakMode"];
            }
            set
            {
                base.Properties["EditBreakMode"] = value;
            }
        }


    }
}

