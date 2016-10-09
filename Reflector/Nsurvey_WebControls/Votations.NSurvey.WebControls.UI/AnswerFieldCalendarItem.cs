namespace Votations.NSurvey.WebControls.UI
{
    using Microsoft.VisualBasic;
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Calendar example that inherits a textbox behaviors and adds a calendar to
    /// to it
    /// </summary>
    public class AnswerFieldCalendarItem : AnswerFieldItem
    {
        private System.Web.UI.WebControls.Calendar _calendar = new System.Web.UI.WebControls.Calendar();

        /// <summary>
        /// Set the value of the textbox to the selected date
        /// </summary>
        protected virtual void _calendar_SelectionChanged(object sender, EventArgs e)
        {
            base._fieldTextBox.Text = this._calendar.SelectedDate.ToShortDateString();
        }

        protected virtual void AddCalendar()
        {
            Style s = new Style();
            s.CssClass = "calendarStyle";
//           s.Font.Size = FontUnit.XXSmall;
            this._calendar.CssClass = "calendarStyle";
            this._calendar.DayHeaderStyle.CopyFrom(s);
            this._calendar.DayStyle.CopyFrom(s);
            this._calendar.NextPrevStyle.CopyFrom(s);
            this._calendar.OtherMonthDayStyle.CopyFrom(s);
            this._calendar.TitleStyle.CopyFrom(s);
//            this._calendar.Width = Unit.Pixel(80);
//            this._calendar.Height = Unit.Pixel(0x56);
            this._calendar.Visible = false;
            this._calendar.DayNameFormat = DayNameFormat.FirstLetter;
            this._calendar.SelectionChanged += new EventHandler(this._calendar_SelectionChanged);
            this.Controls.Add(this._calendar);
        }

        protected virtual void AddCalendarButton()
        {
            ImageButton child = new ImageButton();
            child.ImageUrl = GlobalConfig.ImagesPath + "calendaricon.gif";
            child.ImageAlign = ImageAlign.Top;
            child.Width = Unit.Pixel(18);
            child.Height = Unit.Pixel(18);
            child.Click += new ImageClickEventHandler(this.calendarButton_Click);
            this.Controls.Add(child);
        }

        protected virtual void calendarButton_Click(object sender, ImageClickEventArgs e)
        {
            this._calendar.Visible = !this._calendar.Visible;
        }

        /// <summary>
        /// Create the textbox field from the base 
        /// and adds the calendar button and calendar
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.AddCalendarButton();
            this.AddCalendar();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((base.LanguageCode != null) && (base.LanguageCode.Length > 0))
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(base.LanguageCode);
                }
                catch (ArgumentException)
                {
                }
                base.Render(writer);
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }
            else
            {
                base.Render(writer);
            }
        }

        protected override bool ServerValidation()
        {
            bool flag = base.ServerValidation();
            if (!flag)
            {
                return flag;
            }
            if (((this.Mandatory || (base._fieldTextBox.Text.Length <= 0)) || Information.IsDate(base._fieldTextBox.Text)) && (!this.Mandatory || Information.IsDate(base._fieldTextBox.Text)))
            {
                return flag;
            }
            this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(ResourceManager.GetString("InvalidCalendarDateMessage", base.LanguageCode), this.Text, DateTime.UtcNow.ToShortDateString())));
            return false;
        }
    }
}

