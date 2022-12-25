using Autofac;

using Example.Application.Company.Queries.GetPageOfCompanies;
using Example.Application.Company.Queries.GetPageOfCompanies.Models;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    var query = scope.Resolve<IGetPageOfCompaniesQuery>();

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
}