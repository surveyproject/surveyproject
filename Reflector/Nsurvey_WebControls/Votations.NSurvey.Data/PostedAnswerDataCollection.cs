namespace Votations.NSurvey.Data
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class PostedAnswerDataCollection : ArrayList
    {
        public int Add(PostedAnswerData postedAnswerData)
        {
            return base.Add(postedAnswerData);
        }

        public bool Contains(PostedAnswerData postedAnswerData)
        {
            return base.Contains(postedAnswerData);
        }

        public void Insert(int index, PostedAnswerData postedAnswerData)
        {
            base.Insert(index, postedAnswerData);
        }

        public void Remove(PostedAnswerData postedAnswerData)
        {
            base.Remove(postedAnswerData);
        }

        new public PostedAnswerData this[int index]
        {
            get
            {
                return (PostedAnswerData) base[index];
            }
        }
    }
}

