using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2_v2.ClassLibrary
{
    public class ValidationFailureException : Exception
    {
        public ValidationFailureException(string message) : base(message)
        {

        }

        public ValidationFailureException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
