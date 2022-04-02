using ApiExample.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ApiExample.Tests.TestHelpers;

public class InMemoryDbContext
{
    public static ExampleDbContext Init(string name)
    {
        var options = new DbContextOptionsBuilder<ExampleDbContext>()
            .UseInMemoryDatabase(name)
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var context = new ExampleDbContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();

        return context;
    }
}