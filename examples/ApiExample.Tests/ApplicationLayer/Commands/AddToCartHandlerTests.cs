using ApiExample.ApplicationLayer.Commands.AddToCart;
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
    public class AddToCartHandlerTests
    {
        private readonly ExampleDbContext _context = InMemoryDbContext.Init(nameof(AddToCartHandlerTests));
        private readonly AddToCartHandler _handler;
        private readonly int FirstProductId = 1;
        private readonly int InactiveProductId = 3;

        public AddToCartHandlerTests()
        {
            _context.Products.AddRange(new List<ProductEntity>()
            {
                new ProductEntity()
                {
                    Name = "Example Product 1",
                    Description = "Some simple product 1",
                    Id = FirstProductId,
                    IsActive = true,
                    Price = 5
                },
                new ProductEntity()
                {
                    Name = "Inactive Product",
                    Description = "An inactive product",
                    Id = InactiveProductId,
                    IsActive = false,
                    Price = 10
                },
            });
            _context.SaveChanges();

            _handler = new AddToCartHandler(_context, new AddToCartValidator());
        }

        [Fact]
        public async Task AddNewItemToCart()
        {
            // Arrange

            // Act
            var result = await _handler.HandleAsync(
                new AddToCartCommand()
                {
                    ProductId = FirstProductId,
                    Quantity = 1,
                    UserId = 1
                },
                CancellationToken.None
            );

            // Assert
            result.Should().BeSuccess();
            _context.CartItems.ToList().Should()
                .ContainEquivalentOf(new CartItemEntity()
                {
                    Id = 1,
                    ProductId = FirstProductId,
                    Quantity = 1,
                    Price = 5,
                    SubPrice = 5,
                    UserId = 1
                });
        }

        [Fact]
        public async Task AddMultiplesOfItemToCart()
        {
            // Arrange

            // Act
            var result = await _handler.HandleAsync(
                new AddToCartCommand()
                {
                    ProductId = FirstProductId,
                    Quantity = 2,
                    UserId = 1
                },
                CancellationToken.None
            );

            // Assert
            result.Should().BeSuccess();
            _context.CartItems.ToList().Should()
                .ContainEquivalentOf(new CartItemEntity()
                {
                    Id = 1,
                    ProductId = FirstProductId,
                    Quantity = 2,
                    Price = 5,
                    SubPrice = 10,
                    UserId = 1
                });
        }

        [Fact]
        public async Task AddItemThatIsAlreadyInTheCart()
        {
            // Arrange
            _context.CartItems.Add(new CartItemEntity()
            {
                Id = 1,
                ProductId = FirstProductId,
                Quantity = 1,
                Price = 5,
                SubPrice = 5,
                UserId = 1
            });
            _context.SaveChanges();

            // Act
            var result = await _handler.HandleAsync(
                new AddToCartCommand()
                {
                    ProductId = FirstProductId,
                    Quantity = 1,
                    UserId = 1
                },
                CancellationToken.None
            );

            // Assert
            result.Should().BeSuccess();
            _context.CartItems.ToList().Should()
                .ContainEquivalentOf(new CartItemEntity()
                {
                    Id = 1,
                    ProductId = FirstProductId,
                    Quantity = 2,
                    Price = 5,
                    SubPrice = 10,
                    UserId = 1
                });
        }

        [Fact]
        public async Task AddItemThatIsInactive()
        {
            // Arrange

            // Act
            var result = await _handler.HandleAsync(
                new AddToCartCommand()
                {
                    ProductId = InactiveProductId,
                    Quantity = 1,
                    UserId = 1
                },
                CancellationToken.None
            );

            // Assert
            result.Should().BeFailure()
                .And.HaveErrorCode(ErrorCodes.ProductIsInactive);
            _context.CartItems.ToList().Should().BeEmpty();
        }
    }
}
