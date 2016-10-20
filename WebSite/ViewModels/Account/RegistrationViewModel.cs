using System.ComponentModel.DataAnnotations;

namespace WebSite.ViewModels.Account
{
    public class RegistrationViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}