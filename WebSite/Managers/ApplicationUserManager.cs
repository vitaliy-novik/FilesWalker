using Microsoft.AspNet.Identity;
using WebSite.Models;

namespace WebSite.Managers
{
    internal class ApplicationUserManager : UserManager<IdentityUser>, IUserManager<IdentityUser>
    {
        public ApplicationUserManager(IUserStore<IdentityUser> store) : base(store)
        {
        }
    }
}