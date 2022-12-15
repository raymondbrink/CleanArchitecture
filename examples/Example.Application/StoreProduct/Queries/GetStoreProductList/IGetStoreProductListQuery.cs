namespace Example.Application.StoreProduct.Queries.GetStoreProductList
{
    using Models;

    public interface IGetStoreProductListQuery
    {
        Task<List<StoreProductListModel>> ExecuteAsync(StoreProductQueryParameters? parameters = null);
    }
}