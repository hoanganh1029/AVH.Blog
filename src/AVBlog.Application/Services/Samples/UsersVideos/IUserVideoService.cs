using AVBlog.Application.Services.Base;
using AVBlog.Application.ViewModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.LifeTime;

namespace AVBlog.Application.Services.Samples.UsersVideos
{
    public interface IUserVideoService : IBaseService<UserVimeoVideo, UserVimeoVideoViewModel>, IScopedDependency
    {
        new Task<IEnumerable<UserVimeoVideoViewModel>> GetAllAsync();

        Task<bool> IsPermissionExistAsync(string userId, Guid videoId, Guid? excludedId = default);
    }
}
