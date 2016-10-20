using System;
using System.Collections.Generic;
using Infrastructure.Entities;

namespace EntityFrameworkContext.Entities
{
    public class Role : IRole
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<User> Users { get; set; } 
    }
}
