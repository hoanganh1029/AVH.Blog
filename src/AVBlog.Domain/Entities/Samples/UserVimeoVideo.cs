using AVBlog.Domain.Entities.Base;
using AVBlog.Domain.Entities.Users;

namespace AVBlog.Domain.Entities.Samples
{
    public class UserVimeoVideo : BaseEntity
    {
        public string UserId { get; set; }

        public Guid VimeoVideoId { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public virtual VimeoVideo VimeoVideo { get; set; }

        public virtual AppUser User { get; set; }
    }
}
