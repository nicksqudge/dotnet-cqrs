using ApiExample.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiExample.Tests.TestHelpers
{
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
}
