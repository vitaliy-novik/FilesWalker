using System.Collections.Generic;

namespace WebSite.ViewModels.Roles
{
    public class RolesEditorViewModel
    {
        public RolesEditorViewModel()
        {
            Roles = new List<RoleEditorViewModel>();    
        }

        public List<RoleEditorViewModel> Roles { get; set; }
    }
}