namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Section of answer with checkbox selection
    /// mode
    /// </summary>
    public class CheckBoxAnswerSection : AnswerSection
    {
        /// <summary>
        /// Builds a vertical table containing all the 
        /// answer items
        /// </summary>
        /// <returns>An horizontal table with all the answer items</returns>

        //private Table BuildHorizontalSelectionLayout()
        private Panel BuildHorizontalSelectionLayout()
        {
            //Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetAnswerPercentTable();//JJ
            //TableRow row = new TableRow();
            //TODO SP25
            Panel panel = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetAnswerPercentPanel();//GB
            Control panelrow = new Panel();

            int num = 1;
            foreach (AnswerItem item in base.Answers)
            {
                //TableCell cell = new TableCell();
                //cell.VerticalAlign = VerticalAlign.Top;
                //cell.Controls.Add(item);
                //row.Cells.Add(cell);
                //TODO SP25
                panel.Controls.Add(item);

                num++;
                if ((base.ColumnsNumber != 0) && (num > base.ColumnsNumber))
                {
                    //table.Rows.Add(row);
                    //num = 1;
                    //row = new TableRow();
                    //TODO SP25
                    panel.Controls.Add(panelrow);
                    num = 1;
                    panelrow = new Panel();

                }
            }
            //row.ControlStyle.CopyFrom(base.AnswerStyle);
            //TODO
            panel.ControlStyle.CopyFrom(base.AnswerStyle);

            //table.Rows.Add(row);
            //TODO SP25
            panel.Controls.Add(panelrow);            

            //return table;
            //TODO SP25
            return panel;
        }

        /// <summary>
        /// Builds a vertical table containing all the 
        /// answer items
        /// </summary>
        /// <returns>An horizontal table with all the answer items</returns>
        //private Table BuildVerticalSelectionLayout()
        private Panel BuildVerticalSelectionLayout()
        {
            //Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetAnswerPercentTable();//JJ;
            //TODO SP25
            Panel panel = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetAnswerPercentPanel();//GB

            int num = (base.ColumnsNumber == 0) ? base.Answers.Count : Convert.ToInt32(Math.Ceiling(((double) base.Answers.Count) / ((double) base.ColumnsNumber)));
            int num2 = 0;
            int num3 = 0;
            for (int i = 0; num3 < num; i = 0)
            {
                //TableRow row = new TableRow();
                Control panelrow = new Panel();

                while (i <= base.ColumnsNumber)
                {
                    //TableCell cell = new TableCell();
                    //TODO SP25
                    Control panelcell = new Control();

                    //cell.VerticalAlign = VerticalAlign.Top;

                    if (num2 < base.Answers.Count)
                    {
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

            //return table;
            //TODO SP25
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

