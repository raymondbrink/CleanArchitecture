using Autofac;

using Example.Application.Manufacturer.Commands.AddManufacturer;
using Example.Application.Manufacturer.Commands.AddManufacturer.Models;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    // Create manufacturer model.
    var manufacturerName = $"My Manufacturer ({DateTime.Now:yyyyMMddHHmmsssmmm})";
    var manufacturerToAdd = new AddManufacturerCommandModel
        {
            ManufacturerName = manufacturerName,
            Contact =
                {
                    FamilyName = "Raymond",
                    GivenName = "Brink"
                }
        };

    // Resolve and execute add manufacturer command.
    var newManufacturerId = await scope.Resolve<IAddManufacturerCommand>().ExecuteAsync(manufacturerToAdd);

    Console.WriteLine($"Added: {newManufacturerId}: {manufacturerToAdd.ManufacturerName}");
    Console.WriteLine();
}