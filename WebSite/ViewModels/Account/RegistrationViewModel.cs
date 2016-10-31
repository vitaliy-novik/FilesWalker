using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebSite.ViewModels.Account
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}