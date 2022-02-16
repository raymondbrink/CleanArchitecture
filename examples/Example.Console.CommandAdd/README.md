# Example.Console.CommandAdd

This example demonstrates how to add a new entity to the database. 
It's as simple as creating the required model of a supplier, 
resolving the right command from the DI container and calling the `ExecuteAsync()` method.

Let's have a look at the implementation:

```csharp
public async Task<Guid> ExecuteAsync(AddSupplierCommandModel model)
{
    if (await _repositories.SupplierExistsAsync(model.SupplierName))
    {
        throw new InvalidOperationException($"Supplier with name '{model.SupplierName}' already exists.");
    }

    // Create supplier instance.
    var supplier = _factory.Create(model.SupplierName, model.Contact?.FamilyName, model.Contact?.GivenName);

    // Assert supplier is valid.
    await _validator.AssertIsValidAsync(supplier);

    // Add supplier to repo.
    _repositories.AddSupplier(supplier);

    // Commit changes.
    await _unitOfWork.SaveChangesAsync();

    return supplier.Id;
}
```
As you can see, we first check if a supplier with the given name exists. In that case an exception is thrown.
Then we use a factory to create a new instance of a supplier entity.
Next we verify the supplier entity is valid using a validator. If not, this will generate an exception.
Then we add the new supplier to the repository and commit our changes.
Finally we return the ID of the newly added supplier. We'll have a look at each of the components used to facilitate succesfull execution of this command.

## Factory
Using a factory to instantiate classes enhances flexibility of our code. 
The `SupplierFactory` will new up an instance of the `Supplier` domain entity class for us:
```csharp
namespace Example.Application.Supplier.Commands.AddSupplier.Factory;

using Domain.Entities;

internal class SupplierFactory : ISupplierFactory
{
    public Supplier Create(string name, string contactFamilyName = null, string contractGivenName = null)
    {
        return new Supplier
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
This opens up the ability to create overrides of the factory method and maybe instantiate a different `Supplier` subclass in the future.

## Validator
The `SupplierValidator` assures that the Supplier entity we created is valid for the database schema.
In this case `Supplier.Name` and `Supplier.Contact.FamilyName` are required, which we enforce in our validator:
```csharp
namespace Example.Domain.Validation;

using Entities;

using FluentValidation;

using NetActive.CleanArchitecture.Domain.Validation;

public class SupplierValidator : BaseFluentEntityValidator<Supplier>
{
    public override void Rules()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Supplier.Name)));
        RuleFor(e => e.Contact)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Supplier.Contact)));
        RuleFor(e => e.Contact.FamilyName)
            .NotEmpty()
            .WithMessage(string.Format(ValidationResources.Required, nameof(Supplier.Contact.FamilyName)));
    }
}
```
For validation we use our generic `BaseFluentEntityValidator<TEntity>` class, but you can easily create your own implementation of `NetActive.CleanArchitecture.Domain.Interfaces.IEntityValidator<TEntity>` with your own validation logic based on the validation library or framework you prefer.

## Repository Facade
A respository facade wraps and therefor hides complex repository logic from the client application. 
It provides a clean programming interface, exposing only those methods needed by this command.
In this case it may seem like overkill (agreed again), but for more complex commands, for instance interacting with multiple repo's, this pattern becomes very useful.

```csharp
namespace Example.Application.Supplier.Commands.AddSupplier.Repository;

using Domain.Entities;

using NetActive.CleanArchitecture.Application.Interfaces;
using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Autofac;

internal class AddSupplierRepositoryFacade : IAddSupplierRepositoryFacade
{
    private readonly IRepository<Supplier, Guid> _repo;

    public AddSupplierRepositoryFacade(IRepository<Supplier, Guid> repo)
    {
        _repo = repo;
    }

    /// <inheritdoc />
    public Task<bool> SupplierExistsAsync(string name) => _repo.ExistsAsync(s => s.Name.Equals(name));

    /// <inheritdoc />
    public void AddSupplier(Supplier supplier) => _repo.Add(supplier);
}
```
We only need two interactions with the repo here: 
- Check if a supplier with the given name already exists
- Add the new supplier to the repo

Both are fairly simple, as these exact methods are provided by the `IRepository<TEntity, TKey>` interface.

## Module
To ty all this together we need some registrations in our DI container. 
These are the relevant supplier Module registrations:
```csharp
// IAddSupplierCommand
builder.RegisterService<IAddSupplierCommand, AddSupplierCommand>(RegisterSingleInstance);
builder.RegisterService<IAddSupplierRepositoryFacade, AddSupplierRepositoryFacade>(RegisterSingleInstance);
builder.RegisterService<ISupplierFactory, SupplierFactory>(RegisterSingleInstance);
builder.RegisterService<IEntityValidator<Supplier>, SupplierValidator>(RegisterSingleInstance);
```
The registrations for `IAddSupplierCommand`, `IAddSupplierRepositoryFacade` and `ISupplierFactory` are pretty straight forward.
Finally there's a registration for the `SupplierValidator`, which is an implementation of the generic `IEntityValidator<Supplier>` interface.

Happy coding!

