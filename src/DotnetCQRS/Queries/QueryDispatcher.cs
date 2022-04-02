using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IHandlerFactory _handlerFactory;

        public QueryDispatcher(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public Task<Result<TResult>> RunAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : class, IQuery<TResult>
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var handler = _handlerFactory.GetQueryHandler<TQuery, TResult>();
            if (handler == null) throw new HandlerNotFoundException(typeof(TQuery));

            return handler.HandleAsync(query, cancellationToken);
        }
    }
}