using System;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace EntityFrameworkContext.Entities
{
    public class User : IUser
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IList<Role> Roles { get; set; } 
    }
}
