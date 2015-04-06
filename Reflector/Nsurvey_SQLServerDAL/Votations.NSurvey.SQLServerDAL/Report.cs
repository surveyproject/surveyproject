using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

//using Microsoft.ApplicationBlocks.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Votations.NSurvey.Data;
using Votations.NSurvey.IDAL;
using System.Linq;

namespace Votations.NSurvey.SQLServerDAL
{
    public class Report: IReport
    {

        /// <summary>
        /// Returns all the scores for the report
        /// </summary>
        /// <param name="surveyId"></param>
        /// <param name="_voterId"></param>
        /// <returns></returns>
        public DataSet GetReportScores(int surveyId, int _voterId)
        {

            ArrayList commandParameters = new ArrayList();
            {
                commandParameters.Add(new SqlParameter("@SurveyId", surveyId).SqlValue);
                commandParameters.Add(new SqlParameter("@VoterId", _voterId).SqlValue);
            }

            return DbConnection.db.ExecuteDataSet("vts_spReportGetScoresDws", commandParameters.ToArray());
        }
    }
}
