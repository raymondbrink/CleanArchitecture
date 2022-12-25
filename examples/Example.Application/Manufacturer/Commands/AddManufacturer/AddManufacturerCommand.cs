namespace Example.Application.Manufacturer.Commands.AddManufacturer
{
    using Models;

    using NetActive.CleanArchitecture.Application.MediatR.Interfaces;

    /// <summary>
    /// Adds the given manufacturer to the underlying data store.
    /// </summary>
    /// <param name="model">Manufacturer to add.</param>
    /// <returns>Id of the added manufacturer.</returns>
    public sealed record AddManufacturerCommand(AddManufacturerCommandModel model) : ICommand<Guid>;
}
