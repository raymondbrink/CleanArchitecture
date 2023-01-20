namespace Example.Application.Company.Queries.GetPageOfCompanies.Models
{
    using System.Linq.Expressions;

    using Domain.Entities;

    using LinqKit;

    using NetActive.CleanArchitecture.Application.Models;

    public class CompanyQueryParams
        : BasePagedQueryParameters<Company, Guid, CompanySortBy?, CompanyFilterModel>
    {
        public override Expression<Func<Company, bool>> GetFilterExpression()
        {
            var predicate = PredicateBuilder.New<Company>(true);

            if (!string.IsNullOrWhiteSpace(Filters?.NameContains))
            {
                // Filter by company name.
                predicate = predicate.And(c => c.Name.Contains(Filters.NameContains));
            }

            return predicate;
        }

        public override Expression<Func<Company, object>> GetSortingExpression() => getSortExpression(SortBy);

        public override Expression<Func<Company, object>> GetAdditionalSortingExpression() => getSortExpression(ThenBy);

        private static Expression<Func<Company, object>> getSortExpression(CompanySortBy? sortColumn)
        {
            return sortColumn switch
            {
                null => c => c.Id,
                CompanySortBy.Id => c => c.Id,
                CompanySortBy.CreatedAtUtc => c => c.CreatedAtUtc,
                CompanySortBy.Name => c => c.Name,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}