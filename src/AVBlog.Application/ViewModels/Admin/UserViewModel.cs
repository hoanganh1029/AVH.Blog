using AVBlog.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace AVBlog.Application.ViewModels.Admin
{
    public class UserViewModel
    {
        public string? Id { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "Allow Download")]
        public bool AllowDownload { get; set; }

        [Display(Name = "Expired Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ExpiredDate { get; set; }

        public string Role { get; set; } = RoleConstant.User;
    }
}
