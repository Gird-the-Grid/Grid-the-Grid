using System;
using System.Runtime.Serialization;

namespace BlazorServerAPI.Utils.Exceptions
{
    [Serializable]
    public class ServerException : Exception, ISerializable
    {
        public ServerException() : base("ServerException : ") { }
        public ServerException(string exceptionMessage) : base($"ServerException : {exceptionMessage}") { }
        public ServerException(string exceptionMessage, Exception inner) : base($"ServerException : {exceptionMessage}", inner) { }
        protected ServerException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo,streamingContext){ }
    }
}
