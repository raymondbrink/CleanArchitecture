namespace Example.Application.Manufacturer.Commands.AddManufacturer.Models;

public class AddManufacturerCommandModel
{
    public AddManufacturerCommandModel(string manufacturerName)
    {
        ManufacturerName = manufacturerName;
        Contact = new ManufacturerContactModel();
    }

    public string ManufacturerName { get; set; }

    public ManufacturerContactModel Contact { get; set; }
}