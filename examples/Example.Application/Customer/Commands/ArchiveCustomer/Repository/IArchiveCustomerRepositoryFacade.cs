namespace Example.Application.Customer.Commands.ArchiveCustomer.Repository;

using Domain.Entities;

public interface IArchiveCustomerRepositoryFacade
{
    /// <summary>
    /// Gets the specified customer.
    /// </summary>
    /// <param name="customerId">Id of the customer.</param>
    /// <returns>Customer</returns>
    Task<Customer> GetAsync(int customerId);

    /// <summary>
    /// Archives the given customer.
    /// </summary>
    /// <param name="customer">Customer to archive.</param>
    /// <param name="by"></param>
    void Archive(Customer customer, string @by);
}