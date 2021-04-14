using Newtonsoft.Json;

namespace BlazorServerAPI.Models.Responses
{
    public class LoginResponse : BaseResponse
    {
        public string Token;

        public string UserId;

        public LoginResponse(string token, string userId) : base(true)
        {
            Token = token;
            UserId = userId;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
