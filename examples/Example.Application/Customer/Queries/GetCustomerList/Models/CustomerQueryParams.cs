namespace Example.Application.Customer.Queries.GetCustomerList.Models
{
    using System.Linq.Expressions;

    using Domain.Entities;

    using LinqKit;

    using NetActive.CleanArchitecture.Application.Models;

    /// <inheritdoc cref="BaseQueryParameters{TEntity,TKey,TSortModel,TFilterModel}" />
    public class CustomerQueryParams
        : BaseQueryParameters<Customer, int, CustomerSortBy, CustomerFilterModel>
    {
        public override Expression<Func<Customer, bool>> GetFilterExpression()
        {
            var expression = PredicateBuilder.New<Customer>(true);

            if (!string.IsNullOrWhiteSpace(Filters.NameContains))
            {
                // Filter by customer name.
                expression = expression.And(
                    c => c.Name.GivenName.Contains(Filters.NameContains) ||
                         c.Name.FamilyName.Contains(Filters.NameContains));
            }

            // Filter by customer availability.
            expression = Filters.Availability switch
            {
                EntityAvailability.Archived => expression.And(c => c.ArchivedAtUtc.HasValue),
                EntityAvailability.NonArchived => expression.And(c => !c.ArchivedAtUtc.HasValue),
                _ => expression
            };

            return expression;
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
}