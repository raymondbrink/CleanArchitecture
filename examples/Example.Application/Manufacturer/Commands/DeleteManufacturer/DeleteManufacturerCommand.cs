namespace Example.Application.Manufacturer.Commands.DeleteManufacturer
{
    using Domain.Entities;
    using Interfaces.Persistence;
    using Repository;

    using NetActive.CleanArchitecture.Application.Exceptions;

    public class DeleteManufacturerCommand 
        : IDeleteManufacturerCommand
    {
        private readonly IDeleteManufacturerRepositoryFacade _repositories;
        private readonly IExampleUnitOfWork _unitOfWork;

        public DeleteManufacturerCommand(
            IDeleteManufacturerRepositoryFacade repositories,
            IExampleUnitOfWork unitOfWork)
        {
            _repositories = repositories;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Deletes the given manufacturer from the underlying data store.
        /// </summary>
        /// <param name="manufacturerId">Id of the manufacture te delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public async Task ExecuteAsync(Guid manufacturerId, CancellationToken? cancellationToken = null)
        {
            // Find manufacturer instance.
            var manufacturer = await _repositories.GetAsync(manufacturerId);
            if (manufacturer == null)
            {
                throw new EntityNotFoundException(typeof(Manufacturer), manufacturerId);
            }

            // Delete manufacturer from repo.
            _repositories.Delete(manufacturer);

            // Commit changes.
            await _unitOfWork.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        }
    }
}
