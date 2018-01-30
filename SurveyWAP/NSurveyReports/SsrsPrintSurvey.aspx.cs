using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.Data;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.SQLServerDAL;

namespace Votations.NSurvey.WebAdmin.NSurveyReports
{
    /// <summary>
    /// SSRS Report Codebehind to connect to the SP DB and get query based data to fill the .rdlc based report
    /// This particular report creates and presents an overview of a surveys questions and answers 
    /// Includes the option to print a hardcopy
    /// </summary>
    public partial class SsrsPrintSurvey : PageBase
    {
        /// <summary>
        /// load page and set the conditions to fill and run the SSRS Report Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupSecurity();
            LocalizePage();
            reportViewer.Visible = true;

            if (!Page.IsPostBack)
            {
                // show survey DDL and bind surveys
                ShowSurveyDDL();

                var surveys = new Surveys().GetAssignedSurveysList(((PageBase)Page).NSurveyUser.Identity.UserId);
                if (surveys.Surveys.Count == 1)
                { reportViewer.Visible = true; } else
                { reportViewer.Visible = false; }

            }
        }

        /// <summary>
        /// Create/ add report datasource and dataset
        /// </summary>
        private void CreateReport()
        {
            // SSRS REPORT
            // Set the processing mode for the ReportViewer to Local  
            reportViewer.ProcessingMode = ProcessingMode.Local;

            // Reference to .rdl(c) file as created through SSRS and copied to SP directory NSurveyReports/
            // Can be set on the .aspx page as well: reportviewer object
            //reportViewer.LocalReport.ReportPath = "NSurveyReports/SsrsReport.rdlc";

            // Dataset
            // Option a. create new dataset
            DataSet dataset = new DataSet();

            //Q1 Method to fill the dataset with data in case of option a.
            GetReportData(ref dataset);

            //Option b. use existing SP Dataset from BE project - VoterData
            // reference BLL methods - DAL - SqlserverDAL - DB sproc
            //DataSet dataset = new Voters().GetAllVotersSsrsTest();

            // Datasource - name must match .rdlc datasource entry
            // Option A. ds
            ReportDataSource datasource = new ReportDataSource("dataset", dataset.Tables["ReportData"]);
            // Option B. ds
            //ReportDataSource datasource = new ReportDataSource("dataset", dataset.Tables[0]);

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(datasource);
        }

        //Q1 SP if using option A. Dataset - less secure (!)
        /// <summary>
        /// Report Query and DB connection
        /// </summary>
        /// <param name="dataset"></param>
        private void GetReportData(ref DataSet dataset)
        {
            //Query to get the data to fill the dataset - (also see .rdlc file query)

            string query = "select S.surveyid as ID,  " +
            "S.title as [Title],  " +
            "isnull(S.friendlyname, '-') as [Friendly_URL],  " +
            "isnull(CONVERT(VARCHAR(10), S.opendate, 120)  , '-') as [Date_Open], " +
            "isnull(CONVERT(VARCHAR(10), S.closedate, 120) , '-') as [Date_Closed],  " +
            "case when S.activated = 1 then 'Active' else 'Inactive' end as Status,  " +
            "S.surveydisplaytimes as [NrDisplayed],  " +
            "q.surveyid as Qsid,  " +
            "q.questionid as Qqid,  " +
            "q.pagenumber as [Page],  " +
            "q.displayorder as [Order], " +
            "Case when q.questionid in (select parentquestionid from vts_tbquestion) " +
            "then 'Matrix : ' + q.questiontext " +
            "when q.parentquestionid is not null " +
            "then 'Sub(' + cast(q.parentquestionid as varchar(50)) + ') - ' + q.Questiontext " +
            "else q.questiontext " +
            "end as QuestionText, " +
            "a.answerid Aaid,  " +
            "a.questionid Aqid,  " +
            "a.displayorder as Aorder,  " +
            "Case when q.questionid in (select parentquestionid from vts_tbquestion)  " +
            "then '-'  " +
            "else a.answertext  " +
            "end as AOption,  " +
            "at.AnswerTypeID as ATatid,  " +
            "at.Description " +
            "from vts_tbsurvey S left join vts_tbquestion Q on S.surveyid = Q.SurveyId  " +
            "left join vts_Tbanswer a on q.questionid = a.questionid  " +
            "left join vts_tbAnswerType at on a.answertypeid = at.answertypeid " +
            "where s.surveyid = " + SurveyId.ToString() +
            "order by s.surveyid,  " +
            "Q.pagenumber,  " +
            "Q.DisplayOrder,  " +
            "A.displayorder,  " +
            "A.answerid";

            //SP DB connection
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = DbConnection.NewDbConnectionString
            };

            SqlCommand command =
                new SqlCommand(query, connection);

            SqlDataAdapter ssrsAdapter = new
                SqlDataAdapter(command);

            ssrsAdapter.Fill(dataset, "ReportData");
        }


        // Survey DDL code from TakeSurvey.asxc


        /// <summary>
        /// Security: authorisation and access checks
        /// </summary>
        private void SetupSecurity()
        {
            CheckRight(NSurveyRights.AccessSsrsReports, true);
        }

        /// <summary>
        /// Multilanguage localisation
        /// </summary>
        private void LocalizePage()
        {
            ChooseSurveyLabel.Text = ((PageBase)Page).GetPageResource("ChooseSurveyLabel");
        }
        

        private void InitiateSurvey(int surveyId = -1)
        {
            if (surveyId > -1 || (ddlSurveys.Visible == true && ddlSurveys.SelectedValue != "-1"))
            {
                ((PageBase)Page).SurveyId = surveyId > -1 ? surveyId : int.Parse(ddlSurveys.SelectedValue);
            }
            else return;

            CreateReport();

            SurveyId = ((PageBase)Page).getSurveyId();

        }

        /// <summary>
        /// Drop down list to select a surveyid: ID used in query to retrieve data from DB
        /// </summary>
        private void ShowSurveyDDL()
        {
            var surveys = new Surveys().GetAssignedSurveysList(((PageBase)Page).NSurveyUser.Identity.UserId);

            if (surveys.Surveys.Count == 1) { InitiateSurvey(surveys.Surveys[0].SurveyId); return; }
            if (surveys.Surveys.Count == 0) { int s = ((PageBase)Page).SurveyId; return; }
            ddlSurveys.Items.Clear();
            ddlSurveys.Items.Add(new ListItem(((PageBase)Page).GetPageResource("DDLSelectValue"), "-1"));
            ddlSurveys.AppendDataBoundItems = true;
            ddlSurveys.DataSource = surveys.Surveys;
            ddlSurveys.DataTextField = "Title";
            ddlSurveys.DataValueField = "SurveyId";
            ddlSurveys.DataBind();
            ddlSurveys.Visible = true;
            ChooseSurveyLabel.Visible = true;
        }

        /// <summary>
        /// Action on selecting an option from the (survey) Drop Down List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSurveys_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlSurveys.SelectedValue != "-1")
            {
                ((PageBase)Page).SurveyId = int.Parse(ddlSurveys.SelectedValue);
                InitiateSurvey(int.Parse(ddlSurveys.SelectedValue));
            }
        }

        /// <summary>
        /// Click button action to go back to SSRS Report Overview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnBackButton(object sender, CommandEventArgs e)
        {
            Response.Redirect(UINavigator.SSRSReportHyperlink);
        }

    }
}