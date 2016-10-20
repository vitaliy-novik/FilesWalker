using System.Data.Entity;
using EntityFrameworkContext.Entities;

namespace EntityFrameworkContext
{
    internal class DbContextInitializer : CreateDatabaseIfNotExists<FilesWalkerContext>
    {
        protected override void Seed(FilesWalkerContext context)
        {
            context.Roles.Add(new Role { Name = "Administrator" });
            context.Roles.Add(new Role { Name = "Moderator" });
            context.Roles.Add(new Role { Name = "User" });
            context.Roles.Add(new Role { Name = "Guest" });

            base.Seed(context);
        }
    }
}
