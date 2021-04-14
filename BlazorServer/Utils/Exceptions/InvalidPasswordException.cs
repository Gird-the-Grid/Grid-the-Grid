using System;
using System.Runtime.Serialization;

namespace BlazorServerAPI.Utils.Exceptions
{
    [Serializable]
    public class InvalidPasswordException : Exception, ISerializable
    {
        public InvalidPasswordException() : base("InvalidEmailException : ") { }
        public InvalidPasswordException(string exceptionMessage) : base($"InvalidEmailException : {exceptionMessage}") { }
        public InvalidPasswordException(string exceptionMessage, Exception inner) : base($"InvalidEmailException : {exceptionMessage}", inner) { }

    }

}
