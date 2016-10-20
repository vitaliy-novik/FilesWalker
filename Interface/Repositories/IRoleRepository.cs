using System.Collections.Generic;
using Infrastructure.Entities;

namespace Repository.Interface.Repositories
{
    public interface IRoleRepository : IRepository<IRole>
    {
        IEnumerable<IRole> GetRoles(IUser user);
    }
}
