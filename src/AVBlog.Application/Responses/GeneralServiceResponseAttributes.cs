using System.Net;

namespace AVBlog.Application.Responses
{
    public abstract class GeneralServiceResponseAttributes
    {

        public static Response Success() => new()
        {
            Success = true
        };

        public static Response<T> Success<T>(T content) => new()
        {
            Success = true,
            Content = content
        };

        public static Response NotFound() => new()
        {
            Success = false,
            StatusCode = HttpStatusCode.NotFound,
            Message = "Entity is not found"
        };

        public static Response BadRequest(string message) => new()
        {
            Success = false,
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };

    }
}
