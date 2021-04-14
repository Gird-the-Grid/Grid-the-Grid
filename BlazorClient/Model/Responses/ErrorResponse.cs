using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Model.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public string Error { get; set; }

        //for debug purposes
        public string ErrorMessage { get; set; }

        public ErrorResponse(string error) : base(false)
        {
            Error = error;
            ErrorMessage = "";
        }

        public ErrorResponse(string error, string errorMessage) : base(false)
        {
            Error = error;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
