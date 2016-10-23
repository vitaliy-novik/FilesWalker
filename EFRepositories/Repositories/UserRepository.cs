using EntityFrameworkContext.Entities;
using Infrastructure.Entities;
using Repository.Interface.Repositories;

namespace EFRepositories.Repositories
{
    public class UserRepository : Repository<User, IUser>, IUserRepository
    {
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
