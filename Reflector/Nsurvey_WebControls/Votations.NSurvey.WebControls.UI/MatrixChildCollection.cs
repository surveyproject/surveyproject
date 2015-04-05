namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class MatrixChildCollection : ArrayList
    {
        public int Add(MatrixChildQuestion matrixChildQuestion)
        {
            return base.Add(matrixChildQuestion);
        }

        public bool Contains(MatrixChildQuestion matrixChildQuestion)
        {
            return base.Contains(matrixChildQuestion);
        }

        public void Insert(int index, MatrixChildQuestion matrixChildQuestion)
        {
            base.Insert(index, matrixChildQuestion);
        }

        public void Remove(AnswerItem matrixChildQuestion)
        {
            base.Remove(matrixChildQuestion);
        }

        new public MatrixChildQuestion this[int index]
        {
            get
            {
                return (MatrixChildQuestion) base[index];
            }
        }
    }
}

