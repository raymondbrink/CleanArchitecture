namespace Example.Application.Supplier.Queries.GetSupplierList.Models;

using NetActive.CleanArchitecture.Application.Interfaces;

public class CompanyListModel : IModel<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}