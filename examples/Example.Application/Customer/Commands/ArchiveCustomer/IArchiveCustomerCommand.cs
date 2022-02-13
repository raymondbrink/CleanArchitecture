namespace Example.Application.Customer.Commands.ArchiveCustomer;

public interface IArchiveCustomerCommand
{
    Task ExecuteAsync(int customerId, string archivedBy);
}