using Microsoft.AspNetCore.Identity;
using System.Text;

namespace AVBlog.Application.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string GetErrorsMessage(this IdentityResult result)
        {
            var errors = new StringBuilder();
            foreach (var error in result.Errors)
            {
                errors.AppendLine(error.Description);
            }

            return errors.ToString();
        }
    }
}
