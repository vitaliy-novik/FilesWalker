using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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