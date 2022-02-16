# Example.Console.QueryPaged

This example demonstrates how to retrieve a page of entities from the database. 
It's as simple as resolving the right query from the DI container and calling the `ExecuteAsync()` method.

Let's have a look at the implementation of the query:

```csharp
internal class GetPageOfCompaniesQuery : IGetPageOfCompaniesQuery
{
    private readonly IEntityQueryService<Company, CompanyListModel, Guid> _query;

    public GetPageOfCompaniesQuery(IEntityQueryService<Company, CompanyListModel, Guid> query)
    {
        _query = query;
    }

    /// <inheritdoc />
    public Task<PagedQueryResultModel<CompanyListModel>> ExecuteAsync(CompanyQueryParams parameters = null)
    {
        return _query.GetPageOfItemsAsync(parameters);
    }
}
```
As you can see, there's not much to it. 
That's because the `IEntityQueryService` that gets injected into the constructor of our query, does all the heavy lifting for us.
We just have to call the `GetPageOfItemsAsync()` method on that interface and return the result.
By default it will return a page of max 10 companies, sorted by their Id. 
We can change this behaviour by supplying `CompanyQueryParameters` to it, as we do in this example:

```csharp
var query = scope.Resolve<IGetPageOfCompaniesQuery>();

// Get first page of (max 3) companies, sorted by their name.
var parameters = new CompanyQueryParams { PageSize = 3, SortBy = CompanySortBy.Name };
var pageOfCompanies = await query.ExecuteAsync(parameters);
```

Here we specify we want a page of max 3 companies, sorted by the company's name.
After checking if there is another page available, we can easily get the next page of companies (using the same page size and sorting parameters):

```csharp
if (!pageOfCompanies.HasNextPage())
{
    // No need to re-query for next page.
    break;
}

// Re-query for next page of (max 3) companies.
parameters.PageIndex++;
pageOfCompanies = await query.ExecuteAsync(parameters);
```


## Module
To make this work we need two registrations in our DI container.
These are the relevant company Module registrations for this query:

```csharp
// IGetPageOfCompaniesQuery
builder.RegisterService<IGetPageOfCompaniesQuery, GetPageOfCompaniesQuery>(RegisterSingleInstance);
builder.RegisterService<IEntityQueryService<Company, CompanyListModel, Guid>,
        EntityQueryService<Company, CompanyListModel, Guid>>(RegisterSingleInstance)
    .WithParameter(Constants.ServiceParameters.Mapper, CompanyMapper.Instance);
```

The registration for `IGetPageOfCompaniesQuery` is pretty straight forward.
Finally there's a registration for the `EntityQueryService<Company, CompanyListModel, Guid>`, which is an implementation of the generic `IEntityQueryService<Company, CompanyListModel, Guid>` interface.

What are those types we pass to this generic service?
Well, `Company` is easy, that's the entity we want to query. `CompanyListModel` is the model we want to map the entity to. 
It describes only those details of a company that we need in our application. 
The query makes sure it only retrieves those entity details from the database and returns a list of those.

Last but not least is an optional type `TKey` describing the type of identifier of the entity, in this case it's a `Guid`. 
If omitted, by default the identifier type `long` is assumed. Whether or not you need to specify the third type parameter `TKey` depends on the type of identifier you specified when designing your entity in the datamodel.
For this to work, we must also specify the identifier type `TKey` using a generic interface `IEntityBase<TKey>` on the (base)entity we query, so in this case on `Company`, like so:

```csharp
namespace Example.Domain.Entities;

using System;

using NetActive.CleanArchitecture.Domain.Interfaces;

public partial class Company : IEntity<Guid>
{
}
```

Using Entity Developer this was made easy for us, because besides the entity class itself it also generates an empty partial class that we can use to extend the entity.
We just have to derive from `IEntity<Guid>` by which we specify the identifier of `Company` is of type `Guid`. 
If we wanted to use the default type `long`, we could have inherited from `IEntity` here (withouf specifying `TKey`).

Besides the types, the `EntityQueryService` also requires a mapper instance as input, which it will use to transform each entity (`Company`) to the specified model (`CompanyListModel`).

## Mapping
The mapping from `Company` to `CompanyModel` is defined in a `CompanyMappingProfile`. 
This mapping profile is then used by a mapper, of which a singleton instance can be retrieved (lazy loaded) by calling `CompanyMapper.Instance`. 
This static method is used in the registration above as a constructor parameter for `EntityQueryService`.

The mapping profile in this example looks like this:

```csharp
internal class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Company, CompanyListModel>();
    }
}
```

Happy coding!

