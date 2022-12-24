using Autofac;

using Example.Application.Manufacturer.Queries.GetManufacturerList;
using MediatR;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    // List all manufacturers.
    var query = new GetManufacturerListQuery();
    var result = await scope.Resolve<ISender>().Send(query);

    foreach (var manufacturer in result.Manufacturers)
    {
        Console.WriteLine($"{manufacturer.Id}: {manufacturer.Name}");
    }

    Console.WriteLine();
}