using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Queries
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}