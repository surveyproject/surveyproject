using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.BusinessRules;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.Data;
using Votations.NSurvey.Enums;
using Votations.NSurvey.WebAdmin.UserControls;
using Votations.NSurvey.Web;
using Votations.NSurvey.WebAdmin.NSurveyAdmin;
namespace Votations.NSurvey.WebAdmin.NSurveyAdmin
{
    public partial class DataImport : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Visible = false;
            UITabList.SetResultsTabs((MsterPageTabs)Page.Master, UITabList.ResultTabs.DataImport);
            int surveyId = ((PageBase)Page).SurveyId;
            SetupSecurity();
            LocalizePage();
            if (!IsPostBack)
            {
                txtFrom.Text = new DateTime(2003, 12, 31).ToShortDateString();
                txtTo.Text = DateTime.Today.ToShortDateString();
            }
            MessageLabel.Visible = false;
        }

        private void LocalizePage()
        {
            this.DataImportTitle.Text = GetPageResource("DataImportTitle");
            importFromLabel.Text = GetPageResource("FromLabel");
            importTolabel.Text = GetPageResource("ToLabel");
            importFilelabel.Text = GetPageResource("ImportFile");
            dataSelectionLabel.Text=GetPageResource("DataSelection");
            rbAll.Text = GetPageResource("CSVEXPGetAllDates");
            rbSelRange.Text = GetPageResource("CSVExpGetSelectedDates");
        }

        public void btnImport_Click(Object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(fupDataFile.FileName))
            {
                ((PageBase)Page).ShowErrorMessage(MessageLabel, GetPageResource("SelectAFile"));
                MessageLabel.Visible = true;
                return;

            }
            if (this.fupDataFile.FileName != null)
            {
                NSurveyVoter voterData = new NSurveyVoter();

                try
                {
                    voterData.ReadXml(System.Xml.XmlReader.Create(fupDataFile.PostedFile.InputStream, (new System.Xml.XmlReaderSettings() { XmlResolver = null })));
                    if (rbSelRange.Checked)
                    {
                        DateTime start = DateTime.Parse(txtFrom.Text);
                        DateTime end = DateTime.Parse(txtTo.Text);
                        NSurveyVoter vd = new NSurveyVoter();

                        voterData.Voter.Where(x => x.StartDate >= start && x.StartDate <= end).ToList().
                            ForEach(x => vd.Voter.ImportRow(x));
                        var voterIds = vd.Voter.Select(y => y.VoterID).ToList();
                        voterData.Question.Where(x => voterIds.Contains(x.VoterID)).ToList().
                            ForEach(x => vd.Question.ImportRow(x));
                        voterData.Answer.Where(x => voterIds.Contains(x.VoterId)).ToList().
                            ForEach(x => vd.Answer.ImportRow(x));
                        voterData = vd;
                    }
                    new Voter().ImportVoter(SurveyId, voterData);
                    ((PageBase)Page).ShowNormalMessage
                        (MessageLabel, GetPageResource("ImportSuccessMsg"));
                }
                catch (Exception ex)
                {
                    ((PageBase)Page).ShowErrorMessage(MessageLabel, ex.Message);
                    MessageLabel.Visible = true;
                }
            }
        }



        private void SetupSecurity()
        {
            CheckRight(Data.NSurveyRights.DataImportRight, true);
        }
    }
}