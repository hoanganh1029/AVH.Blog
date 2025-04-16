namespace AVBlog.Application.Responses
{
    public class Response<T> : Response
    {
        public T? Content { get; set; }
    }

    public class SuccessGeneralResponse<T> : Response<T>
    {
        public override bool Success { get; set; } = true;
    }
}
