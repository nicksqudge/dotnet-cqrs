using ApiExample.DataAccessLayer;
using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.ApplicationLayer.Commands.SaveProduct
{
    public class SaveProductHandler : ICommandHandler<SaveProductCommand>
    {
        private readonly IValidator<SaveProductCommand> validator;
        private readonly ExampleDbContext context;

        public SaveProductHandler(IValidator<SaveProductCommand> validator, ExampleDbContext context)
        {
            this.validator = validator;
            this.context = context;
        }

        public async Task<Result> HandleAsync(SaveProductCommand command, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var productExists = await DoesProductExist(command, cancellationToken);

            if (command.HasProductId())
            {
                if (productExists)
                    await UpdateProduct(command, cancellationToken);
                else
                    return Result.Failure(ErrorCodes.NotFound);
            }
            else
            {
                CreateNewProduct(command, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        private void CreateNewProduct(SaveProductCommand command, CancellationToken cancellationToken)
        {
            var entity = new ProductEntity()
            {
                Description = command.Description,
                IsActive = true,
                Name = command.Name,
                Price = command.Price,
            };
            
            context.Products.Add(entity);
        }

        private async Task UpdateProduct(SaveProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await context.Products
                .FirstAsync(p => p.Id == command.ProductId, cancellationToken);

            entity.Description = command.Description;
            entity.Price = command.Price;
            entity.Name = command.Name;
        }

        private async Task<bool> DoesProductExist(SaveProductCommand command, CancellationToken cancellationToken)
        {
            if (command.ProductId == 0)
                return false;

            return await context
                .Products
                .AnyAsync(p => p.Id == command.ProductId, cancellationToken);
        }
    }
}
