# Example.Console.CommandAdd

This example demonstrates how to add a new entity to the database. 
It's as simple as have DI create an instance of the `AddManufacturerCommand` and then executing it. 
For this we get the `IAddManufacturerCommand` interface from the DI container and pass the manufacturer instance to the `ExecuteAsync()` method:

```csharp
// Execute add manufacturer command.
var result = await host.Services
    .GetRequiredService<IAddManufacturerCommand>()
    .ExecuteAsync(manufacturerToAdd);
```

Let's have a look at the implementation of the `AddManufacturerCommand` we registered with DI to handle this command:

```csharp
public async Task<Guid> ExecuteAsync(AddManufacturerCommandModel model, CancellationToken? cancellationToken = null)
{
    if (await _repositories.ManufacturerExistsAsync(model.ManufacturerName))
    {
        throw new InvalidOperationException($"Manufacturer with name '{model.ManufacturerName}' already exists.");
    }

    // Create manufacturer instance.
    var manufacturer = _factory.Create(model.ManufacturerName, model.Contact?.FamilyName, model.Contact?.GivenName);

    // Assert manufacturer is valid.
    await _validator.AssertIsValidAsync(manufacturer);

    // Add manufacturer to repo.
    _repositories.AddManufacturer(manufacturer);

    // Commit changes.            
    await _unitOfWork.SaveChangesAsync(cancellationToken ?? CancellationToken.None);

    return manufacturer.Id;
}
```
As you can see, we first check if a manufacturer with the given name exists. In that case an exception is thrown.
Then we use a factory to create a new instance of a manufacturer entity.
Next we verify the manufacturer entity is valid using a validator. If not, this will generate an exception.
Then we add the new manufacturer to the repository and commit our changes.
After that we publish a notification to notify other processes about the new manufacturer we added.
Finally we return the ID of the newly added manufacturer. 

We'll have a look at each of the components used to facilitate succesfull execution of this command.

## Factory
Using a factory to instantiate classes enhances flexibility of our code. 
The `ManufacturerFactory` will new up an instance of the `Manufacturer` domain entity class for us:
```csharp
namespace Example.Application.Manufacturer.Commands.AddManufacturer.Factory;

using Domain.Entities;

internal class ManufacturerFactory : IManufacturerFactory
{
    public Manufacturer Create(string name, string contactFamilyName = null, string contractGivenName = null)
    {
        return new Manufacturer
            {
                Name = name,
                Contact =
                    {
                        FamilyName = contactFamilyName,
                        GivenName = contractGivenName
                    }
            };
    }
}
```
Here using a factory may seem like overkill (agreed), but it's good practice in general to not instantiate (new up) classes through their constructors.
This opens up the ability to create overrides of the factory method and maybe instantiate a different `Manufacturer` subclass in the future.

## Validator
The `ManufacturerValidator` assures that the Manufacturer entity we created is valid for the database schema.
In this case `Manufacturer.Name` and `Manufacturer.Contact.FamilyName` are required, which we enforce in our validator:
```csharp
namespace Example.Domain.Validation;

using Entities;

using FluentValidation;

using NetActive.CleanArchitecture.Domain.Validation;

public class ManufacturerValidator : BaseFluentEntityValidator<Manufacturer>
{
    public override void Rules()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Manufacturer.Name)));
        RuleFor(e => e.Contact)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Manufacturer.Contact)));
        RuleFor(e => e.Contact.FamilyName)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Manufacturer.Contact.FamilyName)));
    }
}
```
For validation we use our generic `BaseFluentEntityValidator<TEntity>` class, but you can easily create your own implementation of `NetActive.CleanArchitecture.Domain.Interfaces.IEntityValidator<TEntity>` with your own validation logic based on the validation library or framework you prefer.

## Repository Facade
A respository facade wraps and therefor hides complex repository logic from the client application. 
It provides a clean programming interface, exposing only those methods needed by this command.
In this case it may seem like overkill (agreed again), but for more complex commands, for instance interacting with multiple repo's, this pattern becomes very useful.

```csharp
namespace Example.Application.Manufacturer.Commands.AddManufacturer.Repository;

using Domain.Entities;

using NetActive.CleanArchitecture.Application.Interfaces;
using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Autofac;

internal class AddManufacturerRepositoryFacade : IAddManufacturerRepositoryFacade
{
    private readonly IRepository<Manufacturer, Guid> _repo;

    public AddManufacturerRepositoryFacade(IRepository<Manufacturer, Guid> repo)
    {
        _repo = repo;
    }

    /// <inheritdoc />
    public Task<bool> ManufacturerExistsAsync(string name) => _repo.ExistsAsync(s => s.Name.Equals(name));

    /// <inheritdoc />
    public void AddManufacturer(Manufacturer manufacturer) => _repo.Add(manufacturer);
}
```
We only need two interactions with the repo here: 
- Check if a manufacturer with the given name already exists
- Add the new manufacturer to the repo

Both are fairly simple, as these exact methods are provided by the `IRepository<TEntity, TKey>` interface.

## Dependencies
To ty all this together we need some registrations in our DI container. 
Registration of the command is taken care of in Program.cs:

```csharp
// Build a host.
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        // Wire up our clean architecture dependencies.
        services
            .AddPersistenceDependencies<ExampleDbContext, IExampleUnitOfWork, ExampleUnitOfWork>(
                hostContext.Configuration.GetConnectionString("ExampleDbConnection1"),
                options =>
                {
                    options.RegisterEfRepository<Manufacturer, Guid>();
                })
            .AddApplicationManufacturerDependencies();
    })
    .Build();
```

Besides the regular persistence layer dependencies, we also need to register the application layer dependencies.
This is done using an extension method on the `IServiceCollection` interface`, that we created specifically for this purpose: `AddApplicationManufacturerDependencies`
These are the relevant registrations from this extension method:
```csharp
// AddManufacturerCommand dependencies.
services.AddService<IAddManufacturerCommand, AddManufacturerCommand>(lifetime);
services.AddService<IAddManufacturerRepositoryFacade, AddManufacturerRepositoryFacade>(lifetime);
services.AddService<IManufacturerFactory, ManufacturerFactory>(lifetime);
services.AddService<IEntityValidator<Manufacturer>, ManufacturerValidator>(lifetime);
```

The registrations for `IAddManufacturerRepositoryFacade` and `IManufacturerFactory` are pretty straight forward.
Finally there's a registration for the `ManufacturerValidator`, which is an implementation of the generic `IEntityValidator<Manufacturer>` interface.

Happy coding!

