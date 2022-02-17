namespace Example.Application.Manufacturer.Queries.GetManufacturerList.Models;

using NetActive.CleanArchitecture.Application.Interfaces;

public class CompanyListModel : IModel<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}