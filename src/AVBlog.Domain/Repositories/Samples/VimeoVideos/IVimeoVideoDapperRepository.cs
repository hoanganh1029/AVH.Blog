using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.LifeTime;

namespace AVBlog.Domain.Repositories.Samples.VimeoVideos
{

    /// <summary>
    /// The handler is used as POC for using Dapper to get data
    /// Dapper is more convenient and efficient in certain scenarios (e.g. complex query, report).
    /// </summary>
    public interface IVimeoVideoDapperRepository : IScopedDependency
    {
        Task<IEnumerable<VimeoVideo>> GetAll();
    }
}
