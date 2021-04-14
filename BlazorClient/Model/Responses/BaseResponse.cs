using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Model.Responses
{
    abstract public class BaseResponse : IResponse
    {
        public bool Success { get; set; }
        public BaseResponse(bool success)
        {
            Success = success;
        }

        public abstract override string ToString();
    }
}
