# Example.Console.QueryList

This example demonstrates how to retrieve a list of entities from the database. 
It's as simple as creating an instance of the `GetManufacturerListQuery` and sending it to MediatR.

Let's have a look at the implementation of the `GetManufacturerListQueryHandler`:

```csharp
internal sealed class GetManufacturerListQueryHandler : BaseQueryHandler<GetManufacturerListQuery, ManufacturerListResponse>
{
    private readonly IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> _query;

    public GetManufacturerListQueryHandler(IEntityQueryService<Manufacturer, ManufacturerListModel, Guid> query, IPublisher publisher)
        : base(publisher)
    {
        _query = query;
    }

    public override async Task<ManufacturerListResponse> Handle(GetManufacturerListQuery request, CancellationToken cancellationToken)
    {
        var result = new ManufacturerListResponse(await _query.GetItemsAsync(cancellationToken: cancellationToken));

        await PublishNotificationAsync("Requested a list of all manufacturers", cancellationToken);

        return result;
    }
}
```

As you can see, there's not much to it. 
That's because the `IEntityQueryService` that gets injected into the constructor of our query, does all the heavy lifting for us.
We just have to call the `GetItemsAsync()` method on that interface and return the result.
For demonstration purposes we publish a notification using MediatR, that can be used for logging for instance.
To find out where this "magical" generic `IEntityQueryService` comes from we should look at the Manufacturer module.

## Module
To make this work we need only one registration in our DI container as MediatR registers the command and its respective handler for us.

This is the relevant Manufacturer Module registration for this query:

```csharp
// IGetManufacturerListQuery
builder.RegisterService<IEntityQueryService<Manufacturer, ManufacturerListModel, Guid>, EntityQueryService<Manufacturer, ManufacturerListModel, Guid>>(RegisterSingleInstance)
    .WithParameter(Constants.ServiceParameters.Mapper, ManufacturerMapper.Instance);
```

There's a registration for the `EntityQueryService<Manufacturer, ManufacturerListModel, Guid>`, which is an implementation of the generic `IEntityQueryService<Manufacturer, ManufacturerListModel, Guid>` interface.

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

public partial class Company : IEntity<Guid>
{
}
```

Using Entity Developer this was made easy for us, because besides the entity class itself it also generates an empty partial class that we can use to extend the entity.
We just have to derive from `IEntity<Guid>` by which we specify the identifier of `Company` (and therefor also from its derived entity `Manufacturer`) is of type `Guid`. 
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

