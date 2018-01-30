using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Votations.NSurvey.DataAccess;
using Votations.NSurvey.SQLServerDAL;

namespace Votations.NSurvey.WebAdmin.NSurveyReports
{
    public partial class SsrsReport : System.Web.UI.Page
    {
        /// <summary>
        /// load page and set the conditions to fill and run the SSRS Report Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set the processing mode for the ReportViewer to Local  
                reportViewer.ProcessingMode = ProcessingMode.Local;

                // Reference to .rdl(c) file as created through SSRS and copied to SP directory NSurveyReports/
                // Can be set on the .aspx page as well: reportviewer object
                //reportViewer.LocalReport.ReportPath = "NSurveyReports/SsrsReport.rdlc";

                // Dataset
                // Option a. create new dataset
                // DataSet dataset = new DataSet();

                //Q1 Method to fill the dataset with data in case of option a.
                //GetVotersData(ref dataset);

                //Option b. use existing SP Dataset from BE project - VoterData
                // reference BLL methods - DAL - SqlserverDAL - DB sproc
                DataSet dataset = new Voters().GetAllVotersSsrsTest();

                // Datasource - name must match .rdlc datasource entry
                // Option A. ds
                //ReportDataSource datasource = new ReportDataSource("dataset", dataset.Tables["Voter"]);
                // Option B. ds
                ReportDataSource datasource = new ReportDataSource("dataset", dataset.Tables[0]);

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(datasource);
            }
        }

        //Q1 SP if using option A. Dataset - less secure (!)
        private void GetVotersData(ref DataSet dataset)
        {
            //Query to get the data to fill the dataset - (copy from SSRS?)
            string sqlVoters =
                "SELECT VoterID, UID, SurveyID, ContextUserName, VoteDate, StartDate, IPSource, Validated, ResumeUID, ResumeAtPageNumber, ProgressSaveDate, ResumeQuestionNumber, ResumeHighestPageNumber, LanguageCode FROM vts_tbvoter";


            //SP DB connection
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = DbConnection.NewDbConnectionString
            };

            SqlCommand command =
                new SqlCommand(sqlVoters, connection);

            SqlDataAdapter ssrsAdapter = new
                SqlDataAdapter(command);

            ssrsAdapter.Fill(dataset, "Voter");
        }


        /// <summary>
        /// deploy the native assembly SqlServerSpatial140.dll
        /// under the SqlServerTypes\x86 and SqlServerTypes\x64 subdirectories
        /// </summary>
        static bool _isSqlTypesLoaded = false;

        /// <summary>
        /// deploy the native assembly SqlServerSpatial140.dll
        /// </summary>
        public SsrsReport()
        {
            if (!_isSqlTypesLoaded)
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~"));
                _isSqlTypesLoaded = true;
            }

        }


    }
}