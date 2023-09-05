# Example.Console.QueryOne

This example demonstrates how to retrieve a boolean value indicating whether a specific entity exists in the database. 
It's as simple as resolving the `ICompanyExistsQuery` query from the DI container and calling the `ExecuteAsync(...)` method.

Let's have a look at the implementation of the query:

```csharp
internal class CompanyExistsQuery 
    : ICompanyExistsQuery
{
    private readonly IEntityExistsService<Company, Guid> _query;

    public CompanyExistsQuery(IEntityExistsService<Company, Guid> query)
    {
        _query = query;
    }

    public Task<bool> ExecuteAsync(string name)
    {
        return _query.ExistsAsync(c => c.Name.Equals(name));
    }
}
```
As you can see, there's not much to it. 
That's because the `IEntityExistsService` that gets injected into the constructor of our query, does all the heavy lifting for us.
We just have to call the `ExistsAsync()` method on that interface and return the result.

```csharp
// Determine if a company with a specific name exists.
var companyToFind = "some company";
var companyWithNameExists = await scope.Resolve<ICompanyExistsQuery>().ExecuteAsync(companyToFind);
Console.WriteLine($"Company '{companyToFind}' exists: {companyWithNameExists}");
```

## Dependencies
To make this work we need two registrations in our DI container.
These are the relevant company DI registrations for this query:

```csharp
// ICompanyExistsQuery
services.AddService<ICompanyExistsQuery, CompanyExistsQuery>(lifetime);
services.AddEntityExistsService<Company, Guid>(lifetime);
```

Both registrations are pretty straight forward. 
A custom extension method on IServiceCollection is available to register regular interface based services, like the `ICompanyExistsQuery` in this case.
Another custom extension method can be used to register the `IEntityExistsService` for the `Company` entity.
This extension method registers an instance of the generic `EntityExistsService<TEnity, TKey>` for you, which can be used to query the `Company` DbSet in your queries using obvious and easy to use methods.

What are those types we pass to this generic service?
Well, `Company` is easy, that's the entity we want to query. 

There's also an optional type `TKey` describing the type of identifier of the entity, in this case it's a `Guid`. 
If omitted, by default the identifier type `long` is assumed. 
Whether or not you need to specify the third type parameter `TKey` depends on the type of identifier you specified when designing your entity in the datamodel.
For this to work, we must also specify the identifier type `TKey` using a generic interface `IEntityBase<TKey>` on the (base)entity we query, so in this case on `Company`, like so:

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
We just have to derive from `IEntity<Guid>` by which we specify that the identifier of `Company` is of type `Guid`. 
If we wanted to use the default type `long`, we could have inherited from `IEntity` here (withouf specifying `TKey`).
We also derive from `IAggregateRoot` here, which is required if you want to use the `EntityExistsService` to perform queries on the entity.

## No Mapping?
Because we don't return any types from our query, we don't need to register any mapping for this type of query.

Happy coding!

