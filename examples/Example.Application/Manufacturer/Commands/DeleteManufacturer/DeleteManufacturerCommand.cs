namespace Example.Application.Manufacturer.Commands.DeleteManufacturer
{
    using MediatR;

    using NetActive.CleanArchitecture.Application.MediatR.Interfaces;

    /// <summary>
    /// Deletes the given manufacturer from the underlying data store.
    /// </summary>
    /// <param name="ManufacturerId">Id of the manufacture te delete.</param>
    public sealed record DeleteManufacturerCommand(Guid ManufacturerId) : ICommand<Unit>;
}
