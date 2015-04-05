
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
        private Table BuildHorizontalSelectionLayout()
        {
            Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;
            TableRow row = new TableRow();
            int num = 1;
            foreach (AnswerItem item in base.Answers)
            {
                TableCell cell = new TableCell();
                cell.VerticalAlign = VerticalAlign.Top;
                cell.Controls.Add(item);
                row.Cells.Add(cell);
                cell.Width = GetCellWidth(this.ColumnsNumber);//JJ
                num++;
                if ((base.ColumnsNumber != 0) && (num > base.ColumnsNumber))
                {
                    row.ControlStyle.CopyFrom(base.AnswerStyle);
                    table.Rows.Add(row);
                    num = 1;
                    row = new TableRow();
                }
            }
            row.ControlStyle.CopyFrom(base.AnswerStyle);
            table.Rows.Add(row);
            table.CellPadding = 2;
            table.CellSpacing = 0;
            return table;
        }

        /// <summary>
        /// Builds a vertical table containing all the 
        /// answer items
        /// </summary>
        /// <returns>An horizontal table with all the answer items</returns>
        private Table BuildVerticalSelectionLayout()
        {
            Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;
            int num = (base.ColumnsNumber == 0) ? base.Answers.Count : Convert.ToInt32(Math.Ceiling(((double) base.Answers.Count) / ((double) base.ColumnsNumber)));
            int num2 = 0;
            int num3 = 0;
            
            for (int i = 0; num3 < num; i = 0)
            {
                TableRow row = new TableRow();
                while (i <= base.ColumnsNumber)
                {
                    TableCell cell = new TableCell();
                    cell.Width = GetCellWidth(this.ColumnsNumber);//JJ
                    cell.VerticalAlign = VerticalAlign.Top;
                    if (num2 < base.Answers.Count)
                    {
                        
                       

                        cell.Width = GetCellWidth(this.ColumnsNumber);
                        cell.Controls.Add(base.Answers[num2]);
                    }
                    row.Cells.Add(cell);
                    i++;
                    num2 += num;
                }
                row.ControlStyle.CopyFrom(base.AnswerStyle);
                table.Rows.Add(row);
                num3++;
                num2 = num3;
            }
            table.CellPadding = 2;
            table.CellSpacing = 0;
            return table;
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

