using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.Interface.Services;
using Infrastructure.Entities;
using WebSite.Mapping;
using WebSite.ViewModels.Roles;

namespace WebSite.Controllers
{
    public class DataController : Controller
    {
        private readonly IRoleService roleService;

        public DataController(IRoleService roleService)
        {
            if (roleService == null)
            {
                throw new ArgumentNullException("roleService");
            }

            this.roleService = roleService;
        }

        public ActionResult RolesEditor(IEnumerable<IRole> userRoles)
        {
            IEnumerable<IRole> roles = roleService.GetAllRoles();
            RolesEditorViewModel viewModel = new RolesEditorViewModel();
            foreach (IRole role in roles)
            {
                viewModel.Roles.Add(new RoleEditorViewModel()
                {
                    Role = RoleMapper.Map(role),
                    UserInRole = userRoles.Contains(role)
                });
            }
 
            return View(viewModel);
        }

    }
}
