namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class AnswerConnectionsCollection : ArrayList
    {
        public int Add(AnswerConnection answerConnection)
        {
            return base.Add(answerConnection);
        }

        public bool Contains(AnswerConnection answerConnection)
        {
            return base.Contains(answerConnection);
        }

        public void Insert(int index, AnswerConnection answerConnection)
        {
            base.Insert(index, answerConnection);
        }

        public void Remove(AnswerConnection answerConnection)
        {
            base.Remove(answerConnection);
        }

        new public AnswerConnection this[int index]
        {
            get
            {
                return (AnswerConnection) base[index];
            }
        }
    }
}

