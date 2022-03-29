﻿using DotnetCQRS.Commands;
using DotnetCQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

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
