using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCQRS.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private IServiceProvider services;

        public CommandDispatcher(IServiceProvider services)
        {
            this.services = services;
        }

        Task<Result> ICommandDispatcher.Run<T>(T command, CancellationToken cancellationToken)
        {
            var handler = services.GetService();
        }
    }
}
