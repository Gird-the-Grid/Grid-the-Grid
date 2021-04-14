using System;
using System.Runtime.Serialization;

namespace BlazorServerAPI.Utils.Exceptions
{
    [Serializable]
    public class InvalidEmailException : Exception, ISerializable
    {
        public InvalidEmailException() :  base("InvalidEmailException : ") { }
        public InvalidEmailException(string exceptionMessage) : base($"InvalidEmailException : {exceptionMessage}") { }
        public InvalidEmailException(string exceptionMessage, Exception inner) : base($"InvalidEmailException : {exceptionMessage}", inner) { }
        protected InvalidEmailException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    }
}
