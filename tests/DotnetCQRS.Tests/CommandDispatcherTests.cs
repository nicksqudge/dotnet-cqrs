using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DotnetCQRS.Commands;
using DotnetCQRS.Extensions.Autofac.DependencyInjection;
using DotnetCQRS.Extensions.FluentAssertions;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using DotnetCQRS.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DotnetCQRS.Tests
{
    public class CommandDispatcherTests
    {
        [Fact]
        public async Task GivenACommand_WhenRequestingItWithMicrosoftDependencyInjection_ThenFindTheHandler()
        {
            var services = new ServiceCollection()
                .AddDotnetCqrs()
                .AddCommandHandler<CommandTest, CommandTestHandler>()
                .BuildServiceProvider();

            var commandDispatcher = services.GetRequiredService<ICommandDispatcher>();
            var result = await commandDispatcher.Run(new CommandTest(), CancellationToken.None);
            result.Should().BeFailure()
                .And.HaveErrorCode("CommandTestRan");
        }

        [Fact]
        public async Task GivenACommand_WhenRequestingItWithAutofac_ThenFindTheHandler()
        {
            var container = new ContainerBuilder()
                .AddDotnetCqrs()
                .AddCommandHandler<CommandTest, CommandTestHandler>()
                .Build();

            var commandDispatcher = container.Resolve<ICommandDispatcher>();
            var result = await commandDispatcher.Run(new CommandTest(), CancellationToken.None);
            result.Should().BeFailure()
                .And.HaveErrorCode("CommandTestRan");
        }

        [Fact]
        public async Task
            GivenACommandLoadedFromAssemblies_WhenRequestingItWithMicrosoftDependencyInjection_ThenFindTheHandler()
        {
            var services = new ServiceCollection()
                .AddDotnetCqrs()
                .AddCommandHandlersFromAssembly(typeof(ExampleCommand).Assembly)
                .BuildServiceProvider();

            var commandDispatcher = services.GetRequiredService<ICommandDispatcher>();
            var result = await commandDispatcher.Run(new ExampleCommand(), CancellationToken.None);
            result.Should().BeSuccess();
        }

        [Fact]
        public async Task GivenACommandLoadedFromAssemblies_WhenRequestingItWithAutofac_ThenFindTheHandler()
        {
            var container = new ContainerBuilder()
                .AddDotnetCqrs()
                .AddCommandHandlersFromAssembly(typeof(ExampleCommand).Assembly)
                .Build();

            var commandDispatcher = container.Resolve<ICommandDispatcher>();
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