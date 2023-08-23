namespace Example.Application.Manufacturer.Configuration
{
    using Domain.Entities;
    using Domain.Validation;

    using Commands.AddManufacturer;
    using Commands.AddManufacturer.Factory;
    using Commands.AddManufacturer.Repository;
    
    using Commands.DeleteManufacturer;
    using Commands.DeleteManufacturer.Repository;

    using Queries.GetManufacturerList;
    using Queries.GetManufacturerList.Mapping;
    using Queries.GetManufacturerList.Models;

    using Microsoft.Extensions.DependencyInjection;
    
    using NetActive.CleanArchitecture.Application.Configuration;
    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Configuration;
    using NetActive.CleanArchitecture.Domain.Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationManufacturerDependencies(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            // GetManufacturerListQuery dependencies.
            services.AddService<IGetManufacturerListQuery, GetManufacturerListQuery>(lifetime);
            services.AddEntityQueryService<Manufacturer, ManufacturerListModel, Guid>(ManufacturerMapper.Instance, lifetime);

            // AddManufacturerCommand dependencies.
            services.AddService<IAddManufacturerCommand, AddManufacturerCommand>(lifetime);
            services.AddService<IAddManufacturerRepositoryFacade, AddManufacturerRepositoryFacade>(lifetime);
            services.AddService<IManufacturerFactory, ManufacturerFactory>(lifetime);
            services.AddService<IEntityValidator<Manufacturer>, ManufacturerValidator>(lifetime);

            // IDeleteManufacturerCommandHandler dependencies.
            services.AddService<IDeleteManufacturerCommand, DeleteManufacturerCommand>(lifetime);
            services.AddService<IDeleteManufacturerRepositoryFacade, DeleteManufacturerRepositoryFacade>(lifetime);

            return services;
        }
    }
}
