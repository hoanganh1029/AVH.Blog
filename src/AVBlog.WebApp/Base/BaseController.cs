using AVBlog.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AVBlog.WebApp.Base
{
    public class BaseController : Controller
    {
        protected ActionResult HandleResponseAsActionResult<T>(Response<T>? response)
        {
            if (response is null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            return response.Success
                ? View(response.Content)
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
