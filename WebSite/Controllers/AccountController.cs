using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using WebSite.Managers;
using WebSite.Mapping;
using WebSite.Models;
using WebSite.ViewModels.Account;

namespace WebSite.Controllers
{
    /// <summary>
    /// Controller for accounts operations
    /// </summary>
    public class AccountController : Controller
    {
        [Dependency]
        public IUserManager<IdentityUser> UserManager { get; set; }

        /// <summary>
        /// Returns form to log in
        /// </summary>
        /// <returns>View with login form</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Logins user
        /// </summary>
        /// <param name="model">Model with user name and password</param>
        /// <param name="returnUrl">Url to redirect user</param>
        /// <returns>Redirect to required page</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindAsync(model.UserName, model.Password);

                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                    ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
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

        /// <summary>
        /// User log off
        /// </summary>
        /// <returns>redirect to login</returns>
        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Returns form to register new user
        /// </summary>
        /// <returns>View with registration form</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="model">Registration user model</param>
        /// <returns>Redirect to login form</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = UserMapper.Map(model);

                IdentityResult result = await this.UserManager.CreateAsync(user, model.Password);
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
