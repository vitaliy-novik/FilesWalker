using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebSite.Managers;
using WebSite.Mapping;
using WebSite.Models;
using WebSite.ViewModels.Account;

namespace WebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager<IdentityUser> userManager;

        public AccountController(IUserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindAsync(model.UserName, model.Password);

                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                    ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Folders");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = UserMapper.Map(model);

                IdentityResult result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    var errors = string.Join(",", result.Errors);
                    ModelState.AddModelError(string.Empty, errors);
                }
            }

            return View(model);
        }


    }
}
