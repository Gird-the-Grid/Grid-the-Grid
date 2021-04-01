using Newtonsoft.Json;

namespace BlazorServer.Models.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public string Error;

        //for debug purposes
        public string ErrorMessage;

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
