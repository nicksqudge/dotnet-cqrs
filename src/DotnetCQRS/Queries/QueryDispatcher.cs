using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        Task<Result<TResult>> IQueryDispatcher.Run<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
