# NetActive.CleanArchitecture
NetActive.CleanArchitecture is a set of libraries supporting Clean Architecture development in .NET (6+). 

The NuGet packages can be found here: 

Inspired by the video series [Clean Architecture: Patterns, Practices and Principles](https://app.pluralsight.com/library/courses/clean-architecture-patterns-practices-principles/table-of-contents) on PluralSight by [Matthew Renze](https://github.com/matthewrenze), 
I started creating these libraries around the idea of Clean Architecture a few years ago.

The open source community has given me so much over the past decade, I decided it was time to give something back.
Since I find them very practicle and I recently ported them to .NET 6 and Entity Framework Core 6,
I felt it was the right time to share these Clean Architecture libraries with the rest of the world.

Focus is on simplifying implementation of and support for these Clean Architecture patterns and practices in new .NET projects. 
These libraries have already been under active development for a few years and applied in real life production applications many times.

Please check them out and feel free to share your thoughts and ideas by contacting me or submitting a pull request.

Besides the source code you'll also find practicle [examples](examples) on how to use these libraries in Console applications, Web applications or API's.
Here's a quick example (from the `Example.Console.CommandAdd` example project) of what your application code could look like:

```csharp
using Autofac;

using Example.Application.Supplier.Commands.AddSupplier;
using Example.Application.Supplier.Commands.AddSupplier.Models;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    // Create supplier model.
    var supplierName = $"My Supplier ({DateTime.Now:yyyyMMddHHmmsssmmm})";
    var supplierToAdd = new AddSupplierCommandModel
        {
            SupplierName = supplierName,
            Contact =
                {
                    FamilyName = "Raymond",
                    GivenName = "Brink"
                }
        };

    // Resolve and execute add supplier command.
    var newSupplierId = await scope.Resolve<IAddSupplierCommand>().ExecuteAsync(supplierToAdd);

    Console.WriteLine($"Added: {newSupplierId}: {supplierToAdd.SupplierName}");
    Console.WriteLine();
}
```

Currently this repo lacks unit tests, but I consider them to be very stable and practicle in every day use.

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
- [Repository and Unit of Work Patterns](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

If you're a programmer and you haven't heared of these yet, please check out [Uncle Bob Martin](http://cleancoder.com/products) and [Martin Fowler](https://martinfowler.com/).
You might learn a thing or two ;-)

Happy coding!

