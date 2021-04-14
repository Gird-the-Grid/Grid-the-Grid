using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Model.Responses
{
    public class BaseResponse : IResponse
    {
        public bool Success { get; set; }
        public BaseResponse(bool success)
        {
            Success = success;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
