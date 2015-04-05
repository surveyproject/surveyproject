namespace Votations.NSurvey.Security
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class WebSecurityAddInCollection : ArrayList
    {
        public int Add(IWebSecurityAddIn surveySecurityAddIn)
        {
            return base.Add(surveySecurityAddIn);
        }

        public bool Contains(IWebSecurityAddIn surveySecurityAddIn)
        {
            return base.Contains(surveySecurityAddIn);
        }

        public void Insert(int index, IWebSecurityAddIn surveySecurityAddIn)
        {
            base.Insert(index, surveySecurityAddIn);
        }

        public void Remove(IWebSecurityAddIn surveySecurityAddIn)
        {
            base.Remove(surveySecurityAddIn);
        }

        new public IWebSecurityAddIn this[int index]
        {
            get
            {
                return (IWebSecurityAddIn) base[index];
            }
        }
    }
}

