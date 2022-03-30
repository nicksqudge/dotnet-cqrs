using DotnetCQRS;
using DotnetCQRS.Commands;

namespace ApiExample.CQRS.Commands.SaveProduct
{
    public class SaveProductHandler : ICommandHandler<SaveProductCommand>
    {
        public Task<Result> HandleAsync(SaveProductCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
