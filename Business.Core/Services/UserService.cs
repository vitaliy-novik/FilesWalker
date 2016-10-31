using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Services;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace Business.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

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
        
        public void Create(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.userRepository.Create(user);
            this.roleRepository.SetUserRole(user, roleRepository.GetAll(r => r.Name == "User").FirstOrDefault());
        }


        public void Delete(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.userRepository.Delete(user);
        }


        public IUser GetById(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            return this.userRepository.GetById(userId);
        }


        public IUser GetByUserName(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            return this.userRepository.GetAll(user => user.UserName == userName).FirstOrDefault();
        }

        public IEnumerable<IUser> GetAllUsers()
        {
            return this.userRepository.GetAll();
        }


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
