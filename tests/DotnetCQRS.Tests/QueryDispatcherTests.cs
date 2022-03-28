using System.Threading;
using System.Threading.Tasks;
using DotnetCQRS.Extensions.FluentAssertions;
using DotnetCQRS.Extensions.Microsoft.DependencyInjection;
using DotnetCQRS.Queries;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DotnetCQRS.Tests
{
    public class QueryDispatcherTests
    {
        [Fact]
        public async Task GivenAQuery_WhenRequestingIt_ThenItShouldReturnTheResult()
        {
            var services = new ServiceCollection()
                .AddQueryHandler<QueryTest, QueryTestResult, QueryTestHandler>()
                .BuildServiceProvider();

            var dispatcher = new QueryDispatcher(services);
            var result = await dispatcher.Run<QueryTest, QueryTestResult>(new QueryTest(), CancellationToken.None);
            result.Should().BeSuccess()
                .And.BeEquivalentTo(new QueryTestResult()
                {
                    Output = "QueryRan"
                });
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