namespace Votations.NSurvey.WebControlsFactories
{
    using System;
    using Votations.NSurvey.Data;
    using Votations.NSurvey.DataAccess;
    using Votations.NSurvey.WebControls.UI;

    /// <summary>
    /// Creates a new answer item instance from the db answer data
    /// </summary>
    public class SubscriberItemFactory
    {
        private SubscriberItemFactory()
        {
        }

        /// <summary>
        /// Subscribe the answer items which require it to the correct
        /// answer publisher controls defined as defined by the Subscriber collection
        /// </summary>
        public static void ActivateAnswerConnections(AnswerConnectionsCollection answerConnections, AnswerItemCollection answers)
        {
            foreach (AnswerConnection connection in answerConnections)
            {
                IAnswerSubscriber answerItem = GetAnswerItem(answers, connection.SubscriberId) as IAnswerSubscriber;
                if (answerItem != null)
                {
                    AnswerItem item2 = GetAnswerItem(answers, connection.PublisherId);
                    if ((item2 != null) && (item2 is IAnswerPublisher))
                    {
                        item2.AnswerPublished += new AnswerPublisherEventHandler(answerItem.ProcessPublishedAnswers);
                        item2.AnswerPublisherCreated += new AnswerPublisherEventHandler(answerItem.PublisherCreation);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a simple subscriber item that can be 
        /// used to connect a question's answer to another
        /// </summary>
        public static AnswerConnection Create(int publisherId, int subscriberId)
        {
            AnswerConnection connection = new AnswerConnection();
            connection.PublisherId = publisherId;
            connection.SubscriberId = subscriberId;
            return connection;
        }

        /// <summary>
        /// Builds an answer connections collection that can be used
        /// to connect question's answer together
        /// </summary>
        public static AnswerConnectionsCollection CreateSubscriberItemCollection(int questionId)
        {
            AnswerConnectionsCollection connectionss = new AnswerConnectionsCollection();
            foreach (AnswerConnectionData.AnswerConnectionsRow row in new Questions().GetQuestionAnswerConnections(questionId).AnswerConnections)
            {
                connectionss.Add(Create(row.PublisherAnswerId, row.SubscriberAnswerId));
            }
            return connectionss;
        }

        /// <summary>
        /// Retrieves the given answer item from the control collection
        /// </summary>
        private static AnswerItem GetAnswerItem(AnswerItemCollection answers, int answerId)
        {
            foreach (AnswerItem item in answers)
            {
                if (item.AnswerId == answerId)
                {
                    return item;
                }
            }
            return null;
        }
    }
}

