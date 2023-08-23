namespace NetActive.CleanArchitecture.UseCase.FeatureName.Queries.UseCaseName.Models
{
    using System.Linq.Expressions;

    using Domain.Entities;

    using LinqKit;

    using NetActive.CleanArchitecture.Application.Models;

    public class FeatureNameQueryParams
        : BasePagedQueryParameters<FeatureName, KeyType, FeatureNameSortBy?, FeatureNameFilterModel>
    {
        public override Expression<Func<FeatureName, bool>> GetFilterExpression()
        {
            var predicate = PredicateBuilder.New<FeatureName>(true);

            if (!string.IsNullOrWhiteSpace(Filters?.NameContains))
            {
                // Filter by FeatureName name.
                predicate = predicate.And(c => c.Name.Contains(Filters.NameContains));
            }

            return predicate;
        }

        public override Expression<Func<FeatureName, object>> GetSortingExpression() => getSortExpression(SortBy);

        public override Expression<Func<FeatureName, object>> GetAdditionalSortingExpression() => getSortExpression(ThenBy);

        private static Expression<Func<FeatureName, object>> getSortExpression(FeatureNameSortBy? sortColumn)
        {
            return sortColumn switch
            {
                null => c => c.Id,
                FeatureNameSortBy.Id => c => c.Id,
                FeatureNameSortBy.Name => c => c.Name,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}