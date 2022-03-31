using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Queries
{
    public interface IQueryDispatcher
    {
        Task<Result<TResult>> RunAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : class, IQuery<TResult>;
    }
}