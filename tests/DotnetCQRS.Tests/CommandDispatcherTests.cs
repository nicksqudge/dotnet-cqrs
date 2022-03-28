using DotnetCQRS.Commands;
using DotnetCQRS.Extensions.FluentAssertions;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DotnetCQRS.Tests
{
    public class CommandDispatcherTests
    {
        [Fact]
        public async Task GivenACommand_WhenRequestingIt_ThenFindTheHandler()
        {
            var services = new ServiceCollection()
                .AddCommandHandler<CommandTest, CommandTestHandler>()
                .BuildServiceProvider();

            var commandDispatcher = new CommandDispatcher(services);
            var result = await commandDispatcher.Run(new CommandTest(), CancellationToken.None);
            result.Should().BeFailure()
                .And.HaveErrorCode("CommandTestRan");
        }

        public class CommandTest : ICommand
        {
            
        }

        public class CommandTestHandler : ICommandHandler<CommandTest>
        {
            public async Task<Result> HandleAsync(CommandTest command, CancellationToken cancellationToken)
            {
                return Result.Failure("CommandTestRan");
            }
        }
    }
}