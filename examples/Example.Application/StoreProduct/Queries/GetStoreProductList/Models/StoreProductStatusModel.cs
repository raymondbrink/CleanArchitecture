namespace Example.Application.StoreProduct.Queries.GetStoreProductList.Models
{
    public enum StoreProductStatusModel
    {
        /// <summary>
        /// Product is currently not available (yet).
        /// </summary>
        Unavailable = 0,

        /// <summary>
        /// Product is available but not in stock.
        /// </summary>
        NotInStock = 1,

        /// <summary>
        /// Product is available and in stock.
        /// </summary>
        Available = 2
    }
}