namespace Votations.NSurvey.Reporting
{
    using System;
    using System.Text;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.Emailing;
    using Votations.NSurvey.Resources;

    /// <summary>
    /// base class for those who wants to provide 
    /// a specific voter report generation
    /// </summary>
    public abstract class VoterReportGenerator
    {
        protected QuestionData _questionData;
        protected int _surveyId;
        protected VoterAnswersData _voterAnswers;
        protected bool isScored = false;

        public VoterReportGenerator(VoterAnswersData voterAnswers, int surveyId)
        {
            this._voterAnswers = voterAnswers;
            this._surveyId = surveyId;
        }

        protected abstract string GenerateFooter();
        protected abstract string GenerateHeader();
        protected abstract string GenerateQuestionsReport(bool onlyAnswered);
        public string GenerateTextReport(bool onlyAnswered)
        {
            StringBuilder builder = new StringBuilder();
            this.isScored = new Surveys().IsSurveyScored(this._surveyId);
            builder.Append(this.GenerateHeader());
            builder.Append(this.GenerateVoterInfo());
            builder.Append(this.GenerateQuestionsReport(onlyAnswered));
            builder.Append(this.GenerateFooter());
            return builder.ToString();
        }

        protected abstract string GenerateVoterInfo();
        /// <summary>
        /// Generates and sends a report and fill the message body
        /// depending on the notification mode chosen
        /// </summary>
        public void SendReport(NotificationMode notificationMode, string surveyTitle, string emailFrom, string emailTo, string emailSubject)
        {
            IEmailing emailing = EmailingFactory.Create();
            EmailingMessage mail = new EmailingMessage();
            mail.FromEmail = emailFrom;
            if ((notificationMode == NotificationMode.EntryReport) || (notificationMode == NotificationMode.OnlyAnswersReport))
            {
                mail.Body = this.GenerateTextReport(notificationMode == NotificationMode.OnlyAnswersReport);
            }
            else
            {
                mail.Body = ResourceManager.GetString("ShortNotificationMessage");
            }
            mail.Subject = (emailSubject == null) ? string.Format(ResourceManager.GetString("NotificationSubject"), surveyTitle) : emailSubject;
            mail.ToEmail = emailTo;
            emailing.SendEmail(mail);
        }
    }
}

