namespace CleanArchMinimalApi.Application.FeatureName.Configuration
{
    using Domain.Entities;

    using Queries.GetFeatureNameList;
    using Queries.GetFeatureNameList.Mapping;
    using Queries.GetFeatureNameList.Models;
    using Queries.GetFeatureName;
    using Queries.GetFeatureName.Mapping;
    using Queries.GetFeatureName.Models;

    using Microsoft.Extensions.DependencyInjection;

    using NetActive.CleanArchitecture.Application.Configuration;
    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Configuration;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationFeatureNameDependencies(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            // IGetFeatureNameListQuery
            services.AddService<IGetFeatureNameListQuery, GetFeatureNameListQuery>(lifetime);
            services.AddEntityQueryService<FeatureName, FeatureNameListModel, KeyType>(FeatureNameListMapper.Instance, lifetime);

            // IGetFeatureNameQuery
            services.AddService<IGetFeatureNameQuery, GetFeatureNameQuery>(lifetime);
            services.AddEntityQueryService<FeatureName, FeatureNameDetailModel, KeyType>(FeatureNameDetailMapper.Instance, lifetime);

            return services;
        }
    }
}