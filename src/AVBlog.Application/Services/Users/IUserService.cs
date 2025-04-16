using AVBlog.Application.Responses;
using AVBlog.Application.ViewModels.Admin;
using AVBlog.Domain.LifeTime;
using System.Security.Claims;

namespace AVBlog.Application.Services.Users
{
    public interface IUserService : IScopedDependency
    {
        Task<IEnumerable<UserViewModel>> GetUsersAsync(ClaimsPrincipal user);

        Task<Response> GetByUserIdAsync(string userId);

        Task<Response> CreateUserAsync(UserViewModel userModel);

        Task<Response> UpdateUserAsync(UserViewModel userModel);

        Task<Response> DeleteUserAsync(string userId);

        Task<Response> ResetPasswordAsync(ResetPasswordViewModel resetPasswordModel);
    }
}
