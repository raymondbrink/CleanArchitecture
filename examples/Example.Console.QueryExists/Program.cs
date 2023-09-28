using Example.Application.Company.Configuration;
using Example.Application.Company.Queries.CompanyExists;

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

// Determine if a company with a specific name exists.
var companyToFind = "some company";
var companyWithNameExists = await host.Services.GetRequiredService<ICompanyExistsQuery>().ExecuteAsync(companyToFind);

Console.WriteLine($"Company '{companyToFind}' exists: {companyWithNameExists}");
Console.WriteLine();
