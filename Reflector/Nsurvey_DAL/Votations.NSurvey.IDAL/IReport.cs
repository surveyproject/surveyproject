using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Runtime.InteropServices;
using Votations.NSurvey.Data;

namespace Votations.NSurvey.IDAL
{
    /// <summary>
    /// Report interface for the Report DAL.
    /// </summary>
    public interface IReport
    {
        DataSet GetReportScores(int surveyId, int _voterId);
    }
}
