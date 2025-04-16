using AVBlog.Domain.ProjectionModels.Samples;
using AVBlog.Domain.LifeTime;

namespace AVBlog.Domain.Repositories.Users
{
    public interface IUserRepository : IScopedDependency
    {
        Task<IEnumerable<UserWithRoleProjection>> GetByRoleAsync(string roleName, string excludeId);
    }
}
