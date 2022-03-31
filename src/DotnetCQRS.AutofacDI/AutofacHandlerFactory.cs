using System;
using Autofac;
using DotnetCQRS.Commands;
using DotnetCQRS.Queries;

namespace DotnetCQRS.Extensions.Autofac.DependencyInjection
{
    internal class AutofacHandlerFactory : IHandlerFactory
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacHandlerFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public ICommandHandler<TCommand> GetCommandHandler<TCommand>()
            where TCommand : class, ICommand
        {
            Type[] args = {typeof(TCommand)};

            var handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(args);

            return (ICommandHandler<TCommand>) _lifetimeScope.Resolve(handlerType);
        }

        public IQueryHandler<TQuery, TResult> GetQueryHandler<TQuery, TResult>()
            where TQuery : class, IQuery<TResult>
        {
            Type[] args = {typeof(TQuery), typeof(TResult)};

            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(args);

            return (IQueryHandler<TQuery, TResult>) _lifetimeScope.Resolve(handlerType);
        }
    }
}