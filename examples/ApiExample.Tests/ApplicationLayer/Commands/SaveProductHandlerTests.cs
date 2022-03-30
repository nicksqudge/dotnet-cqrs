using ApiExample.ApplicationLayer.Commands.SaveProduct;
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

namespace ApiExample.Tests.ApplicationLayer.Commands
{
    public class SaveProductHandlerTests
    {
        private readonly ExampleDbContext context = InMemoryDbContext.Init();
        private readonly SaveProductHandler handler;

        public SaveProductHandlerTests()
        {
            handler = new SaveProductHandler(
                new SaveProductValidator(),
                context
            );
        }

        [Fact]
        public async void CreateNewProduct()
        {
            // Arrange
            var command = new SaveProductCommand()
            {
                Description = "Test Description",
                Name = "Hat",
                Price = 10,
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Should().BeSuccess();
        }

        [Fact]
        public async void UpdateExistingProduct()
        {
            // Arrange
            context.Products.Add(new ProductEntity()
            {
                Id = 1,
                Name = "Before Hat",
                Description = "None",
                Price = 30,
                IsActive = true
            });
            await context.SaveChangesAsync();

            var command = new SaveProductCommand()
            {
                Description = "Test Description",
                Name = "Big Hat",
                Price = 12,
                ProductId = 1
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Should().BeSuccess();
            context.Products.FirstOrDefault(p => p.Id == 1)
                .Should()
                .NotBeNull().And
                .BeEquivalentTo(new ProductEntity()
                {
                    Id = 1,
                    Description = "Test Description",
                    Name = "Big Hat",
                    Price = 12,
                    IsActive = true
                });
        }

        [Fact]
        public async void TryToUpdateNonExistentProduct()
        {
            // Arrange
            var command = new SaveProductCommand()
            {
                Description = "Test Description",
                Name = "Big Hat",
                Price = 12,
                ProductId = 1
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Should().BeFailure()
                .And.HaveErrorCode(ErrorCodes.NotFound);
        }
    }
}
