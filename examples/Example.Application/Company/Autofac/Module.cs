namespace Example.Application.Company.Autofac;

using Domain.Entities;

using global::Autofac;

using NetActive.CleanArchitecture.Application.Interfaces;
using NetActive.CleanArchitecture.Application.Services;
using NetActive.CleanArchitecture.Autofac;
using NetActive.CleanArchitecture.Autofac.Extensions;

using Queries.CompanyExists;
using Queries.CompanyExists.Models;
using Queries.GetCompanyList;
using Queries.GetCompanyList.Mapping;
using Queries.GetCompanyList.Models;

public class Module : BaseModule
{
    public Module(bool registerSingleInstance)
        : base(registerSingleInstance)
    {
    }

    /// <inheritdoc />
    protected override void Load(ContainerBuilder builder)
    {
        // IGetCompanyListQuery
        builder.RegisterService<IGetCompanyListQuery, GetCompanyListQuery>(RegisterSingleInstance);
        builder
            .RegisterService<IEntityQueryService<Company, CompanyListModel, Guid>,
                EntityQueryService<Company, CompanyListModel, Guid>>(RegisterSingleInstance)
            .WithParameter(Constants.ServiceParameters.Mapper, CompanyMapper.Instance);

        // ICompanyExistsQuery
        builder.RegisterService<ICompanyExistsQuery, CompanyExistsQuery>(RegisterSingleInstance);
        builder
            .RegisterService<IEntityQueryService<Company, CompanyExistsModel, Guid>,
                EntityQueryService<Company, CompanyExistsModel, Guid>>(RegisterSingleInstance)
            .WithParameter(Constants.ServiceParameters.Mapper, CompanyMapper.Instance);
    }
}