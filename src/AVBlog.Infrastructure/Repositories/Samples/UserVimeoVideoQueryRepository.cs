using AVBlog.Domain.ProjectionModels.Samples;
using AVBlog.Domain.Entities.Samples;
using AVBlog.Infrastructure.Data;
using AVBlog.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using AVBlog.Domain.Repositories.Samples.UserVimeoVideos;

namespace AVBlog.Infrastructure.Repositories.Samples
{
    internal class UserVimeoVideoQueryRepository : BaseQueryRepository<UserVimeoVideo>, IUserVimeoVideoQueryRepository
    {
        public UserVimeoVideoQueryRepository(AVBlogQueryContext dbContext) : base(dbContext)
        {

        }

        public async Task<UserVimeoVideoProjection?> GetUserVideoByIdAsync(Guid id)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            
            var eagerQueryable = GetEagerQueryable();
            var userVimeoVideo = await eagerQueryable
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return userVimeoVideo;
        }

        public async Task<IEnumerable<UserVimeoVideoProjection>> GetAllUserVideoAsync()
        {
            var eagerQueryable = GetEagerQueryable();
            var userVideos = await eagerQueryable
                        .OrderBy(x => x.UserName)
                        .ToListAsync();
            return userVideos;
        }

        public async Task<bool> IsPermissionExistAsync(string userId, Guid videoId, Guid? excludedId = default)
        {
            return await _context.UserVimeoVideos.AnyAsync(x =>
            x.UserId == userId && x.VimeoVideoId == videoId && (excludedId == null || x.Id != excludedId.Value));
        }

        public async Task<IEnumerable<UserVimeoVideo>> GetByUserIdAsync(string userId)
        {
            var now = DateTime.UtcNow;
            var userVideos = await _context.UserVimeoVideos
                .Include(x => x.VimeoVideo)
                .Where(x => x.UserId == userId && (!x.ExpiredDate.HasValue || x.ExpiredDate > now))
                .OrderByDescending(x => x.VimeoVideo.PublishedDate)
                .ToListAsync();
            return userVideos;
        }

        /// <summary>
        /// Use Eager loading to get related data of UserVimeoVideo
        /// </summary>
        private IQueryable<UserVimeoVideoProjection> GetEagerQueryable()
        {
            return _context.UserVimeoVideos
                .Include(x => x.VimeoVideo)
                .Include(x => x.User)
                .Select(x => new UserVimeoVideoProjection
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    VimeoVideoId = x.VimeoVideoId,
                    VimeoId = x.VimeoVideo.VimeoId,
                    VideoTitle = x.VimeoVideo.Title,
                    UserName = x.User.UserName,
                    ExpiredDate = x.ExpiredDate
                });
        }

        /// <summary>
        /// Example of using Explicit loading to get related data
        /// SQL query is executed after calling method LoadAsync()
        /// </summary>
        private async Task<UserVimeoVideoProjection> GetExplicitById(Guid id)
        {
            var userVideo = await _context.UserVimeoVideos.SingleAsync(x => x.Id == id);

            await _context.Entry(userVideo)
                            .Reference(uv => uv.VimeoVideo)
                            .LoadAsync();

            await _context.Entry(userVideo)
                            .Reference(uv => uv.User)
                            .LoadAsync();

            return new UserVimeoVideoProjection
            {
                Id = userVideo.Id,
                UserId = userVideo.UserId,
                VimeoVideoId = userVideo.VimeoVideoId,
                VimeoId = userVideo.VimeoVideo.VimeoId,
                VideoTitle = userVideo.VimeoVideo.Title,
                UserName = userVideo.User.UserName,
                ExpiredDate = userVideo.ExpiredDate
            };
        }
    }
}
