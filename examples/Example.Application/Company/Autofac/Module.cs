namespace Example.Application.Company.Autofac;

using Domain.Entities;

using global::Autofac;

using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Services;
using NetActive.CleanArchitecture.Application.Interfaces;
using NetActive.CleanArchitecture.Autofac;
using NetActive.CleanArchitecture.Autofac.Extensions;

using Queries.CompanyExists;
using Queries.CompanyExists.Models;
using Queries.GetCompany;
using Queries.GetCompany.Mapping;
using Queries.GetCompany.Models;
using Queries.GetPageOfCompanies;
using Queries.GetPageOfCompanies.Mapping;
using Queries.GetPageOfCompanies.Models;

public class Module : BaseModule
{
    public Module(bool registerSingleInstance)
        : base(registerSingleInstance)
    {
    }

    /// <inheritdoc />
    protected override void Load(ContainerBuilder builder)
    {
        // IGetCompanyQuery
        builder.RegisterService<IGetCompanyQuery, GetCompanyQuery>(RegisterSingleInstance);
        builder.RegisterService<IEntityQueryService<Company, CompanyDetailModel, Guid>,
                EntityQueryService<Company, CompanyDetailModel, Guid>>(RegisterSingleInstance)
            .WithParameter(Constants.ServiceParameters.Mapper, CompanyDetailMapper.Instance);

        // IGetPageOfCompaniesQuery
        builder.RegisterService<IGetPageOfCompaniesQuery, GetPageOfCompaniesQuery>(RegisterSingleInstance);
        builder.RegisterService<IEntityQueryService<Company, CompanyListModel, Guid>,
                EntityQueryService<Company, CompanyListModel, Guid>>(RegisterSingleInstance)
            .WithParameter(Constants.ServiceParameters.Mapper, CompanyListMapper.Instance);

        // ICompanyExistsQuery
        builder.RegisterService<ICompanyExistsQuery, CompanyExistsQuery>(RegisterSingleInstance);
        builder
            .RegisterService<IEntityQueryService<Company, CompanyExistsModel, Guid>,
                EntityQueryService<Company, CompanyExistsModel, Guid>>(RegisterSingleInstance)
            .WithParameter(Constants.ServiceParameters.Mapper, CompanyListMapper.Instance);
    }
}