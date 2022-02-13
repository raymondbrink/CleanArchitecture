# NetActive.CleanArchitecture - Examples
This folder contains some examples showcasing the usage of the NetActive.CleanArchitecture libraries.
The NetActive.CleanArchitecture libraries heavily rely on Dependency Injection (DI) based on Autofac.
With some tweaking other dependency inversion frameworks can be used as well.

## Clear Architecture layer projects
A few project here are examples of Clean Architecture layer projects.
You can think of them as examples that follow some convention based guidelines.
You need one project for each layer of the Clean Architecture model:

### Example.Application

This project contains all query and command logic in a folder per entity.
Under each entity folder you'll find a folder named:
- /Autofac: Contains a module definition to be registered in DI;
- /Commands: Contains a folder per command with some specific subfolders;
- /Queries: Contains a folder per query with some specific subfolders.

For Dependency Inversion this project also defines the interface for the unit of work it needs from the Persistence layer (see: /Interfaces/Persistence).

### Example.Domain

This project should contain (generated) entities and validation rules.
Entities are generated from the datamodel using Entity Developer (see: Example.Persistence).
Besides the entities this project contains the validation rules to be used by commands for specific entities.
If validation rules for an entity are not met, the validators in this project will throw an exception.

### Example.Infrastructure

This project contains interfaces and implementations for all external resources and systems your project communicates with.
Think of external mail services, payment services, caching or calculation logic.
This project is empty, because there's no connection to any such service required for these examples.

### Example.Persistence

This project deals with database design and database access. 
It contains the database context and unit of work classes.
In this example we use Entity Developer by Devart for a model-first approach.
[Entity Developer](https://www.devart.com/entitydeveloper/) is a database design tool, that can also generate the database context and entity classes for you.
To top it of it can also generate SQL scripts for initial database generation, or for updates by comparing the current model with the current database schema.
It's free to use for models with up to 10 entities, so give it a try!
Other approaches for creating database models are valid too, as long as they result in a [DbContext](https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0) usable with [Entity Framework Core 6](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/).

### Example.Shared

This project is just for my convinience. To not repreat ourselves I've moved configuring the DI container to a separate project.
Basically each example project mentioned below uses the same Dependency Injection container from this project, configured with the same registered components.
This is where the DI "magic" happens. It ties all the layers together.

## Examples

Please find below an overview of all examples provided.
If you'd like an example to be added for a specific scenario not listed here, please let me know and I'll be more than happy to add it.

### 1. Example.Console.CommandAdd

This example showcases how to add a new entity to the database. 


Happy coding!

