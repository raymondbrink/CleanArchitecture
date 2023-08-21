# Example.Console.QueryList

This example demonstrates how to retrieve a list of entities from the database. 
It's as simple as resolving the `IGetManufacturerListQuery` query from the DI container and calling the ExecuteAsync(...) method.

Let's have a look at the implementation of the `GetManufacturerListQueryHandler`:

```csharp
internal class GetManufacturerListQuery 
    : IGetManufacturerListQuery
{
    private readonly IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> _query;

    public GetManufacturerListQuery(IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> query)
    {
        _query = query;
    }

    /// <summary>
    /// Returns a list of all manufacturers.
    /// </summary>
    public Task<List<ManufacturerListModel>> ExecuteAsync(CancellationToken? cancellationToken = null)
    {
        return _query.GetItemsAsync(cancellationToken: cancellationToken ?? CancellationToken.None);
    }
}
```

As you can see, there's not much to it. 
That's because the `IEntityQueryService` that gets injected into the constructor of our query, does all the heavy lifting for us.
We just have to call the `GetItemsAsync()` method on that interface and return the result.

```csharp
// List all manufacturers.
var result = await host.Services.GetRequiredService<IGetManufacturerListQuery>().ExecuteAsync();

foreach (var manufacturer in result)
{
    Console.WriteLine($"{manufacturer.Id}: {manufacturer.Name}");
}
```

## Dependencies
To make this work we need two registrations in our DI container. 
These are the relevant manufacturer DI registrations for this query:

```csharp
// GetManufacturerListQuery dependencies.
services.AddService<IGetManufacturerListQuery, GetManufacturerListQuery>(lifetime);
services.AddEntityQueryService<Manufacturer, ManufacturerListModel, Guid>(ManufacturerMapper.Instance, lifetime);
```

Both registrations are pretty straight forward. 
A custom extension method `.AddService<TIService, TService>(ServiceLifeTime)` on IServiceCollection is available to register regular interface based services, like the `IGetManufacturerListQuery` in this case.

Another custom extension method can be used to register the `IEntityQueryService` for the `Manufacturer` entity.
This extension method registers an instance of the generic `EntityQueryService<TEnity, TModel, TKey>` for you, which can be used to query the `Manufacturer` DbSet in your queries using obvious and easy to use methods, mapping the results to `ManufacturerListModel` models using Automapper.

What are those types we pass to this generic service?
Well, `Manufacturer` is easy, that's the entity we want to query. 
`ManufacturerListModel` is the model we want to map the entity to. 
It describes only those details of the manufacturer that we need in our application. 
The query makes sure it only retrieves those entity details from the database and returns a list of those.

Last but not least is an optional type `TKey` describing the type of identifier of the entity, in this case it's a `Guid`. 
If omitted, by default the identifier type `long` is assumed. Whether or not you need to specify the third type parameter `TKey` depends on the type of identifier you specified when designing your entity in the datamodel.
For this to work, we must also specify the identifier type `TKey` using a generic interface `IEntityBase<TKey>` on the (base)entity we query, so in this case on `Company` (the base class of `Manufacturer`), like so:

```csharp
namespace Example.Domain.Entities;

using System;

using NetActive.CleanArchitecture.Domain.Interfaces;

public partial class Company 
    : IEntity<Guid>, IAggregateRoot
{
}
```

Using Entity Developer this was made easy for us, because besides the entity class itself it also generates an empty partial class that we can use to extend the entity.
We just have to derive from `IEntity<Guid>` (and `IAggregateRoot`) by which we specify the identifier of `Company` (and therefor also from its derived entity `Manufacturer`) is of type `Guid`. 
If we wanted to use the default type `long`, we could have inherited from `IEntity` here (withouf specifying `TKey`).

Besides the types, the `EntityQueryService` also requires a mapper instance as input, which it will use to transform each entity (`Manufacturer`) to the specified model (`ManufacturerListModel`).

## Mapping
The mapping from `Manufacturer` to `ManufacturerModel` is defined in a `ManufacturerMappingProfile`. 
This mapping profile is then used by a mapper, of which a singleton instance can be retrieved (lazy loaded) by calling `ManufacturerMapper.Instance`. 
This static method is used in the registration above as a constructor parameter for `EntityQueryService`.

The mapping profile in this example looks like this:

```csharp
internal class ManufacturerMappingProfile : Profile
{
    public ManufacturerMappingProfile()
    {
        CreateMap<Company, CompanyListModel>();
        CreateMap<Manufacturer, ManufacturerListModel>()
            .IncludeBase<Company, CompanyListModel>();
    }
}
```

Because in our datamodel `Manufacturer` is derived from the base class `Company`, we take that into account in our mapping profile.
This means we also need a mapping from `Company` to `CompanyListModel` and configure that to be included in the mapping from `Manufacturer` to `ManufacturerListModel` as a base mapping.

Happy coding!

