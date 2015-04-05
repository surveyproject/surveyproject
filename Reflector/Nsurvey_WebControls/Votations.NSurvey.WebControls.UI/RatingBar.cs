namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Result bar control to render a results bar
    /// </summary>
    [ToolboxBitmap(typeof(Bitmap))]
    public class RatingBar : WebControl, INamingContainer
    {
        private Style _itemStyle;
        private double _maxRating = 0.0;
        private double _rating = 0.0;
        private Unit _tableWidth = 150;

        protected override void Render(HtmlTextWriter output)
        {
            double num4 = this.TableWidth.Value;
            double num = Math.Round((double) ((70.0 * num4) / 100.0));
            double num2 = Math.Round((double) ((this.RatingPercent * num) / 100.0));
            double num3 = Math.Round((double) (num - num2));
            Table child = new Table();
            TableRow row = new TableRow();
            TableRow row2 = new TableRow();
            TableCell cell = new TableCell();
            TableCell cell2 = new TableCell();
            TableCell cell3 = new TableCell();
            row2.ControlStyle.CopyFrom(this.ItemStyle);
            cell.ColumnSpan = 2;
            if (this.Rating == 0.0)
            {
                cell.Text = string.Format(ResourceManager.GetString("RatingResults"), this.Rating, this.MaxRating);
            }
            else
            {
                cell.Text = string.Format(ResourceManager.GetString("RatingResults"), this.Rating.ToString("##.##"), this.MaxRating);
            }
            row2.Cells.Add(cell);
            child.Rows.Add(row2);
            cell3.ControlStyle.Font.Size = FontUnit.XXSmall;
            cell3.Text = "&nbsp;" + this.MaxRating.ToString();
            child.CellPadding = 0;
            child.CellSpacing = 3;
            child.BorderWidth = 0;
            child.Width = Unit.Pixel((int) num);
            System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
            image.ImageUrl = GlobalConfig.ImagesPath + "PositiveRatingBar.gif";
            image.Width = Unit.Pixel((int) num2);
            image.Height = 12;
            cell2.Controls.Add(image);
            image = new System.Web.UI.WebControls.Image();
            image.ImageUrl = GlobalConfig.ImagesPath + "NegativeRatingBar.gif";
            image.Width = Unit.Pixel((int) num3);
            image.Height = 12;
            cell2.Controls.Add(image);
            row.ControlStyle.CopyFrom(this.ItemStyle);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            child.Controls.Add(row);
            this.Controls.Add(child);
            child.RenderControl(output);
        }

        public Style ItemStyle
        {
            get
            {
                if (this._itemStyle == null)
                {
                    this._itemStyle = new Style();
                }
                return this._itemStyle;
            }
            set
            {
                this._itemStyle = value;
            }
        }

        /// <summary>
        /// Max. rating
        /// </summary>
        public double MaxRating
        {
            get
            {
                return this._maxRating;
            }
            set
            {
                this._maxRating = value;
            }
        }

        /// <summary>
        /// Current rating
        /// </summary>
        public double Rating
        {
            get
            {
                return this._rating;
            }
            set
            {
                this._rating = value;
            }
        }

        /// <summary>
        /// Rating percent
        /// </summary>
        private double RatingPercent
        {
            get
            {
                if (this.Rating == 0.0)
                {
                    return this.Rating;
                }
                return (100.0 / (this.MaxRating / this.Rating));
            }
        }

        /// <summary>
        /// Width of the table container, used 
        /// to scale the bars to the container size
        /// </summary>
        public Unit TableWidth
        {
            get
            {
                return this._tableWidth;
            }
            set
            {
                this._tableWidth = value;
            }
        }
    }
}

