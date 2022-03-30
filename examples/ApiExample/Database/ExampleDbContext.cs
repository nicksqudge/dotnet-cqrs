using Microsoft.EntityFrameworkCore;

namespace ApiExample.Database
{
    public class ExampleDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public ExampleDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
