# .Net CQRS

A simple CQRS library for .net

## Installing

You should install .Net CQRS with Nuget

`Install-Package DotnetCQRS`

or using the .Net core command line

`dotnet add package DotnetCQRS`

## Usage

To define a Command or Query, create a class and inherit from either ICommand or IQuery. For Queries, you will need
to define a return result.

```csharp

public class CreateUserCommand : ICommand
{
  public string Name { get; set; }
}

public class FetchUsersQuery : IQuery<FetchUsersResult>
{

}

public class FetchUsersResult
{
  public IReadOnlyList<Users> Users { get; set; }

  public class Users
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
```

Then define the handlers:

```csharp
public class CreateUserHandler : ICommandHandler<CreateUserCommand>
{
  public async Task<Result> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken)
  {
    // Code here for validating and creating a user
    
    return Result.Success();
  }
}

public class FetchUsersHandler : IQueryHandler<FetchUsersQuery, FetchUsersResult>
{
  public async Task<Result> HandleAsync(CreateUserCommand FetchUsersQuery, CancellationToken cancellationToken)
  {
    var usersList = // Some code to pull users from some kind of data store
    return Result.Success(usersList);
  }
}
```

You will then need to register the Query and Command Handlers with some kind of dependency injection so that you can
call the dispatchers. The project includes some DI setup for both Microsoft.Extensions.DependencyInjection and
Autofac. (documentation coming soon).

Once registered you will need to instantiate the IQueryDispatcher or ICommandDispatcher to run the above commands and
queries.

```csharp
// For query
var dispatcher = serviceProvider.GetService<IQueryDispatcher>();
var result = dispatcher.Run(new FetchUsersQuery(), CancellationTokenSource.Token);

if (result.IsFailure)
{
  // Handle an instance where the result was a failure
}
var users = result.Value; // This is where the result of the query lives

// For command
var dispatcher = serviceProvider.GetService<IQueryDispatcher>();
var result = dispatcher.Run(new CreateUserCommand()
{
  Name = "Foobar",
}, CancellationTokenSource.Token);

if (result.IsSuccess)
{
  // Good times!
}
else
{
  // Handle an instance where the result was a failure
}

```

## FAQ

### What is CQRS?

CQRS (Command and Query Responsibility Segregation) is a pattern for structuring applications. For more
information on this pattern
check [the Microsoft documentation](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)

Unlike the above implementation, this one is used for doing one Command or Query Handler per file rather than being able
to do many in a single file.

For more information check out the [basics](docs/Basics.md) or the [ApiExample](examples/ApiExample/Readme.md)

### What About Dependency Injection?

Check the /src folder to find two Dependency Injection implementations for the Microsoft.Extensions.DependencyInjection
and Autofac libraries. If you want other implementations let me know through the Issues tab in Github.

## Version History

### Version 1.2
 - Added in EmptySuccess as a possible Result static method
 - Added HasValue boolean to Result<T>
 - Made properties of the Result and Result<T> private set

### Version 1.1
 - Made naming of methods more consistent