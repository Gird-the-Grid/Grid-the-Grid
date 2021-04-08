using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Utils.Exceptions
{
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base(String.Format("InvalidPasswordException : ")) { }
        public InvalidPasswordException(String exceptionMessage) : base(String.Format("InvalidPasswordException : {0}", exceptionMessage)) { }
        public InvalidPasswordException(String exceptionMessage, Exception inner) : base(String.Format("InvalidPasswordException : {0}", exceptionMessage), inner) { }

    }

}
