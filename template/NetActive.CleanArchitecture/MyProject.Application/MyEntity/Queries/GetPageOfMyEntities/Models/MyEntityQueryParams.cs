namespace MyProject.Application.MyEntity.Queries.GetPageOfMyEntities.Models
{
    using System.Linq.Expressions;

    using Domain.Entities;

    using LinqKit;

    using NetActive.CleanArchitecture.Application.Models;

    public class MyEntityQueryParams
        : BasePagedQueryParameters<MyEntity, Guid, MyEntitySortBy?, MyEntityFilterModel>
    {
        public override Expression<Func<MyEntity, bool>> GetFilterExpression()
        {
            var predicate = PredicateBuilder.New<MyEntity>(true);

            if (!string.IsNullOrWhiteSpace(Filters?.NameContains))
            {
                // Filter by MyEntity name.
                predicate = predicate.And(c => c.Name.Contains(Filters.NameContains));
            }

            return predicate;
        }

        public override Expression<Func<MyEntity, object>> GetSortingExpression() => getSortExpression(SortBy);

        public override Expression<Func<MyEntity, object>> GetAdditionalSortingExpression() => getSortExpression(ThenBy);

        private static Expression<Func<MyEntity, object>> getSortExpression(MyEntitySortBy? sortColumn)
        {
            return sortColumn switch
            {
                null => c => c.Id,
                MyEntitySortBy.Id => c => c.Id,
                MyEntitySortBy.CreatedAtUtc => c => c.CreatedAtUtc,
                MyEntitySortBy.Name => c => c.Name,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}