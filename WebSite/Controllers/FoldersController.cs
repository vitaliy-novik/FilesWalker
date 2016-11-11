using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.Interface.Services;
using Infrastructure.Entities;
using WebSite.Mapping;
using WebSite.ViewModels.Folders;
using Microsoft.Practices.Unity;

namespace WebSite.Controllers
{
    /// <summary>
    /// Controller for file system operations
    /// </summary>
    public class FoldersController : Controller
    {
        [Dependency]
        public IFoldersService FoldersService { get; set; }

        /// <summary>
        /// Returns view with root computer directory
        /// </summary>
        [Authorize(Roles = "Administrator,User")]
        public ActionResult Index()
        {
            Folder folder = FoldersService.GetDirectories();

            return View(FolderMapper.Map(folder));
        }

        /// <summary>
        /// Return view with directory specified by path
        /// </summary>
        /// <param name="path">Path to the directory</param>
        [Authorize(Roles = "Administrator,User")]
        public ActionResult Folder(string path)
        {
            return GetFolderView(path);
        }

        /// <summary>
        /// Returns form to create new directory
        /// </summary>
        /// <param name="path">Path to parent directory</param>
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(string path)
        {
            return PartialView("CreateFolderDialog");
        }

        /// <summary>
        /// Creates new directory
        /// </summary>
        /// <param name="viewModel">Path to parent directory</param>
        /// <returns>View with updated parent directory</returns>
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
                FoldersService.CreateFolder(folder.Path);
            }
            catch (Exception exception)
            {
                return PartialView("ErrorsList", ProcessException(exception));
            }
            
            return GetFolderView(viewModel.Path);
        }
        
        /// <summary>
        /// Deletes list of directories and files
        /// </summary>
        /// <param name="path">Path to the parent directory</param>
        /// <param name="folders">List of directorie and files names</param>
        /// <returns>View with updated parent directory</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Delete(string path, IEnumerable<string> folders)
        {
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    FoldersService.DeleteFolders(path, folders);
                }
                catch (Exception exception)
                {
                    return PartialView("ErrorsList", ProcessException(exception));
                }
            }

            return GetFolderView(path);
        }

        /// <summary>
        /// Returns form to rename file or directory
        /// </summary>
        /// <param name="path">Path to the parent directory</param>
        /// <param name="folder">Folder or file name</param>
        [Authorize(Roles = "Administrator")]
        public ActionResult Rename(string path, string folder)
        {
            RenameFolderViewModel viewModel = new RenameFolderViewModel()
            {
                OldName = folder,
                Path = path,
                NewName = folder
            };

            return PartialView("RenameFolderDialog", viewModel);
        }

        /// <summary>
        /// Renames folder or file
        /// </summary>
        /// <param name="viewModel">Data for renaming</param>
        /// <returns>View with updated parent directory</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Rename(RenameFolderViewModel viewModel)
        {
            try
            {
                FoldersService.Rename(viewModel.Path, viewModel.OldName, viewModel.NewName);
            }
            catch (Exception exception)
            {
                return PartialView("ErrorsList", ProcessException(exception));
            }

            return GetFolderView(viewModel.Path);
        }

        /// <summary>
        /// Moves file or directory in target directory
        /// </summary>
        /// <param name="path">Path to the parent directory</param>
        /// <param name="source">File or directory name to move</param>
        /// <param name="target">Target directory name</param>
        /// <returns>View with updated parent directory</returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult CopyTo(string path, string source, string target)
        {
            try
            {
                FoldersService.CopyTo(path, source, target);
            }
            catch (Exception exception)
            {
                return PartialView("ErrorsList", ProcessException(exception));
            }


            return GetFolderView(path);
        }

        /// <summary>
        /// Returns form to create new file
        /// </summary>
        /// <param name="path">Path to the directory, where file will be created</param>
        /// <returns>Dialog with form</returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateFile(string path)
        {
            return PartialView("CreateFileDialog");
        }

        /// <summary>
        /// Creates new file
        /// </summary>
        /// <param name="viewModel">Model for file creation</param>
        /// <returns>Updated parent directory if success, error partial view if operation failed</returns>
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
                FoldersService.CreateFile(viewModel.Path, viewModel.Name);
            }
            catch (Exception exception)
            {
                return PartialView("ErrorsList", ProcessException(exception));
            }
            
            return GetFolderView(viewModel.Path);
        }

        /// <summary>
        /// Return view with directory content
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <returns>Directory view, partial view(if ajax request) or errors partial view</returns>
        private ActionResult GetFolderView(string path)
        {
            Folder folder;
            try
            {
                folder = FoldersService.GetDirectories(path);
            }
            catch (Exception exception)
            {
                return View("ErrorsList", ProcessException(exception));
            }

            FolderViewModel viewModel = FolderMapper.Map(folder);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Folders", viewModel);
            }

            return View("Index", viewModel);
        }

        /// <summary>
        /// Retrieve errors list from ModelState object
        /// </summary>
        /// <returns>Enumeration of errors</returns>
        private IEnumerable<string> ProceedModelState()
        {
            return ModelState.Values.SelectMany(value => value.Errors.Select(error => error.ErrorMessage));
        }

        /// <summary>
        /// Retrieve exception message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Enumeration with one exception message</returns>
        private IEnumerable<string> ProcessException(Exception exception)
        {
            return new List<string>() { exception.Message };
        }
    }
}
