using Newtonsoft.Json;

namespace BlazorClient.Model.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public string Error { get; set; }

        //for debug purposes
        public string ErrorMessage { get; set; }

        [JsonConstructor]
        public ErrorResponse(string error, string errorMessage) : base(false)
        {
            //TODO: Check how Newtonsoft.Json works, because it might give errors if more constructors with [JsonConstructor] exist. 
            //TODO: Do add [JsonConstructor] attribute to all json serialized classes
            Error = error;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
