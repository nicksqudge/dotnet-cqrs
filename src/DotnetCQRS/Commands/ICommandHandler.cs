using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Commands
{
    public interface ICommandHandler<T>
        where T : class, ICommand
    {
        Task<Result> HandleAsync(T command, CancellationToken cancellationToken);
    }
}