using EntityFrameworkContext.Entities;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace EFRepositories.Repositories
{
    /// <summary>
    /// Class for users access operations
    /// </summary>
    public class UserRepository : Repository<User, IUser>, IUserRepository
    {
        /// <summary>
        /// Conver user to ORM user entity
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns>ORM user Entity</returns>
        protected override User ConvertToDal(IUser user)
        {
            return new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}
