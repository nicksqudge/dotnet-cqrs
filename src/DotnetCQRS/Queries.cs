using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS
{
    public interface IQuery<TResult>
    { }

    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
