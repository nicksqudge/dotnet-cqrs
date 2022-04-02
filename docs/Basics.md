# CQRS Basics

Here are some basic notes for CQRS. There are loads of tutorials around to explain these concepts in more detail. I recommend (this one)[https://www.pluralsight.com/courses/cqrs-in-practice].

Some of the details in these tutorials may be different from the ones that I have listed below. However I find that some implementations will differ depending on the project. This is in no way meant to be a gospel guide or the rules of CQRS but these examples cover what I have found works best and what the .Net CQRS library is designed for.

## Commands

Commands are used for handling a user action that typically involves changing data in a system. They only return a Success
or Fail result (with an error code for lookup).

## Queries

Queries are a request for data, that are typically used within Databases. However they are not specifically designed for this purpose. Queries do not make changes to data, they just return it.

## Handlers

Handlers are where you put the business logic to execute commands or queries. For example, if you had a command to delete a user, you would create a DeleteUserCommand which has a property for the user ID. From there, the Handler would action the command and make changes to the code.

## Don't Cross the Streams

Do not call commands or queries in other commands or query handlers. If you have shared logic, this should be in either a Service or a Domain object/entity.

Commands and queries are specific implementations of a user's actions and rely on specific implementation details. If those details change, it could be trickier to maintain and update other commands or queries. By splitting the shared logic into another class, you can write unit tests against that class, making it easier to test and maintain.

## Use Specific Names

Name commands and queries as a user would think of them - not as a developer would. So, rather than "AddUserCommand" or "UpdateUserCommand" try "SaveUserCommand". By using ubiquitous language, you avoid confusion by ensuring that your project owners, developers and end users are using the same language.

## Validation

Sometimes validation of commands or queries takes place via a Decorator or at a line above the code that calls the Handler. While this isn't wrong, I  prefer a Handler to contain all of the logic to run that function so I choose to put that validation inside the Handler. However, there are scenarios where this validation may be better placed elsewhere.

I suggest using FluentValidation to easy validation for Queries and Commands.

## Exceptions

The Result class is used to return expected successes or failures back to the user. If something unexpected happens (for example, a server goes offline mid call) that problem should go to an exception which should be handled by the code that is calling the Handler, such as a controller.

## Events

Some CQRS patterns also include an event pattern for doing Event-Driven Architecture (EDA). .Net CQRS doesn't have this functionality currently because EDA will depend on your specific implementation, so I have chosen to omit it. This may change in the future, but at the moment there isn't a need for one.
