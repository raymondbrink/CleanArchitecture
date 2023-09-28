using Example.Application.Manufacturer.Configuration;
using Example.Application.Manufacturer.Commands.AddManufacturer;
using Example.Application.Manufacturer.Commands.AddManufacturer.Models;

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
                    options.RegisterRepository<Manufacturer, Guid>();
                })
            .AddApplicationManufacturerDependencies();
    })
    .Build();

// Create manufacturer model.
var manufacturerName = $"My Manufacturer ({DateTime.Now:yyyyMMddHHmmsssmmm})";
var manufacturerToAdd = new AddManufacturerCommandModel(manufacturerName)
{
    Contact =
    {
        FamilyName = "Brink",
        GivenName = "Raymond" // Optional
    }
};

// Execute add manufacturer command.
var result = await host.Services.GetRequiredService<IAddManufacturerCommand>().ExecuteAsync(manufacturerToAdd);

Console.WriteLine($"Added: {result}: {manufacturerToAdd.ManufacturerName}");
Console.WriteLine();
