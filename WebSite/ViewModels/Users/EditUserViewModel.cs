using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebSite.ViewModels.Roles;

namespace WebSite.ViewModels.Users
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            UserInRoles = new List<RoleEditorViewModel>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        [Display(Name = "User roles")]
        public List<RoleEditorViewModel> UserInRoles { get; set; }
    }
}