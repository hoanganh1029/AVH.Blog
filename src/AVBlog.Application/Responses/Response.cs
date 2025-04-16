using System.Net;

namespace AVBlog.Application.Responses
{
    public class Response
    {
        public virtual bool Success { get; set; } = false;

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; } = string.Empty;

        public GeneralValidationResult ValidationResult { get; set; } = new GeneralValidationResult();
    }
}
