namespace MyProject.Application.MyEntity.Configuration
{
    using Domain.Entities;

    using Queries.MyEntityExists;
    using Queries.MyEntityExists.Models;
    using Queries.GetMyEntity;
    using Queries.GetMyEntity.Mapping;
    using Queries.GetMyEntity.Models;
    using Queries.GetPageOfMyEntities;
    using Queries.GetPageOfMyEntities.Mapping;
    using Queries.GetPageOfMyEntities.Models;

    using Microsoft.Extensions.DependencyInjection;

    using NetActive.CleanArchitecture.Application.Configuration;
    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Configuration;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationMyEntityDependencies(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            // IGetMyEntityQuery
            services.AddService<IGetMyEntityQuery, GetMyEntityQuery>(lifetime);
            services.AddEntityQueryService<MyEntity, MyEntityDetailModel, Guid>(MyEntityDetailMapper.Instance, lifetime);

            // IGetPageOfMyEntitiesQuery
            services.AddService<IGetPageOfMyEntitiesQuery, GetPageOfMyEntitiesQuery>(lifetime);
            services.AddEntityQueryService<MyEntity, MyEntityListModel, Guid>(MyEntityListMapper.Instance, lifetime);

            // IMyEntityExistsQuery
            services.AddService<IMyEntityExistsQuery, MyEntityExistsQuery>(lifetime);
            services.AddEntityQueryService<MyEntity, MyEntityExistsModel, Guid>(MyEntityListMapper.Instance, lifetime);

            return services;
        }
    }
}