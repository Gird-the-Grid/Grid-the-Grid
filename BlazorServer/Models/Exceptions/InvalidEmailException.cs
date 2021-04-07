using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Models.Exceptions
{
    [Serializable]
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() :  base(String.Format("InvalidEmailException : ")) { }
        public InvalidEmailException(String exceptionMessage) : base(String.Format("InvalidEmailException : {0}", exceptionMessage)) { }
        public InvalidEmailException(String exceptionMessage, Exception inner) : base(String.Format("InvalidEmailException : {0}", exceptionMessage), inner) { }
    }
}
