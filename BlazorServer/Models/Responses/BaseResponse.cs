namespace BlazorServer.Models.Responses
{
    public abstract class BaseResponse : IResponse
    {
        public bool Success;

        public BaseResponse(bool success)
        {
            Success = success;
        }

        public abstract override string ToString();
    }
}
