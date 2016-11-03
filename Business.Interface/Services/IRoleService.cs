using System.Collections.Generic;
using Infrastructure.Entities;

namespace Business.Interface.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Returns all user roles
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns>List of roles</returns>
        IEnumerable<IRole> GetRoles(IUser user);

        /// <summary>
        /// Returns all possible users roles in application
        /// </summary>
        /// <returns>List of roles</returns>
        IEnumerable<IRole> GetAllRoles();

        /// <summary>
        /// Set roles for user
        /// </summary>
        /// <param name="roles">List of user roles</param>
        /// <param name="userId">Id of target user</param>
        void SetUserRoles(IEnumerable<IRole> roles, string userId);
    }
}
