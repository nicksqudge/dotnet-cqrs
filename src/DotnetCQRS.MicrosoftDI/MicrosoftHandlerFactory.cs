using DotnetCQRS.Commands;
using DotnetCQRS.Queries;
using System;

namespace DotnetCQRS.Extensions.Microsoft.DependencyInjection
{
    internal class MicrosoftHandlerFactory : IHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MicrosoftHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler<TCommand> GetCommandHandler<TCommand>()
            where TCommand : class, ICommand
        {
            Type[] args = { typeof(TCommand) };

            var handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(args);

            return (ICommandHandler<TCommand>)_serviceProvider.GetService(handlerType);
        }

        public IQueryHandler<TQuery, TResult> GetQueryHandler<TQuery, TResult>()
            where TQuery : class, IQuery<TResult>
        {
            Type[] args = { typeof(TQuery), typeof(TResult) };

            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(args);

            return (IQueryHandler<TQuery, TResult>)_serviceProvider.GetService(handlerType);
        }
    }
}
