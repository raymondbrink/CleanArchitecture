namespace Example.Application.Supplier.Commands.DeleteSupplier;

using Domain.Entities;

using Interfaces.Persistence;

using NetActive.CleanArchitecture.Application.Exceptions;

using Repository;

internal class DeleteSupplierCommand : IDeleteSupplierCommand
{
    private readonly IDeleteSupplierRepositoryFacade _repositories;
    private readonly IExampleUnitOfWork _unitOfWork;

    public DeleteSupplierCommand(IDeleteSupplierRepositoryFacade repositories,
        IExampleUnitOfWork unitOfWork)
    {
        _repositories = repositories;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid supplierId)
    {
        var supplier = await _repositories.GetAsync(supplierId);
        if (supplier == null)
        {
            throw new EntityNotFoundException(typeof(Supplier), supplierId);
        }

        _repositories.Delete(supplier);

        await _unitOfWork.SaveChangesAsync();
    }
}