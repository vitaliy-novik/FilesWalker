using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.Interface.Services;
using Infrastructure.Entities;
using WebSite.Mapping;
using WebSite.ViewModels.Users;

namespace WebSite.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            if (roleService == null)
            {
                throw new ArgumentNullException("roleService");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.userService = userService;
            this.roleService = roleService;
        }

        public ActionResult Index()
        {
            IEnumerable<IUser> users = userService.GetAllUsers();
            List<UserViewModel> viewModel =
                new List<UserViewModel>(users.Select(user => UserMapper.Map(user, roleService)));

            return View(new UsersListViewModel(viewModel));
        }

        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            IUser user = userService.GetById(id);
            EditUserViewModel viewModel = UserMapper.MapToEditUserViewModel(user, roleService);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserMapper.Map(viewModel);
                userService.Update(user);
                roleService.SetUserRoles(RoleMapper.GetRoles(viewModel.UserInRoles), user.Id);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            IUser user = userService.GetById(id);
            userService.Delete(user);

            return RedirectToAction("Index");
        }
    }
}
