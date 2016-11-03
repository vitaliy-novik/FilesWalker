using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.Interface.Services;
using Infrastructure.Entities;
using WebSite.Mapping;
using WebSite.ViewModels.Folders;

namespace WebSite.Controllers
{
    public class FoldersController : Controller
    {
        private readonly IFoldersService foldersService;

        public FoldersController(IFoldersService foldersService)
        {
            if (foldersService == null)
            {
                throw new ArgumentNullException("foldersService");
            }

            this.foldersService = foldersService;
        }


        [Authorize]
        public ActionResult Index()
        {
            Folder folder = foldersService.GetDirectories();

            return View(FolderMapper.Map(folder));
        }

        [Authorize]
        public ActionResult Folder(string path)
        {
            Folder folder;
            try
            {
                folder = foldersService.GetDirectories(path);
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
            try
            {
                foldersService.CreateFolder(folder.Path);
            }
            catch (Exception exception)
            {
                return PartialView("ErrorsList", ProcessException(exception));
            }
            
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

            try
            {
                foldersService.CreateFile(viewModel.Path, viewModel.Name);
            }
            catch (Exception exception)
            {
                return PartialView("ErrorsList", ProcessException(exception));
            }
            
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

        private Action<int> meth()
        {
            return m =>
            {
                return;
            };
        } 
    }
}
