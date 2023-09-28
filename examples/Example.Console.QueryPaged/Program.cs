using Example.Application.Company.Configuration;
using Example.Application.Company.Queries.GetPageOfCompanies;
using Example.Application.Company.Queries.GetPageOfCompanies.Models;
using Example.Application.Interfaces.Persistence;
using Example.Domain.Entities;
using Example.Persistence;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Configuration;

// Build a host.
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        // Wire up our clean architecture dependencies.
        services
            .AddPersistenceDependencies<ExampleDbContext, IExampleUnitOfWork, ExampleUnitOfWork>(
                hostContext.Configuration.GetConnectionString("ExampleDbConnection1"),
                useLazyLoadingProxies: false, 
                options =>
                {
                    options.RegisterRepository<Company, Guid>();
                })
            .AddApplicationCompanyDependencies();
    })
    .Build();

var query = host.Services.GetRequiredService<IGetPageOfCompaniesQuery>();

// Get first page of (max 3) companies, sorted by their name.
var parameters = new CompanyQueryParams { PageSize = 3, SortBy = CompanySortBy.Name };
var pageOfCompanies = await query.ExecuteAsync(parameters);
                
Console.WriteLine($"Found {pageOfCompanies.ItemCount} matching companies in total.");

while (pageOfCompanies.PageIndex < pageOfCompanies.PageCount)
{
    Console.WriteLine(
        $"Listing {pageOfCompanies.PageOfItems.Count} companies on page {pageOfCompanies.PageNumber} of {pageOfCompanies.PageCount}:");
    foreach (var company in pageOfCompanies.PageOfItems)
    {
        Console.WriteLine($"{company.Id}: {company.Name}");
    }

    if (!pageOfCompanies.HasNextPage())
    {
        // No need to re-query for next page.
        break;
    }

    // Re-query for next page of (max 3) companies.
    parameters.PageIndex++;
    pageOfCompanies = await query.ExecuteAsync(parameters);
}

Console.WriteLine();
