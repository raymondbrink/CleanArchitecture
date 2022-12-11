namespace Example.Application.Customer.Queries.GetCustomerList.Models;

using NetActive.CleanArchitecture.Application.Interfaces;

public class CustomerListModel : IModel<int>
{
    public int Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public DateTime? ArchivedAtUtc { get; set; }

    public string? ArchivedBy { get; set; }
}