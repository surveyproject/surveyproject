using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.Data;
using Votations.NSurvey.DALFactory;
using Votations.NSurvey.Constants;

//<input type="hidden" runat="server" id="CtrlEditMode" />
//<input type="hidden" runat="server" id="CtrlLibId" />

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin.UserControls
{
    public partial class LibraryNew : System.Web.UI.UserControl
    {
        private bool _libraryEditMode;
        private int _libraryId;
        public bool LibraryEditMode { get { return _libraryEditMode; } set { ViewState["CtrlEditMode"] = value; _libraryEditMode = value; } }
        public int LibraryId { get { return _libraryId; } set { ViewState["CtrlLibId"] = value; _libraryId = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {

            SetupSecurity();
            if (!Page.IsPostBack)
            {
                FillData();
            }
            else
            {
                LibraryEditMode = (ViewState["CtrlEditMode"] != null) ? (bool)ViewState["CtrlEditMode"] : false;
                LibraryId = (ViewState["CtrlLibId"] != null) ? (int)ViewState["CtrlLibId"] : -1;
            }
        }
        void SetupSecurity()
        {
            //((PageBase)Page).CheckRight(NSurveyRights.ManageLibrary, true);

        }

        public void FillData()
        {
            btnDeleteLib.Visible = LibraryEditMode;
            pnllanguageSelection.Visible = LibraryEditMode;
            btnDeleteLib.Text = ((PageBase)Page).GetPageResource("LibraryTabDelete");
            btnAddLib.Text = ((PageBase)Page).GetPageResource((LibraryEditMode) ? "LibraryTabNewEditLanguage" : "LibraryTabNewAddLanguage");
            lblLibraryTitle.Text = ((PageBase)Page).GetPageResource((LibraryEditMode) ? "LibraryTabEdit" : "LibraryTabNewAdd");

            lblTableName.Text = ((PageBase)Page).GetPageResource("LibraryTabNewName");
            lblNewDescription.Text = ((PageBase)Page).GetPageResource("LibraryTabNewDescription");
            lblLanguageSrc.Text = ((PageBase)Page).GetPageResource("LibraryTabNewEnabledLanguages");
            lblDdlLanguage.Text = ((PageBase)Page).GetPageResource("LibraryTabNewDefaultLanguage");

            MessageLabel.Text = string.Empty;
            lbLangSrc.DataMember = "MultiLanguages";
            lbLangSrc.DataTextField = "LanguageDescription";
            lbLangSrc.DataValueField = "LanguageCode";
            lbLangSrc.DataSource = null;
            lbLangSrc.DataSource = new MultiLanguages().GetMultiLanguages();
            lbLangSrc.DataBind();
            lbLangSrc.SelectedIndex = -1;
            /* JJ Library does not depend on Survey- So this is meaningless */
            if (LibraryEditMode)
            {
                lbLangEnabled.DataMember = "MultiLanguages";
                lbLangEnabled.DataTextField = "LanguageDescription";
                lbLangEnabled.DataValueField = "LanguageCode";
                lbLangEnabled.DataSource = null;
                lbLangEnabled.DataSource = new MultiLanguages().GetSurveyLanguages(LibraryId, Constants.Constants.EntityLibrary);
                lbLangEnabled.DataBind();
                lbLangEnabled.SelectedIndex = -1;
                /*  */
                foreach (ListItem enabledItem in lbLangEnabled.Items)
                {
                    ListItem disabledItem = lbLangSrc.Items.FindByValue(enabledItem.Value);
                    if (disabledItem != null)
                    {
                        lbLangSrc.Items.Remove(disabledItem);
                    }
                }
            }
            Data.MultiLanguageData data = new MultiLanguages().GetSurveyLanguages(LibraryId, Constants.Constants.EntityLibrary);


            LibraryData libraryData = null;
            if (LibraryEditMode)
            {

                libraryData = new Libraries().GetLibraryById(LibraryId);
                LibraryData.LibrariesRow library = (LibraryData.LibrariesRow)libraryData.Libraries.Rows[0];
                txtLibName.Text = library.LibraryName;
                txtLibDescr.Text = library.Description;
            }

            ddlDefaultLang.Items.Clear();

            foreach (Data.MultiLanguageData.MultiLanguagesRow language in data.MultiLanguages)
            {
                ListItem defaultItem = new ListItem(language.LanguageDescription, language.LanguageCode);

                if (language.DefaultLanguage)
                {
                    defaultItem.Selected = true;
                }

                ddlDefaultLang.Items.Add(defaultItem);
            }


            ((PageBase)Page).TranslateListControl(ddlDefaultLang,true);
            ((PageBase)Page).TranslateListControl(lbLangSrc,true);
            ((PageBase)Page).TranslateListControl(lbLangEnabled,true);
        }

        protected void OnAddLibrary(Object sender, EventArgs e)
        {
            ((PageBase)Page).CheckRight(NSurveyRights.ManageLibrary, true);

            if (txtLibName.Text.Length == 0)
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, ((PageBase)Page).GetPageResource("MissingLibraryNameMessage"));
                MessageLabel.Visible = true;
                return;
            }
            LibraryData libraryData = new LibraryData();
            LibraryData.LibrariesRow library = libraryData.Libraries.NewLibrariesRow();
            library.LibraryId = _libraryId;
            library.LibraryName = txtLibName.Text;
            library.Description = txtLibDescr.Text;
            //  library.DefaultLanguageCode = ddlDefaultLang.SelectedValue;
            //    new MultiLanguage().UpdateSurveyLanguage(LibraryId, ddlDefaultLang.SelectedValue, true, Constants.Constants.EntityLibrary);
            libraryData.Libraries.Rows.Add(library);

            if (LibraryEditMode)
            {
                LibraryFactory.Create().UpdateLibrary(libraryData);

                var ml = new MultiLanguage();
                // Reset all other default items  
                foreach (ListItem item in this.ddlDefaultLang.Items)
                    new MultiLanguage().UpdateSurveyLanguage(LibraryId, item.Value,item.Selected, Constants.Constants.EntityLibrary);
            }
            else
            {
                LibraryFactory.Create().AddLibrary(libraryData);
                new MultiLanguage().UpdateSurveyLanguage(libraryData.Libraries[0].LibraryId, System.Globalization.CultureInfo.CurrentCulture.Name, true, Constants.Constants.EntityLibrary);
                txtLibName.Text = string.Empty;
                txtLibDescr.Text = string.Empty;
            }
            ((PageBase)Page).ShowNormalMessage(MessageLabel, ((PageBase)Page).GetPageResource("UpdatedLibraryNameMessage"));
            MessageLabel.Visible = true;
            UINavigator.NavigateToLibraryDirectory(0, 0, 0);
            FillData();
        }

        public void OnAddLang(Object sender, EventArgs e)
        {
            new MultiLanguage().UpdateSurveyLanguage(LibraryId, lbLangSrc.SelectedValue, false, Constants.Constants.EntityLibrary);
            FillData();
        }

        public void OnRemoveLang(Object sender, EventArgs e)
        {
            if (lbLangEnabled.SelectedValue != ddlDefaultLang.SelectedValue)
            {
                new MultiLanguage().DeleteSurveyLanguage(LibraryId, lbLangEnabled.SelectedValue, Constants.Constants.EntityLibrary);
                FillData();
            }
        }

        public void OnDeleteLibrary(Object sender, EventArgs e)
        {
            ((PageBase)Page).CheckRight(NSurveyRights.ManageLibrary, true);
            LibraryFactory.Create().DeleteLibrary(_libraryId);
            UINavigator.NavigateToLibraryDirectory(((PageBase)Page).getSurveyId(), ((PageBase)Page).MenuIndex, 0);
        }


    }
}
