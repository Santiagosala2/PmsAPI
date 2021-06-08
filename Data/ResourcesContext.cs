using Microsoft.EntityFrameworkCore;
using Resources.Models;

namespace Resources.Data
{
    public class ResourcesContext : DbContext
    {
        public ResourcesContext(DbContextOptions<ResourcesContext> opt) : base(opt)
        {
            
        }

        public DbSet<Resource> Resources { get; set; }
    }
}