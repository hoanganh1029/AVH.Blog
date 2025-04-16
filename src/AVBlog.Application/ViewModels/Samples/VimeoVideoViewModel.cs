using AVBlog.Domain.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AVBlog.Application.ViewModels.Samples
{
    public class VimeoVideoViewModel
    {
        public Guid? Id { get; set; } = Guid.Empty;

        [Required]
        [DisplayName("Vimeo Id")]
        public int VimeoId { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Video Type")]
        public VideoType VideoType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Published Date")]
        public DateOnly? PublishedDate { get; set; }

        public string? Description { get; set; }

        public string? Presenter { get; set; }
    }
}
