using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Votations.NSurvey.BE.Votations.NSurvey.Constants
{
 public  static class Commons
    {
       public static Table GetCentPercentTable()
       {
           Table t = new Table();
           t.Width = new Unit(100.00, UnitType.Percentage);
           t.Attributes.Add("style","table-layout:fixed;");
           return t;
       }
    }
}
