namespace Example.Application.StoreProduct.Configuration
{
    using Domain.Entities;

    using Queries.GetStoreProductList;
    using Queries.GetStoreProductList.Mapping;
    using Queries.GetStoreProductList.Models;

    using Microsoft.Extensions.DependencyInjection;

    using NetActive.CleanArchitecture.Application.Configuration;
    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Configuration;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationStoreProductDependencies(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            // IGetStoreProductListQuery
            services
                .AddService<IGetStoreProductListQuery, GetStoreProductListQuery>(lifetime)
                .AddEntityQueryService<StoreProduct, StoreProductListModel>(StoreProductMapper.Instance, lifetime);

            return services;
        }
    }
}
