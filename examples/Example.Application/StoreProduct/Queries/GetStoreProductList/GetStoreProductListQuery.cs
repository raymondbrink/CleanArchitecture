namespace Example.Application.StoreProduct.Queries.GetStoreProductList;

using Domain.Entities;

using Models;

using NetActive.CleanArchitecture.Application.Interfaces;

internal class GetStoreProductListQuery : IGetStoreProductListQuery
{
    private readonly IEntityQueryService<StoreProduct, StoreProductListModel> _query;

    public GetStoreProductListQuery(IEntityQueryService<StoreProduct, StoreProductListModel> query)
    {
        _query = query;
    }

    public Task<List<StoreProductListModel>> ExecuteAsync(StoreProductQueryParameters? parameters = null)
    {
        return _query.GetItemsAsync(parameters);
    }
}