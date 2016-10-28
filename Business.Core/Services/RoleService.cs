using System;
using System.Collections;
using System.Collections.Generic;
using Business.Interface.Services;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace Business.Core.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository roleRepository;
        private IUserRepository userRepository;

        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            if (roleRepository == null)
            {
                throw new ArgumentNullException("roleRepository");
            }

            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            this.roleRepository = roleRepository;
        }

        public IEnumerable<IRole> GetRoles(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return roleRepository.GetRoles(user);
        }


        public IEnumerable<IRole> GetAllRoles()
        {
            return roleRepository.GetAll();
        }


        public void SetUserRoles(IEnumerable<IRole> roles, string userId)
        {
            if (roles == null)
            {
                throw new ArgumentNullException("roles");
            }
             
            roleRepository.SetUserRoles(roles, userId);
        }
    }
}
