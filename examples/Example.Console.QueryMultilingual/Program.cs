using Autofac;

using Example.Application.StoreProduct.Queries.GetStoreProductList;
using Example.Application.StoreProduct.Queries.GetStoreProductList.Models;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    // List all products in a store.
    var storeId = new Guid("ab1fb6fb-e9e7-41d9-beb9-06596f397016");
    var parameters = new StoreProductQueryParameters(storeId)
        {
            SortBy = StoreProductSortBy.Status,
            ThenBy = StoreProductSortBy.ProductName
    };

    var products = await scope.Resolve<IGetStoreProductListQuery>().ExecuteAsync(parameters);

    foreach (var product in products)
    {
        Console.Write($"{product.Id}: {product.Name} is {product.Status} (");
        Console.Write($"IsAvailable: {product.IsAvailable}");
        if (product.AvailableFrom.HasValue)
        {
            Console.Write($", IsAvailableFrom: {product.AvailableFrom?.ToString("d")}");
        }

        if (product.AvailableUntil.HasValue)
        {
            Console.Write($", IsAvailableUntil: {product.AvailableUntil?.ToString("d")}");
        }

        Console.Write($", IsInStock: {product.IsInStock}");
        if (product.IsInStock)
        {
            Console.Write($", InStock: {product.InStock}");
        }

        Console.WriteLine(")");
    }

    Console.WriteLine();
}