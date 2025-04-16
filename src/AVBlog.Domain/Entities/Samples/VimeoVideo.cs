using AVBlog.Domain.Constants;
using AVBlog.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace AVBlog.Domain.Entities.Samples
{
    public class VimeoVideo : BaseEntity
    {
        [Required]
        public int VimeoId { get; set; }

        [Required]
        public string Title { get; set; }

        public VideoType VideoType { get; set; }

        public DateTime? PublishedDate { get; set; }

        public string? Description { get; set; }

        public string? Presenter { get; set; }
    }
}
