using System;
using System.Collections.Generic;
using Business.Interface.Services;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace Business.Core.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            if (roleRepository == null)
            {
                throw new ArgumentNullException("roleRepository");
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
    }
}
