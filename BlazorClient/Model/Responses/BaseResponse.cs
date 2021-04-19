using Newtonsoft.Json;

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
