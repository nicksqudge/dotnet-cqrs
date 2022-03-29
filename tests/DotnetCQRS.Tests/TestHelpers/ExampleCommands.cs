using System.Threading;
using System.Threading.Tasks;
using DotnetCQRS.Commands;
using DotnetCQRS.Queries;

namespace DotnetCQRS.Tests.TestHelpers
{
    public class ExampleCommand : ICommand
    {

    }

    public class ExampleCommandHandler : ICommandHandler<ExampleCommand>
    {
        public async Task<Result> HandleAsync(ExampleCommand command, CancellationToken cancellationToken)
        {
            return Result.Success();
        }
    }
}