namespace Example.Application.Manufacturer.Autofac
{
    using Commands.AddManufacturer.Factory;
    using Commands.AddManufacturer.Repository;
    using Commands.DeleteManufacturer.Repository;

    using Domain.Entities;
    using Domain.Validation;

    using global::Autofac;

    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services;
    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Autofac;
    using NetActive.CleanArchitecture.Autofac.Extensions;
    using NetActive.CleanArchitecture.Domain.Interfaces;

    using Queries.GetManufacturerList.Mapping;
    using Queries.GetManufacturerList.Models;

    public class Module : BaseModule
    {
        public Module(bool registerSingleInstance)
            : base(registerSingleInstance)
        {
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            // GetManufacturerListQueryHandler dependencies.
            builder.RegisterService<IEntityQueryService<Manufacturer, ManufacturerListModel, Guid>, EntityQueryService<Manufacturer, ManufacturerListModel, Guid>>(RegisterSingleInstance)
                .WithParameter(Constants.ServiceParameters.Mapper, ManufacturerMapper.Instance);

            // AddManufacturerCommandHandler dependencies.
            builder.RegisterService<IAddManufacturerRepositoryFacade, AddManufacturerRepositoryFacade>(RegisterSingleInstance);
            builder.RegisterService<IManufacturerFactory, ManufacturerFactory>(RegisterSingleInstance);
            builder.RegisterService<IEntityValidator<Manufacturer>, ManufacturerValidator>(RegisterSingleInstance);

            // IDeleteManufacturerCommandHandler dependencies.
            builder.RegisterService<IDeleteManufacturerRepositoryFacade, DeleteManufacturerRepositoryFacade>(RegisterSingleInstance);
        }
    }
}