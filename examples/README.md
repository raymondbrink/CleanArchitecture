# NetActive.CleanArchitecture - Examples
This folder contains some examples showcasing the usage of the NetActive.CleanArchitecture libraries.
The NetActive.CleanArchitecture libraries heavily rely on Dependency Injection based on Autofac.
With some tweaking other dependency inversion frameworks can be used as well.

## Clear Architecture layer projects
A few project here are examples of Clean Architecture layer projects.
You can think of them as examples that follow some convention based guidelines.
You need one project for each layer of the Clean Architecture model:

### Example.Application

This project contains all query and command logic in a folder per entity.

### Example.Domain

This project contains the (generated) entities and validation rules.

### Example.Infrastructure

This project contains interfaces and implementations for all external resources and systems your project communicates with.
Think of external mail services, payment services, caching or calculation logic.
This project is empty here, because there's no connection to any such service required for these examples.

### Example.Persistence

This project deals with database design and access. 
It holds the database context and unit of work classes.
In this example we use Entity Developer by Devart for a model-first approach.
[Entity Developer](https://www.devart.com/entitydeveloper/) is a database design tool, that can also generate the database context and entity classes for you.
To top it of it can also generate SQL scripts for database generation or updates, by comparing the current model with an actual database.
It's free for models with up to 10 entities, so give it a try!
Other approaches for creating database models are valid too, as long as they result in a [DbContext](https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0) usable with [Entity Framework Core 6](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/).

## Examples

Note that each example project mentioned below uses the same Dependency Injection container with the same registered components (see project Example.Shared).
This is mostly where the "magic" happens. 

Please find an below overview of all examples provided.
If you'd like an example to be added for a specific scenario not listed here, please let me know and I'll be more than happy to add it.

### 1. Example.Console.CommandAdd

This example showcases how to add a new entity to the database. 


Happy coding!

