namespace Example.Application.Manufacturer.Commands.DeleteManufacturer
{
    using Domain.Entities;

    using Repository;

    using MediatR;
    using NetActive.CleanArchitecture.Application.Exceptions;
    using NetActive.CleanArchitecture.Application.MediatR.Abstractions.Commands;

    using System.Threading;
    using System.Threading.Tasks;
    using Example.Application.Interfaces.Persistence;

    internal sealed class DeleteManufacturerCommandHandler : BaseDeleteCommandHandler<DeleteManufacturerCommand>
    {
        private readonly IDeleteManufacturerRepositoryFacade _repositories;

        public DeleteManufacturerCommandHandler(
            IDeleteManufacturerRepositoryFacade repositories, 
            IExampleUnitOfWork unitOfWork, 
            IPublisher publisher) 
            : base(unitOfWork, publisher)
        {
            _repositories = repositories;
        }

        public override async Task<Unit> Handle(DeleteManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer = await _repositories.GetAsync(request.ManufacturerId);
            if (manufacturer == null)
            {
                throw new EntityNotFoundException(typeof(Manufacturer), request.ManufacturerId);
            }

            _repositories.Delete(manufacturer);

            await SaveChangesAsync(cancellationToken);

            await PublishNotificationAsync(manufacturer.Id, cancellationToken);

            return Unit.Value;
        }
    }
}
