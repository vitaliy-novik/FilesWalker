using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Business.Interface.Services
{
    public interface IUserService
    {
        void Create(IUser user);

        void Delete(IUser user);

        IUser GetById(string userId);

        IUser GetByUserName(string userName);

        IEnumerable<IUser> GetAllUsers();

        void Updat(IUser user);
    }
}
