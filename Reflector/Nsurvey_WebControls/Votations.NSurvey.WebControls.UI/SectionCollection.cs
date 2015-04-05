namespace Votations.NSurvey.WebControls.UI
{
    using System;
    using System.Collections;
    using System.Reflection;

    /// <summary>
    /// Collection of question's sections
    /// </summary>
    public class SectionCollection : ArrayList
    {
        public int Add(Section section)
        {
            return base.Add(section);
        }

        public bool Contains(Section section)
        {
            return base.Contains(section);
        }

        public void Insert(int index, Section section)
        {
            base.Insert(index, section);
        }

        public void Remove(Section section)
        {
            base.Remove(section);
        }

        new public Section this[int index]
        {
            get
            {
                return (Section) base[index];
            }
        }
    }
}

