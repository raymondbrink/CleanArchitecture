namespace Example.Application.Supplier.Commands.AddSupplier.Repository;

using Domain.Entities;

using NetActive.CleanArchitecture.Application.Extensions;
using NetActive.CleanArchitecture.Application.Interfaces;

internal class AddSupplierRepositoryFacade : IAddSupplierRepositoryFacade
{
    private readonly IRepository<Supplier, Guid> _repo;

    public AddSupplierRepositoryFacade(IRepository<Supplier, Guid> repo)
    {
        _repo = repo;
    }

    public Task<bool> SupplierExistsAsync(string name)
    {
        return _repo.ExistsAsync(s => s.Name.Equals(name));
    }

    public void AddSupplier(Supplier supplier)
    {
        _repo.Add(supplier);
    }
}