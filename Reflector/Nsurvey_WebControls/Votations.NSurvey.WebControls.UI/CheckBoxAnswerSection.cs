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
        private Panel BuildHorizontalSelectionLayout()
        {
            Panel panelrow = new Panel();

            int num = 1;
            foreach (AnswerItem item in base.Answers)
            {
                Panel panelcell = new Panel();
                panelcell.CssClass = "cellValign";
                panelcell.Width = GetCellWidth(this.ColumnsNumber); //SP25
                panelcell.Style.Value = "float:left;";
                panelcell.Controls.Add(item);

                panelrow.Controls.Add(panelcell);

                num++;

                if (base.ColumnsNumber == 0)
                {
                    panelrow.Style.Value = "display:flex;";
                }
                else if ((base.ColumnsNumber != 0) && (num > base.ColumnsNumber))
                {
                    Panel panelrowadd = new Panel();
                    panelrow.Controls.Add(panelrowadd);
                    num = 1;   
                }
            }
            panelrow.ControlStyle.CopyFrom(base.AnswerStyle);
            return panelrow;
        }

        /// <summary>
        /// Builds a vertical Panel containing all the 
        /// answer items
        /// </summary>
        /// <returns>An vertical panel with all the answer items</returns>
        private Panel BuildVerticalSelectionLayout()
        {
            Panel panelrow = new Panel();

            int num = (base.ColumnsNumber == 0) ? base.Answers.Count : Convert.ToInt32(Math.Ceiling(((double) base.Answers.Count) / ((double) base.ColumnsNumber)));
            int num2 = 0;
            int num3 = 0;

            for (int i = 0; num3 < num; i = 0)
            {
                while (i <= base.ColumnsNumber)
                {

                    Panel panelcell = new Panel();
                    panelcell.CssClass = "cellValign";

                    if (num2 < base.Answers.Count)
                    {
                        panelcell.Width = GetCellWidth(this.ColumnsNumber);
                        panelcell.Controls.Add(base.Answers[num2]);
                    }

                    panelrow.Controls.Add(panelcell);

                    i++;
                    num2 += num;
                }

                panelrow.ControlStyle.CopyFrom(base.AnswerStyle);

                num3++;
                num2 = num3;
            }
            return panelrow;
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

