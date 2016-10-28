using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Business.Interface.Services;
using Infrastructure.Entities;
using Microsoft.AspNet.Identity;
using WebGrease;
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
            Folder folder;
            try
            {
                folder = foldersService.GetDirectories(user, path);
            }
            catch (Exception exception)
            {
                return View("ErrorsList", ProcessException(exception));
            }

            return View("Index", FolderMapper.Map(folder));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create(string path)
        {
            return View("CreateFolderDialog");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(CreateFolderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("ErrorsList", ProceedModelState());
            }

            Folder folder = FolderMapper.Map(viewModel);
            foldersService.CreateFolder(folder.Path);
            return Content(viewModel.Path, "url");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Delete(string path, IEnumerable<string> folders)
        {
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    foldersService.DeleteFolders(path, folders);
                }
                catch (Exception exception)
                {
                    return PartialView("ErrorsList", ProcessException(exception));
                }
            }

            return Content(path, "url");
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
            try
            {
                foldersService.Rename(viewModel.Path, viewModel.OldName, viewModel.NewName);
            }
            catch (Exception exception)
            {
                return PartialView("ErrorsList", ProcessException(exception));
            }

            return Content(viewModel.Path, "url");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CopyTo(string path, string source, string target)
        {
            try
            {
                foldersService.CopyTo(path, source, target);
            }
            catch (Exception exception)
            {
                return PartialView("ErrorsList", ProcessException(exception));
            }


            return Content(path, "url");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult CreateFile(string path)
        {
            return View("CreateFileDialog");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult CreateFile(CreateFileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("ErrorsList", ProceedModelState());
            }

            foldersService.CreateFile(viewModel.Path, viewModel.Name);
            return Content(viewModel.Path, "url");
        }

        private IEnumerable<string> ProceedModelState()
        {
            return ModelState.Values.SelectMany(value => value.Errors.Select(error => error.ErrorMessage));
        }

        private IEnumerable<string> ProcessException(Exception exception)
        {
            return new List<string>() { exception.Message };
        } 
    }
}
