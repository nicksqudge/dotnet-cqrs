using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _services;

        public CommandDispatcher(IServiceProvider services)
        {
            _services = services;
        }

        public Task<Result> Run<T>(T command, CancellationToken cancellationToken)
            where T : class, ICommand
        {
            var handler = GetHandler<T>();
            if (handler == null)
            {
                throw new HandlerNotFoundException(typeof(T));
            }

            return handler.HandleAsync(command, cancellationToken);
        }

        private ICommandHandler<T> GetHandler<T>()
            where T : class, ICommand
        {
            Type[] args = {typeof(T)};
            
            var handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(args);

            return (ICommandHandler<T>) _services.GetService(handlerType);
        }
    }
}
