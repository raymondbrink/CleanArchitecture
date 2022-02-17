namespace Example.Application.Manufacturer.Commands.DeleteManufacturer.Repository;

using Domain.Entities;

public interface IDeleteManufacturerRepositoryFacade
{
    /// <summary>
    /// Gets a manufacturer by its Id.
    /// </summary>
    /// <param name="id">Id of the manufacturer.</param>
    /// <returns>Manufacturer instance.</returns>
    Task<Manufacturer> GetAsync(Guid id);

    /// <summary>
    /// Deletes the given manufacturer from the database.
    /// </summary>
    /// <param name="manufacturer">Manufacturer to delete.</param>
    void Delete(Manufacturer manufacturer);
}