using System;

namespace BlazorServerAPI.Utils.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException() : base() { }
        public BaseException(string exceptionMessage) : base(exceptionMessage) { }
        public BaseException(string exceptionMessage, Exception inner): base(exceptionMessage, inner) { }



    }
}
