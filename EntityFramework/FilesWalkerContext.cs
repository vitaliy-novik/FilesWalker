using System.Data.Entity;
using EntityFrameworkContext.Entities;

namespace EntityFrameworkContext
{
    public class FilesWalkerContext : DbContext
    {
        public FilesWalkerContext() : base("FilesWalker") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
    } 
}
