using AVBlog.Domain.Entities.Samples;
using AVBlog.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AVBlog.Infrastructure.Data
{
    public class AVBlogCommandContext(DbContextOptions<AVBlogCommandContext> options) : IdentityDbContext<AppUser>(options)
    {
        public virtual DbSet<VimeoVideo> VimeoVideos { get; set; } = default!;

        public virtual DbSet<UserVimeoVideo> UserVimeoVideos { get; set; } = default!;
    }
}
