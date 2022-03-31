using ApiExample.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ApiExample.Tests.TestHelpers;

public class InMemoryDbContext
{
    public static ExampleDbContext Init()
    {
        var options = new DbContextOptionsBuilder<ExampleDbContext>()
            .UseInMemoryDatabase("ApiExample")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var context = new ExampleDbContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();

        return context;
    }
}