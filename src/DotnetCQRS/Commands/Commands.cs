using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Commands
{
    /// <summary>
    ///     Used to locate a command handler and run it
    /// </summary>
    public interface ICommandDispatcher
    {
        Task<Result> Run<T>(T command, CancellationToken cancellationToken)
            where T : class, ICommand;
    }
}