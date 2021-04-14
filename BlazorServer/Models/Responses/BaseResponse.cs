namespace BlazorServerAPI.Models.Responses
{
    public abstract class BaseResponse : IResponse
    {
        public bool Success { get; set; }

        protected BaseResponse(bool success)
        {
            Success = success;
        }

        public abstract override string ToString();
    }
}
