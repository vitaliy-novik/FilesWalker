using Infrastructure.Entities;

namespace WebSite.ViewModels.Roles
{
    public class RoleEditorViewModel
    {
        public RoleViewModel Role { get; set; }

        public bool UserInRole { get; set; }
    }
}