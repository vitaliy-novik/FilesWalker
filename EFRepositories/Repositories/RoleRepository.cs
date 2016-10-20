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
        public RoleRepository(FilesWalkerContext context)
            : base(context)
        {
        }

        public IEnumerable<IRole> GetRoles(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

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
