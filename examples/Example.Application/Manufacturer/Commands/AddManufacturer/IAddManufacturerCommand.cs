namespace Example.Application.Manufacturer.Commands.AddManufacturer;

using Models;

public interface IAddManufacturerCommand
{
    /// <summary>
    /// Adds a new manufacturer.
    /// </summary>
    /// <param name="model">Manufacturer model.</param>
    /// <returns>Id of the newly added manufacturer.</returns>
    Task<Guid> ExecuteAsync(AddManufacturerCommandModel model);
}