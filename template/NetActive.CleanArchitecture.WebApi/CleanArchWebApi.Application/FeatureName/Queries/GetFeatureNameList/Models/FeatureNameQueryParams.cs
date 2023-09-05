namespace CleanArchWebApi.Application.FeatureName.Queries.GetFeatureNameList.Models
{
    using System;
    using System.Linq.Expressions;

    using Domain.Entities;

    using LinqKit;

    using NetActive.CleanArchitecture.Application.Models;

    /// <inheritdoc cref="BaseQueryParameters{TEntity,TKey,TSortModel,TFilterModel}" />
    public class FeatureNameQueryParams
        : BaseQueryParameters<FeatureName, KeyType, FeatureNameSortBy, FeatureNameFilterModel>
    {
        public override Expression<Func<FeatureName, bool>> GetFilterExpression()
        {
            var expression = PredicateBuilder.New<FeatureName>(true);

            if (!string.IsNullOrWhiteSpace(Filters.NameContains))
            {
                // Filter by FeatureName name.
                expression = expression.And(c => c.Name.Contains(Filters.NameContains));
            }

            return expression;
        }

        public override Expression<Func<FeatureName, object>> GetSortingExpression()
        {
            return SortBy switch
            {
                FeatureNameSortBy.Id => c => c.Id,
                FeatureNameSortBy.Name => c => c.Name,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}