namespace Example.Application.Customer.Commands.ArchiveCustomer
{
    using Domain.Entities;

    using Interfaces.Persistence;

    using NetActive.CleanArchitecture.Application.Exceptions;

    using Repository;

    internal class ArchiveCustomerCommand : IArchiveCustomerCommand
    {
        private readonly IArchiveCustomerRepositoryFacade _repositories;
        private readonly IExampleUnitOfWork _unitOfWork;

        public ArchiveCustomerCommand(IArchiveCustomerRepositoryFacade repositories, IExampleUnitOfWork unitOfWork)
        {
            _repositories = repositories;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int customerId, string archivedBy)
        {
            if (string.IsNullOrWhiteSpace(archivedBy))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(archivedBy));
            }

            var customer = await _repositories.GetAsync(customerId);
            if (customer == null)
            {
                throw new EntityNotFoundException(typeof(Customer), customerId);
            }

            _repositories.Archive(customer, archivedBy);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}