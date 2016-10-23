using System.Collections.Generic;
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

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<RoleEditorViewModel> UserInRoles { get; set; }
    }
}