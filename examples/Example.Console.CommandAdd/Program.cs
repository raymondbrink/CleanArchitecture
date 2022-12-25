using Autofac;
using Example.Application.Manufacturer.Commands.AddManufacturer;
using Example.Application.Manufacturer.Commands.AddManufacturer.Models;
using MediatR;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
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
    var command = new AddManufacturerCommand(manufacturerToAdd);
    var result = await scope.Resolve<ISender>().Send(command);

    Console.WriteLine($"Added: {result}: {manufacturerToAdd.ManufacturerName}");
    Console.WriteLine();
}