using Microsoft.EntityFrameworkCore;

namespace ApiExample.DataAccessLayer;

public class ExampleDbContext : DbContext
{
    public ExampleDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ProductEntity> Products { get; set; }
}