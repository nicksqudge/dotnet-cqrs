using DotnetCQRS.Commands;
using DotnetCQRS.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetCQRS.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDotnetCQRS(this IServiceCollection services)
        {
            services.AddScoped<ICommandDispatcher>();
            services.AddScoped<IQueryDispatcher>();
            return services;
        }
        
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
    }
}