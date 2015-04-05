namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class QuestionItemCollection : ArrayList
    {
        public int Add(QuestionItem answerItem)
        {
            return base.Add(answerItem);
        }

        public bool Contains(QuestionItem anwerItem)
        {
            return base.Contains(anwerItem);
        }

        public void Insert(int index, QuestionItem anwerItem)
        {
            base.Insert(index, anwerItem);
        }

        public void Remove(QuestionItem anwerItem)
        {
            base.Remove(anwerItem);
        }

        new public QuestionItem this[int index]
        {
            get
            {
                return (QuestionItem) base[index];
            }
        }
    }
}

