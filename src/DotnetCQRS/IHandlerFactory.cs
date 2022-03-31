using DotnetCQRS.Commands;
using DotnetCQRS.Queries;

namespace DotnetCQRS
{
    public interface IHandlerFactory
    {
        ICommandHandler<TCommand> GetCommandHandler<TCommand>()
            where TCommand : class, ICommand;

        IQueryHandler<TQuery, TResult> GetQueryHandler<TQuery, TResult>()
            where TQuery : class, IQuery<TResult>;
    }
}