# CleanArchitecture
This repo contains a set of libraries supporting Clean Architecture development in .NET (6+). 
NuGet packages can be found here: 

Inspired by the video series [Clean Architecture: Patterns, Practices and Principles](https://app.pluralsight.com/library/courses/clean-architecture-patterns-practices-principles/table-of-contents) on PluralSight by [Matthew Renze](https://github.com/matthewrenze).
Since I recently ported the libraries I created around these ideas in the past to .NET 6 and Entity Framework Core 6,
I felt it was time to share them with the rest of the world.

Please feel free to share your thoughts and ideas by contacting me or submitting a pull request.

Focus is on simplifying implementation of and support for these Clean Architecture patterns and practices in new .NET projects. 
These libraries have already been under active development for a few years and applied in real life production applications many times.

Currently this repo lacks unit tests, but I consider them to be very stable and practicle in every day use.

Besides the source code you'll also find practicle examples on how to use these libraries.

Under the hood these libraries also try to apply the following principles and patterns:

- Don't Repeat Yourself
- SOLID:
  - Single Responsibility Principle
  - Open-Closed Principle
  - Liskov Substitution Principle
  - Interface Segregation Principle
  - Dependency Inversion Principle
- Repository and Unit of Work Patterns

Happy coding!