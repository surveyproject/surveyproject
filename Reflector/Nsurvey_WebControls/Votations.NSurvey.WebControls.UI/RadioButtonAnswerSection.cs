
namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Section of answer with radio selection
    /// mode
    /// </summary>
    public class RadioButtonAnswerSection : AnswerSection
    {
        /// <summary>
        /// Builds a vertical table containing all the 
        /// answer items
        /// </summary>
        /// <returns>An horizontal table with all the answer items</returns>
        private Panel BuildHorizontalSelectionLayout()
        {
            //Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetAnswerPercentTable();//JJ;
            //TableRow row = new TableRow();
            Panel panel = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetAnswerPercentPanel();//JJ; 
            Panel panelrow = new Panel();

            int num = 1;
            foreach (AnswerItem item in base.Answers)
            {
                //TableCell cell = new TableCell();
                Panel panelcell = new Panel();
                //cell.VerticalAlign = VerticalAlign.Top;
                panelcell.CssClass = "cellValign";                

                panelcell.Controls.Add(item);
                panelrow.Controls.Add(panelcell);
                //cell.Width = GetCellWidth(this.ColumnsNumber);//JJ
                panelcell.Width = GetCellWidth(this.ColumnsNumber); //SP25
                panelcell.Style.Value = "float:left;";

                num++;

                if ((base.ColumnsNumber != 0) && (num > base.ColumnsNumber))
                {
                    panel.ControlStyle.CopyFrom(base.AnswerStyle);
                    panel.Controls.Add(panelrow);
                    num = 1;
                    //row = new TableRow();
                    panelrow = new Panel();
                    //panelrow.Style.Value = "display:inline-block; vertical-align:top; width:100%;";
                }

            }
            panel.ControlStyle.CopyFrom(base.AnswerStyle);
            panel.Controls.Add(panelrow);

            return panel;
        }

        /// <summary>
        /// Builds a vertical table containing all the 
        /// answer items
        /// </summary>
        /// <returns>An horizontal table with all the answer items</returns>
        private Panel BuildVerticalSelectionLayout()
        {
            //Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetAnswerPercentTable();//JJ;
           Panel panel = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetAnswerPercentPanel();

            int num = (base.ColumnsNumber == 0) ? base.Answers.Count : Convert.ToInt32(Math.Ceiling(((double) base.Answers.Count) / ((double) base.ColumnsNumber)));
            int num2 = 0;
            int num3 = 0;
            
            for (int i = 0; num3 < num; i = 0)
            {
                //TableRow row = new TableRow();
                Panel panelrow = new Panel();
                panelrow.Style.Value = "display:inline-block; vertical-align:top; ";
                panelrow.Width = GetCellWidth(this.ColumnsNumber);

                while (i <= base.ColumnsNumber)
                {
                    //TableCell cell = new TableCell();
                    //cell.Width = GetCellWidth(this.ColumnsNumber);//JJ

                    Panel panelcell = new Panel();
                    ////panelcell.Width = GetCellWidth(this.ColumnsNumber);

                    //cell.VerticalAlign = VerticalAlign.Top;
                    panelcell.CssClass = "cellValign";

                    if (num2 < base.Answers.Count)
                    {
                        ////panelcell.Width = GetCellWidth(this.ColumnsNumber);
                        panelcell.Controls.Add(base.Answers[num2]);
                    }

                    panelrow.Controls.Add(panelcell);

                    i++;
                    num2 += num;
                }

                panel.ControlStyle.CopyFrom(base.AnswerStyle);
                panel.Controls.Add(panelrow);

                num3++;
                num2 = num3;

            }

            return panel;
        }

        /// <summary>
        /// Build a selection layout with
        /// the question's answeritem child controls
        /// </summary>
        protected override Control GenerateSection()
        {
            if (base.LayoutMode == QuestionLayoutMode.Horizontal)
            {
                return this.BuildHorizontalSelectionLayout();
            }
            return this.BuildVerticalSelectionLayout();
        }
    }
}

