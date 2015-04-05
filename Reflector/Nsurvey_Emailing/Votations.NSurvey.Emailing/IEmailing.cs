namespace Votations.NSurvey.Emailing
{
    using System;

    /// <summary>
    /// To those who want to implement other emailing features
    /// than the default one (System.Web.Mail)
    /// </summary>
    public interface IEmailing
    {
        void SendEmail(EmailingMessage mail);
    }
}

