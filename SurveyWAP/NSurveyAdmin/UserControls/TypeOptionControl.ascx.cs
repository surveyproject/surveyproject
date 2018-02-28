/**************************************************************************************************
	Survey™ Project changes: copyright (c) 2009-2017, W3DevPro™ (https://github.com/surveyproject)	

	NSurvey - The web survey and form engine
	Copyright (c) 2004, 2005 Thomas Zumbrunn. (http://www.nsurvey.org)


	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

************************************************************************************************/

using Votations.NSurvey.WebAdmin;

namespace Votations.NSurvey.WebAdmin.UserControls
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.BusinessRules;
    using Votations.NSurvey.Enums;
    using Microsoft.VisualBasic;
    using Votations.NSurvey.Web;
    using Votations.NSurvey.WebControlsFactories;
    using Votations.NSurvey.WebControls.UI;


    /// <summary>
    /// Survey data CU methods
    /// </summary>
    public partial class TypeOptionControl : UserControl
    {
        protected System.Web.UI.WebControls.Label MessageLabel;
        protected System.Web.UI.WebControls.Label fieldTypeOptionTitleLabel;
        protected System.Web.UI.WebControls.TextBox TitleTextBox;
        protected System.Web.UI.WebControls.Label XmlFileNameLabel;
        protected System.Web.UI.WebControls.TextBox XmlFileNameTextbox;
        protected System.Web.UI.WebControls.CheckBox SelectionTypeCheckBox;
        protected System.Web.UI.WebControls.CheckBox FieldTypeCheckBox;
        protected System.Web.UI.WebControls.PlaceHolder FieldOptionsPlaceHolder;
        protected System.Web.UI.WebControls.TextBox FieldWidthTextBox;
        protected System.Web.UI.WebControls.TextBox FieldHeightTextBox;
        protected System.Web.UI.WebControls.TextBox FieldMaxLengthTextBox;
        protected System.Web.UI.WebControls.CheckBox FieldShownInResultsCheckBox;
        protected System.Web.UI.WebControls.TextBox JavascriptFunctionNameTextBox;
        protected System.Web.UI.WebControls.TextBox JavascriptErrorMessageTextBox;
        protected System.Web.UI.WebControls.TextBox JavascriptTextBox;
        protected System.Web.UI.WebControls.Button CreateTypeButton;
        protected System.Web.UI.WebControls.Button ApplyChangesButton;
        protected System.Web.UI.WebControls.Label AllowSelectionLabel;
        protected System.Web.UI.WebControls.Label FieldEntryLabel;
        protected System.Web.UI.WebControls.Label FieldWidthLabel;
        protected System.Web.UI.WebControls.Label FieldHeightLabel;
        protected System.Web.UI.WebControls.Label FieldLengthLabel;
        protected System.Web.UI.WebControls.Label ShownInResultsLabel;
        protected System.Web.UI.WebControls.Label JavascriptFunctionLabel;
        protected System.Web.UI.WebControls.Label JavascriptErrorMessageLabel;
        protected System.Web.UI.WebControls.Label JavascriptCodeLabel;
        protected System.Web.UI.WebControls.Label FieldNameLabel;
        protected System.Web.UI.WebControls.Button MakeBuiltInButton;
        protected System.Web.UI.WebControls.DropDownList DataSourceDropDownList;
        protected System.Web.UI.WebControls.Button DeleteTypeButton;
        protected System.Web.UI.WebControls.Label DataSourceLabel;
        protected System.Web.UI.WebControls.Label SqlQueryLabel;
        protected System.Web.UI.WebControls.TextBox SqlQueryTextbox;
        protected System.Web.UI.WebControls.Label SqlQueryInfoLabel;
        protected System.Web.UI.WebControls.CheckBox RichFieldCheckBox;
        protected System.Web.UI.WebControls.Label RichFieldLabel;
        protected PlaceHolder FullOptionPlaceholder;
        public event EventHandler OptionChanged;

        /// <summary>
        /// Id of the answer type to edit
        /// if no id is given put the 
        /// usercontrol in creation mode
        /// </summary>
        public int AnswerTypeId
        {
            get { return (ViewState["AnswerTypeId"] == null) ? -1 : int.Parse(ViewState["AnswerTypeId"].ToString()); }
            set { ViewState["AnswerTypeId"] = value; }
        }


        private void Page_Load(object sender, System.EventArgs e)
        {
            MessageLabel.Visible = false;
            LocalizePage();

            // Check if any answer type id has been assigned
            if (AnswerTypeId == -1)
            {
                SwitchToCreationMode();
            }
            else
            {
                SwitchToEditionMode();
            }
        }

        private void LocalizePage()
        {
            FieldNameLabel.Text = ((PageBase)Page).GetPageResource("FieldNameLabel");
            DataSourceLabel.Text = ((PageBase)Page).GetPageResource("DataSourceLabel");
            XmlFileNameLabel.Text = ((PageBase)Page).GetPageResource("XmlFileNameLabel");
            AllowSelectionLabel.Text = ((PageBase)Page).GetPageResource("AllowSelectionLabel");
            FieldEntryLabel.Text = ((PageBase)Page).GetPageResource("FieldEntryLabel");
            FieldWidthLabel.Text = ((PageBase)Page).GetPageResource("FieldWidthLabel");
            FieldHeightLabel.Text = ((PageBase)Page).GetPageResource("FieldHeightLabel");
            FieldLengthLabel.Text = ((PageBase)Page).GetPageResource("FieldLengthLabel");
            ShownInResultsLabel.Text = ((PageBase)Page).GetPageResource("ShownInResultsLabel");
            JavascriptFunctionLabel.Text = ((PageBase)Page).GetPageResource("JavascriptFunctionLabel");
            JavascriptCodeLabel.Text = ((PageBase)Page).GetPageResource("JavascriptCodeLabel");
            CreateTypeButton.Text = ((PageBase)Page).GetPageResource("CreateTypeButton");
            CancelButton.Text = ((PageBase)Page).GetPageResource("CancelText");
            ApplyChangesButton.Text = ((PageBase)Page).GetPageResource("ApplyChangesButton");
            DeleteTypeButton.Text = ((PageBase)Page).GetPageResource("DeleteTypeButton");
            MakeBuiltInButton.Text = ((PageBase)Page).GetPageResource("MakeBuiltInButton");
            SqlQueryLabel.Text = ((PageBase)Page).GetPageResource("SqlQueryLabel");
            SqlQueryInfoLabel.Text = ((PageBase)Page).GetPageResource("SqlQueryInfoLabel");
            RichFieldLabel.Text = ((PageBase)Page).GetPageResource("RichFieldLabel");

            if (!Page.IsPostBack)
            {
                DataSourceDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("NoneDataSourceOption"), "1"));
                DataSourceDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("XmlDataSourceOption"), "2"));
                DataSourceDropDownList.Items.Add(new ListItem(((PageBase)Page).GetPageResource("SqlDataSourceOption"), "3"));
            }

        }

        private void FieldEntryChecked(object sender, System.EventArgs e)
        {
            InitUserInterface();
        }

        /// <summary>
        /// Setup the control in creation mode
        /// </summary>
        private void SwitchToCreationMode()
        {
            // Creation mode
            fieldTypeOptionTitleLabel.Text = ((PageBase)Page).GetPageResource("NewAnswerTypeTitle"); ;
            CreateTypeButton.Visible = true;
            CancelButton.Visible = true;
            ApplyChangesButton.Visible = false;
            DeleteTypeButton.Visible = false;
        }

        /// <summary>
        /// Setup the control in edition mode
        /// </summary>
        private void SwitchToEditionMode()
        {
            fieldTypeOptionTitleLabel.Text = ((PageBase)Page).GetPageResource("EditAnswerTypeTitle");
            CreateTypeButton.Visible = false;
            CancelButton.Visible = false;
            ApplyChangesButton.Visible = true;
            DeleteTypeButton.Visible = true;
            MakeBuiltInButton.Visible = false;
        }

        /// <summary>
        /// Get the current DB data and fill 
        /// the fields with them
        /// </summary>
        public void BindFields()
        {
            // Retrieve the answer type data
            AnswerTypeData answerTypeData = new AnswerTypes().GetAnswerTypeById(AnswerTypeId);
            AnswerTypeData.AnswerTypesRow answerType = answerTypeData.AnswerTypes[0];

            if (answerType.BuiltIn)
            {
                DeleteTypeButton.Attributes.Add("onClick",
                    "javascript:if(confirm('" + ((PageBase)Page).GetPageResource("DeleteBuiltInTypeConfirmationMessage") + "')== false) return false;");
                ApplyChangesButton.Attributes.Add("onClick",
                    "javascript:if(confirm('" + ((PageBase)Page).GetPageResource("ApplyBuiltInChangesConfirmationMessage") + "')== false) return false;");
            }
            else
            {
                DeleteTypeButton.Attributes.Remove("onClick");
                ApplyChangesButton.Attributes.Remove("onClick");
                MakeBuiltInButton.Attributes.Add("onClick",
                    "javascript:if(confirm('" + ((PageBase)Page).GetPageResource("MakeBuiltInConfirmationMessage") + "')== false) return false;");
            }


            // Assigns the retrieved data to the correct fields
            TitleTextBox.Text = answerType.Description;
            if (answerType.XmlDataSource !=
                null && answerType.XmlDataSource.Length != 0)
            {
                XmlFileNameTextbox.Text = answerType.XmlDataSource;
                DataSourceDropDownList.SelectedValue = "2";
            }
            else if (answerType.DataSource !=
                null && answerType.DataSource.Length != 0)
            {
                SqlQueryTextbox.Text = answerType.DataSource;
                DataSourceDropDownList.SelectedValue = "3";
            }
            else
            {
                DataSourceDropDownList.SelectedValue = "1";
                SelectionTypeCheckBox.Checked =
                    (((AnswerTypeMode)answerType.TypeMode & AnswerTypeMode.Selection) > 0);
                FieldTypeCheckBox.Checked =
                    (((AnswerTypeMode)answerType.TypeMode & AnswerTypeMode.Field) > 0);

                FieldShownInResultsCheckBox.Checked = answerType.PublicFieldResults;
                FieldHeightTextBox.Text = answerType.FieldHeight.ToString();
                FieldWidthTextBox.Text = answerType.FieldWidth.ToString();
                FieldMaxLengthTextBox.Text = answerType.FieldLength.ToString();
                JavascriptFunctionNameTextBox.Text = answerType.JavascriptFunctionName;
                JavascriptTextBox.Text = answerType.JavascriptCode;
                JavascriptErrorMessageTextBox.Text = answerType.JavascriptErrorMessage;

                RichFieldLabel.Visible = !SelectionTypeCheckBox.Checked;
                RichFieldCheckBox.Visible = !SelectionTypeCheckBox.Checked;
                if (SelectionTypeCheckBox.Checked)
                {
                    RichFieldCheckBox.Checked = false;
                }
                else
                {
                    RichFieldCheckBox.Checked =
                        answerType.TypeNameSpace == "Votations.NSurvey.WebControls.ThirdPartyItems.FreeTextBoxAnswerItem";
                }
            }

            if ((answerType.BuiltIn && !((PageBase)Page).NSurveyUser.Identity.IsAdmin) ||
                (DataSourceDropDownList.SelectedValue == "3" &&
                (!GlobalConfig.SqlBasedAnswerTypesAllowed ||
                !((PageBase)Page).CheckRight(NSurveyRights.SqlAnswerTypesEdition, false))))
            {
                ApplyChangesButton.Visible = false;
                DeleteTypeButton.Visible = false;
                MakeBuiltInButton.Visible = false;
                if (DataSourceDropDownList.SelectedValue == "3"
                    && !((PageBase)Page).CheckRight(NSurveyRights.SqlAnswerTypesEdition, false))
                {
                    DataSourceDropDownList.Enabled = false;
                }
                else
                {
                    DataSourceDropDownList.Enabled = true;
                }
            }
            else if (answerType.BuiltIn && ((PageBase)Page).NSurveyUser.Identity.IsAdmin)
            {
                ApplyChangesButton.Visible = true;
                DeleteTypeButton.Visible = true;
                MakeBuiltInButton.Visible = false;
                DataSourceDropDownList.Enabled = true;
            }
            else
            {
                ApplyChangesButton.Visible = true;
                DeleteTypeButton.Visible = true;
                MakeBuiltInButton.Visible = ((PageBase)Page).NSurveyUser.Identity.IsAdmin;
                DataSourceDropDownList.Enabled = true;
            }

            InitUserInterface();
        }


        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataSourceDropDownList.SelectedIndexChanged += new System.EventHandler(this.DataSourceDropDownList_SelectedIndexChanged);
            this.SelectionTypeCheckBox.CheckedChanged += new System.EventHandler(this.SelectionTypeCheckBox_CheckedChanged);
            this.FieldTypeCheckBox.CheckedChanged += new System.EventHandler(this.FieldEntryChecked);
            this.CreateTypeButton.Click += new System.EventHandler(this.CreateTypeButton_Click);
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            this.ApplyChangesButton.Click += new System.EventHandler(this.ApplyChangesButton_Click);
            this.DeleteTypeButton.Click += new System.EventHandler(this.DeleteTypeButton_Click);
            this.MakeBuiltInButton.Click += new System.EventHandler(this.MakeBuiltInButton_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void XmlDataSourceCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            InitUserInterface();
        }

        private void InitUserInterface()
        {
            switch (DataSourceDropDownList.SelectedValue)
            {
                // Xml datasource
                case "2":
                    {
                        FullOptionPlaceholder.Visible = false;
                        XmlFileNameTextbox.Visible = true;
                        XmlFileNameLabel.Visible = true;
                        FieldOptionsPlaceHolder.Visible = false;
                        SqlQueryLabel.Visible = false;
                        SqlQueryTextbox.Visible = false;
                        SqlQueryInfoLabel.Visible = false;
                        break;
                    }

                // Sql datasource
                case "3":
                    {
                        FullOptionPlaceholder.Visible = false;
                        XmlFileNameTextbox.Visible = false;
                        XmlFileNameLabel.Visible = false;
                        FieldOptionsPlaceHolder.Visible = false;
                        SqlQueryLabel.Visible = true;
                        SqlQueryTextbox.Visible = true;
                        SqlQueryInfoLabel.Visible = true;

                        break;
                    }
                // none
                default:
                    {
                        FullOptionPlaceholder.Visible = true;
                        XmlFileNameTextbox.Visible = false;
                        XmlFileNameLabel.Visible = false;
                        FieldOptionsPlaceHolder.Visible = FieldTypeCheckBox.Checked;
                        RichFieldLabel.Visible = !SelectionTypeCheckBox.Checked;
                        RichFieldCheckBox.Visible = !SelectionTypeCheckBox.Checked;
                        if (SelectionTypeCheckBox.Checked)
                        {
                            RichFieldCheckBox.Checked = false;
                        }
                        SqlQueryLabel.Visible = false;
                        SqlQueryTextbox.Visible = false;
                        SqlQueryInfoLabel.Visible = false;
                        break;
                    }
            }
        }

        private void CreateTypeButton_Click(object sender, System.EventArgs e)
        {
            AnswerTypeData answerTypeData = null;
            if (TitleTextBox.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingAnswerTypeTitleMessage"));
            }
            else if (!SelectionTypeCheckBox.Checked && DataSourceDropDownList.SelectedValue == "1" && !FieldTypeCheckBox.Checked)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingTypeMessage"));
            }
            else
            {
                if (DataSourceDropDownList.SelectedValue == "2")
                {
                    answerTypeData = CreateXmlTypeFromForm();
                }
                else if (DataSourceDropDownList.SelectedValue == "3" &&
                     GlobalConfig.SqlBasedAnswerTypesAllowed &&
                    ((PageBase)Page).CheckRight(NSurveyRights.SqlAnswerTypesEdition, false))
                {
                    answerTypeData = CreateSqlTypeFromForm();
                }
                else if (DataSourceDropDownList.SelectedValue == "1")
                {
                    answerTypeData = CreateTypeFromForm();
                }
            }

            if (answerTypeData != null)
            {
                MessageLabel.Visible = true;
                new AnswerType().AddAnswerType(answerTypeData, ((PageBase)Page).NSurveyUser.Identity.UserId);
                UINavigator.NavigateToTypeEditor(((PageBase)Page).MenuIndex);
            }
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            UINavigator.NavigateToTypeEditor(((PageBase)Page).MenuIndex);
        }


        private AnswerTypeData CreateTypeFromForm()
        {
            int TypeMode = 0;
            AnswerTypeData answerTypeData = new AnswerTypeData();
            AnswerTypeData.AnswerTypesRow newAnswerType = answerTypeData.AnswerTypes.NewAnswerTypesRow();

            newAnswerType.Description = TitleTextBox.Text;
            if (SelectionTypeCheckBox.Checked)
            {
                TypeMode += (int)AnswerTypeMode.Selection;
                newAnswerType.TypeAssembly = "SurveyProject.WebControls";
                newAnswerType.TypeNameSpace = "Votations.NSurvey.WebControls.UI.AnswerSelectionItem";
            }

            if (FieldTypeCheckBox.Checked)
            {
                if (!ValidateFieldOptions())
                {
                    return null;
                }

                TypeMode += (int)AnswerTypeMode.Field;
                TypeMode += (int)AnswerTypeMode.Publisher;
                TypeMode += (int)AnswerTypeMode.RegExValidator;
                TypeMode += (int)AnswerTypeMode.Mandatory;
                if (SelectionTypeCheckBox.Checked)
                {
                    newAnswerType.TypeAssembly = "SurveyProject.WebControls";
                    newAnswerType.TypeNameSpace = "Votations.NSurvey.WebControls.UI.AnswerOtherFieldItem";
                }
                else if (RichFieldCheckBox.Checked)
                {
                    TypeMode += (int)AnswerTypeMode.ExtendedType;
                    newAnswerType.TypeAssembly = "SurveyProject.WebControls";
                    newAnswerType.TypeNameSpace = "Votations.NSurvey.WebControls.ThirdPartyItems.FreeTextBoxAnswerItem";
                }
                else
                {
                    newAnswerType.TypeAssembly = "SurveyProject.WebControls";
                    newAnswerType.TypeNameSpace = "Votations.NSurvey.WebControls.UI.AnswerFieldItem";
                }
            }

            newAnswerType.TypeMode = TypeMode;
            newAnswerType.FieldHeight = int.Parse(FieldHeightTextBox.Text);
            newAnswerType.FieldWidth = int.Parse(FieldWidthTextBox.Text);
            newAnswerType.FieldLength = int.Parse(FieldMaxLengthTextBox.Text);
            newAnswerType.PublicFieldResults = FieldShownInResultsCheckBox.Checked;
            newAnswerType.JavascriptFunctionName = JavascriptFunctionNameTextBox.Text;
            newAnswerType.JavascriptErrorMessage = JavascriptErrorMessageTextBox.Text;
            newAnswerType.JavascriptCode = JavascriptTextBox.Text;
            answerTypeData.AnswerTypes.AddAnswerTypesRow(newAnswerType);
            return answerTypeData;

        }

        private bool ValidateFieldOptions()
        {
            if (!Information.IsNumeric(FieldHeightTextBox.Text))
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidFieldHeightMessage"));
                return false;
            }

            if (!Information.IsNumeric(FieldMaxLengthTextBox.Text))
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidFieldLengthMessage"));
                return false;
            }

            if (!Information.IsNumeric(FieldWidthTextBox.Text))
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("InvalidFieldWidthMessage"));
                return false;
            }

            return true;
        }


        private AnswerTypeData CreateXmlTypeFromForm()
        {
            AnswerTypeData answerTypeData = null;
            if (XmlFileNameTextbox.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingXmlDataSourceMessage"));
            }
            else
            {
                answerTypeData = new AnswerTypeData();
                AnswerTypeData.AnswerTypesRow newAnswerType = answerTypeData.AnswerTypes.NewAnswerTypesRow();
                newAnswerType.Description = TitleTextBox.Text;
                newAnswerType.TypeMode = (int)AnswerTypeMode.DataSource;
                newAnswerType.TypeMode += (int)AnswerTypeMode.Publisher;
                newAnswerType.TypeMode += (int)AnswerTypeMode.Mandatory;

                newAnswerType.XmlDataSource = XmlFileNameTextbox.Text;
                newAnswerType.TypeAssembly = "SurveyProject.WebControls";
                newAnswerType.TypeNameSpace = "Votations.NSurvey.WebControls.UI.AnswerXmlListItem";

                answerTypeData.AnswerTypes.AddAnswerTypesRow(newAnswerType);
            }

            return answerTypeData;
        }

        private AnswerTypeData CreateSqlTypeFromForm()
        {
            AnswerTypeData answerTypeData = null;
            if (SqlQueryTextbox.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingQueryDataSourceMessage"));
            }
            else if (SqlQueryTextbox.Text.IndexOf("update") > -1 ||
                SqlQueryTextbox.Text.IndexOf("delete") > -1 || SqlQueryTextbox.Text.IndexOf("create") > -1 ||
                SqlQueryTextbox.Text.IndexOf("truncate") > -1 || SqlQueryTextbox.Text.IndexOf("fetch") > -1 ||
                SqlQueryTextbox.Text.IndexOf("grant") > -1 || SqlQueryTextbox.Text.IndexOf("insert") > -1 ||
                SqlQueryTextbox.Text.IndexOf("revoke") > -1 || SqlQueryTextbox.Text.IndexOf("open") > -1 ||
                SqlQueryTextbox.Text.IndexOf("alter") > -1)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("OnlySelectQueryAllowedMessage"));
            }
            else
            {
                answerTypeData = new AnswerTypeData();
                AnswerTypeData.AnswerTypesRow newAnswerType = answerTypeData.AnswerTypes.NewAnswerTypesRow();
                newAnswerType.Description = TitleTextBox.Text;
                newAnswerType.TypeMode = (int)AnswerTypeMode.DataSource;
                newAnswerType.TypeMode += (int)AnswerTypeMode.Publisher;
                newAnswerType.TypeMode += (int)AnswerTypeMode.Mandatory;
                SqlQueryTextbox.Text.Replace("delete ", "");
                SqlQueryTextbox.Text.Replace("update ", "");
                SqlQueryTextbox.Text.Replace("truncate ", "");

                newAnswerType.DataSource = SqlQueryTextbox.Text;
                newAnswerType.TypeAssembly = "SurveyProject.WebControls";
                newAnswerType.TypeNameSpace = "Votations.NSurvey.WebControls.UI.AnswerSqlListItem";

                answerTypeData.AnswerTypes.AddAnswerTypesRow(newAnswerType);
            }

            return answerTypeData;
        }

        protected void OnOptionChanged()
        {
            if (OptionChanged != null)
            {
                OptionChanged(this, EventArgs.Empty);
            }
        }

        private void ApplyChangesButton_Click(object sender, System.EventArgs e)
        {
            AnswerTypeData answerTypeData = null;
            if (TitleTextBox.Text.Length == 0)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingAnswerTypeTitleMessage"));
            }
            else if (!SelectionTypeCheckBox.Checked && DataSourceDropDownList.SelectedValue == "1" &&
                !FieldTypeCheckBox.Checked)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingTypeMessage"));
            }
            else
            {
                if (DataSourceDropDownList.SelectedValue == "2")
                {
                    answerTypeData = CreateXmlTypeFromForm();
                }
                else if (DataSourceDropDownList.SelectedValue == "3" &&
                    GlobalConfig.SqlBasedAnswerTypesAllowed &&
                    ((PageBase)Page).CheckRight(NSurveyRights.SqlAnswerTypesEdition, false))
                {
                    answerTypeData = CreateSqlTypeFromForm();
                }
                else if (DataSourceDropDownList.SelectedValue == "1")
                {
                    answerTypeData = CreateTypeFromForm();
                }
            }

            if (answerTypeData != null)
            {
                answerTypeData.AnswerTypes[0].AnswerTypeId = AnswerTypeId;
                new AnswerType().UpdateAnswerType(answerTypeData);
                OnOptionChanged();
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("AnswerTypeUpdatedMessage"));
            }

            InitUserInterface();
        }

        private void DeleteTypeButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                new AnswerType().DeleteAnswerType(AnswerTypeId);
                Visible = false;
                OnOptionChanged();
            }
            catch (AnswerTypeInUseException)
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("AnswerTypeInUse"));
            }
            InitUserInterface();

        }

        private void MakeBuiltInButton_Click(object sender, System.EventArgs e)
        {
            new AnswerType().SetBuiltInAnswerType(AnswerTypeId);
            OnOptionChanged();
            InitUserInterface();
        }

        private void DataSourceDropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (DataSourceDropDownList.SelectedValue == "3" &&
                (!((PageBase)Page).CheckRight(NSurveyRights.SqlAnswerTypesEdition, false) ||
                !GlobalConfig.SqlBasedAnswerTypesAllowed))
            {
                MessageLabel.Visible = true;
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("SqlAnswerTypeAccessDeniedMessage"));
                DataSourceDropDownList.ClearSelection();
                if (AnswerTypeId != -1)
                {
                    BindFields();
                }
            }

            InitUserInterface();
        }

        private void SelectionTypeCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            InitUserInterface();
        }
    }
}
