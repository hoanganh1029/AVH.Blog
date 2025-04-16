using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.LifeTime;
using AVBlog.Domain.Repositories.Base;

namespace AVBlog.Domain.Repositories.Samples.UserVimeoVideos
{
    public interface IUserVimeoVideoCommandRepository : IBaseCommandRepository<UserVimeoVideo>, IScopedDependency
    {
    }
}
