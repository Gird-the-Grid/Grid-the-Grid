namespace BlazorServer.Models.Responses
{
    public class BaseResponse : IResponse
    {
        public bool Success;

        public BaseResponse(bool success)
        {
            Success = success;
        }
    }
}
