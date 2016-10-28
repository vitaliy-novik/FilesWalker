using System.Collections.Generic;
using Infrastructure.Entities;

namespace Repository.Interface.Repositories
{
    public interface IRoleRepository : IRepository<IRole>
    {
        IEnumerable<IRole> GetRoles(IUser user);

        void SetUserRole(IUser user, IRole role);

        void SetUserRoles(IEnumerable<IRole> roles, string userId);
    }
}
