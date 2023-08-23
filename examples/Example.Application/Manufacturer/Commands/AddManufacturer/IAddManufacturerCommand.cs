namespace Example.Application.Manufacturer.Commands.AddManufacturer
{
    using Models;

    using System;
    using System.Threading.Tasks;

    public interface IAddManufacturerCommand
    {
        /// <summary>
        /// Adds the given manufacturer to the underlying data store.
        /// </summary>
        /// <param name="model">Manufacturer to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Id of the added manufacturer.</returns>
        Task<Guid> ExecuteAsync(AddManufacturerCommandModel model, CancellationToken? cancellationToken = null);
    }
}