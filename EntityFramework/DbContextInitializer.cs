using System.Collections.Generic;
using System.Data.Entity;
using EntityFrameworkContext.Entities;

namespace EntityFrameworkContext
{
    /// <summary>
    /// Entity Framework context initializer
    /// </summary>
    internal class DbContextInitializer : CreateDatabaseIfNotExists<FilesWalkerContext>
    {
        /// <summary>
        /// Adds roles and administrator to data base
        /// </summary>
        /// <param name="context"></param>
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
