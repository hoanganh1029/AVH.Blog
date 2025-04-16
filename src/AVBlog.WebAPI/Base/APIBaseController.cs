using AVBlog.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AVBlog.WebAPI.Base
{
    public class APIBaseController : ControllerBase
    {
        protected ActionResult HandleResponseAsActionResult<T>(Response<T>? response)
        {
            if (response is null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response.Success
                ? Ok(response.Content)
                : response.StatusCode switch
                {
                    HttpStatusCode.NotFound => NotFound(response.Message),
                    HttpStatusCode.BadRequest => BadRequest(response.Message),
                    _ => StatusCode((int)response.StatusCode, response.Message)
                };
        }

        protected ActionResult HandleResponseAsActionResult(Response? response)
        {
            if (response is null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response.Success
                ? Ok()
                : response.StatusCode switch
                {
                    HttpStatusCode.NotFound => NotFound(response.Message),
                    HttpStatusCode.BadRequest => BadRequest(response.Message),
                    _ => StatusCode((int)response.StatusCode, response.Message)
                };
        }
    }
}
