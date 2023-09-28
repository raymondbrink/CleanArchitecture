# NetActive.CleanArchitecture
NetActive.CleanArchitecture is a set of libraries supporting Clean Architecture development in .NET (6+). 

Source code and [examples](https://github.com/raymondbrink/CleanArchitecture/tree/develop/examples) can be found on [GitHub](https://github.com/raymondbrink/CleanArchitecture). The NuGet packages, including debug symbols, can be found on [NuGet](https://www.nuget.org/packages?q=netactive.cleanarchitecture).

## Quick Getting Started Guide

These libraries assume you have SQL Server Express LocalDB installed, but they work with any SQL Server version supported by Entity Framework Core.  
You can verify if you have SQL Server Express LocalDB installed by running this command:
- `SqlLocalDB info`

You should get this response:
```
MSSQLLocalDB
```

If you didn't, then first download and install [SQL Server Express LocalDB](https://msdn.microsoft.com/en-us/library/hh510202.aspx).
Once you have SQL Server Express LocalDB installed, you follow the simple steps below.

You will end-up with a new solution containing a basic console application, but the scaffolded Application, Domain and Persistence projects can also be used in Website or WebApi projects, like all the included example projects do.

1. Open a .NET Core command line interface
1. Install the solution template from NuGet:
    - `dotnet new install NetActive.CleanArchitecture.Template`
1. Create and navigate to an empty folder that will hold the Solution (by default the folder name will be used as the primary namespace):
    - `dotnet new cleanarch-console` (add `-h` for more options)
1. Navigate to the subfolder created, who's name ends with `.Console`
1. Run the console application:
    - `dotnet run`

It might take a few seconds to create the database. Then the output should be:
```
d87d6081-fdbd-42c7-9e8d-4d297410c6aa    some entity
d730df59-bd8c-42d1-8133-6699aa47db42    some other entity
51f2d429-9a53-44b5-9205-f02883ab8bbe    yet another entity

Entity 'some entity' exists: True
```

Alternatively you can also run this command from the Package Manager Console:  
- `PM> Install-Package NetActive.CleanArchitecture.Template`  

Once installed you can choose the template from Visual Studio's `Create a new project` wizard, like any other installed solution or project template.

You can remove the solution template by running this command:  
- `dotnet new uninstall NetActive.CleanArchitecture.Template`

## Example Code

Besides the source code you'll also find many practicle [examples](https://github.com/raymondbrink/CleanArchitecture/tree/develop/examples) on how to use these libraries in Console applications, Web applications or API's.
Here's a quick example (from the `Example.Console.CommandAdd` example project) of what your application code could look like (excluding some global usings):

```csharp
using Example.Application.Manufacturer.Configuration;
using Example.Application.Manufacturer.Commands.AddManufacturer;
using Example.Application.Manufacturer.Commands.AddManufacturer.Models;

// Build a host.
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        // Wire up our clean architecture dependencies.
        services
            .AddPersistenceDependencies<ExampleDbContext, IExampleUnitOfWork, ExampleUnitOfWork>(
                hostContext.Configuration.GetConnectionString("ExampleDbConnection1"),
                useLazyLoadingProxies: false,
                options =>
                {
                    options.RegisterRepository<Manufacturer, Guid>();
                })
            .AddApplicationManufacturerDependencies();
    })
    .Build();

// Create manufacturer model.
var manufacturerName = $"My Manufacturer ({DateTime.Now:yyyyMMddHHmmsssmmm})";
var manufacturerToAdd = new AddManufacturerCommandModel(manufacturerName)
{
    Contact =
    {
        FamilyName = "Brink",
        GivenName = "Raymond" // Optional
    }
};

// Execute add manufacturer command.
var result = await host.Services.GetRequiredService<IAddManufacturerCommand>().ExecuteAsync(manufacturerToAdd);

Console.WriteLine($"Added: {result}: {manufacturerToAdd.ManufacturerName}");
Console.WriteLine();
```

## Why create these libraries?

Inspired by the video series [Clean Architecture: Patterns, Practices and Principles](https://app.pluralsight.com/library/courses/clean-architecture-patterns-practices-principles/table-of-contents) on PluralSight by [Matthew Renze](https://github.com/matthewrenze), 
I started creating these libraries around the idea of Clean Architecture a few years ago.

The open source community has given me so much over the past decade, I decided it was time to give something back.
Since I find them very practicle and I recently ported them to .NET 6 and Entity Framework Core 6,
I felt it was the right time to share these Clean Architecture libraries with the rest of the world.

Focus is on simplifying implementation of and support for these Clean Architecture patterns and practices in new .NET projects. 
These libraries have already been under active development for a few years and applied in real life production applications many times.

Please check them out and feel free to share your thoughts and ideas by contacting me or submitting a pull request.

## Background

Under the hood these libraries try to apply the following principles and patterns:

- [Clean Code](http://cleancoder.com/files/cleanCodeCourse.md)
- [Don't Repeat Yourself](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself)
- SOLID:
  - [Single Responsibility Principle](https://en.wikipedia.org/wiki/Single-responsibility_principle)
  - [Open-Closed Principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle)
  - [Liskov Substitution Principle](https://en.wikipedia.org/wiki/Liskov_substitution_principle)
  - [Interface Segregation Principle](https://en.wikipedia.org/wiki/Interface_segregation_principle)
  - [Dependency Inversion Principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle)
- [Command and Query Responsibility Segregation (CQRS)](https://martinfowler.com/bliki/CQRS.html)
- [Facade Pattern](https://en.wikipedia.org/wiki/Facade_pattern)
- [Factory Method Pattern](https://en.wikipedia.org/wiki/Factory_method_pattern)
- [Repository and Unit of Work Patterns](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

If you're a programmer and you haven't heared of these yet, please check out [Uncle Bob Martin](http://cleancoder.com/products) and [Martin Fowler](https://martinfowler.com/).
You might learn a thing or two ;-)

Check out these projects as they are priceless and essential for these libraries to shine:

- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [Entity Framework Core](https://github.com/dotnet/efcore)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)
- [LINQKit](https://github.com/scottksmith95/LINQKit)

Happy coding!

