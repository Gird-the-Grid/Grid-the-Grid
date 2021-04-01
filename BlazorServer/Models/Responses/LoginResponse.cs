using Newtonsoft.Json;

namespace BlazorServerAPI.Models.Responses
{
    public class LoginResponse : BaseResponse
    {
        public string Token;

        public LoginResponse(string token) : base(true)
        {
            Token = token;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
