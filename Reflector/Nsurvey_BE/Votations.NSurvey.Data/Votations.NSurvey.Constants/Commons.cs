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
        //Central Question table
       public static Table GetCentPercentTable()
       {
           Table t = new Table();
           //t.Width = new Unit(100.00, UnitType.Percentage);
            //t.ID = "QuestTbl";
            t.CssClass = "questTbl";
           //t.Attributes.Add("style","table-layout:fixed;");
           return t;
       }

        //Central Question table
        public static Table GetSectPercentTable()
        {
            Table t = new Table();
            //t.Width = new Unit(100.00, UnitType.Percentage);
            t.ID = "SectTbl";
            t.CssClass = "sectTbl";
            //t.Attributes.Add("style","table-layout:fixed;");
            return t;
        }

        //Central Answer table
        public static Table GetAnswerPercentTable()
        {
            Table at = new Table();
            //t.Width = new Unit(100.00, UnitType.Percentage);
            at.ID = "AnswerTbl";
            at.CssClass = "answerTbl";
            //t.Attributes.Add("style","table-layout:fixed;");
            return at;
        }

        // Central Main table
        public static Table GetMainPercentTable()
        {
            Table mt = new Table();
            //t.Width = new Unit(100.00, UnitType.Percentage);
            mt.ID = "MainTbl";
            mt.CssClass = "mainTbl";
            //t.Attributes.Add("style","table-layout:fixed;");
            return mt;
        }
    }
}
