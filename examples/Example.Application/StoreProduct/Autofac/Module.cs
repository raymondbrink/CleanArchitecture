namespace Example.Application.StoreProduct.Autofac
{
    using Domain.Entities;

    using global::Autofac;

    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services;
    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Autofac;
    using NetActive.CleanArchitecture.Autofac.Extensions;

    using Queries.GetStoreProductList;
    using Queries.GetStoreProductList.Mapping;
    using Queries.GetStoreProductList.Models;

    public class Module : BaseModule
    {
        public Module(bool registerSingleInstance)
            : base(registerSingleInstance)
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            // IGetStoreProductListQuery
            builder.RegisterService<IGetStoreProductListQuery, GetStoreProductListQuery>(RegisterSingleInstance);
            builder
                .RegisterService<IEntityQueryService<StoreProduct, StoreProductListModel>,
                    EntityQueryService<StoreProduct, StoreProductListModel>>(RegisterSingleInstance)
                .WithParameter(Constants.ServiceParameters.Mapper, StoreProductMapper.Instance);
        }
    }
}