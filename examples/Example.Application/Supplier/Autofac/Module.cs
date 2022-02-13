namespace Example.Application.Supplier.Autofac;

using Commands.AddSupplier;
using Commands.AddSupplier.Factory;
using Commands.AddSupplier.Repository;
using Commands.DeleteSupplier;
using Commands.DeleteSupplier.Repository;

using Domain.Entities;
using Domain.Validation;

using global::Autofac;

using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services;
using NetActive.CleanArchitecture.Application.Interfaces;
using NetActive.CleanArchitecture.Autofac;
using NetActive.CleanArchitecture.Autofac.Extensions;
using NetActive.CleanArchitecture.Domain.Interfaces;

using Queries;
using Queries.GetSupplierList;
using Queries.GetSupplierList.Mapping;
using Queries.GetSupplierList.Models;

public class Module : BaseModule
{
    public Module(bool registerSingleInstance)
        : base(registerSingleInstance)
    {
    }

    /// <inheritdoc />
    protected override void Load(ContainerBuilder builder)
    {
        // IGetSupplierListQuery
        builder.RegisterService<IGetSupplierListQuery, GetSupplierListQuery>(RegisterSingleInstance);
        builder.RegisterService<IEntityQueryService<Supplier, SupplierListModel, Guid>, EntityQueryService<Supplier, SupplierListModel, Guid>>(RegisterSingleInstance)
            .WithParameter(Constants.ServiceParameters.Mapper, SupplierMapper.Instance);

        // IAddSupplierCommand
        builder.RegisterService<IAddSupplierCommand, AddSupplierCommand>(RegisterSingleInstance);
        builder.RegisterService<IAddSupplierRepositoryFacade, AddSupplierRepositoryFacade>(RegisterSingleInstance);
        builder.RegisterService<ISupplierFactory, SupplierFactory>(RegisterSingleInstance);
        builder.RegisterService<IEntityValidator<Supplier>, SupplierValidator>(RegisterSingleInstance);

        // IDeleteSupplierCommand
        builder.RegisterService<IDeleteSupplierCommand, DeleteSupplierCommand>(RegisterSingleInstance);
        builder.RegisterService<IDeleteSupplierRepositoryFacade, DeleteSupplierRepositoryFacade>(RegisterSingleInstance);
    }
}