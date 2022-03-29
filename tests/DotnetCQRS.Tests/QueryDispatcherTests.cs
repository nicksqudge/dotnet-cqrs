using System.Threading;
using System.Threading.Tasks;
using DotnetCQRS.Extensions.FluentAssertions;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using DotnetCQRS.Queries;
using DotnetCQRS.Tests.TestHelpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DotnetCQRS.Tests
{
    public class QueryDispatcherTests
    {
        [Fact]
        public async Task GivenAQuery_WhenRequestingItWithMicrosoftDependencyInjection_ThenItShouldReturnTheResult()
        {
            var services = new ServiceCollection()
                .AddDotnetCQRS()
                .AddQueryHandler<QueryTest, QueryTestResult, QueryTestHandler>()
                .BuildServiceProvider();

            var dispatcher = services.GetRequiredService<IQueryDispatcher>();
            var result = await dispatcher.Run<QueryTest, QueryTestResult>(new QueryTest(), CancellationToken.None);
            result.Should().BeSuccess()
                .And.BeEquivalentTo(new QueryTestResult()
                {
                    Output = "QueryRan"
                });
        }

        [Fact]
        public async Task GivenAQueryLoadedFromAssemblies_WhenRequestingItWithMicrosoftDependencyInjection_ThenFindTheHandler()
        {
            var services = new ServiceCollection()
                .AddDotnetCQRS()
                .AddQueryHandlersFromAssembly(typeof(ExampleQuery).Assembly)
                .BuildServiceProvider();

            var queryDispatcher = services.GetRequiredService<IQueryDispatcher>();
            var result = await queryDispatcher.Run<ExampleQuery,ExampleQueryResult>(new ExampleQuery(), CancellationToken.None);
            result.Should().BeSuccess();
        }

        public class QueryTest : IQuery<QueryTestResult>
        {
            
        }

        public class QueryTestResult
        {
            public string Output { get; set; }
        }

        public class QueryTestHandler : IQueryHandler<QueryTest, QueryTestResult>
        {
            public async Task<Result<QueryTestResult>> HandleAsync(QueryTest query, CancellationToken cancellationToken)
            { 
                return Result.Success<QueryTestResult>(new QueryTestResult()
                {
                    Output = "QueryRan"
                });
            }
        }
    }
}