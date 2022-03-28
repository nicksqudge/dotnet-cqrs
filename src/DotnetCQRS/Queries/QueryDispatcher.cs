using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _services;

        public QueryDispatcher(IServiceProvider services)
        {
            _services = services;
        }

        public Task<Result<TResult>> Run<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : class, IQuery<TResult>
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            
            var handler = GetHandler<TQuery, TResult>();
            if (handler == null)
            {
                throw new HandlerNotFoundException(typeof(TQuery));
            }

            return handler.HandleAsync(query, cancellationToken);
        }
        
        private IQueryHandler<TQuery, TResult> GetHandler<TQuery, TResult>()
            where TQuery : class, IQuery<TResult>
        {
            Type[] args = {typeof(TQuery), typeof(TResult)};
            
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(args);

            return (IQueryHandler<TQuery, TResult>) _services.GetService(handlerType);
        }
    }
}
