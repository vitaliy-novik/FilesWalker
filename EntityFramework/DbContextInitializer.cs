using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFrameworkContext.Entities;

namespace EntityFrameworkContext
{
    internal class DbContextInitializer : CreateDatabaseIfNotExists<FilesWalkerContext>
    {
        protected override void Seed(FilesWalkerContext context)
        {
            List<Role> roles = new List<Role>()
            {
                new Role() { Name = "Administrator" },
                new Role() { Name = "User" }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            context.Users.Add(new User()
            {
                UserName = "admin",
                Email = "admin",
                Password = "ALAbr9jo7NtKkTIwumGoh2wJMz889pAifwlFLpCkiUnBuu0T0kfHPzbA+/faquX+HA==",
                Roles = roles
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
