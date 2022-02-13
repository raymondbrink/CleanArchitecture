namespace Example.Application.Supplier.Commands.DeleteSupplier;

public interface IDeleteSupplierCommand
{
    /// <summary>
    /// Deletes an existing supplier.
    /// </summary>
    /// <param name="supplierId">Id of the supplier to delete.</param>
    Task ExecuteAsync(Guid supplierId);
}