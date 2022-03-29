using System;
using System.Linq;
using System.Reflection;
using DotnetCQRS.Commands;
using DotnetCQRS.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetCQRS.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDotnetCQRS(this IServiceCollection services)
        {
            return services
                .AddDefaultCommandDispatcher()
                .AddDefaultQueryDispatcher()
                .AddHandlerFactory<MicrosoftHandlerFactory>();
        }

        public static IServiceCollection AddDefaultCommandDispatcher(this IServiceCollection services)
            => services.AddScoped<ICommandDispatcher, CommandDispatcher>();

        public static IServiceCollection AddDefaultQueryDispatcher(this IServiceCollection services)
            => services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        public static IServiceCollection AddHandlerFactory<T>(this IServiceCollection services)
            where T : class, IHandlerFactory
            => services.AddTransient<IHandlerFactory, T>();

        public static IServiceCollection AddHandlersFromAssembly<T>(this IServiceCollection services)
            => services.AddHandlersFromAssembly(typeof(T).Assembly);

        public static IServiceCollection AddHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
            => services
                .AddQueryHandlersFromAssembly(assembly)
                .AddCommandHandlersFromAssembly(assembly);

        public static IServiceCollection AddCommandHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
            => services.AddHandlersFromAssembly(assembly, typeof(ICommandHandler<>));

        public static IServiceCollection AddQueryHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
            => services.AddHandlersFromAssembly(assembly, typeof(IQueryHandler<,>));

        public static IServiceCollection AddQueryHandler<TQuery, TResult, THandler>(this IServiceCollection services)
            where TQuery : class, IQuery<TResult>
            where THandler : class, IQueryHandler<TQuery, TResult>
        {
            services.AddTransient<IQueryHandler<TQuery, TResult>, THandler>();
            return services;
        }

        public static IServiceCollection AddCommandHandler<TCommand, THandler>(this IServiceCollection services)
            where TCommand : class, ICommand
            where THandler : class, ICommandHandler<TCommand>
        {
            services.AddTransient<ICommandHandler<TCommand>, THandler>();
            return services;
        }

        private static IServiceCollection AddHandlersFromAssembly(this IServiceCollection services, Assembly assembly, Type expectedHandlerType)
        {
            var types = assembly.GetTypes()
                .Where(t => t.IsAbstract == false)
                .Where(t => t.GetInterfaces().Any(i => i.IsCorrectInterface(expectedHandlerType)))
                .ToList();

            foreach(var type in types)
            {
                var interfaceType = type.GetInterfaces()
                    .Single(t => t.IsCorrectInterface(expectedHandlerType));

                services.AddTransient(interfaceType, type);
            }

            return services;
        }

        private static bool IsCorrectInterface(this Type type, Type expected)
        {
            if (type.IsGenericType == false)
                return false;

            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == expected;
        }
    }
}