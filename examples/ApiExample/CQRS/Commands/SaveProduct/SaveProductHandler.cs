using DotnetCQRS;
using DotnetCQRS.Commands;
using FluentValidation;

namespace ApiExample.CQRS.Commands.SaveProduct
{
    public class SaveProductHandler : ICommandHandler<SaveProductCommand>
    {
        private readonly IValidator<SaveProductCommand> validator;

        public SaveProductHandler(IValidator<SaveProductCommand> validator)
        {
            this.validator = validator;
        }

        public async Task<Result> HandleAsync(SaveProductCommand command, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            
        }

        private Task CreateNewProduct(SaveProductCommand command, CancellationToken cancellationToken)
        {

        }

        private Task UpdateProduct(SaveProductCommand command, CancellationToken cancellationToken)
        {

        }

        private Task<bool> DoesProductExist(SaveProductCommand command, CancellationToken cancellationToken)
        {

        }
    }
}
