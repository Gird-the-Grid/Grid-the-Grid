

using Newtonsoft.Json;

namespace BlazorServerAPI.Models.Responses
{
    public class MessageResponse : BaseResponse
    {
        public string Message { get; set; }

        public MessageResponse(string message) : base(true) => Message = message;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
