namespace eCommerce.API.Error
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse(int statusCode = 0, string message = null) : base(400)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
