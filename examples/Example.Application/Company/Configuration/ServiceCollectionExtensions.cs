namespace Example.Application.Company.Configuration
{
    using Domain.Entities;

    using Queries.CompanyExists;
    using Queries.GetCompany;
    using Queries.GetCompany.Mapping;
    using Queries.GetCompany.Models;
    using Queries.GetPageOfCompanies;
    using Queries.GetPageOfCompanies.Mapping;
    using Queries.GetPageOfCompanies.Models;

    using Microsoft.Extensions.DependencyInjection;

    using NetActive.CleanArchitecture.Application.Configuration;
    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Configuration;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCompanyDependencies(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            // IGetCompanyQuery
            services.AddService<IGetCompanyQuery, GetCompanyQuery>(lifetime);
            services.AddEntityQueryService<Company, CompanyDetailModel, Guid>(CompanyDetailMapper.Instance, lifetime);

            // IGetPageOfCompaniesQuery
            services.AddService<IGetPageOfCompaniesQuery, GetPageOfCompaniesQuery>(lifetime);
            services.AddEntityQueryService<Company, CompanyListModel, Guid>(CompanyListMapper.Instance, lifetime);

            // ICompanyExistsQuery
            services.AddService<ICompanyExistsQuery, CompanyExistsQuery>(lifetime);
            services.AddEntityExistsService<Company, Guid>(lifetime);

            return services;
        }
    }
}