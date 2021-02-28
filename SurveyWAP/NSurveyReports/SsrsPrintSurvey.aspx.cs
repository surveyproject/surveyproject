using System;
using System.Data;
using System.Data.SqlClient;
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
        /// reportViewer control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected Microsoft.Reporting.WebForms.ReportViewer reportViewer;

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
            reportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;

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
            Microsoft.Reporting.WebForms.ReportDataSource datasource = new Microsoft.Reporting.WebForms.ReportDataSource("dataset", dataset.Tables["ReportData"]);
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

            string query = "select S.surveyID as ID,  " +
            "S.title as [Title],  " +
            "isnull(S.friendlyname, '-') as [Friendly_URL],  " +
            "isnull(CONVERT(VARCHAR(10), S.opendate, 120)  , '-') as [Date_Open], " +
            "isnull(CONVERT(VARCHAR(10), S.closedate, 120) , '-') as [Date_Closed],  " +
            "case when S.activated = 1 then 'Active' else 'Inactive' end as Status,  " +
            "S.surveydisplaytimes as [NrDisplayed],  " +
            "q.surveyID as QsID,  " +
            "q.questionID as QqID,  " +
            "q.pagenumber as [Page],  " +
            "q.displayorder as [Order], " +
            "Case when q.questionID in (select parentquestionID from vts_tbquestion) " +
            "then 'Matrix : ' + q.questiontext " +
            "when q.parentquestionID is not null " +
            "then 'Sub(' + cast(q.parentquestionID as varchar(50)) + ') - ' + q.Questiontext " +
            "else q.questiontext " +
            "end as QuestionText, " +
            "a.answerID AaID,  " +
            "a.questionID AqID,  " +
            "a.displayorder as Aorder,  " +
            "Case when q.questionID in (select parentquestionID from vts_tbquestion)  " +
            "then '-'  " +
            "else a.answertext  " +
            "end as AOption,  " +
            "at.AnswerTypeID as ATatID,  " +
            "at.Description " +
            "from vts_tbsurvey S left join vts_tbquestion Q on S.surveyID = Q.SurveyID  " +
            "left join vts_Tbanswer a on q.questionID = a.questionID  " +
            "left join vts_tbAnswerType at on a.answertypeID = at.answertypeID " +
            "where s.surveyID = " + SurveyId.ToString() +
            "order by s.surveyID,  " +
            "Q.pagenumber,  " +
            "Q.DisplayOrder,  " +
            "A.displayorder,  " +
            "A.answerID";

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


        /// <summary>
        /// deploy the native assembly SqlServerSpatial140.dll
        /// under the SqlServerTypes\x86 and SqlServerTypes\x64 subdirectories
        /// </summary>
        static bool _isSqlTypesLoaded = false;

        /// <summary>
        /// deploy the native assembly SqlServerSpatial140.dll
        /// </summary>
        public SsrsPrintSurvey()
        {
            if (!_isSqlTypesLoaded)
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
                _isSqlTypesLoaded = true;
            }

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