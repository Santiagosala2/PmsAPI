using Groups.Models;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Resources.Models;
using Tokens.Models;
using Users.Models;
using Accounts.Models;

namespace Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {
            
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}