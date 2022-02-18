namespace Example.Application.StoreProduct.Queries.GetStoreProductList.Models;

using System.Linq.Expressions;

using Domain.Entities;

using LinqKit;

using Mapping;

using NetActive.CleanArchitecture.Application.Models;

public class StoreProductQueryParameters
    : BaseQueryParameters<StoreProduct, StoreProductSortBy, StoreProductFilterModel>
{
    public StoreProductQueryParameters(Guid storeId)
    {
        Filters.StoreId = storeId;
    }

    public override Expression<Func<StoreProduct, bool>> GetFilterExpression()
    {
        var predicate = PredicateBuilder.New<StoreProduct>();

        if (Filters.StoreId.HasValue)
        {
            predicate = predicate.And(p => p.StoreId.Equals(Filters.StoreId.Value));
        }

        return predicate;
    }

    public override Expression<Func<StoreProduct, object>> GetSortingExpression() => getSortExpression(SortBy);

    public override Expression<Func<StoreProduct, object>> GetAdditionalSortingExpression() =>
        getSortExpression(ThenBy);

    private static Expression<Func<StoreProduct, object>> getSortExpression(StoreProductSortBy sortColumn)
    {
        return sortColumn switch
            {
                StoreProductSortBy.Id => c => c.Id,
                StoreProductSortBy.ProductName => c => MappingExpressions.ProductName.Invoke(c),
                //StoreProductSortBy.ProductName => c => MappingExpressions.FromTranslation(t => t.Name).Invoke(c.Product.Translations),
                StoreProductSortBy.Status => c => MappingExpressions.Status.Invoke(c),
                StoreProductSortBy.InStock => c => c.InStock,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}