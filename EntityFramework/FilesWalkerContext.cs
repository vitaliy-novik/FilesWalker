using System.Data.Entity;
using EntityFrameworkContext.Entities;

namespace EntityFrameworkContext
{
    public class FilesWalkerContext : DbContext
    {
        public FilesWalkerContext() : base("FilesWalker")
        {
            Database.SetInitializer<FilesWalkerContext>(new DbContextInitializer());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    } 
}
