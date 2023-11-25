
namespace eCommerce.API.Error
{
    public class ApiResponse
    {
        public ApiResponse( int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request",
                401 => "Authorized",
                404 => "Resource not Found",
                500 => " Error Path",
                _ => null
            };
        }
    }
}
