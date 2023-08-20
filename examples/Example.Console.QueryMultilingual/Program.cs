using Example.Application.Interfaces.Persistence;
using Example.Application.StoreProduct.Configuration;
using Example.Application.StoreProduct.Queries.GetStoreProductList;
using Example.Application.StoreProduct.Queries.GetStoreProductList.Models;
using Example.Domain.Entities;
using Example.Persistence;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Configuration;

// Build a host.
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        // Wire up our clean architecture dependencies.
        services
            .AddPersistenceDependencies<ExampleDbContext, IExampleUnitOfWork, ExampleUnitOfWork>(
                hostContext.Configuration.GetConnectionString("ExampleDbConnection1"),
                options =>
                {
                    options.RegisterEfRepository<StoreProduct>();
                })
            .AddApplicationStoreProductDependencies();
    })
    .Build();

// List all products in a store.
var storeId = new Guid("ab1fb6fb-e9e7-41d9-beb9-06596f397016");
var parameters = new StoreProductQueryParameters(storeId)
    {
        SortBy = StoreProductSortBy.Status,
        ThenBy = StoreProductSortBy.ProductName
    };

// Execute get store product list query.
var productsInStore = await host.Services.GetRequiredService<IGetStoreProductListQuery>().ExecuteAsync(parameters);

foreach (var product in productsInStore)
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
