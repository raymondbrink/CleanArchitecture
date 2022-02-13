namespace Example.Application.Supplier.Commands.DeleteSupplier.Repository;

using Domain.Entities;

public interface IDeleteSupplierRepositoryFacade
{
    /// <summary>
    /// Gets a supplier by its Id.
    /// </summary>
    /// <param name="id">Id of the supplier.</param>
    /// <returns>Supplier instance.</returns>
    Task<Supplier> GetAsync(Guid id);

    /// <summary>
    /// Deletes the given supplier from the database.
    /// </summary>
    /// <param name="supplier">Supplier to delete.</param>
    void Delete(Supplier supplier);
}