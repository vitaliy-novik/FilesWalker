using System;
using System.Collections.Generic;
using Business.Interface.Services;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace Business.Core.Services
{
    /// <summary>
    /// Class for operations with users roles
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// Create a new instance of RoleService
        /// </summary>
        /// <param name="roleRepository">IRoleRepository instance</param>
        public RoleService(IRoleRepository roleRepository)
        {
            if (roleRepository == null)
            {
                throw new ArgumentNullException("roleRepository");
            }

            this.roleRepository = roleRepository;
        }

        /// <summary>
        /// Returns all user roles
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns>List of roles</returns>
        public IEnumerable<IRole> GetRoles(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return roleRepository.GetRoles(user);
        }

        /// <summary>
        /// Returns all possible users roles in application
        /// </summary>
        /// <returns>List of roles</returns>
        public IEnumerable<IRole> GetAllRoles()
        {
            return roleRepository.GetAll();
        }

        /// <summary>
        /// Set roles for user
        /// </summary>
        /// <param name="roles">List of user roles</param>
        /// <param name="userId">Id of target user</param>
        public void SetUserRoles(IEnumerable<IRole> roles, string userId)
        {
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }
             
            roleRepository.SetUserRoles(roles, userId);
        }
    }
}
