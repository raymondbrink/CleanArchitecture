namespace Example.Application.Customer.Queries.GetCustomerList.Models;

using NetActive.CleanArchitecture.Application.Interfaces;

public class CustomerListModel : IModel<int>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? ArchivedAtUtc { get; set; }

    public string ArchivedBy { get; set; }
}