namespace Example.Application.Customer.Configuration
{
    using Domain.Entities;

    using Commands.ArchiveCustomer;
    using Commands.ArchiveCustomer.Repository;

    using Queries.GetCustomerList;
    using Queries.GetCustomerList.Mapping;
    using Queries.GetCustomerList.Models;

    using Microsoft.Extensions.DependencyInjection;

    using NetActive.CleanArchitecture.Application.Configuration;
    using NetActive.CleanArchitecture.Application.EntityFrameworkCore.Configuration;

    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationCompanyDependencies(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            // IGetCustomerListQuery
            services.AddService<IGetCustomerListQuery, GetCustomerListQuery>(lifetime);
            services.AddEntityQueryService<Customer, CustomerListModel, int>(CustomerMapper.Instance, lifetime);

            // IArchiveCustomerCommand
            services.AddService<IArchiveCustomerCommand, ArchiveCustomerCommand>(lifetime);
            services.AddService<IArchiveCustomerRepositoryFacade, ArchiveCustomerRepositoryFacade>(lifetime);
        }
    }
}