using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.Data;

namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class SurveyTokenSecurity : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int sId = SurveyId;//This causes a redirection if survey id is not set
            UITabList.SetSecurityTabs((MsterPageTabs)Page.Master, UITabList.SecurityTabs.Token);
            MessageLabel.Text = "";
            LocalizePage();
            if (!IsPostBack)
            {

                BindFields();

                SetupSecurity();
            }
        }

        private void SetupSecurity()
        {
            CheckRight(Data.NSurveyRights.TokenSecurityRight, true);
        }


        IEnumerable<SurveyTokenData.SurveyTokenRow> GetFilteredSelection()
        {
            SurveyTokenData data = new SurveyToken().GetAllSurveyTokens(SurveyId);
            lblTokensCreated.Text = data.SurveyToken.Count.ToString();
            lblTokensUsed.Text = data.SurveyToken.Where(x => x.Used).Count().ToString();
            lblTokensAvailable.Text = data.SurveyToken.Where(x => !x.Used).Count().ToString();
            DateTime dateFilter = DateTime.Today;

            if (!string.IsNullOrEmpty(txtIssuedOn.Text) && !DateTime.TryParse(txtIssuedOn.Text, out dateFilter))
            {
                ShowErrorMessage(MessageLabel, string.Format(GetPageResource("InvalidCalendarDateMessage"), txtIssuedOn.Text, DateTime.Today.ToShortDateString()));
                return null;
            }

            return data.SurveyToken.Where(x => (string.IsNullOrEmpty(txtToken.Text) || x.Token.Contains(txtToken.Text)) &&
                                        ((x.Used && ddlTokenOptions.SelectedValue == "Used") ||
                                        (!x.Used && ddlTokenOptions.SelectedValue == "Unused") ||
                                        (ddlTokenOptions.SelectedValue == "All")) &&
                                        (string.IsNullOrEmpty(txtIssuedOn.Text) || x.CreationDate == dateFilter)).ToList();
        }
        private void BindFields()
        {
            var filteredRows = GetFilteredSelection();
            if (filteredRows == null) return;
            lvTokens.DataSource = filteredRows;
            lvTokens.DataBind();
        }

        private void LocalizePage()
        {
            literalTokenSecurityTitle.Text = GetPageResource("TokenSecurityFormTitle");
            lblGenerateCountPrompt.Text = GetPageResource("lblGenerate");
            lblTokensAvailPrompt.Text = GetPageResource("lblNumberOfTokensAvailable");
            lblTokensCreatedPrompt.Text = GetPageResource("lblNumberOfTokensCreated");
            lblTokensUsedPrompt.Text = GetPageResource("lblNumberOfTokensUsed");
            ltFilterToken.Text = GetPageResource("lblByToken");
            ltFilterType.Text = GetPageResource("lblByTokenType");
            ltIssuedOn.Text = GetPageResource("lblByIssuedDate");
            btnApplyFilter.Text = GetPageResource("lblApplyFilter");
            btnDeleteAll.Text = GetPageResource("lblDeleteAll");
            btnDeleteSelected.Text = GetPageResource("lblDeleteSelected");
            btnExport.Text = GetPageResource("lblExportTokens");
            btnGenerate.Text = GetPageResource("lblGenerate");
           
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int count;
            if (int.TryParse(txtNumTokens.Text, out count) && count > 0 && count < 100000)
                new SurveyToken().AddSurveyTokensMultiple(SurveyId, DateTime.Today, count);
            else
                ShowErrorMessage(MessageLabel, GetPageResource("ValidNumberPlease"));
            BindFields();

        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            List<int> deleteCandidates = new List<int>();

            foreach (var row in lvTokens.Items)
                if (((CheckBox)row.FindControl("chkDelete")).Checked)
                    deleteCandidates.Add(Convert.ToInt32(((Literal)row.FindControl("ltTokenId")).Text));

            if (deleteCandidates.Count > 0)
            {
                new SurveyToken().DeleteSurveyTokensByIdMultiple(deleteCandidates);
                BindFields();
            }
        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            BindFields();
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            var filteredRows = GetFilteredSelection();
            if (filteredRows == null) return;
           
            IList<int> deleteCandidates = filteredRows.Select(x => x.TokenId).ToList();
            if (deleteCandidates.Count() > 0)
            {
                new SurveyToken().DeleteSurveyTokensByIdMultiple(deleteCandidates);
                BindFields();
            }
        }

        protected void lvTokens_PagePropertiesChanged(object sender, EventArgs e)
        {
            BindFields();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=\"dataexport.csv\"");

            // Writes the UTF 8 header
            byte[] BOM = new byte[] { 0xef, 0xbb, 0xbf };
            Response.BinaryWrite(BOM);

            // Export the data
            string csv = ExportCSV();
            if (csv == null) return;
            Response.Write(csv);
            Response.End();
        }

        private string ExportCSV()
        {
            var filteredRows = GetFilteredSelection();
            if (filteredRows == null) return null;
            string retval=string.Empty;
            foreach(var row in  filteredRows)
            retval+=(row.CreationDate.ToShortDateString() + "," + row.Token + "," +( row.Used ? "Used" : "Unused") + Environment.NewLine);
            return retval;
        }

    }
}