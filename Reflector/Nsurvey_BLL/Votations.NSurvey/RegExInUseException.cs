using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votations.NSurvey
{
    /// <summary>
    /// Thrown on delete command when a given Regular Expression is already in use by an
    /// answer.
    /// </summary>
    [Serializable]
    public class RegExInUseException : ApplicationException
    {

        public RegExInUseException() : base("RegularExpression is in use on a survey answer!")
        {
        }

        public RegExInUseException(string message) : base(message)
        {
        }
    }
}
