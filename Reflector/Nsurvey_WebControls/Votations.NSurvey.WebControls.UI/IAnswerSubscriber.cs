namespace Votations.NSurvey.WebControls.UI
{
    using System;

    /// <summary>
    /// Define the properties that are required for a class 
    /// that wants to be able to subscribe to answer published
    /// by other answers.
    /// </summary>
    public interface IAnswerSubscriber
    {
        void ProcessPublishedAnswers(object sender, AnswerItemEventArgs e);
        void PublisherCreation(object sender, AnswerItemEventArgs e);
    }
}

