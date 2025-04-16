using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AVBlog.WebAPI.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetErrorMessages(this ModelStateDictionary modelState)
        {
            string message = string.Empty;
            if (modelState != null && !modelState.IsValid)
            {
                var messages = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                message = string.Join(", ", messages);
            }
            return message;
        }
    }
}
