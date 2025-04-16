using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Repositories.Samples.UserVimeoVideos;
using AVBlog.Infrastructure.Data;
using AVBlog.Infrastructure.Repositories.Base;

namespace AVBlog.Infrastructure.Repositories.Samples
{
    internal class UserVimeoVideoCommandRepository : BaseCommandRepository<UserVimeoVideo>, IUserVimeoVideoCommandRepository
    {
        public UserVimeoVideoCommandRepository(AVBlogCommandContext dbContext) : base(dbContext)
        {

        }
    }
}
