using System.Collections.Generic;
using Infrastructure.Entities;

namespace Business.Interface.Services
{
    public interface IRoleService
    {
        IEnumerable<IRole> GetRoles(IUser user);

        IEnumerable<IRole> GetAllRoles();

        void SetUserRoles(IEnumerable<IRole> roles, string userId);
    }
}
