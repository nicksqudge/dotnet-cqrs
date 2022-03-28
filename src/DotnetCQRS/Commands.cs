using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS
{
    public interface ICommand
    { }

    public interface ICommandHandler<T>
        where T: class, ICommand
    {
        Task<Result> HandleAsync(T command, CancellationToken cancellationToken);
    }
}
