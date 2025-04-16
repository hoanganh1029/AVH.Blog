using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.LifeTime;
using AVBlog.Domain.Repositories.Base;

namespace AVBlog.Domain.Repositories.Samples.VimeoVideos
{
    public interface IVimeoVideoCommandRepository : IBaseCommandRepository<VimeoVideo>, IScopedDependency
    {

    }
}
