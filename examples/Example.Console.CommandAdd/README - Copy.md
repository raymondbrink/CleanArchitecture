# Example.Console.CommandAdd

This example shows how to add a new entity to the database. 
It's as simple as creating the required model of in this case a supplier, 
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
As you can see, we first check if a supplier with the given name already exists. In that case an exception is thrown.
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

## Validator
The `SupplierValidator` assures that the Supplier entity we created is valid for the database schema.
In this case `Supplier.Name` and `Supplier.Contact.FamilyName` are required, which we enforce in our validator:
```csharp
namespace Example.Domain.Validation;

using Entities;

using FluentValidation;

using NetActive.CleanArchitecture.Domain.Validation;

public class SupplierValidator : EntityValidatorBase<Supplier>
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
## Repository Facade
A respository facade wraps and therefor hides complex repository logic from the client application. 
It provides a clean programming interface, exposing only those methods needed by this command.
In this case it may seem like overkill (agreed), but for more complex commands, for instance interacting with multiple repo's, this pattern becomes very useful.

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
builder.RegisterService<IEntityQueryService<Supplier, SupplierListModel, Guid>, EntityQueryService<Supplier, SupplierListModel, Guid>>(RegisterSingleInstance)
    .WithParameter(Constants.ServiceParameters.Mapper, SupplierMapper.Instance);
```
The registrations is for `IAddSupplierCommand`, `IAddSupplierRepositoryFacade` and `ISupplierFactory` are pretty straight forward.
Then there's a registration for the `SupplierValidator`, which is a implementation of the generic `IEntityValidator<Supplier>` interface.

Most interesting here is the registration of the `IEntityQueryService<Supplier, SupplierListModel, Guid>`.
This service drastically simplifies the implementation of queries in CQRS. 
This generic service is very powerfull and simplifies and automates a lot of things for the developer:

- Provides easy understandable query parameters, supporting filtering, sorting and paging
- Maps entities to models using AutoMapper (more on Mapping below)

The first generic parameter `TEntity` defines the source entity to query. 
The second generic parameter `TModel` defines the destination model to map the source entity to.

## Mapping

Happy coding!

