using System;
using System.Runtime.Serialization;

namespace BlazorServerAPI.Utils.Exceptions
{
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("InvalidEmailException : ") { }
        public InvalidPasswordException(string exceptionMessage) : base($"InvalidEmailException : {exceptionMessage}") { }
        public InvalidPasswordException(string exceptionMessage, Exception inner) : base($"InvalidEmailException : {exceptionMessage}", inner) { }
        protected InvalidPasswordException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    }

}
