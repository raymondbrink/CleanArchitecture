namespace MyProject.Application.MyEntity.Autofac
{
    using Domain.Entities;

    using global::Autofac;

    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services;
    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Autofac;
    using NetActive.CleanArchitecture.Autofac.Extensions;

    using Queries.MyEntityExists;
    using Queries.MyEntityExists.Models;
    using Queries.GetMyEntity;
    using Queries.GetMyEntity.Mapping;
    using Queries.GetMyEntity.Models;
    using Queries.GetPageOfMyEntities;
    using Queries.GetPageOfMyEntities.Mapping;
    using Queries.GetPageOfMyEntities.Models;

    public class Module : BaseModule
    {
        public Module(bool registerSingleInstance)
            : base(registerSingleInstance)
        {
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            // IGetMyEntityQuery
            builder.RegisterService<IGetMyEntityQuery, GetMyEntityQuery>(RegisterSingleInstance);
            builder.RegisterService<IEntityQueryService<MyEntity, MyEntityDetailModel, Guid>,
                    EntityQueryService<MyEntity, MyEntityDetailModel, Guid>>(RegisterSingleInstance)
                .WithParameter(Constants.ServiceParameters.Mapper, MyEntityDetailMapper.Instance);

            // IGetPageOfMyEntitiesQuery
            builder.RegisterService<IGetPageOfMyEntitiesQuery, GetPageOfMyEntitiesQuery>(RegisterSingleInstance);
            builder.RegisterService<IEntityQueryService<MyEntity, MyEntityListModel, Guid>,
                    EntityQueryService<MyEntity, MyEntityListModel, Guid>>(RegisterSingleInstance)
                .WithParameter(Constants.ServiceParameters.Mapper, MyEntityListMapper.Instance);

            // IMyEntityExistsQuery
            builder.RegisterService<IMyEntityExistsQuery, MyEntityExistsQuery>(RegisterSingleInstance);
            builder
                .RegisterService<IEntityQueryService<MyEntity, MyEntityExistsModel, Guid>,
                    EntityQueryService<MyEntity, MyEntityExistsModel, Guid>>(RegisterSingleInstance)
                .WithParameter(Constants.ServiceParameters.Mapper, MyEntityListMapper.Instance);
        }
    }
}