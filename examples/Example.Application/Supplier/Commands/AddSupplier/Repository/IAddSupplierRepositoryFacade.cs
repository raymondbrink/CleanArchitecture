namespace Example.Application.Supplier.Commands.AddSupplier.Repository;

using Domain.Entities;

public interface IAddSupplierRepositoryFacade
{
    /// <summary>
    /// Returns a boolean value indicating whether a supplier with the given name exists.
    /// </summary>
    /// <param name="externalReference">External reference of the supplier.</param>
    /// <returns>Boolean value indicating whether a supplier with the given external reference exists.</returns>
    Task<bool> SupplierExistsAsync(string externalReference);

    /// <summary>
    /// Adds the given supplier to the database.
    /// </summary>
    /// <param name="supplier">Supplier to add.</param>
    void AddSupplier(Supplier supplier);
}