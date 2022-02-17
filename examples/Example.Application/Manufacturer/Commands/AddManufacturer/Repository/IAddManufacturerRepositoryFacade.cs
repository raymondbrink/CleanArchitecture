namespace Example.Application.Manufacturer.Commands.AddManufacturer.Repository;

using Domain.Entities;

public interface IAddManufacturerRepositoryFacade
{
    /// <summary>
    /// Returns a boolean value indicating whether a manufacturer with the given name exists.
    /// </summary>
    /// <param name="externalReference">External reference of the manufacturer.</param>
    /// <returns>Boolean value indicating whether a manufacturer with the given external reference exists.</returns>
    Task<bool> ManufacturerExistsAsync(string externalReference);

    /// <summary>
    /// Adds the given manufacturer to the database.
    /// </summary>
    /// <param name="manufacturer">Manufacturer to add.</param>
    void AddManufacturer(Manufacturer manufacturer);
}