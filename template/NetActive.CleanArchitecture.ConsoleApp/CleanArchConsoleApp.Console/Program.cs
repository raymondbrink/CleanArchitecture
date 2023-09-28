using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Configuration;

using CleanArchConsoleApp.Application.Interfaces.Persistence;
using CleanArchConsoleApp.Application.FeatureName.Configuration;
using CleanArchConsoleApp.Application.FeatureName.Queries.GetPageOfMyEntities;
using CleanArchConsoleApp.Application.FeatureName.Queries.FeatureNameExists;
using CleanArchConsoleApp.Domain.Entities;
using CleanArchConsoleApp.Persistence;

// Build a host.
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        // Wire up our clean architecture dependencies.
        services
            .AddPersistenceDependencies<ApplicationDbContext, IApplicationUnitOfWork, ApplicationUnitOfWork>(
                "Server=(localdb)\\MSSQLLocalDB;Database=CleanArchConsoleApp;Integrated Security=true;MultipleActiveResultSets=true;",
                useLazyLoadingProxies: false, 
                options =>
                {
                    options.RegisterEfRepository<FeatureName, Guid>();
                })
            .AddApplicationFeatureNameDependencies();
    })
    .Build();   

// List all entities.
var myEntities = await host.Services.GetRequiredService<IGetPageOfMyEntitiesQuery>().ExecuteAsync();
foreach (var item in myEntities.PageOfItems)
{
	Console.WriteLine($"{item.Id}\t{item.Name}");
}
Console.WriteLine();

// Determine if an entity with a specific name exists.
var FeatureNameToFind = "some entity";
var FeatureNameWithNameExists = await host.Services.GetRequiredService<IFeatureNameExistsQuery>().ExecuteAsync(FeatureNameToFind);
Console.WriteLine($"Entity '{FeatureNameToFind}' exists: {FeatureNameWithNameExists}");

Console.WriteLine();
