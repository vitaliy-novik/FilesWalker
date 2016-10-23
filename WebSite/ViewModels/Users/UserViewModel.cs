using System.Collections.Generic;
using Infrastructure.Entities;

namespace WebSite.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<IRole> Roles { get; set; }
    }
}