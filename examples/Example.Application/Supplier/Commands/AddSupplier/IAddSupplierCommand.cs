namespace Example.Application.Supplier.Commands.AddSupplier;

using Models;

public interface IAddSupplierCommand
{
    /// <summary>
    /// Adds a new supplier.
    /// </summary>
    /// <param name="model">Supplier model.</param>
    /// <returns>Id of the newly added supplier.</returns>
    Task<Guid> ExecuteAsync(AddSupplierCommandModel model);
}