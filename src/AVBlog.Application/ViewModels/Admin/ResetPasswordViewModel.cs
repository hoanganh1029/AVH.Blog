using System.ComponentModel.DataAnnotations;

namespace AVBlog.Application.ViewModels.Admin
{
    public class ResetPasswordViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "User Email")]
        [Required]
        public string UserEmail { get; set; } = string.Empty;

        [Display(Name = "New Password")]
        [Required]
        public string NewPassword { get; set; } = string.Empty;

    }
}