using System.Collections.Generic;
using Infrastructure.Entities;

namespace Repository.Interface.Repositories
{
    /// <summary>
    /// Interface for operations with roles
    /// </summary>
    public interface IRoleRepository : IRepository<IRole>
    {
        /// <summary>
        /// Returns all user roles
        /// </summary>
        /// <param name="user">Target user</param>
        /// <returns>List f roles</returns>
        IEnumerable<IRole> GetRoles(IUser user);

        /// <summary>
        /// Set role for user
        /// </summary>
        /// <param name="user">Target user</param>
        /// <param name="role">User role</param>
        void SetUserRole(IUser user, IRole role);

        /// <summary>
        /// Replace all user roles with list
        /// </summary>
        /// <param name="roles">Roles list</param>
        /// <param name="userId">Target user Id</param>
        void SetUserRoles(IEnumerable<IRole> roles, string userId);
    }
}
