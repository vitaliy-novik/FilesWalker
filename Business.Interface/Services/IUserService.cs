using System.Collections.Generic;
using Infrastructure.Entities;

namespace Business.Interface.Services
{
    /// <summary>
    /// Interface to perform actions with users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Create new application user
        /// </summary>
        void Create(IUser user);

        /// <summary>
        /// Delete user
        /// </summary>
        void Delete(IUser user);

        /// <summary>
        /// Retrieve existing user by Id
        /// </summary>
        IUser GetById(string userId);


        /// <summary>
        /// Retrieve existing user by user name
        /// </summary>
        IUser GetByUserName(string userName);

        /// <summary>
        /// Retrieve al exsting users
        /// </summary>
        IEnumerable<IUser> GetAllUsers();

        /// <summary>
        /// Update existing user with new data
        /// </summary>
        void Update(IUser user);
    }
}
