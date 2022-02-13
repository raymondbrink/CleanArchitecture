namespace Example.Application.Customer.Queries.GetCustomerList.Models;

using System.Linq.Expressions;

using Domain.Entities;

using LinqKit;

using NetActive.CleanArchitecture.Application.Models;

/// <inheritdoc cref="QueryParametersBase&lt;TEntity, TKey, TSortModel, TFilterModel&gt;" />
public class CustomerQueryParams
    : QueryParametersBase<Customer, int, CustomerSortBy, CustomerFilterModel>
{
    public override Expression<Func<Customer, bool>> GetFilterExpression()
    {
        Expression<Func<Customer, bool>> predicate = PredicateBuilder.New<Customer>(true).DefaultExpression;

        if (!string.IsNullOrWhiteSpace(Filters.NameContains))
        {
            // Filter by customer name.
            predicate = predicate.And(c => c.Name.Contains(Filters.NameContains));
        }

        // Filter by customer availability.
        predicate = Filters.Availability switch
        {
            EntityAvailability.Archived => predicate.And(c => c.ArchivedAtUtc.HasValue),
            EntityAvailability.NonArchived => predicate.And(c => !c.ArchivedAtUtc.HasValue),
            _ => predicate
        };

        return predicate;
    }

    public override Expression<Func<Customer, object>> GetSortingExpression()
    {
        return SortBy switch
        {
            CustomerSortBy.Id => c => c.Id,
            CustomerSortBy.Name => c => c.Name,
            CustomerSortBy.ArchivedAtUtc => c => c.ArchivedAtUtc ?? new DateTime(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}