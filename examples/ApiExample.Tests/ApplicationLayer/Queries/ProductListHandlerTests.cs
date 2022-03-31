using ApiExample.ApplicationLayer.Queries.ProductList;
using ApiExample.DataAccessLayer;
using ApiExample.Tests.TestHelpers;
using DotnetCQRS.Extensions.FluentAssertions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApiExample.Tests.ApplicationLayer.Queries
{
    public class ProductListHandlerTests
    {
        private readonly ExampleDbContext _context = InMemoryDbContext.Init(nameof(ProductListHandlerTests));
        private readonly ProductListHandler _handler;

        public ProductListHandlerTests()
        {
            _context.Products.Add(new ProductEntity()
            {
                Name = "Inactive Product",
                Description = "Inactive product desc",
                Id = 1,
                IsActive = false,
                Price = 12.45
            });
            _context.Products.Add(new ProductEntity()
            {
                Name = "Active Product",
                Description = "Active product desc",
                Id = 2,
                IsActive = true,
                Price = 10
            });
            _context.SaveChanges();

            _handler = new ProductListHandler(_context);
        }

        [Fact]
        public async Task ShowOnlyActive()
        {
            var result = await _handler.HandleAsync(new ProductListQuery(), CancellationToken.None);
            result.Should().BeSuccess()
                .And.ResultValue.Products.Should().ContainEquivalentOf(new ProductListResult.Product()
                {
                    Name = "Active Product",
                    Description = "Active product desc",
                    Id = 2,
                    Price = "£10.00"
                });
        }

        [Fact]
        public async Task ShowAll()
        {
            var result = await _handler.HandleAsync(new ProductListQuery()
            {
                ShowOnlyActive = false
            }, CancellationToken.None);

            result.Should().BeSuccess()
                .And.ResultValue.Products.Should()
                .ContainEquivalentOf(new ProductListResult.Product()
                {
                    Name = "Active Product",
                    Description = "Active product desc",
                    Id = 2,
                    Price = "£10.00"
                })
                .And
                .ContainEquivalentOf(new ProductListResult.Product()
                {
                    Name = "Inactive Product",
                    Description = "Inactive product desc",
                    Id = 1,
                    Price = "£12.45"
                });
        }
    }
}
