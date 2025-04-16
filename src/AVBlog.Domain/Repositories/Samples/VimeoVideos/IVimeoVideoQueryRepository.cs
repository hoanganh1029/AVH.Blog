using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.LifeTime;
using AVBlog.Domain.Repositories.Base;

namespace AVBlog.Domain.Repositories.Samples.VimeoVideos
{
    public interface IVimeoVideoQueryRepository : IBaseQueryRepository<VimeoVideo>, IScopedDependency
    {
        Task<bool> IsVimeoIdExist(int vimeoId, Guid? excludedId = default);
    }
}
