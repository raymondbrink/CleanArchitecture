# Example.Console.QueryList

This example demonstrates how to retrieve a list of entities from the database. 
It's as simple as resolving the right query from the DI container and calling the `ExecuteAsync()` method.

Let's have a look at the implementation:

```csharp
internal class GetSupplierListQuery : IGetSupplierListQuery
{
    private readonly IEntityQueryService<Supplier, SupplierListModel, Guid> _query;

    public GetSupplierListQuery(IEntityQueryService<Supplier, SupplierListModel, Guid> query)
    {
        _query = query;
    }

    public Task<List<SupplierListModel>> ExecuteAsync()
    {
        return _query.GetItemsAsync();
    }
}
```
As you can see, there's not much to it. 
This is because the `IEntityQueryService` that gets injected into the constructor of our query, does all the heavy lifting for us.
We just have to call the `GetItemsAsync()` method on that interface and return the result.
To find out where this "magical" generic `IEntityQueryService` comes from we should look at the Supplier module.

## Module
To make this work we need two registrations in our DI container.
These are the relevant supplier Module registrations for this query:

```csharp
// IGetSupplierListQuery
builder.RegisterService<IGetSupplierListQuery, GetSupplierListQuery>(RegisterSingleInstance);
builder.RegisterService<IEntityQueryService<Supplier, SupplierListModel, Guid>, EntityQueryService<Supplier, SupplierListModel, Guid>>(RegisterSingleInstance)
    .WithParameter(Constants.ServiceParameters.Mapper, SupplierMapper.Instance);
```

The registration for `IGetSupplierListQuery` is pretty straight forward.
Finally there's a registration for the `EntityQueryService<Supplier, SupplierListModel, Guid>`, which is an implementation of the generic `IEntityQueryService<Supplier, SupplierListModel, Guid>` interface.

What are those types we pass to this generic service?
Well, `Supplier` is easy, that's the entity we want to query. `SupplierListModel` is the model we want to map the entity to. 
It describes the details of the entity we need in our application. The query will return a list of those.

Last but not least is an optional type `TKey` describing the type of identifier of the entity, in this case it's a `Guid`. 
If omitted, by default the identifier type `long` is assumed. Whether or not you need to specify the third type parameter `TKey` depends on the type of identifier you specified when designing your entity in the datamodel.
For this to work, we must also specify the identifier type `TKey` using a generic interface `IEntityBase<TKey>` on the (base)entity we query, so in this case on `Company` (the base class of `Supplier`) we specify the identitifier (key) type like so:

```csharp
namespace Example.Domain.Entities;

using System;

using NetActive.CleanArchitecture.Domain.Interfaces;

public partial class Company : IEntityBase<Guid>
{
}
```

Using Entity Developer this was made easy for us, because besides the entity class itself it also generates an empty partial class that we can use to extend the entity.
We just have to derive from `IEntityBase<Guid>` by which we specify the identifier of `Company` (and therefor also from its derived entity `Supplier`) is of type `Guid`. 
If we wanted to use the default type `long`, we could have inherited from `IEntityBase` here (withouf specifying `TKey`).

Besides the types, the `EntityQueryService` also requires a mapper instance as input, which it will use to transform each entity (`Supplier`) to the specified model (`SupplierListModel`).

## Mapping
The mapping from `Supplier` to `SupplierModel` is defined in a `SupplierMappingProfile`. 
This mapping profile is then used by a singleton mapper instance, of which a singleton instance can be retrieved (lazy loaded) by calling `SupplierMapper.Instance`. 
This static method is used in the registration above as a parameter for `EntityQueryService`.

The mapping profile in this example looks like this:

```csharp
internal class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        CreateMap<Company, CompanyListModel>();
        CreateMap<Supplier, SupplierListModel>()
            .IncludeBase<Company, CompanyListModel>();
    }
}
```

Because in our datamodel `Supplier` is derived from the base class `Company`, we take that into account in our mapping profile.
This means we also need a mapping from `Company` to `CompanyListModel` and configure that to be included in the mapping from `Supplier` to `SupplierListModel` as a base mapping.

Happy coding!

