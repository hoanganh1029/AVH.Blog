using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.VimeoVideos;
using AVBlog.Infrastructure.Data;
using AVBlog.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AVBlog.Infrastructure.Repositories.Samples
{
    public class VimeoVideoQueryRepository : BaseQueryRepository<VimeoVideo>, IVimeoVideoQueryRepository
    {
        public VimeoVideoQueryRepository(AVBlogQueryContext dbContext) : base(dbContext)
        {

        }

        public async Task<bool> IsVimeoIdExist(int vimeoId, Guid? excludedId)
        {
            return await _currentEntity.AnyAsync(x => x.VimeoId == vimeoId
                && (excludedId == null || x.Id != excludedId));
        }
    }
}
