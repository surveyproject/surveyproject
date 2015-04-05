namespace Votations.NSurvey.Data
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class GridAnswerDataCollection : CollectionBase
    {
        public void Add(GridAnswerData gridAnswer)
        {
            base.List.Add(gridAnswer);
        }

        public void Remove(GridAnswerData gridAnswer)
        {
            base.List.Remove(gridAnswer);
        }

        public GridAnswerData this[int index]
        {
            get
            {
                return (GridAnswerData) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

