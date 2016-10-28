using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Business.Interface.Services;
using Microsoft.AspNet.Identity;
using WebSite.Mapping;
using WebSite.Models;

namespace WebSite.Managers
{
    internal class UserStore : IUserStore<IdentityUser>, IUserRoleStore<IdentityUser>, IUserPasswordStore<IdentityUser>
    {
        private readonly IUserService userService;

        private readonly IRoleService roleService;

        public UserStore(IUserService userService, IRoleService roleService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            if (roleService == null)
            {
                throw new ArgumentNullException("roleService");
            }

            this.userService = userService;
            this.roleService = roleService;
        }

        public Task CreateAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.Run(() => this.userService.Create(user));
        }

        public Task DeleteAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.Run(() => this.userService.Delete(user));
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            return Task.Run(
                () =>
                {
                    var user = this.userService.GetById(userId);
                    return UserMapper.MapUserToIdentity(user);
                });
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            return Task.Run(
                () =>
                {
                    var account = this.userService.GetByUserName(userName);
                    return account == null ? null : UserMapper.MapUserToIdentity(account);
                });
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.Run(
                () =>
                {
                    var roles = this.roleService.GetRoles(user);
                    return (IList<string>)new List<string>(roles.Select(role => role.Name));
                });
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult<string>(user.Password);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.Password = passwordHash;
            return Task.FromResult<IdentityResult>(IdentityResult.Success);
        }
    }
}