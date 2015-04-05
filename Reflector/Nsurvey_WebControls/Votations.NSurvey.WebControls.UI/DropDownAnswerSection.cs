namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Data;

    /// <summary>
    /// Section of answer with dropdown selection
    /// mode
    /// </summary>
    public class DropDownAnswerSection : AnswerSection
    {
        private AnswerListItem _answerListItem = new AnswerListItem();
        private AnswerItemCollection _nonSelectableItems = new AnswerItemCollection();

        /// <summary>
        /// Build an horizontal list layout with
        /// the question's answeritem child controls
        /// </summary>
        private Table BuildHorizonalListLayout()
        {
            TableCell cell;
            int num;
            Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;
            TableRow row = new TableRow();
            this.InitListItems();
            if (this._answerListItem.AnswerItems.Count > 0)
            {
                cell = new TableCell();
                cell.VerticalAlign = VerticalAlign.Top;
                cell.Controls.Add(this._answerListItem);
                row.Cells.Add(cell);
                num = 2;
            }
            else
            {
                num = 1;
            }
            foreach (AnswerItem item in this._nonSelectableItems)
            {
                cell = new TableCell();
                cell.VerticalAlign = VerticalAlign.Top;
                cell.Controls.Add(item);
                row.Cells.Add(cell);
                num++;
                if ((base.ColumnsNumber != 0) && (num > base.ColumnsNumber))
                {
                    row.ControlStyle.CopyFrom(base.AnswerStyle);
                    table.Rows.Add(row);
                    num = 1;
                    row = new TableRow();
                }
            }
            if (row.Cells.Count > 0)
            {
                row.ControlStyle.CopyFrom(base.AnswerStyle);
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// Build an vertical list layout with
        /// the question's answeritem child controls
        /// </summary>
        private Table BuildVerticalListLayout()
        {
            int num4;
            Table table = Votations.NSurvey.BE.Votations.NSurvey.Constants.Commons.GetCentPercentTable();//JJ;
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            this.InitListItems();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            if (this._answerListItem.AnswerItems.Count > 0)
            {
                num4 = (base.ColumnsNumber == 0) ? (this._nonSelectableItems.Count + 1) : Convert.ToInt32(Math.Ceiling(((double) (this._nonSelectableItems.Count + 1)) / ((double) base.ColumnsNumber)));
                cell = new TableCell();
                cell.VerticalAlign = VerticalAlign.Top;
                cell.Controls.Add(this._answerListItem);
                row.Cells.Add(cell);
                num += num4 - 1;
                if (this._nonSelectableItems.Count == 0)
                {
                    row.ControlStyle.CopyFrom(base.AnswerStyle);
                    table.Rows.Add(row);
                }
            }
            else
            {
                num4 = (base.ColumnsNumber == 0) ? this._nonSelectableItems.Count : Convert.ToInt32(Math.Ceiling(((double) this._nonSelectableItems.Count) / ((double) base.ColumnsNumber)));
            }
            while (num2 < num4)
            {
                while (num3 <= base.ColumnsNumber)
                {
                    cell = new TableCell();
                    cell.VerticalAlign = VerticalAlign.Top;
                    if (num < this._nonSelectableItems.Count)
                    {
                        cell.Controls.Add(this._nonSelectableItems[num]);
                    }
                    row.Cells.Add(cell);
                    num3++;
                    num += num4;
                }
                row.ControlStyle.CopyFrom(base.AnswerStyle);
                table.Rows.Add(row);
                row = new TableRow();
                num2++;
                num = num2;
                if (this._answerListItem.AnswerItems.Count > 0)
                {
                    num--;
                }
                num3 = 0;
            }
            return table;
        }

        /// <summary>
        /// Build a selection layout with
        /// the question's answeritem child controls
        /// </summary>
        protected override Control GenerateSection()
        {
            this._answerListItem.SectionContainer = this;
            this._answerListItem.LanguageCode = base.LanguageCode;
            if (base.LayoutMode == QuestionLayoutMode.Horizontal)
            {
                return this.BuildHorizonalListLayout();
            }
            return this.BuildVerticalListLayout();
        }

        private void InitListItems()
        {
            this._answerListItem.ID = string.Concat(new object[] { GlobalConfig.AnswerItemName, base.QuestionId, GlobalConfig.AnswerSectionName, base.SectionUid });
            this._answerListItem.QuestionId = base.QuestionId;
            foreach (AnswerItem item in base.Answers)
            {
                AnswerSelectionItem answerItem = item as AnswerSelectionItem;
                if ((answerItem != null) && (answerItem.SelectionMode == AnswerSelectionMode.ListItem))
                {
                    this._answerListItem.AnswerItems.Add(answerItem);
                }
                else
                {
                    this._nonSelectableItems.Add(item);
                }
            }
        }
    }
}

