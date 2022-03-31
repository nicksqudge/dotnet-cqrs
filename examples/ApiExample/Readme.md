# Api Example

This is an API example of a simple Shop project that deals with managing and viewing products and also allowing users to
buy things. It is all done as a REST API to keep it simple.

## Drawbacks

- **No Auth**: There is no authorisation setup for this project because it isn't needed for this example. For projects
  with authorisation I would handle that in the Controllers and not be something that is dealt in the Handlers.
- **Database**: I am using Sqlite for this implementation and applying that straight into the handlers, in normal
  applications it is suggested that you abstract that logic to Repositories along with the entities. CQRS is meant to be
  for an Application layer that calls methods and elements within your Domain layer, lookup Domain Driven Design for a
  way to do that.

## Other Notes

- **Validation**: I am using FluentValidation for my validators.

## Need other examples?

I am aware that there are instances with tutorials and examples that are either too simplistic or won't at all be
similar to how it would work within the industry. I have tried to come up with an example that maintains some realism (
except the above Drawbacks) but if there are some ways you want to see things done please create an Issue on Github and
I will see what I can do. 