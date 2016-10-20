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

        [Authorize]
        public ActionResult Create(string path)
        {
            return View();
        }
    }
}
