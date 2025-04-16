using AVBlog.Domain.Constants;
using System.Security.Claims;

namespace AVBlog.Application.Extensions
{
    public static class UserClaimsPrincipalExtensions
    {
        public static bool IsSuperAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(RoleConstant.Admin) &&
                !string.IsNullOrEmpty(user.Identity?.Name) &&
                user.Identity.Name.Equals(CommonConstant.UserAdminName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
