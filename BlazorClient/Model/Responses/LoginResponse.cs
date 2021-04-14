using Newtonsoft.Json;

namespace BlazorClient.Model.Responses
{
    public class LoginResponse : BaseResponse
    {
        public LoginResponse(string token, string userId) : base(true)
        {
            Token = token;
            UserId = userId;
        }

        public string Token { get; set; }
        public string UserId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this); ;
        }
    }
}
