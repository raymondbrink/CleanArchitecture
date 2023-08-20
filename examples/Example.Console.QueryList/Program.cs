using Example.Application.Interfaces.Persistence;
using Example.Application.Manufacturer.Configuration;
using Example.Application.Manufacturer.Queries.GetManufacturerList;
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
                options =>
                {
                    options.RegisterEfRepository<Manufacturer, Guid>();
                })
            .AddApplicationManufacturerDependencies();
    })
    .Build();

// List all manufacturers.
var result = await host.Services.GetRequiredService<IGetManufacturerListQuery>().ExecuteAsync();

foreach (var manufacturer in result)
{
    Console.WriteLine($"{manufacturer.Id}: {manufacturer.Name}");
}

Console.WriteLine();
