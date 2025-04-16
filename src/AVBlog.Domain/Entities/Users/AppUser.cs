using Microsoft.AspNetCore.Identity;

namespace AVBlog.Domain.Entities.Users
{
    public class AppUser : IdentityUser
    {
        public bool AllowDownload { get; set; }

        public DateTime? ExpiredDate { get; set; }
    }
}
