using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            ViewBag.Title = "Create";

            return View("CreateFolderDialog");
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

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Delete(string path, IEnumerable<string> folders)
        {
            if (!string.IsNullOrEmpty(path))
            {
                foldersService.DeleteFolders(path, folders);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Rename(string path, string folder)
        {
            RenameFolderViewModel viewModel = new RenameFolderViewModel()
            {
                OldName = folder,
                Path = path,
                NewName = folder
            };

            return View("RenameFolderDialog", viewModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Rename(RenameFolderViewModel viewModel)
        {
            foldersService.RenameFolder(viewModel.Path, viewModel.OldName, viewModel.NewName);

            return RedirectToRoute("Folders", new { path = viewModel.Path });
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CopyTo(string path, string source, string target)
        {
            foldersService.CopyTo(path, source, target);

            return RedirectToRoute("Folders", new { path = path });
        }
    }
}
