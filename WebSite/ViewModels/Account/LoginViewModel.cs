using System.ComponentModel.DataAnnotations;

namespace WebSite.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}