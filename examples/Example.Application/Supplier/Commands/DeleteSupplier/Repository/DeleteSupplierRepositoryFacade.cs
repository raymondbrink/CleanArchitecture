namespace Example.Application.Supplier.Commands.DeleteSupplier.Repository;

using Domain.Entities;

using NetActive.CleanArchitecture.Application.Interfaces;

internal class DeleteSupplierRepositoryFacade : IDeleteSupplierRepositoryFacade
{
    private readonly IRepository<Supplier, Guid> _repo;

    public DeleteSupplierRepositoryFacade(IRepository<Supplier, Guid> repo)
    {
        _repo = repo;
    }

    public Task<Supplier> GetAsync(Guid id)
    {
        return _repo.GetAsync(id);
    }

    public void Delete(Supplier supplier)
    {
        _repo.Remove(supplier);
    }
}