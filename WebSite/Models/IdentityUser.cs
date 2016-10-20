using System;

namespace WebSite.Models
{
    public class IdentityUser: Infrastructure.Entities.IUser, Microsoft.AspNet.Identity.IUser
    {
        public IdentityUser()
            : this(Guid.NewGuid().ToString())
        {
        }

        public IdentityUser(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}