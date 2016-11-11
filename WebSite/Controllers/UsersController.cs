using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.Interface.Services;
using Infrastructure.Entities;
using Microsoft.Practices.Unity;
using WebSite.Mapping;
using WebSite.ViewModels.Users;

namespace WebSite.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency]
        public IRoleService RoleService { get; set; }

        /// <summary>
        /// Returns list with all users
        /// </summary>
        public ActionResult Index()
        {
            IEnumerable<IUser> users = UserService.GetAllUsers();
            List<UserViewModel> viewModel =
                new List<UserViewModel>(users.Select(user => UserMapper.Map(user, RoleService)));

            return View(new UsersListViewModel(viewModel));
        }

        /// <summary>
        /// Returns form to edit user roles
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>View with form</returns>
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            IUser user = UserService.GetById(id);
            EditUserViewModel viewModel = UserMapper.MapToEditUserViewModel(user, RoleService);

            return View(viewModel);
        }

        /// <summary>
        /// Edits user roles
        /// </summary>
        /// <param name="viewModel">Model with new user information</param>
        /// <returns>Users list if operation success and form if failed</returns>
        [HttpPost]
        public ActionResult Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserMapper.Map(viewModel);
                //userService.Update(user);
                RoleService.SetUserRoles(RoleMapper.GetRoles(viewModel.UserInRoles), user.Id);
                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Updated users list</returns>
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            IUser user = UserService.GetById(id);
            UserService.Delete(user);

            return RedirectToAction("Index");
        }
    }
}
