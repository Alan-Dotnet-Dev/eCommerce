using eCommerce.API.Error;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{

    public class ErrorController : BaseAPIController
    {
        [Route("errors/{code}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
