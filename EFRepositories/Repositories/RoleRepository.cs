using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFrameworkContext;
using EntityFrameworkContext.Entities;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace EFRepositories.Repositories
{
    public class RoleRepository : Repository<Role, IRole>, IRoleRepository
    {
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
