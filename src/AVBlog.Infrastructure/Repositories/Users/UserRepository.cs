using AVBlog.Domain.ProjectionModels.Samples;
using AVBlog.Domain.Repositories.Users;
using AVBlog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AVBlog.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AVBlogCommandContext _context;
        public UserRepository(AVBlogCommandContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserWithRoleProjection>> GetByRoleAsync(string roleName, string excludeId)
        {
            //Get user with role from _Context using linq   
            var users = from user in _context.Users
                        join userRole in _context.UserRoles on user.Id equals userRole.UserId
                        join role in _context.Roles on userRole.RoleId equals role.Id
                        where user.Id != excludeId && (string.IsNullOrEmpty(roleName) || role.Name == roleName)
                        select new UserWithRoleProjection
                        {
                            Id = user.Id,
                            Email = user.Email!,
                            Role = role.Name!,
                            AllowDownload = user.AllowDownload,
                            ExpiredDate = user.ExpiredDate
                        };
            return await users.ToListAsync();
        }
    }
}
