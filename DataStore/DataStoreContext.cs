using Groups.Models;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Resources.Models;
using Tokens.Models;
using Users.Models;
using Accounts.Models;

namespace DataStore
{
    public class DataStoreContext : DbContext
    {
        public DataStoreContext(DbContextOptions<DataStoreContext> opt) : base(opt)
        {
            
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<TokenReadDto> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}