namespace Example.Application.Customer.Autofac
{
    using Commands.ArchiveCustomer;
    using Commands.ArchiveCustomer.Repository;

    using Domain.Entities;

    using global::Autofac;

    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services;
    using NetActive.CleanArchitecture.Application.Interfaces;
    using NetActive.CleanArchitecture.Autofac;
    using NetActive.CleanArchitecture.Autofac.Extensions;

    using Queries.GetCustomerList;
    using Queries.GetCustomerList.Mapping;
    using Queries.GetCustomerList.Models;

    public class Module : BaseModule
    {
        public Module(bool registerSingleInstance)
            : base(registerSingleInstance)
        {
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            // IGetCustomerListQuery
            builder.RegisterService<IGetCustomerListQuery, GetCustomerListQuery>(RegisterSingleInstance);
            builder
                .RegisterService<IEntityQueryService<Customer, CustomerListModel, int>,
                    EntityQueryService<Customer, CustomerListModel, int>>(RegisterSingleInstance)
                .WithParameter(Constants.ServiceParameters.Mapper, CustomerMapper.Instance);

            // IArchiveCustomerCommand
            builder.RegisterService<IArchiveCustomerCommand, ArchiveCustomerCommand>(RegisterSingleInstance);
            builder.RegisterService<IArchiveCustomerRepositoryFacade, ArchiveCustomerRepositoryFacade>(RegisterSingleInstance);
        }
    }
}