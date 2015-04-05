namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class AnswerItemCollection : ArrayList
    {
        public int Add(AnswerItem answerItem)
        {
            return base.Add(answerItem);
        }

        public bool Contains(AnswerItem answerItem)
        {
            return base.Contains(answerItem);
        }

        public void Insert(int index, AnswerItem answerItem)
        {
            base.Insert(index, answerItem);
        }

        public void Remove(AnswerItem answerItem)
        {
            base.Remove(answerItem);
        }

        new public AnswerItem this[int index]
        {
            get
            {
                return (AnswerItem) base[index];
            }
        }
    }
}

