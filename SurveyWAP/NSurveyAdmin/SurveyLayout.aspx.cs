using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Security;
using Votations.NSurvey.SQLServerDAL;
using Votations.NSurvey.Data;
using Votations.NSurvey.Constants;
using Votations.NSurvey.DataAccess;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class SurveyLayout : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UITabList.SetDesignerTabs((MsterPageTabs)Page.Master, UITabList.DesignerTabs.SurveyInfoLayout);
            SetupSecurity();
            int surveyid = SurveyId;// If SurveyId is not set redirect
            if (!Page.IsPostBack)
            {

                LocalizePage();
                BindFields();
                BindLanguages();
                cssBtnID.Visible = false;

            }

            //CKEditor config:
            txtPageFooter.config.enterMode = CKEditor.NET.EnterMode.BR;
            txtPageFooter.config.skin = "moonocolor";

            txtPageHeader.config.enterMode = CKEditor.NET.EnterMode.BR;
            txtPageHeader.config.skin = "moonocolor";

        }


        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.SurveyLayoutRight, true);
        }
        private void LocalizePage()
        {
            CssLabel.Text = GetPageResource("CssLabel");
            HeaderLabel.Text = GetPageResource("HeaderLabel");
            FooterLabel.Text = GetPageResource("FooterLabel");
            EditCssTitle.Text = GetPageResource("EditCssTitle");
            EditCssSaveButton.Text = GetPageResource("UpdateText");
            EditCssCancelButton.Text = GetPageResource("CancelText");
            btnCssDelete.Text = GetPageResource("ButtonDeleteColumn");
            btnCssDownload.Text = GetPageResource("FileDownloadButton");
            btnCssEdit.Text = GetPageResource("EditText");
            btnCssUpload.Text = GetPageResource("Upload");
            //btnCssUpload.Text = "Upload File";
            EditionLanguageLabel.Text = GetPageResource("EditionLanguageLabel");
            SurveyLayoutLegend.Text = GetPageResource("SurveyLayoutLegend");
            LayoutSaveButton.Text = GetPageResource("BtnFriendly");

        }

        private void BindLanguages()
        {
            MultiLanguageMode languageMode = MultiLanguageMode.UserSelection;

            languageMode = new MultiLanguages().GetMultiLanguageMode(SurveyId);
            if (languageMode != MultiLanguageMode.None)
            {
                MultiLanguageData surveyLanguages;

                surveyLanguages = new MultiLanguages().GetSurveyLanguages(SurveyId);

                LanguagesDropdownlist.Items.Clear();
                foreach (MultiLanguageData.MultiLanguagesRow language in surveyLanguages.MultiLanguages)
                {
                    ListItem defaultItem = new ListItem(GetPageResource(language.LanguageDescription), language.LanguageCode);
                    if (language.DefaultLanguage)
                    {
                        defaultItem.Value = "";
                        defaultItem.Text += " " + GetPageResource("LanguageDefaultText");
                    }

                    LanguagesDropdownlist.Items.Add(defaultItem);
                }

                LanguagesDropdownlist.Visible = true;
                EditionLanguageLabel.Visible = true;
            }
            else
            {
                LanguagesDropdownlist.Visible = false;
                EditionLanguageLabel.Visible = false;
            }
        }
        private void BindFields()
        {
            SQLServerDAL.SurveyLayout sl = new SQLServerDAL.SurveyLayout();
            SurveyLayoutData ud = sl.SurveyLayoutGet(((PageBase)Page).SurveyId, LanguagesDropdownlist.SelectedValue);
            string[] names = Directory.GetFiles(Constants.Constants.GetFilePathCSS(SurveyId));
            int i;

            for (i = 0; i < names.Length; i++)
            {
                names[i] = Path.GetFileName(names[i]);
            }
            ddlTemplate.Items.Clear();
            ddlTemplate.Items.Add(new ListItem(GetPageResource("DdlNoSelect"), "-1"));
            ddlTemplate.DataSource = names;
            ddlTemplate.DataBind();


            bool cssSelected = false;
            if (ud.SurveyLayout.Count > 0)
            {
                foreach (ListItem item in ddlTemplate.Items)
                {
                    if (item.Text == ud.SurveyLayout[0].SurveyCss)
                    {
                        item.Selected = true;
                        cssSelected = true;
                        break;
                    }
                }

                if (!cssSelected) ddlTemplate.SelectedIndex = 0;

                txtPageFooter.Text = ud.SurveyLayout[0].SurveyFooterText;
                txtPageHeader.Text = ud.SurveyLayout[0].SurveyHeaderText;
            }
        }

        /// <summary>
        /// Command activated on clicking BtnCssUpload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCssFileUpload(object sender, CommandEventArgs e)
        {
            string filename = fuCss.FileName;

            if (fuCss.HasFile && !string.IsNullOrEmpty(filename))
            {
                filename = Path.GetFileName(fuCss.PostedFile.FileName);

                if (Path.GetExtension(filename).ToLower() == ".css")
                {

                    fuCss.SaveAs(Constants.Constants.GetFilePathCSS(SurveyId) + filename);
                    cssBtnID.Visible = true;
                    ShowNormalMessage(MessageLabel, GetPageResource("FileUploadedMessage"));

                    BindFields();
                }
                else
                {
                    ShowErrorMessage(MessageLabel, GetPageResource("InvalidFileTypeLayoutMsg"));
                }
            }
        }

        /// <summary>
        /// Command on LayoutSaveButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSave(object sender, CommandEventArgs e)
        {
            SQLServerDAL.SurveyLayout sl = new SQLServerDAL.SurveyLayout();
            SurveyLayoutData sld = new SurveyLayoutData();
            SurveyLayoutData.SurveyLayoutRow slr = sld.SurveyLayout.NewSurveyLayoutRow();

            slr.SurveyId = ((PageBase)Page).SurveyId; ;

            slr.SurveyCss = ddlTemplate.SelectedIndex > 0 ? ddlTemplate.SelectedItem.Text : "";
            slr.SurveyHeaderText = txtPageHeader.Text;
            slr.SurveyFooterText = txtPageFooter.Text;
            sld.SurveyLayout.AddSurveyLayoutRow(slr);
            sl.SurveyLayoutUpdate(sld, LanguagesDropdownlist.SelectedValue);

            ShowNormalMessage(MessageLabel, GetPageResource("SurveyLayoutSavedMsg"));

        }

        protected void btnCssDelete_Click(object sender, EventArgs e)
        {
            if (ddlTemplate.SelectedIndex == 0) return;
            string cssToDelete = Constants.Constants.GetFilePathCSS(SurveyId) + ddlTemplate.SelectedItem.Text;
            File.Delete(cssToDelete);
            BindFields();

            ddlTemplate.SelectedIndex = 0;
            ShowNormalMessage(MessageLabel, GetPageResource("UploadFileDeletedMessage"));

        }

        protected void btnCssDownload_Click(object sender, EventArgs e)
        {
            if (ddlTemplate.SelectedIndex == 0) return;
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", ddlTemplate.SelectedItem.Text));

            // Writes the UTF 8 header
            byte[] BOM = new byte[] { 0xef, 0xbb, 0xbf };
            Response.BinaryWrite(BOM);

            // Export the data
            Response.TransmitFile(Constants.Constants.GetFilePathCSS(SurveyId) + ddlTemplate.SelectedItem.Text);

            Response.End();
        }


        protected void EditCssCancelButton_Click(object sender, EventArgs e)
        {
            EditCssTextBox.Text = string.Empty;
            EditCssTextBox.Visible = false;
            EditCssPlaceHolder.Visible = false;
            pnlHdrFooter.Visible = true;
        }

        protected void EditCssSaveButton_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Constants.Constants.GetFilePathCSS(SurveyId) + ddlTemplate.SelectedItem.Text, EditCssTextBox.Text);
            EditCssTextBox.Text = string.Empty;
            EditCssPlaceHolder.Visible = false;
            pnlHdrFooter.Visible = true;

            ddlTemplate.SelectedIndex = 0;
            ShowNormalMessage(MessageLabel, GetPageResource("CssFileUpdatedMsg"));
        }

        protected void btnCssEdit_Click(object sender, EventArgs e)
        {

            if (ddlTemplate.SelectedIndex == 0) return;
            EditCssTextBox.Visible = true;
            pnlHdrFooter.Visible = false;
            EditCssPlaceHolder.Visible = true;
            EditCssTextBox.Focus();
            EditCssTextBox.Text = File.ReadAllText(Constants.Constants.GetFilePathCSS(SurveyId) + ddlTemplate.SelectedItem.Text);
        }

        protected void LanguagesDropdownlist_SelectedIndexChanged1(object sender, EventArgs e)
        {
            BindFields();
        }

        protected void TemplatesDropdownlist_SelectedIndexChanged(object sender, EventArgs e)
        {   
            if(ddlTemplate.SelectedIndex != 0)
            {
            cssBtnID.Visible = true;
            }
            else
            {
                cssBtnID.Visible = false;
            }

        }


    }
}