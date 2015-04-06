using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Votations.NSurvey;
using Votations.NSurvey.DALFactory;
using Votations.NSurvey.Data;

namespace Votations.NSurvey.DataAccess
{
    /// <summary>
    /// Provides the method to access the reports's data.
    /// </summary>
    public class Reports
    {
        /// <summary>
        /// Returns all the text entries of the voters
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="voterId"></param>
        /// <returns></returns>
        public DataSet GetReportScores(int surveyId, int _voterId)
        {
            return ReportFactory.Create().GetReportScores(surveyId, _voterId);
        }


    }
}
