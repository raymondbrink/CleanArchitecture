namespace Example.Application.Supplier.Commands.AddSupplier.Repository;

using Domain.Entities;

using NetActive.CleanArchitecture.Application.Interfaces;
using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore.Autofac;

internal class AddSupplierRepositoryFacade : IAddSupplierRepositoryFacade
{
    private readonly IRepository<Supplier, Guid> _repo;

    public AddSupplierRepositoryFacade(IRepository<Supplier, Guid> repo)
    {
        _repo = repo;
    }

    /// <inheritdoc />
    public Task<bool> SupplierExistsAsync(string name) => _repo.ExistsAsync(s => s.Name.Equals(name));

    /// <inheritdoc />
    public void AddSupplier(Supplier supplier) => _repo.Add(supplier);
}