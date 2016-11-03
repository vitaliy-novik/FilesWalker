using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Services;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace Business.Core.Services
{
    /// <summary>
    /// Class for operations with users
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// Create new instance of UserService
        /// </summary>
        /// <param name="userRepository">IUserRepository instance</param>
        /// <param name="roleRepository">IRoleRepository instance</param>
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (roleRepository == null)
            {
                throw new ArgumentNullException("roleRepository");
            }

            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        /// <summary>
        /// Create new application user
        /// </summary>
        /// <param name="user">New user data</param>
        public void Create(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.userRepository.Create(user);
            this.roleRepository.SetUserRole(user, roleRepository.GetAll(r => r.Name == "User").FirstOrDefault());
        }

        /// <summary>
        /// Delete existing user
        /// </summary>
        /// <param name="user">User entity to delete</param>
        public void Delete(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.userRepository.Delete(user);
        }

        /// <summary>
        /// Returns user entity found by Id
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>User entity</returns>
        public IUser GetById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }

            return this.userRepository.GetById(userId);
        }

        /// <summary>
        /// Returns user entity found by user name
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>User entity</returns>
        public IUser GetByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            return this.userRepository.GetAll(user => user.UserName == userName).FirstOrDefault();
        }


        /// <summary>
        /// Returns list of all application users
        /// </summary>
        /// <returns>Users list</returns>
        public IEnumerable<IUser> GetAllUsers()
        {
            return this.userRepository.GetAll();
        }

        /// <summary>
        /// Update user entity with new data
        /// </summary>
        /// <param name="user">New user data</param>
        public void Update(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            userRepository.Update(user);
        }
    }
}
