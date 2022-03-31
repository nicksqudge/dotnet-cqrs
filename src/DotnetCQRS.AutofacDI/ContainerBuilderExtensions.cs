using System;
using System.Reflection;
using Autofac;
using DotnetCQRS.Commands;
using DotnetCQRS.Queries;

namespace DotnetCQRS.Extensions.Autofac.DependencyInjection
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder AddDotnetCqrs(this ContainerBuilder container)
        {
            return container
                .AddDefaultCommandDispatcher()
                .AddDefaultQueryDispatcher()
                .AddHandlerFactory<AutofacHandlerFactory>();
        }

        public static ContainerBuilder AddDefaultCommandDispatcher(this ContainerBuilder container)
        {
            container.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

            return container;
        }

        public static ContainerBuilder AddDefaultQueryDispatcher(this ContainerBuilder container)
        {
            container.RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>()
                .InstancePerLifetimeScope();

            return container;
        }

        public static ContainerBuilder AddHandlerFactory<T>(this ContainerBuilder container)
            where T : class, IHandlerFactory
        {
            container.RegisterType<T>()
                .As<IHandlerFactory>()
                .InstancePerLifetimeScope();

            return container;
        }

        public static ContainerBuilder AddHandlersFromAssembly<T>(this ContainerBuilder container)
        {
            return container.AddHandlersFromAssembly(typeof(T).Assembly);
        }

        public static ContainerBuilder AddHandlersFromAssembly(this ContainerBuilder container, Assembly assembly)
        {
            return container
                .AddQueryHandlersFromAssembly(assembly)
                .AddCommandHandlersFromAssembly(assembly);
        }

        public static ContainerBuilder AddCommandHandlersFromAssembly(this ContainerBuilder container,
            Assembly assembly)
        {
            return container.AddHandlersFromAssembly(assembly, typeof(ICommandHandler<>));
        }

        public static ContainerBuilder AddQueryHandlersFromAssembly(this ContainerBuilder container, Assembly assembly)
        {
            return container.AddHandlersFromAssembly(assembly, typeof(IQueryHandler<,>));
        }

        public static ContainerBuilder AddQueryHandler<TQuery, TResult, THandler>(this ContainerBuilder container)
            where TQuery : class, IQuery<TResult>
            where THandler : class, IQueryHandler<TQuery, TResult>
        {
            container.RegisterType<THandler>()
                .As<IQueryHandler<TQuery, TResult>>()
                .InstancePerLifetimeScope();

            return container;
        }

        public static ContainerBuilder AddCommandHandler<TCommand, THandler>(this ContainerBuilder container)
            where TCommand : class, ICommand
            where THandler : class, ICommandHandler<TCommand>
        {
            container.RegisterType<THandler>()
                .As<ICommandHandler<TCommand>>()
                .InstancePerLifetimeScope();

            return container;
        }

        private static ContainerBuilder AddHandlersFromAssembly(this ContainerBuilder container, Assembly assembly,
            Type expectedHandlerType)
        {
            container
                .RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(expectedHandlerType);

            return container;
        }
    }
}