using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Models.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException() : base() { }
        public BaseException(string exceptionMessage) : base(exceptionMessage) { }
        public BaseException(string exceptionMessage, Exception inner): base(exceptionMessage, inner) { }



    }
}
