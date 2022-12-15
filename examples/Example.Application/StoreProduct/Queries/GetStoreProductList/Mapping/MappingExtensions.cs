namespace Example.Application.StoreProduct.Queries.GetStoreProductList.Mapping
{
    using System.Linq.Expressions;

    using Domain.Entities;

    using LinqKit;

    using Models;

    internal static class MappingExtensions
    {
        /// <summary>
        /// Determines whether product is available.
        /// </summary>
        public static bool GetIsAvailable(this StoreProduct storeProduct) =>
            MappingExpressions.IsAvailable.Invoke(storeProduct);

        /// <summary>
        /// Determines product status based on whether they are available and in stock.
        /// </summary>
        public static StoreProductStatusModel GetStatus(this StoreProduct storeProduct) =>
            MappingExpressions.Status.Invoke(storeProduct);

        /// <summary>
        /// Determines whether product is in-stock.
        /// </summary>
        /// <param name="storeProduct"></param>
        public static bool GetInStock(this StoreProduct storeProduct) =>
            MappingExpressions.IsInStock.Invoke(storeProduct);

        /// <summary>
        /// Determines product's availability date.
        /// </summary>
        /// <param name="storeProduct"></param>
        public static DateTime? GetIsAvailableFrom(this StoreProduct storeProduct)
        {
            Expression<Func<StoreProduct, DateTime?>> expression = product =>
                product.Product.AvailableFrom > DateTime.UtcNow ? product.Product.AvailableFrom : null;

            return expression.Invoke(storeProduct);
        }

        /// <summary>
        /// Determines product's unavailability date.
        /// </summary>
        public static DateTime? GetIsAvailableUntil(this StoreProduct storeProduct)
        {
            Expression<Func<StoreProduct, DateTime?>> expression = product =>
                product.Product.AvailableUntil > DateTime.UtcNow ? product.Product.AvailableUntil : null;

            return expression.Invoke(storeProduct);
        }
    }
}