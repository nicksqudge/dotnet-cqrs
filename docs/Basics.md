# CQRS Basics

Here are some basic notes for CQRS, there are loads of tutorials around to explain these concepts in more detail. I
would recommend (this one)[https://www.pluralsight.com/courses/cqrs-in-practice].

Some of their details may be different from the ones that I have listed below but I find that some implementations will
differ depending on the project. This is in no way meant to be a gospel guide or the rules of CQRS but these are what I
have found works best and what the .Net CQRS library is designed for.

## Commands

Commands are used for handling a user action, typically involving changing data in a system. They only return a Success
or Fail result (with an error code for lookup).

## Queries

Queries are a request for data, whilst not specifically for a Database but typically they would be. They do not make
changes to data just return it.

## Handlers

A command or query contains the details to enact the action requested of it. The Handler is where you put the business
logic to execute that action. So for example if you had an action to delete a user, you would create a DeleteUserCommand
with has a property for the user id and then the handler would take that command in and make changes to the code.

## Don't cross the streams

Do not call commands or queries in other commands or query handlers, if you have shared logic this should be in either a
Service or a Domain. Commands and Queries are specific implementations of a users action and rely on specific
implementation details, if those details change then any other command or query could be made more tricky to maintain
and update. By splitting the shared logic into another class you can write unit tests against that class for ease of
testing and maintaining the logic.

## Be Specific in names

Be mindful when naming commands and queries to not name them as a developer would, name them as a user would think of
them. So rather than "AddUserCommand" or "UpdateUserCommand" try "SaveUserCommand". This helps with the concept of
Ubiquitous language to make sure that your project owners, developers and end users are using the same language to avoid
confusion.

## Validation

Sometimes validation of the Commands or Queries takes place via a Decorator or at a line above whatever calls the
Handler. This is fine but in my opinion I would prefer a Handler to contain all of the logic to run that function, so I
choose to put that Validation inside the Handler but I could see arguments for doing it elsewhere.

## Exceptions

The Result class is used to return expected successes or failures back to the user, if something unexpected happens (
like a server goes offline mid call for example) that problem should go to an exception which should be handled by
whatever is calling the Handler (like a controller). This I think goes a little bit of a way to have a system that
breaks as you would expect it but also be able to know the difference between an expected and unexpected errors.

## Events

Some CQRS patterns also include an Event pattern for doing Event Driven functionality. .Net CQRS doesn't have this
concept currently because that can be a whole implementation specific thing of its own, so I have chosen to omit it. I
might change this in the future but at the moment there isn't a need for one.