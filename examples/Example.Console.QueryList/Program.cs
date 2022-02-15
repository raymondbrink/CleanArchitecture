using Autofac;

using Example.Application.Supplier.Queries.GetSupplierList;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    // List all suppliers.
    var suppliers = await scope.Resolve<IGetSupplierListQuery>().ExecuteAsync();
    foreach (var supplier in suppliers)
    {
        Console.WriteLine($"{supplier.Id}: {supplier.Name}");
    }

    Console.WriteLine();
}