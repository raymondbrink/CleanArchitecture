namespace Example.Application.StoreProduct.Queries.GetStoreProductList.Models;

public class StoreProductFilterModel
{
    /// <summary>
    /// Gets or sets the store ID to filter products by.
    /// </summary>
    public Guid? StoreId { get; set; }
}