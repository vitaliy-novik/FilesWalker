using System;
using System.Collections.Generic;
using System.Linq;
using EntityFrameworkContext;
using EntityFrameworkContext.Entities;
using Infrastructure.Entities;
using Repository.Interface.Repositories;
using Role = EntityFrameworkContext.Entities.Role;

namespace EFRepositories.Repositories
{
    /// <summary>
    /// Class for roles access operations
    /// </summary>
    public class RoleRepository : Repository<Role, IRole>, IRoleRepository
    {
        /// <summary>
        /// Set role for user
        /// </summary>
        /// <param name="user">Target user</param>
        /// <param name="role">User role</param>
        public void SetUserRole(IUser user, IRole role)
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                var dalUser = context.Users.Include("Roles").FirstOrDefault(u => u.Id == user.Id);
                if (dalUser == null)
                {
                    throw new ArgumentException("User does not exist");
                }

                dalUser.Roles.Add(context.Roles.Attach(ConvertToDal(role)));
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all user roles
        /// </summary>
        /// <param name="user">Target user</param>
        /// <returns>List f roles</returns>
        public IEnumerable<IRole> GetRoles(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                var account = context.Set<User>().Include("Roles").FirstOrDefault(u => u.Id == user.Id);
                if (account == null)
                {
                    throw new ArgumentException("user");
                }
                else
                {
                    return account.Roles;
                }
            }
        }

        /// <summary>
        /// Replace all user roles with list
        /// </summary>
        /// <param name="roles">Roles list</param>
        /// <param name="userId">Target user Id</param>
        public void SetUserRoles(IEnumerable<IRole> roles, string userId)
        {
            using (FilesWalkerContext context = new FilesWalkerContext())
            {
                var dalUser = context.Users.Include("Roles").FirstOrDefault(u => u.Id == userId);
                if (dalUser == null)
                {
                    throw new ArgumentException("User does not exist");
                }

                foreach (IRole role in roles)
                {
                    if (dalUser.Roles.All(r => r.Id != role.Id))
                    {
                        dalUser.Roles.Add(context.Roles.Attach(ConvertToDal(role)));
                    }
                }

                for (int i = 0; i < dalUser.Roles.Count; ++i)
                {
                    if (roles.All(r => r.Id != dalUser.Roles[i].Id))
                    {
                        dalUser.Roles.RemoveAt(i);
                    }
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Convert entity to ORM role entity
        /// </summary>
        /// <param name="entity">Role entity</param>
        /// <returns>ORM role entity</returns>
        protected override Role ConvertToDal(IRole entity)
        {
            return new Role()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

    }
}
