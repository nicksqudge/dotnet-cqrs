using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IHandlerFactory _handlerFactory;

        public CommandDispatcher(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public Task<Result> Run<T>(T command, CancellationToken cancellationToken)
            where T : class, ICommand
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var handler = _handlerFactory.GetCommandHandler<T>();
            if (handler == null) throw new HandlerNotFoundException(typeof(T));

            return handler.HandleAsync(command, cancellationToken);
        }
    }
}