using ApiExample.DataAccessLayer;
using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.ApplicationLayer.Commands.AddToCart
{
    public class AddToCartHandler : ICommandHandler<AddToCartCommand>
    {
        private readonly ExampleDbContext _context;
        private readonly IValidator<AddToCartCommand> _validator;

        public AddToCartHandler(ExampleDbContext context, IValidator<AddToCartCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Result> HandleAsync(AddToCartCommand command, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(command, cancellationToken);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);
            if (product == null)
                return Result.Failure(ErrorCodes.NotFound);

            if (product.IsActive == false)
                return Result.Failure(ErrorCodes.ProductIsInactive);

            var cartItem = await _context.CartItems
                .Where(ci => ci.ProductId == command.ProductId)
                .Where(ci => ci.UserId == command.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (cartItem == null)
            {
                cartItem = new CartItemEntity()
                {
                    ProductId = command.ProductId,
                    Price = product.Price,
                    UserId = command.UserId,
                };
                _context.CartItems.Add(cartItem);
            }

            cartItem.Quantity += command.Quantity;
            cartItem.UpdateSubPrice();

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
