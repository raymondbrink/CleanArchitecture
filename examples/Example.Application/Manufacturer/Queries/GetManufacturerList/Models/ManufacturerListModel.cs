namespace Example.Application.Manufacturer.Queries.GetManufacturerList.Models;

public class ManufacturerListModel 
    : CompanyListModel
{
    public PersonModel? Contact { get; set; }
}