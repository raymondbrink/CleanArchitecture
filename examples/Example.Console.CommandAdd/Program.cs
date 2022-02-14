using Autofac;

using Example.Application.Supplier.Commands.AddSupplier;
using Example.Application.Supplier.Commands.AddSupplier.Models;

// Build single-instance DI container.
var builder = new ContainerBuilder();
Example.Shared.AutofacConfig.RegisterComponents(builder, singleInstance: true);
var container = builder.Build();

using (var scope = container.BeginLifetimeScope())
{
    // Create supplier model.
    var supplierName = $"My Supplier ({DateTime.Now:yyyyMMddHHmmsssmmm})";
    var supplierToAdd = new AddSupplierCommandModel
        {
            SupplierName = supplierName,
            Contact =
                {
                    FamilyName = "Raymond",
                    GivenName = "Brink"
                }
        };

    // Resolve add command.
    var addSupplierCommand = scope.Resolve<IAddSupplierCommand>();

    // Execute add command.
    var newSupplierId = await addSupplierCommand.ExecuteAsync(supplierToAdd);

    Console.WriteLine($"Added: {newSupplierId}: {supplierToAdd.SupplierName}");
    Console.WriteLine();
}