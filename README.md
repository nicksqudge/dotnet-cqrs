# .Net CQRS

A simple CQRS library for .net

## Installing

You should install .Net CQRS with Nuget

`Install-Package DotnetCQRS`

or using the .Net core command line

`dotnet add package DotnetCQRS`

## FAQ

### What is CQRS?

CQRS is known as Command and Query Responsibility Segregation, it is a pattern for structuring applications. For more information on the pattern check [the Microsoft documentation](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)

Unlike the above implementation, this one is used for doing One Command or Query Handler per file rather than being able to do many in a single file.

### What about Dependency Injection

Check the /src folder to find two Dependency Injection implementations for the Microsoft.Extensions.DependencyInjection and Autofac libraries. If you want other implementations let me know through the Issues tab in Github.
