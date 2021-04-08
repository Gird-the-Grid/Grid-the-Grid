using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAPI.Utils.Exceptions
{
    [Serializable]
    public class ServerException : Exception
    {
        public ServerException() : base(String.Format("ServerException : ")) { }
        public ServerException(String exceptionMessage) : base(String.Format("ServerException : {0}", exceptionMessage)) { }
        public ServerException(String exceptionMessage, Exception inner) : base(String.Format("ServerException : {0}", exceptionMessage), inner) { }

    }
}
