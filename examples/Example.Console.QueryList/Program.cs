using Autofac;

using Example.Application.Manufacturer.Queries.GetManufacturerList;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    // List all manufacturers.
    var manufacturers = await scope.Resolve<IGetManufacturerListQuery>().ExecuteAsync();
    foreach (var manufacturer in manufacturers)
    {
        Console.WriteLine($"{manufacturer.Id}: {manufacturer.Name}");
    }

    Console.WriteLine();
}