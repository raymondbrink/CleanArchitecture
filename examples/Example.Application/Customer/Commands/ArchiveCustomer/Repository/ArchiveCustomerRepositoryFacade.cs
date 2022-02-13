namespace Example.Application.Customer.Commands.ArchiveCustomer.Repository;

using Domain.Entities;

using NetActive.CleanArchitecture.Application.Interfaces;

internal class ArchiveCustomerRepositoryFacade : IArchiveCustomerRepositoryFacade
{
    private readonly IArchivableRepository<Customer, int> _repo;

    public ArchiveCustomerRepositoryFacade(IArchivableRepository<Customer, int> repo)
    {
        _repo = repo;
    }

    public Task<Customer> GetAsync(int customerId)
    {
        return _repo.GetAsync(customerId);
    }

    public void Archive(Customer customer, string by)
    {
        _repo.Archive(customer, by);
    }
}