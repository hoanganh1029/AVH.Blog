using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using AVBlog.Infrastructure.Data;
using AVBlog.Infrastructure.Repositories.Base;

namespace AVBlog.Infrastructure.Repositories.Samples
{
    public class VimeoVideoCommnandRepository : BaseCommandRepository<VimeoVideo>, IVimeoVideoCommandRepository
    {
        public VimeoVideoCommnandRepository(AVBlogCommandContext dbContext) : base(dbContext)
        {

        }
    }
}