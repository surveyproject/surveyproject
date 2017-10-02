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
            t.CssClass = "questTbl";
           return t;
       }

        //TODO SP25
        //Central Question Panel
        public static Panel GetCentPercentPanel()
        {
            Panel p = new Panel();
            p.CssClass = "questTbl";
            return p;
        }

        //Central Question table
        public static Table GetSectPercentTable()
        {
            Table t = new Table();
            t.ID = "SectTbl";
            t.CssClass = "sectTbl";
            return t;
        }

        //TODO SP25
        //Central Question Panel
        public static Panel GetSectPercentPanel()
        {
            Panel t = new Panel();
            t.ID = "SectPnl";
            t.CssClass = "sectTbl";
            return t;
        }

        //Central Answer table
        public static Table GetAnswerPercentTable()
        {
            Table at = new Table();
            at.ID = "AnswerTbl";
            at.CssClass = "answerTbl";
            return at;
        }

        //TODO SP25
        //Central Answer table
        public static Panel GetAnswerPercentPanel()
        {
            Panel at = new Panel();
            at.ID = "AnswerPnl";
            at.CssClass = "answerTbl";
            return at;
        }



        // Central Main table
        public static Table GetMainPercentTable()
        {
            Table mt = new Table();
            mt.ID = "MainTbl";
            mt.CssClass = "mainTbl";
            return mt;
        }

        //TODO  SP25
        //Central Main Panel
        public static Panel GetMainPercentagePanel()
        {
            Panel mp = new Panel();
            mp.ID = "MainPnl";
            mp.CssClass = "mainTbl";
            return mp;
        }

    }
}
