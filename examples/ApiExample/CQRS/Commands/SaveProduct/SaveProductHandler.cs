using ApiExample.Database;
using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApiExample.CQRS.Commands.SaveProduct
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

            if (await DoesProductExist(command, cancellationToken))
            {
                await UpdateProduct(command, cancellationToken);
                return Result.Success();
            }

            await CreateNewProduct(command, cancellationToken);
            return Result.Success();
        }

        private async Task CreateNewProduct(SaveProductCommand command, CancellationToken cancellationToken)
        {
            var entity = new ProductEntity()
            {
                Description = command.Description,
                IsActive = true,
                Name = command.Name,
                Price = command.Price,
            };
            
            context.Products.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        private async Task UpdateProduct(SaveProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await context.Products
                .FirstAsync(p => p.Id == command.ProductId, cancellationToken);

            entity.Description = command.Description;
            entity.Price = command.Price;
            entity.Name = command.Name;

            await context.SaveChangesAsync(cancellationToken);
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
