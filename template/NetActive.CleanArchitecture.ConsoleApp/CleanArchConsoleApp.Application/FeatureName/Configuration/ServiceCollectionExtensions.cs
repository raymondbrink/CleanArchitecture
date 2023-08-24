namespace CleanArchConsoleApp.Application.FeatureName.Configuration
{
    using Domain.Entities;

    using Queries.FeatureNameExists;
    using Queries.FeatureNameExists.Models;
    using Queries.GetFeatureName;
    using Queries.GetFeatureName.Mapping;
    using Queries.GetFeatureName.Models;
    using Queries.GetPageOfMyEntities;
    using Queries.GetPageOfMyEntities.Mapping;
    using Queries.GetPageOfMyEntities.Models;

    using Microsoft.Extensions.DependencyInjection;

    using NetActive.CleanArchitecture.Application.Configuration;
    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Configuration;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationFeatureNameDependencies(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            // IGetFeatureNameQuery
            services.AddService<IGetFeatureNameQuery, GetFeatureNameQuery>(lifetime);
            services.AddEntityQueryService<FeatureName, FeatureNameDetailModel, Guid>(FeatureNameDetailMapper.Instance, lifetime);

            // IGetPageOfMyEntitiesQuery
            services.AddService<IGetPageOfMyEntitiesQuery, GetPageOfMyEntitiesQuery>(lifetime);
            services.AddEntityQueryService<FeatureName, FeatureNameListModel, Guid>(FeatureNameListMapper.Instance, lifetime);

            // IFeatureNameExistsQuery
            services.AddService<IFeatureNameExistsQuery, FeatureNameExistsQuery>(lifetime);
            services.AddEntityQueryService<FeatureName, FeatureNameExistsModel, Guid>(FeatureNameListMapper.Instance, lifetime);

            return services;
        }
    }
}