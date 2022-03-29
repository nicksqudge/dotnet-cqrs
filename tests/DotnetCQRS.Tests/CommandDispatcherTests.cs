using DotnetCQRS.Commands;
using DotnetCQRS.Extensions.FluentAssertions;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using DotnetCQRS.Tests.TestHelpers;
using Xunit;

namespace DotnetCQRS.Tests
{
    public class CommandDispatcherTests
    {
        [Fact]
        public async Task GivenACommand_WhenRequestingIt_ThenFindTheHandler()
        {
            var services = new ServiceCollection()
                .AddDotnetCQRS()
                .AddCommandHandler<CommandTest, CommandTestHandler>()
                .BuildServiceProvider();

            var commandDispatcher = services.GetRequiredService<ICommandDispatcher>();
            var result = await commandDispatcher.Run(new CommandTest(), CancellationToken.None);
            result.Should().BeFailure()
                .And.HaveErrorCode("CommandTestRan");
        }

        [Fact]
        public async Task GivenACommandLoadedFromAssemblies_WhenRequestingIt_ThenFindTheHandler()
        {
            var services = new ServiceCollection()
                .AddDotnetCQRS()
                .AddCommandHandlersFromAssembly(typeof(ExampleCommand).Assembly)
                .BuildServiceProvider();
            
            var commandDispatcher = services.GetRequiredService<ICommandDispatcher>();
            var result = await commandDispatcher.Run(new ExampleCommand(), CancellationToken.None);
            result.Should().BeSuccess();
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