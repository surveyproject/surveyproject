namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;

    /// <summary>
    /// Result bar control to render a results bar
    /// </summary>
    public class ResultsBar : WebControl, INamingContainer
    {
        private string _barColor = null;
        private Style _itemStyle;
        private int _progress = 0;
        private Unit _tableWidth = 150;

        protected override void Render(HtmlTextWriter output)
        {
            double num4 = this.TableWidth.Value;
            double num = Math.Round((double) ((70.0 * num4) / 100.0));
            double num2 = Math.Round((double) ((this.Progress * num) / 100.0));
            double num3 = Math.Round((double) (num - num2));
            Table child = new Table();
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            child.CellPadding = 0;
            child.CellSpacing = 0;
            child.BorderWidth = 0;
            child.Width = Unit.Pixel((int) num);
            Image image = new Image();
            if (this.BarColor == null)
            {
                image.ImageUrl = GlobalConfig.ImagesPath + "bar/BarOn_red.gif";
            }
            else
            {
                image.ImageUrl = GlobalConfig.ImagesPath + "bar/BarOn_" + this.BarColor + ".gif";
            }
            image.Width = Unit.Pixel((int) num2);
            image.Height = 14;
            cell.Controls.Add(image);
            image = new Image();
            image.ImageUrl = GlobalConfig.ImagesPath + "bar/BarOff.gif";
            image.Width = Unit.Pixel((int) num3);
            image.Height = 14;
            cell.Controls.Add(image);
            row.ControlStyle.CopyFrom(this.ItemStyle);
            row.Cells.Add(cell);
            cell = new TableCell();
            if (this.Progress == 0)
            {
                cell.Text = "&nbsp;&nbsp;0%";
            }
            else
            {
                cell.Text = "&nbsp;&nbsp;" + this.Progress.ToString("##.##") + "%";
            }
            cell.HorizontalAlign = HorizontalAlign.Right;
            row.Cells.Add(cell);
            child.Controls.Add(row);
            this.Controls.Add(child);
            child.RenderControl(output);
        }

        public string BarColor
        {
            get
            {
                return this._barColor;
            }
            set
            {
                this._barColor = value;
            }
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
        /// Current progress of the bar out of 100%
        /// </summary>
        public int Progress
        {
            get
            {
                return this._progress;
            }
            set
            {
                this._progress = value;
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

