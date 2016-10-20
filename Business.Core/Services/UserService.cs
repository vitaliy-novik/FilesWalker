using System;
using System.Linq;
using Business.Interface.Services;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace Business.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            this.userRepository = userRepository;
        }
        
        public void Create(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.userRepository.Create(user);
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

            return this.userRepository.GetAll(user => user.Email == userName).FirstOrDefault();
        }
    }
}
