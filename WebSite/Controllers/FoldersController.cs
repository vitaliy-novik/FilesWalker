using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Business.Interface.Services;
using Infrastructure.Entities;
using Microsoft.AspNet.Identity;
using WebSite.Managers;
using WebSite.Mapping;
using WebSite.Models;
using WebSite.ViewModels.Folders;

namespace WebSite.Controllers
{
    public class FoldersController : Controller
    {
        private readonly IUserManager<IdentityUser> userManager;

        private readonly IFoldersService foldersService;

        public FoldersController(IFoldersService foldersService, IUserManager<IdentityUser> userManager)
        {
            if (foldersService == null)
            {
                throw new ArgumentNullException("foldersService");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.foldersService = foldersService;
            this.userManager = userManager;
        }


        [Authorize]
        public async Task<ActionResult> Index()
        {
            IdentityUser user = await userManager.FindByIdAsync(User.Identity.GetUserId());

            Folder folder = foldersService.GetDirectories(user);

            return View(FolderMapper.Map(folder));
        }

        [Authorize]
        public async Task<ActionResult> Folder(string path)
        {
            IdentityUser user = await userManager.FindByIdAsync(User.Identity.GetUserId());

            Folder folder = foldersService.GetDirectories(user, path);

            return View("Index", FolderMapper.Map(folder));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create(string path)
        {
            TempData["Path"] = path;

            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(CreateFolderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", viewModel);
            }

            Folder folder = FolderMapper.Map(viewModel);
            foldersService.CreateFolder(folder.Path);
            return RedirectToRoute("Folders", new { path = folder.Path });
        }
    }
}
