using System.ComponentModel.DataAnnotations;

namespace AVBlog.Application.ViewModels.Samples
{
    public class UserVimeoVideoViewModel
    {

        public Guid Id { get; set; } = Guid.Empty;


        [Display(Name = "User")]
        [Required]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Expried Date")]
        public DateTime? ExpiredDate { get; set; }

        [Display(Name = "Video")]
        [Required]
        public Guid VimeoVideoId { get; set; }

        [Display(Name = "Vimeo Id")]
        public int VimeoId { get; set; }

        [Display(Name = "Video Title")]
        public string? VideoTitle { get; set; }
    }
}
