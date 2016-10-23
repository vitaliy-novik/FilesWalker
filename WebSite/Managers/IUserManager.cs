using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WebSite.Models;

namespace WebSite.Managers
{
    public interface IUserManager<TUser>
    {
        Task<TUser> FindAsync(string userName, string password);

        Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType);

        Task<IdentityResult> CreateAsync(TUser user, string password);

        Task<IdentityUser> FindByIdAsync(string id);

        Task<IList<string>> GetRolesAsync(string userId);
    }
}
