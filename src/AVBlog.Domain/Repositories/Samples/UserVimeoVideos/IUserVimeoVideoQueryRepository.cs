using AVBlog.Domain.ProjectionModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.LifeTime;
using AVBlog.Domain.Repositories.Base;

namespace AVBlog.Domain.Repositories.Samples.UserVimeoVideos
{
    public interface IUserVimeoVideoQueryRepository : IBaseQueryRepository<UserVimeoVideo>, IScopedDependency
    {
        Task<IEnumerable<UserVimeoVideoProjection>> GetAllUserVideoAsync();

        Task<UserVimeoVideoProjection?> GetUserVideoByIdAsync(Guid id);

        Task<bool> IsPermissionExistAsync(string userId, Guid videoId, Guid? excludedId = default);

        Task<IEnumerable<UserVimeoVideo>> GetByUserIdAsync(string userId);
    }
}
