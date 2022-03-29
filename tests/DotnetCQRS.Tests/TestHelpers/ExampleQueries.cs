using System.Threading;
using System.Threading.Tasks;
using DotnetCQRS.Queries;

namespace DotnetCQRS.Tests.TestHelpers
{
    public class ExampleQuery : IQuery<ExampleQueryResult>
    {
        
    }

    public class ExampleQueryResult
    {
        public string Output { get; set; }
    }

    public class ExampleQueryHandler : IQueryHandler<ExampleQuery, ExampleQueryResult>
    {
        public async Task<Result<ExampleQueryResult>> HandleAsync(ExampleQuery query, CancellationToken cancellationToken)
        {
            return Result.Success(new ExampleQueryResult()
            {
                Output = nameof(ExampleQueryHandler)
            });
        }
    }
}