using System.ComponentModel.DataAnnotations;

namespace AVBlog.Application.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmailAddress(this string str)
        {
            return new EmailAddressAttribute().IsValid(str);
        }
    }
}
