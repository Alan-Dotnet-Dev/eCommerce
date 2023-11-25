using eCommerce.API.Error;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    
    public class BugController : BaseAPIController
    {
        private readonly StoreContext Contex;
        public BugController(StoreContext contex)
        {
            Contex = contex;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var things = Contex.Products.Find(42);
           
            if (things == null) { return NotFound( new ApiResponse(400)); }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var things = Contex.Products.Find(42);
            var thingToReturn = things.ToString();
            return Ok();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequst()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequst(int id)
        {
            return Ok();
        }




    }
}
