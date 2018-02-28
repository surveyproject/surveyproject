namespace Votations.NSurvey.WebControls.UI
{
    using Microsoft.VisualBasic;
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web.UI;
    using System.Web;
    using System.Web.UI.WebControls;
    using Votations.NSurvey;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// Calendar Field Answertype that inherits a textbox behaviors and adds a Jquery datepicker/ calendar to
    /// to it
    /// </summary>
    public class AnswerFieldCalendarItem : AnswerFieldItem
    {
        //new Jquery calendar option
        protected virtual void AddNewCalendar()
        {

            // determine and set language codes:
            string userLanguage = "";
            String[] userLang;

            if ((base.LanguageCode != null) && (base.LanguageCode.Length > 0))
            {
                userLanguage = base.LanguageCode;
            }
            else
            {
                if (HttpContext.Current.Request.UserLanguages != null)
                {
                    userLang = HttpContext.Current.Request.UserLanguages;
                    userLanguage = userLang[0].ToString().ToLower().Substring(0,2);
                }
            }

            // Jquery datapicker script
            string CalendarScript = 
            "<script>"+
            "$(function() {" +

            "var lang = '" + userLanguage +"';" +
            
            "$.datepicker.setDefaults($.datepicker.regional[lang]);" +
            "$(\"#" + base._fieldTextBox.ClientID + "\").datepicker({ dateFormat: 'yy/mm/dd'});" + 
            "$(\"#" + base._fieldTextBox.ClientID + "\").attr('readonly', true);" +

            //"$('#" + base._fieldTextBox.ClientID + "').val('test'); " +

            "});" +
            "</script >";

            // add script to page
            this.Controls.Add(new LiteralControl(CalendarScript));

        }
       

        /// <summary>
        /// Create the textbox field from the base 
        /// and adds the calendar button and calendar
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.AddNewCalendar();
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
            this.OnInvalidAnswer(new AnswerItemInvalidEventArgs(string.Format(ResourceManager.GetString("InvalidCalendarDateMessage", base.LanguageCode), this.Text, DateTime.Now.ToShortDateString())));
            return false;
        }
    }
}

