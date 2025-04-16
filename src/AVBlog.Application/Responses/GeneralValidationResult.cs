namespace AVBlog.Application.Responses
{
    public class GeneralValidationResult
    {
        public bool IsValid { get; set; } = false;

        public Dictionary<string, string> Errors { get; set; } = [];

        public string Message => string.Join(", ", Errors.Select(e => $"{e.Key}: {e.Value}"));
    }

    public class ModelValidationResult<T> : GeneralValidationResult where T : class
    {
        public T? Content { get; set; }
    }
}
